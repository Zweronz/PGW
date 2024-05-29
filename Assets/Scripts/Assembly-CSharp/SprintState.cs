using DG.Tweening;

internal sealed class SprintState : FPSBaseState
{
	public SprintState(FPSStateController fpsstateController_1)
		: base(fpsstateController_1)
	{
		float_0 = 1.4f;
		float_1 = 0f;
	}

	public override void Init()
	{
		base.Init();
		fpsstateController_0.player_move_c_0.PlayerStateController_0.Subscribe(StopSprintState, PlayerEvents.StartShoot);
		if (float_1 != 0f)
		{
			list_0.Add(DOTween.Sequence().AppendInterval(float_1).AppendCallback(StopSprintState));
		}
	}

	public override void Release()
	{
		base.Release();
		fpsstateController_0.player_move_c_0.PlayerStateController_0.Unsubscribe(StopSprintState, PlayerEvents.StartShoot);
	}

	private void StopSprintState()
	{
		fpsstateController_0.SetState(FPSStateController.STATES.DEFAULT);
	}
}
