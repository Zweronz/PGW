using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace UnityThreading
{
	public abstract class Task
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Unit
		{
		}

		private object object_0 = new object();

		private bool bool_0;

		public string string_0;

		public volatile int int_0;

		private ManualResetEvent manualResetEvent_0 = new ManualResetEvent(false);

		private ManualResetEvent manualResetEvent_1 = new ManualResetEvent(false);

		private ManualResetEvent manualResetEvent_2 = new ManualResetEvent(false);

		private bool bool_1;

		[ThreadStatic]
		private static Task task_0;

		private bool bool_2;

		private TaskEndedEventHandler taskEndedEventHandler_0;

		public static Task Task_0
		{
			get
			{
				return task_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return manualResetEvent_0.InterWaitOne(0);
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_0 || manualResetEvent_1.InterWaitOne(0);
			}
		}

		public bool Boolean_2
		{
			get
			{
				return manualResetEvent_2.InterWaitOne(0);
			}
		}

		public bool Boolean_3
		{
			get
			{
				return manualResetEvent_2.InterWaitOne(0) && !manualResetEvent_0.InterWaitOne(0);
			}
		}

		public bool Boolean_4
		{
			get
			{
				return manualResetEvent_2.InterWaitOne(0) && manualResetEvent_0.InterWaitOne(0);
			}
		}

		public abstract object Object_0 { get; }

		private event TaskEndedEventHandler taskEnded
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				taskEndedEventHandler_0 = (TaskEndedEventHandler)Delegate.Combine(taskEndedEventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				taskEndedEventHandler_0 = (TaskEndedEventHandler)Delegate.Remove(taskEndedEventHandler_0, value);
			}
		}

		public event TaskEndedEventHandler TaskEnded
		{
			add
			{
				lock (object_0)
				{
					if (manualResetEvent_2.InterWaitOne(0))
					{
						value(this);
					}
					else
					{
						taskEndedEventHandler_0 = (TaskEndedEventHandler)Delegate.Combine(taskEndedEventHandler_0, value);
					}
				}
			}
			remove
			{
				lock (object_0)
				{
					taskEndedEventHandler_0 = (TaskEndedEventHandler)Delegate.Remove(taskEndedEventHandler_0, value);
				}
			}
		}

		public Task()
		{
		}

		~Task()
		{
			Dispose();
		}

		private void End()
		{
			lock (object_0)
			{
				manualResetEvent_2.Set();
				if (taskEndedEventHandler_0 != null)
				{
					taskEndedEventHandler_0(this);
				}
				manualResetEvent_1.Set();
				if (task_0 == this)
				{
					task_0 = null;
				}
				bool_0 = true;
			}
		}

		protected abstract IEnumerator Do();

		public void Abort()
		{
			manualResetEvent_0.Set();
			if (!bool_1)
			{
				End();
			}
		}

		public void AbortWait()
		{
			Abort();
			if (bool_1)
			{
				Wait();
			}
		}

		public void AbortWaitForSeconds(float float_0)
		{
			Abort();
			if (bool_1)
			{
				WaitForSeconds(float_0);
			}
		}

		public void Wait()
		{
			if (!bool_0)
			{
				int_0--;
				manualResetEvent_1.WaitOne();
			}
		}

		public void WaitForSeconds(float float_0)
		{
			if (!bool_0)
			{
				int_0--;
				manualResetEvent_1.InterWaitOne(TimeSpan.FromSeconds(float_0));
			}
		}

		public abstract TResult Wait<TResult>();

		public abstract TResult WaitForSeconds<TResult>(float float_0);

		public abstract TResult WaitForSeconds<TResult>(float float_0, TResult gparam_0);

		internal void DoInternal()
		{
			task_0 = this;
			bool_1 = true;
			if (!Boolean_0)
			{
				try
				{
					IEnumerator enumerator = Do();
					if (enumerator == null)
					{
						End();
						return;
					}
					RunEnumerator(enumerator);
				}
				catch (Exception ex)
				{
					Abort();
					if (string.IsNullOrEmpty(string_0))
					{
						Debug.LogError("Error while processing task:\n" + ex.ToString());
					}
					else
					{
						Debug.LogError("Error while processing task '" + string_0 + "':\n" + ex.ToString());
					}
				}
			}
			End();
		}

		private void RunEnumerator(IEnumerator ienumerator_0)
		{
			ThreadBase threadBase_ = ThreadBase.ThreadBase_0;
			do
			{
				if (!(ienumerator_0.Current is Task))
				{
					if (!(ienumerator_0.Current is SwitchTo))
					{
						continue;
					}
					SwitchTo switchTo = (SwitchTo)ienumerator_0.Current;
					if (switchTo.TargetType_0 == SwitchTo.TargetType.Main && threadBase_ != null)
					{
						Task task = Create((Action)delegate
						{
							if (ienumerator_0.MoveNext() && !Boolean_0)
							{
								RunEnumerator(ienumerator_0);
							}
						});
						threadBase_.DispatchAndWait(task);
					}
					else if (switchTo.TargetType_0 == SwitchTo.TargetType.Thread && threadBase_ == null)
					{
						break;
					}
				}
				else
				{
					Task task2 = (Task)ienumerator_0.Current;
					threadBase_.DispatchAndWait(task2);
				}
			}
			while (ienumerator_0.MoveNext() && !Boolean_0);
		}

		public void Dispose()
		{
			if (!bool_2)
			{
				bool_2 = true;
				if (bool_1)
				{
					Wait();
				}
				manualResetEvent_2.Close();
				manualResetEvent_1.Close();
				manualResetEvent_0.Close();
			}
		}

		public Task Run(DispatcherBase dispatcherBase_0)
		{
			if (dispatcherBase_0 == null)
			{
				return Run();
			}
			dispatcherBase_0.Dispatch(this);
			return this;
		}

		public Task Run()
		{
			Run(UnityThreadHelper.TaskDistributor_0);
			return this;
		}

		public static Task Create(Action<Task> action_0)
		{
			return new Task<Unit>(action_0);
		}

		public static Task Create(Action action_0)
		{
			return new Task<Unit>(action_0);
		}

		public static Task<T> Create<T>(Func<Task, T> func_0)
		{
			return new Task<T>(func_0);
		}

		public static Task<T> Create<T>(Func<T> func_0)
		{
			return new Task<T>(func_0);
		}

		public static Task Create(IEnumerator ienumerator_0)
		{
			return new Task<IEnumerator>(ienumerator_0);
		}

		public static Task<T> Create<T>(Type type_0, string string_1, params object[] object_1)
		{
			return new Task<T>(type_0, string_1, object_1);
		}

		public static Task<T> Create<T>(object object_1, string string_1, params object[] object_2)
		{
			return new Task<T>(object_1, string_1, object_2);
		}
	}
	public class Task<T> : Task
	{
		private Func<Task, T> function;

		private T result;

		public override object Object_0
		{
			get
			{
				if (!base.Boolean_2)
				{
					Wait();
				}
				return result;
			}
		}

		public T Prop_0
		{
			get
			{
				if (!base.Boolean_2)
				{
					Wait();
				}
				return result;
			}
		}

		public Task(Func<Task, T> func_0)
		{
			function = func_0;
		}

		public Task(Func<T> func_0)
		{
			function = (Task task_0) => func_0();
		}

		public Task(Action<Task> action_0)
		{
			function = delegate(Task task_0)
			{
				action_0(task_0);
				return default(T);
			};
		}

		public Task(Action action_0)
		{
			function = delegate
			{
				action_0();
				return default(T);
			};
		}

		public Task(IEnumerator ienumerator_0)
		{
			function = (Task task_0) => (T)ienumerator_0;
		}

		public Task(Type type_0, string string_1, params object[] object_1)
		{
			MethodInfo methodInfo = type_0.GetMethod(string_1, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
			if (methodInfo == null)
			{
				throw new ArgumentException("methodName", "Fitting method with the given name was not found.");
			}
			function = (Task task_0) => (T)methodInfo.Invoke(null, object_1);
		}

		public Task(object object_1, string string_1, params object[] object_2)
		{
			MethodInfo methodInfo = object_1.GetType().GetMethod(string_1, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
			if (methodInfo == null)
			{
				throw new ArgumentException("methodName", "Fitting method with the given name was not found.");
			}
			function = (Task task_0) => (T)methodInfo.Invoke(object_1, object_2);
		}

		protected override IEnumerator Do()
		{
			result = function(this);
			if (result is IEnumerator)
			{
				return (IEnumerator)(object)result;
			}
			return null;
		}

		public override TResult Wait<TResult>()
		{
			int_0--;
			return (TResult)(object)Prop_0;
		}

		public override TResult WaitForSeconds<TResult>(float float_0)
		{
			int_0--;
			return ((Task)this).WaitForSeconds(float_0, default(TResult));
		}

		public override TResult WaitForSeconds<TResult>(float float_0, TResult gparam_0)
		{
			if (!base.Boolean_1)
			{
				WaitForSeconds(float_0);
			}
			if (base.Boolean_3)
			{
				return (TResult)(object)result;
			}
			return gparam_0;
		}

		public new Task<T> Run(DispatcherBase dispatcherBase_0)
		{
			((Task)this).Run(dispatcherBase_0);
			return this;
		}

		public new Task<T> Run()
		{
			((Task)this).Run();
			return this;
		}
	}
}
