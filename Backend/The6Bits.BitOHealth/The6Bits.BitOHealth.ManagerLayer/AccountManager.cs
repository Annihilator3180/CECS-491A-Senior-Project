using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using The6Bits.Authorization.Contract;
using The6Bits.Authorization;
using The6Bits.Authorization.Implementations;
using The6Bits.HashAndSaltService;
using The6Bits.HashAndSaltService.Contract;

namespace The6Bits.BitOHealth.ManagerLayer;

public class AccountManager
{
    private IAuthenticationService _auth;
    private AccountService _AS;
    private IDBErrors _iDBErrors;
    private ISMTPEmailService _EmailService;
    private IConfiguration _config;
    private HashNSaltService _hash;



    public AccountManager(IRepositoryAuth<string> authdao, IAuthenticationService authenticationService, IDBErrors dbError, ISMTPEmailService email, IConfiguration config, IHashDao dao, string key)
    {
        _iDBErrors = dbError;
        _EmailService = email;
        _auth = authenticationService;
        _config = config;
        _hash = new HashNSaltService( dao, key);
        _AS = new AccountService(authdao, dbError, email,config);
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
                return "Account disabled. Perform account recovery or contact system admin";
            }

        }

        //VALIDATE OTP

        string otp = _AS.ValidateOTP(acc.Username, acc.Code);
        if (otp == "valid")
        {
            otp = _AS.VerifyTwoMins(acc.Code,acc.Username);
            string deletePastOtp = _AS.DeletePastOTP(acc.Username, "OTP");
        }

       
        

        //IMPLEMENTATION OF FAILED ATTEMPTS
        if (otp != "valid" )
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
                            return "Account disabled. Perform account recovery or contact system admin";
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

            return otp ;
        }
        AuthorizationService authentication = new AuthorizationService(new MsSqlRoleAuthorizationDao(_config.GetConnectionString("DefaultConnection")));

        return _auth.generateToken(acc.Username,authentication.getClaims(acc.Username));
    }

    public verifyResponse VerifyAccount(string code, string username)
    {
        verifyResponse response = new verifyResponse();
        String StoredCode = _AS.VerifyAccount(username);
        if (StoredCode.Contains("Database"))
        {
            response.isSuccess = 0;
            response.ErrorMessage= StoredCode;
            return response;
        }
        if (code != StoredCode)
        {
            _= _AS.DeleteCode(username, "Registration");
            response.isSuccess = 0;
            response.ErrorMessage = "Invalid Code";
            return response;
        }
        String DateCheck = _AS.VerifySameDay(code, username, DateTime.Now);
        _= _AS.DeleteCode(username, "Registration");
        if (DateCheck != "True")
        {
            response.isSuccess= 0;
            response.ErrorMessage = DateCheck;
            return response;
        }
        string permUsername = _AS.MakeUsername(username);
        string activated = _AS.MakeNewUserName(permUsername,username);
        if (activated.Contains("Database"))
        {
            response.isSuccess = 0;
            response.ErrorMessage = activated;
            return response;
        }
        response.isSuccess = 1;
        response.data = "Account Verified";
        response.username = permUsername;
        return response;
        
        
    }




    public bool isTokenValid(string token)
    {
        return _auth.ValidateToken(token);
    }

    public string HasToken(string token)
    {
        if (token != null)
            return "Token exists";
        else
            return "Token Not Found";
    }

    public string SendOTP(string username, string password)
    {
        string usernameExists = _AS.UsernameExists(username);

        if (usernameExists != "username exists")
        {
            return "Invalid username or password provided. Retry again or contact system administrator";
        }

        string email = _AS.GetEmail(username);
        if(!email.Contains("@"))
        {
            return email;
        }

        password = _hash.HashAndSalt(password, _hash.GetSalt(username));

        string checkPassword = _AS.CheckPassword(username, password);

        if (checkPassword != "credentials found")
        {
            return "Invalid username or password provided. Retry again or contact system admin";
        }

        //GEN CODE
        Random rnd = new Random();
        string code = "";
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz.,@!";

        foreach (var i in Enumerable.Range(0, 10))
        {
            code+=chars[rnd.Next(0, 62)];
        }
        
        string em = _EmailService.SendEmailNoReply(email, "ONE TIME PASSWORD", "YOUR ONE TIME PASSWORD IS : " + code);
        
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
            return "Account creation error. Retry again or contact system administrator";
        }
        else if (_AS.ValidatePassword(user.Password) == false)
        {
            return "Account creation error. Retry again or contact system administrator";
        }
        string validUsername = _AS.CreateTempUserName();
        if (validUsername[0] != ',')
        {
            return validUsername;
        }
        user.Username = validUsername;
        user.Password= _hash.HashAndSalt(user.Password);
        String unactivated = _AS.SaveUnActivatedAccount(user);
        if (unactivated != "Saved")
        {
            return unactivated;
        }
        String sentCode = _AS.VerifyEmail(user.Username, user.Email, DateTime.Now);
        if (sentCode != "True")
        {
            _AS.EmailFailed(user);
        }
        return "Email Pending Confirmation";

        
    }

    public string RecoverAccount(AccountRecoveryModel arm)
    {
        if (arm.Username[0] == ',')
        {
            return "Account Recovery Error";
        }
        if (_AS.ValidateEmail(arm.Email) == false || _AS.ValidateUsername(arm.Username) == "Invalid Username")
        {
            return "Account Recovery Error";
        }
        string ra = _AS.UsernameAndEmailExists(arm.Username, arm.Email);
        if (ra.Contains("Database"))
        {
            return ra;
        }
        else if(ra == "incorrect")
        {
            return "Account Recovery Error";
        }


        string recoveryValidation = _AS.ValidateRecoveryAttempts(arm.Username);
        if (recoveryValidation != "under")
        {
            return "Account Recovery Error";
        }

        string randomString = _AS.GenerateRandomString();

        const string subject = "Bit O Health Recovery";

        string body = "Please click this link within 24 hours to recover your account "+
                "https://bitohealth.com/#/ResetPassword/" + randomString + "/" + arm.Username;
        
        
        string email = _AS.SendEmail(arm.Email, subject, body);
        

        if (email != "email sent") 
        {
            return email;
        }
        
       
        DateTime dateTime = DateTime.Now;

        string updateRecoveryAttempts = _AS.UpdateRecoveryAttempts(arm.Username, dateTime);


        if (updateRecoveryAttempts != "1")
        {
            return updateRecoveryAttempts;
        }
        string saveCode = _AS.SaveActivationCode(arm.Username, dateTime, randomString, "Recovery");
        if (saveCode != "saved")
        {
            _AS.DeletePastOTP(arm.Username, "Recovery");
            string retry = _AS.SaveActivationCode(arm.Username, dateTime, randomString, "Recovery");
            if (retry.Contains("Database"))
            {
                return retry;
            }
        }
        
        return "Recovery Link Sent To Email: " + arm.Email;
    }

    public List<searchItem> getSearchCount(string type)
    {
        return _AS.getSearchCount(type);
    }

    public List<Tracking> loginTracker(string Type, int months)
    {
       List<Tracking> loginTracked= _AS.GetLogin(Type, months);
       List<Tracking> includeEmptyDays = _AS.EmptyDays(loginTracked);
       return includeEmptyDays;

    }

    public List<Tracking> regTracker()
    {
        return _AS.GetReg();
    }

    public async Task<List<timeTotal>> getAvgTime()
    {
        List<timeTotal> time= await _AS.AvgTime();
        List<timeTotal> avg = _AS.makeAvgTime(time);

        return avg;
    }

    public async Task<List<timeTotal>> GetTotalTime()
    {
        return await  _AS.BiggestTime();
    }

    public string ViewTime(float time, string view)
    {
        try
        {
            if (_AS.ViewExists(view))
            {
                _AS.AddTime(view, time);
            }
            else
            {
                _AS.MakeView(view, time);
            }
            return "updated "+view+"by "+time+" seconds";
        }
        catch (Exception ex)
        {
            return "error addding view " + ex.Message;
        }
    }
        

    public string ResetPassword(string username, string randomString, string password)
    {
        if (!_AS.ValidatePassword(password))
        {
            return "invalid password";
        }
        string validateOTP = _AS.ValidateOTP(username, randomString);
        if (validateOTP != "valid")
        {
            if (validateOTP.Contains("Database"))
            {
                return validateOTP;

            }
            else
            {
                return "Invalid Reset Link";
            }
            
        }
        string sameDay = _AS.VerifySameDay(username, randomString);
        if (sameDay != "same day")
        {
            return sameDay;
        }
        string hashPassword = _hash.HashAndSalt(password);
        string activated = _AS.ActivateUser(username);
        if (activated.Contains("database"))
        {
            return "Database Error";
        }
        string reset = _AS.ResetPassword(hashPassword, username);
        if (reset.Contains("Database"))
        {
            return reset;
        }
        string deleteOTP = _AS.DeletePastOTP(username, "Recovery");
        if (deleteOTP.Contains("Database")){
            return deleteOTP;
        }
        return "Account Recovery Completed Successfully";

    }



    public string DeleteAccount(string token)
    {
        //validate before calling AS
        bool isValid = isTokenValid(token);
        if(isValid != true)
        {
           return "Invalid Token";
        }
        string username = _auth.getUsername(token);
        string user = _AS.UsernameExists(username);
        if (user != "username exists")
        {
            return user;
        }
        return _AS.DeleteAccount(username);
    }
}