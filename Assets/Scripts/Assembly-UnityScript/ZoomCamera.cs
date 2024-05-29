using System;
using UnityEngine;

[Serializable]
public class ZoomCamera : MonoBehaviour
{
	public Transform origin;

	public float zoom;

	public float zoomMin;

	public float zoomMax;

	public float seekTime;

	public bool smoothZoomIn;

	private Vector3 vector3_0;

	private Transform transform_0;

	private float float_0;

	private float float_1;

	private float float_2;

	public ZoomCamera()
	{
		zoomMin = -5f;
		zoomMax = 5f;
		seekTime = 1f;
	}

	public virtual void Start()
	{
		transform_0 = transform;
		vector3_0 = transform_0.localPosition;
		float_0 = zoom;
	}

	public virtual void Update()
	{
		zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
		RaycastHit hitInfo = default(RaycastHit);
		Vector3 position = origin.position;
		Vector3 position2 = vector3_0 + transform_0.parent.InverseTransformDirection(transform_0.forward * zoom);
		Vector3 end = transform_0.parent.TransformPoint(position2);
		if (Physics.Linecast(position, end, out hitInfo, -261))
		{
			Vector3 vector = hitInfo.point + transform_0.TransformDirection(Vector3.forward);
			float_1 = (vector - transform_0.parent.TransformPoint(vector3_0)).magnitude;
		}
		else
		{
			float_1 = zoom;
		}
		float_1 = Mathf.Clamp(float_1, zoomMin, zoomMax);
		if (!smoothZoomIn && !(float_1 - float_0 <= 0f))
		{
			float_0 = float_1;
		}
		else
		{
			float_0 = Mathf.SmoothDamp(float_0, float_1, ref float_2, seekTime);
		}
		position2 = vector3_0 + transform_0.parent.InverseTransformDirection(transform_0.forward * float_0);
		transform_0.localPosition = position2;
	}

	public virtual void Main()
	{
	}
}
