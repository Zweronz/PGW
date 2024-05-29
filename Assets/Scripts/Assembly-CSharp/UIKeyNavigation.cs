using UnityEngine;

public class UIKeyNavigation : MonoBehaviour
{
	public enum Constraint
	{
		None = 0,
		Vertical = 1,
		Horizontal = 2,
		Explicit = 3
	}

	public static BetterList<UIKeyNavigation> betterList_0 = new BetterList<UIKeyNavigation>();

	public Constraint constraint;

	public GameObject onUp;

	public GameObject onDown;

	public GameObject onLeft;

	public GameObject onRight;

	public GameObject onClick;

	public bool startsSelected;

	protected virtual void OnEnable()
	{
		betterList_0.Add(this);
		if (startsSelected && (UICamera.GameObject_0 == null || !NGUITools.GetActive(UICamera.GameObject_0)))
		{
			UICamera.controlScheme_0 = UICamera.ControlScheme.Controller;
			UICamera.GameObject_0 = base.gameObject;
		}
	}

	protected virtual void OnDisable()
	{
		betterList_0.Remove(this);
	}

	protected GameObject GetLeft()
	{
		if (NGUITools.GetActive(onLeft))
		{
			return onLeft;
		}
		if (constraint != Constraint.Vertical && constraint != Constraint.Explicit)
		{
			return Get(Vector3.left, true);
		}
		return null;
	}

	private GameObject GetRight()
	{
		if (NGUITools.GetActive(onRight))
		{
			return onRight;
		}
		if (constraint != Constraint.Vertical && constraint != Constraint.Explicit)
		{
			return Get(Vector3.right, true);
		}
		return null;
	}

	protected GameObject GetUp()
	{
		if (NGUITools.GetActive(onUp))
		{
			return onUp;
		}
		if (constraint != Constraint.Horizontal && constraint != Constraint.Explicit)
		{
			return Get(Vector3.up, false);
		}
		return null;
	}

	protected GameObject GetDown()
	{
		if (NGUITools.GetActive(onDown))
		{
			return onDown;
		}
		if (constraint != Constraint.Horizontal && constraint != Constraint.Explicit)
		{
			return Get(Vector3.down, false);
		}
		return null;
	}

	protected GameObject Get(Vector3 vector3_0, bool bool_0)
	{
		Transform transform = base.transform;
		vector3_0 = transform.TransformDirection(vector3_0);
		Vector3 center = GetCenter(base.gameObject);
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < betterList_0.size; i++)
		{
			UIKeyNavigation uIKeyNavigation = betterList_0[i];
			if (uIKeyNavigation == this)
			{
				continue;
			}
			UIButton component = uIKeyNavigation.GetComponent<UIButton>();
			if (component != null && !component.Boolean_0)
			{
				continue;
			}
			Vector3 direction = GetCenter(uIKeyNavigation.gameObject) - center;
			float num2 = Vector3.Dot(vector3_0, direction.normalized);
			if (num2 >= 0.707f)
			{
				direction = transform.InverseTransformDirection(direction);
				if (bool_0)
				{
					direction.y *= 2f;
				}
				else
				{
					direction.x *= 2f;
				}
				float sqrMagnitude = direction.sqrMagnitude;
				if (sqrMagnitude <= num)
				{
					result = uIKeyNavigation.gameObject;
					num = sqrMagnitude;
				}
			}
		}
		return result;
	}

	protected static Vector3 GetCenter(GameObject gameObject_0)
	{
		UIWidget component = gameObject_0.GetComponent<UIWidget>();
		if (component != null)
		{
			Vector3[] vector3_ = component.Vector3_3;
			return (vector3_[0] + vector3_[2]) * 0.5f;
		}
		return gameObject_0.transform.position;
	}

	protected virtual void OnKey(KeyCode keyCode_0)
	{
		if (!NGUITools.GetActive(this))
		{
			return;
		}
		GameObject gameObject = null;
		switch (keyCode_0)
		{
		case KeyCode.Tab:
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				gameObject = GetRight();
				if (gameObject == null)
				{
					gameObject = GetDown();
				}
				if (gameObject == null)
				{
					gameObject = GetUp();
				}
				if (gameObject == null)
				{
					gameObject = GetLeft();
				}
			}
			else
			{
				gameObject = GetLeft();
				if (gameObject == null)
				{
					gameObject = GetUp();
				}
				if (gameObject == null)
				{
					gameObject = GetDown();
				}
				if (gameObject == null)
				{
					gameObject = GetRight();
				}
			}
			break;
		case KeyCode.UpArrow:
			gameObject = GetUp();
			break;
		case KeyCode.DownArrow:
			gameObject = GetDown();
			break;
		case KeyCode.RightArrow:
			gameObject = GetRight();
			break;
		case KeyCode.LeftArrow:
			gameObject = GetLeft();
			break;
		}
		if (gameObject != null)
		{
			UICamera.GameObject_0 = gameObject;
		}
	}

	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(this) && NGUITools.GetActive(onClick))
		{
			UICamera.GameObject_0 = onClick;
		}
	}
}
