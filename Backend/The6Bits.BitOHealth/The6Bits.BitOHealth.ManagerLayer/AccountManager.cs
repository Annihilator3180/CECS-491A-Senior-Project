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

    
    //TODO:DELETE  OTP AFTER SUCCESS 
    //DO AFTER EVERONE DONE
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
        
        DateTime temp;
        if (DateTime.TryParse(firstfaildate, out temp))
        {
            //IF HAS DATE IN DB THEN CHECK IF NEEDS TO BE DELETED
            failedAttemptsNeedsDelete = temp.AddDays(1) < DateTime.UtcNow;

        }
        else if (firstfaildate != "none")
        {
            //DB ERROR
            return firstfaildate;
        }



        if (failedAttemptsNeedsDelete)
        {
            string del = _AS.DeleteFailedAttempts(acc.Username);
            //ERROR CASE RETURN ERROR
            if (del != "1")
            {
                return del;
            }
        }

        if (isenabled != "enabled")
        {

            //IF SHOULD BE DELETED THEN UPDATE
            if (failedAttemptsNeedsDelete || firstfaildate == "none")
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
        
        
        string checkPassword = _AS.CheckPassword(acc.Username, acc.Password);
        
        if (otp != "valid" || checkPassword != "credentials found")
        {
            //UPDATE FAILED ATTEMPT
            string attempts = _AS.CheckFailedAttempts(acc.Username);
            int attemptInt;
            
            //CHECK FOR ERROR
            if (Int32.TryParse(attempts, out attemptInt))
            {
                if (attemptInt == 0)
                {
                    string insertFail = _AS.InsertFailedAttempts(acc.Username);
                    if (insertFail != "1")
                    {
                        return insertFail;
                    }
                }
                else
                {
                    attemptInt += 1;
                    string updateFail =_AS.UpdateFailedAttempts(acc.Username, attemptInt);
                    if (updateFail != "updated attempts")
                    {
                        return updateFail;
                    }
                    if (attemptInt >= 5)
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

           
            }
            else
            {
                //DB ERROR
                return attempts;
            }
            
            

            //DB ERRORS && INVALID PASS AND OTP RETURN
            return otp != "valid" ? otp : checkPassword;
        }

        
        return _authentication.generateToken(acc.Username);
    }

    public bool isTokenValid(string token)
    {
        return _authentication.ValidateToken(token);
    }

    public string SendOTP(string username)
    {
        string usernameExists = _AS.UsernameExists(username);

        if (usernameExists != "username exists")
        {
            return usernameExists;
        }

        string email = _AS.GetEmail(username);
        if(!email.Contains("@"))
        {
            return email;
        }
        //TODO:SEND CODE
        Random rnd = new Random();
        string code = "";
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        foreach (var i in Enumerable.Range(0, 10))
        {
            code+=chars[rnd.Next(0, 62)];
        }

        
        //SEND CODE

        string del = _AS.DeletePastOTP(username,"OTP");
        if (!del.Contains("deleted"))
        {
            return del;
        }
        
        string save = _AS.SaveActivationCode(username, DateTime.UtcNow, code, "OTP");
        if (save != "saved")
        {
            return save;
        }

        return code;


    }

    public string DeleteFailedAttempts(string username)
    {
        return _AS.DeleteFailedAttempts(username);
    }
}