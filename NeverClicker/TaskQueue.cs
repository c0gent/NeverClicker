using NeverClicker.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeverClicker {
	[Serializable]
	public class TaskQueue : ISerializable {
		//									     15min,   30min,   45min,   60min,   90min,
		public static int[] InvokeDelays = { 0, 900000, 1800000, 2700000, 3600000, 5400000, 0, 0 };
		public SortedList<long, GameTask> TaskList { get; private set; }		

		public GameTask NextTask {
			get {
				return TaskList.First().Value;
			}

			private set { }
		}

		//public GameTaskQueue(SerializationInfo info, StreamingContext context) {
		//	this.Queue = (SortedList<int, GameTask>)info.GetValue("queue", SortedList<int, GameTask>);
		//}

		public TaskQueue() {
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
					} catch (ArgumentException aex_2) {
						MessageBox.Show("Failed to add task with key: '" + gameTask.MatureTime.Ticks + "'.");
						aex = aex_2;
					}
				}

				if (taskMatureTimeUnique == false) {
					gameTask.AddTicks(1000);
				} else {
					return;
				}

				attempts += 1;

				if (attempts > 500) {
					MessageBox.Show("GameTaskQueue::Add(): Error adding task: " + aex.ToString());
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

		//public void Populate(uint charZ, uint charN) {
		//	for (uint i = charZ; i < charN; i++) {
		//		Add(new GameTask(
		//			DateTime.Now.AddMilliseconds(i), i, GameTaskType.Invocation
		//		));
		//	}
		//}

		//public void Populate(uint charCount) {
		//	Populate(0, charCount);
		//}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("queue", TaskList);
		}

		public void WriteXML() {
			System.Xml.Serialization.XmlSerializer writer =
				new System.Xml.Serialization.XmlSerializer(typeof(TaskQueue));

			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//NeverClicker_SerializationTest.xml";
			System.IO.FileStream file = System.IO.File.Create(path);

			writer.Serialize(file, this);
			file.Close();
		}

		// QueueSubsequentTask(): QUEUE FOLLOW UP TASK
		public void QueueSubsequentInvocationTask(Interactor intr, uint charZeroIdx, GameTaskType taskType, int invokesToday) {
			DateTime charNextTaskTime = DateTime.Now;
			DateTime nextThreeThirty = NextThreeAmPst();
			DateTime todaysInvokeDate = TodaysGameDate();
			string charZeroIdxLabel = "Character " + charZeroIdx.ToString();

			int extraTaskDelay = (invokesToday * 45000) + 180000;
			
			if (invokesToday < 6) { // QUEUE FOR LATER TODAY
				// nextInvokeDelay: (Normal delay) + (3 min) + (1 sec * charIdx);
				var nextInvokeDelay = InvokeDelays[invokesToday] + (charZeroIdx * 1000) + extraTaskDelay;
				charNextTaskTime = DateTime.Now.AddMilliseconds(nextInvokeDelay);

				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (charNextTaskTime > nextThreeThirty) {
					invokesToday = 6;
					charNextTaskTime = nextThreeThirty;
				}
			} else { // QUEUE FOR TOMORROW (NEXT 3:30AM)
				try {
					intr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character " 
						+ charZeroIdx + " on: " + todaysInvokeDate, LogEntryType.Debug);
					intr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompleteFor", charZeroIdxLabel);
					charNextTaskTime = nextThreeThirty;
				} catch (Exception ex) {
					//System.Windows.Forms.MessageBox.Show("Error saving InvokesCompleteFor" + ex.ToString());
                    intr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
					//System.Windows.Forms.MessageBox()
				}
			}

			try {
				intr.Log("Next invocation task for character " + charZeroIdx + " at: " + charNextTaskTime.ToShortTimeString() + ".");
				this.Add(new GameTask(charNextTaskTime.AddSeconds(charZeroIdx), charZeroIdx, GameTaskType.Invocation, 0));
				intr.UpdateQueueList(this.TaskList);
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("Error adding new task to queue: " + ex.ToString());
                intr.Log("Error adding new task to queue: " +ex.ToString(), LogEntryType.Error);
			}
		}


		// PopulateQueueProperly(): Populate queue taking in to account last invoke times
		public void PopulateQueueProperly(Interactor intr, int charsMax) {
			for (uint charIdx = 0; charIdx < charsMax; charIdx++) {
				var charLabel = "Character " + charIdx.ToString();

				int invokesToday = 0;
				int.TryParse(intr.GameAccount.GetSetting("InvokesToday", charLabel), out invokesToday);

				DateTime invokesCompletedOn = TodaysGameDate().AddDays(-1);
				DateTime.TryParse(intr.GameAccount.GetSetting("InvokesCompleteFor", charLabel), out invokesCompletedOn);

				DateTime mostRecentInvTime = DateTime.Now.AddHours(-24);
				DateTime.TryParse(intr.GameAccount.GetSetting("MostRecentInvocationTime", charLabel), out mostRecentInvTime);

				DateTime taskMatureTime = mostRecentInvTime + new TimeSpan(0, 0, 0, 0, InvokeDelays[invokesToday]);

				if (invokesToday >= 6 || invokesCompletedOn == TodaysGameDate()) {
					taskMatureTime = NextThreeAmPst();
				}

				intr.Log("Adding invocation task to queue for character " + (charIdx - 1).ToString() + ", matures: " + taskMatureTime.ToString(), LogEntryType.Debug);
				this.Add(new GameTask(taskMatureTime, charIdx, GameTaskType.Invocation, 0));
				
				for (var p = 0; p < 3; p++) {
					DateTime mostRecent = DateTime.Now.AddDays(-1);
					DateTime.TryParse(intr.GameAccount.GetSetting("MostRecentProfTime_" + p, charLabel), out mostRecent);
					intr.Log("Adding profession task to queue for character " + charIdx + ", matures: " + mostRecent.ToString(), LogEntryType.Debug);
					this.Add(new GameTask(mostRecent.AddMinutes(Sequences.ProfessionTaskDurationMinutes[p]), charIdx, GameTaskType.Profession, p));					
				}
			}

		}


		public static DateTime TodaysGameDate() {
			return NextThreeAmPst().Date.AddDays(-1);
		}

		public static DateTime NextThreeAmPst() {
			var utcNow = DateTime.UtcNow;
			var todayThreeThirtyPst = utcNow.Date.AddHours(10).AddMinutes(5).ToLocalTime();
			return (utcNow.ToLocalTime() <= todayThreeThirtyPst ? todayThreeThirtyPst : todayThreeThirtyPst.AddDays(1));
		}
	}
}
