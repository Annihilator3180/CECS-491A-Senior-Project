namespace The6Bits.HashAndSaltService.Contract;

public interface IHashDao
{
    public string GetPassword(string username);
}