using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
	public float float_6 = 45f;

	public float float_7 = 45f;

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
			return Camera_0.fieldOfView;
		}
		set
		{
			Camera_0.fieldOfView = value;
		}
	}

	protected override void OnUpdate(float float_8, bool bool_3)
	{
		Single_3 = float_6 * (1f - float_8) + float_7 * float_8;
	}

	public static TweenFOV Begin(GameObject gameObject_1, float float_8, float float_9)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(gameObject_1, float_8);
		tweenFOV.float_6 = tweenFOV.Single_3;
		tweenFOV.float_7 = float_9;
		if (float_8 <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		float_6 = Single_3;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		float_7 = Single_3;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Single_3 = float_6;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Single_3 = float_7;
	}
}
