namespace CertificateInstallation
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.pwd = new System.Windows.Forms.TextBox();
            this.btn_encrypt = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_encry = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_CertInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_create_cert = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.Panel();
            this.panel_export = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_encryp = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCertName = new System.Windows.Forms.TextBox();
            this.panel_cert_info = new System.Windows.Forms.Panel();
            this.btn_file_info = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_has_private = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_signature_method = new System.Windows.Forms.TextBox();
            this.tb_subject_name = new System.Windows.Forms.TextBox();
            this.tb_SerialNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_pwd_info = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_create = new System.Windows.Forms.Panel();
            this.tb_file_name = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_create_pwd = new System.Windows.Forms.TextBox();
            this.tb_create_name = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel.SuspendLayout();
            this.panel_export.SuspendLayout();
            this.panel_encryp.SuspendLayout();
            this.panel_cert_info.SuspendLayout();
            this.panel_create.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(63, 3);
            this.pwd.Name = "pwd";
            this.pwd.Size = new System.Drawing.Size(151, 21);
            this.pwd.TabIndex = 1;
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.Location = new System.Drawing.Point(61, 33);
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Size = new System.Drawing.Size(153, 23);
            this.btn_encrypt.TabIndex = 2;
            this.btn_encrypt.Text = "加密";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_export,
            this.toolStripSeparator1,
            this.tsb_encry,
            this.toolStripSeparator2,
            this.tsb_CertInfo,
            this.toolStripSeparator3,
            this.tsb_create_cert});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(781, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_export
            // 
            this.tsb_export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_export.Image = ((System.Drawing.Image)(resources.GetObject("tsb_export.Image")));
            this.tsb_export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_export.Name = "tsb_export";
            this.tsb_export.Size = new System.Drawing.Size(60, 22);
            this.tsb_export.Text = "导入证书";
            this.tsb_export.Click += new System.EventHandler(this.tsb_export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_encry
            // 
            this.tsb_encry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_encry.Image = ((System.Drawing.Image)(resources.GetObject("tsb_encry.Image")));
            this.tsb_encry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_encry.Name = "tsb_encry";
            this.tsb_encry.Size = new System.Drawing.Size(60, 22);
            this.tsb_encry.Text = "加密文件";
            this.tsb_encry.Click += new System.EventHandler(this.tsb_encry_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_CertInfo
            // 
            this.tsb_CertInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_CertInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CertInfo.Name = "tsb_CertInfo";
            this.tsb_CertInfo.Size = new System.Drawing.Size(60, 22);
            this.tsb_CertInfo.Text = "证书信息";
            this.tsb_CertInfo.Click += new System.EventHandler(this.tsb_CertInfo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_create_cert
            // 
            this.tsb_create_cert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_create_cert.Image = ((System.Drawing.Image)(resources.GetObject("tsb_create_cert.Image")));
            this.tsb_create_cert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_create_cert.Name = "tsb_create_cert";
            this.tsb_create_cert.Size = new System.Drawing.Size(60, 22);
            this.tsb_create_cert.Text = "创建证书";
            this.tsb_create_cert.Click += new System.EventHandler(this.tsb_create_cert_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.panel_cert_info);
            this.panel.Controls.Add(this.panel_create);
            this.panel.Controls.Add(this.panel_export);
            this.panel.Controls.Add(this.panel_encryp);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(781, 486);
            this.panel.TabIndex = 2;
            // 
            // panel_export
            // 
            this.panel_export.Controls.Add(this.label7);
            this.panel_export.Controls.Add(this.pwd);
            this.panel_export.Controls.Add(this.label5);
            this.panel_export.Controls.Add(this.button1);
            this.panel_export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_export.Location = new System.Drawing.Point(0, 0);
            this.panel_export.Name = "panel_export";
            this.panel_export.Size = new System.Drawing.Size(781, 486);
            this.panel_export.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "密  码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "文  件";
            // 
            // panel_encryp
            // 
            this.panel_encryp.Controls.Add(this.label8);
            this.panel_encryp.Controls.Add(this.label6);
            this.panel_encryp.Controls.Add(this.txtCertName);
            this.panel_encryp.Controls.Add(this.btn_encrypt);
            this.panel_encryp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_encryp.Location = new System.Drawing.Point(0, 0);
            this.panel_encryp.Name = "panel_encryp";
            this.panel_encryp.Size = new System.Drawing.Size(781, 486);
            this.panel_encryp.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "序列号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "文  件";
            // 
            // txtCertName
            // 
            this.txtCertName.Location = new System.Drawing.Point(62, 3);
            this.txtCertName.Name = "txtCertName";
            this.txtCertName.Size = new System.Drawing.Size(150, 21);
            this.txtCertName.TabIndex = 1;
            // 
            // panel_cert_info
            // 
            this.panel_cert_info.Controls.Add(this.btn_file_info);
            this.panel_cert_info.Controls.Add(this.label10);
            this.panel_cert_info.Controls.Add(this.tb_has_private);
            this.panel_cert_info.Controls.Add(this.label9);
            this.panel_cert_info.Controls.Add(this.tb_signature_method);
            this.panel_cert_info.Controls.Add(this.tb_subject_name);
            this.panel_cert_info.Controls.Add(this.tb_SerialNumber);
            this.panel_cert_info.Controls.Add(this.label4);
            this.panel_cert_info.Controls.Add(this.label3);
            this.panel_cert_info.Controls.Add(this.label2);
            this.panel_cert_info.Controls.Add(this.tb_pwd_info);
            this.panel_cert_info.Controls.Add(this.label1);
            this.panel_cert_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_cert_info.Location = new System.Drawing.Point(0, 0);
            this.panel_cert_info.Name = "panel_cert_info";
            this.panel_cert_info.Size = new System.Drawing.Size(781, 486);
            this.panel_cert_info.TabIndex = 4;
            // 
            // btn_file_info
            // 
            this.btn_file_info.Location = new System.Drawing.Point(62, 30);
            this.btn_file_info.Name = "btn_file_info";
            this.btn_file_info.Size = new System.Drawing.Size(152, 23);
            this.btn_file_info.TabIndex = 2;
            this.btn_file_info.Text = "选择文件";
            this.btn_file_info.UseVisualStyleBackColor = true;
            this.btn_file_info.Click += new System.EventHandler(this.btn_file_info_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "文  件";
            // 
            // tb_has_private
            // 
            this.tb_has_private.Location = new System.Drawing.Point(63, 142);
            this.tb_has_private.Name = "tb_has_private";
            this.tb_has_private.ReadOnly = true;
            this.tb_has_private.Size = new System.Drawing.Size(150, 21);
            this.tb_has_private.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "私  钥";
            // 
            // tb_signature_method
            // 
            this.tb_signature_method.Location = new System.Drawing.Point(63, 114);
            this.tb_signature_method.Name = "tb_signature_method";
            this.tb_signature_method.ReadOnly = true;
            this.tb_signature_method.Size = new System.Drawing.Size(150, 21);
            this.tb_signature_method.TabIndex = 7;
            // 
            // tb_subject_name
            // 
            this.tb_subject_name.Location = new System.Drawing.Point(63, 87);
            this.tb_subject_name.Name = "tb_subject_name";
            this.tb_subject_name.ReadOnly = true;
            this.tb_subject_name.Size = new System.Drawing.Size(150, 21);
            this.tb_subject_name.TabIndex = 6;
            // 
            // tb_SerialNumber
            // 
            this.tb_SerialNumber.Location = new System.Drawing.Point(63, 60);
            this.tb_SerialNumber.Name = "tb_SerialNumber";
            this.tb_SerialNumber.ReadOnly = true;
            this.tb_SerialNumber.Size = new System.Drawing.Size(150, 21);
            this.tb_SerialNumber.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "签名法";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "主  题";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "序列号";
            // 
            // tb_pwd_info
            // 
            this.tb_pwd_info.Location = new System.Drawing.Point(63, 3);
            this.tb_pwd_info.Name = "tb_pwd_info";
            this.tb_pwd_info.Size = new System.Drawing.Size(150, 21);
            this.tb_pwd_info.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "密  码";
            // 
            // panel_create
            // 
            this.panel_create.Controls.Add(this.tb_file_name);
            this.panel_create.Controls.Add(this.label14);
            this.panel_create.Controls.Add(this.btn_create);
            this.panel_create.Controls.Add(this.label13);
            this.panel_create.Controls.Add(this.tb_create_pwd);
            this.panel_create.Controls.Add(this.tb_create_name);
            this.panel_create.Controls.Add(this.label12);
            this.panel_create.Controls.Add(this.label11);
            this.panel_create.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_create.Location = new System.Drawing.Point(0, 0);
            this.panel_create.Name = "panel_create";
            this.panel_create.Size = new System.Drawing.Size(781, 486);
            this.panel_create.TabIndex = 4;
            // 
            // tb_file_name
            // 
            this.tb_file_name.Location = new System.Drawing.Point(63, 30);
            this.tb_file_name.Name = "tb_file_name";
            this.tb_file_name.Size = new System.Drawing.Size(152, 21);
            this.tb_file_name.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "文件名";
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(62, 90);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(153, 23);
            this.btn_create.TabIndex = 4;
            this.btn_create.Text = "创建";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "创  建";
            // 
            // tb_create_pwd
            // 
            this.tb_create_pwd.Location = new System.Drawing.Point(63, 59);
            this.tb_create_pwd.Name = "tb_create_pwd";
            this.tb_create_pwd.Size = new System.Drawing.Size(152, 21);
            this.tb_create_pwd.TabIndex = 3;
            // 
            // tb_create_name
            // 
            this.tb_create_name.Location = new System.Drawing.Point(64, 3);
            this.tb_create_name.Name = "tb_create_name";
            this.tb_create_name.Size = new System.Drawing.Size(151, 21);
            this.tb_create_name.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "密  码";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "主题名";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 511);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "证书安装";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel_export.ResumeLayout(false);
            this.panel_export.PerformLayout();
            this.panel_encryp.ResumeLayout(false);
            this.panel_encryp.PerformLayout();
            this.panel_cert_info.ResumeLayout(false);
            this.panel_cert_info.PerformLayout();
            this.panel_create.ResumeLayout(false);
            this.panel_create.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Button btn_encrypt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panel_encryp;
        private System.Windows.Forms.TextBox txtCertName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_CertInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel_export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_create_cert;
        private System.Windows.Forms.Panel panel_cert_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_signature_method;
        private System.Windows.Forms.TextBox tb_subject_name;
        private System.Windows.Forms.TextBox tb_SerialNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_pwd_info;
        private System.Windows.Forms.TextBox tb_has_private;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_file_info;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel_create;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_create_pwd;
        private System.Windows.Forms.TextBox tb_create_name;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_file_name;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripButton tsb_export;
        private System.Windows.Forms.ToolStripButton tsb_encry;
    }
}

