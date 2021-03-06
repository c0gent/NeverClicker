﻿using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NeverClicker {
	public class AccountStates : XmlSettingsFile {
		public static string TasksNodeName = "tasks";
		public static string CharactersNodeName = "characters";

		public AccountStates() : base("accountStates") {
			base.SaveFile();
		}

		public AccountStates(string oldIniFileName) : base("accountStates") {
			if (File.Exists(oldIniFileName)) {
				MigrateIniSettings(oldIniFileName);
			}

			base.SaveFile();
		}

		public XmlElement CharNode(uint charIdx) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			return GetOrCreateSettingNode(charNodeName, CharactersNodeName);
		}

		public XmlElement CharTasksNode(uint charIdx) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, CharactersNodeName);
			var charTasksNode = charNode.SelectSingleNode(TasksNodeName);

			if (charTasksNode == null) {
				charTasksNode = charNode.AppendChild(Doc.CreateElement(TasksNodeName));
			}

			return (XmlElement)charTasksNode;
		}

		// Gets a `String` state value.
		public string GetCharState(uint charIdx, string settingName) {
			var charNode = CharNode(charIdx);

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = charNode.AppendChild(Doc.CreateElement(settingName));
			}

			return charSettingNode.InnerText;
		}

		// Gets a `String` state value or a provided default.
		public string GetCharStateOr(uint charIdx, string settingName, string valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);

			if (string.IsNullOrWhiteSpace(valCurrent)) {
				SaveCharState(valDefault, charIdx, settingName);
				return valDefault;
			} else {
				return valCurrent;
			}
		}

		// Gets an `int` state value or a provided default.
		public int GetCharStateOr(uint charIdx, string settingName, int valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);
			int valResult;

			if(!int.TryParse(valCurrent, out valResult)) {
				SaveCharState(valDefault, charIdx, settingName);
				valResult = valDefault;
			}

			return valResult;
		}

		// Gets a `DateTime` state value or a provided default.
		public DateTime GetCharStateOr(uint charIdx, string settingName, DateTime valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);
			DateTime valResult;

			if(!DateTime.TryParse(valCurrent, out valResult)) {
				SaveCharState(valDefault, charIdx, settingName);
				valResult = valDefault;
			}

			return valResult;
		}

		// Saves a `String` state value.
		public void SaveCharState(string settingVal, uint charIdx, string settingName) {
			var charNode = CharNode(charIdx);

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = charNode.AppendChild(Doc.CreateElement(settingName));
			}

			charSettingNode.InnerText = settingVal;
			SaveFile();
		}

		// Saves a `Int32` state value.
		public void SaveCharState(int settingVal, uint charIdx, string settingName) {
			SaveCharState(settingVal.ToString(), charIdx, settingName);
		}

		// Saves a `DateTime` state value.
		public void SaveCharState(DateTime settingVal, uint charIdx, string settingName) {
			SaveCharState(settingVal.ToString("o"), charIdx, settingName);
		}

		public bool TryGetCharTask(uint charIdx, TaskKind kind, int taskId, out GameTask task) {
			//var charTasksNode = (XmlElement)CharNode(charIdx).SelectSingleNode("Tasks");
			var charTasksNode = CharTasksNode(charIdx);
			var taskNodeName = GameTask.GenXmlNodeName(kind, taskId);
			var taskNode = (XmlElement)charTasksNode.SelectSingleNode(taskNodeName);

			if (taskNode != null) {
				try {
					task = GameTask.FromXmlElement(taskNode);
					return true;
				} catch (Exception) { }
			} 

			task = new GameTask();
			return false;
		}

		public void SaveCharTask(GameTask task) {
			//var charTasksNode = (XmlElement)CharNode(task.CharIdx).SelectSingleNode("Tasks");
			var charTasksNode = CharTasksNode(task.CharIdx);
			var taskNodeName = task.XmlNodeName;

			var taskNode = (XmlElement)charTasksNode.SelectSingleNode(taskNodeName);

			if (taskNode == null) {
				task.AppendXmlElement(charTasksNode);
			} else {
				task.SetXmlAttribs(taskNode);
			}

			SaveFile();
		}		

		// Migrates old ini settings.
		private void MigrateIniSettings(string oldIniFileName) {
			if (!File.Exists(base.FileName)) {
				var oldIni = new IniFile(oldIniFileName);

				for (uint charIdx = 0; charIdx < Global.Default.CharacterCount; charIdx++) {
					string charLabelZero = "Character " + charIdx.ToString();

					if (oldIni.SectionExists(charLabelZero)) {
						var invokesToday = oldIni.GetSettingOr("InvokesToday", charLabelZero, 0);
						var invokesCompleteFor = oldIni.GetSettingOr("InvokesCompleteFor", 
							charLabelZero, Global.Default.SomeOldDateString);
						var mostRecentInvocationTime = oldIni.GetSettingOr("MostRecentInvocationTime", 
							charLabelZero, Global.Default.SomeOldDateString);

						SaveCharState(invokesToday, charIdx, "invokesToday");
						SaveCharState(invokesCompleteFor,charIdx, "invokesCompleteFor");
						SaveCharState(mostRecentInvocationTime, charIdx, "mostRecentInvocationTime");

						for (int t_id = 0; t_id < 3; t_id++) {
							DateTime task_time;

							if (DateTime.TryParse(oldIni.GetSettingOr("MostRecentProfTime_" + t_id.ToString(), 
											charLabelZero, Global.Default.SomeOldDateString), out task_time)) {
								SaveCharTask(new GameTask(Global.Default.SomeOldDate, task_time, charIdx, 
									TaskKind.Profession, t_id, 0.0f));
							}
						}
					}
				}
			}
		}
	}
}
