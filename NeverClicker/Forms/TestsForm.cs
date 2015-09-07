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
	public partial class TestsForm: Form {
			MainForm MainForm;

		public TestsForm(MainForm mainForm) {			
			InitializeComponent();
			MainForm = mainForm;
		}

		private void Tests_Load(object sender, EventArgs e) {
			comboBoxGameTaskType.DataSource = Enum.GetValues(typeof(GameTaskType));	
		}

		private void buttonExecuteStatement_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ExecuteStatementAsync(textBoxExecuteStatement.Text);
		}

		private void buttonCheckVar_Click(object sender, EventArgs e) {
			MainForm.WriteLine(textBox_var.Text + ": " + MainForm.AutomationEngine.GetVar(textBox_var.Text) + "\r\n");
		}

		private void buttonExecuteFunction_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.EvaluateFunction(
				textBoxExecuteFunction.Text,
				textBoxExecuteFunctionP1.Text,
				textBoxExecuteFunctionP2.Text,
				textBoxExecuteFunctionP3.Text
			);
		}

		private async void buttonWindowDetect_Click(object sender, EventArgs e) {
			string resultText;
			if (await MainForm.AutomationEngine.DetectWindowAsync(textBoxDetectWindow.Text)) {
				resultText = "Found!";
			} else {
				resultText = "Not Found";
			}

			MainForm.WriteLine(string.Format("'{0}': {1}", textBoxDetectWindow.Text, resultText));
			buttonWindowDetect.Text = resultText;
		}

		private void textBoxDetectWindow_TextChanged(object sender, EventArgs e) {
			buttonWindowDetect.Text = "Detect";
		}

		private void textBoxDetectWindow_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Enter) {
				buttonWindowDetect_Click(this, new EventArgs());
			}
		}

		private void buttonAddCharIdx_Click(object sender, EventArgs e) {
			int charIdx;
			int delaySec;

			try {
				// TODO: CONVERT TO TRYPARSE()
				charIdx = int.Parse(this.textBoxGameTaskCharIdx.Text);
			} catch (FormatException) {
				MainForm.WriteLine("Error converting character index.");
				return;
			}

			try {
				// TODO: CONVERT TO TRYPARSE()
				delaySec = int.Parse(this.textBoxGameTaskDelaySec.Text);
			} catch (FormatException) {
				MainForm.WriteLine("Error converting delay.");
				return;
			}

			GameTaskType taskType;
			Enum.TryParse(this.comboBoxGameTaskType.SelectedValue.ToString(), out taskType);

			MainForm.AutomationEngine.AddGameTask((uint)charIdx, delaySec);
		}

		private void buttonNextTask_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ProcessNextGameTask();
		}

		private void buttonFindImage_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ImageSearch(textBoxFindImage.Text);
			MainForm.WriteLine("Test1");
		}

		private void buttonWindowInactivate_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.WindowMinimize(textBoxDetectWindow.Text);
		}

		private void buttonWindowActivate_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.WindowActivate(textBoxDetectWindow.Text);
		}

		private void buttonWindowKill_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.WindowKill(textBoxDetectWindow.Text);
		}

		private void buttonSendKeys_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.SendKeys(textBoxSendKeys.Text);
            //Interactions.Keyboard.Send();
		}
	}
}
