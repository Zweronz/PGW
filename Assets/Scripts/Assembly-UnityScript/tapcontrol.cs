using System;
using UnityEngine;

[Serializable]
public class tapcontrol : MonoBehaviour
{
	public GameObject cameraObject;

	public Transform cameraPivot;

	public GUITexture jumpButton;

	public float speed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public float minimumDistanceToMove;

	public float minimumTimeUntilMove;

	public bool zoomEnabled;

	public float zoomEpsilon;

	public float zoomRate;

	public bool rotateEnabled;

	public float rotateEpsilon;

	private ZoomCamera zoomCamera_0;

	private Camera camera_0;

	private Transform transform_0;

	private CharacterController characterController_0;

	private Vector3 vector3_0;

	private bool bool_0;

	private float float_0;

	private float float_1;

	private Vector3 vector3_1;

	private ControlState controlState_0;

	private int[] int_0;

	private Vector2[] vector2_0;

	private int[] int_1;

	private float float_2;

	public tapcontrol()
	{
		inAirMultiplier = 0.25f;
		minimumDistanceToMove = 1f;
		minimumTimeUntilMove = 0.25f;
		rotateEpsilon = 1f;
		controlState_0 = ControlState.WaitingForFirstTouch;
		int_0 = new int[2];
		vector2_0 = new Vector2[2];
		int_1 = new int[2];
	}

	public virtual void Start()
	{
		transform_0 = transform;
		zoomCamera_0 = (ZoomCamera)cameraObject.GetComponent(typeof(ZoomCamera));
		camera_0 = cameraObject.GetComponent<Camera>();
		characterController_0 = (CharacterController)GetComponent(typeof(CharacterController));
		ResetControlState();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if ((bool)gameObject)
		{
			transform_0.position = gameObject.transform.position;
		}
	}

	public virtual void OnEndGame()
	{
		enabled = false;
	}

	public virtual void FaceMovementDirection()
	{
		Vector3 velocity = characterController_0.velocity;
		velocity.y = 0f;
		if (!(velocity.magnitude <= 0.1f))
		{
			transform_0.forward = velocity.normalized;
		}
	}

	public virtual void CameraControl(Touch touch_0, Touch touch_1)
	{
		if (rotateEnabled && controlState_0 == ControlState.RotatingCamera)
		{
			Vector2 vector = touch_1.position - touch_0.position;
			Vector2 lhs = vector / vector.magnitude;
			Vector2 vector2 = touch_1.position - touch_1.deltaPosition - (touch_0.position - touch_0.deltaPosition);
			Vector2 rhs = vector2 / vector2.magnitude;
			float num = Vector2.Dot(lhs, rhs);
			if (!(num >= 1f))
			{
				Vector3 lhs2 = new Vector3(vector.x, vector.y);
				Vector3 rhs2 = new Vector3(vector2.x, vector2.y);
				float z = Vector3.Cross(lhs2, rhs2).normalized.z;
				float num2 = Mathf.Acos(num);
				float_0 += num2 * 57.29578f * z;
				if (!(float_0 >= 0f))
				{
					float_0 += 360f;
				}
				else if (!(float_0 < 360f))
				{
					float_0 -= 360f;
				}
			}
		}
		else if (zoomEnabled && controlState_0 == ControlState.ZoomingCamera)
		{
			float magnitude = (touch_1.position - touch_0.position).magnitude;
			float magnitude2 = (touch_1.position - touch_1.deltaPosition - (touch_0.position - touch_0.deltaPosition)).magnitude;
			float num3 = magnitude - magnitude2;
			zoomCamera_0.zoom += num3 * zoomRate * Time.deltaTime;
		}
	}

	public virtual void CharacterControl()
	{
		int touchCount = Input.touchCount;
		if (touchCount == 1 && controlState_0 == ControlState.MovingCharacter)
		{
			Touch touch = Input.GetTouch(0);
			if (characterController_0.isGrounded && jumpButton.HitTest(touch.position))
			{
				vector3_1 = characterController_0.velocity;
				vector3_1.y = jumpSpeed;
			}
			else if (!jumpButton.HitTest(touch.position) && touch.phase != 0)
			{
				Ray ray = camera_0.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y));
				RaycastHit hitInfo = default(RaycastHit);
				if (Physics.Raycast(ray, out hitInfo))
				{
					float magnitude = (transform.position - hitInfo.point).magnitude;
					if (!(magnitude <= minimumDistanceToMove))
					{
						vector3_0 = hitInfo.point;
					}
					bool_0 = true;
				}
			}
		}
		Vector3 motion = Vector3.zero;
		if (bool_0)
		{
			motion = vector3_0 - transform_0.position;
			motion.y = 0f;
			float magnitude2 = motion.magnitude;
			if (!(magnitude2 >= 1f))
			{
				bool_0 = false;
			}
			else
			{
				motion = motion.normalized * speed;
			}
		}
		if (!characterController_0.isGrounded)
		{
			vector3_1.y += Physics.gravity.y * Time.deltaTime;
			motion.x *= inAirMultiplier;
			motion.z *= inAirMultiplier;
		}
		motion += vector3_1;
		motion += Physics.gravity;
		motion *= Time.deltaTime;
		characterController_0.Move(motion);
		if (characterController_0.isGrounded)
		{
			vector3_1 = Vector3.zero;
		}
		FaceMovementDirection();
	}

	public virtual void ResetControlState()
	{
		controlState_0 = ControlState.WaitingForFirstTouch;
		int_0[0] = -1;
		int_0[1] = -1;
	}

	public virtual void Update()
	{
		int touchCount = Input.touchCount;
		if (touchCount == 0)
		{
			ResetControlState();
		}
		else
		{
			int num = default(int);
			Touch touch = default(Touch);
			Touch[] touches = Input.touches;
			Touch touch_ = default(Touch);
			Touch touch_2 = default(Touch);
			bool flag = false;
			bool flag2 = false;
			if (controlState_0 == ControlState.WaitingForFirstTouch)
			{
				for (num = 0; num < touchCount; num++)
				{
					touch = touches[num];
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						controlState_0 = ControlState.WaitingForSecondTouch;
						float_2 = Time.time;
						int_0[0] = touch.fingerId;
						vector2_0[0] = touch.position;
						int_1[0] = Time.frameCount;
						break;
					}
				}
			}
			if (controlState_0 == ControlState.WaitingForSecondTouch)
			{
				for (num = 0; num < touchCount; num++)
				{
					touch = touches[num];
					if (touch.phase == TouchPhase.Canceled)
					{
						continue;
					}
					if (touchCount < 2 || touch.fingerId == int_0[0])
					{
						if (touchCount == 1)
						{
							Vector2 vector3 = touch.position - vector2_0[0];
							if (touch.fingerId == int_0[0] && (Time.time > float_2 + minimumTimeUntilMove || touch.phase == TouchPhase.Ended))
							{
								controlState_0 = ControlState.MovingCharacter;
								break;
							}
						}
						continue;
					}
					controlState_0 = ControlState.WaitingForMovement;
					int_0[1] = touch.fingerId;
					vector2_0[1] = touch.position;
					int_1[1] = Time.frameCount;
					break;
				}
			}
			if (controlState_0 == ControlState.WaitingForMovement)
			{
				for (num = 0; num < touchCount; num++)
				{
					touch = touches[num];
					if (touch.phase == TouchPhase.Began)
					{
						if (touch.fingerId == int_0[0] && int_1[0] == Time.frameCount)
						{
							touch_ = touch;
							flag = true;
						}
						else if (touch.fingerId != int_0[0] && touch.fingerId != int_0[1])
						{
							int_0[1] = touch.fingerId;
							touch_2 = touch;
							flag2 = true;
						}
					}
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
					{
						if (touch.fingerId == int_0[0])
						{
							touch_ = touch;
							flag = true;
						}
						else if (touch.fingerId == int_0[1])
						{
							touch_2 = touch;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						Vector2 vector = vector2_0[1] - vector2_0[0];
						Vector2 vector2 = touch_2.position - touch_.position;
						Vector2 lhs = vector / vector.magnitude;
						Vector2 rhs = vector2 / vector2.magnitude;
						float num2 = Vector2.Dot(lhs, rhs);
						if (!(num2 >= 1f))
						{
							float num3 = Mathf.Acos(num2);
							if (!(num3 <= rotateEpsilon * ((float)Math.PI / 180f)))
							{
								controlState_0 = ControlState.RotatingCamera;
							}
						}
						if (controlState_0 == ControlState.WaitingForMovement)
						{
							float f = vector.magnitude - vector2.magnitude;
							if (!(Mathf.Abs(f) <= zoomEpsilon))
							{
								controlState_0 = ControlState.ZoomingCamera;
							}
						}
					}
				}
				else
				{
					controlState_0 = ControlState.WaitingForNoFingers;
				}
			}
			if (controlState_0 == ControlState.RotatingCamera || controlState_0 == ControlState.ZoomingCamera)
			{
				for (num = 0; num < touchCount; num++)
				{
					touch = touches[num];
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
					{
						if (touch.fingerId == int_0[0])
						{
							touch_ = touch;
							flag = true;
						}
						else if (touch.fingerId == int_0[1])
						{
							touch_2 = touch;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						CameraControl(touch_, touch_2);
					}
				}
				else
				{
					controlState_0 = ControlState.WaitingForNoFingers;
				}
			}
		}
		CharacterControl();
	}

	public virtual void LateUpdate()
	{
		float y = Mathf.SmoothDampAngle(cameraPivot.eulerAngles.y, float_0, ref float_1, 0.3f);
		Vector3 eulerAngles = cameraPivot.eulerAngles;
		eulerAngles.y = y;
		cameraPivot.eulerAngles = eulerAngles;
	}

	public virtual void Main()
	{
	}
}
