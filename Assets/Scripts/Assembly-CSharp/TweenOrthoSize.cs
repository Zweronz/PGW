using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
	public float float_6 = 1f;

	public float float_7 = 1f;

	private Camera camera_0;

	public Camera Camera_0
	{
		get
		{
			if (camera_0 == null)
			{
				camera_0 = base.GetComponent<Camera>();
			}
			return camera_0;
		}
	}

	[Obsolete("Use 'value' instead")]
	public float Single_2
	{
		get
		{
			return Single_3;
		}
		set
		{
			Single_3 = value;
		}
	}

	public float Single_3
	{
		get
		{
			return Camera_0.orthographicSize;
		}
		set
		{
			Camera_0.orthographicSize = value;
		}
	}

	protected override void OnUpdate(float float_8, bool bool_3)
	{
		Single_3 = float_6 * (1f - float_8) + float_7 * float_8;
	}

	public static TweenOrthoSize Begin(GameObject gameObject_1, float float_8, float float_9)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(gameObject_1, float_8);
		tweenOrthoSize.float_6 = tweenOrthoSize.Single_3;
		tweenOrthoSize.float_7 = float_9;
		if (float_8 <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	public override void SetStartToCurrentValue()
	{
		float_6 = Single_3;
	}

	public override void SetEndToCurrentValue()
	{
		float_7 = Single_3;
	}
}
