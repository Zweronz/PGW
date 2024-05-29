using UnityEngine;
using engine.events;

public class TimeLabel : MonoBehaviour
{
	public Animation TimeLeftAnimation;

	public UISprite animateIconBlue;

	public UISprite animateIconRed;

	private UILabel uilabel_0;

	private void Start()
	{
		base.gameObject.SetActive(Defs.bool_2);
		uilabel_0 = GetComponent<UILabel>();
		DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(OnTimerHurryUp);
	}

	private void OnDestroy()
	{
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(OnTimerHurryUp))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(OnTimerHurryUp);
		}
	}

	private void Update()
	{
		if (uilabel_0 == null || !(HeadUpDisplay.GetPlayerMoveC() != null))
		{
			return;
		}
		uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().GetTimeLeft();
		if (TimeLeftAnimation != null)
		{
			CheckPlayAnimation(TimeLeftAnimation);
			if (!TimeLeftAnimation.enabled)
			{
				uilabel_0.GetComponent<AnimatedColor>().color = Color.white;
				uilabel_0.transform.localScale.Set(0.6f, 0.6f, 0.6f);
			}
		}
		if (animateIconBlue != null)
		{
			CheckPlayAnimation(animateIconBlue.GetComponent<Animation>());
		}
		if (animateIconRed != null)
		{
			CheckPlayAnimation(animateIconRed.GetComponent<Animation>());
		}
	}

	private void CheckPlayAnimation(Animation animation_0)
	{
		if (!(animation_0 == null))
		{
			float num = (float)HeadUpDisplay.GetPlayerMoveC().GetTimeDown();
			if (num > 60f && animation_0.enabled)
			{
				animation_0.enabled = false;
			}
			else if (num < 60f && !animation_0.enabled)
			{
				animation_0.enabled = true;
			}
		}
	}

	private void OnTimerHurryUp()
	{
		int num = (int)HeadUpDisplay.GetPlayerMoveC().GetTimeDown();
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			switch (num)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 20:
			case 30:
			case 60:
				HeadUpDisplay.HeadUpDisplay_0.ShowArenaTimeAttention(num, false);
				break;
			}
		}
		if (num <= 0 && DependSceneEvent<MainUpdateOneSecond>.Contains(OnTimerHurryUp))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(OnTimerHurryUp);
		}
	}
}
