using UnityEngine;

public class SpinWithMouse : MonoBehaviour
{
	public Transform target;

	public float speed = 1f;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void OnDrag(Vector2 vector2_0)
	{
		UICamera.mouseOrTouch_0.clickNotification_0 = UICamera.ClickNotification.None;
		if (target != null)
		{
			target.localRotation = Quaternion.Euler(0f, -0.5f * vector2_0.x * speed, 0f) * target.localRotation;
		}
		else
		{
			transform_0.localRotation = Quaternion.Euler(0f, -0.5f * vector2_0.x * speed, 0f) * transform_0.localRotation;
		}
	}
}
