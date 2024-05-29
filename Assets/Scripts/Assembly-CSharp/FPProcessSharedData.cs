using UnityEngine;

public sealed class FPProcessSharedData
{
	public FirstPersonPlayerController firstPersonPlayerController_0;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	public bool bool_4 = true;

	public bool bool_5 = true;

	public Vector2 vector2_0 = Vector2.zero;

	public Vector2 vector2_1 = Vector2.zero;

	public Vector3 vector3_0 = Vector3.zero;

	public Vector3 vector3_1 = Vector3.zero;

	public Vector3 vector3_2 = Vector3.zero;

	public bool Boolean_0
	{
		get
		{
			return (UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_NINJA_JUMP) != 0) ? true : false;
		}
	}

	public float Single_0
	{
		get
		{
			return PlayerParamsSettings.Get.PlayerForwardSpeed * UserController.UserController_0.GetSpeedModifier();
		}
	}

	public float Single_1
	{
		get
		{
			return PlayerParamsSettings.Get.PlayerBackwardSpeed * UserController.UserController_0.GetSpeedModifier();
		}
	}

	public float Single_2
	{
		get
		{
			return PlayerParamsSettings.Get.PlayerSideStepSpeed * UserController.UserController_0.GetSpeedModifier();
		}
	}

	public float Single_3
	{
		get
		{
			return (!(PlayerParamsSettings.Get.ForwardAcc > 0f)) ? 0.001f : PlayerParamsSettings.Get.ForwardAcc;
		}
	}

	public float Single_4
	{
		get
		{
			return (!(PlayerParamsSettings.Get.ForwardDeacc > 0f)) ? 0.001f : PlayerParamsSettings.Get.ForwardDeacc;
		}
	}

	public float Single_5
	{
		get
		{
			return (!(PlayerParamsSettings.Get.BackwardAcc > 0f)) ? 0.001f : PlayerParamsSettings.Get.BackwardAcc;
		}
	}

	public float Single_6
	{
		get
		{
			return (!(PlayerParamsSettings.Get.BackwardDeacc > 0f)) ? 0.001f : PlayerParamsSettings.Get.BackwardDeacc;
		}
	}

	public float Single_7
	{
		get
		{
			return (!(PlayerParamsSettings.Get.SideAcc > 0f)) ? 0.001f : PlayerParamsSettings.Get.SideAcc;
		}
	}

	public float Single_8
	{
		get
		{
			return (!(PlayerParamsSettings.Get.SideDeacc > 0f)) ? 0.001f : PlayerParamsSettings.Get.SideDeacc;
		}
	}

	public FPProcessSharedData(FirstPersonPlayerController firstPersonPlayerController_1)
	{
		firstPersonPlayerController_0 = firstPersonPlayerController_1;
	}
}
