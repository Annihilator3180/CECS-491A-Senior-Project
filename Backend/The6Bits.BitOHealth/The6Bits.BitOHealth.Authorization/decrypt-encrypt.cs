namespace The6Bits.BitOHealth.Authorization
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.IO;
    using System.Drawing;
    using System.Linq;

    class encryptionMethods
    {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static byte[] encrypt(string input)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a new DES object.
                DES DESalg = DES.Create();
                DESalg.GenerateIV();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    DESalg.CreateEncryptor(key, DESalg.IV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(input);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the
                // MemoryStream that holds the
                // encrypted data.
                byte[] ret = mStream.ToArray();
                String temp = Encoding.UTF8.GetString(ret) + ";" + Encoding.UTF8.GetString(DESalg.IV);
               

                // Close the streams.
                cStream.Close();
                mStream.Close();
                //colon at end of encrpt string and append iv
                //yeah i figured out how to change it to a string
                //still pass in the key for now ?

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }

        }
        public static string DecryptTextFromMemory(string Data)
        {
            try
            {
                // Create a new MemoryStream using the passed
                // array of encrypted data.
                string[] semiColonSplit = Data.Split(";");
                string iv = semiColonSplit[1];
                string encryptedString = semiColonSplit[0];

                byte[] toDecrypt = Encoding.UTF8.GetBytes(encryptedString);
                byte[] ivByte = Convert.FromBase64String(iv);   
                MemoryStream msDecrypt = new MemoryStream(toDecrypt);

                // Create a new DES object.
                DES DESalg = DES.Create();


                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                //get iv from encrypted string
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    DESalg.CreateDecryptor(key,ivByte ),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[Data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

    }
}