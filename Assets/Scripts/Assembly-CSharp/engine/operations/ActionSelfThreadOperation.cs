using System;
using UnityThreading;
using engine.events;

namespace engine.operations
{
	public class ActionSelfThreadOperation : ActionOperation
	{
		private ActionThread actionThread_0;

		public ActionSelfThreadOperation(Action action_1)
			: base(action_1)
		{
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalSubscribe(delegate
			{
				Kill();
			});
		}

		protected override void Execute()
		{
			actionThread_0 = UnityThreadHelper.CreateThread((Action)delegate
			{
				action_0();
			});
		}

		public void Kill(ApplicationQuitUnityEvent applicationQuitUnityEvent_0 = null)
		{
			if (actionThread_0 != null)
			{
				actionThread_0.Exit();
			}
		}

		protected override void Result()
		{
			Kill();
		}
	}
}
