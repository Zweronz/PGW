using UnityEngine;
using DG.Tweening;

namespace DG.Tweening43
{
	public static class ShortcutExtensions
	{
		public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, target.MovePosition, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, target.MovePosition, new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, target.MovePosition, new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			return DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration).SetTarget(target);
		}
	}
}
