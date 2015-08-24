using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using NeverClicker.Properties;


namespace NeverClicker.Forms {
	public partial class MainForm : Form {
		AutomationEngine AutomationEngine;
		private void buttonExit_Click(object sender, EventArgs e) => Close();
		public void WriteTextBox(string message) => textBox1.AppendText(message + "\r\n");

		public MainForm() {
			InitializeComponent();
			this.AutomationEngine = new AutomationEngine(this);
		}

		public Progress<string> GetTextBoxCallback() {
			return new Progress<string>(s => WriteTextBox(s));
		}

		private void buttonAutoInvokeAsync_Click(object sender, EventArgs e) {
			this.AutomationEngine.AutoInvoke();
		}

		private void buttonMoveMouse_Click(object sender, EventArgs e) {
			this.AutomationEngine.MouseMovementTest();
		}

		private void buttonStop_Click(object sender, EventArgs e) {
			this.AutomationEngine.Stop();
		}

		private void buttonLoadOldScript_Click(object sender, EventArgs e) {
			AutomationEngine.Interactor.InitOldAutoCyclerScript(GetTextBoxCallback());
		}

		private async void buttonExecuteStatement_Click(object sender, EventArgs e) {
			WriteTextBox(await AutomationEngine.EvaluateStatementAsync(textBoxExecuteStatement.Text));
		}

		private void buttonCheckVar_Click(object sender, EventArgs e) {
			WriteTextBox(textBox_var.Text + ": " + this.AutomationEngine.GetVar(textBox_var.Text) + "\r\n");
		}

		private void buttonExecuteFunction_Click(object sender, EventArgs e) {
			this.AutomationEngine.EvaluateFunction(
				textBoxExecuteFunction.Text,
				textBoxExecuteFunctionP1.Text,
				textBoxExecuteFunctionP2.Text,
				textBoxExecuteFunctionP3.Text
			);
		}

		private void buttonReload_Click(object sender, EventArgs e) {
			WriteTextBox("Reloading Interactor...");
			this.AutomationEngine.Interactor.Reload();
			WriteTextBox("Interactor reloaded.");
		}

		private void buttonSuspend_Click(object sender, EventArgs e) {

			if (AutomationEngine.Interactor.State == AutomationState.Running) {
				AutomationEngine.Interactor.Pause();
				buttonPause.Text = "Unpause";
			} else if (AutomationEngine.Interactor.State == AutomationState.Paused) {
				AutomationEngine.Interactor.Unpause();
				buttonPause.Text = "Pause";
			}
		}		

		private void buttonOptions_Click(object sender, EventArgs e) {
			var optionsForm = new Options();
			optionsForm.Show();
		}

		private void textBoxDetectWindow_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Enter) {
				buttonDetectWindow_Click(this, new EventArgs());
			}
		}

		private async void buttonDetectWindow_Click(object sender, EventArgs e) {
			string resultText;
			if (await AutomationEngine.DetectWindowAsync(textBoxDetectWindow.Text)) {
				resultText = "Found!";
			} else {
				resultText = "Not Found";
			}

			WriteTextBox(String.Format("'{0}': {1}", textBoxDetectWindow.Text, resultText));
			buttonDetectWindow.Text = resultText;
		}

		private void textBoxDetectWindow_TextChanged(object sender, EventArgs e) {
			buttonDetectWindow.Text = "Detect";
		}


		private void MainForm_Load(object sender, EventArgs e) {
			Settings.Default.Upgrade();
		}
	}
}
