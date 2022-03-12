namespace HashAndSaltService.Contract;

public interface IHashDao
{
    public string GetPassword(string username);
}