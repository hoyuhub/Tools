using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceUtils
{
    public static class EncryptTools
    {
        #region 对称加密
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
        #endregion

        #region 非对称加密

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="base64code">需要进行解密的密文字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string base64code, string privateKey)
        {

            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKey);

                byte[] encryptedData;
                byte[] decryptedData;

                encryptedData = Convert.FromBase64String(base64code);

                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                return ByteConverter.GetString(decryptedData);
            }
        }

        /// <summary>
        /// RSA分段解密;用于对超长字符串解密
        /// </summary>
        /// <param name="toEncryptString">需要进行解密的字符串</param>
        /// <param name="publickKey">私钥</param>
        /// <returns>解密后的明文</returns>
        public static string SectionDecrypt(string base64code, X509Certificate2 x509)
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //using (RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)x509.PrivateKey)
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(x509.PrivateKey.ToXmlString(true));
                    Byte[] CiphertextData = Convert.FromBase64String(base64code);

                    int MaxBlockSize = RSA.KeySize / 8;

                    if (CiphertextData.Length <= MaxBlockSize)
                    {
                        byte[] decryptedData;

                        RSAParameters par = RSA.ExportParameters(true);

                        decryptedData = RSADecrypt(CiphertextData, par, false);

                        return ByteConverter.GetString(decryptedData);
                    }

                    MemoryStream CrypStream = new MemoryStream(CiphertextData);

                    MemoryStream PlaiStream = new MemoryStream();

                    Byte[] Buffer = new Byte[MaxBlockSize];

                    int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToDecrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                        Byte[] Plaintext = RSADecrypt(ToDecrypt, RSA.ExportParameters(true), false);
                        PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                        BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return ByteConverter.GetString(PlaiStream.ToArray());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string SectionDecrypt(string base64code, string issuer)
        {
            try
            {
                string[] strArray = base64code.Split('|');
                return SectionDecrypt(strArray[1], GetCertificateFromStore(strArray[0], issuer));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="toEncryptString">需要进行加密的字符串</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>加密后的密文</returns>
        public static string Encrypt(string toEncryptString, string publicKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            byte[] dataToEncrypt = ByteConverter.GetBytes(toEncryptString);

            byte[] encrytedData;

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                RSA.FromXmlString(publicKey);

                encrytedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                string base64code = Convert.ToBase64String(encrytedData);

                return base64code;
            }
        }

        /// <summary>
        /// RSA分段加密;用于对超长字符串加密
        /// </summary>
        /// <param name="toEncryptString">需要进行加密的字符串</param>
        /// <param name="publickKey">公钥</param>
        /// <returns>加密后的密文</returns>
        public static string SectionEncrypt(string toEncryptString, string publickKey)
        {
            try
            {
                string base64code = string.Empty;

                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                byte[] dataToEncrypt = ByteConverter.GetBytes(toEncryptString);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    RSA.FromXmlString(publickKey);

                    int MaxBlockSize = RSA.KeySize / 8 - 11;

                    if (dataToEncrypt.Length <= MaxBlockSize)
                    {
                        byte[] encrytedData;

                        encrytedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                        base64code = Convert.ToBase64String(encrytedData);

                        return base64code;
                    }

                    MemoryStream plaiStream = new MemoryStream(dataToEncrypt);

                    MemoryStream CrypStream = new MemoryStream();

                    Byte[] Buffer = new Byte[MaxBlockSize];

                    int BlockSize = plaiStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToEncrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                        Byte[] Cryptograph = RSAEncrypt(ToEncrypt, RSA.ExportParameters(false), false);
                        CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                        BlockSize = plaiStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    base64code = Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);

                    return base64code;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                RSA.ImportParameters(RSAKeyInfo);

                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
        }

        private static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    RSA.ImportParameters(RSAKeyInfo);

                    return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取证书
        /// </summary>
        /// <param name="SerialNumber">序号</param>
        /// <param name="issuer">颁发者名称</param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateFromStore(string SerialNumber, string issuer)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            try
            {

                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;

                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);

                X509Certificate2Collection issuerCerts = currentCerts.Find(X509FindType.FindByIssuerDistinguishedName, issuer, false);

                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySerialNumber, SerialNumber, false);



                if (signingCert.Count == 0)
                    return null;

                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                store.Close();
            }

        }

        /// <summary>
        /// 获取证书集
        /// </summary>
        /// <param name="issuer">颁发者名称</param>
        /// <returns></returns>
        public static X509Certificate2Collection GetCertificateFromStore(string issuer)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);

            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;

                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);

                X509Certificate2Collection issuerCerts = currentCerts.Find(X509FindType.FindByIssuerDistinguishedName, issuer, false);

                return issuerCerts;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                store.Close();
            }

        }



        #endregion

    }
}
