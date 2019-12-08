using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EncryptTools;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // bool flag = DataCertificate.CreateCertWithPrivateKey("测试", @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\makecert.exe");
             bool flag = DataCertificate.ExportToPfxFile("HoYuTest", "证书", "password", true);
            //DataCertificate.ImportPfxFile(@"C:\Users\89275\Desktop\导出名称.pfx", "password");
            //X509Certificate2 s509 = DataCertificate.GetCertificateFromStore("测试");
            // try
            // {
            //     //Create a UnicodeEncoder to convert between byte array and string.
            //     UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //     //Create byte arrays to hold original, encrypted, and decrypted data.
            //     byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            //     byte[] encryptedData;
            //     byte[] decryptedData;

            //     //Create a new instance of RSACryptoServiceProvider to generate
            //     //public and private key data.
            //     RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)GetCertificateFromStore("CN=HoYuTest").PublicKey.Key;

            //     //Pass the data to ENCRYPT, the public key information 
            //     //(using RSACryptoServiceProvider.ExportParameters(false),
            //     //and a boolean flag specifying no OAEP padding.
            //     encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);


            //     RSA = (RSACryptoServiceProvider)GetCertificateFromStore("CN=HoYuTest").PrivateKey;
            //     //Pass the data to DECRYPT, the private key information 
            //     //(using RSACryptoServiceProvider.ExportParameters(true),
            //     //and a boolean flag specifying no OAEP padding.
            //     decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

            //     //Display the decrypted plaintext to the console. 
            //     Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));

            // }
            // catch (ArgumentNullException)
            // {
            //     //Catch this exception in case the encryption did
            //     //not succeed.
            //     Console.WriteLine("Encryption failed.");

            // }
        }

        private static X509Certificate2 GetCertificateFromStore(string certName)
        {

            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, false);
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

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        private static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
    }
}
