using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.Authentication.Implementations;

public class JWTAuthenticationService : IAuthenticationService
{

    private static string Base64UrlEncode(byte[] input)
    {
        var output = Convert.ToBase64String(input);
        output = output.Split('=')[0]; // Remove any trailing '='s
        output = output.Replace('+', '-'); // 62nd char of encoding
        output = output.Replace('/', '_'); // 63rd char of encoding
        return output;
    }

    public string generateToken(string data)
    {
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.ToString();
        string mySecret = File.ReadAllText(Path.GetFullPath(p + @"/Keys/private-key.pem"));
        //var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
        byte[] keyBytes = Encoding.UTF8.GetBytes(mySecret);
        var segments = new List<string>();


        var header = new { alg = "HS256", typ = "JWT" };
        var payload = new { username = data, iat = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() };
        byte[] headerBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(header));
        byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));

        segments.Add(Base64UrlEncode(headerBytes));
        segments.Add(Base64UrlEncode(payloadBytes));
        //segments.Add(Encoding.UTF8.GetString(Base64UrlDecode(BYTE ARRAY OF THE DATA STRING HERE)));

        var stringToSign = string.Join(".", segments.ToArray());

        var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

        var sha = new HMACSHA256(keyBytes);
        byte[] signature = sha.ComputeHash(bytesToSign);
        segments.Add(Base64UrlEncode(signature));

        return string.Join(".", segments.ToArray());
    }

    public bool ValidateToken(string token)
    {
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.ToString();
        string key = File.ReadAllText(Path.GetFullPath(p + @"/Keys/private-key.pem"));

        var parts = token.Split('.');
        var header = parts[0];
        var payload = parts[1];
        byte[] crypto = Base64UrlDecode(parts[2]);



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


    //TODO: FIX
    public string Decrypt(string token)
    {

        return "";
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var output = input;
        output = output.Replace('-', '+'); // 62nd char of encoding
        output = output.Replace('_', '/'); // 63rd char of encoding
        switch (output.Length % 4) // Pad with trailing '='s
        {
            case 0: break; // No pad chars in this case
            case 2: output += "=="; break; // Two pad chars
            case 3: output += "="; break; // One pad char
            default: throw new System.Exception("Illegal base64url string!");
        }
        var converted = Convert.FromBase64String(output); // Standard base64 decoder
        return converted;
    }


    public string getUsername(string token)
    {
    
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.ToString();
        string key = File.ReadAllText(Path.GetFullPath(p + @"/Keys/private-key.pem"));

        var parts = token.Split('.');
        var header = parts[0];
        var payload = parts[1];

        var data = Convert.FromBase64String((string)payload);

        var dataString = Encoding.UTF8.GetString(data);

        var user = JsonSerializer.Deserialize<JwtPayloadModel>(dataString);

       return user.username;
    }
}