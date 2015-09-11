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
		public AutomationEngine AutomationEngine;
		private void buttonExit_Click(object sender, EventArgs e) => Close();
		

		public MainForm() {
			InitializeComponent();

			Settings.Default.Upgrade();
			//Settings.Default.AssetsFolderPath = Settings.Default.UserRootFolderPath + "\\Assets";
		}

		private void MainForm_Load(object sender, EventArgs e) {
				
		}

		private void MainForm_Shown(object sender, EventArgs e) {			
			//if (!SettingsManager.SettingsAreValid()) {
			//	this.SetButtonStateAllDisabled();
			//	this.SettingsInvalid();

			if (!Settings.Default.NeverClickerConfigValid) {
				this.SetButtonStateAllDisabled();
				this.ConfigInvalid();
			} else {
				this.AutomationEngine = new AutomationEngine(this);
				this.checkBoxBeginOnStartup.Checked = Settings.Default.BeginOnStartup;

				if (this.checkBoxBeginOnStartup.Checked) {
					int delaySecs = 10;
					this.SetButtonStateRunning();
					WriteLine("Starting AutoCycle in " + delaySecs.ToString() + " seconds...");
					AutomationEngine.AutoCycle(delaySecs);
				}
			}			
		}

		public void WriteLine(string message) {
			textBox1.AppendText(message + "\r\n");
		}		

		public void ConfigInvalid() {
			MessageBox.Show(this, "Settings not configured properly. Opening settings menu.", "NeverClicker - Settings");
			OpenSettingsWindow();
        }

		public void ReloadSettings() {
			//this.AutomationEngine = new AutomationEngine(this);
			//AutomationEngine.ReloadSettings();
			//WriteLine("Reloading settings.");
			//SetButtonStateStopped();
			this.SetButtonStateAllDisabled();
			MessageBox.Show(this, "Please restart to apply settings.");
			this.Close();
		}

		private void OpenSettingsWindow() {
			var opt = new SettingsForm(this);
			opt.ShowDialog();
		}

		private void OpenTestsWindow() {
			var opt = new TestsForm(this);
			opt.ShowDialog();
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
			this.buttonTestsForm.Enabled = false;
			this.buttonOptionsForm.Enabled = false;
			//this.tabControlPrimary.Enabled = false;
			this.buttonStop.Enabled = true;
		}

		public void SetButtonStateStopped() {
			this.buttonAutoCycle.Enabled = true;
			this.buttonTestsForm.Enabled = true;
			this.buttonOptionsForm.Enabled = true;
			//this.buttonAutoInvokeAsync.Enabled = true;
			this.buttonReload.Enabled = false;
			buttonPause.Text = "Pause";
			buttonPause.Enabled = false;
			//this.tabControlPrimary.Enabled = true;
			this.buttonStop.Enabled = false;
		}

		public void SetButtonStateAllDisabled() {
			buttonAutoCycle.Enabled = false;
			//this.buttonAutoInvokeAsync.Enabled = false;
			this.buttonReload.Enabled = false;
			//buttonPause.Text = "Pause";
			buttonPause.Enabled = false;
			this.buttonStop.Enabled = false;
			//this.tabControlPrimary.Enabled = false;
		}
		

		private void buttonOptions_Click(object sender, EventArgs e) {
			OpenSettingsWindow();
		}
		
		public void RefreshTaskQueue(SortedList<long, GameTask> taskList) {
			//AutomationEngine.Log(new LogMessage("Refreshing task queue", LogEntryType.Debug));

			try {
				this.listBoxTaskQueue.Items.Clear();
				foreach (GameTask task in taskList.Values) {
					listBoxTaskQueue.Items.Add(task.MatureTime.ToShortTimeString().Trim() + "\t" + task.Kind.ToString() 
						+ "\t" + task.TaskId.ToString()
						+ "\tCharacter " + task.CharIdx.ToString());
				}
			} catch (Exception ex) {
				MessageBox.Show(this, "Error refreshing task queue: " + ex.ToString());
			}

			//AutomationEngine.Log(new LogMessage("Task queue is refreshed.", LogEntryType.Debug));

			// DEPRICATED this.listBoxTaskQueue.DataSource = taskList.AsEnumerable();
			// DEPRICATED this.listBoxTaskQueue.DisplayMember = taskList.Values[0].ToString();
		}

		private void buttonMoveMouse_Click(object sender, EventArgs e) {
			SetButtonStateRunning();
			this.AutomationEngine.MouseMovementTest();
		}

		private void buttonStop_Click(object sender, EventArgs e) {
			this.AutomationEngine.Stop();
		}

		private void buttonReload_Click(object sender, EventArgs e) {
			WriteLine("Reloading Interactor...");
			AutomationEngine.Reload();
			WriteLine("Interactor reloaded.");
		}

		private void buttonSuspend_Click(object sender, EventArgs e) {
			AutomationEngine.TogglePause();
		}		

		private void buttonAutoCycle_Click(object sender, EventArgs e) {
			this.SetButtonStateRunning();
			AutomationEngine.AutoCycle(0);
		}

		private void buttonTestsForm_Click(object sender, EventArgs e) {
			this.OpenTestsWindow();
		}

		private void checkBoxBeginOnStartup_CheckedChanged(object sender, EventArgs e) {
			Settings.Default.BeginOnStartup = this.checkBoxBeginOnStartup.Checked;
			Settings.Default.Save();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.AutomationEngine != null) {
				this.AutomationEngine.Log("NeverClicker Exiting.");
				this.AutomationEngine.Stop();
			}		
		}
	}
}
