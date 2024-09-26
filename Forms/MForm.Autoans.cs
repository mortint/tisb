using System;
using System.Windows.Forms;

namespace TISB {
    public partial class MainForm {
        private void LoadFormConfigAutoans() {
            dataGridView_autoansTarget.Rows.Clear();
            dataGridView_autoansTargetAdded.Rows.Clear();

            dataGridView_autoansTargetAdded.Rows.Add("", "", "Текст", "Без фото", "Отсутствуют", "");

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            var ats = account.AutoansTaskSettings;

            foreach (var item in ats.Targets) {
                dataGridView_autoansTarget.Rows.Add(
                    item.ChatIds,
                    item.Name,
                    item.Contains,
                    item.PathImage,
                    item.PathPhrases,
                    item.UserIds);
            }

            checkBox_activeAutoans.Checked = ats.Active;
            numericUpDown_autoansDelay.Value = ats.Delay;
        }
        private void checkBox_activeAutoans_CheckedChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.AutoansTaskSettings.Active = checkBox_activeAutoans.Checked;
            account.Save();
        }
        private void numericUpDown_autoansDelay_ValueChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.AutoansTaskSettings.Delay = (int)numericUpDown_autoansDelay.Value;
            account.Save();
        }
        private void button_autoansAdded_Click(object sender, EventArgs e) {
            var row = dataGridView_autoansTargetAdded;

            dataGridView_autoansTarget.Rows.Add(
                row[0, 0].Value,
                row[1, 0].Value,
                row[2, 0].Value,
                row[3, 0].Value,
                row[4, 0].Value,
                row[5, 0].Value);

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.AutoansTaskSettings.ParseDataGridAutoans(dataGridView_autoansTarget);
            account.Save();
        }
        private void button_autoansChange_Click(object sender, EventArgs e) {
            int selIndex = dataGridView_autoansTarget.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (selIndex == -1)
                return;

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            for (int i = 0; i < dataGridView_autoansTarget.Columns.Count; ++i)
                dataGridView_autoansTarget[i, selIndex].Value = dataGridView_autoansTargetAdded[i, 0].Value;
            account.AutoansTaskSettings.ParseDataGridAutoans(dataGridView_autoansTarget);
            account.Save();
        }
        private void button_autoansDeleted_Click(object sender, EventArgs e) {
            if (dataGridView_autoansTarget.Rows.Count == 0)
                return;

            dataGridView_autoansTarget.Rows.RemoveAt(dataGridView_autoansTarget.SelectedRows[0].Index);
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.AutoansTaskSettings.ParseDataGridAutoans(dataGridView_autoansTarget);
            account.Save();
        }
        private void dataGridView_autoansTarget_SelectionChanged(object sender, EventArgs e) {
            int selIndex = dataGridView_autoansTarget.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (selIndex == -1)
                return;
            for (int i = 0; i < dataGridView_autoansTarget.Columns.Count; ++i)
                dataGridView_autoansTargetAdded[i, 0].Value = dataGridView_autoansTarget[i, selIndex].Value;
        }
    }
}
