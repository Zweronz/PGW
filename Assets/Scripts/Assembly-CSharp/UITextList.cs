using System.Text;
using UnityEngine;

public class UITextList : MonoBehaviour
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

	public UILabel textLabel;

	public UIProgressBar scrollBar;

	public Style style;

	public int paragraphHistory = 50;

	protected char[] char_0 = new char[1] { '\n' };

	protected BetterList<Paragraph> betterList_0 = new BetterList<Paragraph>();

	protected float float_0;

	protected int int_0;

	protected int int_1;

	protected int int_2;

	public bool Boolean_0
	{
		get
		{
			return textLabel != null && textLabel.Object_0 != null;
		}
	}

	public float Single_0
	{
		get
		{
			return float_0;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (Boolean_0 && float_0 != value)
			{
				if (scrollBar != null)
				{
					scrollBar.Single_0 = value;
					return;
				}
				float_0 = value;
				UpdateVisibleText();
			}
		}
	}

	protected float Single_1
	{
		get
		{
			return (!(textLabel != null)) ? 20f : ((float)(textLabel.Int32_5 + textLabel.Int32_7));
		}
	}

	protected int Int32_0
	{
		get
		{
			if (!Boolean_0)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)textLabel.Int32_1 / Single_1);
			return Mathf.Max(0, int_0 - num);
		}
	}

	public void Clear()
	{
		betterList_0.Clear();
		UpdateVisibleText();
	}

	private void Start()
	{
		if (textLabel == null)
		{
			textLabel = GetComponentInChildren<UILabel>();
		}
		if (scrollBar != null)
		{
			EventDelegate.Add(scrollBar.list_0, OnScrollBar);
		}
		textLabel.Overflow_0 = UILabel.Overflow.ClampContent;
		if (style == Style.Chat)
		{
			textLabel.Pivot_1 = UIWidget.Pivot.BottomLeft;
			Single_0 = 1f;
		}
		else
		{
			textLabel.Pivot_1 = UIWidget.Pivot.TopLeft;
			Single_0 = 0f;
		}
	}

	private void Update()
	{
		if (Boolean_0 && (textLabel.Int32_0 != int_1 || textLabel.Int32_1 != int_2))
		{
			int_1 = textLabel.Int32_0;
			int_2 = textLabel.Int32_1;
			Rebuild();
		}
	}

	public void OnScroll(float float_1)
	{
		int int32_ = Int32_0;
		if (int32_ != 0)
		{
			float_1 *= Single_1;
			Single_0 = float_0 - float_1 / (float)int32_;
		}
	}

	public void OnDrag(Vector2 vector2_0)
	{
		int int32_ = Int32_0;
		if (int32_ != 0)
		{
			float num = vector2_0.y / Single_1;
			Single_0 = float_0 + num / (float)int32_;
		}
	}

	private void OnScrollBar()
	{
		float_0 = UIProgressBar.uiprogressBar_0.Single_0;
		UpdateVisibleText();
	}

	public void Add(string string_0)
	{
		Add(string_0, true);
	}

	protected void Add(string string_0, bool bool_0)
	{
		Paragraph paragraph = null;
		if (betterList_0.size < paragraphHistory)
		{
			paragraph = new Paragraph();
		}
		else
		{
			paragraph = betterList_0[0];
			betterList_0.RemoveAt(0);
		}
		paragraph.string_0 = string_0;
		betterList_0.Add(paragraph);
		Rebuild();
	}

	protected void Rebuild()
	{
		if (!Boolean_0)
		{
			return;
		}
		textLabel.UpdateNGUIText();
		NGUIText.int_2 = 1000000;
		int_0 = 0;
		for (int i = 0; i < betterList_0.size; i++)
		{
			Paragraph paragraph = betterList_0.buffer[i];
			string string_;
			NGUIText.WrapText(paragraph.string_0, out string_);
			paragraph.string_1 = string_.Split('\n');
			int_0 += paragraph.string_1.Length;
		}
		int_0 = 0;
		int j = 0;
		for (int size = betterList_0.size; j < size; j++)
		{
			int_0 += betterList_0.buffer[j].string_1.Length;
		}
		if (scrollBar != null)
		{
			UIScrollBar uIScrollBar = scrollBar as UIScrollBar;
			if (uIScrollBar != null)
			{
				uIScrollBar.Single_4 = ((int_0 != 0) ? (1f - (float)Int32_0 / (float)int_0) : 1f);
			}
		}
		UpdateVisibleText();
	}

	protected void UpdateVisibleText()
	{
		if (!Boolean_0)
		{
			return;
		}
		if (int_0 == 0)
		{
			textLabel.String_0 = string.Empty;
			return;
		}
		int num = Mathf.FloorToInt((float)textLabel.Int32_1 / Single_1);
		int num2 = Mathf.Max(0, int_0 - num);
		int num3 = Mathf.RoundToInt(float_0 * (float)num2);
		if (num3 < 0)
		{
			num3 = 0;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int num4 = 0;
		int size = betterList_0.size;
		while (num > 0 && num4 < size)
		{
			Paragraph paragraph = betterList_0.buffer[num4];
			int num5 = 0;
			int num6 = paragraph.string_1.Length;
			while (num > 0 && num5 < num6)
			{
				string value = paragraph.string_1[num5];
				if (num3 > 0)
				{
					num3--;
				}
				else
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("\n");
					}
					stringBuilder.Append(value);
					num--;
				}
				num5++;
			}
			num4++;
		}
		textLabel.String_0 = stringBuilder.ToString();
	}
}
