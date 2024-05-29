using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterMotor))]
public class FPSInputController : MonoBehaviour
{
	private CharacterMotor characterMotor_0;

	public virtual void Awake()
	{
		characterMotor_0 = (CharacterMotor)GetComponent(typeof(CharacterMotor));
	}

	public virtual void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		if (vector != Vector3.zero)
		{
			float magnitude = vector.magnitude;
			vector /= magnitude;
			magnitude = Mathf.Min(1f, magnitude);
			magnitude *= magnitude;
			vector *= magnitude;
		}
		characterMotor_0.inputMoveDirection = transform.rotation * vector;
		characterMotor_0.inputJump = Input.GetButton("Jump");
	}

	public virtual void Main()
	{
	}
}
