using UnityEngine;
using engine.helpers;

public class StairsTriggerObject : TriggerObject
{
	private BoxCollider boxCollider_0;

	private bool bool_0;

	public float float_0 = 0.3f;

	private void Start()
	{
		boxCollider_0 = GetComponent<BoxCollider>();
	}

	public override void OnEnter(FirstPersonPlayerController firstPersonPlayerController_0)
	{
		Log.AddLine("StairsTriggerObject::OnEnter");
		firstPersonPlayerController_0.angleFPSResverse = float_0;
		firstPersonPlayerController_0.stairsTransform = boxCollider_0.transform;
		firstPersonPlayerController_0.SetStairsMovement(boxCollider_0.transform.TransformDirection(boxCollider_0.transform.up));
		firstPersonPlayerController_0.FPSStateController_0.SetState(FPSStateController.STATES.STAIRS);
		bool_0 = true;
	}

	public override void OnExit(FirstPersonPlayerController firstPersonPlayerController_0)
	{
		Log.AddLine("StairsTriggerObject::OnExit");
		firstPersonPlayerController_0.SetStairsMovement(Vector3.zero);
		firstPersonPlayerController_0.stairsTransform = null;
		firstPersonPlayerController_0.FPSStateController_0.SetState(FPSStateController.STATES.DEFAULT);
		bool_0 = false;
	}
}
