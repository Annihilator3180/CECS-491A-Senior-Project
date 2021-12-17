namespace The6Bits.BitOHealth.Authorization
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
   

    public class encryptionMethods
    {
        //private key that will be later stored in persistant storage
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        public static string encrypt(string data)
        {
            //Des encryption algorithm object created
            SymmetricAlgorithm algo = DES.Create();
     
            //Actual object doing the encryption is instantiated using private key and generated iv
            ICryptoTransform transform = algo.CreateEncryptor(key, algo.IV);

            //Encrypt method string parameter converted to byte array
            byte[] inputbuffer = Encoding.Unicode.GetBytes(data);
            //data is encrypted using instantiated transform object at length of data byte array
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            //encrypted byte array and iv byte array converted to a string. A semicolon is used to separate the two
            //so iv can be accessed later on when decrypting
            string ans = Convert.ToBase64String(outputBuffer) + ";" + Convert.ToBase64String(algo.IV);
            return ans;
        }
        public static string decrypt(string encryptedData)
        {
            //Des encryption algorith object created
            SymmetricAlgorithm algo = DES.Create();
            //spltting method parameters at the semicolon which was inserted in the encrypt method
            string[] splitData = encryptedData.Split(';');

            //data is the first half, iv is the second
            string data = splitData[0];
            string iv = splitData[1];

            //both strings converted to byte arrays
            byte[] datatoByte = Convert.FromBase64String(data);
            byte[] ivToByte = Convert.FromBase64String(iv);

            //actual decryptor object instantiated
            ICryptoTransform transform = algo.CreateDecryptor(key, ivToByte);
            //decryptor object writes the decrpyted bytes to ans byte array
            byte[] ans = transform.TransformFinalBlock(datatoByte, 0, datatoByte.Length);
            //byte array containing decrypted bytes converted to a string and returned
            return Encoding.Unicode.GetString(ans);
        }
    }
}