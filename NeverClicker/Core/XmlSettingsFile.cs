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
using System.Xml.XPath;

namespace NeverClicker {
	public class XmlSettingsFile {
		protected XmlDocument Doc;
		//XmlElement SessionElement;
		//string Name;
		public string FileName { get; private set; }
		string DocumentElementName;
		XmlElement Root;
		//string SessionElementName;

		public XmlSettingsFile(string name) {
			FileName = Settings.Default.SettingsFolderPath + "\\" + 
				name.First().ToString().ToUpper() + name.Substring(1) + ".xml.txt";
			DocumentElementName = name.First().ToString().ToLower() + name.Substring(1);
			Doc = new XmlDocument();

			if (!File.Exists(FileName)) {
				Doc.AppendChild(Doc.CreateElement(DocumentElementName));
			} else {
				Doc.Load(FileName);
			}

			if (Doc.DocumentElement == null) {
				Root = Doc.CreateElement(DocumentElementName);
				Doc.AppendChild(Root);
			} else {
				Root = Doc.DocumentElement;
			}
		}

		// Saves the document to disk.
		public void SaveFile() {
			Doc.Save(FileName);
		}


		////public XmlElement GetElement(string nodeName, )

		public XmlElement CreateSectionNode(string sectionName) {
			var sectionNode = Doc.CreateElement(sectionName);
			Root.AppendChild(sectionNode);
			//SaveFile();
			return sectionNode;
		}

		public XmlElement GetOrCreateSectionNode(string sectionName) {
			var sectionNode = Root.SelectSingleNode(sectionName) as XmlElement;

			if (sectionNode == null) {
				return CreateSectionNode(sectionName);
			} else {
				return sectionNode;
			}
		}

		public XmlElement CreateSettingNode(string settingName, XmlElement section) {
			//var section = GetOrCreateSection(sectionName);
			var settingNode = Doc.CreateElement(settingName);
			//setting.InnerText = val.ToString().Trim();
			section.AppendChild(settingNode);
			//SaveFile();
			return settingNode;
		}

		public XmlElement GetOrCreateSettingNode(string settingName, string sectionName) {
			var sectionNode = GetOrCreateSectionNode(sectionName);
			var settingNode = sectionNode.SelectSingleNode(settingName) as XmlElement;

			if (settingNode == null) {
				return CreateSettingNode(settingName, sectionNode);
			} else {
				return settingNode;
			}
		}

		// Returns a setting value if it exists or else creates the setting
		// with the specified default value and returns it.
		//
		public string GetSettingValOr(string settingName, string sectionName, string valDefault) {
			var settingNode = GetOrCreateSettingNode(settingName, sectionName);

			if (string.IsNullOrWhiteSpace(settingNode.InnerText)) {
				settingNode.InnerText = valDefault.Trim();
				SaveFile();
			}

			return settingNode.InnerText.Trim();
		}

		// Returns a setting value if it exists or else creates the setting
		// with the specified default value and returns it.
		//
		public int GetSettingValOr(string settingName, string sectionName, int valDefault) {
			var settingNode = GetOrCreateSettingNode(settingName, sectionName);
			int valResult;

			if(!int.TryParse(settingNode.InnerText, out valResult)) {
				settingNode.InnerText = valDefault.ToString();
				SaveFile();
				return valDefault;
			} else {
				return valResult;
			}
		}

		
		// Returns a setting value if it exists or else creates the setting
		// with the specified default value and returns it.
		//
		public DateTime GetSettingValOr(string settingName, string sectionName, DateTime valDefault) {
			var settingNode = GetOrCreateSettingNode(settingName, sectionName);
			DateTime valResult;

			if(!DateTime.TryParse(settingNode.InnerText, out valResult)) {
				settingNode.InnerText = valDefault.ToShortTimeString();
				SaveFile();
				return valDefault;
			} else {
				return valResult;
			}
		}

		// Gets a string setting value if the setting exists.
		public bool TryGetSetting(string settingName, string sectionName, out string settingVal) {
			try {
				settingVal = Root[sectionName][settingName].InnerText.Trim();

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

		// Gets an int setting value if the setting exists.
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


		public void SaveSetting(int val, string settingName, string sectionName) {
			var settingNode = GetOrCreateSettingNode(settingName, sectionName);
			settingNode.InnerText = val.ToString();
			SaveFile();
		}

		public void SaveSetting(string val, string settingName, string sectionName) {
			var settingNode = GetOrCreateSettingNode(settingName, sectionName);
			settingNode.InnerText = val.Trim();
			SaveFile();
		}

		//private SetAttribute(XmlElement) {

		//}


		public void TestWrite() {
			// Try to access a (section) node:
			var sectionName_0 = "Awesome_Category";
			var sectionNode = Root.SelectSingleNode(sectionName_0);			

			// If the (section) node is null, create it:
			if (sectionNode == null) {
				XmlElement sectionElement = Doc.CreateElement(sectionName_0);
				Doc.DocumentElement.AppendChild(sectionElement);
			}


			// Try to access a (setting) node:
			var settingName_0 = "Setting_0";
			var settingNode_0 = sectionNode.SelectSingleNode(settingName_0);

			// If The setting is null, set a default value:
			if (settingNode_0 == null) {
				XmlElement settingElement = Doc.CreateElement(settingName_0);
				//settingElement.Value = "5";
				settingElement.InnerText = "Value_0";
				sectionNode.AppendChild(settingElement);
			}

			string settingVal_0;

			if (TryGetSetting(settingName_0, sectionName_0, out settingVal_0)) {
				MessageBox.Show("[" + sectionName_0 + "][" + settingName_0  + "]: "  + settingVal_0);
			} else {
				MessageBox.Show("No dice pulling up [" + sectionName_0 + "][" + settingName_0 + "].");
			}


			// Try to access a (setting) node:
			var settingName_1 = "Setting_1";
			var settingNode_1 = sectionNode.SelectSingleNode(settingName_1);

			// If The setting is null, set a default value:
			if (settingNode_1 == null) {
				XmlElement settingElement = Doc.CreateElement(settingName_1);
				settingElement.InnerText = "1000";
				sectionNode.AppendChild(settingElement);
			}

			int settingVal_1;

			if (TryGetSetting(settingName_1, sectionName_0, out settingVal_1)) {
				MessageBox.Show("[" + sectionName_0 + "][" + settingName_1  + "]: "  + settingVal_1);
			} else {
				MessageBox.Show("No dice pulling up [" + sectionName_0 + "][" + settingName_1 + "].");
			}


			var sectionName_1 = "Boring_Category";


			var settingName_2 = "Setting_2__" + DateTime.Now.ToFileTime();
			int settingVal_2 = GetSettingValOr(settingName_2, sectionName_1, 9999);
			MessageBox.Show("[" + sectionName_1 + "][" + settingName_2  + "]: "  + settingVal_2);

			SaveFile();
		}
	}

	public class Stuff {
		public int Num { get; set; }
		public string Words { get; set; }
		public StuffKind Kind { get; set; }

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