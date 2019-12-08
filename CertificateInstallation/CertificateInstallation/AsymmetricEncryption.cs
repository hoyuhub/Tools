
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificateInstallation
{
    public class AsymmetricEncryption
    {
        private static X509Certificate2 x509;

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="base64code">需要进行解密的密文字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string base64code, string privateKey)
        {

            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(privateKey);

            byte[] encryptedData;
            byte[] decryptedData;

            encryptedData = Convert.FromBase64String(base64code);

            decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

            return ByteConverter.GetString(decryptedData);
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
                RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)
                    x509.PrivateKey;


                Byte[] CiphertextData = Convert.FromBase64String(base64code);

                int MaxBlockSize = RSA.KeySize / 8;

                if (CiphertextData.Length <= MaxBlockSize)
                {
                    byte[] decryptedData;

                    decryptedData = RSADecrypt(CiphertextData, RSA.ExportParameters(true), false);

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

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSA.FromXmlString(publicKey);

            encrytedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

            string base64code = Convert.ToBase64String(encrytedData);

            return base64code;
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

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

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
            catch (Exception e)
            {
                throw e;
            }
        }

        private static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSA.ImportParameters(RSAKeyInfo);

            return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
        }

        private static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSA.ImportParameters(RSAKeyInfo);

            return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
        }

        #region 获取证书

        public static X509Certificate2 GetCertificateFromStore(string SerialNumber)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                if (x509 != null && x509.SerialNumber.Equals(SerialNumber))
                {
                    return x509;
                }
                store.Open(OpenFlags.ReadOnly);
                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySerialNumber, SerialNumber, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                x509 = signingCert[0];
                return x509;
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

        public static X509Certificate2 GetCertficateFromPath(string path, string password)
        {
            try
            {
                X509Certificate2 x509 = new X509Certificate2(path, password);
                return x509;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region 创建数字证书

        // 根据指定的证书名和makecert全路径生成证书（包含公钥和私钥，并保存在MY存储区）   
        public static bool CreateCertWithPrivateKey(string subjectName, string makecertPath)
        {
            subjectName = "CN=" + subjectName;
            string param = " -pe -ss my -n \"" + subjectName + "\" ";
            try
            {
                Process p = Process.Start(makecertPath, param);
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 导出
        // 从WINDOWS证书存储区的个人MY区找到主题为subjectName的证书，   
        // 并导出为pfx文件，同时为其指定一个密码   
        // 并将证书从个人区删除(如果isDelFromstor为true)   
        public static bool ExportToPfxFile(string subjectName, string pfxFileName,
            string password, bool isDelFromStore)
        {
            subjectName = "CN=" + subjectName;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storecollection = store.Certificates;
            foreach (X509Certificate2 x509 in storecollection)
            {
                if (x509.SubjectName.Name == subjectName)
                {
                    Debug.Print(string.Format("certificate name: {0}", x509.Subject));

                    byte[] pfxByte = x509.Export(X509ContentType.Pfx, password);
                    using (FileStream fileStream = new FileStream(pfxFileName + ".pfx", FileMode.Create))
                    {
                        // Write the data to the file, byte by byte.   
                        for (int i = 0; i < pfxByte.Length; i++)
                            fileStream.WriteByte(pfxByte[i]);
                        // Set the stream position to the beginning of the file.   
                        fileStream.Seek(0, SeekOrigin.Begin);
                        // Read and verify the data.   
                        for (int i = 0; i < fileStream.Length; i++)
                        {
                            if (pfxByte[i] != fileStream.ReadByte())
                            {
                                fileStream.Close();
                                return false;
                            }
                        }
                        fileStream.Close();
                    }
                    if (isDelFromStore == true)
                        store.Remove(x509);
                }
            }
            store.Close();
            store = null;
            storecollection = null;
            return true;
        }

        #endregion

        //获取证书集合
        public static X509Certificate2Collection GetLocal()
        {
            using (X509Store store = new X509Store(StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadWrite);
                return store.Certificates;

            }
        }

    }
}
