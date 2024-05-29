using engine.events;
using engine.operations;

public sealed class WaitTakeWeaponBonusOperation : Operation
{
	private WeaponData weaponData_0;

	public WaitTakeWeaponBonusOperation(int int_0)
	{
		weaponData_0 = WeaponController.WeaponController_0.GetWeapon(int_0);
	}

	protected override void Execute()
	{
		if (weaponData_0 == null)
		{
			Complete();
		}
		else
		{
			DependSceneEvent<EventTakenWeaponBonus, int>.GlobalSubscribe(OnWeaponBonusTaken, true);
		}
	}

	private void OnWeaponBonusTaken(int int_0)
	{
		if (int_0 == weaponData_0.Int32_0)
		{
			DependSceneEvent<EventTakenWeaponBonus, int>.GlobalUnsubscribe(OnWeaponBonusTaken);
			Complete();
		}
	}
}
