using UnityEngine;

public class WallMovementController : MonoBehaviour
{
	public bool moveX;

	public bool moveY;

	public bool moveZ;

	public Vector3 amplitude = Vector3.one;

	private Transform transform_0;

	private float float_0;

	private Vector3 vector3_0;

	private void Awake()
	{
		transform_0 = GetComponent<Transform>();
		vector3_0 = transform_0.position;
		float_0 = 0f;
	}

	private void Update()
	{
		float_0 += Time.deltaTime * 1.2f;
		Vector3 position = new Vector3((!moveX) ? vector3_0.x : (vector3_0.x + amplitude.x * Mathf.Sin(float_0)), (!moveY) ? vector3_0.y : (vector3_0.y + amplitude.y * Mathf.Sin(float_0)), (!moveZ) ? vector3_0.z : (vector3_0.z + amplitude.z * Mathf.Sin(float_0)));
		transform_0.position = position;
	}
}
