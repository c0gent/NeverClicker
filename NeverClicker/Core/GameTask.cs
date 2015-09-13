using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	[Serializable]
	public class GameTask : IComparable<GameTask>, ISerializable {
		public DateTime MatureTime;
		public TaskKind Kind;
		public uint CharIdx;
		public int TaskId;

		public string CharZeroIdxLabel { get { return "Character " + CharIdx.ToString(); } }

		public GameTask(DateTime matureTime, uint characterIdx, TaskKind kind, int priority) {
			this.MatureTime = matureTime;
			this.CharIdx = characterIdx;
			this.Kind = kind;
			this.TaskId = priority;
		}

		public int CompareTo(GameTask task) {
			return this.MatureTime.Ticks.CompareTo(task.MatureTime);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("MatureTime", MatureTime);
			info.AddValue("CharacterIdx", CharIdx);
			info.AddValue("TaskKind", Kind);
		}

		public void AddTicks(int ticks) {
			this.MatureTime = this.MatureTime.AddTicks(ticks);
        }
	}

	public enum TaskKind {
		Invocation,
		Profession,
	}
}
