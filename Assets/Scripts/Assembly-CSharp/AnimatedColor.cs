using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	public Color color = Color.white;

	private UIWidget uiwidget_0;

	private void OnEnable()
	{
		uiwidget_0 = GetComponent<UIWidget>();
		LateUpdate();
	}

	private void LateUpdate()
	{
		uiwidget_0.Color_0 = color;
	}
}
