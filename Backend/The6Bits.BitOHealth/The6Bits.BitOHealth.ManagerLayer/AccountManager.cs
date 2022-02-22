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
        string us = _AS.UsernameExists(acc.Username);
        if (us != "username exists")
        {
            return us;
        }

        string isenabled = _AS.IsEnabled(acc.Username);

        if (isenabled != "enabled")
        {
            return isenabled;
        }

        string otp = _AS.ValidateOTP(acc.Username, acc.Code);

        if (otp != "valid")
        {
            string attempts = _AS.CheckFailedAttempts(acc.Username);
            if (attempts == "0")
            {
                _AS.InsertFailedAttempts(acc.Username);
            }
            else
            {
                int z = Int32.Parse(attempts);
                z = z + 1;
                _AS.UpdateFailedAttempts(acc.Username,z );
            }
            return otp;
        }
        
        
        
        
        
        
        
        
        
        string cp = _AS.CheckPassword(acc.Username, acc.Password);
        if (cp != "credentials found")
        {
            string attempts = _AS.CheckFailedAttempts(acc.Username);
            if (attempts == "0")
            {
                _AS.InsertFailedAttempts(acc.Username);
            }
            else
            {
                _AS.UpdateFailedAttempts(acc.Username,Int32.Parse(attempts)+1 );
            }
            return cp;
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
        //TODO:SEND CODE
        Random rnd = new Random();
        string code  = rnd.Next(1000, 9999).ToString();
        
        //SEND CODE

        string del = _AS.DeletePastOTP(username,"OTP");

        string save = _AS.SaveActivationCode(username, DateTime.UtcNow, code, "OTP");
        
        return code;


    }
}