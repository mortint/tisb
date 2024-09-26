using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TISB.Forms;
using TISB.Handler;
using TISB.Helpers;
using TISB.Targets.Autoans;
using TL;

namespace TISB.Tasks {
    internal struct StructMessages(List<int> ids, int offset) {
        public List<int> Ids { get; set; } = ids;
        public int Offset { get; set; } = offset;
    }

    internal class AutoansTask {
        private async Task<StructMessages?> SendMessage(Account account, AutoansTarget target, List<int> answeredMessages, DateTime now, int offset = 0) {
            var ats = account.AutoansTaskSettings;
            var link = LinkParse.Parse(target.ChatIds);
            var client = account.Client;
            var message = target.Name + ats.RandomPhrase(target);
            var targetIds = target.UserIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            if (link.Type == TypeLink.Chat) {
                if (ats.Chats.Count == 0) {
                    ats.MessagesDialogs = await client.Messages_GetAllDialogs();
                    if (ats.MessagesDialogs.chats != null) {
                        foreach (var item in ats.MessagesDialogs.chats) {
                            if (!ats.Chats.ContainsKey(item.Key)) {
                                ats.Chats.Add(item.Key, item.Value);
                            }
                        }
                    }
                }

                ChatBase chatBase = null;

                if (long.TryParse(link.Id, out long chatId) && ats.Chats.TryGetValue(chatId, out chatBase)) {
                    var history = await client.Messages_GetHistory(chatBase, add_offset: 0);

                    var messages = history.Messages.ToList();

                    if (messages.Count == 0) return new StructMessages { Ids = null, Offset = offset };

                    var targetMessages = messages
                        .Where(x => x.Date.AddHours(3) > now && targetIds.Contains(x.From.ID) && !answeredMessages.Contains(x.ID)).ToList();

                    if (targetMessages.Count == 0) return new StructMessages { Ids = null, Offset = offset };

                    var latestMessage = targetMessages.OrderByDescending(x => x.Date).FirstOrDefault();
                    var muted = await client.Messages_GetPeerSettings(chatBase);

                    if (latestMessage != null) {
                        await ats.HandleChatAction(target, chatBase, client, message, latestMessage.ID);
                        return new StructMessages { Ids = targetMessages.Select(x => x.ID).ToList(), Offset = messages.Count };
                    }
                }
            }

            return null;
        }



        public async Task RunAsync(Account account) {
            var ats = account.AutoansTaskSettings;
            var targets = ats.Targets;

            var tasks = targets.Select(async target => {
                var now = DateTime.Now;
                var offset = 0;
                var answeredMessages = new List<int>();
                var lockObj = new object();

                while (ats.Active && Account.IsRunning) {
                    try {
                        var messages = await SendMessage(account, target, answeredMessages, now, offset);

                        if (messages.HasValue) {
                            var structMessages = messages.Value;

                            if (structMessages.Ids != null) {
                                lock (lockObj) {
                                    offset += structMessages.Offset;
                                    answeredMessages.AddRange(structMessages.Ids);
                                }
                            }
                        }

                        await Task.Delay(ats.Delay);
                    }
                    catch (Exception ex) {
                        LogForm.PushToLog($"[Автоответчик]: {ex.Message}");
                    }
                }
            }).ToList();

            try {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex) {
                LogForm.PushToLog("[Error autoans]: " + ex.ToString());
            }
        }
    }
}