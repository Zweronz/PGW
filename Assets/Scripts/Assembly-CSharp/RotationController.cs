using UnityEngine;

public class RotationController : MonoBehaviour
{
	public float speedX = 20f;

	public float speedY = 30f;

	public float speedZ = 40f;

	private Transform transform_0;

	private void Awake()
	{
		transform_0 = GetComponent<Transform>();
	}

	private void Update()
	{
		transform_0.Rotate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime);
	}
}
