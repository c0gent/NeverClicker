using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			bool taskMatureTimeUnique = false;
			uint attempts = 0;


			while (taskMatureTimeUnique == false) {
				ArgumentException aex = new ArgumentException();

				if (this.TaskList.ContainsKey(gameTask.MatureTime.Ticks)) {
					//MessageBox.Show("Key '" + gameTask.MatureTime.Ticks + "' already taken!!!!!");
					taskMatureTimeUnique = false;
				} else {
					//MessageBox.Show("Key '" + gameTask.MatureTime.Ticks + "' is unique.");
					taskMatureTimeUnique = true;

					try {
						TaskList.Add(gameTask.MatureTime.Ticks, gameTask);
					} catch (ArgumentException) {
						MessageBox.Show("Failed to add task with key: '" + gameTask.MatureTime.Ticks + "'.");
					}
				}

				if (taskMatureTimeUnique == false) {
					gameTask.AddTicks(1000);
				} else {
					return;
				}

				attempts += 1;

				if (attempts > 500) {
					//MessageBox.Show("GameTaskQueue::Add(): Error adding task: " + aex.ToString());
					//throw aex;
				}
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

		public void WriteXML() {
			System.Xml.Serialization.XmlSerializer writer =
				new System.Xml.Serialization.XmlSerializer(typeof(GameTaskQueue));

			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//NeverClicker_SerializationTest.xml";
			System.IO.FileStream file = System.IO.File.Create(path);

			writer.Serialize(file, this);
			file.Close();
		}
	}
}
