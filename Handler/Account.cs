using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISB.Conifgs;
using TISB.Forms;
using TISB.Forms.Dialogs;
using TISB.Tasks;
using TISB.Tasks.Settings;
using WTelegram;

namespace TISB.Handler {
    public class Account {
        private CancellationTokenSource TaskToken { get; set; }
        public FlooderTaskSettings FlooderTaskSettings { get; set; }
        public AutoansTaskSettings AutoansTaskSettings { get; set; }

        public ReactionsTaskSettings ReactionsTaskSettings { get; set; }

        [NonSerialized] public Client Client;

        [NonSerialized] public MainForm Form;
        public string Phone { get; set; }
        public string FileSession { get; set; }

        private readonly DirectoryInfo _sessionsDir;

        [NonSerialized] public static bool IsRunning = false;

        public string UserName { get; set; }

        public Account(string phone) {
            FlooderTaskSettings = new FlooderTaskSettings();
            AutoansTaskSettings = new AutoansTaskSettings();
            ReactionsTaskSettings = new ReactionsTaskSettings();

            _sessionsDir = new DirectoryInfo(@"Configs\Sessions");

            if (!_sessionsDir.Exists)
                _sessionsDir.Create();

            Phone = phone;

            FileSession = Path.Combine(_sessionsDir.FullName, $"{Phone}.session");
        }
        private string GetConfig(string wh) {
            return wh switch {
                "api_id" => GlobalConfig.AppId.ToString(),
                "api_hash" => GlobalConfig.APIKey,
                "phone_number" => Phone,
                "verification_code" => new Func<string>(() => {
                    using var cf = new CodeForm { AccPhone = Phone };

                    if (cf.ShowDialog() != DialogResult.OK)
                        return "cancelled";
                    return cf.GetCode();

                }).Invoke(),
                "session_pathname" => FileSession,
                _ => null
            };
        }

        public async Task<bool> AuthAsync() {
            try {
                Client = new Client(GetConfig);
                //await Client.Account_FinishTakeoutSession();
                await Client.LoginUserIfNeeded();
                UserName = Client.User.ToString() ?? Client.User.first_name;
                return true;
            }
            catch (Exception ex) {
                LogForm.PushToLog($"[Ошибка авторизации]: {ex.Message}");
                return false;
            }
        }

        public void Save() => File.WriteAllText($"Configs\\Accounts\\{Phone}.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        public void StartAllTask() {
            if (Client == null) {
                LogForm.PushToLog("Не загружено ни одного аккаунта");
                return;
            }

            IsRunning = true;

            //TaskToken = new CancellationTokenSource();

            var tasks = new List<Task>
            {
                new FlooderTask().RunAsync(this),
                new AutoansTask().RunAsync(this),
                new ReactionsTask().RunAsync(this)
            };

            Task.WhenAll(tasks);
        }

        public void StopAllTask() {
            IsRunning = false;
            //TaskToken?.Cancel();
        }
    }
}
