using UnityEngine;

public sealed class FPCheckBlockProcess : FPBaseProcess
{
	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		fpprocessSharedData_0.bool_0 = false;
		if (Player_move_c.Boolean_0)
		{
			fpprocessSharedData_0.bool_0 = true;
			if (fpprocessSharedData_0.bool_1)
			{
				fpprocessSharedData_0.bool_1 = false;
				fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0.PlayerStateController_0.DispatchStartShoot();
			}
			fpprocessSharedData_0.bool_2 = false;
			fpprocessSharedData_0.bool_3 = false;
			fpprocessSharedData_0.vector2_0 = Vector2.zero;
		}
	}
}
