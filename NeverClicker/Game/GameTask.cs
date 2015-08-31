using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	[Serializable]
	public class GameTask : IComparable<GameTask>, ISerializable {
		public DateTime MatureTime { get; private set; }
		public GameTaskType Type { get; private set; }
		public uint CharacterZeroIdx { get; private set; }

		public string CharacterOneIdxLabel {
			get {
				return "Character " + (CharacterZeroIdx + 1).ToString();
			}
		}

		public GameTask(DateTime matureTime, uint characterIdx, GameTaskType type) {
			this.MatureTime = matureTime;
			this.CharacterZeroIdx = characterIdx;
			this.Type = type;
		}

		public int CompareTo(GameTask task) {
			return this.MatureTime.Ticks.CompareTo(task.MatureTime);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("MatureTime", MatureTime);
			info.AddValue("CharacterIdx", CharacterZeroIdx);
			info.AddValue("TaskKind", Type);
		}

		public void AddTicks(int ticks) {
			this.MatureTime = this.MatureTime.AddTicks(ticks);
        }
	}

	public enum GameTaskType {
		Invocation,
		Profession
	}
}
