using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISB.Handler;
using TISB.Targets;
using TL;
using WTelegram;
using WTelegramClient.Extensions.Updates;

namespace TISB.Tasks {
    public class ReactionsTask {
        private Dictionary<long, ChatBase> Chats = new Dictionary<long, ChatBase>();
        private async Task<Dictionary<long, ChatBase>> GetChats(Client client) {
            var chat = await client.Messages_GetAllChats();

            foreach (var wChat in chat.chats) {
                if (!Chats.ContainsKey(wChat.Key) && Chats.Count < 10) {
                    Chats.Add(wChat.Key, wChat.Value);
                }
                else {
                    break;
                }
            }

            return Chats;
        }
        private async Task<Reaction> FullChat(Account acc, InputPeer input, ReactionsTarget rtt) {
            var rt = acc.ReactionsTaskSettings.Targets;
            var wClient = acc.Client;
            var all_emoji = await wClient.Messages_GetAvailableReactions();
            var chat = await GetChats(wClient);

            var full = await wClient.GetFullChat(chat[Convert.ToInt64(rtt.ChatId)]);
            Reaction reaction = full.full_chat.AvailableReactions switch {
                ChatReactionsSome some => some.reactions[0],
                ChatReactionsAll all =>
                    all.flags.HasFlag(ChatReactionsAll.Flags.allow_custom) && wClient.User.flags.HasFlag(TL.User.Flags.premium)
                    ? new ReactionCustomEmoji { document_id = 5190875290439525089 }
                    : new ReactionEmoji { emoticon = all_emoji.reactions[new Random().Next(all_emoji.reactions.Length)].reaction },
                _ => null
            };

            if (reaction == null)
                return null;

            return reaction;
        }
        public async Task SendReactions(Account acc, ReactionsTarget rt) {
            var wClient = acc.Client;
            var rts = acc.ReactionsTaskSettings;
            var chatId = long.Parse(rt.ChatId);

            var reaction = await FullChat(acc, null, rt);

            wClient.RegisterUpdateType<UpdateNewChannelMessage>(async (update, @base) => {
                if (update.message is not TL.Message message)
                    return;
                var inputPeer = @base.UserOrChat(message.Peer).ToInputPeer();
                if (inputPeer.ID !=
                long.Parse(acc.ReactionsTaskSettings.Targets[new Random().Next(acc.ReactionsTaskSettings.Targets.Count)].ChatId))
                    return;
                if (@base is null)
                    return;

                await wClient.Messages_SendReaction(inputPeer, message.id, [reaction]);

                await Task.Delay(rts.Delay);
            });
        }

        public async Task RunAsync(Account acc) {
            var index = -1;
            var target = acc.ReactionsTaskSettings.Targets;

            await GetChats(acc.Client);

            index = (index + 1) % target.Count;
            var rt = target[index];

            await SendReactions(acc, rt);
        }
    }
}
