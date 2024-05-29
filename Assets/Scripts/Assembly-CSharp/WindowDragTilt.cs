using UnityEngine;

public class WindowDragTilt : MonoBehaviour
{
	public int updateOrder;

	public float degrees = 30f;

	private Vector3 vector3_0;

	private Transform transform_0;

	private float float_0;

	private void OnEnable()
	{
		transform_0 = base.transform;
		vector3_0 = transform_0.position;
	}

	private void Update()
	{
		Vector3 vector = transform_0.position - vector3_0;
		vector3_0 = transform_0.position;
		float_0 += vector.x * degrees;
		float_0 = NGUIMath.SpringLerp(float_0, 0f, 20f, Time.deltaTime);
		transform_0.localRotation = Quaternion.Euler(0f, 0f, 0f - float_0);
	}
}
