using System;
using Holoville.HOTween;
using UnityEngine;

internal sealed class TrainingFinger : MonoBehaviour
{
	public float AngleX;

	public float AngleY;

	private Vector3 vector3_0;

	private RectTransform rectTransform_0;

	private void Awake()
	{
		rectTransform_0 = GetComponent<RectTransform>();
		vector3_0 = rectTransform_0.localPosition;
	}

	private void OnEnable()
	{
		rectTransform_0.localPosition = vector3_0;
		AngleX = 0f;
		HOTween.To(this, 4f, new TweenParms().Prop("AngleX", (float)Math.PI * 2f, true).Ease(EaseType.Linear).Loops(-1, LoopType.Restart));
		AngleY = 0f;
		HOTween.To(this, 2f, new TweenParms().Prop("AngleY", (float)Math.PI * 2f, true).Ease(EaseType.Linear).Loops(-1, LoopType.Restart));
	}

	private void Update()
	{
		Vector3 vector = new Vector3(60f * Mathf.Sin(AngleX), 30f * Mathf.Sin(AngleY), 0f);
		base.transform.localPosition = vector3_0 + vector;
	}
}
