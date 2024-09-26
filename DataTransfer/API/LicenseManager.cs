using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISB.DataTransfer.JsonParser;
using TISB.DataTransfer.JsonParser.Enums;
using TISB.Forms;

namespace TISB.DataTransfer.API {
    internal class LicenseManager {
        public async Task<bool> CheckLicense(string login) {
            var responseLicense = await Task.Run(() => APIserver.Request("license.checker.php", new Dictionary<string, string> { { "login", login } }));
            var jsonDateLicense = JsonConvert.DeserializeObject<UserJsonParse>(responseLicense); ;
            return jsonDateLicense.GetResponseStatus() != ResponseStatus.Failed;
        }

        public async Task<string> GetRemainingLicenseTime(string userLogin, bool notify) {
            var timeDate = await Task.Run(() => APIserver.Request("users.get.php", new Dictionary<string, string> { { "login", userLogin } }));
            var json = JsonConvert.DeserializeObject<UserJsonParse>(timeDate).User.LicenseExpDate.Split('-');

            var targetDate = new DateTime(int.Parse(json[0]), int.Parse(json[1]), int.Parse(json[2]));
            var currentDate = DateTime.Today;

            if (currentDate < targetDate) {
                TimeSpan remainingTime = targetDate - currentDate;
                var remainingDays = remainingTime.Days.ToString();
                var formattedDate = targetDate.ToString("d MMMM yyyy");

                if (remainingTime.Days == 2 && notify) {
                    return "До окончания лицензии осталось 2 дня. Успей продлить, чтобы продолжить пользоваться приложением!";
                }

                return $"Лицензия закончится: {formattedDate} (осталось {remainingDays} дней)";
            }

            LogForm.PushToLog("[Error license manager]: " + json.ToString());

            return "Error to license";
        }
    }
}
