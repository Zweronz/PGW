using UnityEngine;
using engine.events;

namespace engine.operations
{
	public class WaitOperation : Operation
	{
		private float float_0;

		private float float_1;

		public WaitOperation(float float_2)
		{
			float_0 = float_2;
			float_1 = 0f;
		}

		protected override void Execute()
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Check);
		}

		protected virtual void Check()
		{
			float_1 += Time.deltaTime;
			if (float_1 > float_0 || Boolean_4)
			{
				DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Check);
				Complete();
			}
		}
	}
}
