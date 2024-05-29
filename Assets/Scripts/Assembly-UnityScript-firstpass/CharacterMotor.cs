using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024SubtractNewPlatformVelocity_002459 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal Transform transform_0;

			internal CharacterMotor characterMotor_0;

			public _0024(CharacterMotor characterMotor_1)
			{
				characterMotor_0 = characterMotor_1;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (characterMotor_0.movingPlatform.enabled && (characterMotor_0.movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer || characterMotor_0.movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
					{
						if (characterMotor_0.movingPlatform.newPlatform)
						{
							transform_0 = characterMotor_0.movingPlatform.activePlatform;
							result = (Yield(2, new WaitForFixedUpdate()) ? 1 : 0);
							break;
						}
						goto case 4;
					}
					goto IL_0112;
				case 2:
					result = (Yield(3, new WaitForFixedUpdate()) ? 1 : 0);
					break;
				case 3:
					if (characterMotor_0.grounded && transform_0 == characterMotor_0.movingPlatform.activePlatform)
					{
						result = (Yield(4, 1) ? 1 : 0);
						break;
					}
					goto case 4;
				case 4:
					characterMotor_0.movement.velocity = characterMotor_0.movement.velocity - characterMotor_0.movingPlatform.platformVelocity;
					goto IL_0112;
				case 1:
					{
						result = 0;
						break;
					}
					IL_0112:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal CharacterMotor characterMotor_0;

		public _0024SubtractNewPlatformVelocity_002459(CharacterMotor characterMotor_1)
		{
			characterMotor_0 = characterMotor_1;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(characterMotor_0);
		}
	}

	public bool canControl;

	public bool useFixedUpdate;

	[NonSerialized]
	public Vector3 inputMoveDirection;

	[NonSerialized]
	public bool inputJump;

	public CharacterMotorMovement movement;

	public CharacterMotorJumping jumping;

	public CharacterMotorMovingPlatform movingPlatform;

	public CharacterMotorSliding sliding;

	[NonSerialized]
	public bool grounded;

	[NonSerialized]
	public Vector3 groundNormal;

	private Vector3 vector3_0;

	private Transform transform_0;

	private CharacterController characterController_0;

	public CharacterMotor()
	{
		canControl = true;
		useFixedUpdate = true;
		inputMoveDirection = Vector3.zero;
		movement = new CharacterMotorMovement();
		jumping = new CharacterMotorJumping();
		movingPlatform = new CharacterMotorMovingPlatform();
		sliding = new CharacterMotorSliding();
		grounded = true;
		groundNormal = Vector3.zero;
		vector3_0 = Vector3.zero;
	}

	public virtual void Awake()
	{
		characterController_0 = (CharacterController)GetComponent(typeof(CharacterController));
		transform_0 = transform;
	}

	private void UpdateFunction()
	{
		Vector3 velocity = movement.velocity;
		velocity = ApplyInputVelocityChange(velocity);
		velocity = ApplyGravityAndJumping(velocity);
		Vector3 zero = Vector3.zero;
		if (MoveWithPlatform())
		{
			Vector3 vector = movingPlatform.activePlatform.TransformPoint(movingPlatform.activeLocalPoint);
			zero = vector - movingPlatform.activeGlobalPoint;
			if (zero != Vector3.zero)
			{
				characterController_0.Move(zero);
			}
			Quaternion quaternion = movingPlatform.activePlatform.rotation * movingPlatform.activeLocalRotation;
			float y = (quaternion * Quaternion.Inverse(movingPlatform.activeGlobalRotation)).eulerAngles.y;
			if (y != 0f)
			{
				transform_0.Rotate(0f, y, 0f);
			}
		}
		Vector3 position = transform_0.position;
		Vector3 motion = velocity * Time.deltaTime;
		float num = Mathf.Max(characterController_0.stepOffset, new Vector3(motion.x, 0f, motion.z).magnitude);
		if (grounded)
		{
			motion -= num * Vector3.up;
		}
		movingPlatform.hitPlatform = null;
		groundNormal = Vector3.zero;
		movement.collisionFlags = characterController_0.Move(motion);
		movement.lastHitPoint = movement.hitPoint;
		vector3_0 = groundNormal;
		if (movingPlatform.enabled && movingPlatform.activePlatform != movingPlatform.hitPlatform && movingPlatform.hitPlatform != null)
		{
			movingPlatform.activePlatform = movingPlatform.hitPlatform;
			movingPlatform.lastMatrix = movingPlatform.hitPlatform.localToWorldMatrix;
			movingPlatform.newPlatform = true;
		}
		Vector3 vector2 = new Vector3(velocity.x, 0f, velocity.z);
		movement.velocity = (transform_0.position - position) / Time.deltaTime;
		Vector3 lhs = new Vector3(movement.velocity.x, 0f, movement.velocity.z);
		if (vector2 == Vector3.zero)
		{
			movement.velocity = new Vector3(0f, movement.velocity.y, 0f);
		}
		else
		{
			float value = Vector3.Dot(lhs, vector2) / vector2.sqrMagnitude;
			movement.velocity = vector2 * Mathf.Clamp01(value) + movement.velocity.y * Vector3.up;
		}
		if (!(movement.velocity.y >= velocity.y - 0.001f))
		{
			if (!(movement.velocity.y >= 0f))
			{
				movement.velocity.y = velocity.y;
			}
			else
			{
				jumping.holdingJumpButton = false;
			}
		}
		if (grounded && !IsGroundedTest())
		{
			grounded = false;
			if (movingPlatform.enabled && (movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer || movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
			{
				movement.frameVelocity = movingPlatform.platformVelocity;
				movement.velocity += movingPlatform.platformVelocity;
			}
			SendMessage("OnFall", SendMessageOptions.DontRequireReceiver);
			transform_0.position += num * Vector3.up;
		}
		else if (!grounded && IsGroundedTest())
		{
			grounded = true;
			jumping.jumping = false;
			StartCoroutine(SubtractNewPlatformVelocity());
			SendMessage("OnLand", SendMessageOptions.DontRequireReceiver);
		}
		if (MoveWithPlatform())
		{
			movingPlatform.activeGlobalPoint = transform_0.position + Vector3.up * (characterController_0.center.y - characterController_0.height * 0.5f + characterController_0.radius);
			movingPlatform.activeLocalPoint = movingPlatform.activePlatform.InverseTransformPoint(movingPlatform.activeGlobalPoint);
			movingPlatform.activeGlobalRotation = transform_0.rotation;
			movingPlatform.activeLocalRotation = Quaternion.Inverse(movingPlatform.activePlatform.rotation) * movingPlatform.activeGlobalRotation;
		}
	}

	public virtual void FixedUpdate()
	{
		if (movingPlatform.enabled)
		{
			if (movingPlatform.activePlatform != null)
			{
				if (!movingPlatform.newPlatform)
				{
					movingPlatform.platformVelocity = (movingPlatform.activePlatform.localToWorldMatrix.MultiplyPoint3x4(movingPlatform.activeLocalPoint) - movingPlatform.lastMatrix.MultiplyPoint3x4(movingPlatform.activeLocalPoint)) / Time.deltaTime;
				}
				movingPlatform.lastMatrix = movingPlatform.activePlatform.localToWorldMatrix;
				movingPlatform.newPlatform = false;
			}
			else
			{
				movingPlatform.platformVelocity = Vector3.zero;
			}
		}
		if (useFixedUpdate)
		{
			UpdateFunction();
		}
	}

	public virtual void Update()
	{
		if (!useFixedUpdate)
		{
			UpdateFunction();
		}
	}

	private Vector3 ApplyInputVelocityChange(Vector3 vector3_1)
	{
		if (!canControl)
		{
			inputMoveDirection = Vector3.zero;
		}
		Vector3 vector = default(Vector3);
		if (grounded && TooSteep())
		{
			vector = new Vector3(groundNormal.x, 0f, groundNormal.z).normalized;
			Vector3 vector2 = Vector3.Project(inputMoveDirection, vector);
			vector = vector + vector2 * sliding.speedControl + (inputMoveDirection - vector2) * sliding.sidewaysControl;
			vector *= sliding.slidingSpeed;
		}
		else
		{
			vector = GetDesiredHorizontalVelocity();
		}
		if (movingPlatform.enabled && movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer)
		{
			vector += movement.frameVelocity;
			vector.y = 0f;
		}
		if (grounded)
		{
			vector = AdjustGroundVelocityToNormal(vector, groundNormal);
		}
		else
		{
			vector3_1.y = 0f;
		}
		float num = GetMaxAcceleration(grounded) * Time.deltaTime;
		Vector3 vector3 = vector - vector3_1;
		if (!(vector3.sqrMagnitude <= num * num))
		{
			vector3 = vector3.normalized * num;
		}
		if (grounded || canControl)
		{
			vector3_1 += vector3;
		}
		if (grounded)
		{
			vector3_1.y = Mathf.Min(vector3_1.y, 0f);
		}
		return vector3_1;
	}

	private Vector3 ApplyGravityAndJumping(Vector3 vector3_1)
	{
		if (!inputJump || !canControl)
		{
			jumping.holdingJumpButton = false;
			jumping.lastButtonDownTime = -100f;
		}
		if (inputJump && !(jumping.lastButtonDownTime >= 0f) && canControl)
		{
			jumping.lastButtonDownTime = Time.time;
		}
		if (grounded)
		{
			vector3_1.y = Mathf.Min(0f, vector3_1.y) - movement.gravity * Time.deltaTime;
		}
		else
		{
			vector3_1.y = movement.velocity.y - movement.gravity * Time.deltaTime;
			if (jumping.jumping && jumping.holdingJumpButton && !(Time.time >= jumping.lastStartTime + jumping.extraHeight / CalculateJumpVerticalSpeed(jumping.baseHeight)))
			{
				vector3_1 += jumping.jumpDir * movement.gravity * Time.deltaTime;
			}
			vector3_1.y = Mathf.Max(vector3_1.y, 0f - movement.maxFallSpeed);
		}
		if (grounded)
		{
			if (jumping.enabled && canControl && !(Time.time - jumping.lastButtonDownTime >= 0.2f))
			{
				grounded = false;
				jumping.jumping = true;
				jumping.lastStartTime = Time.time;
				jumping.lastButtonDownTime = -100f;
				jumping.holdingJumpButton = true;
				if (TooSteep())
				{
					jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.steepPerpAmount);
				}
				else
				{
					jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.perpAmount);
				}
				vector3_1.y = 0f;
				vector3_1 += jumping.jumpDir * CalculateJumpVerticalSpeed(jumping.baseHeight);
				if (movingPlatform.enabled && (movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer || movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
				{
					movement.frameVelocity = movingPlatform.platformVelocity;
					vector3_1 += movingPlatform.platformVelocity;
				}
				SendMessage("OnJump", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				jumping.holdingJumpButton = false;
			}
		}
		return vector3_1;
	}

	public virtual void OnControllerColliderHit(ControllerColliderHit controllerColliderHit_0)
	{
		if (!(controllerColliderHit_0.normal.y <= 0f) && !(controllerColliderHit_0.normal.y <= groundNormal.y) && !(controllerColliderHit_0.moveDirection.y >= 0f))
		{
			if (!((controllerColliderHit_0.point - movement.lastHitPoint).sqrMagnitude > 0.001f) && !(vector3_0 == Vector3.zero))
			{
				groundNormal = vector3_0;
			}
			else
			{
				groundNormal = controllerColliderHit_0.normal;
			}
			movingPlatform.hitPlatform = controllerColliderHit_0.collider.transform;
			movement.hitPoint = controllerColliderHit_0.point;
			movement.frameVelocity = Vector3.zero;
		}
	}

	private IEnumerator SubtractNewPlatformVelocity()
	{
		return new _0024SubtractNewPlatformVelocity_002459(this).GetEnumerator();
	}

	private bool MoveWithPlatform()
	{
		bool num = movingPlatform.enabled;
		if (num)
		{
			num = grounded;
			if (!num)
			{
				num = movingPlatform.movementTransfer == MovementTransferOnJump.PermaLocked;
				if (num)
				{
					goto IL_002f;
				}
				goto IL_0041;
			}
		}
		if (num)
		{
			goto IL_002f;
		}
		goto IL_0041;
		IL_002f:
		num = movingPlatform.activePlatform != null;
		goto IL_0041;
		IL_0041:
		return num;
	}

	private Vector3 GetDesiredHorizontalVelocity()
	{
		Vector3 vector = transform_0.InverseTransformDirection(inputMoveDirection);
		float num = MaxSpeedInDirection(vector);
		if (grounded)
		{
			float time = Mathf.Asin(movement.velocity.normalized.y) * 57.29578f;
			num *= movement.slopeSpeedMultiplier.Evaluate(time);
		}
		return transform_0.TransformDirection(vector * num);
	}

	private Vector3 AdjustGroundVelocityToNormal(Vector3 vector3_1, Vector3 vector3_2)
	{
		Vector3 lhs = Vector3.Cross(Vector3.up, vector3_1);
		return Vector3.Cross(lhs, vector3_2).normalized * vector3_1.magnitude;
	}

	private bool IsGroundedTest()
	{
		return groundNormal.y > 0.01f;
	}

	public virtual float GetMaxAcceleration(bool bool_0)
	{
		return (!bool_0) ? movement.maxAirAcceleration : movement.maxGroundAcceleration;
	}

	public virtual float CalculateJumpVerticalSpeed(float float_0)
	{
		return Mathf.Sqrt(2f * float_0 * movement.gravity);
	}

	public virtual bool IsJumping()
	{
		return jumping.jumping;
	}

	public virtual bool IsSliding()
	{
		bool num = grounded;
		if (num)
		{
			num = sliding.enabled;
			if (num)
			{
				goto IL_001d;
			}
		}
		else if (num)
		{
			goto IL_001d;
		}
		goto IL_0024;
		IL_001d:
		num = TooSteep();
		goto IL_0024;
		IL_0024:
		return num;
	}

	public virtual bool IsTouchingCeiling()
	{
		return (movement.collisionFlags & CollisionFlags.Above) != 0;
	}

	public virtual bool IsGrounded()
	{
		return grounded;
	}

	public virtual bool TooSteep()
	{
		return !(groundNormal.y > Mathf.Cos(characterController_0.slopeLimit * ((float)Math.PI / 180f)));
	}

	public virtual Vector3 GetDirection()
	{
		return inputMoveDirection;
	}

	public virtual void SetControllable(bool bool_0)
	{
		canControl = bool_0;
	}

	public virtual float MaxSpeedInDirection(Vector3 vector3_1)
	{
		float result;
		if (vector3_1 == Vector3.zero)
		{
			result = 0f;
		}
		else
		{
			float num = ((vector3_1.z <= 0f) ? movement.maxBackwardsSpeed : movement.maxForwardSpeed) / movement.maxSidewaysSpeed;
			Vector3 normalized = new Vector3(vector3_1.x, 0f, vector3_1.z / num).normalized;
			float num2 = new Vector3(normalized.x, 0f, normalized.z * num).magnitude * movement.maxSidewaysSpeed;
			result = num2;
		}
		return result;
	}

	public virtual void SetVelocity(Vector3 vector3_1)
	{
		grounded = false;
		movement.velocity = vector3_1;
		movement.frameVelocity = Vector3.zero;
		SendMessage("OnExternalVelocity");
	}

	public virtual void Main()
	{
	}
}
