using System;
using UnityEngine;

[Serializable]
public class RotationConstraint : MonoBehaviour
{
	public ConstraintAxis axis;

	public float min;

	public float max;

	public GameObject playerGun;

	private Transform transform_0;

	private Vector3 vector3_0;

	private Quaternion quaternion_0;

	private Quaternion quaternion_1;

	private float float_0;

	public virtual void Start()
	{
		transform_0 = transform;
		switch (axis)
		{
		case ConstraintAxis.X:
			vector3_0 = Vector3.right;
			break;
		case ConstraintAxis.Y:
			vector3_0 = Vector3.up;
			break;
		case ConstraintAxis.Z:
			vector3_0 = Vector3.forward;
			break;
		}
		Quaternion quaternion = Quaternion.AngleAxis(transform_0.localRotation.eulerAngles[(int)axis], vector3_0);
		quaternion_0 = quaternion * Quaternion.AngleAxis(min, vector3_0);
		quaternion_1 = quaternion * Quaternion.AngleAxis(max, vector3_0);
		float_0 = max - min;
	}

	public virtual void SetActiveFalse()
	{
		enabled = false;
	}

	public virtual void LateUpdate()
	{
		Quaternion localRotation = transform_0.localRotation;
		Quaternion a = Quaternion.AngleAxis(localRotation.eulerAngles[(int)axis], vector3_0);
		float num = Quaternion.Angle(a, quaternion_0);
		float num2 = Quaternion.Angle(a, quaternion_1);
		if (!(num > float_0) && !(num2 > float_0))
		{
			playerGun.transform.rotation = transform_0.rotation;
			return;
		}
		Vector3 eulerAngles = localRotation.eulerAngles;
		if (!(num <= num2))
		{
			eulerAngles[(int)axis] = quaternion_1.eulerAngles[(int)axis];
		}
		else
		{
			eulerAngles[(int)axis] = quaternion_0.eulerAngles[(int)axis];
		}
		transform_0.localEulerAngles = eulerAngles;
		playerGun.transform.rotation = transform_0.rotation;
	}

	public virtual void Main()
	{
	}
}
