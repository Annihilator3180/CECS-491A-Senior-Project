using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.Authentication.Implementations;

public class JWTAuthenticationService : IAuthenticationService
{


    private IConfiguration _configuration;


    public JWTAuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }



    public string generateToken(string data)
    {
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.ToString();
        string mySecret = File.ReadAllText(Path.GetFullPath(p + _configuration.GetSection("PKs")["JWT"]));
        byte[] keyBytes = Encoding.UTF8.GetBytes(mySecret);
        var segments = new List<string>();


        var header = new { alg = "HS256", typ = "JWT" };
        var payload = new { username = data, iat = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() };
        byte[] headerBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(header));
        byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));

        segments.Add(Convert.ToBase64String(headerBytes));
        segments.Add(Convert.ToBase64String(payloadBytes));
        //segments.Add(Encoding.UTF8.GetString(Base64UrlDecode(BYTE ARRAY OF THE DATA STRING HERE)));

        var stringToSign = string.Join(".", segments.ToArray());

        var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

        var sha = new HMACSHA256(keyBytes);
        byte[] signature = sha.ComputeHash(bytesToSign);
        segments.Add(Convert.ToBase64String(signature));

        return string.Join(".", segments.ToArray());
    }

    public bool ValidateToken(string token)
    {
        try
        {
            
            //DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            //string p = di.Parent.ToString();
            var code = Assembly.GetExecutingAssembly().CodeBase;
            var root = Path.GetDirectoryName(Uri.UnescapeDataString((new UriBuilder(code)).Path));
            string key = File.ReadAllText(Path.GetFullPath(root + _configuration.GetSection("PKs")["JWT"]));

            var parts = token.Split('.');
            var header = parts[0];
            var payload = parts[1];
            byte[] crypto = Convert.FromBase64String(parts[2]);



            var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var sha = new HMACSHA256(keyBytes);
            byte[] signature = sha.ComputeHash(bytesToSign);
            var decodedCrypto = Convert.ToBase64String(crypto);
            var decodedSignature = Convert.ToBase64String(signature);

            

            if (decodedCrypto != decodedSignature)
            {
                return false;
            }


            return true;
        }
        catch
        {
            return false;
        }

    }


    //TODO: FIX
    public string Decrypt(string token)
    {

        return "";
    }


    public string getUsername(string token)
    {

        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        //string p = di.Parent.ToString();
        //string key = File.ReadAllText(Path.GetFullPath(p + @"/Keys/private-key.pem"));

        var code= Assembly.GetExecutingAssembly().CodeBase;
        var root = Path.GetDirectoryName(Uri.UnescapeDataString((new UriBuilder(code)).Path));
        string key = File.ReadAllText(Path.GetFullPath(root + _configuration.GetSection("PKs")["JWT"]));

        var parts = token.Split('.');
        var header = parts[0];
        var payload = parts[1];

       if (payload.Length != 64)
       {
            return "Incorrect Payload format";
        }
        var data = Convert.FromBase64String((string)payload);
        var dataString = Encoding.UTF8.GetString(data);

        var user = JsonSerializer.Deserialize<JwtPayloadModel>(dataString);

       return user.username;
    }
}