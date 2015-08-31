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
using System.Collections;

namespace NeverClicker.Forms {
	public partial class MainForm : Form {
		AutomationEngine AutomationEngine;
		private void buttonExit_Click(object sender, EventArgs e) => Close();
		

		public MainForm() {
			InitializeComponent();	
		}

		private void MainForm_Load(object sender, EventArgs e) {
			comboBoxGameTaskType.DataSource = Enum.GetValues(typeof(GameTaskType));
			//cbStatus.DataSource = Enum.GetValues(typeof(Status));
		}

		private void MainForm_Shown(object sender, EventArgs e) {
			//Settings.Default.Upgrade();
			this.AutomationEngine = new AutomationEngine(this);
		}

		public void WriteLine(string message) {
			textBox1.AppendText(message + "\r\n");
		}		

		public void SettingsNotSet() {
			MessageBox.Show("Settings not configured. Opening settings menu.");
			var opt = new Options();
			opt.ShowDialog();
        }

		public void RefreshTaskQueue(SortedList<long, GameTask> taskList) {
			//this.listBoxTaskQueue.Items.Clear();
			//foreach (GameTask task in taskList.Values) {
			//	listBoxTaskQueue.Items.Add(task.MatureTime.ToShortTimeString() + "\t" + task.Type.ToString()
			//		+ "\tCharacter " + task.CharacterZeroIdx.ToString());
			//}

			//this.listBoxTaskQueue.DataSource = taskList.AsEnumerable();
			//this.listBoxTaskQueue.DisplayMember = taskList.Values[0].ToString();
		}

		private void buttonMoveMouse_Click(object sender, EventArgs e) {
			SetButtonStateRunning();
			this.AutomationEngine.MouseMovementTest();
		}

		private void buttonStop_Click(object sender, EventArgs e) {
			this.AutomationEngine.Stop();
		}

		private void buttonLoadOldScript_Click(object sender, EventArgs e) {
			AutomationEngine.InitOldScript();
		}

		private void buttonExecuteStatement_Click(object sender, EventArgs e) {
			AutomationEngine.ExecuteStatementAsync(textBoxExecuteStatement.Text);
		}

		private void buttonCheckVar_Click(object sender, EventArgs e) {
			WriteLine(textBox_var.Text + ": " + this.AutomationEngine.GetVar(textBox_var.Text) + "\r\n");
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
			WriteLine("Reloading Interactor...");
			AutomationEngine.Reload();
			WriteLine("Interactor reloaded.");
		}

		private void buttonSuspend_Click(object sender, EventArgs e) {
			AutomationEngine.TogglePause();

 			//if (AutomationEngine.Interactor.State == AutomationState.Running) {
			//	AutomationEngine.Interactor.Pause();
			//	buttonPause.Text = "Unpause";
			//} else if (AutomationEngine.Interactor.State == AutomationState.Paused) {
			//	AutomationEngine.Interactor.Unpause();
			//	buttonPause.Text = "Pause";
			//}
		}		

		private void buttonOptions_Click(object sender, EventArgs e) {
			var optionsForm = new Options();
			optionsForm.ShowDialog();
		}

		private void textBoxDetectWindow_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Enter) {
				buttonWindowDetect_Click(this, new EventArgs());
			}
		}

		private async void buttonWindowDetect_Click(object sender, EventArgs e) {
			string resultText;
			if (await AutomationEngine.DetectWindowAsync(textBoxDetectWindow.Text)) {
				resultText = "Found!";
			} else {
				resultText = "Not Found";
			}

			WriteLine(String.Format("'{0}': {1}", textBoxDetectWindow.Text, resultText));
			buttonWindowDetect.Text = resultText;
		}

		private void textBoxDetectWindow_TextChanged(object sender, EventArgs e) {
			buttonWindowDetect.Text = "Detect";
		}

		private void buttonAutoCycle_Click(object sender, EventArgs e) {
			this.SetButtonStateRunning();
			AutomationEngine.AutoCycle();
		}

		public void UpdateButtonState() {
			//switch (AutomationEngine.EvaluateStatementAsync) {
			//	case 
			//}
			WriteLine("UpdateButtonState(): not yet implemented.");
		}

		public void SetButtonStatePaused() {
			// SET STUFF UP
			this.buttonPause.Enabled = true;
			buttonPause.Text = "UnPause";
		}

		public void SetButtonStateRunning() {
			buttonAutoCycle.Enabled = false;
			//this.buttonAutoInvokeAsync.Enabled = false;
			this.buttonReload.Enabled = false;
			buttonPause.Text = "Pause";
			buttonPause.Enabled = true;


			this.buttonStop.Enabled = true;			
		}

		public void SetButtonStateStopped() {
			this.buttonAutoCycle.Enabled = true;
			//this.buttonAutoInvokeAsync.Enabled = true;
			this.buttonReload.Enabled = false;
			buttonPause.Text = "Pause";
			buttonPause.Enabled = false;

			this.buttonStop.Enabled = false;			
		}

		private void buttonAddCharIdx_Click(object sender, EventArgs e) {
			int charIdx;
			int delaySec;

			try {
				// TODO: CONVERT TO TRYPARSE()
				charIdx = int.Parse(this.textBoxGameTaskCharIdx.Text);
			} catch (FormatException) {
				WriteLine("Error converting character index.");
				return;
			}

			try {
				// TODO: CONVERT TO TRYPARSE()
				delaySec = int.Parse(this.textBoxGameTaskDelaySec.Text);
			} catch (FormatException) {
				WriteLine("Error converting delay.");
				return;
			}

			GameTaskType taskType;
			Enum.TryParse<GameTaskType>(this.comboBoxGameTaskType.SelectedValue.ToString(), out taskType);

			AutomationEngine.AddGameTask((uint)charIdx, delaySec);
		}

		private void buttonNextTask_Click(object sender, EventArgs e) {
			AutomationEngine.ProcessNextGameTask();
		}

		private void buttonFindImage_Click(object sender, EventArgs e) {
			AutomationEngine.ImageSearch(textBoxFindImage.Text);
			WriteLine("Test1");
		}

		private void buttonWindowInactivate_Click(object sender, EventArgs e) {
			AutomationEngine.WindowMinimize(textBoxDetectWindow.Text);
		}

		private void buttonWindowActivate_Click(object sender, EventArgs e) {
			AutomationEngine.WindowActivate(textBoxDetectWindow.Text);
		}

		private void buttonWindowKill_Click(object sender, EventArgs e) {
			AutomationEngine.WindowKill(textBoxDetectWindow.Text);
		}

		
	}
}
