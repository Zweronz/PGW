using UnityEngine;

public class WindowAutoYaw : MonoBehaviour
{
	public int updateOrder;

	public Camera uiCamera;

	public float yawAmount = 20f;

	private Transform transform_0;

	private void OnDisable()
	{
		transform_0.localRotation = Quaternion.identity;
	}

	private void OnEnable()
	{
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		transform_0 = base.transform;
	}

	private void Update()
	{
		if (uiCamera != null)
		{
			Vector3 vector = uiCamera.WorldToViewportPoint(transform_0.position);
			transform_0.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * yawAmount, 0f);
		}
	}
}
