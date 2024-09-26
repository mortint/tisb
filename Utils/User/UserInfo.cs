using Newtonsoft.Json;

namespace TISB.Utils.User {
    internal class UserInfo {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("is_access")] public int IsAccess { get; set; }
        [JsonProperty("license_exp_date")] public string LicenseExpDate { get; set; }
    }
}
