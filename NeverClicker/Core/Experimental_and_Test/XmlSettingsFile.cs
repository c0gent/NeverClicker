using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NeverClicker.Core {
	public class XmlSettingsFile {
		XmlDocument SettingsXmlDoc;
		//XmlElement SessionElement;
		//string Name;
		string FileName;
		string RootElementName;
		//string SessionElementName;

		public XmlSettingsFile(string name) {
			//Name = name;
			FileName = @"c:\opt\" + name + ".xml.txt";
			RootElementName = name;
			SettingsXmlDoc = new XmlDocument();
		}

		public void Write() {
			if (!File.Exists(FileName)) {
				SettingsXmlDoc.AppendChild(SettingsXmlDoc.CreateElement(RootElementName));
				SettingsXmlDoc.Save(FileName);
			} else {
				SettingsXmlDoc.Load(FileName);
			}

			//var logMessage = new LogMessage(Core.Globals.OH_SHIT, LogEntryType.Info);

			//var SessionElementName = "session_name";
			var categoryName = "Awesome_Category";
			XmlElement categoryElement = SettingsXmlDoc.CreateElement(categoryName);
			//categoryElement.SetAttribute("id", DateTime.Now.ToFileTime().ToString());
			categoryElement.SetAttribute("misc_attrib", "I'm an attribute. Yay.");
			SettingsXmlDoc.DocumentElement.AppendChild(categoryElement);

			var setting = (XmlElement)categoryElement.AppendChild(SettingsXmlDoc.CreateElement("setting"));
			//setting.SetAttribute("time", DateTime.Now.ToString());
			//setting.SetAttribute("type", logMessage.Type.ToString());
			//setting.SetAttribute("text", logMessage.Text);
			setting.SetAttribute("Awesomeness_Factor", "5111");

			//setting.AppendChild(SettingsXmlDoc.CreateElement("H_111"));


			SettingsXmlDoc.Save(FileName);

			//SettingsXmlDoc.DocumentElement["TestSettings"]
			//	.AppendChild(SettingsXmlDoc.CreateElement("I_Append_things"));

			//XmlElement ele = SettingsXmlDoc.DocumentElement[categoryName];

			foreach (XmlNode node in SettingsXmlDoc.DocumentElement) {
				node.AppendChild(SettingsXmlDoc.CreateElement("I_Just_Appended_This_0"));
			}
			//ele.AppendChild(SettingsXmlDoc.CreateElement("I_Just_Appended_This"));

			SettingsXmlDoc.Save(FileName);
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