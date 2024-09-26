using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISB.Forms;
using TISB.Targets.Autoans;
using TL;
using WTelegram;

namespace TISB.Tasks.Settings {
    public class AutoansTaskSettings {
        private class ContainerConst {
            public static List<string> PhrasesList { get; set; }
            public static int ImgIndex { get; set; }
        }
        public List<AutoansTarget> Targets { get; set; }
        public bool Active { get; set; }
        public int Delay { get; set; } = 5000;

        [NonSerialized] public Dictionary<long, ChatBase> Chats = new Dictionary<long, ChatBase>();
        [NonSerialized] public Messages_Dialogs MessagesDialogs = null;
        [NonSerialized] public ChatBase ChatBase = null;
        [NonSerialized] public InputPeer InputPeers = null;
        [NonSerialized] private readonly Random _rnd;

        public AutoansTaskSettings() {
            Targets = new List<AutoansTarget>();

            _rnd = new Random();
        }
        public void ParseDataGridAutoans(DataGridView rows) {
            Targets.Clear();

            lock (rows) {
                foreach (DataGridViewRow row in rows.Rows) {
                    Targets.Add(new AutoansTarget {
                        ChatIds = Convert.ToString(row.Cells[0].Value ?? ""),
                        Name = Convert.ToString(row.Cells[1].Value ?? ""),
                        Contains = Convert.ToString(row.Cells[2].Value ?? ""),
                        PathImage = Convert.ToString(row.Cells[3].Value ?? ""),
                        PathPhrases = Convert.ToString(row.Cells[4].Value ?? ""),
                        UserIds = Convert.ToString(row.Cells[5].Value ?? "")
                    });
                }
            }
        }

        public string RandomPhrase(AutoansTarget at) {
            try {
                var pathOfFilePhrases = $"Phrases\\{at.PathPhrases}";

                if (!File.Exists(pathOfFilePhrases) ||
                    File.ReadAllLines(pathOfFilePhrases).Length == 0 || at.PathPhrases.Contains("Отсутствуют")) {
                    LogForm.PushToLog("Отсутствуют фразы или файл не найден");
                    return null;
                }

                ContainerConst.PhrasesList = File.ReadAllLines(pathOfFilePhrases).ToList();

                return ContainerConst.PhrasesList[_rnd.Next(ContainerConst.PhrasesList.Count)];
            }
            catch {
                LogForm.PushToLog($"[Автоответчик]: Ошибка при чтении файла");
                return null;
            }
        }

        public async Task HandleChatAction(AutoansTarget at, ChatBase chatBase, Client client, string message, int toReply = 0) {
            InputPeer peer = chatBase;

            try {
                switch (at.Contains) {
                    case "Текст": {
                            await client.SendMessageAsync(peer, message, reply_to_msg_id: toReply);
                            break;
                        }
                    case "Текст+фото": {
                            List<InputFileBase> uploadedFiles = await UploadPhotosAsync(at, client);

                            if (uploadedFiles == null || uploadedFiles.Count == 0) {
                                LogForm.PushToLog("Нет доступных изображений в папке Uploads");
                                return;
                            }

                            int imgIndex = (ContainerConst.ImgIndex + 1) % uploadedFiles.Count;

                            await client.SendMediaAsync(peer, message, uploadedFiles[imgIndex], reply_to_msg_id: toReply);

                            break;
                        }
                }
            }
            catch (Exception ex) {
                if (!ex.Message.Contains("CHAT_SEND_PLAIN_FORBIDDEN"))
                    LogForm.PushToLog($"[Автоответчик]: {ex.Message}");
            }
        }

        public async Task<List<InputFileBase>> UploadPhotosAsync(AutoansTarget at, Client client) {
            var uploadedFiles = new List<InputFileBase>();
            var targetImage = at.PathImage;

            if (targetImage == null || targetImage.Contains("Без фото")) {
                LogForm.PushToLog("Папка Uploads пуста...");
                return null;
            }

            var inputFile = await client.UploadFileAsync("Uploads\\" + targetImage);
            uploadedFiles.Add(inputFile);

            return uploadedFiles;
        }
    }
}
