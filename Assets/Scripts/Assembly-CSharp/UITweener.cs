using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public abstract class UITweener : MonoBehaviour
{
	public enum Method
	{
		Linear = 0,
		EaseIn = 1,
		EaseOut = 2,
		EaseInOut = 3,
		BounceIn = 4,
		BounceOut = 5
	}

	public enum Style
	{
		Once = 0,
		Loop = 1,
		PingPong = 2
	}

	public static UITweener uitweener_0;

	public Method method_0;

	public Style style_0;

	public AnimationCurve animationCurve_0 = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	public bool bool_0 = true;

	public float float_0;

	public float float_1 = 1f;

	public bool bool_1;

	public int int_0;

	public List<EventDelegate> list_0 = new List<EventDelegate>();

	public GameObject gameObject_0;

	public string string_0;

	private bool bool_2;

	private float float_2;

	private float float_3;

	private float float_4 = 1000f;

	private float float_5;

	private List<EventDelegate> list_1;

	public float Single_0
	{
		get
		{
			if (float_3 != float_1)
			{
				float_3 = float_1;
				float_4 = Mathf.Abs((!(float_1 > 0f)) ? 1000f : (1f / float_1));
			}
			return float_4;
		}
	}

	public float Single_1
	{
		get
		{
			return float_5;
		}
		set
		{
			float_5 = Mathf.Clamp01(value);
		}
	}

	public Direction Direction_0
	{
		get
		{
			return (!(float_4 < 0f)) ? Direction.Forward : Direction.Reverse;
		}
	}

	private void Reset()
	{
		if (!bool_2)
		{
			SetStartToCurrentValue();
			SetEndToCurrentValue();
		}
	}

	protected virtual void Start()
	{
		Update();
	}

	private void Update()
	{
		float num = ((!bool_0) ? Time.deltaTime : RealTime.Single_1);
		float num2 = ((!bool_0) ? Time.time : RealTime.Single_0);
		if (!bool_2)
		{
			bool_2 = true;
			float_2 = num2 + float_0;
		}
		if (num2 < float_2)
		{
			return;
		}
		float_5 += Single_0 * num;
		if (style_0 == Style.Loop)
		{
			if (float_5 > 1f)
			{
				float_5 -= Mathf.Floor(float_5);
			}
		}
		else if (style_0 == Style.PingPong)
		{
			if (float_5 > 1f)
			{
				float_5 = 1f - (float_5 - Mathf.Floor(float_5));
				float_4 = 0f - float_4;
			}
			else if (float_5 < 0f)
			{
				float_5 = 0f - float_5;
				float_5 -= Mathf.Floor(float_5);
				float_4 = 0f - float_4;
			}
		}
		if (style_0 == Style.Once && (float_1 == 0f || float_5 > 1f || float_5 < 0f))
		{
			float_5 = Mathf.Clamp01(float_5);
			Sample(float_5, true);
			if (float_1 == 0f || (float_5 == 1f && float_4 > 0f) || (float_5 == 0f && float_4 < 0f))
			{
				base.enabled = false;
			}
			if (!(uitweener_0 == null))
			{
				return;
			}
			uitweener_0 = this;
			if (list_0 != null)
			{
				list_1 = list_0;
				list_0 = new List<EventDelegate>();
				EventDelegate.Execute(list_1);
				for (int i = 0; i < list_1.Count; i++)
				{
					EventDelegate eventDelegate = list_1[i];
					if (eventDelegate != null)
					{
						EventDelegate.Add(list_0, eventDelegate, eventDelegate.oneShot);
					}
				}
				list_1 = null;
			}
			if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
			{
				gameObject_0.SendMessage(string_0, this, SendMessageOptions.DontRequireReceiver);
			}
			uitweener_0 = null;
		}
		else
		{
			Sample(float_5, false);
		}
	}

	public void SetOnFinished(EventDelegate.Callback callback_0)
	{
		EventDelegate.Set(list_0, callback_0);
	}

	public void SetOnFinished(EventDelegate eventDelegate_0)
	{
		EventDelegate.Set(list_0, eventDelegate_0);
	}

	public void AddOnFinished(EventDelegate.Callback callback_0)
	{
		EventDelegate.Add(list_0, callback_0);
	}

	public void AddOnFinished(EventDelegate eventDelegate_0)
	{
		EventDelegate.Add(list_0, eventDelegate_0);
	}

	public void RemoveOnFinished(EventDelegate eventDelegate_0)
	{
		if (list_0 != null)
		{
			list_0.Remove(eventDelegate_0);
		}
		if (list_1 != null)
		{
			list_1.Remove(eventDelegate_0);
		}
	}

	private void OnDisable()
	{
		bool_2 = false;
	}

	public void Sample(float float_6, bool bool_3)
	{
		float num = Mathf.Clamp01(float_6);
		if (method_0 == Method.EaseIn)
		{
			num = 1f - Mathf.Sin((float)Math.PI / 2f * (1f - num));
			if (bool_1)
			{
				num *= num;
			}
		}
		else if (method_0 == Method.EaseOut)
		{
			num = Mathf.Sin((float)Math.PI / 2f * num);
			if (bool_1)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (method_0 == Method.EaseInOut)
		{
			num -= Mathf.Sin(num * ((float)Math.PI * 2f)) / ((float)Math.PI * 2f);
			if (bool_1)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (method_0 == Method.BounceIn)
		{
			num = BounceLogic(num);
		}
		else if (method_0 == Method.BounceOut)
		{
			num = 1f - BounceLogic(1f - num);
		}
		OnUpdate((animationCurve_0 == null) ? num : animationCurve_0.Evaluate(num), bool_3);
	}

	private float BounceLogic(float float_6)
	{
		float_6 = ((float_6 < 0.363636f) ? (7.5685f * float_6 * float_6) : ((float_6 < 0.727272f) ? (7.5625f * (float_6 -= 0.545454f) * float_6 + 0.75f) : ((!(float_6 < 0.90909f)) ? (7.5625f * (float_6 -= 0.9545454f) * float_6 + 63f / 64f) : (7.5625f * (float_6 -= 0.818181f) * float_6 + 0.9375f))));
		return float_6;
	}

	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		Play(true);
	}

	public void PlayForward()
	{
		Play(true);
	}

	public void PlayReverse()
	{
		Play(false);
	}

	public void Play(bool bool_3)
	{
		float_4 = Mathf.Abs(Single_0);
		if (!bool_3)
		{
			float_4 = 0f - float_4;
		}
		base.enabled = true;
		Update();
	}

	public void ResetToBeginning()
	{
		bool_2 = false;
		float_5 = ((!(float_4 < 0f)) ? 0f : 1f);
		Sample(float_5, false);
	}

	public void Toggle()
	{
		if (float_5 > 0f)
		{
			float_4 = 0f - Single_0;
		}
		else
		{
			float_4 = Mathf.Abs(Single_0);
		}
		base.enabled = true;
	}

	protected abstract void OnUpdate(float float_6, bool bool_3);

	public static T Begin<T>(GameObject gameObject_1, float float_6) where T : UITweener
	{
		T val = gameObject_1.GetComponent<T>();
		if ((UnityEngine.Object)val != (UnityEngine.Object)null && val.int_0 != 0)
		{
			val = (T)null;
			T[] components = gameObject_1.GetComponents<T>();
			int i = 0;
			for (int num = components.Length; i < num; i++)
			{
				val = components[i];
				if ((UnityEngine.Object)val != (UnityEngine.Object)null && val.int_0 == 0)
				{
					break;
				}
				val = (T)null;
			}
		}
		if ((UnityEngine.Object)val == (UnityEngine.Object)null)
		{
			val = gameObject_1.AddComponent<T>();
		}
		val.bool_2 = false;
		val.float_1 = float_6;
		val.float_5 = 0f;
		val.float_4 = Mathf.Abs(val.float_4);
		val.style_0 = Style.Once;
		val.animationCurve_0 = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
		val.gameObject_0 = null;
		val.string_0 = null;
		val.enabled = true;
		if (float_6 <= 0f)
		{
			val.Sample(1f, true);
			val.enabled = false;
		}
		return val;
	}

	public virtual void SetStartToCurrentValue()
	{
	}

	public virtual void SetEndToCurrentValue()
	{
	}
}
