using engine.events;
using engine.operations;

public class WaitFinalLastEnemyWaveOperation : Operation
{
	protected override void Execute()
	{
		DependSceneEvent<EventKillAllEnemyInWave>.GlobalSubscribe(OnFinalLastEnemyWave, true);
	}

	private void OnFinalLastEnemyWave()
	{
		DependSceneEvent<EventKillAllEnemyInWave>.GlobalUnsubscribe(OnFinalLastEnemyWave);
		Complete();
	}
}
