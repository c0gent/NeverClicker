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

		//public static int[] ProfessionTaskDurationMinutes = { 720, 600, 960, 1440 };
		public static int[] ProfessionTaskDurationMinutes = { 690, 575, 920, 1375 };
		public static string[] ProfessionTaskNames = { "Protect Magical", "Deliver Metals", "Destroy Enemy", "Battle Elemental" };

		private SortedList<long, GameTask> Queue { get; set; }		

		public GameTask NextTask { get { return Queue.First().Value; } }

		public bool IsEmpty { get { return Queue.Count == 0; } }


		public TaskQueue() {
			Queue = new SortedList<long, GameTask>(300);
		}


		public void Add(GameTask gameTask) {
			bool taskMatureTimeUnique = false;
			uint attempts = 0;


			while (taskMatureTimeUnique == false) {
				ArgumentException aex = new ArgumentException();

				if (this.Queue.ContainsKey(gameTask.MatureTime.Ticks)) {
					//MessageBox.Show("Key '" + gameTask.MatureTime.Ticks + "' already taken!!!!!");
					taskMatureTimeUnique = false;
				} else {
					//MessageBox.Show("Key '" + gameTask.MatureTime.Ticks + "' is unique.");
					taskMatureTimeUnique = true;

					try {
						Queue.Add(gameTask.MatureTime.Ticks, gameTask);
					} catch (ArgumentException aexNew) {
						MessageBox.Show("Failed to add task with key: '" + gameTask.MatureTime.Ticks + "'.");
						aex = aexNew;
					}
				}

				if (taskMatureTimeUnique == false) {
					gameTask.AddTicks(1);
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

		// FOR INVOCATION
		public void AdvanceTask(Interactor intr, uint charIdx, TaskKind taskKind, bool incrementTaskId) {
			if (taskKind == TaskKind.Profession) {
				throw new Exception("TaskQueue::AdvanceTask(): Profession tasks must specify a taskId as the fourth parameter");
			} else {
				this.AdvanceTask(intr, charIdx, taskKind, 999, incrementTaskId);
			}
		}

		// FOR PROFESSIONS
		public void AdvanceTask(Interactor intr, uint charIdx, TaskKind taskKind, int taskId) {	
			if (taskKind == TaskKind.Invocation) {
				throw new Exception("TaskQueue::AdvanceTask(): Invocation tasks must specify whether or not to increment daily invokes as fourth parameter.");
			} else {
				this.AdvanceTask(intr, charIdx, taskKind, taskId, false);
			}
		}

		public void AdvanceTask(Interactor intr, uint charIdx, TaskKind taskKind, int taskId, bool incrementTaskId) {
			// CHECK TO SEE IF THAT TASK IS ALREADY IN QUEUE, 
			// IF NOT ADD
			// IF SO, CHECK TO SEE IF THAT TASK HAS MATURED
			// IF IT HAS MATURED, ADVANCE
			var nowTicks = DateTime.Now.AddMinutes(3).Ticks;
			long taskKey = 0;
			bool keyExists = this.TryGetTaskKey(charIdx, taskKind, taskId, out taskKey);

			//DateTime matureTime = DateTime.Now;

			if (keyExists) {
				if (taskKey < nowTicks) { // MATURE
					var oldTask = Queue[taskKey];
					Queue.Remove(taskKey); 

					if (taskKind == TaskKind.Invocation) { 
						var invokesToday = (incrementTaskId) ? oldTask.TaskId + 1 : oldTask.TaskId;
						this.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
					} else if (taskKind == TaskKind.Profession) {
						this.QueueSubsequentProfessionTask(intr, charIdx, taskId);
					}					
				} else { // NOT MATURE
					// DO NOTHING?
				}
			} else { // DOESN'T EXIST YET
				if (taskKind == TaskKind.Invocation) {
					this.QueueSubsequentInvocationTask(intr, charIdx, 1);
				} else if (taskKind == TaskKind.Profession) {
					this.QueueSubsequentProfessionTask(intr, charIdx, taskId);
				}	
			}
		}

						
		// QUEUESUBSEQUENTINVOCATIONTASK(): QUEUE FOLLOW UP TASK
		public void QueueSubsequentInvocationTask(Interactor intr, uint charIdx, int invokesToday) {
			var now = DateTime.Now;
			DateTime charNextTaskTime = now;
			DateTime nextThreeThirty = NextThreeAmPst();
			DateTime todaysInvokeDate = TodaysGameDate();
			string charZeroIdxLabel = "Character " + charIdx.ToString();			
			
            if (invokesToday < 6) { // QUEUE FOR LATER TODAY
				//int extraTaskDelay = (invokesToday * 45000) + (charZeroIdx * 1000) + 180000;
				// EXTRA DELAY REMOVED
				var nextInvokeDelay = InvokeDelays[invokesToday];
				charNextTaskTime = now.AddMilliseconds(nextInvokeDelay);

				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (charNextTaskTime > nextThreeThirty) {
					invokesToday = 6;
					charNextTaskTime = nextThreeThirty;
				}
			} else { // QUEUE FOR TOMORROW
				try {
					intr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character " 
						+ charIdx + " on: " + todaysInvokeDate, LogEntryType.Debug);
					intr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompleteFor", charZeroIdxLabel);
					charNextTaskTime = nextThreeThirty;
				} catch (Exception ex) {
                    intr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
				}
			}

			try {				
				intr.Log("Next invocation task for character " + charIdx + " at: " + charNextTaskTime.ToShortTimeString() + ".");
				this.Add(new GameTask(charNextTaskTime.AddMilliseconds(charIdx), charIdx, TaskKind.Invocation, invokesToday));
				intr.UpdateQueueList(this.ListClone());
			} catch (Exception ex) {
                intr.Log("Error adding new task to queue: " +ex.ToString(), LogEntryType.Error);
			}
		}


		// QUEUESUBSEQUENTPROFESSIONTASK(): QUEUE FOLLOW UP TASK
		public void QueueSubsequentProfessionTask(Interactor intr, uint charIdx, int taskId) {
			var now = DateTime.Now;
			var charZeroIdxLabel = "Character " + charIdx.ToString();
			
			var mostRecentProfTime = now;
			DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentProfTime_" + taskId, "Character " + charIdx), out mostRecentProfTime);

			//int mostRecentProfTask = taskId;
			//int.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentProfTask_" + taskId, charZeroIdxLabel), out mostRecentProfTask);

			var nextTaskTime = now;

			if (mostRecentProfTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]) < now) {
				intr.GameAccount.SaveSetting(now.ToString(), "MostRecentProfTime_" + taskId, charZeroIdxLabel);
				nextTaskTime = now.AddMinutes(ProfessionTaskDurationMinutes[taskId]);				
			} else {
				nextTaskTime = mostRecentProfTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]);	
			}
				
			intr.Log("Next profession task (" + ProfessionTaskNames[taskId] + ") for character " + charIdx 
				+ " at: " + nextTaskTime.ToShortTimeString() + ".");

			intr.GameAccount.SaveSetting(now.ToString(), "MostRecentProfTime_" + taskId, charZeroIdxLabel);
			//prevTask = queue.Pop();				
			this.Add(new GameTask(nextTaskTime.AddSeconds(charIdx), charIdx, TaskKind.Profession, taskId));
			intr.UpdateQueueList(this.ListClone());
		}


		// POPULATE(): Populate queue taking in to account last invoke times
		public void Populate(Interactor intr, int charsMax) {
			var now = DateTime.Now;

			for (uint charIdx = 0; charIdx < charsMax; charIdx++) {
				var charSettingSection = "Character " + charIdx.ToString();

				// ################################### INVOCATION #####################################
				int invokesToday = 0;
				int.TryParse(intr.GameAccount.GetSettingOrEmpty("InvokesToday", charSettingSection), out invokesToday);

				var invokesCompletedOn = TodaysGameDate().AddDays(-1);
				DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("InvokesCompleteFor", charSettingSection), out invokesCompletedOn);

				var mostRecent = now.AddHours(-24);
				DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentInvocationTime", charSettingSection), out mostRecent);

				var mostRecentExp = mostRecent.AddMilliseconds(InvokeDelays[invokesToday]);
				var taskMatureTime = (mostRecentExp < now) ? now : mostRecent;
				taskMatureTime = taskMatureTime.AddMilliseconds(charIdx);

				if (invokesToday >= 6) {
					if (invokesCompletedOn < TodaysGameDate()) { // START FRESH DAY
						intr.GameAccount.SaveSetting("0", "InvokesToday", charSettingSection);
						invokesToday = 0;
						taskMatureTime = now;
					} else { // DONE FOR THE DAY
						taskMatureTime = NextThreeAmPst();
					}
				}

				intr.Log("Adding invocation task to queue for character " + (charIdx - 1).ToString() + ", matures: " + taskMatureTime.ToString(), LogEntryType.Info);
				this.Add(new GameTask(taskMatureTime.AddMilliseconds(charIdx), charIdx, TaskKind.Invocation, invokesToday));


				// ################################## PROFESSIONS #####################################

				// CLEAN OUT TASKS MORE THAN ONE DAY OLD				
				for (var p = 0; p < ProfessionTaskNames.Length; p++) {
					var settingKey = "MostRecentProfTime_" + p;
					var oneDayAgo = now.AddDays(-1);
					var TaskMatureTime = now.AddDays(-3);

					if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty(settingKey, charSettingSection), out taskMatureTime)) {
						if (taskMatureTime < oneDayAgo) {
							intr.GameAccount.RemoveSetting(settingKey, charSettingSection);
						}
					}
				}

				// QUEUE UP
				int tasksQueued = 0;

				for (var p = 0; p < ProfessionTaskNames.Length; p++) {
					var settingKey = "MostRecentProfTime_" + p;
					mostRecent = now.AddDays(-1);			

					if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty(settingKey, charSettingSection), out mostRecent)) {
						intr.Log("Adding profession task to queue for character " + charIdx 
							+ ", matures: " + mostRecent.ToString()
							+ ", taskId: " + p.ToString() + ".", 
							LogEntryType.Info);

						mostRecentExp = mostRecent.AddMinutes(ProfessionTaskDurationMinutes[p]);
						taskMatureTime = (mostRecentExp < now) ? now : mostRecent;
						taskMatureTime = taskMatureTime.AddMilliseconds(charIdx);

						this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Profession, p));
						tasksQueued += 1;
					}									
				}

				intr.Log("[" + tasksQueued.ToString() + "] profession tasks queued for character " + charIdx + ".", LogEntryType.Info);
			}
		}


		public bool TryGetTaskKey(uint charIdx, TaskKind taskKind, out long taskKey) {
			if (taskKind == TaskKind.Profession) {
				throw new Exception("TaskQueue::GetTaskKey(): Profession tasks must specify a taskId as the third parameter");
			} else {
				return this.TryGetTaskKey(charIdx, taskKind, 999, out taskKey);
			}
		}

		public bool TryGetTaskKey(uint charIdx, TaskKind taskKind, int taskId, out long taskKey) {
			List<long> taskKeys = new List<long>(10);

			if (taskKind == TaskKind.Invocation) {
				foreach (var kvp in this.Queue) {
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Type == taskKind) {
						taskKeys.Add(kvp.Key);
					}
				}
			} else if (taskKind == TaskKind.Profession) {
				foreach (var kvp in this.Queue) {
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Type == taskKind && kvp.Value.TaskId == taskId) {
						taskKeys.Add(kvp.Key);
					}
				}
			}

			if (taskKeys.Count > 1) {
				throw new Exception("TaskQueue::GetTaskKey(): Queue consistency error. Found " + taskKeys.Count 
					+ " keys for charIdx: " + charIdx 
					+ ", taskKind: " + taskKind.ToString()
					+ ", taskId: " + taskId + ".");
			} else if (taskKeys.Count < 1) {
				taskKey = 0;
				return false;
			} else {
				taskKey = taskKeys[0];
				return true;
			}			
		}

		public GameTask Pop() {
			var nextTask = Queue.First();
			Queue.Remove(nextTask.Key);
			return nextTask.Value;
		}

		public TimeSpan NextTaskWaitTime() {
			//var nextTask = TaskList.First();
			//return new TimeSpan(nextTask.Value.MatureTime.CompareTo(DateTime.Now));
			return Queue.First().Value.MatureTime - DateTime.Now;
		}


		public static DateTime TodaysGameDate() {
			return NextThreeAmPst().Date.AddDays(-1);
		}

		public static DateTime NextThreeAmPst() {
			var utcNow = DateTime.UtcNow;
			var todayThreeThirtyPst = utcNow.Date.AddHours(10).AddMinutes(5).ToLocalTime();
			return (utcNow.ToLocalTime() <= todayThreeThirtyPst ? todayThreeThirtyPst : todayThreeThirtyPst.AddDays(1));
		}


		public SortedList<long, GameTask> ListClone() {
			return new SortedList<long, GameTask>(this.Queue);
		}


		//public GameTaskQueue(SerializationInfo info, StreamingContext context) {
		//	this.Queue = (SortedList<int, GameTask>)info.GetValue("queue", SortedList<int, GameTask>);
		//}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("queue", Queue);
		}	

		public void WriteXML() {
			System.Xml.Serialization.XmlSerializer writer =
				new System.Xml.Serialization.XmlSerializer(typeof(TaskQueue));

			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//NeverClicker_SerializationTest.xml";
			System.IO.FileStream file = System.IO.File.Create(path);

			writer.Serialize(file, this);
			file.Close();
		}
	}
}



//// AdvanceTasks(): ADVANCE ANY EXPIRED TASKS FOR A GIVEN CHARACTER AND TASK TYPE
//		public void AdvanceTasks_Unimplemented(uint charIdx, TaskKind taskKind) {
//			var nowTicks = DateTime.Now.Ticks;
//			//var forRemoval = this.GetTaskKey(charIdx, taskKind);

//			//if (idx < nowTicks) {
//			//	var prevTask = this.TaskList[idx];
//			//	this.TaskList.Remove(idx);
//			//}

//			//foreach (long idx in forRemoval) {
//			//	// WE ONLY WANT TO DEAL WITH TASKS WHICH HAVE ALREADY MATURED
						

//			//	//figure out mature time based on task type and priority...

//			//	//this.Add(new GameTask(newMatureTime, prevTask.CharIdx, prevTask.Type, prevTask.Priority));
//			//}
//		}