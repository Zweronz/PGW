using System;
using System.Runtime.CompilerServices;
using UnityThreading;
using engine.events;
using engine.helpers;
using engine.unity;

namespace engine.operations
{
	public class Operation : BaseEvent<Operation>
	{
		public enum StatusEvent
		{
			Executed = 0,
			Complete = 1
		}

		public static bool bool_0;

		protected ProgressEvent progressEvent_0;

		private bool bool_1;

		[CompilerGenerated]
		private bool bool_2;

		[CompilerGenerated]
		private bool bool_3;

		[CompilerGenerated]
		private bool bool_4;

		[CompilerGenerated]
		private bool bool_5;

		[CompilerGenerated]
		private string string_0;

		public virtual bool Boolean_4
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			set
			{
				bool_2 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_3;
			}
			[CompilerGenerated]
			protected set
			{
				bool_3 = value;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_4;
			}
			[CompilerGenerated]
			set
			{
				bool_4 = value;
			}
		}

		public ProgressEvent ProgressEvent_0
		{
			get
			{
				if (progressEvent_0 == null)
				{
					progressEvent_0 = new ProgressEvent();
				}
				return progressEvent_0;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return progressEvent_0 != null;
			}
		}

		public bool Boolean_3
		{
			[CompilerGenerated]
			get
			{
				return bool_5;
			}
			[CompilerGenerated]
			set
			{
				bool_5 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		public Operation()
		{
			Boolean_3 = bool_0;
			String_0 = GetType().FullName.Replace("engine.operations.", string.Empty);
		}

		public bool Start()
		{
			if (bool_1)
			{
				return false;
			}
			bool_1 = true;
			if (Boolean_3)
			{
				Log.AddLine(string.Format("{0} Strated. {1}", String_0, ToString()));
			}
			if (!Boolean_4)
			{
				try
				{
					Execute();
				}
				catch (Exception exception_)
				{
					MonoSingleton<Log>.Prop_0.DumpError(exception_);
					Complete();
				}
				Dispatch(this, StatusEvent.Executed);
			}
			else
			{
				Complete();
			}
			return true;
		}

		public virtual void Complete()
		{
			Boolean_0 = true;
		}

		public void CompleteInUnityThread()
		{
			if (Dispatcher.Dispatcher_2 != Dispatcher.Dispatcher_1)
			{
				return;
			}
			try
			{
				if (Boolean_3)
				{
					string empty = string.Empty;
					empty = string.Format("{0} Complete. {1}", String_0, ToString());
					Log.AddLine(empty);
				}
				Result();
				Dispatch(this, StatusEvent.Complete);
				Clear();
			}
			catch (Exception exception_)
			{
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
			}
		}

		protected virtual void Result()
		{
		}

		public void Error()
		{
			Complete();
			Boolean_1 = true;
			Fail();
			Dispatch(this, StatusEvent.Complete);
			Clear();
		}

		protected virtual void Execute()
		{
		}

		protected virtual void Fail()
		{
		}

		private void Clear()
		{
			UnsubscribeAll();
			if (progressEvent_0 != null)
			{
				progressEvent_0.UnsubscribeAll();
			}
		}
	}
}
