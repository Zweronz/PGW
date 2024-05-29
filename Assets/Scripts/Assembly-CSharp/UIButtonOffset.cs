using UnityEngine;

public class UIButtonOffset : MonoBehaviour
{
	public Transform tweenTarget;

	public Vector3 hover = Vector3.zero;

	public Vector3 pressed = new Vector3(2f, -2f);

	public float duration = 0.2f;

	private Vector3 vector3_0;

	private bool bool_0;

	private void Start()
	{
		if (!bool_0)
		{
			bool_0 = true;
			if (tweenTarget == null)
			{
				tweenTarget = base.transform;
			}
			vector3_0 = tweenTarget.localPosition;
		}
	}

	private void OnEnable()
	{
		if (bool_0)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	private void OnDisable()
	{
		if (bool_0 && tweenTarget != null)
		{
			TweenPosition component = tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.Vector3_1 = vector3_0;
				component.enabled = false;
			}
		}
	}

	private void OnPress(bool bool_1)
	{
		if (base.enabled)
		{
			if (!bool_0)
			{
				Start();
			}
			TweenPosition.Begin(tweenTarget.gameObject, duration, bool_1 ? (vector3_0 + pressed) : ((!UICamera.IsHighlighted(base.gameObject)) ? vector3_0 : (vector3_0 + hover))).method_0 = UITweener.Method.EaseInOut;
		}
	}

	private void OnHover(bool bool_1)
	{
		if (base.enabled)
		{
			if (!bool_0)
			{
				Start();
			}
			TweenPosition.Begin(tweenTarget.gameObject, duration, (!bool_1) ? vector3_0 : (vector3_0 + hover)).method_0 = UITweener.Method.EaseInOut;
		}
	}

	private void OnSelect(bool bool_1)
	{
		if (base.enabled && (!bool_1 || UICamera.controlScheme_0 == UICamera.ControlScheme.Controller))
		{
			OnHover(bool_1);
		}
	}
}
