using System.Security.Cryptography;
using System.Text;
using The6Bits.HashAndSaltService.Contract;


namespace The6Bits.HashAndSaltService;

public class HashNSaltService 
{
    private readonly string _pk;
    private IHashDao _dao;

    public HashNSaltService(  IHashDao dao, string key)
    {
        _pk = key;
        _dao = dao;
    }
    
    public string HashAndSalt(string password)
    {
        try
        {

            string mySecret = _pk;
            byte[] keyBytes = Encoding.UTF8.GetBytes(mySecret);


            Random rnd = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            string salt = "";
            foreach (var i in Enumerable.Range(0, 10))
            {
                salt += chars[rnd.Next(0, 62)];
            }

            password += salt;




            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            var bytesToSign = Encoding.UTF8.GetBytes(Convert.ToBase64String(passBytes));




            var sha = new HMACSHA256(keyBytes);



            byte[] signature = sha.ComputeHash(bytesToSign);
            string hashedsalted = Convert.ToBase64String(signature) + "." + salt;

            return hashedsalted;
        }
        catch
        {
            return "Error Hashing";
        }

    }
    
    public string HashAndSalt(string password, string salt)
    {
        try
        {
            string mySecret = _pk;
            byte[] keyBytes = Encoding.UTF8.GetBytes(mySecret);
            
            password += salt;
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            var bytesToSign = Encoding.UTF8.GetBytes(Convert.ToBase64String(passBytes));




            var sha = new HMACSHA256(keyBytes);



            byte[] signature = sha.ComputeHash(bytesToSign);
            string hashedsalted = Convert.ToBase64String(signature) + "." + salt;

            return hashedsalted;
        }
        catch
        {
            return "Error Hashing";
        }
    }


    public string GetSalt(string username)
    {
        
        string pass = _dao.GetPassword(username);

        try
        {
            return pass.Split('.')[1];
        }
        catch
        {
            
            //this returns db error
            return pass;
        }
    }







}