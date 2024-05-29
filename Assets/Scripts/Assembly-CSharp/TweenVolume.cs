using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TweenVolume : UITweener
{
	[Range(0f, 1f)]
	public float float_6 = 1f;

	[Range(0f, 1f)]
	public float float_7 = 1f;

	private AudioSource audioSource_0;

	public AudioSource AudioSource_0
	{
		get
		{
			if (audioSource_0 == null)
			{
				audioSource_0 = base.GetComponent<AudioSource>();
				if (audioSource_0 == null)
				{
					audioSource_0 = GetComponent<AudioSource>();
					if (audioSource_0 == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return audioSource_0;
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
			return (!(AudioSource_0 != null)) ? 0f : audioSource_0.volume;
		}
		set
		{
			if (AudioSource_0 != null)
			{
				audioSource_0.volume = value;
			}
		}
	}

	protected override void OnUpdate(float float_8, bool bool_3)
	{
		Single_3 = float_6 * (1f - float_8) + float_7 * float_8;
		audioSource_0.enabled = audioSource_0.volume > 0.01f;
	}

	public static TweenVolume Begin(GameObject gameObject_1, float float_8, float float_9)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(gameObject_1, float_8);
		tweenVolume.float_6 = tweenVolume.Single_3;
		tweenVolume.float_7 = float_9;
		return tweenVolume;
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
