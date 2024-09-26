namespace TISB.Forms.AuthForms
{
    partial class AuthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip_linkTelegram = new System.Windows.Forms.ToolStripMenuItem();
            this.button_signIn = new System.Windows.Forms.Button();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_showFormRegister = new System.Windows.Forms.Button();
            this.checkBox_rememberMe = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_linkTelegram});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(369, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip_linkTelegram
            // 
            this.toolStrip_linkTelegram.Name = "toolStrip_linkTelegram";
            this.toolStrip_linkTelegram.Size = new System.Drawing.Size(107, 20);
            this.toolStrip_linkTelegram.Text = "Администрация";
            this.toolStrip_linkTelegram.Click += new System.EventHandler(this.toolStrip_linkTelegram_Click);
            // 
            // button_signIn
            // 
            this.button_signIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_signIn.Location = new System.Drawing.Point(60, 152);
            this.button_signIn.Name = "button_signIn";
            this.button_signIn.Size = new System.Drawing.Size(237, 31);
            this.button_signIn.TabIndex = 3;
            this.button_signIn.Text = "Войти";
            this.button_signIn.UseVisualStyleBackColor = true;
            this.button_signIn.Click += new System.EventHandler(this.button_signIn_Click);
            // 
            // textBox_login
            // 
            this.textBox_login.Location = new System.Drawing.Point(60, 68);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(237, 20);
            this.textBox_login.TabIndex = 4;
            this.textBox_login.TextChanged += new System.EventHandler(this.textBox_login_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Логин (имя пользователя)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пароль";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(60, 106);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '•';
            this.textBox_password.Size = new System.Drawing.Size(237, 20);
            this.textBox_password.TabIndex = 6;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // button_showFormRegister
            // 
            this.button_showFormRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_showFormRegister.Location = new System.Drawing.Point(60, 189);
            this.button_showFormRegister.Name = "button_showFormRegister";
            this.button_showFormRegister.Size = new System.Drawing.Size(237, 31);
            this.button_showFormRegister.TabIndex = 8;
            this.button_showFormRegister.Text = "Зарегистрироваться";
            this.button_showFormRegister.UseVisualStyleBackColor = true;
            this.button_showFormRegister.Click += new System.EventHandler(this.button_showFormRegister_Click);
            // 
            // checkBox_rememberMe
            // 
            this.checkBox_rememberMe.AutoSize = true;
            this.checkBox_rememberMe.Location = new System.Drawing.Point(61, 131);
            this.checkBox_rememberMe.Name = "checkBox_rememberMe";
            this.checkBox_rememberMe.Size = new System.Drawing.Size(146, 17);
            this.checkBox_rememberMe.TabIndex = 9;
            this.checkBox_rememberMe.Text = "Запомнить мои данные";
            this.checkBox_rememberMe.UseVisualStyleBackColor = true;
            this.checkBox_rememberMe.CheckedChanged += new System.EventHandler(this.checkBox_rememberMe_CheckedChanged);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(369, 247);
            this.Controls.Add(this.checkBox_rememberMe);
            this.Controls.Add(this.button_showFormRegister);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.button_signIn);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T-ISB :: Авторизация";
            this.Shown += new System.EventHandler(this.AuthForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_linkTelegram;
        private System.Windows.Forms.Button button_signIn;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_showFormRegister;
        private System.Windows.Forms.CheckBox checkBox_rememberMe;
    }
}