using System;
using UnityEngine;

public class UIAnchor : MonoBehaviour
{
	public enum Side
	{
		BottomLeft = 0,
		Left = 1,
		TopLeft = 2,
		Top = 3,
		TopRight = 4,
		Right = 5,
		BottomRight = 6,
		Bottom = 7,
		Center = 8
	}

	public Camera uiCamera;

	public GameObject container;

	public Side side = Side.Center;

	public bool runOnlyOnce = true;

	public Vector2 relativeOffset = Vector2.zero;

	public Vector2 pixelOffset = Vector2.zero;

	[SerializeField]
	private UIWidget uiwidget_0;

	private Transform transform_0;

	private Animation animation_0;

	private Rect rect_0 = default(Rect);

	private UIRoot uiroot_0;

	private bool bool_0;

	private void Awake()
	{
		transform_0 = base.transform;
		animation_0 = base.GetComponent<Animation>();
		UICamera.onScreenResize_0 = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize_0, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void OnDestroy()
	{
		UICamera.onScreenResize_0 = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize_0, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void ScreenSizeChanged()
	{
		if (bool_0 && runOnlyOnce)
		{
			Update();
		}
	}

	private void Start()
	{
		if (container == null && uiwidget_0 != null)
		{
			container = uiwidget_0.gameObject;
			uiwidget_0 = null;
		}
		uiroot_0 = NGUITools.FindInParents<UIRoot>(base.gameObject);
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		Update();
		bool_0 = true;
	}

	private void Update()
	{
		if (animation_0 != null && animation_0.enabled && animation_0.isPlaying)
		{
			return;
		}
		bool flag = false;
		UIWidget uIWidget = ((!(container == null)) ? container.GetComponent<UIWidget>() : null);
		UIPanel uIPanel = ((!(container == null) || !(uIWidget == null)) ? container.GetComponent<UIPanel>() : null);
		if (uIWidget != null)
		{
			Bounds bounds = uIWidget.CalculateBounds(container.transform.parent);
			rect_0.x = bounds.min.x;
			rect_0.y = bounds.min.y;
			rect_0.width = bounds.size.x;
			rect_0.height = bounds.size.y;
		}
		else if (uIPanel != null)
		{
			if (uIPanel.Clipping_0 == UIDrawCall.Clipping.None)
			{
				Vector2 vector2_ = NGUITools.Vector2_0;
				float num = ((!(uiroot_0 != null)) ? 0.5f : ((float)uiroot_0.Int32_0 / vector2_.y * 0.5f));
				rect_0.xMin = (0f - vector2_.x) * num;
				rect_0.yMin = (0f - vector2_.y) * num;
				rect_0.xMax = 0f - rect_0.xMin;
				rect_0.yMax = 0f - rect_0.yMin;
			}
			else
			{
				Vector4 vector4_ = uIPanel.Vector4_2;
				rect_0.x = vector4_.x - vector4_.z * 0.5f;
				rect_0.y = vector4_.y - vector4_.w * 0.5f;
				rect_0.width = vector4_.z;
				rect_0.height = vector4_.w;
			}
		}
		else if (container != null)
		{
			Transform parent = container.transform.parent;
			Bounds bounds2 = ((!(parent != null)) ? NGUIMath.CalculateRelativeWidgetBounds(container.transform) : NGUIMath.CalculateRelativeWidgetBounds(parent, container.transform));
			rect_0.x = bounds2.min.x;
			rect_0.y = bounds2.min.y;
			rect_0.width = bounds2.size.x;
			rect_0.height = bounds2.size.y;
		}
		else
		{
			if (!(uiCamera != null))
			{
				return;
			}
			flag = true;
			Vector2 vector2_2 = NGUITools.Vector2_0;
			rect_0 = uiCamera.rect;
			rect_0.xMin *= vector2_2.x;
			rect_0.yMin *= vector2_2.y;
			rect_0.xMax *= vector2_2.x;
			rect_0.yMax *= vector2_2.y;
		}
		float x = (rect_0.xMin + rect_0.xMax) * 0.5f;
		float y = (rect_0.yMin + rect_0.yMax) * 0.5f;
		Vector3 vector = new Vector3(x, y, 0f);
		if (side != Side.Center)
		{
			if (side != Side.Right && side != Side.TopRight && side != Side.BottomRight)
			{
				if (side != Side.Top && side != Side.Center && side != Side.Bottom)
				{
					vector.x = rect_0.xMin;
				}
				else
				{
					vector.x = x;
				}
			}
			else
			{
				vector.x = rect_0.xMax;
			}
			if (side != Side.Top && side != Side.TopRight && side != Side.TopLeft)
			{
				if (side != Side.Left && side != Side.Center && side != Side.Right)
				{
					vector.y = rect_0.yMin;
				}
				else
				{
					vector.y = y;
				}
			}
			else
			{
				vector.y = rect_0.yMax;
			}
		}
		float width = rect_0.width;
		float height = rect_0.height;
		vector.x += pixelOffset.x + relativeOffset.x * width;
		vector.y += pixelOffset.y + relativeOffset.y * height;
		if (flag)
		{
			if (uiCamera.orthographic)
			{
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
			}
			vector.z = uiCamera.WorldToScreenPoint(transform_0.position).z;
			if (uiCamera.rect.width > 0f)
			{
				vector = uiCamera.ScreenToWorldPoint(vector);
			}
		}
		else
		{
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			if (uIPanel != null)
			{
				vector = uIPanel.Transform_0.TransformPoint(vector);
			}
			else if (container != null)
			{
				Transform parent2 = container.transform.parent;
				if (parent2 != null)
				{
					vector = parent2.TransformPoint(vector);
				}
			}
			vector.z = transform_0.position.z;
		}
		if (uiCamera.rect.width > 0f && transform_0.position != vector)
		{
			transform_0.position = vector;
		}
		if (runOnlyOnce && Application.isPlaying)
		{
			base.enabled = false;
		}
	}
}
