using UnityEngine;

public class UIImageButton : MonoBehaviour
{
	public UISprite target;

	public string normalSprite;

	public string hoverSprite;

	public string pressedSprite;

	public string disabledSprite;

	public bool pixelSnap = true;

	public bool Boolean_0
	{
		get
		{
			Collider collider = base.GetComponent<Collider>();
			return (bool)collider && collider.enabled;
		}
		set
		{
			Collider collider = base.GetComponent<Collider>();
			if ((bool)collider && collider.enabled != value)
			{
				collider.enabled = value;
				UpdateImage();
			}
		}
	}

	private void OnEnable()
	{
		if (target == null)
		{
			target = GetComponentInChildren<UISprite>();
		}
		UpdateImage();
	}

	private void OnValidate()
	{
		if (target != null)
		{
			if (string.IsNullOrEmpty(normalSprite))
			{
				normalSprite = target.String_0;
			}
			if (string.IsNullOrEmpty(hoverSprite))
			{
				hoverSprite = target.String_0;
			}
			if (string.IsNullOrEmpty(pressedSprite))
			{
				pressedSprite = target.String_0;
			}
			if (string.IsNullOrEmpty(disabledSprite))
			{
				disabledSprite = target.String_0;
			}
		}
	}

	private void UpdateImage()
	{
		if (target != null)
		{
			if (Boolean_0)
			{
				SetSprite((!UICamera.IsHighlighted(base.gameObject)) ? normalSprite : hoverSprite);
			}
			else
			{
				SetSprite(disabledSprite);
			}
		}
	}

	private void OnHover(bool bool_0)
	{
		if (Boolean_0 && target != null)
		{
			SetSprite((!bool_0) ? normalSprite : hoverSprite);
		}
	}

	private void OnPress(bool bool_0)
	{
		if (bool_0)
		{
			SetSprite(pressedSprite);
		}
		else
		{
			UpdateImage();
		}
	}

	private void SetSprite(string string_0)
	{
		if (!(target.UIAtlas_0 == null) && target.UIAtlas_0.GetSprite(string_0) != null)
		{
			target.String_0 = string_0;
			if (pixelSnap)
			{
				target.MakePixelPerfect();
			}
		}
	}
}
