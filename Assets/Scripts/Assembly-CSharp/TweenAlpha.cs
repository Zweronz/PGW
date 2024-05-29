using System;
using UnityEngine;

public class TweenAlpha : UITweener
{
	[Range(0f, 1f)]
	public float float_6 = 1f;

	[Range(0f, 1f)]
	public float float_7 = 1f;

	private UIRect uirect_0;

	public UIRect UIRect_0
	{
		get
		{
			if (uirect_0 == null)
			{
				uirect_0 = GetComponent<UIRect>();
				if (uirect_0 == null)
				{
					uirect_0 = GetComponentInChildren<UIRect>();
				}
			}
			return uirect_0;
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
			return UIRect_0.Single_2;
		}
		set
		{
			UIRect_0.Single_2 = value;
		}
	}

	protected override void OnUpdate(float float_8, bool bool_3)
	{
		Single_3 = Mathf.Lerp(float_6, float_7, float_8);
	}

	public static TweenAlpha Begin(GameObject gameObject_1, float float_8, float float_9)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(gameObject_1, float_8);
		tweenAlpha.float_6 = tweenAlpha.Single_3;
		tweenAlpha.float_7 = float_9;
		if (float_8 <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
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
