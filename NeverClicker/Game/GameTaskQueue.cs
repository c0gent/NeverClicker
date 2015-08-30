using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	[Serializable]
	public class GameTaskQueue : ISerializable {

		private SortedList<long, GameTask> TaskList;

		public GameTask NextTask {
			get {
				return TaskList.First().Value;
			}

			private set { }
		}

		//public GameTaskQueue(SerializationInfo info, StreamingContext context) {
		//	this.Queue = (SortedList<int, GameTask>)info.GetValue("queue", SortedList<int, GameTask>);
		//}

		public GameTaskQueue() {
			TaskList = new SortedList<long, GameTask>(300);
		}

		public void Add(GameTask gameTask) {
			try {
				TaskList.Add(gameTask.MatureTime.Ticks, gameTask);
			} catch (ArgumentException) {
				gameTask.AddTicks(1);
				Add(gameTask);
            }
		}

		public bool IsEmpty() {
			return TaskList.Count == 0;
		}

		public GameTask Pop() {
			var nextTask = TaskList.First();
			TaskList.Remove(nextTask.Key);
			return nextTask.Value;
		}

		public TimeSpan NextTaskWaitTime() {
			//var nextTask = TaskList.First();
			//return new TimeSpan(nextTask.Value.MatureTime.CompareTo(DateTime.Now));
			return TaskList.First().Value.MatureTime - DateTime.Now;
        }

		//public uint NextTaskCharacterIdx() {
		//	var nextTask = TaskList.First();
		//	return nextTask.Value.CharacterIdx;
		//}

		//public GameTask NextTask() {
		//	return TaskList.First().Value;
		//}

		public void Populate(uint charZ, uint charN) {
			for (uint i = charZ; i < charN; i++) {
				Add(new GameTask(
					DateTime.Now.AddMilliseconds(i), i, TaskKind.Invocation
				));
			}
		}

		public void Populate(uint charCount) {
			Populate(0, charCount);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("queue", TaskList);
		}
	}
}
