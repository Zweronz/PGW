using System;
using UnityEngine;

[Serializable]
public class SmoothFollow2D : MonoBehaviour
{
	public Transform target;

	public float smoothTime;

	private Transform transform_0;

	private Vector2 vector2_0;

	public SmoothFollow2D()
	{
		smoothTime = 0.3f;
	}

	public virtual void Start()
	{
		transform_0 = transform;
	}

	public virtual void Update()
	{
		float x = Mathf.SmoothDamp(transform_0.position.x, target.position.x, ref vector2_0.x, smoothTime);
		Vector3 position = transform_0.position;
		position.x = x;
		transform_0.position = position;
		float y = Mathf.SmoothDamp(transform_0.position.y, target.position.y, ref vector2_0.y, smoothTime);
		Vector3 position2 = transform_0.position;
		position2.y = y;
		transform_0.position = position2;
	}

	public virtual void Main()
	{
	}
}
