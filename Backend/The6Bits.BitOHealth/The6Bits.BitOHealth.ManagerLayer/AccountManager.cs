using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;
using The6Bits.EmailService;


namespace The6Bits.BitOHealth.ManagerLayer;

public class AccountManager
{
    private IAuthenticationService _authentication;
    private AccountService _AS;
    private IDBErrors _iDBErrors;
    private ISMTPEmailServiceShould _EmailService;



    public AccountManager(IRepositoryAuth<string> authdao, IAuthenticationService authenticationService, IDBErrors dbError, ISMTPEmailServiceShould email)
    {
        _iDBErrors = dbError;
        _EmailService = email;
        _authentication = authenticationService;
        _AS = new AccountService(authdao, dbError, email);
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
                if (res != "account updated")
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

    public string VerifyAccount(string code, string username)
    {
        String StoredCode = _AS.VerifyAccount(username);
        if (StoredCode.Contains("Database"))
        {
            return _iDBErrors.DBErrorCheck(int.Parse(StoredCode));
        }
        if (code != StoredCode)
        {
            _AS.DeleteCode(username, "Registration");
            return "Invalid Code";
        }
        String DateCheck = _AS.VerifySameDay(code, username, DateTime.Now);
        _AS.DeleteCode(username, "Registration");
        if (DateCheck == "True")
        {
            return "Account Verified";
        }
        return "Code Expired";
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

    public string AcceptEULA(string username)
    {
        return _AS.AcceptEULA(username);
    }

    public string DeclineEULA(string username)
    {
        return _AS.DeclineEULA(username);
    }

    public string CreateAccount(User user)
    {
        if (_AS.ValidateEmail(user.Email) == false)
        {
            return "Invalid Email";
        }
        else if (_AS.ValidatePassword(user.Password) == false)
        {
            return "Invalid Password";
        }
        String isValidUsername = _AS.ValidateUsername(user.Username);
        if (isValidUsername != "new username")
        {
            return isValidUsername;
        }
        String unactivated = _AS.SaveUnActivatedAccount(user);
        if (unactivated != "Saved")
        {
            return unactivated;
        }
        String SentCode = _AS.VerifyEmail(user.Username, user.Email, DateTime.Now);
        if (SentCode != "True")
        {
            _AS.EmailFailed(user);
            return SentCode;
        }
        return "Email Pending Confirmation";

        
    }

    public string recoverAccount(AccountRecoveryModel arm)
    {
        //add nuke method here ?

        if (_AS.ValidateEmail(arm.Email) == false || _AS.ValidateUsername(arm.Username) == "Invalid Username")
        {
            return "invalid username or email";
        }
        string ra = _AS.UsernameAndEmailExists(arm.Username, arm.Email);
        if (ra.Contains("Database"))
        {
            return _iDBErrors.DBErrorCheck(int.Parse(ra));
        }
        else if(ra == "incorrect")
        {
            return "Account Recovery Error";
        }



        string enabled = _AS.IsEnabled(arm.Username);
        if (enabled != "enabled")
        {
            return "disabled account";
        }

        string recoveryValidation = _AS.ValidateRecoveryAttempts(arm.Username);
        if (recoveryValidation != "under")
        {
            return recoveryValidation;
        }

        string r = _AS.GenerateRandomString();
        /*
        string email = _AS.SendEmail(arm.Email, "Bit O Health Recovery", "Please click URL within 24 hours to recover your account" +
            "\n https://localhost:7011/Account/ResetPassword?r=" + r + "&u=" + arm.Username);
        

        if (email != "email sent") 
        {
            return email;
        }
        */
        DateTime dateTime = DateTime.Now;

        string updateRecoveryAttempts = _AS.UpdateRecoveryAttempts(arm.Username, dateTime);


        if (updateRecoveryAttempts != "1")
        {
            return _iDBErrors.DBErrorCheck(int.Parse(updateRecoveryAttempts));
        }
        string saveCode = _AS.SaveActivationCode(arm.Username, dateTime, r, "Recovery");
        if (saveCode != "saved")
        {
            _AS.DeletePastOTP(arm.Username, "Recovery");
            string retry = _AS.SaveActivationCode(arm.Username, dateTime, r, "Recovery");
            if (retry.Contains("Database"))
            {
                return _iDBErrors.DBErrorCheck(int.Parse(retry));
            }
        }
        
        return "Recovery Link Sent To Email: " + arm.Email;
    }
    public string ResetPassword(string u, string r, string p)
    {
        if (!_AS.ValidatePassword(p))
        {
            return "invalid password";
        }
        string validateOTP = _AS.ValidateOTP(u, r);
        if (validateOTP != "valid")
        {
            return _iDBErrors.DBErrorCheck(int.Parse(validateOTP));
        }
        string sameDay = _AS.VerifySameDay(u, r);
        if (sameDay != "1")
        {
            return _iDBErrors.DBErrorCheck(int.Parse(sameDay));
        }
        
        string reset = _AS.ResetPassword(p, u);
        if (reset != "1")
        {
            return _iDBErrors.DBErrorCheck(int.Parse(reset));
        }
        return "Account Recovery Completed Successfully";

    }



}