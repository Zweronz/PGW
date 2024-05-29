using UnityEngine;

public class UIDragDropItem : MonoBehaviour
{
	public enum Restriction
	{
		None = 0,
		Horizontal = 1,
		Vertical = 2,
		PressAndHold = 3
	}

	public Restriction restriction;

	public bool cloneOnDrag;

	public float float_0 = 1f;

	protected Transform transform_0;

	protected Transform transform_1;

	protected Collider collider_0;

	protected UIButton uibutton_0;

	protected UIRoot uiroot_0;

	protected UIGrid uigrid_0;

	protected UITable uitable_0;

	protected int int_0 = int.MinValue;

	protected float float_1;

	protected UIDragScrollView uidragScrollView_0;

	protected virtual void Start()
	{
		transform_0 = base.transform;
		collider_0 = base.GetComponent<Collider>();
		uibutton_0 = GetComponent<UIButton>();
		uidragScrollView_0 = GetComponent<UIDragScrollView>();
	}

	private void OnPress(bool bool_0)
	{
		if (bool_0)
		{
			float_1 = RealTime.Single_0;
		}
	}

	private void OnDragStart()
	{
		if (!base.enabled || int_0 != int.MinValue)
		{
			return;
		}
		if (restriction != 0)
		{
			if (restriction == Restriction.Horizontal)
			{
				Vector2 vector2_ = UICamera.mouseOrTouch_0.vector2_3;
				if (Mathf.Abs(vector2_.x) < Mathf.Abs(vector2_.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.Vertical)
			{
				Vector2 vector2_2 = UICamera.mouseOrTouch_0.vector2_3;
				if (Mathf.Abs(vector2_2.x) > Mathf.Abs(vector2_2.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.PressAndHold && float_1 + float_0 > RealTime.Single_0)
			{
				return;
			}
		}
		if (cloneOnDrag)
		{
			GameObject gameObject = NGUITools.AddChild(base.transform.parent.gameObject, base.gameObject);
			gameObject.transform.localPosition = base.transform.localPosition;
			gameObject.transform.localRotation = base.transform.localRotation;
			gameObject.transform.localScale = base.transform.localScale;
			UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
			if (component != null)
			{
				component.Color_0 = GetComponent<UIButtonColor>().Color_0;
			}
			UICamera.mouseOrTouch_0.gameObject_3 = gameObject;
			UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
			component2.Start();
			component2.OnDragDropStart();
		}
		else
		{
			OnDragDropStart();
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (base.enabled && int_0 == UICamera.int_0)
		{
			OnDragDropMove((Vector3)vector2_0 * uiroot_0.Single_0);
		}
	}

	private void OnDragEnd()
	{
		if (base.enabled && int_0 == UICamera.int_0)
		{
			OnDragDropRelease(UICamera.gameObject_6);
		}
	}

	protected virtual void OnDragDropStart()
	{
		if (uidragScrollView_0 != null)
		{
			uidragScrollView_0.enabled = false;
		}
		if (uibutton_0 != null)
		{
			uibutton_0.Boolean_0 = false;
		}
		else if (collider_0 != null)
		{
			collider_0.enabled = false;
		}
		int_0 = UICamera.int_0;
		transform_1 = transform_0.parent;
		uiroot_0 = NGUITools.FindInParents<UIRoot>(transform_1);
		uigrid_0 = NGUITools.FindInParents<UIGrid>(transform_1);
		uitable_0 = NGUITools.FindInParents<UITable>(transform_1);
		if (UIDragDropRoot.transform_0 != null)
		{
			transform_0.parent = UIDragDropRoot.transform_0;
		}
		Vector3 localPosition = transform_0.localPosition;
		localPosition.z = 0f;
		transform_0.localPosition = localPosition;
		TweenPosition component = GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (uitable_0 != null)
		{
			uitable_0.Boolean_0 = true;
		}
		if (uigrid_0 != null)
		{
			uigrid_0.Boolean_0 = true;
		}
	}

	protected virtual void OnDragDropMove(Vector3 vector3_0)
	{
		transform_0.localPosition += vector3_0;
	}

	protected virtual void OnDragDropRelease(GameObject gameObject_0)
	{
		if (!cloneOnDrag)
		{
			int_0 = int.MinValue;
			if (uibutton_0 != null)
			{
				uibutton_0.Boolean_0 = true;
			}
			else if (collider_0 != null)
			{
				collider_0.enabled = true;
			}
			UIDragDropContainer uIDragDropContainer = ((!gameObject_0) ? null : NGUITools.FindInParents<UIDragDropContainer>(gameObject_0));
			if (uIDragDropContainer != null)
			{
				transform_0.parent = ((!(uIDragDropContainer.reparentTarget != null)) ? uIDragDropContainer.transform : uIDragDropContainer.reparentTarget);
				Vector3 localPosition = transform_0.localPosition;
				localPosition.z = 0f;
				transform_0.localPosition = localPosition;
			}
			else
			{
				transform_0.parent = transform_1;
			}
			transform_1 = transform_0.parent;
			uigrid_0 = NGUITools.FindInParents<UIGrid>(transform_1);
			uitable_0 = NGUITools.FindInParents<UITable>(transform_1);
			if (uidragScrollView_0 != null)
			{
				uidragScrollView_0.enabled = true;
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (uitable_0 != null)
			{
				uitable_0.Boolean_0 = true;
			}
			if (uigrid_0 != null)
			{
				uigrid_0.Boolean_0 = true;
			}
		}
		else
		{
			NGUITools.Destroy(base.gameObject);
		}
	}
}
