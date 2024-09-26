using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TISB.Handler;
using TISB.Helpers;

namespace TISB.Forms.Dialogs {
    public partial class SearchIDForm : Form {
        public Account telegram;
        public SearchIDForm() {
            InitializeComponent();
        }
        private async void GetChat() {
            var _client = telegram.Client;

            var userChats = await _client.Messages_GetAllDialogs();

            foreach (var chat in userChats.chats) {
                listBox_listDialogs.Items.Add($"{chat.Value.Title} - ID {chat.Value.ID}");
            }
        }
        private void ShowAndCopyToClipboard(string message, string value) {
            MessageBox.Show($"{message}\n\nСкопировано в буфер обмена", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clipboard.SetText(value);
        }
        private async void button_runSearch_Click(object sender, EventArgs e) {
            var _client = telegram.Client;

            try {
                if (_client != null) {
                    var idParse = LinkParse.Parse(textBox_chatIds.Text);

                    var userChats = await _client.Messages_GetAllDialogs();

                    if (idParse.Type == TypeLink.Chat) {
                        var chatBase = userChats.chats.Select(x => x.Value)
                                 .ToList().Find((x => x.Title.Contains(textBox_chatIds.Text.Trim().Replace("chat=", ""))));

                        ShowAndCopyToClipboard($"ID: {chatBase.ID}", chatBase.ID.ToString());
                    }
                    else if (idParse.Type == TypeLink.User) {
                        var userBase = userChats.users.Select(x => x.Value)
                            .ToList().Find(x => x.first_name.Contains(textBox_chatIds.Text.Trim().Replace("user=", "")));

                        ShowAndCopyToClipboard($"ID: {userBase.id}", userBase.id.ToString());
                    }
                    else
                        MessageBox.Show($"Неверный формат. Повторите попытку.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else {
                    MessageBox.Show($"Вы не вошли в аккаунт :(", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show($"Неизвестная ошибка...", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchIDForm_Load(object sender, EventArgs e) {
            GetChat();
        }

        private void listBox_listDialogs_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox_listDialogs.SelectedItem != null) {
                textBox_chatIds.Text = listBox_listDialogs.SelectedItem.ToString();
            }
        }
    }
}
