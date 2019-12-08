using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CertificateInstallation
{
    public partial class CertTools : Form
    {
        public CertTools()
        {
            InitializeComponent();
        }
        #region 导入证书

        private void btn_import_Click(object sender, EventArgs e)
        {
            try
            {
                Pwd pwd = new Pwd();
                DialogResult dialog = pwd.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    //初始化一个OpenFileDialog类
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    //判断用户是否正确的选择了文件
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //获取用户选择文件的后缀名
                        string extension = Path.GetExtension(fileDialog.FileName);
                        //声明允许的后缀名
                        string[] str = new string[] { ".pfx" };
                        if (!((IList)str).Contains(extension))
                        {
                            MessageBox.Show("仅能使用pfx格式的文件！");
                        }
                        else
                        {
                            bool flag = ImportPfxFile(fileDialog.FileName, string.IsNullOrEmpty(pwd.strPwd) == true ? null : pwd.strPwd);
                            if (flag)
                            {
                                MessageBox.Show("导入成功");
                            }
                        }
                    }
                }
                else
                {
                    pwd.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //导入文件方法
        private static bool ImportPfxFile(string filePath, string password = null)
        {
            try
            {
                X509Certificate2 certificate;
                if (password != null)
                {
                    certificate = new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable);
                }
                else
                {
                    certificate = new X509Certificate2(filePath);
                }
                string privateKey = certificate.PrivateKey.ToXmlString(false);
                //证书安装到本地存储，根节点
                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Remove(certificate);   //可省略
                store.Add(certificate);
                store.Close();
                //File.Delete(filePath);
                return true;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

        }

        #endregion

        #region 加密文件

        private static string SerialNumber;

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                //按颁发者筛选证书
                X509Certificate2Collection xcc = AsymmetricEncryption.GetLocal();
                Form form = GetCert(xcc, ConfigurationManager.AppSettings["Issuer"]);
                DialogResult dialog = form.ShowDialog();
                if (dialog != DialogResult.OK)
                {
                    return;
                }

                //利用选择的证书加密
                X509Certificate2 x509 = xcc.Find(X509FindType.FindBySerialNumber, SerialNumber, false)[0];
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获取用户选择文件的后缀名
                    string extension = Path.GetExtension(fileDialog.FileName);
                    //声明允许的后缀名
                    string[] str = new string[] { ".config" };
                    if (!((IList)str).Contains(extension))
                    {
                        MessageBox.Show("仅能使用config格式的文件！");
                    }
                    else
                    {

                        string copyName = fileDialog.FileName + ".config";
                        File.Copy(fileDialog.FileName, copyName, true);
                        Configuration config = ConfigurationManager.OpenExeConfiguration(fileDialog.FileName);

                        foreach (ConnectionStringSettings item in config.ConnectionStrings.ConnectionStrings)
                        {
                            item.ConnectionString = AsymmetricEncryption.SectionEncrypt(item.ConnectionString, x509.PublicKey.Key.ToXmlString(false));
                            //item.ConnectionString = AsymmetricEncryption.SectionDecrypt(item.ConnectionString, x509);
                        }

                        foreach (KeyValueConfigurationElement item in config.AppSettings.Settings)
                        {
                            if (item.Key.Equals("CertSerialNumber"))
                                continue;
                            item.Value = AsymmetricEncryption.SectionEncrypt(item.Value, x509.PublicKey.Key.ToXmlString(false));
                            //item.Value = AsymmetricEncryption.SectionDecrypt(item.Value, x509);
                        }
                        config.Save();
                        File.Copy(copyName, fileDialog.FileName, true);
                        File.Delete(copyName);
                        MessageBox.Show("加密成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btn_choose_cert_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = ((Button)sender);
                SerialNumber = btn.Name;
                btn.FindForm().DialogResult = DialogResult.OK;
                btn.FindForm().Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查看证书信息
        private void btn_cert_info_Click(object sender, EventArgs e)
        {
            //按颁发者筛选证书
            X509Certificate2Collection xcc = AsymmetricEncryption.GetLocal();
            Form form = GetCert(xcc, ConfigurationManager.AppSettings["Issuer"]);
            DialogResult dialog = form.ShowDialog();
            if (dialog != DialogResult.OK)
            {
                return;
            }

            //利用选择的证书展示信息
            X509Certificate2 x509 = xcc.Find(X509FindType.FindBySerialNumber, SerialNumber, false)[0];

            MessageBox.Show(x509.SerialNumber);
            //Form formInfo = new Form();
            //formInfo.Text = "证书信息";
            //formInfo.FormBorderStyle = FormBorderStyle.FixedSingle;
            //formInfo.StartPosition = FormStartPosition.CenterScreen;
            //formInfo.ShowIcon = false;
            //formInfo.MaximizeBox = false;
            //formInfo.MinimizeBox = false;
            //formInfo.Width = 284;
            //formInfo.TopMost = true;

            //formInfo.Controls.Add(new Label() { Text = string.Format("序列号：{0}", x509.SerialNumber) });
            //formInfo.Controls.Add(new Label() { Text = string.Format("序列号：{0}", x509.SerialNumber) });
            //formInfo.ShowDialog();
        }


        #endregion

        private Form GetCert(X509Certificate2Collection x509xcc, string issuer)
        {
            try
            {
                Form form = new Form();
                form.Text = "选择证书";
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowIcon = false;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.Width = 284;
                form.TopMost = true;


                X509Certificate2Collection xcc = x509xcc.Find(X509FindType.FindByIssuerDistinguishedName, issuer, false);

                int locationY = 12;
                foreach (X509Certificate2 item in xcc)
                {
                    Button btn = new Button();
                    btn.Text = item.FriendlyName;
                    btn.Name = item.SerialNumber;
                    btn.Width = 244;
                    btn.Height = 36;
                    btn.Location = new Point(12, locationY);
                    btn.Click += new EventHandler(btn_choose_cert_Click);
                    locationY += 42;
                    if (locationY > form.Height)
                        form.Height = locationY + 36;
                    form.Controls.Add(btn);
                }
                return form;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
