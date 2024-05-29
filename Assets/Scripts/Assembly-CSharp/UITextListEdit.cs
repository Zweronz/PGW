using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UITextListEdit : MonoBehaviour
{
	public enum Style
	{
		Text = 0,
		Chat = 1
	}

	protected class Paragraph
	{
		public string string_0;

		public string[] string_1;
	}

	public Style style;

	public UILabel textLabel;

	public float maxWidth;

	public float maxHeight;

	public int maxEntries = 50;

	public bool supportScrollWheel = true;

	protected char[] char_0 = new char[1] { '\n' };

	protected List<Paragraph> list_0 = new List<Paragraph>();

	protected float float_0;

	protected bool bool_0;

	protected int int_0;

	public void Clear()
	{
		list_0.Clear();
		UpdateVisibleText();
	}

	public void Add(string string_0)
	{
		Add(string_0, true);
	}

	protected void Add(string string_0, bool bool_1)
	{
		Paragraph paragraph = null;
		if (list_0.Count < maxEntries)
		{
			paragraph = new Paragraph();
		}
		else
		{
			paragraph = list_0[0];
			list_0.RemoveAt(0);
		}
		paragraph.string_0 = string_0;
		list_0.Add(paragraph);
		if (textLabel != null && textLabel.UIFont_0 != null)
		{
			int_0 = 0;
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				int_0 += list_0[i].string_1.Length;
			}
		}
		if (bool_1)
		{
			UpdateVisibleText();
		}
	}

	private void Awake()
	{
		if (textLabel == null)
		{
			textLabel = GetComponentInChildren<UILabel>();
		}
		if (textLabel != null)
		{
			textLabel.Int32_8 = 0;
		}
		Collider collider = base.GetComponent<Collider>();
		if (collider != null)
		{
			if (maxHeight <= 0f)
			{
				maxHeight = collider.bounds.size.y / base.transform.lossyScale.y;
			}
			if (maxWidth <= 0f)
			{
				maxWidth = collider.bounds.size.x / base.transform.lossyScale.x;
			}
		}
	}

	public void OnSelect(bool bool_1)
	{
		bool_0 = bool_1;
	}

	protected void UpdateVisibleText()
	{
		if (!(textLabel != null))
		{
			return;
		}
		UIFont uIFont_ = textLabel.UIFont_0;
		if (!(uIFont_ != null))
		{
			return;
		}
		int num = 0;
		int num2 = ((!(maxHeight > 0f)) ? 100000 : Mathf.FloorToInt(maxHeight / textLabel.Transform_0.localScale.y));
		int num3 = Mathf.RoundToInt(float_0);
		if (num2 + num3 > int_0)
		{
			num3 = Mathf.Max(0, int_0 - num2);
			float_0 = num3;
		}
		if (style == Style.Chat)
		{
			num3 = Mathf.Max(0, int_0 - num2 - num3);
		}
		StringBuilder stringBuilder = new StringBuilder();
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			Paragraph paragraph = list_0[list_0.Count - 1 - i];
			int j = 0;
			for (int num4 = paragraph.string_1.Length; j < num4; j++)
			{
				string value = paragraph.string_1[j];
				if (num3 > 0)
				{
					num3--;
					continue;
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append(value);
				num++;
				if (num >= num2)
				{
					break;
				}
			}
			if (num >= num2)
			{
				break;
			}
		}
		textLabel.String_0 = stringBuilder.ToString();
	}

	public void OnScroll(float float_1)
	{
		if (bool_0 && supportScrollWheel)
		{
			float_1 *= ((style != Style.Chat) ? (-10f) : 10f);
			float_0 = Mathf.Max(0f, float_0 + float_1);
			UpdateVisibleText();
		}
	}
}
