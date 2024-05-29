using UnityEngine;

public sealed class FPDefaultRotationProcess : FPBaseProcess
{
	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		float single_ = PresetGameSettings.PresetGameSettings_0.Single_0;
		float num = 1f;
		if (fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0 != null)
		{
			num *= ((!fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0.Boolean_16) ? 1f : 0.2f);
		}
		fpprocessSharedData_0.vector2_1 *= single_ * num / 10f;
		if (float.IsInfinity(fpprocessSharedData_0.vector2_1.x) || float.IsNegativeInfinity(fpprocessSharedData_0.vector2_1.x) || float.IsInfinity(fpprocessSharedData_0.vector2_1.y) || float.IsNegativeInfinity(fpprocessSharedData_0.vector2_1.y))
		{
			fpprocessSharedData_0.vector2_1 = Vector2.zero;
		}
		fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.Rotate(0f, fpprocessSharedData_0.vector2_1.x, 0f, Space.World);
		fpprocessSharedData_0.firstPersonPlayerController_0.cameraPivot.Rotate(((!fpprocessSharedData_0.firstPersonPlayerController_0.Boolean_1) ? 1f : (-1f)) * (0f - fpprocessSharedData_0.vector2_1.y), 0f, 0f);
		CameraSceneController.UpdateMouse();
	}
}
