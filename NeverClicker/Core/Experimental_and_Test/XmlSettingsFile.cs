using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NeverClicker.Core {
	public class XmlSettingsFile {
		XmlDocument SettingsXmlDoc;
		//XmlElement SessionElement;
		//string Name;
		string FileName;
		string DocumentElementName;
		XmlElement DocumentElement;
		//string SessionElementName;

		public XmlSettingsFile(string name) {
			//Name = name;
			FileName = @"c:\opt\" + name + ".xml.txt";
			DocumentElementName = name;
			SettingsXmlDoc = new XmlDocument();

			// ################# MOVE MOST OF THIS TO AN INIT FUNCTION OR HELPER CLASS ################# 
			if (!File.Exists(FileName)) {
				SettingsXmlDoc.AppendChild(SettingsXmlDoc.CreateElement(DocumentElementName));
				SettingsXmlDoc.Save(FileName);
			} else {
				SettingsXmlDoc.Load(FileName);
			}

			if (SettingsXmlDoc.DocumentElement == null) {
				DocumentElement = SettingsXmlDoc.CreateElement(DocumentElementName);
				SettingsXmlDoc.AppendChild(DocumentElement);
			} else {
				DocumentElement = SettingsXmlDoc.DocumentElement;
			}
			// ################ /MOVE ################
		}

		public bool TryGetSetting(string settingName, string sectionName, out string settingVal) {
			try {
				settingVal = DocumentElement[sectionName][settingName].InnerText.Trim();

				if (string.IsNullOrWhiteSpace(settingVal)) {
					settingVal = "";
					return false;
				} else {
					return true;
				}
			} catch (NullReferenceException) {
				settingVal = "";
				return false;
			}
		}

		public bool TryGetSetting(string settingName, string sectionName, out int settingValInt) {
			string settingValString;

			if (TryGetSetting(settingName, sectionName, out settingValString)) {
				if (int.TryParse(settingValString, out settingValInt)) {
					return true;
				} else {
					return false;
				}
 			} else {
				settingValInt = 0;
				return false;
			}
		}

		//private SetAttribute(XmlElement) {

		//}


		public void TestWrite() {
			// Try to access a (section) node:
			var sectionName = "Awesome_Category";
			var sectionNode = DocumentElement.SelectSingleNode(sectionName);			

			// If the (section) node is null, create it:
			if (sectionNode == null) {
				XmlElement sectionElement = SettingsXmlDoc.CreateElement(sectionName);
				SettingsXmlDoc.DocumentElement.AppendChild(sectionElement);
			}

			// Try to access a (setting) node:
			var settingName_0 = "Setting_0";
			var settingNode_0 = sectionNode.SelectSingleNode(settingName_0);

			// If The setting is null, set a default value:
			if (settingNode_0 == null) {
				XmlElement settingElement = SettingsXmlDoc.CreateElement(settingName_0);
				//settingElement.Value = "5";
				settingElement.InnerText = "Value_0";
				sectionNode.AppendChild(settingElement);
			}

			// Try to access a (setting) node:
			var settingName_1 = "Setting_1";
			var settingNode_1 = sectionNode.SelectSingleNode(settingName_1);

			// If The setting is null, set a default value:
			if (settingNode_1 == null) {
				XmlElement settingElement = SettingsXmlDoc.CreateElement(settingName_1);
				settingElement.InnerText = "1000";
				sectionNode.AppendChild(settingElement);
			}

			string settingVal_0;

			if (TryGetSetting(settingName_0, sectionName, out settingVal_0)) {
				MessageBox.Show("[" + sectionName + "][" + settingName_0  + "]: "  + settingVal_0);
			} else {
				MessageBox.Show("No dice pulling up [" + sectionName + "][" + settingName_0 + "].");
			}

			int settingVal_1;

			if (TryGetSetting(settingName_1, sectionName, out settingVal_1)) {
				MessageBox.Show("[" + sectionName + "][" + settingName_1  + "]: "  + settingVal_1);
			} else {
				MessageBox.Show("No dice pulling up [" + sectionName + "][" + settingName_1 + "].");
			}

			SettingsXmlDoc.Save(FileName);
		}
	}

	public class Stuff {
		public int Num {get; set;}
		public string Words {get; set;}
		public StuffKind Kind {get; set;}

		public Stuff(int num, string words, StuffKind kind) {
			this.Num = num;
			this.Words = words;
			this.Kind = kind;
		}
	}

	public enum StuffKind {
		Awesome,
		Super,
		Terrible,
	}

}


//[EXPERIMENTATION]:
		//public void Write() {
		//	if (!File.Exists(FileName)) {
		//		SettingsXmlDoc.AppendChild(SettingsXmlDoc.CreateElement(RootElementName));
		//		SettingsXmlDoc.Save(FileName);
		//	} else {
		//		SettingsXmlDoc.Load(FileName);
		//	}

		//	//var logMessage = new LogMessage(Core.Globals.OH_SHIT, LogEntryType.Info);

		//	//var SessionElementName = "session_name";
		//	var categoryName = "Awesome_Category";
		//	XmlElement categoryElement = SettingsXmlDoc.CreateElement(categoryName);
		//	//categoryElement.SetAttribute("id", DateTime.Now.ToFileTime().ToString());
		//	categoryElement.SetAttribute("misc_attrib", "I'm an attribute. Yay.");
		//	SettingsXmlDoc.DocumentElement.AppendChild(categoryElement);

		//	var setting = (XmlElement)categoryElement.AppendChild(SettingsXmlDoc.CreateElement("setting"));
		//	//setting.SetAttribute("time", DateTime.Now.ToString());
		//	//setting.SetAttribute("type", logMessage.Type.ToString());
		//	//setting.SetAttribute("text", logMessage.Text);
		//	setting.SetAttribute("Awesomeness_Factor", "5111");

		//	//setting.AppendChild(SettingsXmlDoc.CreateElement("H_111"));


		//	SettingsXmlDoc.Save(FileName);

		//	//SettingsXmlDoc.DocumentElement["TestSettings"]
		//	//	.AppendChild(SettingsXmlDoc.CreateElement("I_Append_things"));

		//	//XmlElement ele = SettingsXmlDoc.DocumentElement[categoryName];

		//	foreach (XmlNode node in SettingsXmlDoc.DocumentElement) {
		//		node.AppendChild(SettingsXmlDoc.CreateElement("I_Just_Appended_This_0"));
		//	}
		//	//ele.AppendChild(SettingsXmlDoc.CreateElement("I_Just_Appended_This"));

		//	SettingsXmlDoc.Save(FileName);
		//}



		//public void Write() {
		//	ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
		//	//var settingsFolderPath = Settings.Default.SettingsFolderPath;
		//	//configMap.ExeConfigFilename = settingsFolderPath + @"\XmlSettingsFileTest.config.xml.txt";
		//	configMap.ExeConfigFilename = @"c:\opt\XmlSettingsFileTest.config.xml.txt";
		//	Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

		//	//var cSec = new ConfigurationSection();

		//	//config.Sections.Add("Section1", this.Stuffs);

		//	config.Save();
		//}