using System.Collections.Generic;
using System.Windows.Forms;
using TISB.Targets;

namespace TISB.Tasks.Settings {
    public class ReactionsTaskSettings {
        public List<ReactionsTarget> Targets { get; set; }

        public int Delay { get; set; } = 1000;

        public bool Active { get; set; }
        public ReactionsTaskSettings() => Targets = new List<ReactionsTarget>();

        public void ParseListBox(ListBox view) {
            Targets.Clear();

            foreach (var item in view.Items) {
                string chatId = item.ToString();
                Targets.Add(new ReactionsTarget {
                    ChatId = chatId
                });
            }
        }


    }
}
