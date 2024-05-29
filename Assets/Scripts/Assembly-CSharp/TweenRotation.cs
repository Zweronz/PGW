using System;
using UnityEngine;

public class TweenRotation : UITweener
{
	public Vector3 vector3_0;

	public Vector3 vector3_1;

	private Transform transform_0;

	public Transform Transform_0
	{
		get
		{
			if (transform_0 == null)
			{
				transform_0 = base.transform;
			}
			return transform_0;
		}
	}

	[Obsolete("Use 'value' instead")]
	public Quaternion Quaternion_0
	{
		get
		{
			return Quaternion_1;
		}
		set
		{
			Quaternion_1 = value;
		}
	}

	public Quaternion Quaternion_1
	{
		get
		{
			return Transform_0.localRotation;
		}
		set
		{
			Transform_0.localRotation = value;
		}
	}

	protected override void OnUpdate(float float_6, bool bool_3)
	{
		Quaternion_1 = Quaternion.Euler(new Vector3(Mathf.Lerp(vector3_0.x, vector3_1.x, float_6), Mathf.Lerp(vector3_0.y, vector3_1.y, float_6), Mathf.Lerp(vector3_0.z, vector3_1.z, float_6)));
	}

	public static TweenRotation Begin(GameObject gameObject_1, float float_6, Quaternion quaternion_0)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(gameObject_1, float_6);
		tweenRotation.vector3_0 = tweenRotation.Quaternion_1.eulerAngles;
		tweenRotation.vector3_1 = quaternion_0.eulerAngles;
		if (float_6 <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		vector3_0 = Quaternion_1.eulerAngles;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		vector3_1 = Quaternion_1.eulerAngles;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Quaternion_1 = Quaternion.Euler(vector3_0);
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Quaternion_1 = Quaternion.Euler(vector3_1);
	}
}
