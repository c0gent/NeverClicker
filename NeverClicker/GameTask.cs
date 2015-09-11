﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	[Serializable]
	public struct GameTask : IComparable<GameTask>, ISerializable {
		public DateTime MatureTime { get; private set; }
		public TaskKind Type { get; private set; }
		public uint CharIdx { get; private set; }
		public int TaskId {get; private set; }

		public string CharZeroIdxLabel { get { return "Character " + CharIdx.ToString(); } }

		public GameTask(DateTime matureTime, uint characterIdx, TaskKind type, int priority) {
			this.MatureTime = matureTime;
			this.CharIdx = characterIdx;
			this.Type = type;
			this.TaskId = priority;
		}

		public int CompareTo(GameTask task) {
			return this.MatureTime.Ticks.CompareTo(task.MatureTime);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("MatureTime", MatureTime);
			info.AddValue("CharacterIdx", CharIdx);
			info.AddValue("TaskKind", Type);
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
