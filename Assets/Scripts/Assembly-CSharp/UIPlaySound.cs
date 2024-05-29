using UnityEngine;

public class UIPlaySound : MonoBehaviour
{
	public enum Trigger
	{
		OnClick = 0,
		OnMouseOver = 1,
		OnMouseOut = 2,
		OnPress = 3,
		OnRelease = 4,
		Custom = 5
	}

	public AudioClip audioClip;

	public Trigger trigger;

	private bool bool_0;

	[Range(0f, 1f)]
	public float volume = 1f;

	[Range(0f, 2f)]
	public float pitch = 1f;

	private bool Boolean_0
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = GetComponent<UIButton>();
			return component == null || component.Boolean_0;
		}
	}

	private void OnHover(bool bool_1)
	{
		if (trigger == Trigger.OnMouseOver)
		{
			if (bool_0 == bool_1)
			{
				return;
			}
			bool_0 = bool_1;
		}
		if (Boolean_0 && ((bool_1 && trigger == Trigger.OnMouseOver) || (!bool_1 && trigger == Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(audioClip, volume, pitch);
		}
	}

	private void OnPress(bool bool_1)
	{
		if (trigger == Trigger.OnPress)
		{
			if (bool_0 == bool_1)
			{
				return;
			}
			bool_0 = bool_1;
		}
		if (Boolean_0 && ((bool_1 && trigger == Trigger.OnPress) || (!bool_1 && trigger == Trigger.OnRelease)))
		{
			NGUITools.PlaySound(audioClip, volume, pitch);
		}
	}

	private void OnClick()
	{
		if (Boolean_0 && trigger == Trigger.OnClick)
		{
			NGUITools.PlaySound(audioClip, volume, pitch);
		}
	}

	private void OnSelect(bool bool_1)
	{
		if (Boolean_0 && (!bool_1 || UICamera.controlScheme_0 == UICamera.ControlScheme.Controller))
		{
			OnHover(bool_1);
		}
	}

	public void Play()
	{
		NGUITools.PlaySound(audioClip, volume, pitch);
	}
}
