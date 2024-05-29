using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DG.Tweening46
{
	public static class ShortcutExtensions
	{
		public static Tweener DOFade(this CanvasGroup target, float endValue, float duration)
		{
			return DOTween.To(() => target.alpha, delegate(float x)
			{
				target.alpha = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOColor(this Graphic target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Graphic target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOColor(this Image target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Image target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFillAmount(this Image target, float endValue, float duration)
		{
			if (endValue > 1f)
			{
				endValue = 1f;
			}
			else if (endValue < 0f)
			{
				endValue = 0f;
			}
			return DOTween.To(() => target.fillAmount, delegate(float x)
			{
				target.fillAmount = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFlexibleSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), delegate(Vector2 x)
			{
				target.flexibleWidth = x.x;
				target.flexibleHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMinSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.minWidth, target.minHeight), delegate(Vector2 x)
			{
				target.minWidth = x.x;
				target.minHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOPreferredSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), delegate(Vector2 x)
			{
				target.preferredWidth = x.x;
				target.preferredHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOColor(this Outline target, Color endValue, float duration)
		{
			return DOTween.To(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Outline target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOScale(this Outline target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.effectDistance, delegate(Vector2 x)
			{
				target.effectDistance = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOAnchorPos(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPos3D(this RectTransform target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOSizeDelta(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.sizeDelta, delegate(Vector2 x)
			{
				target.sizeDelta = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOValue(this Slider target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.value, delegate(float x)
			{
				target.value = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOColor(this Text target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Text target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOText(this Text target, string endValue, float duration, bool scramble = false, string scrambleChars = null)
		{
			return DOTween.To(() => target.text, delegate(string x)
			{
				target.text = x;
			}, endValue, duration).SetOptions(scramble, scrambleChars).SetTarget(target);
		}
	}
}
