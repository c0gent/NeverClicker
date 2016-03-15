using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeverClicker.Forms {
	public partial class ErrorsForm: Form {
		MainForm MainForm;

		public ErrorsForm(MainForm mainForm) {
			InitializeComponent();
			MainForm = mainForm;
		}

		private void ErrorsForm_Load(object sender, EventArgs e) {

		}

		private void buttonClose_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
