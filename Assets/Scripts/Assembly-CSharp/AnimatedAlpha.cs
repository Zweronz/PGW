using UnityEngine;

public class AnimatedAlpha : MonoBehaviour
{
	[Range(0f, 1f)]
	public float alpha = 1f;

	private UIWidget uiwidget_0;

	private UIPanel uipanel_0;

	private void OnEnable()
	{
		uiwidget_0 = GetComponent<UIWidget>();
		uipanel_0 = GetComponent<UIPanel>();
		LateUpdate();
	}

	private void LateUpdate()
	{
		if (uiwidget_0 != null)
		{
			uiwidget_0.Single_2 = alpha;
		}
		if (uipanel_0 != null)
		{
			uipanel_0.Single_2 = alpha;
		}
	}
}
