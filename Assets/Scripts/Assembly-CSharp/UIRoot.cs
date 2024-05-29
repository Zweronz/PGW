using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
	public enum Scaling
	{
		PixelPerfect = 0,
		FixedSize = 1,
		FixedSizeOnMobiles = 2
	}

	public static List<UIRoot> list_0 = new List<UIRoot>();

	public Scaling scalingStyle;

	public int manualHeight = 720;

	public int minimumHeight = 320;

	public int maximumHeight = 1536;

	public bool adjustByDPI;

	public bool shrinkPortraitUI;

	private Transform transform_0;

	public int Int32_0
	{
		get
		{
			if (scalingStyle == Scaling.FixedSize)
			{
				return manualHeight;
			}
			Vector2 vector2_ = NGUITools.Vector2_0;
			float num = vector2_.x / vector2_.y;
			if (vector2_.y < (float)minimumHeight)
			{
				vector2_.y = minimumHeight;
				vector2_.x = vector2_.y * num;
			}
			else if (vector2_.y > (float)maximumHeight)
			{
				vector2_.y = maximumHeight;
				vector2_.x = vector2_.y * num;
			}
			int num2 = Mathf.RoundToInt((!shrinkPortraitUI || !(vector2_.y > vector2_.x)) ? vector2_.y : (vector2_.y / num));
			return (!adjustByDPI) ? num2 : NGUIMath.AdjustByDPI(num2);
		}
	}

	public float Single_0
	{
		get
		{
			return GetPixelSizeAdjustment(Mathf.RoundToInt(NGUITools.Vector2_0.y));
		}
	}

	public static float GetPixelSizeAdjustment(GameObject gameObject_0)
	{
		UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(gameObject_0);
		return (!(uIRoot != null)) ? 1f : uIRoot.Single_0;
	}

	public float GetPixelSizeAdjustment(int int_0)
	{
		int_0 = Mathf.Max(2, int_0);
		if (scalingStyle == Scaling.FixedSize)
		{
			return (float)manualHeight / (float)int_0;
		}
		if (int_0 < minimumHeight)
		{
			return (float)minimumHeight / (float)int_0;
		}
		if (int_0 > maximumHeight)
		{
			return (float)maximumHeight / (float)int_0;
		}
		return 1f;
	}

	protected virtual void Awake()
	{
		transform_0 = base.transform;
	}

	protected virtual void OnEnable()
	{
		list_0.Add(this);
	}

	protected virtual void OnDisable()
	{
		list_0.Remove(this);
	}

	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			Update();
		}
	}

	private void Update()
	{
		if (!(transform_0 != null))
		{
			return;
		}
		float num = Int32_0;
		if (num > 0f)
		{
			float num2 = 2f / num;
			Vector3 localScale = transform_0.localScale;
			if (!(Mathf.Abs(localScale.x - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.y - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.z - num2) <= float.Epsilon))
			{
				transform_0.localScale = new Vector3(num2, num2, num2);
			}
		}
	}

	public static void Broadcast(string string_0)
	{
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			UIRoot uIRoot = list_0[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(string_0, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public static void Broadcast(string string_0, object object_0)
	{
		if (object_0 == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			UIRoot uIRoot = list_0[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(string_0, object_0, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
