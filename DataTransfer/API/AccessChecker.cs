using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TISB.DataTransfer.JsonParser;
using TISB.DataTransfer.JsonParser.Enums;

namespace TISB.DataTransfer.API {
    internal class AccessChecker {
        public event EventHandler<bool> UserAccessChanged;

        private Timer AccessCheckTimer;

        private LicenseManager _licenseManager = new LicenseManager();
        public string LicenseExpDate { get; set; }

        public void StartAccessCheck(string login) => AccessCheckTimer = new Timer(CheckUserAccess, login, 0, 900000);

        private async void CheckUserAccess(object state) {
            var login = (string)state;

            try {
                var isAccessEnabled = await CheckUserAccess(login);
                UserAccessChanged?.Invoke(this, isAccessEnabled);

                var isLicenseValid = await _licenseManager.CheckLicense(login);

                if (!isLicenseValid)
                    UserAccessChanged?.Invoke(this, false);
            }
            catch //(Exception ex)
            {
                // LogForm.PushToLog("[Error license]: " + ex.ToString());     
            }
        }

        public async Task<bool> CheckUserAccess(string login) {
            var userResponse = await GetUserResponse(login);
            if (userResponse.GetResponseStatus() == ResponseStatus.Success)
                return userResponse.User.IsAccess != 0;
            else
                return false;
        }

        private async Task<UserJsonParse> GetUserResponse(string login) {
            var response = await Task.Run(() => APIserver.Request("users.get.php", new Dictionary<string, string>() { { "login", login } }));
            return JsonConvert.DeserializeObject<UserJsonParse>(response);
        }
    }
}
