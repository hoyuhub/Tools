using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace EncryptTools
{
    public static class EncryptTool
    {
        private static byte[] Key;
        private static byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public static string AesEncrypt(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (Key == null)
            {
                GetKey();
            }

            // Encrypt the string to an array of bytes.
            try
            {
                byte[] encrypted = EncryptStringToBytes(rawInput, Key, IV);
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string AesDecrypt(string encryptedInput)
        {
            if (string.IsNullOrEmpty(encryptedInput))
            {
                return string.Empty;
            }

            if (Key == null)
            {
                GetKey();
            }
            try
            {
                byte[] encrypted = Convert.FromBase64String(encryptedInput);
                // Decrypt the bytes to a string.
                string roundtrip = DecryptStringFromBytes(encrypted, Key, IV);
                return roundtrip;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        //获取Key
        public static void GetKey(string input = null)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = "09167c6c-c2f4-4a64-86c9-22d86eda60b2";
                }
                SHA256 sha = new SHA256CryptoServiceProvider();
                byte[] bytes_in = Encoding.UTF8.GetBytes(input);
                byte[] bytes_out = sha.ComputeHash(bytes_in);
                sha.Dispose();
                Key = bytes_out.Skip(0).Take(16).ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

}