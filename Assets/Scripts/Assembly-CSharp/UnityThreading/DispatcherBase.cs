using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityThreading
{
	public abstract class DispatcherBase : IDisposable
	{
		protected int int_0;

		protected object object_0 = new object();

		protected Queue<Task> queue_0 = new Queue<Task>();

		protected Queue<Task> queue_1 = new Queue<Task>();

		protected ManualResetEvent manualResetEvent_0 = new ManualResetEvent(false);

		public bool bool_0;

		public TaskSortingSystem taskSortingSystem_0;

		[CompilerGenerated]
		private static Func<Task, int> func_0;

		public bool Boolean_0
		{
			get
			{
				return manualResetEvent_0.InterWaitOne(0);
			}
		}

		public virtual int Int32_0
		{
			get
			{
				lock (object_0)
				{
					return queue_0.Count;
				}
			}
		}

		public DispatcherBase()
		{
		}

		public void Lock()
		{
			lock (object_0)
			{
				int_0++;
			}
		}

		public void Unlock()
		{
			lock (object_0)
			{
				int_0--;
				if (int_0 == 0 && queue_1.Count > 0)
				{
					while (queue_1.Count > 0)
					{
						queue_0.Enqueue(queue_1.Dequeue());
					}
					if (taskSortingSystem_0 == TaskSortingSystem.ReorderWhenAdded || taskSortingSystem_0 == TaskSortingSystem.ReorderWhenExecuted)
					{
						ReorderTasks();
					}
					TasksAdded();
				}
			}
		}

		public Task<T> Dispatch<T>(Func<T> func_1)
		{
			CheckAccessLimitation();
			Task<T> task = new Task<T>(func_1);
			AddTask(task);
			return task;
		}

		public Task Dispatch(Action action_0)
		{
			CheckAccessLimitation();
			Task task = Task.Create(action_0);
			AddTask(task);
			return task;
		}

		public Task Dispatch(Task task_0)
		{
			CheckAccessLimitation();
			AddTask(task_0);
			return task_0;
		}

		internal virtual void AddTask(Task task_0)
		{
			lock (object_0)
			{
				if (int_0 > 0)
				{
					queue_1.Enqueue(task_0);
					return;
				}
				queue_0.Enqueue(task_0);
				if (taskSortingSystem_0 == TaskSortingSystem.ReorderWhenAdded || taskSortingSystem_0 == TaskSortingSystem.ReorderWhenExecuted)
				{
					ReorderTasks();
				}
			}
			TasksAdded();
		}

		internal void AddTasks(IEnumerable<Task> ienumerable_0)
		{
			lock (object_0)
			{
				if (int_0 > 0)
				{
					{
						foreach (Task item in ienumerable_0)
						{
							queue_1.Enqueue(item);
						}
						return;
					}
				}
				foreach (Task item2 in ienumerable_0)
				{
					queue_0.Enqueue(item2);
				}
				if (taskSortingSystem_0 == TaskSortingSystem.ReorderWhenAdded || taskSortingSystem_0 == TaskSortingSystem.ReorderWhenExecuted)
				{
					ReorderTasks();
				}
			}
			TasksAdded();
		}

		internal virtual void TasksAdded()
		{
			manualResetEvent_0.Set();
		}

		protected void ReorderTasks()
		{
			queue_0 = new Queue<Task>(queue_0.OrderBy((Task task_0) => task_0.int_0));
		}

		internal IEnumerable<Task> SplitTasks(int int_1)
		{
			if (int_1 == 0)
			{
				int_1 = 2;
			}
			int int_2 = Int32_0 / int_1;
			return IsolateTasks(int_2);
		}

		internal IEnumerable<Task> IsolateTasks(int int_1)
		{
			Queue<Task> queue = new Queue<Task>();
			if (int_1 == 0)
			{
				int_1 = queue_0.Count;
			}
			lock (object_0)
			{
				for (int i = 0; i < int_1 && i < queue_0.Count; i++)
				{
					queue.Enqueue(queue_0.Dequeue());
				}
				if (Int32_0 == 0)
				{
					manualResetEvent_0.Reset();
				}
			}
			return queue;
		}

		protected abstract void CheckAccessLimitation();

		public virtual void Dispose()
		{
			while (true)
			{
				Task task;
				lock (object_0)
				{
					if (queue_0.Count != 0)
					{
						task = queue_0.Dequeue();
						goto IL_0031;
					}
				}
				break;
				IL_0031:
				task.Dispose();
			}
			manualResetEvent_0.Close();
			manualResetEvent_0 = null;
		}
	}
}
