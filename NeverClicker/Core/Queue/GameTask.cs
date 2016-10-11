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
		public static string NodeNameStartTime = "startTime";
		public static string NodeNameMatureTime = "matureTime";
		public static string NodeNameCharIdx = "charIdx";
		public static string NodeNameKind = "kind";
		public static string NodeNameTaskId = "taskId";
		public static string NodeNameBonusFactor = "bonusFactor";
		public static string NodePrefixInvocation = "i";
		public static string NodePrefixProfession = "p";

		public readonly DateTime StartTime;
		public readonly DateTime MatureTime;	
		public readonly uint CharIdx; 
		public readonly TaskKind Kind; 
		public readonly int TaskId;
		public readonly float BonusFactor;

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
			info.AddValue(NodeNameMatureTime, MatureTime);
			info.AddValue(NodeNameCharIdx, CharIdx);
			info.AddValue(NodeNameKind, Kind);
		}

		public GameTask AddTicks(int ticks) {
			return new GameTask(this.StartTime, this.MatureTime.AddTicks(ticks), this.CharIdx, this.Kind, this.TaskId, this.BonusFactor);
        }

		public string XmlNodeName { get {
			return GenXmlNodeName(Kind, TaskId);
		} }

		public static string GenXmlNodeName(TaskKind kind, int taskId) {
			if (kind == TaskKind.Invocation) {
				return NodePrefixInvocation;
			} else if (kind == TaskKind.Profession) {
				return NodePrefixProfession + taskId.ToString();
			} else {
				return "UNKNOWN";
			}
		}

		public void AppendXmlElement(XmlElement tasksNode) {
			var newTaskNode = (XmlElement)tasksNode.AppendChild(tasksNode.OwnerDocument.CreateElement(XmlNodeName));
			SetXmlAttribs(newTaskNode);
		}

		public void SetXmlAttribs(XmlElement taskNode) {
			taskNode.SetAttribute(NodeNameStartTime, this.StartTime.ToString("o"));
			taskNode.SetAttribute(NodeNameMatureTime, this.MatureTime.ToString("o"));
			taskNode.SetAttribute(NodeNameCharIdx, this.CharIdx.ToString());		
			taskNode.SetAttribute(NodeNameKind, this.Kind.ToString());			
			taskNode.SetAttribute(NodeNameTaskId, this.TaskId.ToString());
			taskNode.SetAttribute(NodeNameBonusFactor, this.BonusFactor.ToString());
		}

		public static GameTask FromXmlElement(XmlElement taskNode) {
			TaskKind kind;
			int taskId;

			if (taskNode.Name[0] == NodePrefixInvocation.ToCharArray()[0]) {
				kind = TaskKind.Invocation;
				taskId = int.Parse(taskNode.GetAttribute(NodeNameTaskId));
			} else if (taskNode.Name[0] == NodePrefixProfession.ToCharArray()[0]) {
				kind = TaskKind.Profession;
				taskId = int.Parse(taskNode.Name[1].ToString());
			} else {
				throw new Exception("GameTask::FromXmlElement: Invalid xml element.");
			}
			
			DateTime startTime = DateTime.Parse(taskNode.GetAttribute(NodeNameStartTime));
			DateTime matureTime = DateTime.Parse(taskNode.GetAttribute(NodeNameMatureTime));
			uint charIdx = uint.Parse(taskNode.GetAttribute(NodeNameCharIdx));
			TaskKind kindCheck = (TaskKind)Enum.Parse(typeof(TaskKind), taskNode.GetAttribute(NodeNameKind));
			int taskIdCheck = int.Parse(taskNode.GetAttribute(NodeNameTaskId));
			float bonusFactor = float.Parse(taskNode.GetAttribute(NodeNameBonusFactor));

			if (kind != kindCheck || taskId != taskIdCheck) {
				throw new Exception("GameTask::FromXmlElement: Error parsing xml element. Values do not check out.");
			}

			return new GameTask(startTime, matureTime, charIdx, kind, taskId, bonusFactor);
		}
	}	
}
