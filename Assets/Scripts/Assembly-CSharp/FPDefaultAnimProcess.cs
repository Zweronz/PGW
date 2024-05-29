using UnityEngine;

public sealed class FPDefaultAnimProcess : FPBaseProcess
{
	private float float_0;

	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		SkinName skinName_ = fpprocessSharedData_0.firstPersonPlayerController_0.SkinName_0;
		if (fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.isGrounded)
		{
			if (fpprocessSharedData_0.vector2_0.y > 0f)
			{
				skinName_.SetAnim("Walk");
			}
			else if (fpprocessSharedData_0.vector2_0.y < 0f)
			{
				skinName_.SetAnim("Walk_Back");
			}
			else if (fpprocessSharedData_0.vector2_0.x > 0f)
			{
				skinName_.SetAnim("Walk_Right");
			}
			else if (fpprocessSharedData_0.vector2_0.x < 0f)
			{
				skinName_.SetAnim("Walk_Left");
			}
			else
			{
				skinName_.SetAnim("Idle");
			}
		}
		else if (Defs.bool_8)
		{
			if (fpprocessSharedData_0.vector2_0.y > 0f)
			{
				skinName_.SetAnim("Jetpack_Run_Front");
			}
			else if (fpprocessSharedData_0.vector2_0.y < 0f)
			{
				skinName_.SetAnim("Jetpack_Run_Back");
			}
			else if (fpprocessSharedData_0.vector2_0.x > 0f)
			{
				skinName_.SetAnim("Jetpack_Run_Righte");
			}
			else if (fpprocessSharedData_0.vector2_0.x < 0f)
			{
				skinName_.SetAnim("Jetpack_Run_Left");
			}
			else
			{
				skinName_.SetAnim("Jetpack_Idle");
			}
		}
		float_0 -= Time.deltaTime;
		if (float_0 < 0f && fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.isGrounded)
		{
			float_0 = 0.5f;
			if (fpprocessSharedData_0.vector2_0.x == 0f && fpprocessSharedData_0.vector2_0.y == 0f)
			{
				fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0.IdleAnimation();
			}
			else
			{
				fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0.WalkAnimation();
			}
		}
	}
}
