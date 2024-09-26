using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TISB.Forms {
    public partial class LogForm : Form {
        private static readonly ConcurrentQueue<string> Logs = new ConcurrentQueue<string>();
        public LogForm() {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            LogUpdater_Tick(null, null);
        }
        private static StringBuilder _strBuild = new StringBuilder();
        public static void PushToLog(string info) {
            lock (Logs) {
                Logs.Enqueue($"[{DateTime.Now.ToShortTimeString()}]: {info}");

                _strBuild.AppendLine(info);

                using (StreamWriter sw = new StreamWriter("LOGGERS.txt", true)) {
                    sw.WriteLine($"[{DateTime.Now.ToShortTimeString()}]: {info}");
                }
            }
        }

        private void LogUpdater_Tick(object sender, EventArgs e) {
            if (!richTextBox1.IsDisposed && richTextBox1.IsHandleCreated) {
                string[] logsArray = Logs.ToArray();
                Array.Reverse(logsArray);
                BeginInvoke(new Action(() => richTextBox1.Clear()));

                foreach (string log in logsArray)
                    BeginInvoke(new Action(() => richTextBox1.AppendText(log + "\n")));
            }
        }

        private void LogForm_Shown(object sender, EventArgs e) => Focus();
    }
}
