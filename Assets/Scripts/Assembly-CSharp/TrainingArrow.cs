using Holoville.HOTween;
using UnityEngine;

internal sealed class TrainingArrow : MonoBehaviour
{
	public Vector2 arrowDelta = Vector2.zero;

	private Vector2 vector2_0;

	private RectTransform rectTransform_0;

	private Tweener tweener_0;

	private void Init()
	{
		if (rectTransform_0 == null)
		{
			rectTransform_0 = GetComponent<RectTransform>();
			vector2_0 = rectTransform_0.anchoredPosition;
		}
	}

	public void SetAnchoredPosition(Vector3 vector3_0)
	{
		Init();
		if (rectTransform_0 != null)
		{
			rectTransform_0.anchoredPosition = vector3_0;
			vector2_0 = rectTransform_0.anchoredPosition;
			if (tweener_0 != null)
			{
				tweener_0.Kill();
			}
			tweener_0 = HOTween.To(rectTransform_0, 0.5f, new TweenParms().Prop("anchoredPosition", arrowDelta, true).Loops(-1, LoopType.YoyoInverse));
		}
	}

	private void Awake()
	{
		Init();
	}

	private void OnEnable()
	{
		rectTransform_0.anchoredPosition = vector2_0;
		if (tweener_0 != null)
		{
			tweener_0.Kill();
		}
		tweener_0 = HOTween.To(rectTransform_0, 0.5f, new TweenParms().Prop("anchoredPosition", arrowDelta, true).Loops(-1, LoopType.YoyoInverse));
	}

	private void OnDisable()
	{
		if (tweener_0 != null)
		{
			tweener_0.Kill();
			tweener_0 = null;
		}
	}
}
