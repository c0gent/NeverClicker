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
	class ConfigSettingsFile {

		public void Write() {
			ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
			//var settingsFolderPath = Settings.Default.SettingsFolderPath;
			//configMap.ExeConfigFilename = settingsFolderPath + @"\XmlSettingsFileTest.config.xml.txt";
			configMap.ExeConfigFilename = @"c:\opt\XmlSettingsFileTest.config.xml.txt";
			Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

			//var cSec = new ConfigurationSection();

			//config.Sections.Add("Section1", this.Stuffs);

			config.Save();
		}
	}
}
