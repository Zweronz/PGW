using UnityEngine;

public class UIButtonMessage : MonoBehaviour
{
	public enum Trigger
	{
		OnClick = 0,
		OnMouseOver = 1,
		OnMouseOut = 2,
		OnPress = 3,
		OnRelease = 4,
		OnDoubleClick = 5
	}

	public GameObject target;

	public string functionName;

	public Trigger trigger;

	public bool includeChildren;

	private bool bool_0;

	private void Start()
	{
		bool_0 = true;
	}

	private void OnEnable()
	{
		if (bool_0)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	private void OnHover(bool bool_1)
	{
		if (base.enabled && ((bool_1 && trigger == Trigger.OnMouseOver) || (!bool_1 && trigger == Trigger.OnMouseOut)))
		{
			Send();
		}
	}

	private void OnPress(bool bool_1)
	{
		if (base.enabled && ((bool_1 && trigger == Trigger.OnPress) || (!bool_1 && trigger == Trigger.OnRelease)))
		{
			Send();
		}
	}

	private void OnSelect(bool bool_1)
	{
		if (base.enabled && (!bool_1 || UICamera.controlScheme_0 == UICamera.ControlScheme.Controller))
		{
			OnHover(bool_1);
		}
	}

	private void OnClick()
	{
		if (base.enabled && trigger == Trigger.OnClick)
		{
			Send();
		}
	}

	private void OnDoubleClick()
	{
		if (base.enabled && trigger == Trigger.OnDoubleClick)
		{
			Send();
		}
	}

	private void Send()
	{
		if (string.IsNullOrEmpty(functionName))
		{
			return;
		}
		if (target == null)
		{
			target = base.gameObject;
		}
		if (includeChildren)
		{
			Transform[] componentsInChildren = target.GetComponentsInChildren<Transform>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			target.SendMessage(functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}
}
