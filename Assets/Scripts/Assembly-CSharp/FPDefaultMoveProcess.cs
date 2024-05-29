using UnityEngine;

public sealed class FPDefaultMoveProcess : FPBaseProcess
{
	private FPProcessSharedData fpprocessSharedData_0;

	public Vector3 vector3_0 = Vector3.zero;

	private Vector3 vector3_1 = Vector3.zero;

	private float float_0;

	private float float_1;

	private Vector3 vector3_2 = Vector3.zero;

	public override void Update(FPProcessSharedData fpprocessSharedData_1)
	{
		fpprocessSharedData_0 = fpprocessSharedData_1;
		UpdateMovement();
	}

	private void UpdateMovement()
	{
		if (fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.enabled)
		{
			PreProcessMovement();
			newMovementLogic();
			PostProcessMovement();
			ProcessMovement();
		}
	}

	private void PreProcessMovement()
	{
		FPSStateController fPSStateController_ = fpprocessSharedData_0.firstPersonPlayerController_0.FPSStateController_0;
		if (fPSStateController_.STATES_0 == FPSStateController.STATES.STAIRS)
		{
			vector3_0 = fpprocessSharedData_0.vector3_2;
			vector3_0.x = 0f;
			if (fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.isGrounded && fpprocessSharedData_0.vector2_0.y < 0f)
			{
				fPSStateController_.SetState(FPSStateController.STATES.DEFAULT);
			}
			Transform cameraPivot = fpprocessSharedData_0.firstPersonPlayerController_0.cameraPivot;
			if ((cameraPivot.localRotation.w < 0f && cameraPivot.localRotation.x <= fpprocessSharedData_0.firstPersonPlayerController_0.angleFPSResverse * -1f) || (cameraPivot.localRotation.w > 0f && cameraPivot.localRotation.x >= fpprocessSharedData_0.firstPersonPlayerController_0.angleFPSResverse))
			{
				fpprocessSharedData_0.vector2_0.y *= -1f;
			}
			if (fpprocessSharedData_0.firstPersonPlayerController_0.stairsTransform != null && Mathf.Approximately(float_0, fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.transform.position.y) && fpprocessSharedData_0.vector2_0.y != 0f && float_1 == fpprocessSharedData_0.vector2_0.y)
			{
				float f = fpprocessSharedData_0.firstPersonPlayerController_0.stairsTransform.position.x - fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.transform.position.x;
				fpprocessSharedData_0.vector2_0.x = -1f * Mathf.Sign(f);
				float_1 = 0f;
			}
			else
			{
				float_1 = fpprocessSharedData_0.vector2_0.y;
			}
			float_0 = fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.transform.position.y;
			vector3_0.x += fpprocessSharedData_0.vector2_0.x;
			vector3_0.y *= Mathf.Sign(fpprocessSharedData_0.vector2_0.y);
			vector3_0.z *= Mathf.Sign(fpprocessSharedData_0.vector2_0.y);
			vector3_1 = vector3_0;
			fpprocessSharedData_0.vector3_0 = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.TransformDirection(vector3_1);
			fpprocessSharedData_0.vector3_1.y = 0f;
		}
		else
		{
			vector3_1.x = fpprocessSharedData_0.vector2_0.x;
			vector3_1.y = 0f;
			vector3_1.z = fpprocessSharedData_0.vector2_0.y;
			fpprocessSharedData_0.vector3_0 = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.TransformDirection(vector3_1);
			fpprocessSharedData_0.vector3_0.y = 0f;
		}
		fpprocessSharedData_0.vector3_0.Normalize();
	}

	private void newMovementLogic()
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		float single_ = fpprocessSharedData_0.Single_3;
		float single_2 = fpprocessSharedData_0.Single_0;
		float single_3 = fpprocessSharedData_0.Single_5;
		float single_4 = fpprocessSharedData_0.Single_1;
		float single_5 = fpprocessSharedData_0.Single_7;
		float single_6 = fpprocessSharedData_0.Single_2;
		float single_7 = fpprocessSharedData_0.Single_4;
		float single_8 = fpprocessSharedData_0.Single_6;
		float single_9 = fpprocessSharedData_0.Single_8;
		if (fpprocessSharedData_0.vector2_0.y > 0f)
		{
			vector += new Vector3(0f, 0f, Time.deltaTime / single_) * single_2;
		}
		if (fpprocessSharedData_0.vector2_0.y < 0f)
		{
			vector += new Vector3(0f, 0f, (0f - Time.deltaTime) / single_3) * single_4;
		}
		if (fpprocessSharedData_0.vector2_0.x > 0f)
		{
			vector += new Vector3(Time.deltaTime / single_5, 0f, 0f) * single_6;
		}
		if (fpprocessSharedData_0.vector2_0.x < 0f)
		{
			vector += new Vector3((0f - Time.deltaTime) / single_5, 0f, 0f) * single_6;
		}
		if (vector3_2.z > 0f && fpprocessSharedData_0.vector2_0.y <= 0f)
		{
			vector += new Vector3(0f, 0f, (0f - Time.deltaTime) / single_7) * single_2;
			if (vector3_2.z + vector.z < 0f)
			{
				vector.z = 0f - vector3_2.z;
			}
		}
		if (vector3_2.z < 0f && fpprocessSharedData_0.vector2_0.y >= 0f)
		{
			vector += new Vector3(0f, 0f, Time.deltaTime / single_8) * single_4;
			if (vector3_2.z + vector.z > 0f)
			{
				vector.z = 0f - vector3_2.z;
			}
		}
		if (vector3_2.x > 0f && fpprocessSharedData_0.vector2_0.x <= 0f)
		{
			vector += new Vector3((0f - Time.deltaTime) / single_9, 0f, 0f) * single_6;
			if (vector3_2.x + vector.x < 0f)
			{
				vector.x = 0f - vector3_2.x;
			}
		}
		if (vector3_2.x < 0f && fpprocessSharedData_0.vector2_0.x >= 0f)
		{
			vector += new Vector3(Time.deltaTime / single_9, 0f, 0f) * single_6;
			if (vector3_2.x + vector.x > 0f)
			{
				vector.x = 0f - vector3_2.x;
			}
		}
		vector3_2 += vector;
		if (vector3_2.z > single_2)
		{
			vector3_2.z = single_2;
		}
		else if (vector3_2.z < 0f - single_4)
		{
			vector3_2.z = 0f - single_4;
		}
		if (vector3_2.x > single_6)
		{
			vector3_2.x = single_6;
		}
		else if (vector3_2.x < 0f - single_6)
		{
			vector3_2.x = 0f - single_6;
		}
		Vector3 direction = vector3_2;
		if (fpprocessSharedData_0.vector2_0.x != 0f && fpprocessSharedData_0.vector2_0.y != 0f)
		{
			float num = 0f;
			num = ((!(fpprocessSharedData_0.vector2_0.y > 0f)) ? Mathf.Max(single_6, single_4) : Mathf.Max(single_6, single_2));
			float num2 = vector3_2.x * vector3_2.x + vector3_2.z * vector3_2.z;
			float num3 = num * num;
			if (num2 > num3)
			{
				direction = new Vector3(vector3_2.x, vector3_2.y, vector3_2.z);
				float num4 = Mathf.Sqrt(num3) / Mathf.Sqrt(num2);
				direction.z = num4 * vector3_2.z;
				direction.x = num4 * vector3_2.x;
			}
		}
		if (fpprocessSharedData_0.firstPersonPlayerController_0.FPSStateController_0.STATES_0 != FPSStateController.STATES.STAIRS)
		{
			fpprocessSharedData_0.vector3_0 = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.TransformDirection(direction);
			fpprocessSharedData_0.vector3_0.y = 0f;
		}
		else
		{
			direction = new Vector3(vector3_2.x * Mathf.Abs(vector3_1.x), vector3_2.z * Mathf.Abs(vector3_1.y), vector3_2.z * Mathf.Abs(vector3_1.z));
			fpprocessSharedData_0.vector3_0 = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.TransformDirection(direction);
		}
	}

	private void PostProcessMovement()
	{
		fpprocessSharedData_0.vector3_0 += fpprocessSharedData_0.vector3_1;
		fpprocessSharedData_0.vector3_0 += ((fpprocessSharedData_0.firstPersonPlayerController_0.FPSStateController_0.STATES_0 != FPSStateController.STATES.STAIRS) ? (Physics.gravity * fpprocessSharedData_0.firstPersonPlayerController_0.Single_0) : Vector3.zero);
		fpprocessSharedData_0.vector3_0 *= Time.deltaTime * fpprocessSharedData_0.firstPersonPlayerController_0.Single_1;
	}

	private void ProcessMovement()
	{
		fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.Move(fpprocessSharedData_0.vector3_0);
		fpprocessSharedData_0.vector3_0 = Vector2.zero;
		if (fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.isGrounded)
		{
			fpprocessSharedData_0.vector3_1 = Vector3.zero;
		}
	}
}
