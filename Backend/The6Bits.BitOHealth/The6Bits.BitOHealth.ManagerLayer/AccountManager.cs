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

    public string Login(LoginModel acc)
    {
        string us = _AS.UsernameExists(acc.Username);
        if (us != "username exists")
        {
            return us;
        }
        string cp = _AS.CheckPassword(acc.Username, acc.Password);
        if (cp != "credentials found")
        {
            return cp;
        }
        return _authentication.generateToken(acc.Username);
    }

    public bool isTokenValid(string token)
    {
        return _authentication.ValidateToken(token);
    }

    //Checks to see if the account has a Token or not
    public string HasToken(string token) //FIX
    {
        HttpCookie httpCookie = new HttpCookie("aCookie");
        httpCookie = HttpRequestException.Cookies["aCookie"];

        if (httpCookie != null)
            return "<p>" + httpCookie.Name + "<p>" + httpCookie.Value;
        else
            return "Cookie Not Found :(";



        //if (token != "" || token != " ") //If there is a value in jwtToken return true
        //{
            //return true;
        //}
        //if (token == "" || token == " ") //If token is blank
        //{
            //return false;
        //}
        //return true;
    }
}