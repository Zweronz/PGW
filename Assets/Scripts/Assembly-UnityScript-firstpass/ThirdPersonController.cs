using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
	public AnimationClip idleAnimation;

	public AnimationClip walkAnimation;

	public AnimationClip runAnimation;

	public AnimationClip jumpPoseAnimation;

	public float walkMaxAnimationSpeed;

	public float trotMaxAnimationSpeed;

	public float runMaxAnimationSpeed;

	public float jumpAnimationSpeed;

	public float landAnimationSpeed;

	private Animation animation_0;

	private CharacterState characterState_0;

	public float walkSpeed;

	public float trotSpeed;

	public float runSpeed;

	public float inAirControlAcceleration;

	public float jumpHeight;

	public float gravity;

	public float speedSmoothing;

	public float rotateSpeed;

	public float trotAfterSeconds;

	public bool canJump;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	private Vector3 vector3_0;

	private float float_4;

	private float float_5;

	private CollisionFlags collisionFlags_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private float float_6;

	private float float_7;

	private float float_8;

	private float float_9;

	private Vector3 vector3_1;

	private float float_10;

	private bool bool_4;

	public ThirdPersonController()
	{
		walkMaxAnimationSpeed = 0.75f;
		trotMaxAnimationSpeed = 1f;
		runMaxAnimationSpeed = 1f;
		jumpAnimationSpeed = 1.15f;
		landAnimationSpeed = 1f;
		walkSpeed = 2f;
		trotSpeed = 4f;
		runSpeed = 6f;
		inAirControlAcceleration = 3f;
		jumpHeight = 0.5f;
		gravity = 20f;
		speedSmoothing = 10f;
		rotateSpeed = 500f;
		trotAfterSeconds = 3f;
		canJump = true;
		float_0 = 0.05f;
		float_1 = 0.15f;
		float_2 = 0.25f;
		vector3_0 = Vector3.zero;
		float_7 = -10f;
		float_8 = -1f;
		vector3_1 = Vector3.zero;
		bool_4 = true;
	}

	public virtual void Awake()
	{
		vector3_0 = transform.TransformDirection(Vector3.forward);
		animation_0 = (Animation)GetComponent(typeof(Animation));
		if (!animation_0)
		{
			Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
		}
		if (!idleAnimation)
		{
			animation_0 = null;
			Debug.Log("No idle animation found. Turning off animations.");
		}
		if (!walkAnimation)
		{
			animation_0 = null;
			Debug.Log("No walk animation found. Turning off animations.");
		}
		if (!runAnimation)
		{
			animation_0 = null;
			Debug.Log("No run animation found. Turning off animations.");
		}
		if (!jumpPoseAnimation && canJump)
		{
			animation_0 = null;
			Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
		}
	}

	public virtual void UpdateSmoothedMovementDirection()
	{
		Transform transform = Camera.main.transform;
		bool flag = IsGrounded();
		Vector3 vector = transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 vector2 = new Vector3(vector.z, 0f, 0f - vector.x);
		float axisRaw = Input.GetAxisRaw("Vertical");
		float axisRaw2 = Input.GetAxisRaw("Horizontal");
		if (!(axisRaw >= -0.2f))
		{
			bool_2 = true;
		}
		else
		{
			bool_2 = false;
		}
		bool flag2 = bool_3;
		bool num = Mathf.Abs(axisRaw2) > 0.1f;
		if (!num)
		{
			num = Mathf.Abs(axisRaw) > 0.1f;
		}
		bool_3 = num;
		Vector3 vector3 = axisRaw2 * vector2 + axisRaw * vector;
		if (flag)
		{
			float_3 += Time.deltaTime;
			if (bool_3 != flag2)
			{
				float_3 = 0f;
			}
			if (vector3 != Vector3.zero)
			{
				if (!(float_5 >= walkSpeed * 0.9f) && flag)
				{
					vector3_0 = vector3.normalized;
				}
				else
				{
					vector3_0 = Vector3.RotateTowards(vector3_0, vector3, rotateSpeed * ((float)Math.PI / 180f) * Time.deltaTime, 1000f);
					vector3_0 = vector3_0.normalized;
				}
			}
			float t = speedSmoothing * Time.deltaTime;
			float num2 = Mathf.Min(vector3.magnitude, 1f);
			characterState_0 = CharacterState.Idle;
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				if (!(Time.time - trotAfterSeconds <= float_6))
				{
					num2 *= trotSpeed;
					characterState_0 = CharacterState.Trotting;
				}
				else
				{
					num2 *= walkSpeed;
					characterState_0 = CharacterState.Walking;
				}
			}
			else
			{
				num2 *= runSpeed;
				characterState_0 = CharacterState.Running;
			}
			float_5 = Mathf.Lerp(float_5, num2, t);
			if (!(float_5 >= walkSpeed * 0.3f))
			{
				float_6 = Time.time;
			}
		}
		else
		{
			if (bool_0)
			{
				float_3 = 0f;
			}
			if (bool_3)
			{
				vector3_1 += vector3.normalized * Time.deltaTime * inAirControlAcceleration;
			}
		}
	}

	public virtual void ApplyJumping()
	{
		if (!(float_8 + float_0 > Time.time) && IsGrounded() && canJump && !(Time.time >= float_7 + float_1))
		{
			float_4 = CalculateJumpVerticalSpeed(jumpHeight);
			SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ApplyGravity()
	{
		if (bool_4)
		{
			Input.GetButton("Jump");
			if (bool_0 && !bool_1 && !(float_4 > 0f))
			{
				bool_1 = true;
				SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}
			if (IsGrounded())
			{
				float_4 = 0f;
			}
			else
			{
				float_4 -= gravity * Time.deltaTime;
			}
		}
	}

	public virtual float CalculateJumpVerticalSpeed(float float_11)
	{
		return Mathf.Sqrt(2f * float_11 * gravity);
	}

	public virtual void DidJump()
	{
		bool_0 = true;
		bool_1 = false;
		float_8 = Time.time;
		float_9 = transform.position.y;
		float_7 = -10f;
		characterState_0 = CharacterState.Jumping;
	}

	public virtual void Update()
	{
		if (!bool_4)
		{
			Input.ResetInputAxes();
		}
		if (Input.GetButtonDown("Jump"))
		{
			float_7 = Time.time;
		}
		UpdateSmoothedMovementDirection();
		ApplyGravity();
		ApplyJumping();
		Vector3 vector = vector3_0 * float_5 + new Vector3(0f, float_4, 0f) + vector3_1;
		vector *= Time.deltaTime;
		CharacterController characterController = (CharacterController)GetComponent(typeof(CharacterController));
		collisionFlags_0 = characterController.Move(vector);
		if ((bool)animation_0)
		{
			if (characterState_0 == CharacterState.Jumping)
			{
				if (!bool_1)
				{
					animation_0[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
					animation_0[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					animation_0.CrossFade(jumpPoseAnimation.name);
				}
				else
				{
					animation_0[jumpPoseAnimation.name].speed = 0f - landAnimationSpeed;
					animation_0[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					animation_0.CrossFade(jumpPoseAnimation.name);
				}
			}
			else if (!(characterController.velocity.sqrMagnitude >= 0.1f))
			{
				animation_0.CrossFade(idleAnimation.name);
			}
			else if (characterState_0 == CharacterState.Running)
			{
				animation_0[runAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, 0f, runMaxAnimationSpeed);
				animation_0.CrossFade(runAnimation.name);
			}
			else if (characterState_0 == CharacterState.Trotting)
			{
				animation_0[walkAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, 0f, trotMaxAnimationSpeed);
				animation_0.CrossFade(walkAnimation.name);
			}
			else if (characterState_0 == CharacterState.Walking)
			{
				animation_0[walkAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, 0f, walkMaxAnimationSpeed);
				animation_0.CrossFade(walkAnimation.name);
			}
		}
		if (IsGrounded())
		{
			transform.rotation = Quaternion.LookRotation(vector3_0);
		}
		else
		{
			Vector3 forward = vector;
			forward.y = 0f;
			if (!(forward.sqrMagnitude <= 0.001f))
			{
				transform.rotation = Quaternion.LookRotation(forward);
			}
		}
		if (IsGrounded())
		{
			float_10 = Time.time;
			vector3_1 = Vector3.zero;
			if (bool_0)
			{
				bool_0 = false;
				SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void OnControllerColliderHit(ControllerColliderHit controllerColliderHit_0)
	{
		if (controllerColliderHit_0.moveDirection.y <= 0.01f)
		{
		}
	}

	public virtual float GetSpeed()
	{
		return float_5;
	}

	public virtual bool IsJumping()
	{
		return bool_0;
	}

	public virtual bool IsGrounded()
	{
		return (collisionFlags_0 & CollisionFlags.Below) != 0;
	}

	public virtual Vector3 GetDirection()
	{
		return vector3_0;
	}

	public virtual bool IsMovingBackwards()
	{
		return bool_2;
	}

	public virtual float GetLockCameraTimer()
	{
		return float_3;
	}

	public virtual bool IsMoving()
	{
		return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
	}

	public virtual bool HasJumpReachedApex()
	{
		return bool_1;
	}

	public virtual bool IsGroundedWithTimeout()
	{
		return float_10 + float_2 > Time.time;
	}

	public virtual void Reset()
	{
		gameObject.tag = "Player";
	}

	public virtual void Main()
	{
	}
}
