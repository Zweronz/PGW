using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UIViewport : MonoBehaviour
{
	public Camera sourceCamera;

	public Transform topLeft;

	public Transform bottomRight;

	public float fullSize = 1f;

	private Camera camera_0;

	private void Start()
	{
		camera_0 = base.GetComponent<Camera>();
		if (sourceCamera == null)
		{
			sourceCamera = Camera.main;
		}
	}

	private void LateUpdate()
	{
		if (topLeft != null && bottomRight != null)
		{
			Vector3 vector = sourceCamera.WorldToScreenPoint(topLeft.position);
			Vector3 vector2 = sourceCamera.WorldToScreenPoint(bottomRight.position);
			Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = fullSize * rect.height;
			if (rect != camera_0.rect)
			{
				camera_0.rect = rect;
			}
			if (camera_0.orthographicSize != num)
			{
				camera_0.orthographicSize = num;
			}
		}
	}
}
