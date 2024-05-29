using UnityEngine;

namespace engine.unity
{
	public class WindowFader : MonoBehaviour
	{
		public UISprite Fader;

		public UIPlayTween Tweener;

		private TweenAlpha tweenAlpha_0;

		private TweenAlpha tweenAlpha_1;

		private bool bool_0;

		private bool bool_1;

		private bool bool_2;

		private void Awake()
		{
			InitParams();
		}

		public void Show()
		{
			InitParams();
			if (!bool_1)
			{
				bool_1 = true;
				NGUITools.SetActive(base.gameObject, true, false);
				bool_0 = false;
				if (tweenAlpha_1 != null)
				{
					tweenAlpha_1.enabled = false;
				}
				if (Tweener != null)
				{
					Tweener.tweenGroup = 0;
					Tweener.Play(true);
				}
			}
		}

		public void Hide()
		{
			InitParams();
			bool_1 = false;
			bool_0 = true;
			if (Tweener != null)
			{
				Tweener.tweenGroup = 1;
			}
			if (tweenAlpha_0 != null)
			{
				tweenAlpha_0.enabled = false;
			}
			if (Mathf.Approximately(Fader.Single_2, 0f))
			{
				OnFadeOutComplete();
			}
			else if (Tweener != null)
			{
				Tweener.Play(true);
			}
			else
			{
				OnFadeOutComplete();
			}
		}

		public void OnFadeOutComplete()
		{
			if (bool_0)
			{
				NGUITools.SetActive(base.gameObject, false, false);
			}
		}

		public void SetAlphaForce(float float_0)
		{
			if (bool_1)
			{
				if (tweenAlpha_1 != null)
				{
					tweenAlpha_1.ResetToBeginning();
					tweenAlpha_1.enabled = false;
				}
				if (tweenAlpha_0 != null)
				{
					tweenAlpha_0.ResetToBeginning();
					tweenAlpha_0.enabled = false;
				}
				Fader.Single_2 = float_0;
			}
		}

		private void InitParams()
		{
			if (bool_2)
			{
				return;
			}
			bool_2 = true;
			TweenAlpha[] components = Fader.GetComponents<TweenAlpha>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].int_0 == 0)
				{
					tweenAlpha_0 = components[i];
				}
				else if (components[i].int_0 == 1)
				{
					tweenAlpha_1 = components[i];
				}
			}
		}
	}
}
