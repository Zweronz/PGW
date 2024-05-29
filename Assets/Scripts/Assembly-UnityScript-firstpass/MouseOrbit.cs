using System;
using UnityEngine;

[Serializable]
public class MouseOrbit : MonoBehaviour
{
	public Transform target;

	public float distance;

	public float xSpeed;

	public float ySpeed;

	public int yMinLimit;

	public int yMaxLimit;

	private float float_0;

	private float float_1;

	public MouseOrbit()
	{
		distance = 10f;
		xSpeed = 250f;
		ySpeed = 120f;
		yMinLimit = -20;
		yMaxLimit = 80;
	}

	public virtual void Start()
	{
		Vector3 eulerAngles = transform.eulerAngles;
		float_0 = eulerAngles.y;
		float_1 = eulerAngles.x;
		if ((bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	public virtual void LateUpdate()
	{
		if ((bool)target)
		{
			float_0 += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			float_1 -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			float_1 = ClampAngle(float_1, yMinLimit, yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(float_1, float_0, 0f);
			Vector3 position = quaternion * new Vector3(0f, 0f, 0f - distance) + target.position;
			transform.rotation = quaternion;
			transform.position = position;
		}
	}

	public static float ClampAngle(float float_2, float float_3, float float_4)
	{
		if (!(float_2 >= -360f))
		{
			float_2 += 360f;
		}
		if (!(float_2 <= 360f))
		{
			float_2 -= 360f;
		}
		return Mathf.Clamp(float_2, float_3, float_4);
	}

	public virtual void Main()
	{
	}
}
