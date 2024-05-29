using engine.controllers;
using engine.unity;

public sealed class FightReconnectController
{
	private enum State
	{
		None = 0,
		NeedReconnect = 1
	}

	private static FightReconnectController fightReconnectController_0;

	private State state_0;

	public static FightReconnectController FightReconnectController_0
	{
		get
		{
			return fightReconnectController_0 ?? (fightReconnectController_0 = new FightReconnectController());
		}
	}

	private FightReconnectController()
	{
		AppStateController.AppStateController_0.Subscribe(OnMainMenu, AppStateController.States.MAIN_MENU);
	}

	public void SetNeedReconnect()
	{
		state_0 = State.NeedReconnect;
		if (AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU)
		{
			OnMainMenu();
		}
	}

	private void OnMainMenu()
	{
		if (state_0 != 0)
		{
			state_0 = State.None;
			MonoSingleton<FightController>.Prop_0.Disconnect(true);
		}
	}
}
