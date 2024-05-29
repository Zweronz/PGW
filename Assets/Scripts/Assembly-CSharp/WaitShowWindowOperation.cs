using engine.operations;
using engine.unity;

public class WaitShowWindowOperation : Operation
{
	private GameWindowType gameWindowType_0 = GameWindowType.None;

	private bool bool_6;

	public WaitShowWindowOperation(GameWindowType gameWindowType_1, bool bool_7)
	{
		gameWindowType_0 = gameWindowType_1;
		bool_6 = bool_7;
	}

	protected override void Execute()
	{
		if (gameWindowType_0 == GameWindowType.None)
		{
			Complete();
		}
		else
		{
			BaseGameWindow.Subscribe(gameWindowType_0, OnShopOpen);
		}
	}

	private void OnShopOpen(GameWindowEventArg gameWindowEventArg_0)
	{
		if (gameWindowEventArg_0.Boolean_0 == bool_6)
		{
			BaseGameWindow.Unsubscribe(gameWindowType_0, OnShopOpen);
			Complete();
		}
	}
}
