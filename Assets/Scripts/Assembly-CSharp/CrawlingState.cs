using DG.Tweening;
using UnityEngine;

internal sealed class CrawlingState : FPSBaseState
{
	private Vector3 vector3_1 = new Vector3(0f, -0.5f, -1f);

	public CrawlingState(FPSStateController fpsstateController_1)
		: base(fpsstateController_1)
	{
	}

	public override void Init()
	{
		base.Init();
		if (!fpsstateController_0.bool_0)
		{
			list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOLocalRotate(new Vector3(90f, 0f, 0f), float_2));
			list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOLocalMove(vector3_1, float_2));
		}
	}
}
