using UnityEngine;
using engine.events;
using engine.operations;

public sealed class WaitUserInputAxisOperation : Operation
{
	public enum AXIS_ROTATES
	{
		None = 0,
		Left = 1,
		Right = 2,
		Up = 3,
		Down = 4
	}

	private AXIS_ROTATES axis_ROTATES_0;

	public WaitUserInputAxisOperation(AXIS_ROTATES axis_ROTATES_1)
	{
		axis_ROTATES_0 = axis_ROTATES_1;
	}

	protected override void Execute()
	{
		if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update, true);
		}
	}

	private void Update()
	{
		Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
		Transform transform2 = GameObject.FindGameObjectWithTag("PlayerGun").transform;
		switch (axis_ROTATES_0)
		{
		case AXIS_ROTATES.Left:
			if (transform.rotation.y <= -0.9f)
			{
				WaitComplete();
			}
			break;
		case AXIS_ROTATES.Right:
			if (transform.rotation.y >= -0.6f)
			{
				WaitComplete();
			}
			break;
		case AXIS_ROTATES.Up:
			if (transform2.rotation.x <= -0.1f)
			{
				WaitComplete();
			}
			break;
		case AXIS_ROTATES.Down:
			if (transform2.rotation.x >= 0.05f)
			{
				WaitComplete();
			}
			break;
		}
	}

	private void WaitComplete()
	{
		if (DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		}
		axis_ROTATES_0 = AXIS_ROTATES.None;
		Complete();
	}
}
