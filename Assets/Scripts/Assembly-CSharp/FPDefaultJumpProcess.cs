using System.Collections;
using UnityEngine;

public sealed class FPDefaultJumpProcess : FPBaseProcess
{
	private bool bool_0 = true;

	private bool bool_1;

	private FPProcessSharedData fpprocessSharedData_0;

	public override void Update(FPProcessSharedData fpprocessSharedData_1)
	{
		fpprocessSharedData_0 = fpprocessSharedData_1;
		if (!fpprocessSharedData_1.firstPersonPlayerController_0.CharacterController_0.isGrounded && fpprocessSharedData_1.firstPersonPlayerController_0.FPSStateController_0.STATES_0 != FPSStateController.STATES.STAIRS)
		{
			if (fpprocessSharedData_1.bool_3 && ((fpprocessSharedData_1.Boolean_0 && !bool_0 && fpprocessSharedData_1.bool_5) || Defs.bool_8))
			{
				bool_0 = true;
				fpprocessSharedData_1.bool_4 = false;
				if (!Defs.bool_8)
				{
					fpprocessSharedData_1.firstPersonPlayerController_0.SkinName_0.SetAnim("Jump");
				}
				if (Defs.Boolean_0 && !Defs.bool_8)
				{
					NGUITools.PlaySound(fpprocessSharedData_1.firstPersonPlayerController_0.jumpClip);
				}
				fpprocessSharedData_1.vector3_1.y = 1.1f * (PlayerParamsSettings.Get.PlayerJumpSpeed * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_JUMP_MODIFIER)));
			}
			fpprocessSharedData_1.vector3_1.y += Physics.gravity.y * fpprocessSharedData_1.firstPersonPlayerController_0.Single_0 * Time.deltaTime;
		}
		else
		{
			if (fpprocessSharedData_1.Boolean_0)
			{
				bool_0 = false;
			}
			fpprocessSharedData_1.bool_4 = true;
			if (fpprocessSharedData_1.bool_2)
			{
				fpprocessSharedData_1.bool_2 = false;
				fpprocessSharedData_1.bool_4 = false;
				if (!Defs.bool_8)
				{
					fpprocessSharedData_1.firstPersonPlayerController_0.SkinName_0.SetAnim("Jump");
				}
				if (Defs.Boolean_0 && !Defs.bool_8)
				{
					NGUITools.PlaySound(fpprocessSharedData_1.firstPersonPlayerController_0.jumpClip);
				}
				fpprocessSharedData_1.bool_5 = false;
				fpprocessSharedData_1.firstPersonPlayerController_0.StartCoroutine(EnableSecondJump());
				fpprocessSharedData_1.vector3_1 = Vector3.zero;
				float num = PlayerParamsSettings.Get.PlayerJumpSpeed * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_JUMP_MODIFIER));
				if (fpprocessSharedData_1.firstPersonPlayerController_0.FPSStateController_0.STATES_0 == FPSStateController.STATES.STAIRS)
				{
					fpprocessSharedData_1.vector3_1.x = (0f - fpprocessSharedData_1.firstPersonPlayerController_0.Transform_0.forward.x) * num;
				}
				else
				{
					fpprocessSharedData_1.vector3_1.y = num;
				}
			}
		}
		if (Defs.bool_8 && fpprocessSharedData_1.firstPersonPlayerController_0.Player_move_c_0 != null)
		{
			if (fpprocessSharedData_1.bool_3 != bool_1)
			{
				fpprocessSharedData_1.firstPersonPlayerController_0.Player_move_c_0.SetJetpackParticleEnabled(fpprocessSharedData_1.bool_3);
			}
			bool_1 = fpprocessSharedData_1.bool_3;
		}
		else
		{
			bool_1 = false;
		}
	}

	private IEnumerator EnableSecondJump()
	{
		yield return new WaitForSeconds(0.25f);
		fpprocessSharedData_0.bool_5 = true;
	}
}
