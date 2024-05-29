using Holoville.HOTween;
using UnityEngine;

internal sealed class TrainingFingerSwipe : MonoBehaviour
{
	public Vector2 arrowDdelta = new Vector3(300f, 0f);

	private Vector3 vector3_0;

	private RectTransform rectTransform_0;

	private Tweener tweener_0;

	private void Awake()
	{
		rectTransform_0 = GetComponent<RectTransform>();
		vector3_0 = rectTransform_0.anchoredPosition;
	}

	private void OnEnable()
	{
		rectTransform_0.anchoredPosition = vector3_0;
		if (tweener_0 != null)
		{
			tweener_0.Kill();
		}
		tweener_0 = HOTween.To(rectTransform_0, 1f, new TweenParms().Prop("anchoredPosition", arrowDdelta, true).Ease(EaseType.EaseInQuad).Loops(-1, LoopType.Restart));
	}

	private void Update()
	{
		int completedLoop = tweener_0.completedLoops;
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
