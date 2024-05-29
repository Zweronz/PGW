using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class NGUITools
{
	private static AudioListener audioListener_0;

	private static bool bool_0 = false;

	private static float float_0 = 1f;

	private static Vector3[] vector3_0 = new Vector3[4];

	public static float Single_0
	{
		get
		{
			if (!bool_0)
			{
				bool_0 = true;
				float_0 = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return float_0;
		}
		set
		{
			if (float_0 != value)
			{
				bool_0 = true;
				float_0 = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	public static bool Boolean_0
	{
		get
		{
			return true;
		}
	}

	public static string String_0
	{
		get
		{
			TextEditor textEditor = new TextEditor();
			textEditor.Paste();
			return textEditor.content.text;
		}
		set
		{
			TextEditor textEditor = new TextEditor();
			textEditor.content = new GUIContent(value);
			textEditor.OnFocus();
			textEditor.Copy();
		}
	}

	public static Vector2 Vector2_0
	{
		get
		{
			return new Vector2(Screen.width, Screen.height);
		}
	}

	public static AudioSource PlaySound(AudioClip audioClip_0)
	{
		return PlaySound(audioClip_0, 1f, 1f);
	}

	public static AudioSource PlaySound(AudioClip audioClip_0, float float_1)
	{
		return PlaySound(audioClip_0, float_1, 1f);
	}

	public static AudioSource PlaySound(AudioClip audioClip_0, float float_1, float float_2)
	{
		float_1 *= Single_0;
		if (audioClip_0 != null && float_1 > 0.01f)
		{
			if (audioListener_0 == null || !GetActive(audioListener_0))
			{
				AudioListener[] array = UnityEngine.Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (GetActive(array[i]))
						{
							audioListener_0 = array[i];
							break;
						}
					}
				}
				if (audioListener_0 == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = UnityEngine.Object.FindObjectOfType(typeof(Camera)) as Camera;
					}
					if (camera != null)
					{
						audioListener_0 = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (audioListener_0 != null && audioListener_0.enabled && GetActive(audioListener_0.gameObject))
			{
				AudioSource audioSource = audioListener_0.GetComponent<AudioSource>();
				if (audioSource == null)
				{
					audioSource = audioListener_0.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = float_2;
				audioSource.PlayOneShot(audioClip_0, float_1);
				return audioSource;
			}
		}
		return null;
	}

	public static int RandomRange(int int_0, int int_1)
	{
		if (int_0 == int_1)
		{
			return int_0;
		}
		return UnityEngine.Random.Range(int_0, int_1 + 1);
	}

	public static string GetHierarchy(GameObject gameObject_0)
	{
		if (gameObject_0 == null)
		{
			return string.Empty;
		}
		string text = gameObject_0.name;
		while (gameObject_0.transform.parent != null)
		{
			gameObject_0 = gameObject_0.transform.parent.gameObject;
			text = gameObject_0.name + "\\" + text;
		}
		return text;
	}

	public static T[] FindActive<T>() where T : Component
	{
		return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
	}

	public static Camera FindCameraForLayer(int int_0)
	{
		int num = 1 << int_0;
		int num2 = 0;
		Camera camera_;
		while (true)
		{
			if (num2 < UICamera.betterList_0.size)
			{
				camera_ = UICamera.betterList_0.buffer[num2].Camera_0;
				if ((bool)camera_ && (camera_.cullingMask & num) != 0)
				{
					break;
				}
				num2++;
				continue;
			}
			camera_ = Camera.main;
			if ((bool)camera_ && (camera_.cullingMask & num) != 0)
			{
				return camera_;
			}
			Camera[] array = new Camera[Camera.allCamerasCount];
			int allCameras = Camera.GetAllCameras(array);
			int num3 = 0;
			while (true)
			{
				if (num3 < allCameras)
				{
					camera_ = array[num3];
					if ((bool)camera_ && camera_.enabled && (camera_.cullingMask & num) != 0)
					{
						break;
					}
					num3++;
					continue;
				}
				return null;
			}
			return camera_;
		}
		return camera_;
	}

	public static void AddWidgetCollider(GameObject gameObject_0)
	{
		AddWidgetCollider(gameObject_0, false);
	}

	public static void AddWidgetCollider(GameObject gameObject_0, bool bool_1)
	{
		if (!(gameObject_0 != null))
		{
			return;
		}
		Collider component = gameObject_0.GetComponent<Collider>();
		BoxCollider boxCollider = component as BoxCollider;
		if (boxCollider != null)
		{
			UpdateWidgetCollider(boxCollider, bool_1);
		}
		else
		{
			if (component != null)
			{
				return;
			}
			BoxCollider2D component2 = gameObject_0.GetComponent<BoxCollider2D>();
			if (component2 != null)
			{
				UpdateWidgetCollider(component2, bool_1);
				return;
			}
			UICamera uICamera = UICamera.FindCameraForLayer(gameObject_0.layer);
			if (uICamera != null && (uICamera.eventType == UICamera.EventType.World_2D || uICamera.eventType == UICamera.EventType.UI_2D))
			{
				component2 = gameObject_0.AddComponent<BoxCollider2D>();
				component2.isTrigger = true;
				UIWidget component3 = gameObject_0.GetComponent<UIWidget>();
				if (component3 != null)
				{
					component3.bool_6 = true;
				}
				UpdateWidgetCollider(component2, bool_1);
			}
			else
			{
				boxCollider = gameObject_0.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
				UIWidget component4 = gameObject_0.GetComponent<UIWidget>();
				if (component4 != null)
				{
					component4.bool_6 = true;
				}
				UpdateWidgetCollider(boxCollider, bool_1);
			}
		}
	}

	public static void UpdateWidgetCollider(GameObject gameObject_0)
	{
		UpdateWidgetCollider(gameObject_0, false);
	}

	public static void UpdateWidgetCollider(GameObject gameObject_0, bool bool_1)
	{
		if (!(gameObject_0 != null))
		{
			return;
		}
		BoxCollider component = gameObject_0.GetComponent<BoxCollider>();
		if (component != null)
		{
			UpdateWidgetCollider(component, bool_1);
			return;
		}
		BoxCollider2D component2 = gameObject_0.GetComponent<BoxCollider2D>();
		if (component2 != null)
		{
			UpdateWidgetCollider(component2, bool_1);
		}
	}

	public static void UpdateWidgetCollider(BoxCollider boxCollider_0, bool bool_1)
	{
		if (!(boxCollider_0 != null))
		{
			return;
		}
		GameObject gameObject = boxCollider_0.gameObject;
		UIWidget component = gameObject.GetComponent<UIWidget>();
		if (component != null)
		{
			Vector4 vector4_ = component.Vector4_0;
			if (vector4_.x == 0f && vector4_.y == 0f && vector4_.z == 1f && vector4_.w == 1f)
			{
				Vector3[] vector3_ = component.Vector3_2;
				boxCollider_0.center = Vector3.Lerp(vector3_[0], vector3_[2], 0.5f);
				boxCollider_0.size = vector3_[2] - vector3_[0];
			}
			else
			{
				Vector4 vector4_2 = component.Vector4_2;
				boxCollider_0.center = new Vector3((vector4_2.x + vector4_2.z) * 0.5f, (vector4_2.y + vector4_2.w) * 0.5f);
				boxCollider_0.size = new Vector3(vector4_2.z - vector4_2.x, vector4_2.w - vector4_2.y);
			}
		}
		else
		{
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, bool_1);
			boxCollider_0.center = bounds.center;
			boxCollider_0.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
		}
	}

	public static void UpdateWidgetCollider(BoxCollider2D boxCollider2D_0, bool bool_1)
	{
		if (boxCollider2D_0 != null)
		{
			GameObject gameObject = boxCollider2D_0.gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector3[] vector3_ = component.Vector3_2;
				boxCollider2D_0.offset = Vector3.Lerp(vector3_[0], vector3_[2], 0.5f);
				boxCollider2D_0.size = vector3_[2] - vector3_[0];
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, bool_1);
				boxCollider2D_0.offset = bounds.center;
				boxCollider2D_0.size = new Vector2(bounds.size.x, bounds.size.y);
			}
		}
	}

	public static string GetTypeName<T>()
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static string GetTypeName(UnityEngine.Object object_0)
	{
		if (object_0 == null)
		{
			return "Null";
		}
		string text = object_0.GetType().ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static void RegisterUndo(UnityEngine.Object object_0, string string_0)
	{
	}

	public static void SetDirty(UnityEngine.Object object_0)
	{
	}

	public static GameObject AddChild(GameObject gameObject_0)
	{
		return AddChild(gameObject_0, true);
	}

	public static GameObject AddChild(GameObject gameObject_0, bool bool_1)
	{
		GameObject gameObject = new GameObject();
		if (gameObject_0 != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = gameObject_0.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = gameObject_0.layer;
		}
		return gameObject;
	}

	public static GameObject AddChild(GameObject gameObject_0, GameObject gameObject_1)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(gameObject_1) as GameObject;
		if (gameObject != null && gameObject_0 != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = gameObject_0.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = gameObject_0.layer;
		}
		return gameObject;
	}

	public static int CalculateRaycastDepth(GameObject gameObject_0)
	{
		UIWidget component = gameObject_0.GetComponent<UIWidget>();
		if (component != null)
		{
			return component.Int32_3;
		}
		UIWidget[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIWidget>();
		if (componentsInChildren.Length == 0)
		{
			return 0;
		}
		int num = int.MaxValue;
		int i = 0;
		for (int num2 = componentsInChildren.Length; i < num2; i++)
		{
			if (componentsInChildren[i].enabled)
			{
				num = Mathf.Min(num, componentsInChildren[i].Int32_3);
			}
		}
		return num;
	}

	public static int CalculateNextDepth(GameObject gameObject_0)
	{
		int num = -1;
		UIWidget[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIWidget>();
		int i = 0;
		for (int num2 = componentsInChildren.Length; i < num2; i++)
		{
			num = Mathf.Max(num, componentsInChildren[i].Int32_2);
		}
		return num + 1;
	}

	public static int CalculateNextDepth(GameObject gameObject_0, bool bool_1)
	{
		if (bool_1)
		{
			int num = -1;
			UIWidget[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num2 = componentsInChildren.Length; i < num2; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				if (!(uIWidget.GameObject_0 != gameObject_0) || (!(uIWidget.GetComponent<Collider>() != null) && !(uIWidget.GetComponent<Collider2D>() != null)))
				{
					num = Mathf.Max(num, uIWidget.Int32_2);
				}
			}
			return num + 1;
		}
		return CalculateNextDepth(gameObject_0);
	}

	public static int AdjustDepth(GameObject gameObject_0, int int_0)
	{
		if (gameObject_0 != null)
		{
			UIPanel component = gameObject_0.GetComponent<UIPanel>();
			if (component != null)
			{
				UIPanel[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIPanel>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Int32_1 += int_0;
				}
				return 1;
			}
			UIWidget[] componentsInChildren2 = gameObject_0.GetComponentsInChildren<UIWidget>(true);
			int j = 0;
			for (int num = componentsInChildren2.Length; j < num; j++)
			{
				componentsInChildren2[j].Int32_2 += int_0;
			}
			return 2;
		}
		return 0;
	}

	public static void BringForward(GameObject gameObject_0)
	{
		switch (AdjustDepth(gameObject_0, 1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void PushBack(GameObject gameObject_0)
	{
		switch (AdjustDepth(gameObject_0, -1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void NormalizeDepths()
	{
		NormalizeWidgetDepths();
		NormalizePanelDepths();
	}

	public static void NormalizeWidgetDepths()
	{
		UIWidget[] array = FindActive<UIWidget>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		Array.Sort(array, UIWidget.FullCompareFunc);
		int num2 = 0;
		int int32_ = array[0].Int32_2;
		for (int i = 0; i < num; i++)
		{
			UIWidget uIWidget = array[i];
			if (uIWidget.Int32_2 == int32_)
			{
				uIWidget.Int32_2 = num2;
				continue;
			}
			int32_ = uIWidget.Int32_2;
			num2 = (uIWidget.Int32_2 = num2 + 1);
		}
	}

	public static void NormalizePanelDepths()
	{
		UIPanel[] array = FindActive<UIPanel>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		Array.Sort(array, UIPanel.CompareFunc);
		int num2 = 0;
		int int32_ = array[0].Int32_1;
		for (int i = 0; i < num; i++)
		{
			UIPanel uIPanel = array[i];
			if (uIPanel.Int32_1 == int32_)
			{
				uIPanel.Int32_1 = num2;
				continue;
			}
			int32_ = uIPanel.Int32_1;
			num2 = (uIPanel.Int32_1 = num2 + 1);
		}
	}

	public static UIPanel CreateUI(bool bool_1)
	{
		return CreateUI(null, bool_1, -1);
	}

	public static UIPanel CreateUI(bool bool_1, int int_0)
	{
		return CreateUI(null, bool_1, int_0);
	}

	public static UIPanel CreateUI(Transform transform_0, bool bool_1, int int_0)
	{
		UIRoot uIRoot = ((!(transform_0 != null)) ? null : FindInParents<UIRoot>(transform_0.gameObject));
		if (uIRoot == null && UIRoot.list_0.Count > 0)
		{
			uIRoot = UIRoot.list_0[0];
		}
		if (uIRoot != null)
		{
			UICamera componentInChildren = uIRoot.GetComponentInChildren<UICamera>();
			if (componentInChildren != null && componentInChildren.GetComponent<Camera>().orthographic == bool_1)
			{
				transform_0 = null;
				uIRoot = null;
			}
		}
		if (uIRoot == null)
		{
			GameObject gameObject = AddChild(null, false);
			uIRoot = gameObject.AddComponent<UIRoot>();
			if (int_0 == -1)
			{
				int_0 = LayerMask.NameToLayer("UI");
			}
			if (int_0 == -1)
			{
				int_0 = LayerMask.NameToLayer("2D UI");
			}
			gameObject.layer = int_0;
			if (bool_1)
			{
				gameObject.name = "UI Root (3D)";
				uIRoot.scalingStyle = UIRoot.Scaling.FixedSize;
			}
			else
			{
				gameObject.name = "UI Root";
				uIRoot.scalingStyle = UIRoot.Scaling.PixelPerfect;
			}
		}
		UIPanel uIPanel = uIRoot.GetComponentInChildren<UIPanel>();
		if (uIPanel == null)
		{
			Camera[] array = FindActive<Camera>();
			float num = -1f;
			bool flag = false;
			int num2 = 1 << uIRoot.gameObject.layer;
			foreach (Camera camera in array)
			{
				if (camera.clearFlags == CameraClearFlags.Color || camera.clearFlags == CameraClearFlags.Skybox)
				{
					flag = true;
				}
				num = Mathf.Max(num, camera.depth);
				camera.cullingMask &= ~num2;
			}
			Camera camera2 = AddChild<Camera>(uIRoot.gameObject, false);
			camera2.gameObject.AddComponent<UICamera>();
			camera2.clearFlags = ((!flag) ? CameraClearFlags.Color : CameraClearFlags.Depth);
			camera2.backgroundColor = Color.grey;
			camera2.cullingMask = num2;
			camera2.depth = num + 1f;
			if (bool_1)
			{
				camera2.nearClipPlane = 0.1f;
				camera2.farClipPlane = 4f;
				camera2.transform.localPosition = new Vector3(0f, 0f, -700f);
			}
			else
			{
				camera2.orthographic = true;
				camera2.orthographicSize = 1f;
				camera2.nearClipPlane = -10f;
				camera2.farClipPlane = 10f;
			}
			AudioListener[] array2 = FindActive<AudioListener>();
			if (array2 == null || array2.Length == 0)
			{
				camera2.gameObject.AddComponent<AudioListener>();
			}
			uIPanel = uIRoot.gameObject.AddComponent<UIPanel>();
		}
		if (transform_0 != null)
		{
			while (transform_0.parent != null)
			{
				transform_0 = transform_0.parent;
			}
			if (IsChild(transform_0, uIPanel.transform))
			{
				uIPanel = transform_0.gameObject.AddComponent<UIPanel>();
			}
			else
			{
				transform_0.parent = uIPanel.transform;
				transform_0.localScale = Vector3.one;
				transform_0.localPosition = Vector3.zero;
				SetChildLayer(uIPanel.Transform_0, uIPanel.GameObject_0.layer);
			}
		}
		return uIPanel;
	}

	public static void SetChildLayer(Transform transform_0, int int_0)
	{
		for (int i = 0; i < transform_0.childCount; i++)
		{
			Transform child = transform_0.GetChild(i);
			child.gameObject.layer = int_0;
			SetChildLayer(child, int_0);
		}
	}

	public static T AddChild<T>(GameObject gameObject_0) where T : Component
	{
		GameObject gameObject = AddChild(gameObject_0);
		gameObject.name = GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	public static T AddChild<T>(GameObject gameObject_0, bool bool_1) where T : Component
	{
		GameObject gameObject = AddChild(gameObject_0, bool_1);
		gameObject.name = GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	public static T AddWidget<T>(GameObject gameObject_0) where T : UIWidget
	{
		int int32_ = CalculateNextDepth(gameObject_0);
		T result = AddChild<T>(gameObject_0);
		result.Int32_0 = 100;
		result.Int32_1 = 100;
		result.Int32_2 = int32_;
		result.gameObject.layer = gameObject_0.layer;
		return result;
	}

	public static UISprite AddSprite(GameObject gameObject_0, UIAtlas uiatlas_0, string string_0)
	{
		UISpriteData uISpriteData = ((!(uiatlas_0 != null)) ? null : uiatlas_0.GetSprite(string_0));
		UISprite uISprite = AddWidget<UISprite>(gameObject_0);
		uISprite.Type_0 = ((uISpriteData != null && uISpriteData.Boolean_0) ? UIBasicSprite.Type.Sliced : UIBasicSprite.Type.Simple);
		uISprite.UIAtlas_0 = uiatlas_0;
		uISprite.String_0 = string_0;
		return uISprite;
	}

	public static GameObject GetRoot(GameObject gameObject_0)
	{
		Transform transform = gameObject_0.transform;
		while (true)
		{
			Transform parent = transform.parent;
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.gameObject;
	}

	public static T FindInParents<T>(GameObject gameObject_0) where T : Component
	{
		if (gameObject_0 == null)
		{
			return (T)null;
		}
		T component = gameObject_0.GetComponent<T>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			Transform parent = gameObject_0.transform.parent;
			while (parent != null && (UnityEngine.Object)component == (UnityEngine.Object)null)
			{
				component = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	public static T FindInParents<T>(Transform transform_0) where T : Component
	{
		if (transform_0 == null)
		{
			return (T)null;
		}
		T component = transform_0.GetComponent<T>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			Transform parent = transform_0.transform.parent;
			while (parent != null && (UnityEngine.Object)component == (UnityEngine.Object)null)
			{
				component = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	public static void Destroy(UnityEngine.Object object_0)
	{
		if (!(object_0 != null))
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (object_0 is GameObject)
			{
				GameObject gameObject = object_0 as GameObject;
				gameObject.transform.parent = null;
			}
			UnityEngine.Object.Destroy(object_0);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(object_0);
		}
	}

	public static void DestroyImmediate(UnityEngine.Object object_0)
	{
		if (object_0 != null)
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(object_0);
			}
			else
			{
				UnityEngine.Object.Destroy(object_0);
			}
		}
	}

	public static void Broadcast(string string_0)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(string_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	public static void Broadcast(string string_0, object object_0)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(string_0, object_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	public static bool IsChild(Transform transform_0, Transform transform_1)
	{
		if (!(transform_0 == null) && !(transform_1 == null))
		{
			while (true)
			{
				if (transform_1 != null)
				{
					if (transform_1 == transform_0)
					{
						break;
					}
					transform_1 = transform_1.parent;
					continue;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	private static void Activate(Transform transform_0)
	{
		Activate(transform_0, false);
	}

	private static void Activate(Transform transform_0, bool bool_1)
	{
		SetActiveSelf(transform_0.gameObject, true);
		if (!bool_1)
		{
			return;
		}
		int num = 0;
		int childCount = transform_0.childCount;
		while (true)
		{
			if (num < childCount)
			{
				Transform child = transform_0.GetChild(num);
				if (!child.gameObject.activeSelf)
				{
					num++;
					continue;
				}
				break;
			}
			int i = 0;
			for (int childCount2 = transform_0.childCount; i < childCount2; i++)
			{
				Transform child2 = transform_0.GetChild(i);
				Activate(child2, true);
			}
			break;
		}
	}

	private static void Deactivate(Transform transform_0)
	{
		SetActiveSelf(transform_0.gameObject, false);
	}

	public static void SetActive(GameObject gameObject_0, bool bool_1)
	{
		SetActive(gameObject_0, bool_1, true);
	}

	public static void SetActive(GameObject gameObject_0, bool bool_1, bool bool_2)
	{
		if ((bool)gameObject_0)
		{
			if (bool_1)
			{
				Activate(gameObject_0.transform, bool_2);
				CallCreatePanel(gameObject_0.transform);
			}
			else
			{
				Deactivate(gameObject_0.transform);
			}
		}
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	private static void CallCreatePanel(Transform transform_0)
	{
		UIWidget component = transform_0.GetComponent<UIWidget>();
		if (component != null)
		{
			component.CreatePanel();
		}
		int i = 0;
		for (int childCount = transform_0.childCount; i < childCount; i++)
		{
			CallCreatePanel(transform_0.GetChild(i));
		}
	}

	public static void SetActiveChildren(GameObject gameObject_0, bool bool_1)
	{
		Transform transform = gameObject_0.transform;
		if (bool_1)
		{
			int i = 0;
			for (int childCount = transform.childCount; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				Activate(child);
			}
		}
		else
		{
			int j = 0;
			for (int childCount2 = transform.childCount; j < childCount2; j++)
			{
				Transform child2 = transform.GetChild(j);
				Deactivate(child2);
			}
		}
	}

	[Obsolete("Use NGUITools.GetActive instead")]
	public static bool IsActive(Behaviour behaviour_0)
	{
		return behaviour_0 != null && behaviour_0.enabled && behaviour_0.gameObject.activeInHierarchy;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(Behaviour behaviour_0)
	{
		return (bool)behaviour_0 && behaviour_0.enabled && behaviour_0.gameObject.activeInHierarchy;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(GameObject gameObject_0)
	{
		return (bool)gameObject_0 && gameObject_0.activeInHierarchy;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static void SetActiveSelf(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	public static void SetLayer(GameObject gameObject_0, int int_0)
	{
		gameObject_0.layer = int_0;
		Transform transform = gameObject_0.transform;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			SetLayer(child.gameObject, int_0);
		}
	}

	public static Vector3 Round(Vector3 vector3_1)
	{
		vector3_1.x = Mathf.Round(vector3_1.x);
		vector3_1.y = Mathf.Round(vector3_1.y);
		vector3_1.z = Mathf.Round(vector3_1.z);
		return vector3_1;
	}

	public static void MakePixelPerfect(Transform transform_0)
	{
		UIWidget component = transform_0.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		if (transform_0.GetComponent<UIAnchor>() == null && transform_0.GetComponent<UIRoot>() == null)
		{
			transform_0.localPosition = Round(transform_0.localPosition);
			transform_0.localScale = Round(transform_0.localScale);
		}
		int i = 0;
		for (int childCount = transform_0.childCount; i < childCount; i++)
		{
			MakePixelPerfect(transform_0.GetChild(i));
		}
	}

	public static bool Save(string string_0, byte[] byte_0)
	{
		if (!Boolean_0)
		{
			return false;
		}
		string path = Application.persistentDataPath + "/" + string_0;
		if (byte_0 == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
			return false;
		}
		fileStream.Write(byte_0, 0, byte_0.Length);
		fileStream.Close();
		return true;
	}

	public static byte[] Load(string string_0)
	{
		if (!Boolean_0)
		{
			return null;
		}
		string path = Application.persistentDataPath + "/" + string_0;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	public static Color ApplyPMA(Color color_0)
	{
		if (color_0.a != 1f)
		{
			color_0.r *= color_0.a;
			color_0.g *= color_0.a;
			color_0.b *= color_0.a;
		}
		return color_0;
	}

	public static void MarkParentAsChanged(GameObject gameObject_0)
	{
		UIRect[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIRect>();
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			componentsInChildren[i].ParentHasChanged();
		}
	}

	[Obsolete("Use NGUIText.EncodeColor instead")]
	public static string EncodeColor(Color color_0)
	{
		return NGUIText.EncodeColor24(color_0);
	}

	[Obsolete("Use NGUIText.ParseColor instead")]
	public static Color ParseColor(string string_0, int int_0)
	{
		return NGUIText.ParseColor24(string_0, int_0);
	}

	[Obsolete("Use NGUIText.StripSymbols instead")]
	public static string StripSymbols(string string_0)
	{
		return NGUIText.StripSymbols(string_0);
	}

	public static T AddMissingComponent<T>(this GameObject gameObject_0) where T : Component
	{
		T val = gameObject_0.GetComponent<T>();
		if ((UnityEngine.Object)val == (UnityEngine.Object)null)
		{
			val = gameObject_0.AddComponent<T>();
		}
		return val;
	}

	public static Vector3[] GetSides(this Camera camera_0)
	{
		return camera_0.GetSides(Mathf.Lerp(camera_0.nearClipPlane, camera_0.farClipPlane, 0.5f), null);
	}

	public static Vector3[] GetSides(this Camera camera_0, float float_1)
	{
		return camera_0.GetSides(float_1, null);
	}

	public static Vector3[] GetSides(this Camera camera_0, Transform transform_0)
	{
		return camera_0.GetSides(Mathf.Lerp(camera_0.nearClipPlane, camera_0.farClipPlane, 0.5f), transform_0);
	}

	public static Vector3[] GetSides(this Camera camera_0, float float_1, Transform transform_0)
	{
		Rect rect = camera_0.rect;
		Vector2 vector2_ = Vector2_0;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = rect.width / rect.height;
		num *= num5;
		num2 *= num5;
		num *= vector2_.x;
		num2 *= vector2_.x;
		num3 *= vector2_.y;
		num4 *= vector2_.y;
		Transform transform = camera_0.transform;
		vector3_0[0] = transform.TransformPoint(new Vector3(num, 0f, float_1));
		vector3_0[1] = transform.TransformPoint(new Vector3(0f, num4, float_1));
		vector3_0[2] = transform.TransformPoint(new Vector3(num2, 0f, float_1));
		vector3_0[3] = transform.TransformPoint(new Vector3(0f, num3, float_1));
		if (transform_0 != null)
		{
			for (int i = 0; i < 4; i++)
			{
				vector3_0[i] = transform_0.InverseTransformPoint(vector3_0[i]);
			}
		}
		return vector3_0;
	}

	public static Vector3[] GetWorldCorners(this Camera camera_0)
	{
		return camera_0.GetWorldCorners(Mathf.Lerp(camera_0.nearClipPlane, camera_0.farClipPlane, 0.5f), null);
	}

	public static Vector3[] GetWorldCorners(this Camera camera_0, float float_1)
	{
		return camera_0.GetWorldCorners(float_1, null);
	}

	public static Vector3[] GetWorldCorners(this Camera camera_0, Transform transform_0)
	{
		return camera_0.GetWorldCorners(Mathf.Lerp(camera_0.nearClipPlane, camera_0.farClipPlane, 0.5f), transform_0);
	}

	public static Vector3[] GetWorldCorners(this Camera camera_0, float float_1, Transform transform_0)
	{
		Rect rect = camera_0.rect;
		Vector2 vector2_ = Vector2_0;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = rect.width / rect.height;
		num *= num5;
		num2 *= num5;
		num *= vector2_.x;
		num2 *= vector2_.x;
		num3 *= vector2_.y;
		num4 *= vector2_.y;
		Transform transform = camera_0.transform;
		vector3_0[0] = transform.TransformPoint(new Vector3(num, num3, float_1));
		vector3_0[1] = transform.TransformPoint(new Vector3(num, num4, float_1));
		vector3_0[2] = transform.TransformPoint(new Vector3(num2, num4, float_1));
		vector3_0[3] = transform.TransformPoint(new Vector3(num2, num3, float_1));
		if (transform_0 != null)
		{
			for (int i = 0; i < 4; i++)
			{
				vector3_0[i] = transform_0.InverseTransformPoint(vector3_0[i]);
			}
		}
		return vector3_0;
	}

	public static string GetFuncName(object object_0, string string_0)
	{
		if (object_0 == null)
		{
			return "<null>";
		}
		string text = object_0.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		return (!string.IsNullOrEmpty(string_0)) ? (text + "/" + string_0) : text;
	}

	public static void Execute<T>(GameObject gameObject_0, string string_0) where T : Component
	{
		T[] components = gameObject_0.GetComponents<T>();
		T[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			T obj = array[i];
			MethodInfo method = obj.GetType().GetMethod(string_0, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(obj, null);
			}
		}
	}

	public static void ExecuteAll<T>(GameObject gameObject_0, string string_0) where T : Component
	{
		Execute<T>(gameObject_0, string_0);
		Transform transform = gameObject_0.transform;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			ExecuteAll<T>(transform.GetChild(i).gameObject, string_0);
		}
	}

	public static void ImmediatelyCreateDrawCalls(GameObject gameObject_0)
	{
		ExecuteAll<UIWidget>(gameObject_0, "Start");
		ExecuteAll<UIPanel>(gameObject_0, "Start");
		ExecuteAll<UIWidget>(gameObject_0, "Update");
		ExecuteAll<UIPanel>(gameObject_0, "Update");
		ExecuteAll<UIPanel>(gameObject_0, "LateUpdate");
	}
}
