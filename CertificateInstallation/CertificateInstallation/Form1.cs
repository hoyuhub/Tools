using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.Configuration;
using System.Diagnostics;

namespace CertificateInstallation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tsb_export_Click(object sender, EventArgs e)
        {
            ButtonCheck(tsb_export);
            PanelShow(panel_export);
        }

        private void tsb_encry_Click(object sender, EventArgs e)
        {
            ButtonCheck(tsb_encry);
            PanelShow(panel_encryp);
        }

        private void tsb_CertInfo_Click(object sender, EventArgs e)
        {
            ButtonCheck(tsb_CertInfo);
            PanelShow(panel_cert_info);

        }

        private void tsb_create_cert_Click(object sender, EventArgs e)
        {
            ButtonCheck(tsb_create_cert);
            PanelShow(panel_create);
        }


        #region 导入证书

        //导入
        private void button1_Click(object sender, EventArgs e)
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
                    if (string.IsNullOrWhiteSpace(pwd.Text))
                    {
                        MessageBox.Show("请输入密码！");
                    }
                    else
                    {
                        bool flag = ImportPfxFile(fileDialog.FileName, pwd.Text);
                        if (flag)
                        {
                            MessageBox.Show("安装成功");
                        }
                    }
                }
            }
        }

        //导入文件方法
        private static bool ImportPfxFile(string filePath, string password)
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable);
                string privateKey = certificate.PrivateKey.ToXmlString(false);
                //证书安装到本地存储，根节点
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
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

        #region 加密

        //加密
        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCertName.Text))
                {
                    MessageBox.Show("请输入数字证书主题名称！");
                    return;
                }
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string copyName = fileDialog.FileName + ".config";
                    File.Copy(fileDialog.FileName, copyName, true);
                    Configuration config = ConfigurationManager.OpenExeConfiguration(fileDialog.FileName);

                    X509Certificate2 x509 = AsymmetricEncryption.GetCertificateFromStore(txtCertName.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 证书信息


        //获取证书信息
        private void btn_file_info_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获取用户选择文件的后缀名
                    string extension = Path.GetExtension(fileDialog.FileName);
                    //声明允许的后缀名
                    string[] str = new string[] { ".pfx" };
                    if (!((IList)str).Contains(extension))
                    {
                        MessageBox.Show("仅能使用pfx格式的文件！");
                        return;
                    }
                    ShowCertInfo(fileDialog.FileName, tb_pwd_info.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ShowCertInfo(string filePath, string password)
        {
            try
            {
                X509Certificate2 x509 = AsymmetricEncryption.GetCertficateFromPath(filePath, password);
                tb_SerialNumber.Text = x509.SerialNumber;
                tb_subject_name.Text = x509.SubjectName.Name;
                tb_signature_method.Text = x509.SignatureAlgorithm.FriendlyName;
                if (x509.HasPrivateKey)
                    tb_has_private.Text = "有";
                else
                    tb_has_private.Text = "无";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region 创建证书

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                bool createFile = AsymmetricEncryption.CreateCertWithPrivateKey(tb_create_name.Text, System.Environment.CurrentDirectory + "\\makecert.exe");
                if (createFile)
                {
                    if (AsymmetricEncryption.ExportToPfxFile(tb_create_name.Text, tb_file_name.Text, tb_create_pwd.Text, true))
                    {
                        MessageBox.Show("创建成功");
                        PanelShow(panel_cert_info);
                        ShowCertInfo(Environment.CurrentDirectory + "\\" + tb_file_name.Text + ".pfx", tb_create_pwd.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 共通
        //展示面板
        private void PanelShow(Panel panel)
        {
            panel_create.Visible = false;
            panel_export.Visible = false;
            panel_encryp.Visible = false;
            panel_cert_info.Visible = false;

            panel.Visible = true;
        }

        //按钮选中
        private void ButtonCheck(ToolStripButton tsb)
        {
            tsb_export.Checked = false;
            tsb_encry.Checked = false;
            tsb_CertInfo.Checked = false;
            tsb_create_cert.Checked = false;
            tsb.Checked = true;

        }
        #endregion

    }
}
