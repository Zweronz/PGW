using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPathAnimator : MonoBehaviour
{
	public enum animationModes
	{
		once = 0,
		loop = 1,
		reverse = 2,
		reverseLoop = 3,
		pingPong = 4
	}

	public enum orientationModes
	{
		custom = 0,
		target = 1,
		mouselook = 2,
		followpath = 3,
		reverseFollowpath = 4,
		followTransform = 5,
		twoDimentions = 6,
		fixedOrientation = 7,
		none = 8
	}

	public delegate void AnimationStartedEventHandler();

	public delegate void AnimationPausedEventHandler();

	public delegate void AnimationStoppedEventHandler();

	public delegate void AnimationFinishedEventHandler();

	public delegate void AnimationLoopedEventHandler();

	public delegate void AnimationPingPongEventHandler();

	public delegate void AnimationPointReachedEventHandler();

	public delegate void AnimationCustomEventHandler(string string_0);

	public delegate void AnimationPointReachedWithNumberEventHandler(int int_0);

	public float minimumCameraSpeed = 0.01f;

	public Transform orientationTarget;

	[SerializeField]
	private CameraPath cameraPath_0;

	public bool playOnStart = true;

	public Transform animationObject;

	private Camera camera_0;

	private bool bool_0 = true;

	private bool bool_1;

	public animationModes animationMode;

	public orientationModes orientationMode;

	private float float_0 = 1f;

	public Vector3 fixedOrientaion = Vector3.forward;

	public bool normalised = true;

	public float editorPercentage;

	[SerializeField]
	private float float_1 = 10f;

	[SerializeField]
	private float float_2 = 10f;

	private float float_3;

	private float float_4;

	public float nearestOffset;

	private float float_5;

	public float sensitivity = 5f;

	public float minX = -90f;

	public float maxX = 90f;

	private float float_6;

	private float float_7;

	public bool showPreview = true;

	public GameObject editorPreview;

	public bool showScenePreview = true;

	private bool bool_2;

	public Vector3 animatedObjectStartPosition;

	public Quaternion animatedObjectStartRotation;

	private AnimationStartedEventHandler animationStartedEventHandler_0;

	private AnimationPausedEventHandler animationPausedEventHandler_0;

	private AnimationStoppedEventHandler animationStoppedEventHandler_0;

	private AnimationFinishedEventHandler animationFinishedEventHandler_0;

	private AnimationLoopedEventHandler animationLoopedEventHandler_0;

	private AnimationPingPongEventHandler animationPingPongEventHandler_0;

	private AnimationPointReachedEventHandler animationPointReachedEventHandler_0;

	private AnimationPointReachedWithNumberEventHandler animationPointReachedWithNumberEventHandler_0;

	private AnimationCustomEventHandler animationCustomEventHandler_0;

	public float Single_0
	{
		get
		{
			return float_2;
		}
		set
		{
			if (cameraPath_0.CameraPathSpeedList_0.Boolean_0)
			{
				Debug.LogWarning("Path Speed in Animator component is ignored and overridden by Camera Path speed points.");
			}
			float_2 = Mathf.Max(value, minimumCameraSpeed);
		}
	}

	public float Single_1
	{
		get
		{
			return float_1 * float_3;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_1;
		}
	}

	public float Single_2
	{
		get
		{
			return float_3;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return float_0 == 1f;
		}
	}

	public CameraPath CameraPath_0
	{
		get
		{
			if (!cameraPath_0)
			{
				cameraPath_0 = GetComponent<CameraPath>();
			}
			return cameraPath_0;
		}
	}

	private bool Boolean_2
	{
		get
		{
			return animationMode == animationModes.reverse || animationMode == animationModes.reverseLoop || float_0 < 0f;
		}
	}

	public bool Boolean_3
	{
		get
		{
			if (animationObject == null)
			{
				bool_0 = false;
			}
			else
			{
				bool_0 = camera_0 != null;
			}
			return bool_0;
		}
	}

	public bool Boolean_4
	{
		get
		{
			return bool_2;
		}
		set
		{
			if (value != bool_2)
			{
				bool_2 = value;
				if (animationObject != null)
				{
					if (bool_2)
					{
						animatedObjectStartPosition = animationObject.transform.position;
						animatedObjectStartRotation = animationObject.transform.rotation;
					}
					else
					{
						animationObject.transform.position = animatedObjectStartPosition;
						animationObject.transform.rotation = animatedObjectStartRotation;
					}
				}
			}
			bool_2 = value;
		}
	}

	public event AnimationStartedEventHandler AnimationStartedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationStartedEventHandler_0 = (AnimationStartedEventHandler)Delegate.Combine(animationStartedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationStartedEventHandler_0 = (AnimationStartedEventHandler)Delegate.Remove(animationStartedEventHandler_0, value);
		}
	}

	public event AnimationPausedEventHandler AnimationPausedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationPausedEventHandler_0 = (AnimationPausedEventHandler)Delegate.Combine(animationPausedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationPausedEventHandler_0 = (AnimationPausedEventHandler)Delegate.Remove(animationPausedEventHandler_0, value);
		}
	}

	public event AnimationStoppedEventHandler AnimationStoppedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationStoppedEventHandler_0 = (AnimationStoppedEventHandler)Delegate.Combine(animationStoppedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationStoppedEventHandler_0 = (AnimationStoppedEventHandler)Delegate.Remove(animationStoppedEventHandler_0, value);
		}
	}

	public event AnimationFinishedEventHandler AnimationFinishedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationFinishedEventHandler_0 = (AnimationFinishedEventHandler)Delegate.Combine(animationFinishedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationFinishedEventHandler_0 = (AnimationFinishedEventHandler)Delegate.Remove(animationFinishedEventHandler_0, value);
		}
	}

	public event AnimationLoopedEventHandler AnimationLoopedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationLoopedEventHandler_0 = (AnimationLoopedEventHandler)Delegate.Combine(animationLoopedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationLoopedEventHandler_0 = (AnimationLoopedEventHandler)Delegate.Remove(animationLoopedEventHandler_0, value);
		}
	}

	public event AnimationPingPongEventHandler AnimationPingPongEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationPingPongEventHandler_0 = (AnimationPingPongEventHandler)Delegate.Combine(animationPingPongEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationPingPongEventHandler_0 = (AnimationPingPongEventHandler)Delegate.Remove(animationPingPongEventHandler_0, value);
		}
	}

	public event AnimationPointReachedEventHandler AnimationPointReachedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationPointReachedEventHandler_0 = (AnimationPointReachedEventHandler)Delegate.Combine(animationPointReachedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationPointReachedEventHandler_0 = (AnimationPointReachedEventHandler)Delegate.Remove(animationPointReachedEventHandler_0, value);
		}
	}

	public event AnimationPointReachedWithNumberEventHandler AnimationPointReachedWithNumberEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationPointReachedWithNumberEventHandler_0 = (AnimationPointReachedWithNumberEventHandler)Delegate.Combine(animationPointReachedWithNumberEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationPointReachedWithNumberEventHandler_0 = (AnimationPointReachedWithNumberEventHandler)Delegate.Remove(animationPointReachedWithNumberEventHandler_0, value);
		}
	}

	public event AnimationCustomEventHandler AnimationCustomEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			animationCustomEventHandler_0 = (AnimationCustomEventHandler)Delegate.Combine(animationCustomEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			animationCustomEventHandler_0 = (AnimationCustomEventHandler)Delegate.Remove(animationCustomEventHandler_0, value);
		}
	}

	public void Play()
	{
		bool_1 = true;
		if (!Boolean_2)
		{
			if (float_3 == 0f)
			{
				if (animationStartedEventHandler_0 != null)
				{
					animationStartedEventHandler_0();
				}
				CameraPath_0.CameraPathEventList_0.OnAnimationStart(0f);
			}
		}
		else if (float_3 == 1f)
		{
			if (animationStartedEventHandler_0 != null)
			{
				animationStartedEventHandler_0();
			}
			CameraPath_0.CameraPathEventList_0.OnAnimationStart(1f);
		}
		float_4 = float_3;
	}

	public void Stop()
	{
		bool_1 = false;
		float_3 = 0f;
		if (animationStoppedEventHandler_0 != null)
		{
			animationStoppedEventHandler_0();
		}
	}

	public void Pause()
	{
		bool_1 = false;
		if (animationPausedEventHandler_0 != null)
		{
			animationPausedEventHandler_0();
		}
	}

	public void Seek(float float_8)
	{
		float_3 = Mathf.Clamp01(float_8);
		float_4 = float_3;
		UpdateAnimationTime(false);
		UpdatePointReached();
		bool flag = bool_1;
		bool_1 = true;
		UpdateAnimation();
		bool_1 = flag;
	}

	public void Reverse()
	{
		switch (animationMode)
		{
		case animationModes.once:
			animationMode = animationModes.reverse;
			break;
		case animationModes.loop:
			animationMode = animationModes.reverseLoop;
			break;
		case animationModes.reverse:
			animationMode = animationModes.once;
			break;
		case animationModes.reverseLoop:
			animationMode = animationModes.loop;
			break;
		case animationModes.pingPong:
			float_0 = ((float_0 == -1f) ? 1 : (-1));
			break;
		}
	}

	public Quaternion GetAnimatedOrientation(float float_8, bool bool_3)
	{
		Quaternion quaternion = Quaternion.identity;
		switch (orientationMode)
		{
		case orientationModes.custom:
			quaternion = CameraPath_0.GetPathRotation(float_8, bool_3);
			break;
		case orientationModes.target:
		{
			Vector3 pathPosition = CameraPath_0.GetPathPosition(float_8);
			Vector3 forward = ((!(orientationTarget != null)) ? Vector3.forward : (orientationTarget.transform.position - pathPosition));
			quaternion = Quaternion.LookRotation(forward);
			break;
		}
		case orientationModes.mouselook:
			if (!Application.isPlaying)
			{
				quaternion = Quaternion.LookRotation(CameraPath_0.GetPathDirection(float_8));
				quaternion *= Quaternion.Euler(base.transform.forward * (0f - CameraPath_0.GetPathTilt(float_8)));
			}
			else
			{
				quaternion = GetMouseLook();
			}
			break;
		case orientationModes.followpath:
			quaternion = Quaternion.LookRotation(CameraPath_0.GetPathDirection(float_8));
			quaternion *= Quaternion.Euler(base.transform.forward * (0f - CameraPath_0.GetPathTilt(float_8)));
			break;
		case orientationModes.reverseFollowpath:
			quaternion = Quaternion.LookRotation(-CameraPath_0.GetPathDirection(float_8));
			quaternion *= Quaternion.Euler(base.transform.forward * (0f - CameraPath_0.GetPathTilt(float_8)));
			break;
		case orientationModes.followTransform:
		{
			if (orientationTarget == null)
			{
				return Quaternion.identity;
			}
			float nearestPoint = CameraPath_0.GetNearestPoint(orientationTarget.position);
			nearestPoint = Mathf.Clamp01(nearestPoint + nearestOffset);
			Vector3 pathPosition = CameraPath_0.GetPathPosition(nearestPoint);
			Vector3 forward = orientationTarget.transform.position - pathPosition;
			quaternion = Quaternion.LookRotation(forward);
			break;
		}
		case orientationModes.twoDimentions:
			quaternion = Quaternion.LookRotation(Vector3.forward);
			break;
		case orientationModes.fixedOrientation:
			quaternion = Quaternion.LookRotation(fixedOrientaion);
			break;
		case orientationModes.none:
			quaternion = animationObject.rotation;
			break;
		}
		return quaternion * base.transform.rotation;
	}

	private void Awake()
	{
		if (animationObject == null)
		{
			bool_0 = false;
		}
		else
		{
			camera_0 = animationObject.GetComponentInChildren<Camera>();
			bool_0 = camera_0 != null;
		}
		Camera[] allCameras = Camera.allCameras;
		if (allCameras.Length == 0)
		{
			Debug.LogWarning("Warning: There are no cameras in the scene");
			bool_0 = false;
		}
		if (!Boolean_2)
		{
			float_3 = 0f;
		}
		else
		{
			float_3 = 1f;
		}
		Vector3 eulerAngles = CameraPath_0.GetPathRotation(0f, false).eulerAngles;
		float_6 = eulerAngles.y;
		float_7 = eulerAngles.x;
	}

	private void OnEnable()
	{
		CameraPath_0.CameraPathEventList_0.CameraPathEventPoint += OnCustomEvent;
		CameraPath_0.CameraPathDelayList_0.CameraPathDelayEvent += OnDelayEvent;
		if (animationObject != null)
		{
			camera_0 = animationObject.GetComponentInChildren<Camera>();
		}
	}

	private void Start()
	{
		if (playOnStart)
		{
			Play();
		}
		if (Application.isPlaying && orientationTarget == null && (orientationMode == orientationModes.followTransform || orientationMode == orientationModes.target))
		{
			Debug.LogWarning("There has not been an orientation target specified in the Animation component of Camera Path.", base.transform);
		}
	}

	private void Update()
	{
		if (!Boolean_3)
		{
			if (bool_1)
			{
				UpdateAnimationTime();
				UpdateAnimation();
				UpdatePointReached();
			}
			else if (cameraPath_0.CameraPath_0 != null && float_3 >= 1f)
			{
				PlayNextAnimation();
			}
		}
	}

	private void LateUpdate()
	{
		if (Boolean_3)
		{
			if (bool_1)
			{
				UpdateAnimationTime();
				UpdateAnimation();
				UpdatePointReached();
			}
			else if (cameraPath_0.CameraPath_0 != null && float_3 >= 1f)
			{
				PlayNextAnimation();
			}
		}
	}

	private void OnDisable()
	{
		CleanUp();
	}

	private void OnDestroy()
	{
		CleanUp();
	}

	private void PlayNextAnimation()
	{
		if (cameraPath_0.CameraPath_0 != null)
		{
			cameraPath_0.CameraPath_0.GetComponent<CameraPathAnimator>().Play();
			float_3 = 0f;
			Stop();
		}
	}

	private void UpdateAnimation()
	{
		if (animationObject == null)
		{
			Debug.LogError("There is no animation object specified in the Camera Path Animator component. Nothing to animate.\nYou can find this component in the main camera path game object called " + base.gameObject.name + ".");
			Stop();
		}
		else
		{
			if (!bool_1)
			{
				return;
			}
			if (CameraPath_0.CameraPathSpeedList_0.Boolean_0)
			{
				float_1 = cameraPath_0.Single_0 / Mathf.Max(CameraPath_0.GetPathSpeed(float_3), minimumCameraSpeed);
			}
			else
			{
				float_1 = cameraPath_0.Single_0 / Mathf.Max(float_2 * CameraPath_0.GetPathEase(float_3), minimumCameraSpeed);
			}
			animationObject.position = CameraPath_0.GetPathPosition(float_3);
			if (orientationMode != orientationModes.none)
			{
				animationObject.rotation = GetAnimatedOrientation(float_3, false);
			}
			if (Boolean_3 && cameraPath_0.CameraPathFOVList_0.bool_1)
			{
				if (orientationMode != orientationModes.twoDimentions)
				{
					camera_0.fieldOfView = cameraPath_0.GetPathFOV(float_3);
				}
				else
				{
					camera_0.orthographicSize = cameraPath_0.GetPathFOV(float_3);
				}
			}
			CheckEvents();
		}
	}

	private void UpdatePointReached()
	{
		if (float_3 == float_4)
		{
			return;
		}
		if (Mathf.Abs(Single_2 - float_4) > 0.999f)
		{
			float_4 = Single_2;
			return;
		}
		for (int i = 0; i < CameraPath_0.Int32_1; i++)
		{
			CameraPathControlPoint cameraPathControlPoint = CameraPath_0[i];
			if ((cameraPathControlPoint.percentage >= float_4 && !(cameraPathControlPoint.percentage > Single_2)) || (cameraPathControlPoint.percentage >= Single_2 && cameraPathControlPoint.percentage <= float_4))
			{
				if (animationPointReachedEventHandler_0 != null)
				{
					animationPointReachedEventHandler_0();
				}
				if (animationPointReachedWithNumberEventHandler_0 != null)
				{
					animationPointReachedWithNumberEventHandler_0(i);
				}
			}
		}
		float_4 = Single_2;
	}

	private void UpdateAnimationTime()
	{
		UpdateAnimationTime(true);
	}

	private void UpdateAnimationTime(bool bool_3)
	{
		if (orientationMode == orientationModes.followTransform)
		{
			return;
		}
		if (float_5 > 0f)
		{
			float_5 += 0f - Time.deltaTime;
			return;
		}
		if (bool_3)
		{
			switch (animationMode)
			{
			case animationModes.once:
				if (float_3 >= 1f)
				{
					bool_1 = false;
					if (animationFinishedEventHandler_0 != null)
					{
						animationFinishedEventHandler_0();
					}
				}
				else
				{
					float_3 += Time.deltaTime * (1f / float_1);
				}
				break;
			case animationModes.loop:
				if (float_3 >= 1f)
				{
					float_3 = 0f;
					float_4 = 0f;
					if (animationLoopedEventHandler_0 != null)
					{
						animationLoopedEventHandler_0();
					}
				}
				float_3 += Time.deltaTime * (1f / float_1);
				break;
			case animationModes.reverse:
				if (float_3 <= 0f)
				{
					float_3 = 0f;
					bool_1 = false;
					if (animationFinishedEventHandler_0 != null)
					{
						animationFinishedEventHandler_0();
					}
				}
				else
				{
					float_3 += (0f - Time.deltaTime) * (1f / float_1);
				}
				break;
			case animationModes.reverseLoop:
				if (float_3 <= 0f)
				{
					float_3 = 1f;
					float_4 = 1f;
					if (animationLoopedEventHandler_0 != null)
					{
						animationLoopedEventHandler_0();
					}
				}
				float_3 += (0f - Time.deltaTime) * (1f / float_1);
				break;
			case animationModes.pingPong:
			{
				float num = Time.deltaTime * (1f / float_1);
				float_3 += num * float_0;
				if (float_3 >= 1f)
				{
					float_3 = 1f - num;
					float_4 = 1f;
					float_0 = -1f;
					if (animationPingPongEventHandler_0 != null)
					{
						animationPingPongEventHandler_0();
					}
				}
				if (float_3 <= 0f)
				{
					float_3 = num;
					float_4 = 0f;
					float_0 = 1f;
					if (animationPingPongEventHandler_0 != null)
					{
						animationPingPongEventHandler_0();
					}
				}
				break;
			}
			}
		}
		float_3 = Mathf.Clamp01(float_3);
	}

	private Quaternion GetMouseLook()
	{
		if (animationObject == null)
		{
			return Quaternion.identity;
		}
		float_6 += Input.GetAxis("Mouse X") * sensitivity;
		float_7 += (0f - Input.GetAxis("Mouse Y")) * sensitivity;
		float_7 = Mathf.Clamp(float_7, minX, maxX);
		return Quaternion.Euler(new Vector3(float_7, float_6, 0f));
	}

	private void CheckEvents()
	{
		CameraPath_0.CheckEvents(float_3);
	}

	private void CleanUp()
	{
		CameraPath_0.CameraPathEventList_0.CameraPathEventPoint += OnCustomEvent;
		CameraPath_0.CameraPathDelayList_0.CameraPathDelayEvent += OnDelayEvent;
	}

	private void OnDelayEvent(float float_8)
	{
		if (float_8 > 0f)
		{
			float_5 = float_8;
		}
		else
		{
			Pause();
		}
	}

	private void OnCustomEvent(string string_0)
	{
		if (animationCustomEventHandler_0 != null)
		{
			animationCustomEventHandler_0(string_0);
		}
	}
}
