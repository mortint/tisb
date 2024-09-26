using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISB.Handler;
using TISB.Logging;

namespace TISB.Forms {
    public partial class AccountsListForm : Form {
        public MainForm MainForm;
        public List<Account> Accounts;

        public ComboBox ComboBox;

        //[DllImport("user32.dll")]
        //private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        //[DllImport("user32.dll")]
        //private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public AccountsListForm() {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            //RegisterHotKey(Handle, 1, 0, (int)Keys.Enter);
            //RegisterHotKey(Handle, 2, (int)(Keys.Alt & Keys.Q), (int)Keys.Q);
            //RegisterHotKey(Handle, 3, (int)(Keys.Alt & Keys.W), (int)Keys.W); 
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            //UnregisterHotKey(Handle, 1); 
            //UnregisterHotKey(Handle, 2);
            //UnregisterHotKey(Handle, 3);
            //base.OnFormClosing(e);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0312 && Focused)  
        //    {
        //        var param = m.WParam.ToInt32();
        //        switch (param)
        //        {
        //            case 1:
        //                button__addedAccount_Click(null, null);
        //                break;
        //            case 2:
        //                button_deletedAccount_Click(null, null);
        //                break;
        //            case 3:
        //                button_authorize_Click(null, null);
        //                break;
        //        }
        //    }
        //    base.WndProc(ref m);
        //}
        private void button__addedAccount_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(textBox_phone.Text))
                return;

            dataGridView_accountsList.Rows.Add(textBox_phone.Text);

            textBox_phone.Text = null;
        }

        private void button_deletedAccount_Click(object sender, EventArgs e) {
            if (dataGridView_accountsList.Rows.Count == 0)
                return;

            dataGridView_accountsList.Rows.RemoveAt(dataGridView_accountsList.SelectedRows[0].Index);
        }

        private async void button_authorize_Click(object sender, EventArgs e) {
            try {
                var rows = dataGridView_accountsList.Rows.Cast<DataGridViewRow>().ToList();
                await ProcessAccountsAsync(rows);
                ComboBox.SelectedIndex = 0;
                MainForm.comboBox_accountsList_SelectedIndexChanged(null, null);
            }
            catch {
                Logger.Show("Ошибка сессии. Удалите файл сессии аккаунта.", Enums.MessageBox.MessageButton.Error, Enums.MessageBox.MessageIcon.Error);
            }
        }
        private async Task ProcessAccountsAsync(List<DataGridViewRow> rows) {
            ComboBox.Items.Clear();

            foreach (var row in rows) {
                string phone = row.Cells[0].Value.ToString();
                var account = new Account(phone);

                if (!File.Exists($"Configs\\Accounts\\{phone}.json")) {
                    var result = await account.AuthAsync();

                    if (result) {
                        Accounts.Add(account);
                        ComboBox.Items.Add($"{phone} ({account.UserName})");
                        MainForm.DisabledControls();
                        account.Save();
                    }
                    else
                        LogForm.PushToLog($"[{phone}]: Не удалось авторизовать аккаунт");
                }
                else {
                    account = JsonConvert.DeserializeObject<Account>(File.ReadAllText($"Configs\\Accounts\\{phone}.json"));
                    Accounts.Add(account);
                    ComboBox.Items.Add($"{phone} ({account.UserName})");
                }
            }
        }

        private void AccountsListForm_Shown(object sender, EventArgs e) {
            try {
                foreach (var acc in Accounts) dataGridView_accountsList.Rows.Add(acc.Phone);
            }
            catch {

            }
        }
    }
}
