using UnityEngine;
using engine.events;

namespace engine.operations
{
	public class LoadLevelOperation : Operation
	{
		private string string_1;

		private int int_0;

		private bool bool_6;

		private bool bool_7;

		private AsyncOperation asyncOperation_0;

		public LoadLevelOperation(int int_1, bool bool_8 = false)
		{
			int_0 = int_1;
			bool_6 = false;
			bool_7 = bool_8;
		}

		public LoadLevelOperation(string string_2, bool bool_8 = false)
		{
			string_1 = string_2;
			bool_6 = true;
			bool_7 = bool_8;
		}

		protected override void Execute()
		{
			if (bool_6)
			{
				asyncOperation_0 = ((!bool_7) ? Application.LoadLevelAdditiveAsync(string_1) : Application.LoadLevelAdditiveAsync(string_1));
			}
			else
			{
				asyncOperation_0 = ((!bool_7) ? Application.LoadLevelAdditiveAsync(int_0) : Application.LoadLevelAdditiveAsync(int_0));
			}
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}

		private void Update()
		{
			if (!asyncOperation_0.isDone)
			{
				base.ProgressEvent_0.Dispatch(asyncOperation_0.progress);
				return;
			}
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			Complete();
		}
	}
}
