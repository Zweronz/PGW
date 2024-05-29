using UnityEngine;

public sealed class FPHookMoveProcess : FPBaseProcess
{
	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		Vector3 position = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.position;
		Vector3 vector = fpprocessSharedData_0.firstPersonPlayerController_0.Vector3_0 - position;
		if ((double)vector.magnitude < 0.3)
		{
			fpprocessSharedData_0.firstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Default);
			return;
		}
		float num = 100f;
		Vector3 vector2 = vector.normalized * num * Time.deltaTime;
		if (vector2.magnitude > vector.magnitude)
		{
			vector2 = vector;
		}
		fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.Move(vector2);
		Vector3 vector3 = fpprocessSharedData_0.firstPersonPlayerController_0.Transform_0.position - position;
		if (fpprocessSharedData_0.firstPersonPlayerController_0.CharacterController_0.isGrounded)
		{
			if (vector2.y < 0f)
			{
				vector2.y = 0f;
			}
			if (vector3.y < 0f)
			{
				vector3.y = 0f;
			}
		}
		Vector3 vector4 = vector3 - vector2;
		if (vector2.magnitude != 0f && vector3.magnitude != 0f)
		{
			if ((double)(vector4.magnitude / vector2.magnitude) > 0.2)
			{
				fpprocessSharedData_0.firstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Default);
			}
		}
		else
		{
			fpprocessSharedData_0.firstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Default);
		}
	}
}
