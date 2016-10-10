using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NeverClicker {
	public enum TaskKind {
		Invocation,
		Profession,
	}

	[Serializable]
	public struct GameTask : IComparable<GameTask>, ISerializable {
		public readonly DateTime StartTime;
		public readonly DateTime MatureTime;	
		public readonly uint CharIdx; 
		public readonly TaskKind Kind; 
		public readonly int TaskId;
		public readonly float BonusFactor;

		public string CharIdxLabel { get { return "Character_" + CharIdx.ToString(); } }

		public GameTask(DateTime startTime, DateTime matureTime, uint charIdx, TaskKind kind, int taskId, float bonusFactor) {
			this.StartTime = startTime;
			this.MatureTime = matureTime;			
			this.CharIdx = charIdx;
			this.Kind = kind;
			this.TaskId = taskId;
			this.BonusFactor = bonusFactor;
		}

		public int CompareTo(GameTask task) {
			return this.MatureTime.Ticks.CompareTo(task.MatureTime);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("MatureTime", MatureTime);
			info.AddValue("CharacterIdx", CharIdx);
			info.AddValue("TaskKind", Kind);
		}

		public GameTask AddTicks(int ticks) {
			//this.MatureTime = this.MatureTime.AddTicks(ticks);
			return new GameTask(this.StartTime, this.MatureTime.AddTicks(ticks), this.CharIdx, this.Kind, this.TaskId, this.BonusFactor);
        }

		public string XmlNodeName { get {
			return GenXmlNodeName(Kind, TaskId);
		} }

		public static string GenXmlNodeName(TaskKind kind, int taskId) {
			if (kind == TaskKind.Invocation) {
				return "I";
			} else if (kind == TaskKind.Profession) {
				return "P" + taskId.ToString();
			} else {
				return "UNKNOWN";
			}
		}

		public void AppendXmlElement(XmlElement tasksNode) {
			var newTaskNode = (XmlElement)tasksNode.AppendChild(tasksNode.OwnerDocument.CreateElement(XmlNodeName));
			SetXmlAttribs(newTaskNode);
		}

		public void SetXmlAttribs(XmlElement taskNode) {
			taskNode.SetAttribute("StartTime", this.StartTime.ToString("o"));
			taskNode.SetAttribute("MatureTime", this.MatureTime.ToString("o"));
			taskNode.SetAttribute("CharIdx", this.CharIdx.ToString());		
			taskNode.SetAttribute("TaskKind", this.Kind.ToString());			
			taskNode.SetAttribute("TaskId", this.TaskId.ToString());
			taskNode.SetAttribute("BonusFactor", this.BonusFactor.ToString());
		}

		public static GameTask FromXmlElement(XmlElement taskNode) {
			TaskKind kind;
			int taskId;

			if (taskNode.Name[0] == 'I') {
				kind = TaskKind.Invocation;
				taskId = int.Parse(taskNode.GetAttribute("TaskId"));
			} else if (taskNode.Name[0] == 'P') {
				kind = TaskKind.Profession;
				taskId = int.Parse(taskNode.Name[1].ToString());
			} else {
				throw new Exception("GameTask::FromXmlElement: Invalid xml element.");
			}
			
			DateTime startTime = DateTime.Parse(taskNode.GetAttribute("StartTime"));
			DateTime matureTime = DateTime.Parse(taskNode.GetAttribute("MatureTime"));
			uint charIdx = uint.Parse(taskNode.GetAttribute("CharIdx"));
			TaskKind kindCheck = (TaskKind)Enum.Parse(typeof(TaskKind), taskNode.GetAttribute("TaskKind"));
			int taskIdCheck = int.Parse(taskNode.GetAttribute("TaskId"));
			float bonusFactor = float.Parse(taskNode.GetAttribute("BonusFactor"));

			if (kind != kindCheck || taskId != taskIdCheck) {
				throw new Exception("GameTask::FromXmlElement: Error parsing xml element. Values do not check out.");
			}

			return new GameTask(startTime, matureTime, charIdx, kind, taskId, bonusFactor);
		}
	}	
}
