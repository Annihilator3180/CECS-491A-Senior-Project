using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;


namespace The6Bits.BitOHealth.ManagerLayer;

public class AccountManager
{
    private IAuthenticationService _authentication;
    private AccountService _AS;
    

    public AccountManager( IRepositoryAuth<string> authdao, IAuthenticationService authenticationService)
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

    public string HasToken(string token)
    {
        if (token != null)
            return "Token exists";
        else
            return "Token Not Found";
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
}