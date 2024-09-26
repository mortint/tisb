using Newtonsoft.Json;
using System.IO;

namespace TBattels.Conifgs.Themes {
    internal sealed class ThemeConfig : IConfig {
        [JsonProperty("background_image")] public string BackgroundImage { get; set; }

        public ThemeConfig() {
            BackgroundImage = null;
        }

        public void Save() => File.WriteAllText("Configs\\themeConfig.json", JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}
