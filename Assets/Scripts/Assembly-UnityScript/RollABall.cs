using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Rigidbody))]
public class RollABall : MonoBehaviour
{
	public Vector3 tilt;

	public float speed;

	private float float_0;

	private Vector3 vector3_0;

	public RollABall()
	{
		tilt = Vector3.zero;
	}

	public virtual void Start()
	{
		float_0 = (float)Math.PI * 2f * GetComponent<Collider>().bounds.extents.x;
		vector3_0 = transform.position;
	}

	public virtual void Update()
	{
		tilt.x = 0f - Input.acceleration.y;
		tilt.z = Input.acceleration.x;
		GetComponent<Rigidbody>().AddForce(tilt * speed * Time.deltaTime);
	}

	public virtual void LateUpdate()
	{
		Vector3 vector = transform.position - vector3_0;
		vector = new Vector3(vector.z, 0f, 0f - vector.x);
		transform.Rotate(vector / float_0 * 360f, Space.World);
		vector3_0 = transform.position;
	}

	public virtual void Main()
	{
	}
}
