using UnityEngine;

public class UIDragCamera : MonoBehaviour
{
	public UIDraggableCamera draggableCamera;

	private void Awake()
	{
		if (draggableCamera == null)
		{
			draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	private void OnPress(bool bool_0)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && draggableCamera != null)
		{
			draggableCamera.Press(bool_0);
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && draggableCamera != null)
		{
			draggableCamera.Drag(vector2_0);
		}
	}

	private void OnScroll(float float_0)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && draggableCamera != null)
		{
			draggableCamera.Scroll(float_0);
		}
	}
}
