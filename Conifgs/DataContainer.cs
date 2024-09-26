using Newtonsoft.Json;
using System.IO;
using TBattels.Conifgs.Themes;
using TISB.Conifgs;

namespace TBattels.Conifgs {
    internal static class DataContainer {
        public static ThemeConfig ThemeConfig;
        public static AuthConfig AuthConfig;

        public static void LoadConfig() {
            try {
                ThemeConfig = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText("Configs\\themeConfig.json"));
            }
            catch {
                ThemeConfig = new ThemeConfig();
            }

            try {
                AuthConfig = JsonConvert.DeserializeObject<AuthConfig>(File.ReadAllText("Configs\\authConfig.json"));
            }
            catch {
                AuthConfig = new AuthConfig();
            }
        }
    }
}
