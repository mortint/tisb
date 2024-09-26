using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISB.Forms;
using TISB.Helpers;
using TISB.Targets.Flooder;
using TL;
using WTelegram;

namespace TISB.Tasks.Settings {
    public class FlooderTaskSettings {
        private class ContainerConst {
            public static List<string> PhrasesList { get; set; }
            public static int ImgIndex { get; set; } = 0;
        }
        private Dictionary<long, ChatBase> Chats = new Dictionary<long, ChatBase>();
        public int Delay { get; set; } = 333;
        public bool Active { get; set; }
        public List<FlooderTarget> Targets { get; set; }

        private Messages_Dialogs MessagesDialogs = null;
        private ChatBase ChatBase = null;
        private InputPeer InputPeer = null;
        private readonly Random _rnd;
        public FlooderTaskSettings() {
            Targets = new List<FlooderTarget>();

            _rnd = new Random();
        }
        public string RandomPhrase(FlooderTarget ft) {
            try {
                var pathOfFilePhrases = $"Phrases\\{ft.PhrasesList}";

                if (!File.Exists(pathOfFilePhrases) ||
                    File.ReadAllLines(pathOfFilePhrases).Length == 0 || ft.PhrasesList.Contains("Отсутствуют")) {
                    LogForm.PushToLog("Отсутствуют фразы или файл не найден");
                    return null;
                }

                ContainerConst.PhrasesList = File.ReadAllLines(pathOfFilePhrases).ToList();

                return ContainerConst.PhrasesList[_rnd.Next(ContainerConst.PhrasesList.Count)];
            }
            catch {
                LogForm.PushToLog($"[Флудер]: Ошибка при чтении файла");
                return null;
            }
        }

        public void ParseDataGrid(DataGridView data) {
            lock (data) {
                Targets.Clear();

                foreach (DataGridViewRow row in data.Rows) {
                    Targets.Add(new FlooderTarget {
                        UserName = (row.Cells[0].Value ?? "").ToString(),
                        Name = (row.Cells[1].Value ?? "").ToString(),
                        Contains = (row.Cells[2].Value ?? "").ToString(),
                        ImageList = (row.Cells[3].Value ?? "").ToString(),
                        PhrasesList = (row.Cells[4].Value ?? "").ToString()
                    });
                }
            }
        }
        public async Task HandleChatAction(FlooderTarget ft, Client client, string message, int toReply = 0) {
            try {
                var link = LinkParse.Parse(ft.UserName);

                if (Chats.Count == 0) {
                    LogForm.PushToLog("Получаю все чаты...");
                    MessagesDialogs = await client.Messages_GetAllDialogs();
                    foreach (var item in MessagesDialogs.chats) {
                        Chats.Add(item.Key, item.Value);
                    }

                    LogForm.PushToLog($"Чаты [{Chats.Count}] успешно получены");
                }

                if (link.Type == TypeLink.Chat) {
                    if (long.TryParse(link.Id, out long chatId)) {
                        if (Chats.ContainsKey(chatId)) {
                            InputPeer = Chats[chatId];
                        }
                        else {
                            LogForm.PushToLog($"Чат с ID {chatId} не найден");
                        }
                    }
                    else {
                        ChatBase = MessagesDialogs.chats.Select(x => x.Value).ToList()
                            .Find(x => x.Title.Contains(link.Id));
                    }
                }

                switch (ft.Contains) {
                    case "Текст":

                        if (InputPeer != null)
                            await client.SendMessageAsync(InputPeer, message, null, toReply == 0 ? 0 : toReply);
                        else if (ChatBase != null)
                            await client.SendMessageAsync((InputPeer)ChatBase, message);
                        break;
                    case "Текст+фото":
                        List<InputFileBase> uploadedFiles = await UploadPhotosAsync(ft, client);

                        if (uploadedFiles == null || uploadedFiles.Count == 0)
                            return;

                        int imgIndex = (ContainerConst.ImgIndex + 1) % uploadedFiles.Count;
                        if (InputPeer != null)
                            await client.SendMediaAsync(InputPeer, message, uploadedFiles[imgIndex]);
                        else if (ChatBase != null)
                            await client.SendMediaAsync((InputPeer)ChatBase, message, uploadedFiles[imgIndex]);
                        break;
                }
            }
            catch (Exception ex) {
                LogForm.PushToLog($"[Флудер]: {ex.Message}");
            }
        }

        public async Task HandleUserAction(Client client, FlooderTarget ft, string message) {
            var link = LinkParse.Parse(ft.UserName);

            var userChats = await client.Messages_GetAllDialogs();

            switch (ft.Contains) {
                case "Текст":
                    await SendMessage(link.Id, message, userChats, client);
                    break;
                case "Текст+фото":
                    List<InputFileBase> uploadedFiles = await UploadPhotosAsync(ft, client);
                    await SendMessageWithMedia(link.Id, message, uploadedFiles, userChats, client);
                    break;
                default:
                    LogForm.PushToLog("Неизвестное действие");
                    break;
            }
        }

        private async Task SendMessage(string identifier, string message, Messages_Dialogs userChats, Client client) {
            if (long.TryParse(identifier, out _)) {
                long userId = long.Parse(identifier);
                InputPeer peer = userChats.users[userId];
                await client.SendMessageAsync(peer, message);
            }
            else {
                var found = await client.Contacts_ResolveUsername(identifier);

                foreach (var keyValuePair in found.users) {
                    await client.SendMessageAsync(keyValuePair.Value, message);
                    LogForm.PushToLog($"[Успешно]: сообщение отправлено {keyValuePair.Key}");
                }
            }
        }

        private async Task SendMessageWithMedia(string identifier, string message, List<InputFileBase> media, Messages_Dialogs userChats, Client client) {
            if (long.TryParse(identifier, out _)) {
                long userId = long.Parse(identifier);
                if (userChats.users.ContainsKey(userId)) {
                    InputPeer peer = userChats.users[userId];
                    if (ContainerConst.ImgIndex < media.Count)
                        await client.SendMediaAsync(peer, message, media[ContainerConst.ImgIndex]);
                    else
                        LogForm.PushToLog("[Флудер]: Ошибка при отправке изображения");
                }
                else {
                    LogForm.PushToLog("[Флудер]: Пользователь не найден");
                }
            }
            else {
                var found = await client.Contacts_ResolveUsername(identifier);
                foreach (var keyValuePair in found.users) {
                    if (ContainerConst.ImgIndex < media.Count)
                        await client.SendMediaAsync(keyValuePair.Value, message, media[ContainerConst.ImgIndex]);
                    else
                        LogForm.PushToLog("[Флудер]: Ошибка при отправке изображения");
                }
            }
        }

        public async Task<List<InputFileBase>> UploadPhotosAsync(FlooderTarget ft, Client client) {
            var uploadedFiles = new List<InputFileBase>();
            var targetImage = ft.ImageList;

            if (targetImage == null || targetImage.Contains("Без фото")) {
                LogForm.PushToLog("Нет доступных изображений в папке Uploads");
                return null;
            }

            var inputFile = await client.UploadFileAsync("Uploads\\" + targetImage);
            uploadedFiles.Add(inputFile);

            return uploadedFiles;
        }
    }
}
