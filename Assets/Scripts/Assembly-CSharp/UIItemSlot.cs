using System.Collections.Generic;
using UnityEngine;

public abstract class UIItemSlot : MonoBehaviour
{
	public UISprite icon;

	public UIWidget background;

	public UILabel label;

	public AudioClip grabSound;

	public AudioClip placeSound;

	public AudioClip errorSound;

	private InvGameItem invGameItem_0;

	private string string_0 = string.Empty;

	private static InvGameItem invGameItem_1;

	protected abstract InvGameItem InvGameItem_0 { get; }

	protected abstract InvGameItem Replace(InvGameItem invGameItem_2);

	private void OnTooltip(bool bool_0)
	{
		InvGameItem invGameItem = ((!bool_0) ? null : invGameItem_0);
		if (invGameItem != null)
		{
			InvBaseItem invBaseItem_ = invGameItem.InvBaseItem_0;
			if (invBaseItem_ != null)
			{
				string text = "[" + NGUIText.EncodeColor(invGameItem.Color_0) + "]" + invGameItem.String_0 + "[-]\n";
				string text2 = text;
				text = text2 + "[AFAFAF]Level " + invGameItem.itemLevel + " " + invBaseItem_.slot;
				List<InvStat> list = invGameItem.CalculateStats();
				int i = 0;
				for (int count = list.Count; i < count; i++)
				{
					InvStat invStat = list[i];
					if (invStat.amount != 0)
					{
						text = ((invStat.amount >= 0) ? (text + "\n[00FF00]+" + invStat.amount) : (text + "\n[FF0000]" + invStat.amount));
						if (invStat.modifier == InvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + invStat.id;
						text += "[-]";
					}
				}
				if (!string.IsNullOrEmpty(invBaseItem_.description))
				{
					text = text + "\n[FF9900]" + invBaseItem_.description;
				}
				UITooltip.ShowText(text);
				return;
			}
		}
		UITooltip.ShowText(null);
	}

	private void OnClick()
	{
		if (invGameItem_1 != null)
		{
			OnDrop(null);
		}
		else if (invGameItem_0 != null)
		{
			invGameItem_1 = Replace(null);
			if (invGameItem_1 != null)
			{
				NGUITools.PlaySound(grabSound);
			}
			UpdateCursor();
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (invGameItem_1 == null && invGameItem_0 != null)
		{
			UICamera.mouseOrTouch_0.clickNotification_0 = UICamera.ClickNotification.BasedOnDelta;
			invGameItem_1 = Replace(null);
			NGUITools.PlaySound(grabSound);
			UpdateCursor();
		}
	}

	private void OnDrop(GameObject gameObject_0)
	{
		InvGameItem invGameItem = Replace(invGameItem_1);
		if (invGameItem_1 == invGameItem)
		{
			NGUITools.PlaySound(errorSound);
		}
		else if (invGameItem != null)
		{
			NGUITools.PlaySound(grabSound);
		}
		else
		{
			NGUITools.PlaySound(placeSound);
		}
		invGameItem_1 = invGameItem;
		UpdateCursor();
	}

	private void UpdateCursor()
	{
		if (invGameItem_1 != null && invGameItem_1.InvBaseItem_0 != null)
		{
			UICursor.Set(invGameItem_1.InvBaseItem_0.iconAtlas, invGameItem_1.InvBaseItem_0.iconName);
		}
		else
		{
			UICursor.Clear();
		}
	}

	private void Update()
	{
		InvGameItem invGameItem = InvGameItem_0;
		if (invGameItem_0 == invGameItem)
		{
			return;
		}
		invGameItem_0 = invGameItem;
		InvBaseItem invBaseItem = ((invGameItem == null) ? null : invGameItem.InvBaseItem_0);
		if (label != null)
		{
			string text = ((invGameItem == null) ? null : invGameItem.String_0);
			if (string.IsNullOrEmpty(string_0))
			{
				string_0 = label.String_0;
			}
			label.String_0 = ((text == null) ? string_0 : text);
		}
		if (icon != null)
		{
			if (invBaseItem != null && !(invBaseItem.iconAtlas == null))
			{
				icon.UIAtlas_0 = invBaseItem.iconAtlas;
				icon.String_0 = invBaseItem.iconName;
				icon.enabled = true;
				icon.MakePixelPerfect();
			}
			else
			{
				icon.enabled = false;
			}
		}
		if (background != null)
		{
			background.Color_0 = ((invGameItem == null) ? Color.white : invGameItem.Color_0);
		}
	}
}
