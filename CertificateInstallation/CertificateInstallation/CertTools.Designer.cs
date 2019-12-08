namespace CertificateInstallation
{
    partial class CertTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_encrypt = new System.Windows.Forms.Button();
            this.btn_cert_info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(12, 12);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(244, 36);
            this.btn_import.TabIndex = 0;
            this.btn_import.Text = "导入证书";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.Location = new System.Drawing.Point(12, 54);
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Size = new System.Drawing.Size(244, 36);
            this.btn_encrypt.TabIndex = 1;
            this.btn_encrypt.Text = "加密文件";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // btn_cert_info
            // 
            this.btn_cert_info.Location = new System.Drawing.Point(12, 96);
            this.btn_cert_info.Name = "btn_cert_info";
            this.btn_cert_info.Size = new System.Drawing.Size(244, 36);
            this.btn_cert_info.TabIndex = 2;
            this.btn_cert_info.Text = "查看序列码";
            this.btn_cert_info.UseVisualStyleBackColor = true;
            this.btn_cert_info.Click += new System.EventHandler(this.btn_cert_info_Click);
            // 
            // CertTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 196);
            this.Controls.Add(this.btn_cert_info);
            this.Controls.Add(this.btn_encrypt);
            this.Controls.Add(this.btn_import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CertTools";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CertTools";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Button btn_encrypt;
        private System.Windows.Forms.Button btn_cert_info;
    }
}