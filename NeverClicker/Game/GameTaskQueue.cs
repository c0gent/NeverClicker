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
				gameTask.MatureTime = gameTask.MatureTime.AddTicks(1);
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

		public int NextTaskCharacterIdx() {
            var nextTask = TaskList.First();
			return nextTask.Value.CharacterIdx;
		}

		public void Populate(int charZ, int charN) {
			for (int i = charZ; i < charN; i++) {
				Add(new GameTask(
					DateTime.Now.AddMilliseconds(i), i, TaskKind.Invocation
				));
			}

		}

		public void Populate(int charCount) {
			Populate(0, charCount);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("queue", TaskList);
		}
	}
}
