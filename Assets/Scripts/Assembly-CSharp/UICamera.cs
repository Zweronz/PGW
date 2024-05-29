using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	public enum ControlScheme
	{
		Mouse = 0,
		Touch = 1,
		Controller = 2
	}

	public enum ClickNotification
	{
		None = 0,
		Always = 1,
		BasedOnDelta = 2
	}

	public class MouseOrTouch
	{
		public Vector2 vector2_0;

		public Vector2 vector2_1;

		public Vector2 vector2_2;

		public Vector2 vector2_3;

		public Camera camera_0;

		public GameObject gameObject_0;

		public GameObject gameObject_1;

		public GameObject gameObject_2;

		public GameObject gameObject_3;

		public float float_0;

		public ClickNotification clickNotification_0 = ClickNotification.Always;

		public bool bool_0 = true;

		public bool bool_1;

		public bool bool_2;
	}

	public enum EventType
	{
		World_3D = 0,
		UI_3D = 1,
		World_2D = 2,
		UI_2D = 3
	}

	private struct DepthEntry
	{
		public int int_0;

		public RaycastHit raycastHit_0;

		public Vector3 vector3_0;

		public GameObject gameObject_0;
	}

	public delegate void OnScreenResize();

	public delegate void OnCustomInput();

	public static BetterList<UICamera> betterList_0 = new BetterList<UICamera>();

	public static OnScreenResize onScreenResize_0;

	public EventType eventType = EventType.UI_3D;

	public LayerMask eventReceiverMask = -1;

	public bool debug;

	public bool useMouse = true;

	public bool useTouch = true;

	public bool allowMultiTouch = true;

	public bool useKeyboard = true;

	public bool useController = true;

	public bool stickyTooltip = true;

	public float tooltipDelay = 1f;

	public float mouseDragThreshold = 4f;

	public float mouseClickThreshold = 10f;

	public float touchDragThreshold = 40f;

	public float touchClickThreshold = 40f;

	public float rangeDistance = -1f;

	public string scrollAxisName = "Mouse ScrollWheel";

	public string verticalAxisName = "Vertical";

	public string horizontalAxisName = "Horizontal";

	public KeyCode submitKey0 = KeyCode.Return;

	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	public KeyCode cancelKey0 = KeyCode.Escape;

	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	public static OnCustomInput onCustomInput_0;

	public static bool bool_0 = true;

	public static Vector2 vector2_0 = Vector2.zero;

	public static Vector3 vector3_0 = Vector3.zero;

	public static RaycastHit raycastHit_0;

	public static UICamera uicamera_0 = null;

	public static Camera camera_0 = null;

	public static ControlScheme controlScheme_0 = ControlScheme.Mouse;

	public static int int_0 = -1;

	public static KeyCode keyCode_0 = KeyCode.None;

	public static MouseOrTouch mouseOrTouch_0 = null;

	public static bool bool_1 = false;

	public static GameObject gameObject_0;

	public static GameObject gameObject_1;

	private static GameObject gameObject_2 = null;

	private static GameObject gameObject_3 = null;

	private static ControlScheme controlScheme_1 = ControlScheme.Controller;

	private static MouseOrTouch[] mouseOrTouch_1 = new MouseOrTouch[3]
	{
		new MouseOrTouch(),
		new MouseOrTouch(),
		new MouseOrTouch()
	};

	private static GameObject gameObject_4;

	public static MouseOrTouch mouseOrTouch_2 = new MouseOrTouch();

	private static float float_0 = 0f;

	private static Dictionary<int, MouseOrTouch> dictionary_0 = new Dictionary<int, MouseOrTouch>();

	private static int int_1 = 0;

	private static int int_2 = 0;

	private GameObject gameObject_5;

	private Camera camera_1;

	private float float_1;

	private float float_2;

	public static bool bool_2 = false;

	public static GameObject gameObject_6;

	private static DepthEntry depthEntry_0 = default(DepthEntry);

	private static BetterList<DepthEntry> betterList_1 = new BetterList<DepthEntry>();

	private static Plane plane_0 = new Plane(Vector3.back, 0f);

	private static bool bool_3 = false;

	[CompilerGenerated]
	private static bool bool_4;

	[CompilerGenerated]
	private static BetterList<DepthEntry>.CompareFunc compareFunc_0;

	[CompilerGenerated]
	private static BetterList<DepthEntry>.CompareFunc compareFunc_1;

	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool Boolean_0
	{
		get
		{
			return true;
		}
	}

	public static Ray Ray_0
	{
		get
		{
			return (!(camera_0 != null) || mouseOrTouch_0 == null) ? default(Ray) : camera_0.ScreenPointToRay(mouseOrTouch_0.vector2_0);
		}
	}

	private bool Boolean_1
	{
		get
		{
			return UICamera_0 == this;
		}
	}

	public Camera Camera_0
	{
		get
		{
			if (camera_1 == null)
			{
				camera_1 = base.GetComponent<Camera>();
			}
			return camera_1;
		}
	}

	public static GameObject GameObject_0
	{
		get
		{
			return gameObject_2;
		}
		set
		{
			SetSelection(value, controlScheme_0);
		}
	}

	public static int Int32_0
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, MouseOrTouch> item in dictionary_0)
			{
				if (item.Value.gameObject_2 != null)
				{
					num++;
				}
			}
			for (int i = 0; i < mouseOrTouch_1.Length; i++)
			{
				if (mouseOrTouch_1[i].gameObject_2 != null)
				{
					num++;
				}
			}
			if (mouseOrTouch_2.gameObject_2 != null)
			{
				num++;
			}
			return num;
		}
	}

	public static int Int32_1
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, MouseOrTouch> item in dictionary_0)
			{
				if (item.Value.gameObject_3 != null)
				{
					num++;
				}
			}
			for (int i = 0; i < mouseOrTouch_1.Length; i++)
			{
				if (mouseOrTouch_1[i].gameObject_3 != null)
				{
					num++;
				}
			}
			if (mouseOrTouch_2.gameObject_3 != null)
			{
				num++;
			}
			return num;
		}
	}

	public static Camera Camera_1
	{
		get
		{
			UICamera uICamera_ = UICamera_0;
			return (!(uICamera_ != null)) ? null : uICamera_.Camera_0;
		}
	}

	public static UICamera UICamera_0
	{
		get
		{
			int num = 0;
			UICamera uICamera;
			while (true)
			{
				if (num < betterList_0.size)
				{
					uICamera = betterList_0.buffer[num];
					if (!(uICamera == null) && uICamera.enabled && NGUITools.GetActive(uICamera.gameObject))
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return uICamera;
		}
	}

	internal static bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	public static bool IsPressed(GameObject gameObject_7)
	{
		int num = 0;
		while (true)
		{
			if (num < 3)
			{
				if (mouseOrTouch_1[num].gameObject_2 == gameObject_7)
				{
					break;
				}
				num++;
				continue;
			}
			foreach (KeyValuePair<int, MouseOrTouch> item in dictionary_0)
			{
				if (item.Value.gameObject_2 == gameObject_7)
				{
					return true;
				}
			}
			if (mouseOrTouch_2.gameObject_2 == gameObject_7)
			{
				return true;
			}
			return false;
		}
		return true;
	}

	protected static void SetSelection(GameObject gameObject_7, ControlScheme controlScheme_2)
	{
		if (gameObject_3 != null)
		{
			gameObject_3 = gameObject_7;
		}
		else
		{
			if (!(gameObject_2 != gameObject_7))
			{
				return;
			}
			gameObject_3 = gameObject_7;
			controlScheme_1 = controlScheme_2;
			if (betterList_0.size > 0)
			{
				UICamera uICamera = ((!(gameObject_3 != null)) ? betterList_0[0] : FindCameraForLayer(gameObject_3.layer));
				if (uICamera != null)
				{
					uICamera.StartCoroutine(uICamera.ChangeSelection());
				}
			}
		}
	}

	private IEnumerator ChangeSelection()
	{
		yield return new WaitForEndOfFrame();
		Notify(gameObject_2, "OnSelect", false);
		gameObject_2 = gameObject_3;
		gameObject_3 = null;
		if (gameObject_2 != null)
		{
			uicamera_0 = this;
			camera_0 = camera_1;
			controlScheme_0 = controlScheme_1;
			bool_1 = gameObject_2.GetComponent<UIInput>() != null;
			Notify(gameObject_2, "OnSelect", true);
			uicamera_0 = null;
		}
		else
		{
			bool_1 = false;
		}
	}

	private static int CompareFunc(UICamera uicamera_1, UICamera uicamera_2)
	{
		if (uicamera_1.Camera_0.depth < uicamera_2.Camera_0.depth)
		{
			return 1;
		}
		if (uicamera_1.Camera_0.depth > uicamera_2.Camera_0.depth)
		{
			return -1;
		}
		return 0;
	}

	public static bool Raycast(Vector3 vector3_1)
	{
		int num = 0;
		GameObject gameObject4;
		while (true)
		{
			if (num < betterList_0.size)
			{
				UICamera uICamera = betterList_0.buffer[num];
				if (uICamera.enabled && NGUITools.GetActive(uICamera.gameObject))
				{
					camera_0 = uICamera.Camera_0;
					Vector3 vector = camera_0.ScreenToViewportPoint(vector3_1);
					if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y) && !(vector.x < 0f) && !(vector.x > 1f) && !(vector.y < 0f) && vector.y <= 1f)
					{
						Ray ray = camera_0.ScreenPointToRay(vector3_1);
						int layerMask = camera_0.cullingMask & (int)uICamera.eventReceiverMask;
						float enter = ((!(uICamera.rangeDistance > 0f)) ? (camera_0.farClipPlane - camera_0.nearClipPlane) : uICamera.rangeDistance);
						if (uICamera.eventType == EventType.World_3D)
						{
							if (Physics.Raycast(ray, out raycastHit_0, enter, layerMask))
							{
								vector3_0 = raycastHit_0.point;
								gameObject_6 = raycastHit_0.collider.gameObject;
								return true;
							}
						}
						else if (uICamera.eventType == EventType.UI_3D)
						{
							RaycastHit[] array = Physics.RaycastAll(ray, enter, layerMask);
							if (array.Length > 1)
							{
								for (int i = 0; i < array.Length; i++)
								{
									GameObject gameObject = array[i].collider.gameObject;
									UIWidget component = gameObject.GetComponent<UIWidget>();
									if (component != null)
									{
										if (!component.Boolean_2 || (component.hitCheck_0 != null && !component.hitCheck_0(array[i].point)))
										{
											continue;
										}
									}
									else
									{
										UIRect uIRect = NGUITools.FindInParents<UIRect>(gameObject);
										if (uIRect != null && !(uIRect.finalAlpha >= 0.001f))
										{
											continue;
										}
									}
									depthEntry_0.int_0 = NGUITools.CalculateRaycastDepth(gameObject);
									if (depthEntry_0.int_0 != int.MaxValue)
									{
										depthEntry_0.raycastHit_0 = array[i];
										depthEntry_0.vector3_0 = array[i].point;
										depthEntry_0.gameObject_0 = array[i].collider.gameObject;
										betterList_1.Add(depthEntry_0);
									}
								}
								betterList_1.Sort((DepthEntry depthEntry_1, DepthEntry depthEntry_2) => depthEntry_2.int_0.CompareTo(depthEntry_1.int_0));
								for (int j = 0; j < betterList_1.size; j++)
								{
									if (IsVisible(ref betterList_1.buffer[j]))
									{
										raycastHit_0 = betterList_1[j].raycastHit_0;
										gameObject_6 = betterList_1[j].gameObject_0;
										vector3_0 = betterList_1[j].vector3_0;
										betterList_1.Clear();
										return true;
									}
								}
								betterList_1.Clear();
							}
							else if (array.Length == 1)
							{
								GameObject gameObject2 = array[0].collider.gameObject;
								UIWidget component2 = gameObject2.GetComponent<UIWidget>();
								if (component2 != null)
								{
									if (!component2.Boolean_2 || (component2.hitCheck_0 != null && !component2.hitCheck_0(array[0].point)))
									{
										goto IL_05bb;
									}
								}
								else
								{
									UIRect uIRect2 = NGUITools.FindInParents<UIRect>(gameObject2);
									if (uIRect2 != null && !(uIRect2.finalAlpha >= 0.001f))
									{
										goto IL_05bb;
									}
								}
								if (IsVisible(array[0].point, array[0].collider.gameObject))
								{
									raycastHit_0 = array[0];
									vector3_0 = array[0].point;
									gameObject_6 = raycastHit_0.collider.gameObject;
									return true;
								}
							}
						}
						else if (uICamera.eventType == EventType.World_2D)
						{
							if (plane_0.Raycast(ray, out enter))
							{
								Vector3 point = ray.GetPoint(enter);
								Collider2D collider2D = Physics2D.OverlapPoint(point, layerMask);
								if ((bool)collider2D)
								{
									vector3_0 = point;
									gameObject_6 = collider2D.gameObject;
									return true;
								}
							}
						}
						else if (uICamera.eventType == EventType.UI_2D && plane_0.Raycast(ray, out enter))
						{
							vector3_0 = ray.GetPoint(enter);
							Collider2D[] array2 = Physics2D.OverlapPointAll(vector3_0, layerMask);
							if (array2.Length > 1)
							{
								for (int k = 0; k < array2.Length; k++)
								{
									GameObject gameObject3 = array2[k].gameObject;
									UIWidget component3 = gameObject3.GetComponent<UIWidget>();
									if (component3 != null)
									{
										if (!component3.Boolean_2 || (component3.hitCheck_0 != null && !component3.hitCheck_0(vector3_0)))
										{
											continue;
										}
									}
									else
									{
										UIRect uIRect3 = NGUITools.FindInParents<UIRect>(gameObject3);
										if (uIRect3 != null && !(uIRect3.finalAlpha >= 0.001f))
										{
											continue;
										}
									}
									depthEntry_0.int_0 = NGUITools.CalculateRaycastDepth(gameObject3);
									if (depthEntry_0.int_0 != int.MaxValue)
									{
										depthEntry_0.gameObject_0 = gameObject3;
										depthEntry_0.vector3_0 = vector3_0;
										betterList_1.Add(depthEntry_0);
									}
								}
								betterList_1.Sort((DepthEntry depthEntry_1, DepthEntry depthEntry_2) => depthEntry_2.int_0.CompareTo(depthEntry_1.int_0));
								for (int l = 0; l < betterList_1.size; l++)
								{
									if (IsVisible(ref betterList_1.buffer[l]))
									{
										gameObject_6 = betterList_1[l].gameObject_0;
										betterList_1.Clear();
										return true;
									}
								}
								betterList_1.Clear();
							}
							else if (array2.Length == 1)
							{
								gameObject4 = array2[0].gameObject;
								UIWidget component4 = gameObject4.GetComponent<UIWidget>();
								if (component4 != null)
								{
									if (!component4.Boolean_2 || (component4.hitCheck_0 != null && !component4.hitCheck_0(vector3_0)))
									{
										goto IL_05bb;
									}
								}
								else
								{
									UIRect uIRect4 = NGUITools.FindInParents<UIRect>(gameObject4);
									if (uIRect4 != null && !(uIRect4.finalAlpha >= 0.001f))
									{
										goto IL_05bb;
									}
								}
								if (IsVisible(vector3_0, gameObject4))
								{
									break;
								}
							}
						}
					}
				}
				goto IL_05bb;
			}
			return false;
			IL_05bb:
			num++;
		}
		gameObject_6 = gameObject4;
		return true;
	}

	private static bool IsVisible(Vector3 vector3_1, GameObject gameObject_7)
	{
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(gameObject_7);
		while (true)
		{
			if (uIPanel != null)
			{
				if (!uIPanel.IsVisible(vector3_1))
				{
					break;
				}
				uIPanel = uIPanel.UIPanel_0;
				continue;
			}
			return true;
		}
		return false;
	}

	private static bool IsVisible(ref DepthEntry depthEntry_1)
	{
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(depthEntry_1.gameObject_0);
		while (true)
		{
			if (uIPanel != null)
			{
				if (!uIPanel.IsVisible(depthEntry_1.vector3_0))
				{
					break;
				}
				uIPanel = uIPanel.UIPanel_0;
				continue;
			}
			return true;
		}
		return false;
	}

	public static bool IsHighlighted(GameObject gameObject_7)
	{
		if (controlScheme_0 == ControlScheme.Mouse)
		{
			return gameObject_6 == gameObject_7;
		}
		if (controlScheme_0 == ControlScheme.Controller)
		{
			return GameObject_0 == gameObject_7;
		}
		return false;
	}

	public static UICamera FindCameraForLayer(int int_3)
	{
		int num = 1 << int_3;
		int num2 = 0;
		UICamera uICamera;
		while (true)
		{
			if (num2 < betterList_0.size)
			{
				uICamera = betterList_0.buffer[num2];
				Camera camera = uICamera.Camera_0;
				if (camera != null && (camera.cullingMask & num) != 0)
				{
					break;
				}
				num2++;
				continue;
			}
			return null;
		}
		return uICamera;
	}

	private static int GetDirection(KeyCode keyCode_1, KeyCode keyCode_2)
	{
		if (Input.GetKeyDown(keyCode_1))
		{
			return 1;
		}
		if (Input.GetKeyDown(keyCode_2))
		{
			return -1;
		}
		return 0;
	}

	private static int GetDirection(KeyCode keyCode_1, KeyCode keyCode_2, KeyCode keyCode_3, KeyCode keyCode_4)
	{
		if (!Input.GetKeyDown(keyCode_1) && !Input.GetKeyDown(keyCode_2))
		{
			if (!Input.GetKeyDown(keyCode_3) && !Input.GetKeyDown(keyCode_4))
			{
				return 0;
			}
			return -1;
		}
		return 1;
	}

	private static int GetDirection(string string_0)
	{
		float single_ = RealTime.Single_0;
		if (float_0 < single_ && !string.IsNullOrEmpty(string_0))
		{
			float axis = Input.GetAxis(string_0);
			if (axis > 0.75f)
			{
				float_0 = single_ + 0.25f;
				return 1;
			}
			if (axis < -0.75f)
			{
				float_0 = single_ + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	public static void Notify(GameObject gameObject_7, string string_0, object object_0)
	{
		if (bool_3)
		{
			return;
		}
		bool_3 = true;
		if (NGUITools.GetActive(gameObject_7))
		{
			gameObject_7.SendMessage(string_0, object_0, SendMessageOptions.DontRequireReceiver);
			if (gameObject_0 != null && gameObject_0 != gameObject_7)
			{
				gameObject_0.SendMessage(string_0, object_0, SendMessageOptions.DontRequireReceiver);
			}
		}
		bool_3 = false;
	}

	public static MouseOrTouch GetMouse(int int_3)
	{
		return mouseOrTouch_1[int_3];
	}

	public static MouseOrTouch GetTouch(int int_3)
	{
		MouseOrTouch value = null;
		if (int_3 < 0)
		{
			return GetMouse(-int_3 - 1);
		}
		if (!dictionary_0.TryGetValue(int_3, out value))
		{
			value = new MouseOrTouch();
			value.bool_0 = true;
			dictionary_0.Add(int_3, value);
		}
		return value;
	}

	public static void RemoveTouch(int int_3)
	{
		dictionary_0.Remove(int_3);
	}

	private void Awake()
	{
		int_1 = Screen.width;
		int_2 = Screen.height;
		if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.WP8Player && Application.platform != RuntimePlatform.BB10Player)
		{
			if (Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360)
			{
				useMouse = false;
				useTouch = false;
				useKeyboard = false;
				useController = true;
			}
		}
		else
		{
			useMouse = false;
			useTouch = true;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				useKeyboard = false;
				useController = false;
			}
		}
		mouseOrTouch_1[0].vector2_0.x = Input.mousePosition.x;
		mouseOrTouch_1[0].vector2_0.y = Input.mousePosition.y;
		for (int i = 1; i < 3; i++)
		{
			mouseOrTouch_1[i].vector2_0 = mouseOrTouch_1[0].vector2_0;
			mouseOrTouch_1[i].vector2_1 = mouseOrTouch_1[0].vector2_0;
		}
		vector2_0 = mouseOrTouch_1[0].vector2_0;
	}

	private void OnEnable()
	{
		betterList_0.Add(this);
		betterList_0.Sort(CompareFunc);
	}

	private void OnDisable()
	{
		betterList_0.Remove(this);
	}

	private void Start()
	{
		if (eventType != 0 && Camera_0.transparencySortMode != TransparencySortMode.Orthographic)
		{
			Camera_0.transparencySortMode = TransparencySortMode.Orthographic;
		}
		if (Application.isPlaying)
		{
			Camera_0.eventMask = 0;
		}
		if (Boolean_1)
		{
			NGUIDebug.Boolean_0 = debug;
		}
	}

	private void Update()
	{
		if (!Boolean_1)
		{
			return;
		}
		uicamera_0 = this;
		if (useTouch)
		{
			ProcessTouches();
		}
		else if (useMouse)
		{
			ProcessMouse();
		}
		if (onCustomInput_0 != null)
		{
			onCustomInput_0();
		}
		if (useMouse && gameObject_2 != null)
		{
			if (cancelKey0 != 0 && Input.GetKeyDown(cancelKey0))
			{
				controlScheme_0 = ControlScheme.Controller;
				keyCode_0 = cancelKey0;
				GameObject_0 = null;
			}
			else if (cancelKey1 != 0 && Input.GetKeyDown(cancelKey1))
			{
				controlScheme_0 = ControlScheme.Controller;
				keyCode_0 = cancelKey1;
				GameObject_0 = null;
			}
		}
		if (gameObject_2 == null)
		{
			bool_1 = false;
		}
		if (gameObject_2 != null)
		{
			ProcessOthers();
		}
		if (useMouse && gameObject_4 != null)
		{
			float num = (string.IsNullOrEmpty(scrollAxisName) ? 0f : Input.GetAxis(scrollAxisName));
			if (num != 0f)
			{
				Notify(gameObject_4, "OnScroll", num);
			}
			if (bool_0 && float_1 != 0f && (float_1 < RealTime.Single_0 || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				gameObject_5 = gameObject_4;
				ShowTooltip(true);
			}
		}
		uicamera_0 = null;
	}

	private void LateUpdate()
	{
		if (!Boolean_1)
		{
			return;
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != int_1 || height != int_2)
		{
			int_1 = width;
			int_2 = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (onScreenResize_0 != null)
			{
				onScreenResize_0();
			}
		}
	}

	public void ProcessMouse()
	{
		vector2_0 = Input.mousePosition;
		mouseOrTouch_1[0].vector2_2 = vector2_0 - mouseOrTouch_1[0].vector2_0;
		mouseOrTouch_1[0].vector2_0 = vector2_0;
		bool flag = mouseOrTouch_1[0].vector2_2.sqrMagnitude > 0.001f;
		for (int i = 1; i < 3; i++)
		{
			mouseOrTouch_1[i].vector2_0 = mouseOrTouch_1[0].vector2_0;
			mouseOrTouch_1[i].vector2_2 = mouseOrTouch_1[0].vector2_2;
		}
		bool flag2 = false;
		bool flag3 = false;
		for (int j = 0; j < 3; j++)
		{
			if (Input.GetMouseButtonDown(j))
			{
				controlScheme_0 = ControlScheme.Mouse;
				flag3 = true;
				flag2 = true;
			}
			else if (Input.GetMouseButton(j))
			{
				controlScheme_0 = ControlScheme.Mouse;
				flag2 = true;
			}
		}
		if (flag2 || flag || float_2 < RealTime.Single_0)
		{
			float_2 = RealTime.Single_0 + 0.02f;
			if (!Raycast(Input.mousePosition))
			{
				gameObject_6 = gameObject_1;
			}
			if (gameObject_6 == null)
			{
				gameObject_6 = gameObject_0;
			}
			for (int k = 0; k < 3; k++)
			{
				mouseOrTouch_1[k].gameObject_1 = gameObject_6;
			}
		}
		bool flag4;
		if (flag4 = mouseOrTouch_1[0].gameObject_0 != mouseOrTouch_1[0].gameObject_1)
		{
			controlScheme_0 = ControlScheme.Mouse;
		}
		if (flag2)
		{
			float_1 = 0f;
		}
		else if (flag && (!stickyTooltip || flag4))
		{
			if (float_1 != 0f)
			{
				float_1 = RealTime.Single_0 + tooltipDelay;
			}
			else if (gameObject_5 != null)
			{
				ShowTooltip(false);
			}
		}
		if ((flag3 || !flag2) && gameObject_4 != null && flag4)
		{
			controlScheme_0 = ControlScheme.Mouse;
			if (gameObject_5 != null)
			{
				ShowTooltip(false);
			}
			Notify(gameObject_4, "OnHover", false);
			gameObject_4 = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			if (mouseButtonDown || mouseButtonUp)
			{
				controlScheme_0 = ControlScheme.Mouse;
			}
			mouseOrTouch_0 = mouseOrTouch_1[l];
			int_0 = -1 - l;
			keyCode_0 = (KeyCode)(323 + l);
			if (mouseButtonDown)
			{
				mouseOrTouch_0.camera_0 = camera_0;
			}
			else if (mouseOrTouch_0.gameObject_2 != null)
			{
				camera_0 = mouseOrTouch_0.camera_0;
			}
			ProcessTouch(mouseButtonDown, mouseButtonUp);
			keyCode_0 = KeyCode.None;
		}
		mouseOrTouch_0 = null;
		if (!flag2 && flag4)
		{
			controlScheme_0 = ControlScheme.Mouse;
			float_1 = RealTime.Single_0 + tooltipDelay;
			gameObject_4 = mouseOrTouch_1[0].gameObject_1;
			Notify(gameObject_4, "OnHover", true);
		}
		mouseOrTouch_1[0].gameObject_0 = mouseOrTouch_1[0].gameObject_1;
		for (int m = 1; m < 3; m++)
		{
			mouseOrTouch_1[m].gameObject_0 = mouseOrTouch_1[0].gameObject_0;
		}
	}

	public void ProcessTouches()
	{
		if (Boolean_2)
		{
			return;
		}
		controlScheme_0 = ControlScheme.Touch;
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			int_0 = ((!allowMultiTouch) ? 1 : touch.fingerId);
			mouseOrTouch_0 = GetTouch(int_0);
			bool flag = touch.phase == TouchPhase.Began || mouseOrTouch_0.bool_0;
			bool flag2 = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
			mouseOrTouch_0.bool_0 = false;
			mouseOrTouch_0.vector2_2 = ((flag || flag2) ? Vector2.zero : (touch.position - mouseOrTouch_0.vector2_0));
			mouseOrTouch_0.vector2_0 = touch.position;
			if (!Raycast(mouseOrTouch_0.vector2_0))
			{
				gameObject_6 = gameObject_1;
			}
			if (gameObject_6 == null)
			{
				gameObject_6 = gameObject_0;
			}
			mouseOrTouch_0.gameObject_0 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.gameObject_1 = gameObject_6;
			vector2_0 = mouseOrTouch_0.vector2_0;
			if (flag)
			{
				mouseOrTouch_0.camera_0 = camera_0;
			}
			else if (mouseOrTouch_0.gameObject_2 != null)
			{
				camera_0 = mouseOrTouch_0.camera_0;
			}
			if (touch.tapCount > 1)
			{
				mouseOrTouch_0.float_0 = RealTime.Single_0;
			}
			ProcessTouch(flag, flag2);
			if (flag2)
			{
				RemoveTouch(int_0);
			}
			mouseOrTouch_0.gameObject_0 = null;
			mouseOrTouch_0 = null;
			if (!allowMultiTouch)
			{
				break;
			}
		}
		if (Input.touchCount == 0 && useMouse)
		{
			ProcessMouse();
		}
	}

	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			int_0 = 1;
			mouseOrTouch_0 = mouseOrTouch_1[0];
			mouseOrTouch_0.bool_0 = mouseButtonDown;
			Vector2 vector = Input.mousePosition;
			mouseOrTouch_0.vector2_2 = ((!mouseButtonDown) ? (vector - mouseOrTouch_0.vector2_0) : Vector2.zero);
			mouseOrTouch_0.vector2_0 = vector;
			if (!Raycast(mouseOrTouch_0.vector2_0))
			{
				gameObject_6 = gameObject_1;
			}
			if (gameObject_6 == null)
			{
				gameObject_6 = gameObject_0;
			}
			mouseOrTouch_0.gameObject_0 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.gameObject_1 = gameObject_6;
			vector2_0 = mouseOrTouch_0.vector2_0;
			if (mouseButtonDown)
			{
				mouseOrTouch_0.camera_0 = camera_0;
			}
			else if (mouseOrTouch_0.gameObject_2 != null)
			{
				camera_0 = mouseOrTouch_0.camera_0;
			}
			ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				RemoveTouch(int_0);
			}
			mouseOrTouch_0.gameObject_0 = null;
			mouseOrTouch_0 = null;
		}
	}

	public void ProcessOthers()
	{
		int_0 = -100;
		mouseOrTouch_0 = mouseOrTouch_2;
		bool flag = false;
		bool flag2 = false;
		if (submitKey0 != 0 && Input.GetKeyDown(submitKey0))
		{
			keyCode_0 = submitKey0;
			flag = true;
		}
		if (submitKey1 != 0 && Input.GetKeyDown(submitKey1))
		{
			keyCode_0 = submitKey1;
			flag = true;
		}
		if (submitKey0 != 0 && Input.GetKeyUp(submitKey0))
		{
			keyCode_0 = submitKey0;
			flag2 = true;
		}
		if (submitKey1 != 0 && Input.GetKeyUp(submitKey1))
		{
			keyCode_0 = submitKey1;
			flag2 = true;
		}
		if (flag || flag2)
		{
			controlScheme_0 = ControlScheme.Controller;
			mouseOrTouch_0.gameObject_0 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.gameObject_1 = gameObject_2;
			ProcessTouch(flag, flag2);
			mouseOrTouch_0.gameObject_0 = null;
		}
		int num = 0;
		int num2 = 0;
		if (useKeyboard)
		{
			if (bool_1)
			{
				num += GetDirection(KeyCode.UpArrow, KeyCode.DownArrow);
				num2 += GetDirection(KeyCode.RightArrow, KeyCode.LeftArrow);
			}
			else
			{
				num += GetDirection(KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow);
				num2 += GetDirection(KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow);
			}
		}
		if (useController)
		{
			if (!string.IsNullOrEmpty(verticalAxisName))
			{
				num += GetDirection(verticalAxisName);
			}
			if (!string.IsNullOrEmpty(horizontalAxisName))
			{
				num2 += GetDirection(horizontalAxisName);
			}
		}
		if (num != 0)
		{
			controlScheme_0 = ControlScheme.Controller;
			Notify(gameObject_2, "OnKey", (num <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow);
		}
		if (num2 != 0)
		{
			controlScheme_0 = ControlScheme.Controller;
			Notify(gameObject_2, "OnKey", (num2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow);
		}
		if (useKeyboard && Input.GetKeyDown(KeyCode.Tab))
		{
			keyCode_0 = KeyCode.Tab;
			controlScheme_0 = ControlScheme.Controller;
			Notify(gameObject_2, "OnKey", KeyCode.Tab);
		}
		if (cancelKey0 != 0 && Input.GetKeyDown(cancelKey0))
		{
			keyCode_0 = cancelKey0;
			controlScheme_0 = ControlScheme.Controller;
			Notify(gameObject_2, "OnKey", KeyCode.Escape);
		}
		if (cancelKey1 != 0 && Input.GetKeyDown(cancelKey1))
		{
			keyCode_0 = cancelKey1;
			controlScheme_0 = ControlScheme.Controller;
			Notify(gameObject_2, "OnKey", KeyCode.Escape);
		}
		mouseOrTouch_0 = null;
		keyCode_0 = KeyCode.None;
	}

	public void ProcessTouch(bool bool_5, bool bool_6)
	{
		bool flag;
		float num = ((!(flag = controlScheme_0 == ControlScheme.Mouse)) ? touchDragThreshold : mouseDragThreshold);
		float num2 = ((!flag) ? touchClickThreshold : mouseClickThreshold);
		num *= num;
		num2 *= num2;
		if (bool_5)
		{
			if (gameObject_5 != null)
			{
				ShowTooltip(false);
			}
			mouseOrTouch_0.bool_1 = true;
			Notify(mouseOrTouch_0.gameObject_2, "OnPress", false);
			mouseOrTouch_0.gameObject_2 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.gameObject_3 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.clickNotification_0 = ClickNotification.BasedOnDelta;
			mouseOrTouch_0.vector2_3 = Vector2.zero;
			mouseOrTouch_0.bool_2 = false;
			Notify(mouseOrTouch_0.gameObject_2, "OnPress", true);
			if (mouseOrTouch_0.gameObject_2 != gameObject_2)
			{
				if (gameObject_5 != null)
				{
					ShowTooltip(false);
				}
				controlScheme_0 = ControlScheme.Touch;
				GameObject_0 = mouseOrTouch_0.gameObject_2;
			}
		}
		else if (mouseOrTouch_0.gameObject_2 != null && (mouseOrTouch_0.vector2_2.sqrMagnitude != 0f || mouseOrTouch_0.gameObject_1 != mouseOrTouch_0.gameObject_0))
		{
			mouseOrTouch_0.vector2_3 += mouseOrTouch_0.vector2_2;
			float sqrMagnitude = mouseOrTouch_0.vector2_3.sqrMagnitude;
			bool flag2 = false;
			if (!mouseOrTouch_0.bool_2 && mouseOrTouch_0.gameObject_0 != mouseOrTouch_0.gameObject_1)
			{
				mouseOrTouch_0.bool_2 = true;
				mouseOrTouch_0.vector2_2 = mouseOrTouch_0.vector2_3;
				bool_2 = true;
				Notify(mouseOrTouch_0.gameObject_3, "OnDragStart", null);
				Notify(mouseOrTouch_0.gameObject_0, "OnDragOver", mouseOrTouch_0.gameObject_3);
				bool_2 = false;
			}
			else if (!mouseOrTouch_0.bool_2 && num < sqrMagnitude)
			{
				flag2 = true;
				mouseOrTouch_0.bool_2 = true;
				mouseOrTouch_0.vector2_2 = mouseOrTouch_0.vector2_3;
			}
			if (mouseOrTouch_0.bool_2)
			{
				if (gameObject_5 != null)
				{
					ShowTooltip(false);
				}
				bool_2 = true;
				bool flag3 = mouseOrTouch_0.clickNotification_0 == ClickNotification.None;
				if (flag2)
				{
					Notify(mouseOrTouch_0.gameObject_3, "OnDragStart", null);
					Notify(mouseOrTouch_0.gameObject_1, "OnDragOver", mouseOrTouch_0.gameObject_3);
				}
				else if (mouseOrTouch_0.gameObject_0 != mouseOrTouch_0.gameObject_1)
				{
					Notify(mouseOrTouch_0.gameObject_0, "OnDragOut", mouseOrTouch_0.gameObject_3);
					Notify(mouseOrTouch_0.gameObject_1, "OnDragOver", mouseOrTouch_0.gameObject_3);
				}
				Notify(mouseOrTouch_0.gameObject_3, "OnDrag", mouseOrTouch_0.vector2_2);
				mouseOrTouch_0.gameObject_0 = mouseOrTouch_0.gameObject_1;
				bool_2 = false;
				if (flag3)
				{
					mouseOrTouch_0.clickNotification_0 = ClickNotification.None;
				}
				else if (mouseOrTouch_0.clickNotification_0 == ClickNotification.BasedOnDelta && num2 < sqrMagnitude)
				{
					mouseOrTouch_0.clickNotification_0 = ClickNotification.None;
				}
			}
		}
		if (!bool_6)
		{
			return;
		}
		mouseOrTouch_0.bool_1 = false;
		if (gameObject_5 != null)
		{
			ShowTooltip(false);
		}
		if (mouseOrTouch_0.gameObject_2 != null)
		{
			if (mouseOrTouch_0.bool_2)
			{
				Notify(mouseOrTouch_0.gameObject_0, "OnDragOut", mouseOrTouch_0.gameObject_3);
				Notify(mouseOrTouch_0.gameObject_3, "OnDragEnd", null);
			}
			Notify(mouseOrTouch_0.gameObject_2, "OnPress", false);
			if (flag)
			{
				Notify(mouseOrTouch_0.gameObject_1, "OnHover", true);
			}
			gameObject_4 = mouseOrTouch_0.gameObject_1;
			if (!(mouseOrTouch_0.gameObject_3 == mouseOrTouch_0.gameObject_1) && (controlScheme_0 == ControlScheme.Controller || mouseOrTouch_0.clickNotification_0 == ClickNotification.None || mouseOrTouch_0.vector2_3.sqrMagnitude >= num))
			{
				if (mouseOrTouch_0.bool_2)
				{
					Notify(mouseOrTouch_0.gameObject_1, "OnDrop", mouseOrTouch_0.gameObject_3);
				}
			}
			else
			{
				if (mouseOrTouch_0.gameObject_2 != gameObject_2)
				{
					gameObject_3 = null;
					gameObject_2 = mouseOrTouch_0.gameObject_2;
					Notify(mouseOrTouch_0.gameObject_2, "OnSelect", true);
				}
				else
				{
					gameObject_3 = null;
					gameObject_2 = mouseOrTouch_0.gameObject_2;
				}
				if (mouseOrTouch_0.clickNotification_0 != 0 && mouseOrTouch_0.gameObject_2 == mouseOrTouch_0.gameObject_1)
				{
					float single_ = RealTime.Single_0;
					Notify(mouseOrTouch_0.gameObject_2, "OnClick", null);
					if (mouseOrTouch_0.float_0 + 0.35f > single_)
					{
						Notify(mouseOrTouch_0.gameObject_2, "OnDoubleClick", null);
					}
					mouseOrTouch_0.float_0 = single_;
				}
			}
		}
		mouseOrTouch_0.bool_2 = false;
		mouseOrTouch_0.gameObject_2 = null;
		mouseOrTouch_0.gameObject_3 = null;
	}

	public void ShowTooltip(bool bool_5)
	{
		float_1 = 0f;
		Notify(gameObject_5, "OnTooltip", bool_5);
		if (!bool_5)
		{
			gameObject_5 = null;
		}
	}

	private void OnApplicationPause()
	{
		MouseOrTouch mouseOrTouch = mouseOrTouch_0;
		if (useTouch)
		{
			BetterList<int> betterList = new BetterList<int>();
			foreach (KeyValuePair<int, MouseOrTouch> item in dictionary_0)
			{
				if (item.Value != null && (bool)item.Value.gameObject_2)
				{
					mouseOrTouch_0 = item.Value;
					int_0 = item.Key;
					controlScheme_0 = ControlScheme.Touch;
					mouseOrTouch_0.clickNotification_0 = ClickNotification.None;
					ProcessTouch(false, true);
					betterList.Add(int_0);
				}
			}
			for (int i = 0; i < betterList.size; i++)
			{
				RemoveTouch(betterList[i]);
			}
		}
		if (useMouse)
		{
			for (int j = 0; j < 3; j++)
			{
				if ((bool)mouseOrTouch_1[j].gameObject_2)
				{
					mouseOrTouch_0 = mouseOrTouch_1[j];
					int_0 = -1 - j;
					keyCode_0 = (KeyCode)(323 + j);
					controlScheme_0 = ControlScheme.Mouse;
					mouseOrTouch_0.clickNotification_0 = ClickNotification.None;
					ProcessTouch(false, true);
				}
			}
		}
		if (useController && (bool)mouseOrTouch_2.gameObject_2)
		{
			mouseOrTouch_0 = mouseOrTouch_2;
			int_0 = -100;
			controlScheme_0 = ControlScheme.Controller;
			mouseOrTouch_0.gameObject_0 = mouseOrTouch_0.gameObject_1;
			mouseOrTouch_0.gameObject_1 = gameObject_2;
			mouseOrTouch_0.clickNotification_0 = ClickNotification.None;
			ProcessTouch(false, true);
			mouseOrTouch_0.gameObject_0 = null;
		}
		mouseOrTouch_0 = mouseOrTouch;
	}
}
