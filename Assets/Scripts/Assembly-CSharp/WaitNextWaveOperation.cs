using engine.events;
using engine.operations;

public sealed class WaitNextWaveOperation : Operation
{
	protected override void Execute()
	{
		if (!MonstersController.Boolean_0)
		{
			Complete();
		}
		else
		{
			DependSceneEvent<EventNextEnemyWave, ZombieCreator.StateNextWaveType>.GlobalSubscribe(OnStartNextWave, true);
		}
	}

	public void OnStartNextWave(ZombieCreator.StateNextWaveType stateNextWaveType_0)
	{
		DependSceneEvent<EventNextEnemyWave, ZombieCreator.StateNextWaveType>.GlobalUnsubscribe(OnStartNextWave);
		Complete();
	}
}
