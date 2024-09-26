using System.Threading.Tasks;
using WTelegram;

namespace TISB.StatePattern {
    public class ChatBot {
        private IChatBotState state;

        public int lastMessageId = 0;
        private readonly Client _client;

        public ChatBot(Client client) {
            _client = client;

        }

        public async Task ProcessMessage(string message, string chatID) {
            state = await state.HandleMessage(message, chatID, _client);
        }
    }
}
