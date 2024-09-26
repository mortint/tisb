namespace TISB.Forms
{
    partial class AccountsListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsListForm));
            this.dataGridView_accountsList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_phone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button__addedAccount = new System.Windows.Forms.Button();
            this.button_deletedAccount = new System.Windows.Forms.Button();
            this.button_authorize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_accountsList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_accountsList
            // 
            this.dataGridView_accountsList.AllowUserToAddRows = false;
            this.dataGridView_accountsList.AllowUserToDeleteRows = false;
            this.dataGridView_accountsList.AllowUserToResizeColumns = false;
            this.dataGridView_accountsList.AllowUserToResizeRows = false;
            this.dataGridView_accountsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_accountsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView_accountsList.Location = new System.Drawing.Point(12, 25);
            this.dataGridView_accountsList.Name = "dataGridView_accountsList";
            this.dataGridView_accountsList.ReadOnly = true;
            this.dataGridView_accountsList.RowHeadersVisible = false;
            this.dataGridView_accountsList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_accountsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_accountsList.Size = new System.Drawing.Size(303, 267);
            this.dataGridView_accountsList.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Номер телефона";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 300;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Загруженные аккаунты";
            // 
            // textBox_phone
            // 
            this.textBox_phone.Location = new System.Drawing.Point(12, 311);
            this.textBox_phone.Name = "textBox_phone";
            this.textBox_phone.Size = new System.Drawing.Size(303, 20);
            this.textBox_phone.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите номер телефона";
            // 
            // button__addedAccount
            // 
            this.button__addedAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button__addedAccount.ForeColor = System.Drawing.Color.Black;
            this.button__addedAccount.Location = new System.Drawing.Point(12, 337);
            this.button__addedAccount.Name = "button__addedAccount";
            this.button__addedAccount.Size = new System.Drawing.Size(150, 28);
            this.button__addedAccount.TabIndex = 4;
            this.button__addedAccount.Text = "Добавить ";
            this.button__addedAccount.UseVisualStyleBackColor = true;
            this.button__addedAccount.Click += new System.EventHandler(this.button__addedAccount_Click);
            // 
            // button_deletedAccount
            // 
            this.button_deletedAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_deletedAccount.ForeColor = System.Drawing.Color.Black;
            this.button_deletedAccount.Location = new System.Drawing.Point(168, 337);
            this.button_deletedAccount.Name = "button_deletedAccount";
            this.button_deletedAccount.Size = new System.Drawing.Size(150, 28);
            this.button_deletedAccount.TabIndex = 5;
            this.button_deletedAccount.Text = "Удалить ";
            this.button_deletedAccount.UseVisualStyleBackColor = true;
            this.button_deletedAccount.Click += new System.EventHandler(this.button_deletedAccount_Click);
            // 
            // button_authorize
            // 
            this.button_authorize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_authorize.ForeColor = System.Drawing.Color.Black;
            this.button_authorize.Location = new System.Drawing.Point(12, 374);
            this.button_authorize.Name = "button_authorize";
            this.button_authorize.Size = new System.Drawing.Size(306, 28);
            this.button_authorize.TabIndex = 6;
            this.button_authorize.Text = "Авторизовать";
            this.button_authorize.UseVisualStyleBackColor = true;
            this.button_authorize.Click += new System.EventHandler(this.button_authorize_Click);
            // 
            // AccountsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(325, 414);
            this.Controls.Add(this.button_authorize);
            this.Controls.Add(this.button_deletedAccount);
            this.Controls.Add(this.button__addedAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_phone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_accountsList);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AccountsListForm";
            this.Text = "T-ISB :: Добавить аккаунт";
            this.Shown += new System.EventHandler(this.AccountsListForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_accountsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_accountsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_phone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button__addedAccount;
        private System.Windows.Forms.Button button_deletedAccount;
        private System.Windows.Forms.Button button_authorize;
    }
}