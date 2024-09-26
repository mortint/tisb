using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBattels.Conifgs;
using TISB.DataTransfer.API;
using TISB.Enums.MessageBox;
using TISB.Forms;
using TISB.Forms.Dialogs;
using TISB.Handler;
using TISB.Logging;
using TISB.Properties;

namespace TISB {
    public partial class MainForm : Form {
        public string UserLogin { get; set; }
        private bool IsActiveTask { get; set; } = false;

        private LogForm _logForm;
        private AccountsListForm _accountsListForm;

        private Account _account;
        public List<Account> Accounts { get; set; }

        private AccessChecker _accessChecker;
        private LicenseManager _licenseManager;

        private EventHandler _accountsEventHandler;

        public MainForm() {
            InitializeComponent();

            Accounts = new List<Account>();

            _licenseManager = new LicenseManager();
            _accessChecker = new AccessChecker();

            LogForm.PushToLog("Запись в LOGGERS.txt включена");

            Directory.CreateDirectory("Phrases");
            Directory.CreateDirectory("Uploads");
            Directory.CreateDirectory("Configs");
            Directory.CreateDirectory("Configs\\Accounts");

            _accountsEventHandler = (sender, e) => {
                if (Accounts.Count == 0) return;

                comboBox_accountsList.Items.Clear();

                comboBox_accountsList.Items.AddRange(Accounts.Select(x => $"{x.Phone} ({x.UserName})").ToArray());
                comboBox_accountsList.SelectedIndex = 0;

                DisabledControls();
            };
        }
        public void DisabledControls() {
            tabControl.Enabled = true;
            button_Run.Enabled = true;
        }
        private void OnAccountsChanged() => _accountsEventHandler?.Invoke(this, EventArgs.Empty);

        private async void MainForm_Shown(object sender, EventArgs e) {
            try {
                // отображение рекламы с сервера

                //var response = await Task.Run(() => {
                //    return APIserver.Request("ad.send.php", new Dictionary<string, string>
                //    {
                //        { "ad_text", label_textAd.Text },
                //        { "action", "get" }
                //    });
                //});

                //var jsonData = JsonConvert.DeserializeObject<AdJsonParse>(response);

                //if (jsonData.GetResponseStatus() == DataTransfer.JsonParser.Enums.ResponseStatus.Success) {
                //    label_textAd.Text = jsonData.Text;
                //}
                //else {
                //    label_textAd.Text = "здесь может быть\nваша реклама";
                //}
            }
            catch {

            }

            try {
                // проверка лицензии на валидность

                //_accessChecker.UserAccessChanged += (sender, isAccessEnabled) => {
                //    if (!isAccessEnabled) Application.Exit();
                //};

                //_accessChecker.StartAccessCheck(UserLogin);

                //Text += $" — {UserLogin}";

                //CheckTimeLicense(true);

                Accounts = await LoadAccountsAsync();

                OnAccountsChanged();

                async Task<List<Account>> LoadAccountsAsync() {
                    var accounts = new List<Account>();

                    try {
                        foreach (var account in Directory.GetFiles(@"Configs\Accounts", "*.json", SearchOption.TopDirectoryOnly)) {
                            try {
                                _account = JsonConvert.DeserializeObject<Account>(File.ReadAllText(account));

                                if (_account is null)
                                    throw new FileLoadException(account);

                                if (!await _account.AuthAsync()) {
                                    LogForm.PushToLog($"[{account}]: Не удалось авторизовать аккаунт");
                                    continue;
                                }

                                accounts.Add(_account);

                                LogForm.PushToLog($"Вы вошли как: {_account.Client.User.first_name}");
                            }
                            catch (FileLoadException) {
                                LogForm.PushToLog($"Не удалось загрузить JSON конфигурацию аккаунта: {account}");
                            }
                            catch (Exception ex) {
                                LogForm.PushToLog($"[{account}]: Не удалось загрузить аккаунт: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex) {
                        LogForm.PushToLog($"[{nameof(LoadAccountsAsync)}]: Неизвестная ошибка: {ex.Message}");
                    }

                    return accounts;
                }
            }
            catch (Exception ex) {
                LogForm.PushToLog("[app config error]" + ex.ToString());
                Logger.Show("Application configuration error.", icon: MessageIcon.Error);
                Application.Exit();
            }
        }

        private void toolStrip_openLogger_Click(object sender, EventArgs e) {
            _logForm?.Close();
            _logForm = new LogForm();
            _logForm.Show(this);
        }
        private void button_Run_Click(object sender, EventArgs e) {
            if (IsActiveTask) {
                LogForm.PushToLog("Бот остановлен");
                button_Run.BackgroundImage = Resources.Start;
                menu_reload.Enabled = true;

                foreach (var item in Accounts)
                    item.StopAllTask();
            }
            else {
                LogForm.PushToLog("Бот запущен");
                button_Run.BackgroundImage = Resources.Stop;
                menu_reload.Enabled = false;
                foreach (var item in Accounts)
                    item.StartAllTask();
            }

            IsActiveTask = !IsActiveTask;
        }

        private void SetBackgroundImage(string img) {
            BackgroundImage = !string.IsNullOrEmpty(img) && File.Exists(img) ? Image.FromFile(img) : null;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button_design_Click(object sender, EventArgs e) {
            var open = new OpenFileDialog {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Изменить фоновое изображение"
            };

            if (open.ShowDialog() == DialogResult.OK) {
                try {
                    BackgroundImage = new Bitmap(open.FileName);
                    BackgroundImageLayout = ImageLayout.Stretch;

                    DataContainer.ThemeConfig.BackgroundImage = open.FileName;
                    DataContainer.ThemeConfig.Save();
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }
            }
        }

        private async void button_autoTarget_Click(object sender, EventArgs e) {
            try {
                dataGridView_target.Rows.Clear();

                var allDialogs = await _account.Client.Messages_GetAllDialogs();

                foreach (var chat in allDialogs.chats) {
                    if (chat.Value.ToString().Contains("Group"))
                        dataGridView_target.Rows.Add(chat.Value.ToString().Replace("Group ", "chat=").Replace("\"", ""), "");
                }

                var account = Accounts[comboBox_accountsList.SelectedIndex];
                account.FlooderTaskSettings.ParseDataGrid(dataGridView_target);
                account.Save();
            }
            catch (Exception ex) {
                LogForm.PushToLog("[Автоцели]:" + ex.Message.ToString());
            }
        }

        private void button_searchID_Click(object sender, EventArgs e) {
            if (Accounts.Count == 0)
                return;

            var sForm = new SearchIDForm() {
                telegram = Accounts[comboBox_accountsList.SelectedIndex]
            };

            sForm.Show();
        }

        private void menu_reload_Click(object sender, EventArgs e) {
            LogForm.PushToLog("Контент приложения перезагружен");
            LoadFormGlobalConfig();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

        private void toolStrip_licenseInfo_Click(object sender, EventArgs e) {
            //CheckTimeLicense(false);

            Logger.Show("Функционал вырезан.", MessageButton.Ok, MessageIcon.Info);
        }

        private async void CheckTimeLicense(bool notify) {
            var message = await _licenseManager.GetRemainingLicenseTime(UserLogin, notify);

            if (notify && message.Contains("2 дня"))
                Logger.Show(message, icon: MessageIcon.Warning);
            else if (!notify)
                Logger.Show(message);
        }

        private void button_multiAccounts_Click(object sender, EventArgs e) {
            _accountsListForm?.Close();
            _accountsListForm = new AccountsListForm();
            _accountsListForm.MainForm = this;
            _accountsListForm.Accounts = Accounts;
            _accountsListForm.ComboBox = comboBox_accountsList;
            _accountsListForm.Show();
        }

        public void comboBox_accountsList_SelectedIndexChanged(object sender, EventArgs e) {
            if (Accounts.Count == 0) return;
            if (comboBox_accountsList.SelectedIndex == -1) return;

            LoadFormConfigFlooder();
            LoadFormConfigAutoans();
            LoadFormGlobalConfig();
        }

        private void label_textAd_Click(object sender, EventArgs e) {

        }

        private void button_addedTargetReactions_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(textBox_chatIDReactions.Text)) {
                Logger.Show("Укажите ID чата");
                return;
            }

            if (listBox_targetReactions.Items.Contains(textBox_chatIDReactions.Text)) {
                Logger.Show("Такой чат уже добавлен...", MessageButton.Error);
                return;
            }

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            var config = account.ReactionsTaskSettings;
            listBox_targetReactions.Items.Add(textBox_chatIDReactions.Text);
            config.ParseListBox(listBox_targetReactions);
            account.Save();
        }

        private void listBox_targetReactions_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox_targetReactions.SelectedItem != null) {
                textBox_chatIDReactions.Text = listBox_targetReactions.SelectedItem.ToString();
            }
        }

        private void button_changeTargetReactions_Click(object sender, EventArgs e) {
            if (listBox_targetReactions.SelectedItem != null) {
                int selectedIndex = listBox_targetReactions.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < listBox_targetReactions.Items.Count) {
                    if (listBox_targetReactions.Items[selectedIndex].ToString() == textBox_chatIDReactions.Text) {
                        Logger.Show("Нельзя заменить на такой же чат", MessageButton.Error);
                        return;
                    }

                    listBox_targetReactions.Items[selectedIndex] = textBox_chatIDReactions.Text;
                }

                var account = Accounts[comboBox_accountsList.SelectedIndex];
                var config = account.ReactionsTaskSettings;
                config.ParseListBox(listBox_targetReactions);
                account.Save();
            }
        }

        private void button_removeTargetReactions_Click(object sender, EventArgs e) {
            if (listBox_targetReactions.SelectedItem != null) {
                listBox_targetReactions.Items.Remove(listBox_targetReactions.SelectedItem);
            }

            textBox_chatIDReactions.Text = "";
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            var config = account.ReactionsTaskSettings;
            config.ParseListBox(listBox_targetReactions);
            account.Save();
        }

        private void checkBox_reactionsActive_CheckedChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            var config = account.ReactionsTaskSettings;
            config.Active = checkBox_reactionsActive.Checked;
            account.Save();
        }

        private void numericUpDown_reactionsDelay_ValueChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            var config = account.ReactionsTaskSettings;
            config.Delay = (int)numericUpDown_reactionsDelay.Value;
            account.Save();
        }
    }
}