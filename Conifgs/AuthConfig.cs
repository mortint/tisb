using Newtonsoft.Json;
using System.IO;
using TBattels.Conifgs;

namespace TISB.Conifgs {
    internal sealed class AuthConfig : IConfig {
        [JsonProperty("remember_me")] public bool RememberMe { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("login")] public string Login { get; set; }

        public AuthConfig() {
            RememberMe = false;
            Password = Login = "";
        }
        public void Save() =>
            File.WriteAllText("Configs\\authConfig.json", JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}
