namespace The6Bits.BitOHealth.Models;

public class FailedAttemptsModel
{
    public string Username { get; set; }
    public string Attempts { get; set; }
    public string Date_Time { get; set; }

    public FailedAttemptsModel()
    {
    }

    public FailedAttemptsModel(string username, string attempts, string datetime)
    {

        Username = username;
        Attempts = attempts;
        Date_Time = datetime;

    }
}