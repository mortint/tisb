using System.IO;
using System.Linq;
using System.Windows.Forms;
using TBattels.Conifgs;

namespace TISB {
    public partial class MainForm {
        private void LoadFormGlobalConfig() {
            DataContainer.LoadConfig();

            try {
                SetBackgroundImage(DataContainer.ThemeConfig.BackgroundImage);
            }
            catch {

            }

            try {
                var avs = Directory.GetFiles("Uploads").Select(Path.GetFileName).ToList();
                var phrases = Directory.GetFiles("Phrases").Select(Path.GetFileName).ToList();

                var comboBoxFloodAdded = (DataGridViewComboBoxColumn)dataGridView_addedTarget.Columns[3];
                var comboboxFloodAddedPhrases = (DataGridViewComboBoxColumn)dataGridView_addedTarget.Columns[4];

                var comboboxAutoansAddedImage = (DataGridViewComboBoxColumn)dataGridView_autoansTargetAdded.Columns[3];
                var comboboxAutoansAddedPhrases = (DataGridViewComboBoxColumn)dataGridView_autoansTargetAdded.Columns[4];

                comboBoxFloodAdded.Items.Clear();
                comboboxFloodAddedPhrases.Items.Clear();

                comboboxAutoansAddedPhrases.Items.Clear();
                comboboxAutoansAddedImage.Items.Clear();

                comboBoxFloodAdded.Items.AddRange(avs.Concat(["Без фото"]).ToArray());
                comboboxFloodAddedPhrases.Items.AddRange(phrases.Concat(["Отсутствуют"]).ToArray());

                comboboxAutoansAddedImage.Items.AddRange(avs.Concat(["Без фото"]).ToArray());
                comboboxAutoansAddedPhrases.Items.AddRange(phrases.Concat(["Отсутствуют"]).ToArray());

                var reactions = Accounts[comboBox_accountsList.SelectedIndex];
                listBox_targetReactions.Items.Clear();
                reactions.ReactionsTaskSettings.Targets.ForEach(x => listBox_targetReactions.Items.Add(x.ChatId));

                if (listBox_targetReactions.Items.Count > 0)
                    listBox_targetReactions.SelectedIndex = 0;
            }
            catch {

            }
        }
    }
}
