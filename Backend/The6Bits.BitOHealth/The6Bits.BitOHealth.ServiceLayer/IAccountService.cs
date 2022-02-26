namespace The6Bits.BitOHealth.ServiceLayer
{
    public interface IAccountService
    {
        string UsernameExists(string username);
        string DeleteAccount(string username);
        string UserRole(string username);
        string CheckPassword(string username, string password);
        string GetEmail(string username);
        string DeletePastOTP(string username, string codeType);
        string SaveActivationCode(string username, DateTime time, string code, string codeType);
        string IsEnabled(string username);
        string ValidateOTP(string username, string code);
        string CheckFailedAttempts(string username);
        string CheckFailDate(string username);
        string InsertFailedAttempts(string username);
        string UpdateFailedAttempts(string username, int updatedValue);
        string UpdateIsEnabled(string username, int updateValue);
        string DeleteFailedAttempts(string username);


    }
}
