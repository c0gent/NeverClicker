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
		public static int[] InvokeDelayMinutes = { 0, 15, 30, 45, 60, 90, 0, 0, 0, 0 };

		//public static int[] ProfessionTaskDurationMinutes = { 720, 600, 960, 1440 };
		public static int[] ProfessionTaskDurationMinutes = { 920, 690, 575, 920, 1375, 690 };
		public static string[] ProfessionTaskNames = { "Escort a Wizard", "Kill a Young Dragon", "Deliver Metals", "Destroy Enemy", "Battle Elemental", "Protect Magical" };

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
			intr.Log("Advancing task: "
				+ "[charIdx: " + charIdx 
				+ ", taskKind: " + taskKind.ToString()
				+ ", taskId: " + taskId + "]."				
				, LogEntryType.Info);

			var nowTicks = DateTime.Now.AddSeconds(1).Ticks;
			long taskKey = 0;
			bool keyExists = this.TryGetTaskKey(charIdx, taskKind, taskId, out taskKey);

			//DateTime matureTime = DateTime.Now;

			if (keyExists) {
				intr.Log("Key found for task.", LogEntryType.Debug);
				if (taskKey < nowTicks) { // MATURE
					intr.Log("Task is mature.", LogEntryType.Debug);
					var oldTask = Queue[taskKey];
					intr.Log("Removing old task.", LogEntryType.Debug);
					Queue.Remove(taskKey); 

					if (taskKind == TaskKind.Invocation) {
						intr.Log("Queuing subsequent invocation task.", LogEntryType.Debug);
						var invokesToday = (incrementTaskId) ? oldTask.TaskId + 1 : oldTask.TaskId;
						this.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
					} else if (taskKind == TaskKind.Profession) {
						intr.Log("Queuing subsequent professions task.", LogEntryType.Debug);
						this.QueueSubsequentProfessionTask(intr, charIdx, taskId);
					}					
				} else { // NOT MATURE
					intr.Log("Task is not mature.", LogEntryType.Debug);
					// DO NOTHING?
				}
			} else { // DOESN'T EXIST YET
				intr.Log("Key not found for task.", LogEntryType.Info);
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
			DateTime taskMatureTime = now;
			DateTime nextThreeThirty = NextThreeAmPst();
			DateTime todaysInvokeDate = TodaysGameDate();
			string charLabel = "Character " + charIdx.ToString();			
			
            if (invokesToday < 6) { // QUEUE FOR LATER
				//int extraTaskDelay = (invokesToday * 45000) + (charZeroIdx * 1000) + 180000;
				// EXTRA DELAY REMOVED
				//var nextInvokeDelay = InvokeDelays[invokesToday];
				taskMatureTime = CalculateTaskMatureTime(now, charIdx, TaskKind.Invocation, invokesToday);

				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (taskMatureTime > nextThreeThirty) {
					invokesToday = 6;
					taskMatureTime = nextThreeThirty;
				}
			} else { // QUEUE FOR TOMORROW
				try {
					intr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character " 
						+ charIdx + " on: " + todaysInvokeDate, LogEntryType.Debug);
					intr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompleteFor", charLabel);
					taskMatureTime = nextThreeThirty;
					invokesToday = 6;
				} catch (Exception ex) {
                    intr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
				}
			}

			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(intr, DateTime.Now);
				intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				intr.GameAccount.SaveSetting(now.ToString(), "MostRecentInvocationTime", charLabel);
				intr.GameAccount.SaveSetting(charIdx.ToString(), "CharLastInvoked", "Invocation");
				intr.Log("Settings saved to ini for: " + charLabel + ".", LogEntryType.Debug);
			} catch (Exception ex) {
				intr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString(), LogEntryType.Error);
			}
						
			intr.Log("Next invocation task for character " + charIdx + " at: " + taskMatureTime.ToShortTimeString() + ".");
			this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Invocation, invokesToday));
			intr.UpdateQueueList(this.ListClone());
		}


		// QUEUESUBSEQUENTPROFESSIONTASK(): QUEUE FOLLOW UP TASK
		public void QueueSubsequentProfessionTask(Interactor intr, uint charIdx, int taskId) {
			var now = DateTime.Now;
			var charLabel = "Character " + charIdx.ToString();
			
			var mostRecentProfTime = now;
			DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentProfTime_" + taskId, charLabel), out mostRecentProfTime);

			//int mostRecentProfTask = taskId;
			//int.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentProfTask_" + taskId, charZeroIdxLabel), out mostRecentProfTask);

			var taskMatureTime = now;

			if (mostRecentProfTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]) < now) {
				//intr.GameAccount.SaveSetting(now.ToString(), "MostRecentProfTime_" + taskId, charLabel);				
				//taskMatureTime = now.AddMinutes(ProfessionTaskDurationMinutes[taskId]);
				taskMatureTime = CalculateTaskMatureTime(now, charIdx, TaskKind.Profession, taskId);		
			} else {
				//taskMatureTime = mostRecentProfTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]);	
				taskMatureTime = CalculateTaskMatureTime(mostRecentProfTime, charIdx, TaskKind.Profession, taskId);
			}
				
			intr.Log("Next profession task (" + ProfessionTaskNames[taskId] + ") for character " + charIdx 
				+ " at: " + taskMatureTime.ToShortTimeString() + ".");

			intr.GameAccount.SaveSetting(now.ToString(), "MostRecentProfTime_" + taskId, charLabel);
			this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Profession, taskId));
			intr.UpdateQueueList(this.ListClone());
		}


		// AdvanceTasks(): ADVANCE ANY EXPIRED TASKS FOR A GIVEN CHARACTER AND TASK TYPE
		public void AdvanceMatured(Interactor intr, uint charIdx) {
			var nowTicks = DateTime.Now.AddSeconds(1).Ticks;
			List<long> maturedTaskKeys = new List<long>(10);

			foreach (var kvp in this.Queue) {
				if (kvp.Value.CharIdx == charIdx && kvp.Key < nowTicks) {
					maturedTaskKeys.Add(kvp.Key);
				}
			}

			foreach (long idx in maturedTaskKeys) {
				var prevTask = this.Queue[idx];
				this.Queue.Remove(idx);

				if (prevTask.Kind == TaskKind.Invocation) {
					this.QueueSubsequentInvocationTask(intr, charIdx, prevTask.TaskId + 1);
				} else if (prevTask.Kind == TaskKind.Profession) {
					this.QueueSubsequentProfessionTask(intr, charIdx, prevTask.TaskId);
				}
			}
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

				var taskMatureTime = CalculateTaskMatureTime(mostRecent, charIdx, TaskKind.Invocation, invokesToday);
				taskMatureTime = (taskMatureTime < now) ? now : taskMatureTime;

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
				this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Invocation, invokesToday));


				// ################################## PROFESSIONS #####################################
				for (var p = 0; p < ProfessionTaskNames.Length; p++) {
					var settingKey = "MostRecentProfTime_" + p;
					var oldTaskThreshold = now.AddDays(-2);
					var TaskMatureTime = now;

					if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty(settingKey, charSettingSection), out taskMatureTime)) {
						intr.Log("Found " + settingKey + " for " + charSettingSection + " in ini file: " + taskMatureTime.ToString() + ".", LogEntryType.Info);
					}

					if (taskMatureTime < oldTaskThreshold) {
						intr.Log("Removing " + settingKey + " for " + charSettingSection + " from ini file.", LogEntryType.Info);
						intr.GameAccount.RemoveSetting(settingKey, charSettingSection);
					}
				}

				int tasksQueued = 0;

				for (var taskId = 0; taskId < ProfessionTaskNames.Length; taskId++) {
					var settingKey = "MostRecentProfTime_" + taskId;
					mostRecent = now.AddDays(-1);			

					if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty(settingKey, charSettingSection), out mostRecent)) {
						intr.Log("Adding profession task to queue for character " + charIdx 
							+ ", matures: " + mostRecent.ToString()	+ ", taskId: " + taskId.ToString() + ".", LogEntryType.Info);

						taskMatureTime = CalculateTaskMatureTime(mostRecent, charIdx, TaskKind.Profession, taskId);
						taskMatureTime = (taskMatureTime < now) ? now : taskMatureTime;

                        this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Profession, taskId));
						tasksQueued += 1;
					}						
				}

				intr.Log("[" + tasksQueued.ToString() + "] profession tasks queued for character " + charIdx + ".", LogEntryType.Info);
			}
		}


		public DateTime CalculateTaskMatureTime(DateTime startTime, uint charIdx, TaskKind kind, int taskId) {
			DateTime taskMatureTime;

			switch (kind) {
				case TaskKind.Invocation:
					taskMatureTime = startTime.AddMinutes(InvokeDelayMinutes[taskId]);
					break;
				case TaskKind.Profession:
					taskMatureTime = startTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]);
					break;
				default:
					return DateTime.Now;
			}

			taskMatureTime = taskMatureTime.AddTicks(10000 * charIdx).AddTicks(100 * taskId);
			return taskMatureTime;
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
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Kind == taskKind) {
						taskKeys.Add(kvp.Key);
					}
				}
			} else if (taskKind == TaskKind.Profession) {
				foreach (var kvp in this.Queue) {
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Kind == taskKind && kvp.Value.TaskId == taskId) {
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

		public TimeSpan NextTaskWaitDelay() {
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



