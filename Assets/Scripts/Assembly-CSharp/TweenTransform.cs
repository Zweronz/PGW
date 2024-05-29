using UnityEngine;

public class TweenTransform : UITweener
{
	public Transform transform_0;

	public Transform transform_1;

	public bool bool_3;

	private Transform transform_2;

	private Vector3 vector3_0;

	private Quaternion quaternion_0;

	private Vector3 vector3_1;

	protected override void OnUpdate(float float_6, bool bool_4)
	{
		if (transform_1 != null)
		{
			if (transform_2 == null)
			{
				transform_2 = base.transform;
				vector3_0 = transform_2.position;
				quaternion_0 = transform_2.rotation;
				vector3_1 = transform_2.localScale;
			}
			if (transform_0 != null)
			{
				transform_2.position = transform_0.position * (1f - float_6) + transform_1.position * float_6;
				transform_2.localScale = transform_0.localScale * (1f - float_6) + transform_1.localScale * float_6;
				transform_2.rotation = Quaternion.Slerp(transform_0.rotation, transform_1.rotation, float_6);
			}
			else
			{
				transform_2.position = vector3_0 * (1f - float_6) + transform_1.position * float_6;
				transform_2.localScale = vector3_1 * (1f - float_6) + transform_1.localScale * float_6;
				transform_2.rotation = Quaternion.Slerp(quaternion_0, transform_1.rotation, float_6);
			}
			if (bool_3 && bool_4)
			{
				transform_2.parent = transform_1;
			}
		}
	}

	public static TweenTransform Begin(GameObject gameObject_1, float float_6, Transform transform_3)
	{
		return Begin(gameObject_1, float_6, null, transform_3);
	}

	public static TweenTransform Begin(GameObject gameObject_1, float float_6, Transform transform_3, Transform transform_4)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(gameObject_1, float_6);
		tweenTransform.transform_0 = transform_3;
		tweenTransform.transform_1 = transform_4;
		if (float_6 <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}
}
