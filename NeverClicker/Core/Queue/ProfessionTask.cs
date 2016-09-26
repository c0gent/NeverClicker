using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NeverClicker {
	// All possible professions.
	public enum ProfessionKind {
		Leadership,
		Alchemy,
	}

	// A potentially queueable profession task.
	[Serializable]
	public struct ProfessionTask: IComparable<ProfessionTask>, ISerializable {
		public readonly string Name;
		public readonly int DurationMinutes;
		public readonly ProfessionKind Kind;

		public ProfessionTask(string name, int durationMinutes, ProfessionKind kind) {
			this.Name = name;
			this.DurationMinutes = durationMinutes;
			this.Kind = kind;
		}

		public int CompareTo(ProfessionTask task) {
			//return this.MatureTime.Ticks.CompareTo(task.MatureTime);
			return this.Name.CompareTo(task.Name);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("Name", Name);
			//info.AddValue("CharacterIdx", CharIdx);
			//info.AddValue("TaskKind", Kind);
		}
	}

	// [IN PROGRESS]:
	//public class ProfessionTasks {
	//	public const int MAX_PROFESSION_TASKS = 15;
	//
	//	public readonly List<ProfessionTask> List;
	//
	//	public ProfessionTasks() {
	//		List<ProfessionTask> taskList;
	//
	//		for (int pt = 0; pt < MAX_PROFESSION_TASKS; pt += 1) {
	//			string name;
	//			int duration;
	//			ProfessionKind kind;
	//		}
	//	}
	//}



	//[Serializable]
	public struct ProfessionTasksRef {
		public static int[] ProfessionTaskDurationMinutes = {
			//235,
			960,
			//690,
			720,
			//1375
			480
		};

		public static string[] ProfessionTaskNames = {
			//"Guard Young Noble",
			"Escort a Wizard",
			//"Kill a Young Dragon",
			"Protect Magical",
			//"Battle Elemental"
			"Guard Clerics"
		};
	}
}
