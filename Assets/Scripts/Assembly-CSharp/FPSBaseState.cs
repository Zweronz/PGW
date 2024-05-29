using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

internal abstract class FPSBaseState
{
	protected FPSStateController fpsstateController_0;

	protected float float_0 = 1f;

	protected float float_1 = 1f;

	protected List<Tween> list_0 = new List<Tween>();

	protected float float_2 = 0.25f;

	protected Vector3 vector3_0 = new Vector3(0f, -1f, 0f);

	[CompilerGenerated]
	private bool bool_0;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		protected set
		{
			bool_0 = value;
		}
	}

	public FPSBaseState(FPSStateController fpsstateController_1)
	{
		fpsstateController_0 = fpsstateController_1;
	}

	public virtual void Init()
	{
		fpsstateController_0.firstPersonPlayerController_0.Single_1 = float_0;
		if (!fpsstateController_0.bool_0)
		{
			if (fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.localScale.y != 1f)
			{
				list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOScaleY(1f, float_2));
			}
			if (fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.localRotation.eulerAngles.x != 0f)
			{
				list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), float_2));
				list_0.Add(fpsstateController_0.player_move_c_0.mySkinName.FPSplayerObject.transform.DOLocalMove(vector3_0, float_2));
			}
		}
	}

	public virtual void Release()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			list_0[i].Kill();
		}
		list_0.Clear();
	}

	public virtual void Update()
	{
	}
}
