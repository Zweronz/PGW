using System;
using UnityEngine;

public class UIStretch : MonoBehaviour
{
	public enum Style
	{
		None = 0,
		Horizontal = 1,
		Vertical = 2,
		Both = 3,
		BasedOnHeight = 4,
		FillKeepingRatio = 5,
		FitInternalKeepingRatio = 6
	}

	public Camera uiCamera;

	public GameObject container;

	public Style style;

	public bool runOnlyOnce = true;

	public Vector2 relativeSize = Vector2.one;

	public Vector2 initialSize = Vector2.one;

	public Vector2 borderPadding = Vector2.zero;

	[SerializeField]
	private UIWidget uiwidget_0;

	private Transform transform_0;

	private UIWidget uiwidget_1;

	private UISprite uisprite_0;

	private UIPanel uipanel_0;

	private UIRoot uiroot_0;

	private Animation animation_0;

	private Rect rect_0;

	private bool bool_0;

	private void Awake()
	{
		animation_0 = base.GetComponent<Animation>();
		rect_0 = default(Rect);
		transform_0 = base.transform;
		uiwidget_1 = GetComponent<UIWidget>();
		uisprite_0 = GetComponent<UISprite>();
		uipanel_0 = GetComponent<UIPanel>();
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
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		uiroot_0 = NGUITools.FindInParents<UIRoot>(base.gameObject);
		Update();
		bool_0 = true;
	}

	private void Update()
	{
		if ((animation_0 != null && animation_0.isPlaying) || style == Style.None)
		{
			return;
		}
		UIWidget uIWidget = ((!(container == null)) ? container.GetComponent<UIWidget>() : null);
		UIPanel uIPanel = ((!(container == null) || !(uIWidget == null)) ? container.GetComponent<UIPanel>() : null);
		float num = 1f;
		if (uIWidget != null)
		{
			Bounds bounds = uIWidget.CalculateBounds(base.transform.parent);
			rect_0.x = bounds.min.x;
			rect_0.y = bounds.min.y;
			rect_0.width = bounds.size.x;
			rect_0.height = bounds.size.y;
		}
		else if (uIPanel != null)
		{
			if (uIPanel.Clipping_0 == UIDrawCall.Clipping.None)
			{
				float num2 = ((!(uiroot_0 != null)) ? 0.5f : ((float)uiroot_0.Int32_0 / (float)Screen.height * 0.5f));
				rect_0.xMin = (float)(-Screen.width) * num2;
				rect_0.yMin = (float)(-Screen.height) * num2;
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
			Transform parent = base.transform.parent;
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
			rect_0 = uiCamera.pixelRect;
			if (uiroot_0 != null)
			{
				num = uiroot_0.Single_0;
			}
		}
		float num3 = rect_0.width;
		float num4 = rect_0.height;
		if (num != 1f && num4 > 1f)
		{
			float num5 = (float)uiroot_0.Int32_0 / num4;
			num3 *= num5;
			num4 *= num5;
		}
		Vector3 vector = ((!(uiwidget_1 != null)) ? transform_0.localScale : new Vector3(uiwidget_1.Int32_0, uiwidget_1.Int32_1));
		if (style == Style.BasedOnHeight)
		{
			vector.x = relativeSize.x * num4;
			vector.y = relativeSize.y * num4;
		}
		else if (style == Style.FillKeepingRatio)
		{
			float num6 = num3 / num4;
			float num7 = initialSize.x / initialSize.y;
			if (num7 < num6)
			{
				float num8 = num3 / initialSize.x;
				vector.x = num3;
				vector.y = initialSize.y * num8;
			}
			else
			{
				float num9 = num4 / initialSize.y;
				vector.x = initialSize.x * num9;
				vector.y = num4;
			}
		}
		else if (style == Style.FitInternalKeepingRatio)
		{
			float num10 = num3 / num4;
			float num11 = initialSize.x / initialSize.y;
			if (num11 > num10)
			{
				float num12 = num3 / initialSize.x;
				vector.x = num3;
				vector.y = initialSize.y * num12;
			}
			else
			{
				float num13 = num4 / initialSize.y;
				vector.x = initialSize.x * num13;
				vector.y = num4;
			}
		}
		else
		{
			if (style != Style.Vertical)
			{
				vector.x = relativeSize.x * num3;
			}
			if (style != Style.Horizontal)
			{
				vector.y = relativeSize.y * num4;
			}
		}
		if (uisprite_0 != null)
		{
			float num14 = ((!(uisprite_0.UIAtlas_0 != null)) ? 1f : uisprite_0.UIAtlas_0.Single_0);
			vector.x -= borderPadding.x * num14;
			vector.y -= borderPadding.y * num14;
			if (style != Style.Vertical)
			{
				uisprite_0.Int32_0 = Mathf.RoundToInt(vector.x);
			}
			if (style != Style.Horizontal)
			{
				uisprite_0.Int32_1 = Mathf.RoundToInt(vector.y);
			}
			vector = Vector3.one;
		}
		else if (uiwidget_1 != null)
		{
			if (style != Style.Vertical)
			{
				uiwidget_1.Int32_0 = Mathf.RoundToInt(vector.x - borderPadding.x);
			}
			if (style != Style.Horizontal)
			{
				uiwidget_1.Int32_1 = Mathf.RoundToInt(vector.y - borderPadding.y);
			}
			vector = Vector3.one;
		}
		else if (uipanel_0 != null)
		{
			Vector4 vector4_2 = uipanel_0.Vector4_1;
			if (style != Style.Vertical)
			{
				vector4_2.z = vector.x - borderPadding.x;
			}
			if (style != Style.Horizontal)
			{
				vector4_2.w = vector.y - borderPadding.y;
			}
			uipanel_0.Vector4_1 = vector4_2;
			vector = Vector3.one;
		}
		else
		{
			if (style != Style.Vertical)
			{
				vector.x -= borderPadding.x;
			}
			if (style != Style.Horizontal)
			{
				vector.y -= borderPadding.y;
			}
		}
		if (transform_0.localScale != vector)
		{
			transform_0.localScale = vector;
		}
		if (runOnlyOnce && Application.isPlaying)
		{
			base.enabled = false;
		}
	}
}
