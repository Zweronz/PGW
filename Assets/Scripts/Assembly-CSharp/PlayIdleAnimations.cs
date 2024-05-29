using System.Collections.Generic;
using UnityEngine;

public class PlayIdleAnimations : MonoBehaviour
{
	private Animation animation_0;

	private AnimationClip animationClip_0;

	private List<AnimationClip> list_0 = new List<AnimationClip>();

	private float float_0;

	private int int_0;

	private void Start()
	{
		animation_0 = GetComponentInChildren<Animation>();
		if (animation_0 == null)
		{
			Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has no Animation component");
			Object.Destroy(this);
			return;
		}
		foreach (AnimationState item in animation_0)
		{
			if (item.clip.name == "idle")
			{
				item.layer = 0;
				animationClip_0 = item.clip;
				animation_0.Play(animationClip_0.name);
			}
			else if (item.clip.name.StartsWith("idle"))
			{
				item.layer = 1;
				list_0.Add(item.clip);
			}
		}
		if (list_0.Count == 0)
		{
			Object.Destroy(this);
		}
	}

	private void Update()
	{
		if (!(float_0 < Time.time))
		{
			return;
		}
		if (list_0.Count == 1)
		{
			AnimationClip animationClip = list_0[0];
			float_0 = Time.time + animationClip.length + Random.Range(5f, 15f);
			animation_0.CrossFade(animationClip.name);
			return;
		}
		int num = Random.Range(0, list_0.Count - 1);
		if (int_0 == num)
		{
			num++;
			if (num >= list_0.Count)
			{
				num = 0;
			}
		}
		int_0 = num;
		AnimationClip animationClip2 = list_0[num];
		float_0 = Time.time + animationClip2.length + Random.Range(2f, 8f);
		animation_0.CrossFade(animationClip2.name);
	}
}
