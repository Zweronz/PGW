using System;
using System.Runtime.CompilerServices;

namespace engine.operations
{
	public class ActionOperation : Operation
	{
		protected Action action_0;

		[CompilerGenerated]
		private bool bool_6;

		public new bool Boolean_4
		{
			[CompilerGenerated]
			get
			{
				return bool_6;
			}
			[CompilerGenerated]
			set
			{
				bool_6 = value;
			}
		}

		public ActionOperation(Action action_1)
		{
			base.Boolean_3 = false;
			Boolean_4 = true;
			base.String_0 = "Action operation: " + action_1.Method.Name;
			action_0 = action_1;
			base.ProgressEvent_0.Dispatch(0f);
		}

		protected override void Execute()
		{
			if (action_0 != null)
			{
				action_0();
			}
			base.ProgressEvent_0.Dispatch(1f);
			if (Boolean_4)
			{
				Complete();
			}
		}
	}
	public class ActionOperation<T> : Operation
	{
		protected Action<T> _method;

		protected T _parameter;

		public new bool Boolean_4 { get; set; }

		public ActionOperation(Action<T> action_0, T gparam_0)
		{
			base.Boolean_3 = false;
			Boolean_4 = true;
			base.String_0 = "Action operation: " + action_0.Method.Name;
			_method = action_0;
			_parameter = gparam_0;
			base.ProgressEvent_0.Dispatch(0f);
		}

		protected override void Execute()
		{
			if (_method != null)
			{
				_method(_parameter);
			}
			base.ProgressEvent_0.Dispatch(1f);
			if (Boolean_4)
			{
				Complete();
			}
		}
	}
}
