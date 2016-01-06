using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NeverClicker.Forms {
	public partial class TestsForm: Form {
		MainForm MainForm;
		//ExeConfigurationFileMap ConfigMap = new ExeConfigurationFileMap();
		//Configuration ConfigTest;
		//KeyValueConfigurationCollection TestSettings;

		private static readonly object Locker = new object();
		private static XmlDocument SettingsXmlDoc = new XmlDocument();		
		private const string SettingsRootElementName = "configTest";
		private string SettingsFileName = Settings.Default.SettingsFolderPath + "\\" + SettingsRootElementName + ".xml.txt";

		public TestsForm(MainForm mainForm) {			
			InitializeComponent();
			MainForm = mainForm;

			if (File.Exists(SettingsFileName)) {
				try {
					SettingsXmlDoc.Load(SettingsFileName);
				} catch (Exception ex) {
					MessageBox.Show(this, "LogFile::AppendMessage(): Error loading settings file. Please rename or delete. \r\n Error info: " + ex.ToString());
				}
			} else {
				try {
					var root = SettingsXmlDoc.CreateElement(SettingsRootElementName);
					SettingsXmlDoc.AppendChild(root);
					SettingsXmlDoc.Save(SettingsFileName);
				} catch (Exception ex) {
					MessageBox.Show(this, "LogFile::AppendMessage(): Error saving settings file: " + ex.ToString());
				}
			}

			//ConfigMap.ExeConfigFilename = Settings.Default.SettingsFolderPath + @"\configTest.txt";
			//configMap.LocalUserConfigFilename = Settings.Default.SettingsFolderPath + @"\configTest.config.xml.txt";
			//this.ConfigTest = ConfigurationManager.OpenMappedExeConfiguration(this.ConfigMap, ConfigurationUserLevel.None);
			//this.TestSettings = this.ConfigTest.AppSettings.Settings;
		}

		private void Tests_Load(object sender, EventArgs e) {
			comboBoxGameTaskType.DataSource = Enum.GetValues(typeof(TaskKind));
			textBoxFileName.Text = SettingsFileName;
		}

		private void buttonExecuteStatement_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ExecuteStatement(textBoxExecuteStatement.Text);
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
			if (await MainForm.AutomationEngine.DetectWindow(textBoxDetectWindow.Text)) {
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

			TaskKind taskType;
			Enum.TryParse(this.comboBoxGameTaskType.SelectedValue.ToString(), out taskType);

			MainForm.AutomationEngine.AddGameTask((uint)charIdx, delaySec);
		}

		private void buttonNextTask_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ProcessNextGameTask();
		}

		private void buttonFindImage_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ImageSearch(textBoxFindImage.Text);
			//MainForm.WriteLine("Test1");
		}

		private void buttonClickImage_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.ImageClick(textBoxFindImage.Text);
			//MainForm.WriteLine("Test1");
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

		private void buttonWindowMinimize_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.WindowMinimize(textBoxDetectWindow.Text);
		}

		private void buttonSendKeys_Click(object sender, EventArgs e) {
			MainForm.AutomationEngine.SendKeys(textBoxSendKeys.Text);
            //Interactions.Keyboard.Send();
		}


		private void buttonClose_Click(object sender, EventArgs e) {
			this.Close();
		}


		private void buttonReadSetting_Click(object sender, EventArgs e) {
			string readValue = SettingsXmlDoc.DocumentElement[this.textBoxSettingName.Text]?["node1"]?.GetAttribute("valueParam");
			string readValue2 = SettingsXmlDoc.DocumentElement[this.textBoxSettingName.Text]?["node2"]?.GetAttribute("valueParam");
			string readValue3 = SettingsXmlDoc.DocumentElement[this.textBoxSettingName.Text]?["node3"]?.GetAttribute("valueParam");
			if (readValue != null && readValue2 != null && readValue3 != null) {
				this.textBoxSettingValue.Text = readValue;
				this.textBoxSettingValue2.Text = readValue2;
				this.textBoxSettingValue3.Text = readValue3;
			} else {
				this.textBoxSettingValue.Text = "Invalid setting name.";
				this.textBoxSettingValue2.Text = "''";
				this.textBoxSettingValue3.Text = "''";
			}
		}

		private void buttonSaveSetting_Click(object sender, EventArgs e) {			
			if (!string.IsNullOrWhiteSpace(this.textBoxSettingName.Text)) {
				XmlNode selectedNode = SettingsXmlDoc.DocumentElement.SelectSingleNode(this.textBoxSettingName.Text);

				if (selectedNode != null) {
					SettingsXmlDoc.DocumentElement.RemoveChild(selectedNode);				
				}

				var settingElement = SettingsXmlDoc.CreateElement(this.textBoxSettingName.Text);

				settingElement.SetAttribute("attrib", "Test Attribute");				
				
				settingElement.AppendChild(SettingsXmlDoc.CreateElement("node1"));
				settingElement["node1"].SetAttribute("valueParam", this.textBoxSettingValue.Text);
				//settingElement["node1"].AppendChild(SettingsXmlDoc.CreateTextNode(this.textBoxSettingValue1.Text));

				settingElement.AppendChild(SettingsXmlDoc.CreateElement("node2"));
				settingElement["node2"].SetAttribute("valueParam", this.textBoxSettingValue2.Text);
				//settingElement["node2"].AppendChild(SettingsXmlDoc.CreateTextNode(this.textBoxSettingValue2.Text));

				settingElement.AppendChild(SettingsXmlDoc.CreateElement("node3"));
				settingElement["node3"].SetAttribute("valueParam", this.textBoxSettingValue3.Text);
				//settingElement["node3"].AppendChild(SettingsXmlDoc.CreateTextNode(this.textBoxSettingValue3.Text));

				
				//settingElement.AppendChild(SettingsXmlDoc.CreateTextNode("comment2"));
				//settingElement.AppendChild(SettingsXmlDoc.CreateTextNode("textNode3"));

				SettingsXmlDoc.DocumentElement.AppendChild(settingElement);

				SettingsXmlDoc.Save(SettingsFileName);

				//this.textBoxReadSettingValue.Text = this.textBoxSettingValue.Text;
				//this.textBoxReadSettingValue2.Text = this.textBoxSettingValue2.Text;
				//this.textBoxReadSettingValue3.Text = this.textBoxSettingValue3.Text;
			}
		}

		private void button3_Click(object sender, EventArgs e) {

		}


	}
}