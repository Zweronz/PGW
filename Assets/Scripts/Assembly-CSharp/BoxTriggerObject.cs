using engine.helpers;

public class BoxTriggerObject : TriggerObject
{
	public SlotType slotType_0 = SlotType.SLOT_WEAPON_MELEE;

	public override void OnEnter(FirstPersonPlayerController firstPersonPlayerController_0)
	{
		Log.AddLine("BoxTriggerObject::OnEnter > name: " + base.gameObject.name);
		firstPersonPlayerController_0.Player_move_c_0.ChangeWeapon(slotType_0, false);
	}

	public override void OnExit(FirstPersonPlayerController firstPersonPlayerController_0)
	{
		Log.AddLine("BoxTriggerObject::OnExit > name: " + base.gameObject.name);
		firstPersonPlayerController_0.Player_move_c_0.ChangePreviousWeapon();
	}
}
