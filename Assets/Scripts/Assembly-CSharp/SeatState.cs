using DG.Tweening;

internal sealed class SeatState : FPSBaseState
{
	public SeatState(FPSStateController fpsstateController_1)
		: base(fpsstateController_1)
	{
	}

	public override void Init()
	{
		base.Init();
		if (!fpsstateController_0.bool_0)
		{
			list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOScaleY(0.5f, float_2));
		}
	}
}
