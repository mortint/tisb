using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISB.DataTransfer.API;
using TISB.DataTransfer.JsonParser;
using TISB.DataTransfer.JsonParser.Enums;
using TISB.Enums.MessageBox;
using TISB.Logging;

namespace TISB.Forms.AuthForms {
    public partial class RegisterForm : Form {
        public RegisterForm() {
            InitializeComponent();
        }

        private async void button_signUp_Click(object sender, EventArgs e) => await RegisterUserAsync();

        async Task RegisterUserAsync() {
            var registrationData = new Dictionary<string, string>
            {
                { "login", textBox_login.Text },
                { "password", textBox_password.Text }
            };

            try {
                var response = await Task.Run(() => APIserver.Request("register.php", registrationData));
                var parsedResponse = JsonConvert.DeserializeObject<RegisterJsonParse>(response);

                if (parsedResponse.GetResponseStatus() == ResponseStatus.Success) {
                    Logger.Show("Вы успешно прошли регистрацию");
                    Hide();
                }
                else {
                    var errorMessage = parsedResponse.GetErrorMessage() ?? "Не удалось выполнить регистрацию";
                    Logger.Show(errorMessage, icon: MessageIcon.Error);
                }
            }
            catch (Exception ex) {
                Logger.Show($"Неизвестная ошибка: {ex.Message}", icon: MessageIcon.Error);
            }
        }
    }
}
