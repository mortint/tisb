using System;
using System.Windows.Forms;

namespace TISB {
    public partial class MainForm {
        private void LoadFormConfigFlooder() {
            dataGridView_target.Rows.Clear();
            dataGridView_addedTarget.Rows.Clear();

            dataGridView_addedTarget.Rows.Add("", "", "Текст", "Без фото", "Отсутствуют");

            var account = Accounts[comboBox_accountsList.SelectedIndex];

            var fts = account.FlooderTaskSettings;

            foreach (var item in fts.Targets)
                dataGridView_target.Rows.Add(
                    item.UserName,
                    item.Name,
                    item.Contains,
                    item.ImageList,
                    item.PhrasesList);

            numericUpDown_FloodDelay.Value = fts.Delay;
            checkBox_activeFlood.Checked = fts.Active;

            checkBox_reactionsActive.Checked = account.ReactionsTaskSettings.Active;
            numericUpDown_reactionsDelay.Value = account.ReactionsTaskSettings.Delay;
        }

        private void checkBox_activeFlood_CheckedChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.FlooderTaskSettings.Active = checkBox_activeFlood.Checked;
            account.Save();
        }

        private void numericUpDown_FloodDelay_ValueChanged(object sender, EventArgs e) {
            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.FlooderTaskSettings.Delay = (int)numericUpDown_FloodDelay.Value;
            account.Save();
        }

        private void button_Added_Click(object sender, EventArgs e) {
            var row = dataGridView_addedTarget;

            dataGridView_target.Rows.Add(
                row[0, 0].Value,
                row[1, 0].Value,
                row[2, 0].Value,
                row[3, 0].Value,
                row[4, 0].Value);

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.FlooderTaskSettings.ParseDataGrid(dataGridView_target);
            account.Save();
        }

        private void button_Change_Click(object sender, EventArgs e) {
            int selIndex = dataGridView_target.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (selIndex == -1)
                return;

            for (int i = 0; i < dataGridView_target.Columns.Count; ++i)
                dataGridView_target[i, selIndex].Value = dataGridView_addedTarget[i, 0].Value;

            var account = Accounts[comboBox_accountsList.SelectedIndex];
            account.FlooderTaskSettings.ParseDataGrid(dataGridView_target);
            account.Save();
        }

        private void button_Deleted_Click(object sender, EventArgs e) {
            if (dataGridView_target.Rows.Count == 0)
                return;

            var account = Accounts[comboBox_accountsList.SelectedIndex];

            dataGridView_target.Rows.RemoveAt(dataGridView_target.SelectedRows[0].Index);

            account.FlooderTaskSettings.ParseDataGrid(dataGridView_target);
            account.Save();
        }

        private void dataGridView_target_SelectionChanged(object sender, EventArgs e) {
            int selIndex = dataGridView_target.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (selIndex == -1)
                return;
            for (int i = 0; i < dataGridView_target.Columns.Count; ++i)
                dataGridView_addedTarget[i, 0].Value = dataGridView_target[i, selIndex].Value;
        }
    }
}
