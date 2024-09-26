using System.Threading.Tasks;
using WTelegram;

namespace TISB.StatePattern {
    public interface IChatBotState {
        Task<IChatBotState> HandleMessage(string message, string chatID, Client client);
    }
}
