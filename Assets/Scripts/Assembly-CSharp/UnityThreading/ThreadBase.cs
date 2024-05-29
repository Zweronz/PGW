using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace UnityThreading
{
	public abstract class ThreadBase : IDisposable
	{
		protected Dispatcher dispatcher_0;

		protected Thread thread_0;

		protected ManualResetEvent manualResetEvent_0 = new ManualResetEvent(false);

		[ThreadStatic]
		private static ThreadBase threadBase_0;

		private string string_0;

		private System.Threading.ThreadPriority threadPriority_0 = System.Threading.ThreadPriority.BelowNormal;

		public static int Int32_0
		{
			get
			{
				return SystemInfo.processorCount;
			}
		}

		public static ThreadBase ThreadBase_0
		{
			get
			{
				return threadBase_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return thread_0 != null && thread_0.IsAlive;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return manualResetEvent_0.InterWaitOne(0);
			}
		}

		public System.Threading.ThreadPriority ThreadPriority_0
		{
			get
			{
				return threadPriority_0;
			}
			set
			{
				threadPriority_0 = value;
				if (thread_0 != null)
				{
					thread_0.Priority = threadPriority_0;
				}
			}
		}

		public ThreadBase(string string_1)
			: this(string_1, true)
		{
		}

		public ThreadBase(string string_1, bool bool_0)
			: this(string_1, Dispatcher.Dispatcher_1, bool_0)
		{
		}

		public ThreadBase(string string_1, Dispatcher dispatcher_1)
			: this(string_1, dispatcher_1, true)
		{
		}

		public ThreadBase(string string_1, Dispatcher dispatcher_1, bool bool_0)
		{
			string_0 = string_1;
			dispatcher_0 = dispatcher_1;
			if (bool_0)
			{
				Start();
			}
		}

		public void Start()
		{
			if (thread_0 != null)
			{
				Abort();
			}
			manualResetEvent_0.Reset();
			thread_0 = new Thread(DoInternal);
			thread_0.Name = string_0;
			thread_0.Priority = threadPriority_0;
			thread_0.Start();
		}

		public void Exit()
		{
			if (thread_0 != null)
			{
				manualResetEvent_0.Set();
			}
		}

		public void Abort()
		{
			Exit();
			if (thread_0 != null)
			{
				thread_0.Join();
			}
		}

		public void AbortWaitForSeconds(float float_0)
		{
			Exit();
			if (thread_0 != null)
			{
				thread_0.Join((int)(float_0 * 1000f));
				if (thread_0.IsAlive)
				{
					thread_0.Abort();
				}
			}
		}

		public Task<T> Dispatch<T>(Func<T> func_0)
		{
			return dispatcher_0.Dispatch(func_0);
		}

		public T DispatchAndWait<T>(Func<T> func_0)
		{
			Task<T> task = Dispatch(func_0);
			task.Wait();
			return task.Prop_0;
		}

		public T DispatchAndWait<T>(Func<T> func_0, float float_0)
		{
			Task<T> task = Dispatch(func_0);
			task.WaitForSeconds(float_0);
			return task.Prop_0;
		}

		public Task Dispatch(Action action_0)
		{
			return dispatcher_0.Dispatch(action_0);
		}

		public void DispatchAndWait(Action action_0)
		{
			Task task = Dispatch(action_0);
			task.Wait();
		}

		public void DispatchAndWait(Action action_0, float float_0)
		{
			Task task = Dispatch(action_0);
			task.WaitForSeconds(float_0);
		}

		public Task Dispatch(Task task_0)
		{
			return dispatcher_0.Dispatch(task_0);
		}

		public void DispatchAndWait(Task task_0)
		{
			Task task = Dispatch(task_0);
			task.Wait();
		}

		public void DispatchAndWait(Task task_0, float float_0)
		{
			Task task = Dispatch(task_0);
			task.WaitForSeconds(float_0);
		}

		protected void DoInternal()
		{
			threadBase_0 = this;
			IEnumerator enumerator = Do();
			if (enumerator != null)
			{
				RunEnumerator(enumerator);
			}
		}

		private void RunEnumerator(IEnumerator ienumerator_0)
		{
			do
			{
				if (!(ienumerator_0.Current is Task))
				{
					if (!(ienumerator_0.Current is SwitchTo))
					{
						continue;
					}
					SwitchTo switchTo = (SwitchTo)ienumerator_0.Current;
					if (switchTo.TargetType_0 == SwitchTo.TargetType.Main && ThreadBase_0 != null)
					{
						Task task_ = Task.Create((Action)delegate
						{
							if (ienumerator_0.MoveNext() && !Boolean_1)
							{
								RunEnumerator(ienumerator_0);
							}
						});
						DispatchAndWait(task_);
					}
					else if (switchTo.TargetType_0 == SwitchTo.TargetType.Thread && ThreadBase_0 == null)
					{
						break;
					}
				}
				else
				{
					Task task_2 = (Task)ienumerator_0.Current;
					DispatchAndWait(task_2);
				}
			}
			while (ienumerator_0.MoveNext() && !Boolean_1);
		}

		protected abstract IEnumerator Do();

		public virtual void Dispose()
		{
			AbortWaitForSeconds(1f);
		}
	}
}
