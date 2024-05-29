using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UIOrthoCamera : MonoBehaviour
{
	private Camera camera_0;

	private Transform transform_0;

	private void Start()
	{
		camera_0 = base.GetComponent<Camera>();
		transform_0 = base.transform;
		camera_0.orthographic = true;
	}

	private void Update()
	{
		float num = camera_0.rect.yMin * (float)Screen.height;
		float num2 = camera_0.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * transform_0.lossyScale.y;
		if (!Mathf.Approximately(camera_0.orthographicSize, num3))
		{
			camera_0.orthographicSize = num3;
		}
	}
}
