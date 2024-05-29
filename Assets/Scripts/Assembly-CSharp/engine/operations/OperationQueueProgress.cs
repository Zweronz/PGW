using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using engine.events;

namespace engine.operations
{
	public class OperationQueueProgress
	{
		private List<Operation> list_0 = new List<Operation>();

		private float float_0;

		private float float_1;

		private bool bool_0;

		private bool bool_1;

		private float float_2;

		private Action<float> action_0;

		private Action action_1;

		[CompilerGenerated]
		private bool bool_2;

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			private set
			{
				bool_2 = value;
			}
		}

		public float Single_0
		{
			get
			{
				return float_0 / (float)list_0.Count;
			}
		}

		public event Action<float> ProgressCallback
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				action_0 = (Action<float>)Delegate.Combine(action_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				action_0 = (Action<float>)Delegate.Remove(action_0, value);
			}
		}

		public event Action AllOperationsCompleteCallback
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				action_1 = (Action)Delegate.Combine(action_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				action_1 = (Action)Delegate.Remove(action_1, value);
			}
		}

		public OperationQueueProgress()
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(MainUpdateEvent);
		}

		public void Add(Operation operation_0)
		{
			if (operation_0.Boolean_2)
			{
				operation_0.ProgressEvent_0.Subscribe(HandleOperationProgress);
				list_0.Add(operation_0);
			}
			float_1 = list_0.Count;
		}

		private void HandleOperationProgress(float float_3)
		{
			float_2 = float_3;
			bool_1 = true;
		}

		public void Update()
		{
			if (float_1 == 0f)
			{
				return;
			}
			int num = 0;
			for (int i = 0; (float)i < float_1; i++)
			{
				if (list_0[i].Boolean_0)
				{
					num++;
				}
			}
			if ((float)num != float_0)
			{
				float_0 = num;
				float_2 = 0f;
				bool_1 = true;
			}
			if (!bool_0 && float_0 == float_1)
			{
				bool_0 = true;
				if (action_1 != null)
				{
					action_1();
				}
				DependSceneEvent<MainUpdate>.GlobalUnsubscribe(MainUpdateEvent);
			}
		}

		private void MainUpdateEvent()
		{
			if (bool_1)
			{
				bool_1 = false;
				if (action_0 != null)
				{
					action_0(Single_0 + float_2 / float_1);
				}
			}
		}
	}
}
