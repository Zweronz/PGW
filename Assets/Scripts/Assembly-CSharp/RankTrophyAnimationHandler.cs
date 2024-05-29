using System;
using UnityEngine;

public class RankTrophyAnimationHandler : MonoBehaviour
{
	public Action onAppearCompleteCallback;

	public Action onRankUpDownAnimationComplete;

	public void OnAppearAnimationComplete()
	{
		if (onAppearCompleteCallback != null)
		{
			onAppearCompleteCallback();
		}
	}

	public void OnRankUpDownAnimationComplete()
	{
		if (onRankUpDownAnimationComplete != null)
		{
			onRankUpDownAnimationComplete();
		}
	}
}
