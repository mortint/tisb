using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBattels.Conifgs;
using TISB.Conifgs;
using TISB.DataTransfer.API;
using TISB.DataTransfer.JsonParser;
using TISB.DataTransfer.JsonParser.Enums;
using TISB.Enums.MessageBox;
using TISB.HardwareInfo;
using TISB.Logging;

namespace TISB.Forms.AuthForms {
    public partial class AuthForm : Form {
        private RegisterForm _registerForm;
        private MainForm _mainForm;
        private DeviceDataRetriever _deviceDataRetriever;
        public AuthForm() {
            InitializeComponent();
        }

        private void AuthForm_Shown(object sender, EventArgs e) {
            textBox_login.Focus();
            KeyPreview = true;
            KeyDown += SignInKeyDows;

            try {
                Directory.CreateDirectory("Configs");

                DataContainer.LoadConfig();

                if (DataContainer.AuthConfig.RememberMe) {
                    textBox_login.Text = DataContainer.AuthConfig.Login;
                    textBox_password.Text = DataContainer.AuthConfig.Password;
                    checkBox_rememberMe.Checked = DataContainer.AuthConfig.RememberMe;
                }
                else {
                    DataContainer.AuthConfig = new AuthConfig();
                    DataContainer.AuthConfig.Save();
                }
            }
            catch {

            }
        }
        private void SignInKeyDows(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                button_signIn.PerformClick();
        }
        private async void button_signIn_Click(object sender, EventArgs e) => await AuthorizeUserAsync();
        async Task AuthorizeUserAsync() {
            _deviceDataRetriever = new DeviceDataRetriever();

            var registrationData = new Dictionary<string, string>
            {
                { "login", textBox_login.Text },
                { "password", textBox_password.Text },
                { "hard_driver_id", _deviceDataRetriever.GetHardwareID() }
            };

            try {
                var response = await Task.Run(() => APIserver.Request("authorize.php", registrationData));
                var parsedResponse = JsonConvert.DeserializeObject<AuthorizeJsonParse>(response);

                if (parsedResponse.GetResponseStatus() == ResponseStatus.Success) {
                    _mainForm?.Close();
                    _mainForm = new MainForm();
                    _mainForm.UserLogin = textBox_login.Text;
                    _mainForm.Show();
                    Hide();
                }
                else {
                    var errorMessage = parsedResponse.GetErrorMessage() ?? "Не удалось выполнить вход";
                    Logger.Show(errorMessage, icon: MessageIcon.Error);
                }
            }
            catch (Exception ex) {
                Logger.Show($"Неизвестная ошибка: {ex.Message}", icon: MessageIcon.Error);
            }
        }
        private void button_showFormRegister_Click(object sender, EventArgs e) {
            _registerForm?.Close();
            _registerForm = new RegisterForm();
            _registerForm.Show();
        }

        private void checkBox_rememberMe_CheckedChanged(object sender, EventArgs e) {
            DataContainer.AuthConfig.RememberMe = checkBox_rememberMe.Checked;
            DataContainer.AuthConfig.Save();
        }

        private void textBox_login_TextChanged(object sender, EventArgs e) {
            DataContainer.AuthConfig.Login = textBox_login.Text;
            DataContainer.AuthConfig.Save();
        }

        private void textBox_password_TextChanged(object sender, EventArgs e) {
            DataContainer.AuthConfig.Password = textBox_password.Text;
            DataContainer.AuthConfig.Save();
        }

        private void toolStrip_linkTelegram_Click(object sender, EventArgs e) => Process.Start(GlobalConfig.LinkAdmin);
    }
}
