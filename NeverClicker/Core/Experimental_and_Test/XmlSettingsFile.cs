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
		}


		public void TestWrite() {
			var categoryName = "Awesome_Category";
			var categoryNode = DocumentElement.SelectSingleNode(categoryName);			

			if (categoryNode == null) {
				XmlElement categoryElement = SettingsXmlDoc.CreateElement(categoryName);
				SettingsXmlDoc.DocumentElement.AppendChild(categoryElement);
			}

			var settingName = "Setting_0";
			var settingNode = categoryNode.SelectSingleNode(settingName);

			if (settingNode == null) {
				XmlElement settingElement = SettingsXmlDoc.CreateElement(settingName);
				//settingElement.Value = "5";
				settingElement.InnerText = "Value_0";
				categoryNode.AppendChild(settingElement);
			}			

			MessageBox.Show("InnerText: " +
				DocumentElement[categoryName].SelectSingleNode(settingName).InnerText.ToString() +
				", Value: " +
				//DocumentElement[categoryName].SelectSingleNode(settingName).Value.ToString()
				""
			);

			//var setting = (XmlElement)categoryElement.AppendChild(SettingsXmlDoc.CreateElement("setting"));
			//setting.InnerText = "Value";

			SettingsXmlDoc.Save(FileName);
		}


		public bool TryGetSetting(string settingName, string sectionName, out string settingVal) {
			try {
				//settingVal = DocumentElement[sectionName][settingName].Trim();
				settingVal = DocumentElement[sectionName][settingName].Value.Trim();

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

		//private SetAttribute(XmlElement) {

		//}
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