using engine.operations;

public class WaitArtikulUserInvOperation : Operation
{
	private int int_0;

	public WaitArtikulUserInvOperation(int int_1)
	{
		int_0 = int_1;
	}

	protected override void Execute()
	{
		if (int_0 == 0)
		{
			Complete();
			return;
		}
		UserArtikul userArtikulByArtikulId = UserController.UserController_0.GetUserArtikulByArtikulId(int_0);
		if (userArtikulByArtikulId != null && userArtikulByArtikulId.int_1 != 0)
		{
			Complete();
		}
		else
		{
			UsersData.Subscribe(UsersData.EventType.ARTIKULS_CHANGED, OnChangeInventory);
		}
	}

	private void OnChangeInventory(UsersData.EventData eventData_0)
	{
		UserArtikul userArtikulByArtikulId = UserController.UserController_0.GetUserArtikulByArtikulId(int_0);
		if (userArtikulByArtikulId != null && userArtikulByArtikulId.int_1 != 0)
		{
			UsersData.Unsubscribe(UsersData.EventType.ARTIKULS_CHANGED, OnChangeInventory);
			Complete();
		}
	}
}
