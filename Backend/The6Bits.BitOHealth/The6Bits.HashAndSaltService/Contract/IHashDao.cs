namespace The6Bits.HashAndSaltService;

public interface IHashDao
{
    public string GetPassword(string username);
}