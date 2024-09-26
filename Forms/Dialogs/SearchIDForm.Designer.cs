namespace TISB.Forms.Dialogs
{
    partial class SearchIDForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchIDForm));
            this.textBox_chatIds = new System.Windows.Forms.TextBox();
            this.button_runSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_listDialogs = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_chatIds
            // 
            this.textBox_chatIds.Location = new System.Drawing.Point(15, 322);
            this.textBox_chatIds.Name = "textBox_chatIds";
            this.textBox_chatIds.Size = new System.Drawing.Size(234, 20);
            this.textBox_chatIds.TabIndex = 0;
            // 
            // button_runSearch
            // 
            this.button_runSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_runSearch.Location = new System.Drawing.Point(15, 348);
            this.button_runSearch.Name = "button_runSearch";
            this.button_runSearch.Size = new System.Drawing.Size(234, 23);
            this.button_runSearch.TabIndex = 1;
            this.button_runSearch.Text = "Начать поиск";
            this.button_runSearch.UseVisualStyleBackColor = true;
            this.button_runSearch.Click += new System.EventHandler(this.button_runSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Нет чата в списке? Введи имя чата.";
            // 
            // listBox_listDialogs
            // 
            this.listBox_listDialogs.FormattingEnabled = true;
            this.listBox_listDialogs.Location = new System.Drawing.Point(15, 25);
            this.listBox_listDialogs.Name = "listBox_listDialogs";
            this.listBox_listDialogs.Size = new System.Drawing.Size(234, 277);
            this.listBox_listDialogs.TabIndex = 3;
            this.listBox_listDialogs.SelectedIndexChanged += new System.EventHandler(this.listBox_listDialogs_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Лист диалогов на текущем аккаунте";
            // 
            // SearchIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(268, 389);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_listDialogs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_runSearch);
            this.Controls.Add(this.textBox_chatIds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SearchIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T-ISB :: Найти ID чата";
            this.Load += new System.EventHandler(this.SearchIDForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_chatIds;
        private System.Windows.Forms.Button button_runSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_listDialogs;
        private System.Windows.Forms.Label label2;
    }
}