using UnityEngine;

public class RotationWeaponGameObject : MonoBehaviour
{
	public enum ConstraintAxis
	{
		X = 0,
		Y = 1,
		Z = 2
	}

	public ConstraintAxis axis;

	public float min;

	public float max;

	public Transform playerGun;

	public Transform mechGun;

	public Player_move_c player_move_c;

	private Transform transform_0;

	private Vector3 vector3_0;

	private Quaternion quaternion_0;

	private Quaternion quaternion_1;

	private float float_0;

	private void Start()
	{
		transform_0 = base.transform;
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
		Quaternion quaternion = Quaternion.AngleAxis((axis == ConstraintAxis.X) ? transform_0.localRotation.eulerAngles.x : ((axis != ConstraintAxis.Y) ? transform_0.localRotation.eulerAngles.z : transform_0.localRotation.eulerAngles.y), vector3_0);
		quaternion_0 = quaternion * Quaternion.AngleAxis(min, vector3_0);
		quaternion_1 = quaternion * Quaternion.AngleAxis(max, vector3_0);
		float_0 = max - min;
	}

	private void SetActiveFalse()
	{
		base.enabled = false;
	}

	private void LateUpdate()
	{
		Quaternion localRotation = transform_0.localRotation;
		Quaternion a = Quaternion.AngleAxis((axis == ConstraintAxis.X) ? localRotation.eulerAngles.x : ((axis != ConstraintAxis.Y) ? localRotation.eulerAngles.z : localRotation.eulerAngles.y), vector3_0);
		float num = Quaternion.Angle(a, quaternion_0);
		float num2 = Quaternion.Angle(a, quaternion_1);
		if (num <= float_0 && num2 <= float_0)
		{
			playerGun.rotation = transform_0.rotation;
			playerGun.Rotate(player_move_c.Single_0, 0f, 0f);
			mechGun.rotation = transform_0.rotation;
			mechGun.Rotate(player_move_c.Single_0, 0f, 0f);
		}
		else
		{
			Vector3 localEulerAngles = localRotation.eulerAngles;
			localEulerAngles = ((!(num > num2)) ? new Vector3((axis != 0) ? localEulerAngles.x : quaternion_0.eulerAngles.x, (axis != ConstraintAxis.Y) ? localEulerAngles.y : quaternion_0.eulerAngles.y, (axis != ConstraintAxis.Z) ? localEulerAngles.z : quaternion_0.eulerAngles.z) : new Vector3((axis != 0) ? localEulerAngles.x : quaternion_1.eulerAngles.x, (axis != ConstraintAxis.Y) ? localEulerAngles.y : quaternion_1.eulerAngles.y, (axis != ConstraintAxis.Z) ? localEulerAngles.z : quaternion_1.eulerAngles.z));
			transform_0.localEulerAngles = localEulerAngles;
			playerGun.rotation = transform_0.rotation;
			playerGun.Rotate(player_move_c.Single_0, 0f, 0f);
			mechGun.rotation = transform_0.rotation;
			mechGun.Rotate(player_move_c.Single_0, 0f, 0f);
		}
	}
}
