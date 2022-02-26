namespace The6Bits.BitOHealth.Models;

    public class DeleteAccountModel
    {
        public string Jwt { get; set; }
        //public string Username { get; set; }

    public DeleteAccountModel()
    {

    }
    public DeleteAccountModel(string jwt)
    {
        Jwt = jwt;

    }
}

