using System;
using System.Windows.Forms;

namespace TISB.Forms.Dialogs {
    public partial class CodeForm : Form {
        public string AccPhone { get; set; } = "";
        public CodeForm() {
            InitializeComponent();
        }
        public string GetCode() {
            return textBox_code.Text;
        }
        private void button_Ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CodeForm_Shown(object sender, EventArgs e) {
            Text = "Вход: " + AccPhone;
        }
    }
}
