using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateInstallation
{
    public partial class Pwd : Form
    {
        public Pwd()
        {
            InitializeComponent();
        }

        public string strPwd = string.Empty;

        private void btn_ok_Click(object sender, EventArgs e)
        {
            strPwd = tb_pwd.Text.Trim();
        }
    }
}
