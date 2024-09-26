using System.Windows.Forms;
using TISB.Enums.MessageBox;

namespace TISB.Logging {
    internal class Logger {
        public static void Show(string message, MessageButton button = MessageButton.Ok, MessageIcon icon = MessageIcon.Info) {
            MessageBoxIcon messageType = MessageBoxIcon.Information;

            switch (icon) {
                case MessageIcon.Info:
                    messageType = MessageBoxIcon.Information;
                    break;
                case MessageIcon.Warning:
                    messageType = MessageBoxIcon.Warning;
                    break;
                case MessageIcon.Error:
                    messageType = MessageBoxIcon.Error;
                    break;
            }

            MessageBoxButtons messageButtonType = MessageBoxButtons.OK;

            switch (button) {
                case MessageButton.Ok:
                    messageButtonType = MessageBoxButtons.OK;
                    break;
                case MessageButton.Warning:
                    messageButtonType = MessageBoxButtons.OK;
                    break;
                case MessageButton.Error:
                    messageButtonType = MessageBoxButtons.OK;
                    break;
            }

            MessageBox.Show(message, "Message", messageButtonType, messageType);
        }
    }
}
