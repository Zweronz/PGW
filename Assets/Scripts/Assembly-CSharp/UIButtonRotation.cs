using UnityEngine;

public class UIButtonRotation : MonoBehaviour
{
	public Transform tweenTarget;

	public Vector3 hover = Vector3.zero;

	public Vector3 pressed = Vector3.zero;

	public float duration = 0.2f;

	private Quaternion quaternion_0;

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
			quaternion_0 = tweenTarget.localRotation;
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
			TweenRotation component = tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.Quaternion_1 = quaternion_0;
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
			TweenRotation.Begin(tweenTarget.gameObject, duration, bool_1 ? (quaternion_0 * Quaternion.Euler(pressed)) : ((!UICamera.IsHighlighted(base.gameObject)) ? quaternion_0 : (quaternion_0 * Quaternion.Euler(hover)))).method_0 = UITweener.Method.EaseInOut;
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
			TweenRotation.Begin(tweenTarget.gameObject, duration, (!bool_1) ? quaternion_0 : (quaternion_0 * Quaternion.Euler(hover))).method_0 = UITweener.Method.EaseInOut;
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
