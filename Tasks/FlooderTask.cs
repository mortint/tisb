using System;
using System.Threading.Tasks;
using TISB.Forms;
using TISB.Handler;
using TISB.Helpers;
using TISB.Targets.Flooder;

namespace TISB.Tasks {
    public class FlooderTask {
        public async Task SendMessage(Account account, FlooderTarget to) {
            var fts = account.FlooderTaskSettings;
            var link = LinkParse.Parse(to.UserName);

            var message = to.Name + fts.RandomPhrase(to);

            if (string.IsNullOrEmpty(message))
                return;

            switch (link.Type) {
                case TypeLink.User:
                    await fts.HandleUserAction(account.Client, to, message);
                    break;
                case TypeLink.Chat:
                    await fts.HandleChatAction(to, account.Client, message);
                    break;
                default:
                    LogForm.PushToLog($"[Флудер]: {to.UserName} — некорректный формат ссылки");
                    break;
            }
        }

        public async Task RunAsync(Account account) {
            var fts = account.FlooderTaskSettings;
            var target = fts.Targets;
            int targetIter = -1;

            while (fts.Active && Account.IsRunning) {
                try {
                    targetIter = (targetIter + 1) % target.Count;

                    FlooderTarget ftg = target[targetIter];

                    try {
                        await SendMessage(account, ftg);
                    }
                    catch (Exception ex) {
                        LogForm.PushToLog($"[Флудер]: {ex}");
                    }

                    await Task.Delay(fts.Delay);
                }
                catch (Exception ex) {
                    LogForm.PushToLog($"[Error to flooder]: {ex.Message} at {DateTime.Now}");
                }
            }

        }
    }
}
