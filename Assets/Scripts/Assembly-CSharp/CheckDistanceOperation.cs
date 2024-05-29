using UnityEngine;
using engine.events;
using engine.operations;

public class CheckDistanceOperation : Operation
{
	private Transform transform_0;

	private Transform transform_1;

	private float float_0;

	public CheckDistanceOperation(GameObject gameObject_0, GameObject gameObject_1, float float_1)
	{
		float_0 = Mathf.Max(0f, float_1);
		if (gameObject_0 != null)
		{
			transform_0 = gameObject_0.transform;
		}
		if (gameObject_1 != null)
		{
			transform_1 = gameObject_1.transform;
		}
	}

	protected override void Execute()
	{
		if (transform_0 == null || transform_1 == null)
		{
			Complete();
		}
		else
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}
	}

	private void Update()
	{
		float num = Vector3.Distance(transform_1.position, transform_0.position);
		if (num <= float_0)
		{
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			Complete();
		}
	}
}
