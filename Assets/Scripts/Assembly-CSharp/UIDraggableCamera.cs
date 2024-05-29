using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UIDraggableCamera : MonoBehaviour
{
	public Transform rootForBounds;

	public Vector2 scale = Vector2.one;

	public float scrollWheelFactor;

	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	public bool smoothDragStart = true;

	public float momentumAmount = 35f;

	private Camera camera_0;

	private Transform transform_0;

	private bool bool_0;

	private Vector2 vector2_0 = Vector2.zero;

	private Bounds bounds_0;

	private float float_0;

	private UIRoot uiroot_0;

	private bool bool_1;

	public Vector2 Vector2_0
	{
		get
		{
			return vector2_0;
		}
		set
		{
			vector2_0 = value;
		}
	}

	private void Start()
	{
		camera_0 = base.GetComponent<Camera>();
		transform_0 = base.transform;
		uiroot_0 = NGUITools.FindInParents<UIRoot>(base.gameObject);
		if (rootForBounds == null)
		{
			Debug.LogError(NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	private Vector3 CalculateConstrainOffset()
	{
		if (!(rootForBounds == null) && rootForBounds.childCount != 0)
		{
			Vector3 position = new Vector3(camera_0.rect.xMin * (float)Screen.width, camera_0.rect.yMin * (float)Screen.height, 0f);
			Vector3 position2 = new Vector3(camera_0.rect.xMax * (float)Screen.width, camera_0.rect.yMax * (float)Screen.height, 0f);
			position = camera_0.ScreenToWorldPoint(position);
			position2 = camera_0.ScreenToWorldPoint(position2);
			Vector2 vector = new Vector2(bounds_0.min.x, bounds_0.min.y);
			Vector2 vector2_ = new Vector2(bounds_0.max.x, bounds_0.max.y);
			return NGUIMath.ConstrainRect(vector, vector2_, position, position2);
		}
		return Vector3.zero;
	}

	public bool ConstrainToBounds(bool bool_2)
	{
		if (transform_0 != null && rootForBounds != null)
		{
			Vector3 vector = CalculateConstrainOffset();
			if (vector.sqrMagnitude > 0f)
			{
				if (bool_2)
				{
					transform_0.position -= vector;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(base.gameObject, transform_0.position - vector, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	public void Press(bool bool_2)
	{
		if (bool_2)
		{
			bool_1 = false;
		}
		if (!(rootForBounds != null))
		{
			return;
		}
		bool_0 = bool_2;
		if (bool_2)
		{
			bounds_0 = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
			vector2_0 = Vector2.zero;
			float_0 = 0f;
			SpringPosition component = GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
		else if (dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
		{
			ConstrainToBounds(false);
		}
	}

	public void Drag(Vector2 vector2_1)
	{
		if (smoothDragStart && !bool_1)
		{
			bool_1 = true;
			return;
		}
		UICamera.mouseOrTouch_0.clickNotification_0 = UICamera.ClickNotification.BasedOnDelta;
		if (uiroot_0 != null)
		{
			vector2_1 *= uiroot_0.Single_0;
		}
		Vector2 vector = Vector2.Scale(vector2_1, -scale);
		transform_0.localPosition += (Vector3)vector;
		vector2_0 = Vector2.Lerp(vector2_0, vector2_0 + vector * (0.01f * momentumAmount), 0.67f);
		if (dragEffect != UIDragObject.DragEffect.MomentumAndSpring && ConstrainToBounds(true))
		{
			vector2_0 = Vector2.zero;
			float_0 = 0f;
		}
	}

	public void Scroll(float float_1)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Mathf.Sign(float_0) != Mathf.Sign(float_1))
			{
				float_0 = 0f;
			}
			float_0 += float_1 * scrollWheelFactor;
		}
	}

	private void Update()
	{
		float single_ = RealTime.Single_1;
		if (bool_0)
		{
			SpringPosition component = GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			float_0 = 0f;
		}
		else
		{
			vector2_0 += scale * (float_0 * 20f);
			float_0 = NGUIMath.SpringLerp(float_0, 0f, 20f, single_);
			if (vector2_0.magnitude > 0.01f)
			{
				transform_0.localPosition += (Vector3)NGUIMath.SpringDampen(ref vector2_0, 9f, single_);
				bounds_0 = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				if (!ConstrainToBounds(dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = GetComponent<SpringPosition>();
					if (component2 != null)
					{
						component2.enabled = false;
					}
				}
				return;
			}
			float_0 = 0f;
		}
		NGUIMath.SpringDampen(ref vector2_0, 9f, single_);
	}
}
