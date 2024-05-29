using engine.events;
using engine.operations;

public sealed class WaitTakeCoinBonusOperation : Operation
{
	protected override void Execute()
	{
		DependSceneEvent<EventTakenCoinBonus>.GlobalSubscribe(OnCoinBonusTaken, true);
	}

	private void OnCoinBonusTaken()
	{
		DependSceneEvent<EventTakenCoinBonus>.GlobalUnsubscribe(OnCoinBonusTaken);
		Complete();
	}
}
