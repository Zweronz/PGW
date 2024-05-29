using System;
using UnityEngine;

public class UIPanel : UIRect
{
	public enum RenderQueue
	{
		Automatic = 0,
		StartAt = 1,
		Explicit = 2
	}

	public delegate void OnGeometryUpdated();

	public delegate void OnClippingMoved(UIPanel uipanel_0);

	public static BetterList<UIPanel> betterList_1 = new BetterList<UIPanel>();

	public OnGeometryUpdated onGeometryUpdated_0;

	public bool bool_6 = true;

	public bool bool_7;

	public bool bool_8;

	public bool bool_9;

	public bool bool_10;

	public bool bool_11;

	public RenderQueue renderQueue_0;

	public int int_1 = 3000;

	[NonSerialized]
	public BetterList<UIWidget> betterList_2 = new BetterList<UIWidget>();

	[NonSerialized]
	public BetterList<UIDrawCall> betterList_3 = new BetterList<UIDrawCall>();

	[NonSerialized]
	public Matrix4x4 matrix4x4_0 = Matrix4x4.identity;

	[NonSerialized]
	public Vector4 vector4_0 = new Vector4(0f, 0f, 1f, 1f);

	public OnClippingMoved onClippingMoved_0;

	[SerializeField]
	private float float_0 = 1f;

	[SerializeField]
	private UIDrawCall.Clipping clipping_0;

	[SerializeField]
	private Vector4 vector4_1 = new Vector4(0f, 0f, 300f, 200f);

	[SerializeField]
	private Vector2 vector2_0 = new Vector2(4f, 4f);

	[SerializeField]
	private int int_2;

	[SerializeField]
	private int int_3;

	private bool bool_12;

	private bool bool_13;

	private Camera camera_1;

	[SerializeField]
	private Vector2 vector2_1 = Vector2.zero;

	private float float_1;

	private float float_2;

	private int int_4 = -1;

	private int int_5;

	private int int_6 = -1;

	private static float[] float_3 = new float[4];

	private Vector2 vector2_2 = Vector2.zero;

	private Vector2 vector2_3 = Vector2.zero;

	private bool bool_14;

	private bool bool_15;

	private bool bool_16;

	private UIPanel uipanel_0;

	private static Vector3[] vector3_1 = new Vector3[4];

	private static int int_7 = -1;

	private bool bool_17;

	public static int Int32_0
	{
		get
		{
			int num = int.MinValue;
			for (int i = 0; i < betterList_1.size; i++)
			{
				num = Mathf.Max(num, betterList_1[i].Int32_1);
			}
			return (num != int.MinValue) ? (num + 1) : 0;
		}
	}

	public override bool Boolean_7
	{
		get
		{
			return clipping_0 != UIDrawCall.Clipping.None;
		}
	}

	public override float Single_2
	{
		get
		{
			return float_0;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (float_0 != num)
			{
				int_5 = -1;
				bool_13 = true;
				float_0 = num;
				SetDirty();
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return int_2;
		}
		set
		{
			if (int_2 != value)
			{
				int_2 = value;
				betterList_1.Sort(CompareFunc);
			}
		}
	}

	public int Int32_2
	{
		get
		{
			return int_3;
		}
		set
		{
			if (int_3 != value)
			{
				int_3 = value;
				UpdateDrawCalls();
			}
		}
	}

	public float Single_0
	{
		get
		{
			return GetViewSize().x;
		}
	}

	public float Single_1
	{
		get
		{
			return GetViewSize().y;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_14;
		}
	}

	public bool Boolean_3
	{
		get
		{
			return camera_1 != null && camera_1.orthographic;
		}
	}

	public Vector3 Vector3_0
	{
		get
		{
			if (bool_14 && camera_1 != null && camera_1.orthographic)
			{
				float num = 1f / GetWindowSize().y / camera_1.orthographicSize;
				return new Vector3(0f - num, num);
			}
			return Vector3.zero;
		}
	}

	public UIDrawCall.Clipping Clipping_0
	{
		get
		{
			return clipping_0;
		}
		set
		{
			if (clipping_0 != value)
			{
				bool_13 = true;
				clipping_0 = value;
				int_4 = -1;
			}
		}
	}

	public UIPanel UIPanel_0
	{
		get
		{
			return uipanel_0;
		}
	}

	public int Int32_3
	{
		get
		{
			int num = 0;
			UIPanel uIPanel = this;
			while (uIPanel != null)
			{
				if (uIPanel.clipping_0 == UIDrawCall.Clipping.SoftClip)
				{
					num++;
				}
				uIPanel = uIPanel.uipanel_0;
			}
			return num;
		}
	}

	public bool Boolean_4
	{
		get
		{
			return clipping_0 == UIDrawCall.Clipping.SoftClip;
		}
	}

	public bool Boolean_5
	{
		get
		{
			return Int32_3 != 0;
		}
	}

	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool Boolean_6
	{
		get
		{
			return Boolean_5;
		}
	}

	public Vector2 Vector2_0
	{
		get
		{
			return vector2_1;
		}
		set
		{
			if (Mathf.Abs(vector2_1.x - value.x) > 0.001f || Mathf.Abs(vector2_1.y - value.y) > 0.001f)
			{
				vector2_1 = value;
				InvalidateClipping();
				if (onClippingMoved_0 != null)
				{
					onClippingMoved_0(this);
				}
			}
		}
	}

	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 Vector4_0
	{
		get
		{
			return Vector4_1;
		}
		set
		{
			Vector4_1 = value;
		}
	}

	public Vector4 Vector4_1
	{
		get
		{
			return vector4_1;
		}
		set
		{
			if (Mathf.Abs(vector4_1.x - value.x) > 0.001f || Mathf.Abs(vector4_1.y - value.y) > 0.001f || Mathf.Abs(vector4_1.z - value.z) > 0.001f || Mathf.Abs(vector4_1.w - value.w) > 0.001f)
			{
				bool_13 = true;
				float_1 = ((float_1 != 0f) ? (RealTime.Single_0 + 0.15f) : 0.001f);
				vector4_1 = value;
				int_4 = -1;
				UIScrollView component = GetComponent<UIScrollView>();
				if (component != null)
				{
					component.UpdatePosition();
				}
				if (onClippingMoved_0 != null)
				{
					onClippingMoved_0(this);
				}
			}
		}
	}

	public Vector4 Vector4_2
	{
		get
		{
			Vector2 viewSize = GetViewSize();
			if (clipping_0 != 0)
			{
				return new Vector4(vector4_1.x + vector2_1.x, vector4_1.y + vector2_1.y, viewSize.x, viewSize.y);
			}
			return new Vector4(0f, 0f, viewSize.x, viewSize.y);
		}
	}

	public Vector2 Vector2_1
	{
		get
		{
			return vector2_0;
		}
		set
		{
			if (vector2_0 != value)
			{
				vector2_0 = value;
			}
		}
	}

	public override Vector3[] Vector3_2
	{
		get
		{
			if (clipping_0 == UIDrawCall.Clipping.None)
			{
				Vector2 viewSize = GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float x = num + viewSize.x;
				float y = num2 + viewSize.y;
				Transform transform = ((!(camera_1 != null)) ? null : camera_1.transform);
				if (transform != null)
				{
					vector3_1[0] = transform.TransformPoint(num, num2, 0f);
					vector3_1[1] = transform.TransformPoint(num, y, 0f);
					vector3_1[2] = transform.TransformPoint(x, y, 0f);
					vector3_1[3] = transform.TransformPoint(x, num2, 0f);
					transform = base.Transform_0;
					for (int i = 0; i < 4; i++)
					{
						vector3_1[i] = transform.InverseTransformPoint(vector3_1[i]);
					}
				}
				else
				{
					vector3_1[0] = new Vector3(num, num2);
					vector3_1[1] = new Vector3(num, y);
					vector3_1[2] = new Vector3(x, y);
					vector3_1[3] = new Vector3(x, num2);
				}
			}
			else
			{
				float num3 = vector2_1.x + vector4_1.x - 0.5f * vector4_1.z;
				float num4 = vector2_1.y + vector4_1.y - 0.5f * vector4_1.w;
				float x2 = num3 + vector4_1.z;
				float y2 = num4 + vector4_1.w;
				vector3_1[0] = new Vector3(num3, num4);
				vector3_1[1] = new Vector3(num3, y2);
				vector3_1[2] = new Vector3(x2, y2);
				vector3_1[3] = new Vector3(x2, num4);
			}
			return vector3_1;
		}
	}

	public override Vector3[] Vector3_3
	{
		get
		{
			if (clipping_0 == UIDrawCall.Clipping.None)
			{
				if (camera_1 != null)
				{
					Vector3[] worldCorners = camera_1.GetWorldCorners();
					UIRoot uIRoot_ = base.UIRoot_0;
					if (uIRoot_ != null)
					{
						float single_ = uIRoot_.Single_0;
						for (int i = 0; i < 4; i++)
						{
							worldCorners[i] *= single_;
						}
					}
					return worldCorners;
				}
				Vector2 viewSize = GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float x = num + viewSize.x;
				float y = num2 + viewSize.y;
				vector3_1[0] = new Vector3(num, num2, 0f);
				vector3_1[1] = new Vector3(num, y, 0f);
				vector3_1[2] = new Vector3(x, y, 0f);
				vector3_1[3] = new Vector3(x, num2, 0f);
			}
			else
			{
				float num3 = vector2_1.x + vector4_1.x - 0.5f * vector4_1.z;
				float num4 = vector2_1.y + vector4_1.y - 0.5f * vector4_1.w;
				float x2 = num3 + vector4_1.z;
				float y2 = num4 + vector4_1.w;
				Transform transform = base.Transform_0;
				vector3_1[0] = transform.TransformPoint(num3, num4, 0f);
				vector3_1[1] = transform.TransformPoint(num3, y2, 0f);
				vector3_1[2] = transform.TransformPoint(x2, y2, 0f);
				vector3_1[3] = transform.TransformPoint(x2, num4, 0f);
			}
			return vector3_1;
		}
	}

	public static int CompareFunc(UIPanel uipanel_1, UIPanel uipanel_2)
	{
		if (uipanel_1 != uipanel_2 && uipanel_1 != null && uipanel_2 != null)
		{
			if (uipanel_1.int_2 < uipanel_2.int_2)
			{
				return -1;
			}
			if (uipanel_1.int_2 > uipanel_2.int_2)
			{
				return 1;
			}
			return (uipanel_1.GetInstanceID() >= uipanel_2.GetInstanceID()) ? 1 : (-1);
		}
		return 0;
	}

	private void InvalidateClipping()
	{
		bool_13 = true;
		int_4 = -1;
		float_1 = ((float_1 != 0f) ? (RealTime.Single_0 + 0.15f) : 0.001f);
		for (int i = 0; i < betterList_1.size; i++)
		{
			UIPanel uIPanel = betterList_1[i];
			if (uIPanel != this && uIPanel.UIPanel_0 == this)
			{
				uIPanel.InvalidateClipping();
			}
		}
	}

	public override Vector3[] GetSides(Transform transform_1)
	{
		if (clipping_0 == UIDrawCall.Clipping.None && !bool_11)
		{
			return base.GetSides(transform_1);
		}
		Vector2 viewSize = GetViewSize();
		Vector2 vector = ((clipping_0 == UIDrawCall.Clipping.None) ? Vector2.zero : ((Vector2)vector4_1 + vector2_1));
		float num = vector.x - 0.5f * viewSize.x;
		float num2 = vector.y - 0.5f * viewSize.y;
		float num3 = num + viewSize.x;
		float num4 = num2 + viewSize.y;
		float x = (num + num3) * 0.5f;
		float y = (num2 + num4) * 0.5f;
		Matrix4x4 localToWorldMatrix = base.Transform_0.localToWorldMatrix;
		vector3_1[0] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num, y));
		vector3_1[1] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(x, num4));
		vector3_1[2] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num3, y));
		vector3_1[3] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(x, num2));
		if (transform_1 != null)
		{
			for (int i = 0; i < 4; i++)
			{
				vector3_1[i] = transform_1.InverseTransformPoint(vector3_1[i]);
			}
		}
		return vector3_1;
	}

	public override void Invalidate(bool bool_18)
	{
		int_5 = -1;
		base.Invalidate(bool_18);
	}

	public override float CalculateFinalAlpha(int int_8)
	{
		if (int_5 != int_8)
		{
			int_5 = int_8;
			UIRect uIRect_ = base.UIRect_0;
			finalAlpha = ((!(base.UIRect_0 != null)) ? float_0 : (uIRect_.CalculateFinalAlpha(int_8) * float_0));
		}
		return finalAlpha;
	}

	public override void SetRect(float float_4, float float_5, float float_6, float float_7)
	{
		int num = Mathf.FloorToInt(float_6 + 0.5f);
		int num2 = Mathf.FloorToInt(float_7 + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform transform = base.Transform_0;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(float_4 + 0.5f);
		localPosition.y = Mathf.Floor(float_5 + 0.5f);
		if (num < 2)
		{
			num = 2;
		}
		if (num2 < 2)
		{
			num2 = 2;
		}
		Vector4_1 = new Vector4(localPosition.x, localPosition.y, num, num2);
		if (base.Boolean_1)
		{
			transform = transform.parent;
			if ((bool)leftAnchor.target)
			{
				leftAnchor.SetHorizontal(transform, float_4);
			}
			if ((bool)rightAnchor.target)
			{
				rightAnchor.SetHorizontal(transform, float_4 + float_6);
			}
			if ((bool)bottomAnchor.target)
			{
				bottomAnchor.SetVertical(transform, float_5);
			}
			if ((bool)topAnchor.target)
			{
				topAnchor.SetVertical(transform, float_5 + float_7);
			}
		}
	}

	public bool IsVisible(Vector3 vector3_2, Vector3 vector3_3, Vector3 vector3_4, Vector3 vector3_5)
	{
		UpdateTransformMatrix();
		vector3_2 = matrix4x4_0.MultiplyPoint3x4(vector3_2);
		vector3_3 = matrix4x4_0.MultiplyPoint3x4(vector3_3);
		vector3_4 = matrix4x4_0.MultiplyPoint3x4(vector3_4);
		vector3_5 = matrix4x4_0.MultiplyPoint3x4(vector3_5);
		float_3[0] = vector3_2.x;
		float_3[1] = vector3_3.x;
		float_3[2] = vector3_4.x;
		float_3[3] = vector3_5.x;
		float num = Mathf.Min(float_3);
		float num2 = Mathf.Max(float_3);
		float_3[0] = vector3_2.y;
		float_3[1] = vector3_3.y;
		float_3[2] = vector3_4.y;
		float_3[3] = vector3_5.y;
		float num3 = Mathf.Min(float_3);
		float num4 = Mathf.Max(float_3);
		if (num2 < vector2_2.x)
		{
			return false;
		}
		if (num4 < vector2_2.y)
		{
			return false;
		}
		if (num > vector2_3.x)
		{
			return false;
		}
		if (num3 > vector2_3.y)
		{
			return false;
		}
		return true;
	}

	public bool IsVisible(Vector3 vector3_2)
	{
		if (float_0 < 0.001f)
		{
			return false;
		}
		if (clipping_0 != 0 && clipping_0 != UIDrawCall.Clipping.ConstrainButDontClip)
		{
			UpdateTransformMatrix();
			Vector3 vector = matrix4x4_0.MultiplyPoint3x4(vector3_2);
			if (vector.x < vector2_2.x)
			{
				return false;
			}
			if (vector.y < vector2_2.y)
			{
				return false;
			}
			if (vector.x > vector2_3.x)
			{
				return false;
			}
			if (vector.y > vector2_3.y)
			{
				return false;
			}
			return true;
		}
		return true;
	}

	public bool IsVisible(UIWidget uiwidget_0)
	{
		UIPanel uIPanel = this;
		Vector3[] array = null;
		while (true)
		{
			if (uIPanel != null)
			{
				if ((uIPanel.clipping_0 == UIDrawCall.Clipping.None || uIPanel.clipping_0 == UIDrawCall.Clipping.ConstrainButDontClip) && !uiwidget_0.bool_7)
				{
					uIPanel = uIPanel.uipanel_0;
					continue;
				}
				if (array == null)
				{
					array = uiwidget_0.Vector3_3;
				}
				if (!uIPanel.IsVisible(array[0], array[1], array[2], array[3]))
				{
					break;
				}
				uIPanel = uIPanel.uipanel_0;
				continue;
			}
			return true;
		}
		return false;
	}

	public bool Affects(UIWidget uiwidget_0)
	{
		if (uiwidget_0 == null)
		{
			return false;
		}
		UIPanel uIPanel = uiwidget_0.uipanel_0;
		if (uIPanel == null)
		{
			return false;
		}
		UIPanel uIPanel2 = this;
		while (true)
		{
			if (uIPanel2 != null)
			{
				if (uIPanel2 == uIPanel)
				{
					break;
				}
				if (uIPanel2.Boolean_5)
				{
					uIPanel2 = uIPanel2.uipanel_0;
					continue;
				}
				return false;
			}
			return false;
		}
		return true;
	}

	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		bool_12 = true;
	}

	public void SetDirty()
	{
		for (int i = 0; i < betterList_3.size; i++)
		{
			betterList_3.buffer[i].isDirty = true;
		}
		Invalidate(true);
	}

	private void Awake()
	{
		gameObject_0 = base.gameObject;
		transform_0 = base.transform;
		bool_14 = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.XBOX360 || Application.platform == RuntimePlatform.WindowsEditor;
		if (bool_14)
		{
			bool_14 = SystemInfo.graphicsShaderLevel < 40;
		}
	}

	private void FindParent()
	{
		Transform parent = base.Transform_0.parent;
		uipanel_0 = ((!(parent != null)) ? null : NGUITools.FindInParents<UIPanel>(parent.gameObject));
	}

	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		FindParent();
	}

	protected override void OnStart()
	{
		int_6 = gameObject_0.layer;
		UICamera uICamera = UICamera.FindCameraForLayer(int_6);
		camera_1 = ((!(uICamera != null)) ? NGUITools.FindCameraForLayer(int_6) : uICamera.Camera_0);
	}

	protected override void OnEnable()
	{
		bool_12 = true;
		int_5 = -1;
		int_4 = -1;
		base.OnEnable();
		int_4 = -1;
	}

	protected override void OnInit()
	{
		base.OnInit();
		if (base.GetComponent<Rigidbody>() == null)
		{
			UICamera uICamera = ((!(camera_1 != null)) ? null : camera_1.GetComponent<UICamera>());
			if (uICamera != null && (uICamera.eventType == UICamera.EventType.UI_3D || uICamera.eventType == UICamera.EventType.World_3D))
			{
				Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
		}
		FindParent();
		bool_12 = true;
		int_5 = -1;
		int_4 = -1;
		betterList_1.Add(this);
		betterList_1.Sort(CompareFunc);
	}

	protected override void OnDisable()
	{
		for (int i = 0; i < betterList_3.size; i++)
		{
			UIDrawCall uIDrawCall = betterList_3.buffer[i];
			if (uIDrawCall != null)
			{
				UIDrawCall.Destroy(uIDrawCall);
			}
		}
		betterList_3.Clear();
		betterList_1.Remove(this);
		int_5 = -1;
		int_4 = -1;
		if (betterList_1.size == 0)
		{
			UIDrawCall.ReleaseAll();
			int_7 = -1;
		}
		base.OnDisable();
	}

	private void UpdateTransformMatrix()
	{
		int frameCount = Time.frameCount;
		if (int_4 != frameCount)
		{
			int_4 = frameCount;
			matrix4x4_0 = base.Transform_0.worldToLocalMatrix;
			Vector2 vector = GetViewSize() * 0.5f;
			float num = vector2_1.x + vector4_1.x;
			float num2 = vector2_1.y + vector4_1.y;
			vector2_2.x = num - vector.x;
			vector2_2.y = num2 - vector.y;
			vector2_3.x = num + vector.x;
			vector2_3.y = num2 + vector.y;
		}
	}

	protected override void OnAnchor()
	{
		if (clipping_0 == UIDrawCall.Clipping.None)
		{
			return;
		}
		Transform transform = base.Transform_0;
		Transform parent = transform.parent;
		Vector2 viewSize = GetViewSize();
		Vector2 vector = transform.localPosition;
		float num;
		float num2;
		float num3;
		float num4;
		if (leftAnchor.target == bottomAnchor.target && leftAnchor.target == rightAnchor.target && leftAnchor.target == topAnchor.target)
		{
			Vector3[] sides = leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, rightAnchor.relative) + (float)rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, topAnchor.relative) + (float)topAnchor.absolute;
			}
			else
			{
				Vector2 vector2 = GetLocalPos(leftAnchor, parent);
				num = vector2.x + (float)leftAnchor.absolute;
				num3 = vector2.y + (float)bottomAnchor.absolute;
				num2 = vector2.x + (float)rightAnchor.absolute;
				num4 = vector2.y + (float)topAnchor.absolute;
			}
		}
		else
		{
			if ((bool)leftAnchor.target)
			{
				Vector3[] sides2 = leftAnchor.GetSides(parent);
				num = ((sides2 == null) ? (GetLocalPos(leftAnchor, parent).x + (float)leftAnchor.absolute) : (NGUIMath.Lerp(sides2[0].x, sides2[2].x, leftAnchor.relative) + (float)leftAnchor.absolute));
			}
			else
			{
				num = vector4_1.x - 0.5f * viewSize.x;
			}
			if ((bool)rightAnchor.target)
			{
				Vector3[] sides3 = rightAnchor.GetSides(parent);
				num2 = ((sides3 == null) ? (GetLocalPos(rightAnchor, parent).x + (float)rightAnchor.absolute) : (NGUIMath.Lerp(sides3[0].x, sides3[2].x, rightAnchor.relative) + (float)rightAnchor.absolute));
			}
			else
			{
				num2 = vector4_1.x + 0.5f * viewSize.x;
			}
			if ((bool)bottomAnchor.target)
			{
				Vector3[] sides4 = bottomAnchor.GetSides(parent);
				num3 = ((sides4 == null) ? (GetLocalPos(bottomAnchor, parent).y + (float)bottomAnchor.absolute) : (NGUIMath.Lerp(sides4[3].y, sides4[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute));
			}
			else
			{
				num3 = vector4_1.y - 0.5f * viewSize.y;
			}
			if ((bool)topAnchor.target)
			{
				Vector3[] sides5 = topAnchor.GetSides(parent);
				num4 = ((sides5 == null) ? (GetLocalPos(topAnchor, parent).y + (float)topAnchor.absolute) : (NGUIMath.Lerp(sides5[3].y, sides5[1].y, topAnchor.relative) + (float)topAnchor.absolute));
			}
			else
			{
				num4 = vector4_1.y + 0.5f * viewSize.y;
			}
		}
		num -= vector.x + vector2_1.x;
		num2 -= vector.x + vector2_1.x;
		num3 -= vector.y + vector2_1.y;
		num4 -= vector.y + vector2_1.y;
		float x = Mathf.Lerp(num, num2, 0.5f);
		float y = Mathf.Lerp(num3, num4, 0.5f);
		float num5 = num2 - num;
		float num6 = num4 - num3;
		float num7 = Mathf.Max(2f, vector2_0.x);
		float num8 = Mathf.Max(2f, vector2_0.y);
		if (num5 < num7)
		{
			num5 = num7;
		}
		if (num6 < num8)
		{
			num6 = num8;
		}
		Vector4_1 = new Vector4(x, y, num5, num6);
	}

	private void LateUpdate()
	{
		if (int_7 == Time.frameCount)
		{
			return;
		}
		int_7 = Time.frameCount;
		for (int i = 0; i < betterList_1.size; i++)
		{
			betterList_1[i].UpdateSelf();
		}
		int num = 3000;
		for (int j = 0; j < betterList_1.size; j++)
		{
			UIPanel uIPanel = betterList_1.buffer[j];
			if (uIPanel.renderQueue_0 == RenderQueue.Automatic)
			{
				uIPanel.int_1 = num;
				uIPanel.UpdateDrawCalls();
				num += uIPanel.betterList_3.size;
			}
			else if (uIPanel.renderQueue_0 == RenderQueue.StartAt)
			{
				uIPanel.UpdateDrawCalls();
				if (uIPanel.betterList_3.size != 0)
				{
					num = Mathf.Max(num, uIPanel.int_1 + uIPanel.betterList_3.size);
				}
			}
			else
			{
				uIPanel.UpdateDrawCalls();
				if (uIPanel.betterList_3.size != 0)
				{
					num = Mathf.Max(num, uIPanel.int_1 + 1);
				}
			}
		}
	}

	private void UpdateSelf()
	{
		float_2 = RealTime.Single_0;
		UpdateTransformMatrix();
		UpdateLayers();
		UpdateWidgets();
		if (bool_12)
		{
			bool_12 = false;
			FillAllDrawCalls();
		}
		else
		{
			int num = 0;
			while (num < betterList_3.size)
			{
				UIDrawCall uIDrawCall = betterList_3.buffer[num];
				if (uIDrawCall.isDirty && !FillDrawCall(uIDrawCall))
				{
					UIDrawCall.Destroy(uIDrawCall);
					betterList_3.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}
		if (bool_16)
		{
			bool_16 = false;
			UIScrollView component = GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars();
			}
		}
	}

	public void SortWidgets()
	{
		bool_15 = false;
		betterList_2.Sort(UIWidget.PanelCompareFunc);
	}

	private void FillAllDrawCalls()
	{
		for (int i = 0; i < betterList_3.size; i++)
		{
			UIDrawCall.Destroy(betterList_3.buffer[i]);
		}
		betterList_3.Clear();
		Material material = null;
		Texture texture = null;
		Shader shader = null;
		UIDrawCall uIDrawCall = null;
		if (bool_15)
		{
			SortWidgets();
		}
		for (int j = 0; j < betterList_2.size; j++)
		{
			UIWidget uIWidget = betterList_2.buffer[j];
			if (uIWidget.Boolean_2 && uIWidget.Boolean_3)
			{
				Material material_ = uIWidget.Material_0;
				Texture texture_ = uIWidget.Texture_0;
				Shader shader_ = uIWidget.Shader_0;
				if (material != material_ || texture != texture_ || shader != shader_)
				{
					if (uIDrawCall != null && uIDrawCall.betterList_2.size != 0)
					{
						betterList_3.Add(uIDrawCall);
						uIDrawCall.UpdateGeometry();
						uIDrawCall = null;
					}
					material = material_;
					texture = texture_;
					shader = shader_;
				}
				if (!(material != null) && !(shader != null) && !(texture != null))
				{
					continue;
				}
				if (uIDrawCall == null)
				{
					uIDrawCall = UIDrawCall.Create(this, material, texture, shader);
					uIDrawCall.int_1 = uIWidget.Int32_2;
					uIDrawCall.int_2 = uIDrawCall.int_1;
					uIDrawCall.uipanel_1 = this;
				}
				else
				{
					int int32_ = uIWidget.Int32_2;
					if (int32_ < uIDrawCall.int_1)
					{
						uIDrawCall.int_1 = int32_;
					}
					if (int32_ > uIDrawCall.int_2)
					{
						uIDrawCall.int_2 = int32_;
					}
				}
				uIWidget.uidrawCall_0 = uIDrawCall;
				if (bool_7)
				{
					uIWidget.WriteToBuffers(uIDrawCall.betterList_2, uIDrawCall.betterList_5, uIDrawCall.betterList_6, uIDrawCall.betterList_3, uIDrawCall.betterList_4);
				}
				else
				{
					uIWidget.WriteToBuffers(uIDrawCall.betterList_2, uIDrawCall.betterList_5, uIDrawCall.betterList_6, null, null);
				}
			}
			else
			{
				uIWidget.uidrawCall_0 = null;
			}
		}
		if (uIDrawCall != null && uIDrawCall.betterList_2.size != 0)
		{
			betterList_3.Add(uIDrawCall);
			uIDrawCall.UpdateGeometry();
		}
	}

	private bool FillDrawCall(UIDrawCall uidrawCall_0)
	{
		if (uidrawCall_0 != null)
		{
			uidrawCall_0.isDirty = false;
			int num = 0;
			while (num < betterList_2.size)
			{
				UIWidget uIWidget = betterList_2[num];
				if (uIWidget == null)
				{
					betterList_2.RemoveAt(num);
					continue;
				}
				if (uIWidget.uidrawCall_0 == uidrawCall_0)
				{
					if (uIWidget.Boolean_2 && uIWidget.Boolean_3)
					{
						if (bool_7)
						{
							uIWidget.WriteToBuffers(uidrawCall_0.betterList_2, uidrawCall_0.betterList_5, uidrawCall_0.betterList_6, uidrawCall_0.betterList_3, uidrawCall_0.betterList_4);
						}
						else
						{
							uIWidget.WriteToBuffers(uidrawCall_0.betterList_2, uidrawCall_0.betterList_5, uidrawCall_0.betterList_6, null, null);
						}
					}
					else
					{
						uIWidget.uidrawCall_0 = null;
					}
				}
				num++;
			}
			if (uidrawCall_0.betterList_2.size != 0)
			{
				uidrawCall_0.UpdateGeometry();
				return true;
			}
		}
		return false;
	}

	private void UpdateDrawCalls()
	{
		Transform transform = base.Transform_0;
		bool boolean_ = Boolean_3;
		if (Clipping_0 != 0)
		{
			vector4_0 = Vector4_2;
			vector4_0.z *= 0.5f;
			vector4_0.w *= 0.5f;
		}
		else
		{
			vector4_0 = Vector4.zero;
		}
		if (vector4_0.z == 0f)
		{
			vector4_0.z = (float)Screen.width * 0.5f;
		}
		if (vector4_0.w == 0f)
		{
			vector4_0.w = (float)Screen.height * 0.5f;
		}
		if (Boolean_2)
		{
			vector4_0.x -= 0.5f;
			vector4_0.y += 0.5f;
		}
		Vector3 position;
		if (boolean_)
		{
			Transform parent = base.Transform_0.parent;
			position = base.Transform_0.localPosition;
			if (parent != null)
			{
				float num = Mathf.Round(position.x);
				float num2 = Mathf.Round(position.y);
				vector4_0.x += position.x - num;
				vector4_0.y += position.y - num2;
				position.x = num;
				position.y = num2;
				position = parent.TransformPoint(position);
			}
			position += Vector3_0;
		}
		else
		{
			position = transform.position;
		}
		Quaternion rotation = transform.rotation;
		Vector3 lossyScale = transform.lossyScale;
		for (int i = 0; i < betterList_3.size; i++)
		{
			UIDrawCall uIDrawCall = betterList_3.buffer[i];
			Transform transform2 = uIDrawCall.Transform_0;
			transform2.position = position;
			transform2.rotation = rotation;
			transform2.localScale = lossyScale;
			uIDrawCall.Int32_0 = ((renderQueue_0 != RenderQueue.Explicit) ? (int_1 + i) : int_1);
			uIDrawCall.bool_0 = bool_10 && (clipping_0 == UIDrawCall.Clipping.None || clipping_0 == UIDrawCall.Clipping.ConstrainButDontClip);
			uIDrawCall.Int32_1 = int_3;
		}
	}

	private void UpdateLayers()
	{
		if (int_6 != base.GameObject_0.layer)
		{
			int_6 = gameObject_0.layer;
			UICamera uICamera = UICamera.FindCameraForLayer(int_6);
			camera_1 = ((!(uICamera != null)) ? NGUITools.FindCameraForLayer(int_6) : uICamera.Camera_0);
			NGUITools.SetChildLayer(base.Transform_0, int_6);
			for (int i = 0; i < betterList_3.size; i++)
			{
				betterList_3.buffer[i].gameObject.layer = int_6;
			}
		}
	}

	private void UpdateWidgets()
	{
		bool flag = !bool_9 && float_1 > float_2;
		bool flag2 = false;
		if (bool_17 != flag)
		{
			bool_17 = flag;
			bool_13 = true;
		}
		bool boolean_ = Boolean_5;
		int i = 0;
		for (int size = betterList_2.size; i < size; i++)
		{
			UIWidget uIWidget = betterList_2.buffer[i];
			if (!(uIWidget.uipanel_0 == this) || !uIWidget.enabled)
			{
				continue;
			}
			int frameCount = Time.frameCount;
			if (uIWidget.UpdateTransform(frameCount) || bool_13)
			{
				bool flag3 = flag || uIWidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
				uIWidget.UpdateVisibility(flag3, flag || (!boolean_ && !uIWidget.bool_7) || IsVisible(uIWidget));
			}
			if (!uIWidget.UpdateGeometry(frameCount))
			{
				continue;
			}
			flag2 = true;
			if (!bool_12)
			{
				if (uIWidget.uidrawCall_0 != null)
				{
					uIWidget.uidrawCall_0.isDirty = true;
				}
				else
				{
					FindDrawCall(uIWidget);
				}
			}
		}
		if (flag2 && onGeometryUpdated_0 != null)
		{
			onGeometryUpdated_0();
		}
		bool_13 = false;
	}

	public UIDrawCall FindDrawCall(UIWidget uiwidget_0)
	{
		Material material_ = uiwidget_0.Material_0;
		Texture texture_ = uiwidget_0.Texture_0;
		int int32_ = uiwidget_0.Int32_2;
		int num = 0;
		UIDrawCall uIDrawCall;
		while (true)
		{
			if (num < betterList_3.size)
			{
				uIDrawCall = betterList_3.buffer[num];
				int num2 = ((num != 0) ? (betterList_3.buffer[num - 1].int_2 + 1) : int.MinValue);
				int num3 = ((num + 1 != betterList_3.size) ? (betterList_3.buffer[num + 1].int_1 - 1) : int.MaxValue);
				if (num2 <= int32_ && num3 >= int32_)
				{
					break;
				}
				num++;
				continue;
			}
			bool_12 = true;
			return null;
		}
		if (uIDrawCall.Material_0 == material_ && uIDrawCall.Texture_0 == texture_)
		{
			if (uiwidget_0.Boolean_2)
			{
				uiwidget_0.uidrawCall_0 = uIDrawCall;
				if (uiwidget_0.Boolean_3)
				{
					uIDrawCall.isDirty = true;
				}
				return uIDrawCall;
			}
		}
		else
		{
			bool_12 = true;
		}
		return null;
	}

	public void AddWidget(UIWidget uiwidget_0)
	{
		bool_16 = true;
		if (betterList_2.size == 0)
		{
			betterList_2.Add(uiwidget_0);
		}
		else if (bool_15)
		{
			betterList_2.Add(uiwidget_0);
			SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(uiwidget_0, betterList_2[0]) == -1)
		{
			betterList_2.Insert(0, uiwidget_0);
		}
		else
		{
			int num = betterList_2.size;
			while (num > 0)
			{
				if (UIWidget.PanelCompareFunc(uiwidget_0, betterList_2[--num]) != -1)
				{
					betterList_2.Insert(num + 1, uiwidget_0);
					break;
				}
			}
		}
		FindDrawCall(uiwidget_0);
	}

	public void RemoveWidget(UIWidget uiwidget_0)
	{
		if (betterList_2.Remove(uiwidget_0) && uiwidget_0.uidrawCall_0 != null)
		{
			int int32_ = uiwidget_0.Int32_2;
			if (int32_ == uiwidget_0.uidrawCall_0.int_1 || int32_ == uiwidget_0.uidrawCall_0.int_2)
			{
				bool_12 = true;
			}
			uiwidget_0.uidrawCall_0.isDirty = true;
			uiwidget_0.uidrawCall_0 = null;
		}
	}

	public void Refresh()
	{
		bool_12 = true;
		if (betterList_1.size > 0)
		{
			betterList_1[0].LateUpdate();
		}
	}

	public virtual Vector3 CalculateConstrainOffset(Vector2 vector2_4, Vector2 vector2_5)
	{
		Vector4 vector4_ = Vector4_2;
		float num = vector4_.z * 0.5f;
		float num2 = vector4_.w * 0.5f;
		Vector2 vector = new Vector2(vector2_4.x, vector2_4.y);
		Vector2 vector2 = new Vector2(vector2_5.x, vector2_5.y);
		Vector2 vector3 = new Vector2(vector4_.x - num, vector4_.y - num2);
		Vector2 vector4 = new Vector2(vector4_.x + num, vector4_.y + num2);
		if (Clipping_0 == UIDrawCall.Clipping.SoftClip)
		{
			vector3.x += Vector2_1.x;
			vector3.y += Vector2_1.y;
			vector4.x -= Vector2_1.x;
			vector4.y -= Vector2_1.y;
		}
		return NGUIMath.ConstrainRect(vector, vector2, vector3, vector4);
	}

	public bool ConstrainTargetToBounds(Transform transform_1, ref Bounds bounds_0, bool bool_18)
	{
		Vector3 vector = CalculateConstrainOffset(bounds_0.min, bounds_0.max);
		if (vector.sqrMagnitude > 0f)
		{
			if (bool_18)
			{
				transform_1.localPosition += vector;
				bounds_0.center += vector;
				SpringPosition component = transform_1.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(transform_1.gameObject, transform_1.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	public bool ConstrainTargetToBounds(Transform transform_1, bool bool_18)
	{
		Bounds bounds_ = NGUIMath.CalculateRelativeWidgetBounds(base.Transform_0, transform_1);
		return ConstrainTargetToBounds(transform_1, ref bounds_, bool_18);
	}

	public static UIPanel Find(Transform transform_1)
	{
		return Find(transform_1, false, -1);
	}

	public static UIPanel Find(Transform transform_1, bool bool_18)
	{
		return Find(transform_1, bool_18, -1);
	}

	public static UIPanel Find(Transform transform_1, bool bool_18, int int_8)
	{
		UIPanel uIPanel = null;
		while (uIPanel == null && transform_1 != null)
		{
			uIPanel = transform_1.GetComponent<UIPanel>();
			if (!(uIPanel != null))
			{
				if (transform_1.parent == null)
				{
					break;
				}
				transform_1 = transform_1.parent;
				continue;
			}
			return uIPanel;
		}
		return (!bool_18) ? null : NGUITools.CreateUI(transform_1, false, int_8);
	}

	private Vector2 GetWindowSize()
	{
		UIRoot uIRoot_ = base.UIRoot_0;
		Vector2 result = NGUITools.Vector2_0;
		if (uIRoot_ != null)
		{
			result *= uIRoot_.GetPixelSizeAdjustment(Mathf.RoundToInt(result.y));
		}
		return result;
	}

	public Vector2 GetViewSize()
	{
		if (clipping_0 != 0)
		{
			return new Vector2(vector4_1.z, vector4_1.w);
		}
		Vector2 result = NGUITools.Vector2_0;
		UIRoot uIRoot_ = base.UIRoot_0;
		if (uIRoot_ != null)
		{
			result *= uIRoot_.Single_0;
		}
		return result;
	}
}
