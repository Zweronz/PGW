using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityThreading
{
	public class TaskDistributor : DispatcherBase
	{
		private TaskWorker[] taskWorker_0;

		private static TaskDistributor taskDistributor_0;

		public int int_1;

		private string string_0;

		private bool bool_1;

		private ThreadPriority threadPriority_0 = ThreadPriority.BelowNormal;

		[CompilerGenerated]
		private static Func<TaskWorker, bool> func_1;

		internal WaitHandle WaitHandle_0
		{
			get
			{
				return manualResetEvent_0;
			}
		}

		public static TaskDistributor TaskDistributor_0
		{
			get
			{
				if (taskDistributor_0 == null)
				{
					throw new InvalidOperationException("No default TaskDistributor found, please create a new TaskDistributor instance before calling this property.");
				}
				return taskDistributor_0;
			}
		}

		public static TaskDistributor TaskDistributor_1
		{
			get
			{
				return taskDistributor_0;
			}
		}

		public override int Int32_0
		{
			get
			{
				int num = base.Int32_0;
				lock (taskWorker_0)
				{
					for (int i = 0; i < taskWorker_0.Length; i++)
					{
						num += taskWorker_0[i].dispatcher_1.Int32_0;
					}
					return num;
				}
			}
		}

		public ThreadPriority ThreadPriority_0
		{
			get
			{
				return threadPriority_0;
			}
			set
			{
				threadPriority_0 = value;
				TaskWorker[] array = taskWorker_0;
				foreach (TaskWorker taskWorker in array)
				{
					taskWorker.ThreadPriority_0 = value;
				}
			}
		}

		public TaskDistributor(string string_1)
			: this(string_1, 0)
		{
		}

		public TaskDistributor(string string_1, int int_2)
			: this(string_1, int_2, true)
		{
		}

		public TaskDistributor(string string_1, int int_2, bool bool_2)
		{
			string_0 = string_1;
			if (int_2 <= 0)
			{
				int_2 = ThreadBase.Int32_0 * 2;
			}
			taskWorker_0 = new TaskWorker[int_2];
			lock (taskWorker_0)
			{
				for (int i = 0; i < int_2; i++)
				{
					taskWorker_0[i] = new TaskWorker(string_1, this);
				}
			}
			if (taskDistributor_0 == null)
			{
				taskDistributor_0 = this;
			}
			if (bool_2)
			{
				Start();
			}
		}

		public void Start()
		{
			lock (taskWorker_0)
			{
				for (int i = 0; i < taskWorker_0.Length; i++)
				{
					if (!taskWorker_0[i].Boolean_0)
					{
						taskWorker_0[i].Start();
					}
				}
			}
		}

		public void SpawnAdditionalWorkerThread()
		{
			lock (taskWorker_0)
			{
				Array.Resize(ref taskWorker_0, taskWorker_0.Length + 1);
				taskWorker_0[taskWorker_0.Length - 1] = new TaskWorker(string_0, this);
				taskWorker_0[taskWorker_0.Length - 1].ThreadPriority_0 = threadPriority_0;
				taskWorker_0[taskWorker_0.Length - 1].Start();
			}
		}

		internal void FillTasks(Dispatcher dispatcher_0)
		{
			dispatcher_0.AddTasks(IsolateTasks(1));
		}

		protected override void CheckAccessLimitation()
		{
			if (int_1 <= 0 && bool_0 && ThreadBase.ThreadBase_0 != null && ThreadBase.ThreadBase_0 is TaskWorker && ((TaskWorker)ThreadBase.ThreadBase_0).TaskDistributor_0 == this)
			{
				throw new InvalidOperationException("Access to TaskDistributor prohibited when called from inside a TaskDistributor thread. Dont dispatch new Tasks through the same TaskDistributor. If you want to distribute new tasks create a new TaskDistributor and use the new created instance. Remember to dispose the new instance to prevent thread spamming.");
			}
		}

		internal override void TasksAdded()
		{
			if (int_1 > 0 && (taskWorker_0.All((TaskWorker taskWorker_1) => taskWorker_1.dispatcher_1.Int32_0 > 0 || taskWorker_1.Boolean_2) || queue_0.Count > taskWorker_0.Length))
			{
				Interlocked.Decrement(ref int_1);
				SpawnAdditionalWorkerThread();
			}
			base.TasksAdded();
		}

		public override void Dispose()
		{
			if (bool_1)
			{
				return;
			}
			while (true)
			{
				Task task;
				lock (object_0)
				{
					if (queue_0.Count != 0)
					{
						task = queue_0.Dequeue();
						goto IL_002d;
					}
				}
				break;
				IL_002d:
				task.Dispose();
			}
			lock (taskWorker_0)
			{
				for (int i = 0; i < taskWorker_0.Length; i++)
				{
					taskWorker_0[i].Dispose();
				}
				taskWorker_0 = new TaskWorker[0];
			}
			manualResetEvent_0.Close();
			manualResetEvent_0 = null;
			if (taskDistributor_0 == this)
			{
				taskDistributor_0 = null;
			}
			bool_1 = true;
		}
	}
}
