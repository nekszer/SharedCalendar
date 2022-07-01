using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public class CypherService : ICypherService
    {
        public string Password { get; set; } = "devazt";
        public string Salt { get; set; } = "_";
        public string HashAlgorithm { get; set; } = "SHA1";
        public int PasswordIterations { get; set; } = 1;
        public string InitialVector { get; set; } = "freelancer";
        public int KeySize { get; set; } = 128;

        public async Task<string> Encrypt(object data)
        {
            await Task.Delay(1);
            return EasyEncrypt(Password, data.ToString(), true);
            // return AESEncrypt(data.ToString(), "devazt", "_", "SHA1", 1, "freelancer", 128);
        }

        public async Task<string> Decrypt(string data)
        {
            await Task.Delay(1);
            return EasyDecrypt(Password, data, true);
            // return AESDecrypt(data, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
        }

        public string EasyEncrypt(string key, string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                //of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string EasyDecrypt(string key, string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        // <summary>  
        // Encrypts a string          
        // </summary>        
        // <param name="CipherText">Text to be Encrypted</param>         
        // <param name="Password">Password to Encrypt with</param>         
        // <param name="Salt">Salt to Encrypt with</param>          
        // <param name="HashAlgorithm">Can be either SHA1 or MD5</param>         
        // <param name="PasswordIterations">Number of iterations to do</param>          
        // <param name="InitialVector">Needs to be 16 ASCII characters long</param>          
        // <param name="KeySize">Can be 128, 192, or 256</param>          
        // <returns>A decrypted string</returns>       
        public string AESEncrypt(string PlainText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
        {
            if (string.IsNullOrEmpty(PlainText))
            {
                return "The Text to be Decryped by AES must not be null...";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                return "The Password for AES Decryption must not be null...";
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();

            SymmetricKey.Mode = CipherMode.CBC;

            byte[] CipherTextBytes = null;

            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {

                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);

        }

        // <summary>  
        // Decrypts a string          
        // </summary>        
        // <param name="CipherText">Text to be decrypted</param>         
        // <param name="Password">Password to decrypt with</param>         
        // <param name="Salt">Salt to decrypt with</param>          
        // <param name="HashAlgorithm">Can be either SHA1 or MD5</param>         
        // <param name="PasswordIterations">Number of iterations to do</param>          
        // <param name="InitialVector">Needs to be 16 ASCII characters long</param>          
        // <param name="KeySize">Can be 128, 192, or 256</param>          
        // <returns>A decrypted string</returns>        
        public string AESDecrypt(string CipherText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
        {
            if (string.IsNullOrEmpty(CipherText))
            {
                return "The Text to be Decryped by AES must not be null...";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                return "The Password for AES Decryption must not be null...";
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            try
            {

                using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
                {
                    using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                    {
                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                        {
                            ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                            MemStream.Close();
                            CryptoStream.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return "Please Enter the Correct Password and Salt..." + "The Following Error Occured: " + "/n" + e;
            }
            SymmetricKey.Clear();
            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);

        }
    }
}
