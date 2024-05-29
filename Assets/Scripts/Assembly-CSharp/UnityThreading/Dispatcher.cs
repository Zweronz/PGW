using System;
using System.Collections.Generic;
using System.Threading;

namespace UnityThreading
{
	public class Dispatcher : DispatcherBase
	{
		[ThreadStatic]
		private static Task task_0;

		[ThreadStatic]
		internal static Dispatcher dispatcher_0;

		protected static Dispatcher dispatcher_1;

		public static Task Task_0
		{
			get
			{
				if (task_0 == null)
				{
					throw new InvalidOperationException("No task is currently running.");
				}
				return task_0;
			}
		}

		public static Dispatcher Dispatcher_0
		{
			get
			{
				if (dispatcher_0 == null)
				{
					throw new InvalidOperationException("No Dispatcher found for the current thread, please create a new Dispatcher instance before calling this property.");
				}
				return dispatcher_0;
			}
			set
			{
				if (dispatcher_0 != null)
				{
					dispatcher_0.Dispose();
				}
				dispatcher_0 = value;
			}
		}

		public static Dispatcher Dispatcher_1
		{
			get
			{
				return dispatcher_0;
			}
		}

		public static Dispatcher Dispatcher_2
		{
			get
			{
				if (dispatcher_1 == null)
				{
					throw new InvalidOperationException("No Dispatcher found for the main thread, please create a new Dispatcher instance before calling this property.");
				}
				return dispatcher_1;
			}
		}

		public static Dispatcher Dispatcher_3
		{
			get
			{
				return dispatcher_1;
			}
		}

		public Dispatcher()
			: this(true)
		{
		}

		public Dispatcher(bool bool_1)
		{
			if (bool_1)
			{
				if (dispatcher_0 != null)
				{
					throw new InvalidOperationException("Only one Dispatcher instance allowed per thread.");
				}
				dispatcher_0 = this;
				if (dispatcher_1 == null)
				{
					dispatcher_1 = this;
				}
			}
		}

		public static Func<T> CreateSafeFunction<T>(Func<T> func_1)
		{
			return delegate
			{
				try
				{
					return func_1();
				}
				catch
				{
					Task_0.Abort();
					return default(T);
				}
			};
		}

		public static Action CreateSafeAction<T>(Action action_0)
		{
			return delegate
			{
				try
				{
					action_0();
				}
				catch
				{
					Task_0.Abort();
				}
			};
		}

		public void ProcessTasks()
		{
			if (manualResetEvent_0.InterWaitOne(0))
			{
				ProcessTasksInternal();
			}
		}

		public bool ProcessTasks(WaitHandle waitHandle_0)
		{
			if (WaitHandle.WaitAny(new WaitHandle[2] { waitHandle_0, manualResetEvent_0 }) == 0)
			{
				return false;
			}
			ProcessTasksInternal();
			return true;
		}

		public bool ProcessNextTask()
		{
			Task task_;
			lock (object_0)
			{
				if (queue_0.Count == 0)
				{
					return false;
				}
				task_ = queue_0.Dequeue();
			}
			ProcessSingleTask(task_);
			if (Int32_0 == 0)
			{
				manualResetEvent_0.Reset();
			}
			return true;
		}

		public bool ProcessNextTask(WaitHandle waitHandle_0)
		{
			if (WaitHandle.WaitAny(new WaitHandle[2] { waitHandle_0, manualResetEvent_0 }) == 0)
			{
				return false;
			}
			Task task_;
			lock (object_0)
			{
				if (queue_0.Count == 0)
				{
					return false;
				}
				task_ = queue_0.Dequeue();
			}
			ProcessSingleTask(task_);
			if (Int32_0 == 0)
			{
				manualResetEvent_0.Reset();
			}
			return true;
		}

		private void ProcessTasksInternal()
		{
			List<Task> list;
			lock (object_0)
			{
				list = new List<Task>(queue_0);
				queue_0.Clear();
			}
			while (list.Count != 0)
			{
				Task task_ = list[0];
				list.RemoveAt(0);
				ProcessSingleTask(task_);
			}
			if (Int32_0 == 0)
			{
				manualResetEvent_0.Reset();
			}
		}

		private void ProcessSingleTask(Task task_1)
		{
			RunTask(task_1);
			if (taskSortingSystem_0 == TaskSortingSystem.ReorderWhenExecuted)
			{
				lock (object_0)
				{
					ReorderTasks();
				}
			}
		}

		internal void RunTask(Task task_1)
		{
			Task task = task_0;
			task_0 = task_1;
			task_0.DoInternal();
			task_0 = task;
		}

		protected override void CheckAccessLimitation()
		{
			if (bool_0 && dispatcher_0 == this)
			{
				throw new InvalidOperationException("Dispatching a Task with the Dispatcher associated to the current thread is prohibited. You can run these Tasks without the need of a Dispatcher.");
			}
		}

		public override void Dispose()
		{
			while (true)
			{
				lock (object_0)
				{
					if (queue_0.Count != 0)
					{
						task_0 = queue_0.Dequeue();
						goto IL_0035;
					}
				}
				break;
				IL_0035:
				task_0.Dispose();
			}
			manualResetEvent_0.Close();
			manualResetEvent_0 = null;
			if (dispatcher_0 == this)
			{
				dispatcher_0 = null;
			}
			if (dispatcher_1 == this)
			{
				dispatcher_1 = null;
			}
		}
	}
}
