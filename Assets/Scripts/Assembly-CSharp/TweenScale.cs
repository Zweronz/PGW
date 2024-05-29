using System;
using UnityEngine;

public class TweenScale : UITweener
{
	public Vector3 vector3_0 = Vector3.one;

	public Vector3 vector3_1 = Vector3.one;

	public bool bool_3;

	private Transform transform_0;

	private UITable uitable_0;

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

	public Vector3 Vector3_0
	{
		get
		{
			return Transform_0.localScale;
		}
		set
		{
			Transform_0.localScale = value;
		}
	}

	[Obsolete("Use 'value' instead")]
	public Vector3 Vector3_1
	{
		get
		{
			return Vector3_0;
		}
		set
		{
			Vector3_0 = value;
		}
	}

	protected override void OnUpdate(float float_6, bool bool_4)
	{
		Vector3_0 = vector3_0 * (1f - float_6) + vector3_1 * float_6;
		if (!bool_3)
		{
			return;
		}
		if (uitable_0 == null)
		{
			uitable_0 = NGUITools.FindInParents<UITable>(base.gameObject);
			if (uitable_0 == null)
			{
				bool_3 = false;
				return;
			}
		}
		uitable_0.Boolean_0 = true;
	}

	public static TweenScale Begin(GameObject gameObject_1, float float_6, Vector3 vector3_2)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(gameObject_1, float_6);
		tweenScale.vector3_0 = tweenScale.Vector3_0;
		tweenScale.vector3_1 = vector3_2;
		if (float_6 <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		vector3_0 = Vector3_0;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		vector3_1 = Vector3_0;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Vector3_0 = vector3_0;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Vector3_0 = vector3_1;
	}
}
