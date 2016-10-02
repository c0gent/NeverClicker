using NeverClicker.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Immutable;

namespace NeverClicker {

	[Serializable]
	public class TaskQueue : ISerializable {
		// Invocation Delays:
		public static int[] InvokeDelayMinutes = { 0, 15, 30, 45, 60, 90, 0, 0, 0, 0 };
		//// [BRING ME BACK]:
		//private readonly ProfessionTasks ProfessionTaskList;
		//private SortedList<long, GameTask> Queue { get; set; }
		private SortedDictionary<long, GameTask> Queue { get; set; }
		public GameTask NextTask { get { return Queue.First().Value; } }
		public bool IsEmpty { get { return Queue.Count == 0; } }

		public TaskQueue() {
			//Queue = new SortedList<long, GameTask>(300);
			Queue = new SortedDictionary<long, GameTask>();
			//// [BRING ME BACK]:
			//ProfessionTaskList = new ProfessionTasks();
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
					gameTask = gameTask.AddTicks(1);
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

		private void AdvanceTask(Interactor intr, uint charIdx, TaskKind taskKind, int taskId, bool incrementTaskId) {
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
				intr.Log("Key found for task: " + 
					"id: " + taskId.ToString() + ", " + 
					"kind: " + taskKind.ToString() + ", " +
					"char: " + charIdx.ToString() + ". ",
					LogEntryType.Debug);

				if (taskKey < nowTicks) { // MATURE
					intr.Log("Task is mature.", LogEntryType.Debug);
					//var oldTask = Queue[taskKey];
					intr.Log("Removing old task.", LogEntryType.Debug);
					Queue.Remove(taskKey); 

					if (taskKind == TaskKind.Invocation) {
						intr.Log("Queuing subsequent invocation task.", LogEntryType.Debug);
						var invokesToday = (incrementTaskId) ? taskId + 1 : taskId;
						this.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
					} else if (taskKind == TaskKind.Profession) {
						intr.Log("Queuing subsequent professions task.", LogEntryType.Debug);
						this.QueueSubsequentProfessionTask(intr, charIdx, taskId);
					}					
				} else { // NOT MATURE
					intr.Log("Task is not mature: taskKey: " + taskKey + 
						", nowTicks: " + nowTicks + ".", LogEntryType.Debug);
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

		// FOR INVOCATION
		public void AdvanceInvocationTask(Interactor intr, uint charIdx, int invokesToday, bool incrementInvokes) {
			//if (taskKind == TaskKind.Profession) {
			//	throw new Exception("TaskQueue::AdvanceTask(): Profession tasks must specify a taskId as the fourth parameter");
			//} else {
			//	this.AdvanceTask(intr, charIdx, taskKind, invokesToday, incrementInvokes);
			//}

			this.AdvanceTask(intr, charIdx, TaskKind.Invocation, invokesToday, incrementInvokes);
		}

		// FOR PROFESSIONS
		public void AdvanceProfessionsTask(Interactor intr, uint charIdx, int taskId) {
			//if (taskKind == TaskKind.Invocation) {
			//	throw new Exception("TaskQueue::AdvanceTask(): Invocation tasks must specify 
			// whether or not to increment daily invokes as fourth parameter.");
			//} else {
			//	this.AdvanceTask(intr, charIdx, taskKind, taskId, false);
			//}

			this.AdvanceTask(intr, charIdx, TaskKind.Profession, taskId, false);
		}


		// QUEUESUBSEQUENTINVOCATIONTASK(): QUEUE FOLLOW UP TASK
		//
		// If `invokesToday` is 0 it can be assumed that things are out of sync. 
		//     - `invokesToday` defaults to 1 initially (see `AdvanceTask()` near the bottom). 
		//     - There are no other known reasons why it would be 0, therefore an extra 15 minutes of delay is added.
		private void QueueSubsequentInvocationTask(Interactor intr, uint charIdx, int invokesToday) {
			var now = DateTime.Now;
			DateTime taskMatureTime = now;
			DateTime nextThreeThirty = NextThreeAmPst;
			DateTime todaysInvokeDate = TodaysGameDate;
			string charLabel = "Character_" + charIdx.ToString();			
			
            if (invokesToday < 6) { // QUEUE FOR LATER
				TimeSpan extraDelay = TimeSpan.FromMinutes(0);
				if (invokesToday == 0) {
					extraDelay = TimeSpan.FromMinutes(15);
				}
				taskMatureTime = CalculateTaskMatureTime(now + extraDelay, charIdx, TaskKind.Invocation, invokesToday);
				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (taskMatureTime > nextThreeThirty) {
					invokesToday = 6;
					taskMatureTime = nextThreeThirty;
				}
			} else { // (INVOKES >= 6) QUEUE FOR TOMORROW
				try {
					intr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character " 
						+ charIdx + " on: " + todaysInvokeDate, LogEntryType.Debug);
					intr.AccountStates.SaveCharState(todaysInvokeDate.ToString(), charIdx, "InvokesCompleteFor");
					intr.AccountStates.SaveCharState(invokesToday, charIdx, "InvokesToday");
					taskMatureTime = nextThreeThirty;
					//invokesToday = 6;
				} catch (Exception ex) {
                    intr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
				}
			}

			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(intr, DateTime.Now);
				//intr.AccountSettings.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				intr.AccountStates.SaveCharState(invokesToday, charIdx, "InvokesToday");
				intr.AccountStates.SaveCharState(now, charIdx, "MostRecentInvocationTime");
				//intr.GameAccount.SaveSetting(charIdx.ToString(), "CharLastInvoked", "Invocation");
				intr.Log("Settings saved to ini for: " + charLabel + ".", LogEntryType.Debug);
			} catch (Exception ex) {
				intr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString(), LogEntryType.Error);
			}
						
			intr.Log("Next invocation task for character " + charIdx + " at: " + taskMatureTime.ToString() + ".");
			this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Invocation, invokesToday));
			intr.UpdateQueueList(this.ListClone());
		}


		// QUEUESUBSEQUENTPROFESSIONTASK(): QUEUE FOLLOW UP TASK
		private void QueueSubsequentProfessionTask(Interactor intr, uint charIdx, int taskId) {
			var now = DateTime.Now;
			//var charLabel = "Character_" + charIdx.ToString();

			var mostRecentProfTime = intr.AccountStates.GetCharStateOr(charIdx, 
				"MostRecentProfTime_" + taskId, Global.Default.SomeOldDate);

			//int mostRecentProfTask = taskId;
			//int.TryParse(intr.GameAccount.GetSettingOrEmpty("MostRecentProfTask_" + taskId, charZeroIdxLabel), out mostRecentProfTask);

			var taskMatureTime = now;

			if (mostRecentProfTime.AddMinutes(ProfessionTasksRef.ProfessionTaskDurationMinutes[taskId]) < now) {
				//intr.GameAccount.SaveSetting(now.ToString(), "MostRecentProfTime_" + taskId, charLabel);				
				//taskMatureTime = now.AddMinutes(ProfessionTaskDurationMinutes[taskId]);
				taskMatureTime = CalculateTaskMatureTime(now, charIdx, TaskKind.Profession, taskId);
			} else {
				//taskMatureTime = mostRecentProfTime.AddMinutes(ProfessionTaskDurationMinutes[taskId]);	
				taskMatureTime = CalculateTaskMatureTime(mostRecentProfTime, charIdx, TaskKind.Profession, taskId);
			}

			intr.Log("Next profession task (" + ProfessionTasksRef.ProfessionTaskNames[taskId] + ") for character " + charIdx
				+ " at: " + taskMatureTime.ToString() + ".");

			intr.AccountStates.SaveCharState(now, charIdx, "MostRecentProfTime_" + taskId);
			this.Add(new GameTask(taskMatureTime, charIdx, TaskKind.Profession, taskId));
			intr.UpdateQueueList(this.ListClone());
		}


		// AdvanceTasks(): ADVANCE ANY EXPIRED TASKS FOR A GIVEN CHARACTER AND TASK TYPE
		public void AdvanceMatured(Interactor intr, uint charIdx) {
			var nowTicks = DateTime.Now.AddMinutes(-1).Ticks;
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
					//this.QueueSubsequentProfessionTask(intr, charIdx, prevTask.TaskId);
				}
			}
		}

		public void PostponeUntilNextInvoke(Interactor intr, uint charIdx) {
			var invKey = GetTaskKeys(charIdx, TaskKind.Invocation);
			var profKeys = GetTaskKeys(charIdx, TaskKind.Profession);
			
			if (invKey.Count == 1) {
				foreach (var pk in profKeys) {
					if (pk < invKey[0]) {
						var oldProfTask = Queue[pk];
						Queue.Remove(pk);
						var newMatureTime = pk + 1 + oldProfTask.TaskId;
						Queue.Add(newMatureTime, new GameTask(new DateTime(newMatureTime), 
							charIdx, TaskKind.Profession, oldProfTask.TaskId));
					}
				}
			} else {
				intr.Log("TaskQueue::DelayUntilNextInvoke(): Error retrieving next invocation " + 
					"task item for character " + charIdx + ".", LogEntryType.Fatal);
			}
		}


		// POPULATE(): Populate queue taking in to account last invoke times
		public void Populate(Interactor intr, int charsMax, bool resetDay) {
			var now = DateTime.Now;

			for (uint charIdx = 0; charIdx < charsMax; charIdx++) {
				//var charSettingSection = "Character_" + charIdx.ToString();

				// ################################### INVOCATION #####################################
				//int invokesToday = 0;
				//int.TryParse(intr.AccountSettings.GetSettingValOr("InvokesToday", 
				//	charSettingSection, ""), out invokesToday);

				int invokesToday = intr.AccountStates.GetCharStateOr(charIdx, "InvokesCompleteFor", 0);

				//var invokesCompletedOn = TodaysGameDate.AddDays(-1);
				//DateTime.TryParse(intr.AccountStates.GetCharState(charIdx, "InvokesCompleteFor"), out invokesCompletedOn);

				DateTime invokesCompletedOn = intr.AccountStates.GetCharStateOr(charIdx, 
					"InvokesCompleteFor", Global.Default.SomeOldDate);

				// Clear any stale invoke count:
				if (invokesCompletedOn < TodaysGameDate.AddDays(-1)) {
					invokesToday = 0;
				}

				//var mostRecent = now.AddHours(-24);
				//DateTime.TryParse(intr.AccountSettings.GetSettingValOr("MostRecentInvocationTime", 
				//	charSettingSection, ""), out mostRecent);

				DateTime mostRecentInvoke = intr.AccountStates.GetCharStateOr(charIdx, "MostRecentInvocationTime", 
					now.AddHours(-24));

				var invTaskMatureTime = CalculateTaskMatureTime(mostRecentInvoke, charIdx, 
					TaskKind.Invocation, invokesToday);
				invTaskMatureTime = (invTaskMatureTime < now) ? now : invTaskMatureTime;

				if (invokesToday >= 6) {
					if (invokesCompletedOn < TodaysGameDate) { // START FRESH DAY
						//intr.AccountSettings.SaveSetting("0", "InvokesToday", charSettingSection);
						intr.AccountStates.SaveCharState(0, charIdx, "InvokesToday");
						invokesToday = 0;
						invTaskMatureTime = now;
					} else { // DONE FOR THE DAY
						invTaskMatureTime = NextThreeAmPst;
					}
				}

				if (resetDay) {
					//intr.AccountStates.SaveSetting("0", "InvokesToday", charSettingSection);
					intr.AccountStates.SaveCharState(0, charIdx, "InvokesToday");
					intr.AccountStates.SaveCharState(TodaysGameDate.AddHours(-2).ToString(), charIdx,
						"MostRecentInvocationTime");
					invokesToday = 0;
					invTaskMatureTime = now;
				}

				intr.Log("Adding invocation task to queue for character " + (charIdx - 1).ToString() + ", matures: " + 
					invTaskMatureTime.ToString(), LogEntryType.Debug);
				this.Add(new GameTask(invTaskMatureTime, charIdx, TaskKind.Invocation, invokesToday));


				// ################################## PROFESSIONS #####################################
				// ################ Prune Stale Profession Tasks ################
				//for (var p = 0; p < ProfessionTasksRef.ProfessionTaskNames.Length; p++) {
				//	var settingKey = "MostRecentProfTime_" + p;
				//	var oldTaskThreshold = now.AddDays(-2);

				//	//DateTime profTaskMatureTime;

				//	//if (DateTime.TryParse(intr.AccountStates.GetCharStateOr(charIdx,
				//	//			charSettingSection, ""), out profTaskMatureTime)) {
				//	//	intr.Log("Found " + settingKey + " for " + charSettingSection + " in ini file: " +
				//	//		profTaskMatureTime.ToString() + ".", LogEntryType.Debug);

				//	//	//// [TODO]: Is this necessary?:
				//	//	//if (profTaskMatureTime < oldTaskThreshold) {
				//	//	//	intr.Log("Removing " + settingKey + " for " + charSettingSection + " from ini file.", LogEntryType.Debug);
				//	//	//	intr.GameAccount.RemoveSetting(settingKey, charSettingSection);
				//	//	//}
				//	//}
				//	DateTime profTaskMatureTime = intr.AccountStates.GetCharStateOr(charIdx,
				//		settingKey, Global.Default.SomeOldDate);
				//}

				// ################ Add Tasks to Queue ################
				int tasksQueued = 0;

				for (var taskId = 0; taskId < ProfessionTasksRef.ProfessionTaskNames.Length; taskId++) {
					var settingKey = "MostRecentProfTime_" + taskId;
					//var mostRecentTask = now.AddDays(-1);
					DateTime profTaskMatureTime;

					var mostRecentTask = intr.AccountStates.GetCharStateOr(charIdx, settingKey, Global.Default.SomeOldDate);

					intr.Log("Adding profession task to queue for character " + charIdx
						+ ", matures: " + mostRecentTask.ToString() + ", taskId: " + taskId.ToString() + ".", LogEntryType.Info);

					profTaskMatureTime = CalculateTaskMatureTime(mostRecentTask, charIdx, TaskKind.Profession, taskId);
					profTaskMatureTime = (profTaskMatureTime < now) ? now : profTaskMatureTime;

					this.Add(new GameTask(profTaskMatureTime, charIdx, TaskKind.Profession, taskId));

					tasksQueued += 1;
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
					taskMatureTime = startTime.AddMinutes(ProfessionTasksRef.ProfessionTaskDurationMinutes[taskId]);
					break;
				default:
					return DateTime.Now;
			}

			taskMatureTime = taskMatureTime.AddTicks(10000 * charIdx).AddTicks(10 * taskId);
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
			} else if (taskKeys.Count == 1) {
				taskKey = taskKeys[0];
				return true;
			} else {
				taskKey = 0;
				return false;
			}			
		}

		public List<long> GetTaskKeys(uint charIdx, TaskKind taskKind) {
			List<long> taskKeys = new List<long>(10);

			if (taskKind == TaskKind.Invocation) {
				foreach (var kvp in this.Queue) {
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Kind == taskKind) {
						taskKeys.Add(kvp.Key);
					}
				}
			} else if (taskKind == TaskKind.Profession) {
				foreach (var kvp in this.Queue) {
					if (kvp.Value.CharIdx == charIdx && kvp.Value.Kind == taskKind) {
						taskKeys.Add(kvp.Key);
					}
				}
			}
			
			return taskKeys;		
		}

		private GameTask Pop() {
			var nextTask = Queue.First();
			Queue.Remove(nextTask.Key);
			return nextTask.Value;
		}

		public TimeSpan NextTaskMatureDelay() {
			//var nextTask = TaskList.First();
			//return new TimeSpan(nextTask.Value.MatureTime.CompareTo(DateTime.Now));
			return Queue.First().Value.MatureTime - DateTime.Now;
		}


		public static DateTime TodaysGameDate {
			get { return NextThreeAmPst.Date.AddDays(-1); }
		}

		public static DateTime NextThreeAmPst {
			get {
				var utcNow = DateTime.UtcNow;
				var todayThreeAmPst = utcNow.Date.AddHours(10).AddMinutes(5).ToLocalTime();
				return (utcNow.ToLocalTime() <= todayThreeAmPst ? todayThreeAmPst : todayThreeAmPst.AddDays(1));
			}
		}


		public ImmutableSortedDictionary<long, GameTask> ListClone() {
			return this.Queue.ToImmutableSortedDictionary();
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



