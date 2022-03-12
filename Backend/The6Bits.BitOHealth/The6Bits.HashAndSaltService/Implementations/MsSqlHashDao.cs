using System.Data.SqlClient;
using Dapper;
using The6Bits.BitOHealth.Models;
using The6Bits.HashAndSaltService;

namespace The6Bits.HashAndSaltService;

public class MsSqlHashDao : IHashDao
{

    private readonly string _connectString;
    
    public MsSqlHashDao(string conn)
    {
        _connectString = conn;
    }

    public string GetPassword(string username)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(_connectString))
            {
                connection.Open();
                IEnumerable<string> str = connection.Query<string>($"select Password from Accounts where Username = @Username", new { Username = username });
                return str.First();
                
            }
        }
        catch (SqlException ex)
        {
            return ex.Number.ToString() ;
        }
    }
}