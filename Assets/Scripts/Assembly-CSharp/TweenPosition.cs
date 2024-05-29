using System;
using UnityEngine;

public class TweenPosition : UITweener
{
	public Vector3 vector3_0;

	public Vector3 vector3_1;

	public bool bool_3;

	private Transform transform_0;

	private UIRect uirect_0;

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
	public Vector3 Vector3_0
	{
		get
		{
			return Vector3_1;
		}
		set
		{
			Vector3_1 = value;
		}
	}

	public Vector3 Vector3_1
	{
		get
		{
			return (!bool_3) ? Transform_0.localPosition : Transform_0.position;
		}
		set
		{
			if (!(uirect_0 == null) && uirect_0.Boolean_1 && !bool_3)
			{
				value -= Transform_0.localPosition;
				NGUIMath.MoveRect(uirect_0, value.x, value.y);
			}
			else if (bool_3)
			{
				Transform_0.position = value;
			}
			else
			{
				Transform_0.localPosition = value;
			}
		}
	}

	private void Awake()
	{
		uirect_0 = GetComponent<UIRect>();
	}

	protected override void OnUpdate(float float_6, bool bool_4)
	{
		Vector3_1 = vector3_0 * (1f - float_6) + vector3_1 * float_6;
	}

	public static TweenPosition Begin(GameObject gameObject_1, float float_6, Vector3 vector3_2)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(gameObject_1, float_6);
		tweenPosition.vector3_0 = tweenPosition.Vector3_1;
		tweenPosition.vector3_1 = vector3_2;
		if (float_6 <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		vector3_0 = Vector3_1;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		vector3_1 = Vector3_1;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Vector3_1 = vector3_0;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Vector3_1 = vector3_1;
	}
}
