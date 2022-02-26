using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;


namespace The6Bits.BitOHealth.ManagerLayer;

public class AccountManager
{
    private IAuthenticationService _authentication;
    private AccountService _AS;


    public AccountManager(IRepositoryAuth<string> authdao, IAuthenticationService authenticationService)
    {
        _authentication = authenticationService;
        _AS = new AccountService(authdao);
    }

    
    //TODO:Safer parse int
    public string Login(LoginModel acc)
    {
        bool failedAttemptsNeedsDelete = false;
        
        //CHECK IF USERNAME EXISTS
        string us = _AS.UsernameExists(acc.Username);
        if (us != "username exists")
        {
            return us;
        }
        
        //CHECK IF ACCOUNT IS ENABLED

        string isenabled = _AS.IsEnabled(acc.Username);
        
        string firstfaildate = _AS.CheckFailDate(acc.Username);
        
        if (firstfaildate != "none")
        {
            
            failedAttemptsNeedsDelete = DateTime.Parse(firstfaildate).AddDays(1) < DateTime.UtcNow;

        }



        if (failedAttemptsNeedsDelete)
        {
            string del = _AS.DeleteFailedAttempts(acc.Username);

        }

        if (isenabled != "enabled")
        {

            //CHECK IF HAS BEEN 24 HRS
            if (failedAttemptsNeedsDelete)
            {
                //ENABLE ACCOUNT
                string res = _AS.UpdateIsEnabled(acc.Username, 1);
                if ( res!= "account updated")
                {
                    return res;
                }


            }
            else
            {
                //still disabled
                return isenabled;
            }

        }
        
        //VALIDATE OTP

        string otp = _AS.ValidateOTP(acc.Username, acc.Code);
        
        
        string cp = _AS.CheckPassword(acc.Username, acc.Password);
        
        if (otp != "valid" || cp != "credentials found")
        {
            //UPDATE FAILED ATTEMPT
            string attempts = _AS.CheckFailedAttempts(acc.Username);
            if (attempts == "0")
            {
                _AS.InsertFailedAttempts(acc.Username);
            }
            else
            {
                int newFailedAttempts = Int32.Parse(attempts);
                newFailedAttempts += 1;
                _AS.UpdateFailedAttempts(acc.Username, newFailedAttempts);

                if (newFailedAttempts >= 5)
                {
                    string disabled = _AS.UpdateIsEnabled(acc.Username, 0);
                    if (disabled == "account updated")
                    {
                        return "account disabled";
                    }
                    //db error 
                    return disabled;


                }
            }
            
            
            return otp != "valid" ? otp : cp;
        }

        
        return _authentication.generateToken(acc.Username);
    }

    public bool isTokenValid(string token)
    {
        return _authentication.ValidateToken(token);
    }
    public string recoverAccount(AccountRecoveryModel arm)
    {
        string ra = _AS.UsernameAndEmailExists(arm.Username, arm.Email);
        if (ra != "Email and Username found")
        {
            return ra;
        }

        string enabled = _AS.IsEnabled(arm.Username);
        if(enabled != "enabled")
        {
            return "disabled account";
        }
        string recoveryValidation = _AS.ValidateRecoveryAttempts(arm.Username);
        if(recoveryValidation != "under")
        {
            return recoveryValidation;
        }
        string r = _AS.GenerateRandomString();
        string email = _AS.SendEmail("angelcueva47@gmail.com", "Bit O Health Recovery", "Please click URL within 24 hours to recover your account" +
            "\n https://localhost:7011/Account/ResetPassword?r=" + r + "&u=" + arm.Username);
        DateTime dateTime = DateTime.Now;

        if (email != "email sent")
        {
            return email;
        }
        string updateRecoveryAttempts = _AS.UpdateRecoveryAttempts(arm.Username);

        if (updateRecoveryAttempts != "1")
        {
            return updateRecoveryAttempts;
        }
       
        string saveCode = _AS.SaveActivationCode(arm.Username, dateTime, r, "Recovery");
        if (saveCode != "1")
        {
            _AS.DeletePastOTP(arm.Username, "Recovery");
            _AS.SaveActivationCode(arm.Username, dateTime, r, "Recovery");
        }
        return "Recovery Link Sent To Email: " + arm.Email;
    }

    


    public string SendOTP(string username)
    {
        string usernameExists = _AS.UsernameExists(username);

        if (usernameExists != "username exists")
        {
            return usernameExists;
        }

        string email = _AS.GetEmail(username);
        //TODO:SEND CODE
        Random rnd = new Random();
        string code  = rnd.Next(1000, 9999).ToString();
        
        //SEND CODE

        string del = _AS.DeletePastOTP(username,"OTP");

        string save = _AS.SaveActivationCode(username, DateTime.UtcNow, code, "OTP");
        
        return code;


    }

    public string DeleteFailedAttempts(string username)
    {
        return _AS.DeleteFailedAttempts(username);
    }
    public string ResetPassword(string u, string r, string p)
    {
        string validateOTP =  _AS.ValidateOTP(u, r);
        if(validateOTP != "valid")
        {
            return validateOTP;
        }
        string sameDay = _AS.VerifySameDay(u, r);
        if(sameDay != "1")
        {
            return "failed" ;
        }
        string reset = _AS.ResetPassword(p, u);
        if (reset != "1")
        {
            return "password failed to reset";
        }
        return "Account Recovered Successfully";

    }
  
}
