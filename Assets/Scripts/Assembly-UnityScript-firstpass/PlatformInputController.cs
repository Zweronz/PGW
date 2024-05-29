using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterMotor))]
public class PlatformInputController : MonoBehaviour
{
	public bool autoRotate;

	public float maxRotationSpeed;

	private CharacterMotor characterMotor_0;

	public PlatformInputController()
	{
		autoRotate = true;
		maxRotationSpeed = 360f;
	}

	public virtual void Awake()
	{
		characterMotor_0 = (CharacterMotor)GetComponent(typeof(CharacterMotor));
	}

	public virtual void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		if (vector != Vector3.zero)
		{
			float magnitude = vector.magnitude;
			vector /= magnitude;
			magnitude = Mathf.Min(1f, magnitude);
			magnitude *= magnitude;
			vector *= magnitude;
		}
		vector = Camera.main.transform.rotation * vector;
		Quaternion quaternion = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
		vector = quaternion * vector;
		characterMotor_0.inputMoveDirection = vector;
		characterMotor_0.inputJump = Input.GetButton("Jump");
		if (autoRotate && !(vector.sqrMagnitude <= 0.01f))
		{
			Vector3 vector3_ = ConstantSlerp(transform.forward, vector, maxRotationSpeed * Time.deltaTime);
			vector3_ = ProjectOntoPlane(vector3_, transform.up);
			transform.rotation = Quaternion.LookRotation(vector3_, transform.up);
		}
	}

	public virtual Vector3 ProjectOntoPlane(Vector3 vector3_0, Vector3 vector3_1)
	{
		return vector3_0 - Vector3.Project(vector3_0, vector3_1);
	}

	public virtual Vector3 ConstantSlerp(Vector3 vector3_0, Vector3 vector3_1, float float_0)
	{
		float t = Mathf.Min(1f, float_0 / Vector3.Angle(vector3_0, vector3_1));
		return Vector3.Slerp(vector3_0, vector3_1, t);
	}

	public virtual void Main()
	{
	}
}
