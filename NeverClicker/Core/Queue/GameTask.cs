using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	public enum TaskKind {
		Invocation,
		Profession,
	}

	[Serializable]
	public struct GameTask : IComparable<GameTask>, ISerializable {
		public readonly DateTime MatureTime; /*{ get; private set; }*/
		public readonly TaskKind Kind; /*{ get; private set; }*/
		public readonly uint CharIdx; /*{ get; private set; }*/
		public readonly int TaskId; /*{ get; private set; }*/

		public string CharIdxLabel { get { return "Character_" + CharIdx.ToString(); } }

		public GameTask(DateTime matureTime, uint charIdx, TaskKind kind, int taskId) {
			this.MatureTime = matureTime;
			this.CharIdx = charIdx;
			this.Kind = kind;
			this.TaskId = taskId;
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
			return new GameTask(this.MatureTime.AddTicks(ticks), this.CharIdx, this.Kind, this.TaskId);
        }
	}	
}
