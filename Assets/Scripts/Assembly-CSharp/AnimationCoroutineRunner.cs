using System;
using System.Collections;
using UnityEngine;

public class AnimationCoroutineRunner : MonoBehaviour
{
	public void StartPlay(Animation animation_0, string string_0, bool bool_0, Action action_0)
	{
		StartCoroutine(Play(animation_0, string_0, bool_0, action_0));
	}

	public IEnumerator Play(Animation animation_0, string string_0, bool bool_0, Action action_0)
	{
		Debug.Log("Overwritten Play animation, useTimeScale? " + bool_0);
		if (!bool_0)
		{
			Debug.Log("Started this animation! ( " + string_0 + " ) ");
			AnimationState animationState = animation_0[string_0];
			bool flag = true;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			animation_0.Play(string_0);
			num2 = Time.realtimeSinceStartup;
			while (flag)
			{
				num3 = Time.realtimeSinceStartup;
				num4 = num3 - num2;
				num2 = num3;
				num += num4;
				animationState.normalizedTime = num / animationState.length;
				animation_0.Sample();
				if (num >= animationState.length)
				{
					if (animationState.wrapMode != WrapMode.Loop)
					{
						flag = false;
					}
					else
					{
						num = 0f;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			yield return null;
			if (action_0 != null)
			{
				Debug.Log("Start onComplete");
				action_0();
			}
		}
		else
		{
			animation_0.Play(string_0);
		}
	}
}
