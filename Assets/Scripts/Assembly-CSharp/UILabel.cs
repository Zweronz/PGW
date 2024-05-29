using System;
using System.Collections.Generic;
using UnityEngine;

public class UILabel : UIWidget
{
	public enum Effect
	{
		None = 0,
		Shadow = 1,
		Outline = 2
	}

	public enum Overflow
	{
		ShrinkContent = 0,
		ClampContent = 1,
		ResizeFreely = 2,
		ResizeHeight = 3
	}

	public enum Crispness
	{
		Never = 0,
		OnDesktop = 1,
		Always = 2
	}

	public Crispness crispness_0 = Crispness.OnDesktop;

	[SerializeField]
	private Font font_0;

	[SerializeField]
	private UIFont uifont_0;

	[Multiline(6)]
	[SerializeField]
	private string string_0 = string.Empty;

	[SerializeField]
	private int int_6 = 16;

	[SerializeField]
	private FontStyle fontStyle_0;

	[SerializeField]
	private NGUIText.Alignment alignment_0;

	[SerializeField]
	private bool bool_14 = true;

	[SerializeField]
	private int int_7;

	[SerializeField]
	private Effect effect_0;

	[SerializeField]
	private Color color_1 = Color.black;

	[SerializeField]
	private NGUIText.SymbolStyle symbolStyle_0 = NGUIText.SymbolStyle.Normal;

	[SerializeField]
	private Vector2 vector2_0 = Vector2.one;

	[SerializeField]
	private Overflow overflow_0;

	[SerializeField]
	private Material material_0;

	[SerializeField]
	private bool bool_15;

	[SerializeField]
	private Color color_2 = Color.white;

	[SerializeField]
	private Color color_3 = new Color(0.7f, 0.7f, 0.7f);

	[SerializeField]
	private int int_8;

	[SerializeField]
	private int int_9;

	[SerializeField]
	private bool bool_16;

	[SerializeField]
	private int int_10;

	[SerializeField]
	private int int_11;

	[SerializeField]
	private float float_2;

	[SerializeField]
	private bool bool_17 = true;

	[NonSerialized]
	private Font font_1;

	private float float_3 = 1f;

	private bool bool_18 = true;

	private string string_1;

	private bool bool_19;

	private Vector2 vector2_1 = Vector2.zero;

	private float float_4 = 1f;

	private int int_12;

	private int int_13;

	private int int_14;

	private static BetterList<UILabel> betterList_1 = new BetterList<UILabel>();

	private static Dictionary<Font, int> dictionary_0 = new Dictionary<Font, int>();

	private static BetterList<Vector3> betterList_2 = new BetterList<Vector3>();

	private static BetterList<int> betterList_3 = new BetterList<int>();

	private bool Boolean_5
	{
		get
		{
			return bool_18;
		}
		set
		{
			if (value)
			{
				bool_0 = true;
				bool_18 = true;
			}
			else
			{
				bool_18 = false;
			}
		}
	}

	public override bool Boolean_12
	{
		get
		{
			return base.Boolean_12 || overflow_0 == Overflow.ResizeFreely;
		}
	}

	public override bool Boolean_13
	{
		get
		{
			return base.Boolean_13 || overflow_0 == Overflow.ResizeFreely || overflow_0 == Overflow.ResizeHeight;
		}
	}

	public override Material Material_0
	{
		get
		{
			if (material_0 != null)
			{
				return material_0;
			}
			if (uifont_0 != null)
			{
				return uifont_0.Material_0;
			}
			if (font_0 != null)
			{
				return font_0.material;
			}
			return null;
		}
		set
		{
			if (material_0 != value)
			{
				MarkAsChanged();
				material_0 = value;
				MarkAsChanged();
			}
		}
	}

	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont UIFont_0
	{
		get
		{
			return UIFont_1;
		}
		set
		{
			UIFont_1 = value;
		}
	}

	public UIFont UIFont_1
	{
		get
		{
			return uifont_0;
		}
		set
		{
			if (uifont_0 != value)
			{
				RemoveFromPanel();
				uifont_0 = value;
				font_0 = null;
				MarkAsChanged();
			}
		}
	}

	public Font Font_0
	{
		get
		{
			if (font_0 != null)
			{
				return font_0;
			}
			return (!(uifont_0 != null)) ? null : uifont_0.Font_0;
		}
		set
		{
			if (font_0 != value)
			{
				SetActiveFont(null);
				RemoveFromPanel();
				font_0 = value;
				Boolean_5 = true;
				uifont_0 = null;
				SetActiveFont(value);
				ProcessAndRequest();
				if (font_1 != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	public UnityEngine.Object Object_0
	{
		get
		{
			return (!(uifont_0 != null)) ? ((UnityEngine.Object)font_0) : ((UnityEngine.Object)uifont_0);
		}
		set
		{
			UIFont uIFont = value as UIFont;
			if (uIFont != null)
			{
				UIFont_1 = uIFont;
			}
			else
			{
				Font_0 = value as Font;
			}
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			if (string_0 == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(string_0))
				{
					string_0 = string.Empty;
					MarkAsChanged();
					ProcessAndRequest();
				}
			}
			else if (string_0 != value)
			{
				string_0 = value;
				MarkAsChanged();
				ProcessAndRequest();
			}
			if (bool_6)
			{
				ResizeCollider();
			}
		}
	}

	public new int Int32_4
	{
		get
		{
			return (Font_0 != null) ? int_6 : ((!(uifont_0 != null)) ? 16 : uifont_0.Int32_3);
		}
	}

	public new int Int32_5
	{
		get
		{
			return int_6;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (int_6 != value)
			{
				int_6 = value;
				Boolean_5 = true;
				ProcessAndRequest();
			}
		}
	}

	public FontStyle FontStyle_0
	{
		get
		{
			return fontStyle_0;
		}
		set
		{
			if (fontStyle_0 != value)
			{
				fontStyle_0 = value;
				Boolean_5 = true;
				ProcessAndRequest();
			}
		}
	}

	public NGUIText.Alignment Alignment_0
	{
		get
		{
			return alignment_0;
		}
		set
		{
			if (alignment_0 != value)
			{
				alignment_0 = value;
				Boolean_5 = true;
				ProcessAndRequest();
			}
		}
	}

	public bool Boolean_6
	{
		get
		{
			return bool_15;
		}
		set
		{
			if (bool_15 != value)
			{
				bool_15 = value;
				MarkAsChanged();
			}
		}
	}

	public Color Color_1
	{
		get
		{
			return color_2;
		}
		set
		{
			if (color_2 != value)
			{
				color_2 = value;
				if (bool_15)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public Color Color_2
	{
		get
		{
			return color_3;
		}
		set
		{
			if (color_3 != value)
			{
				color_3 = value;
				if (bool_15)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public int Int32_6
	{
		get
		{
			return int_8;
		}
		set
		{
			if (int_8 != value)
			{
				int_8 = value;
				MarkAsChanged();
			}
		}
	}

	public int Int32_7
	{
		get
		{
			return int_9;
		}
		set
		{
			if (int_9 != value)
			{
				int_9 = value;
				MarkAsChanged();
			}
		}
	}

	private new bool Boolean_7
	{
		get
		{
			if (Font_0 != null && crispness_0 != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool Boolean_8
	{
		get
		{
			return bool_14;
		}
		set
		{
			if (bool_14 != value)
			{
				bool_14 = value;
				Boolean_5 = true;
			}
		}
	}

	public NGUIText.SymbolStyle SymbolStyle_0
	{
		get
		{
			return symbolStyle_0;
		}
		set
		{
			if (symbolStyle_0 != value)
			{
				symbolStyle_0 = value;
				Boolean_5 = true;
			}
		}
	}

	public Overflow Overflow_0
	{
		get
		{
			return overflow_0;
		}
		set
		{
			if (overflow_0 != value)
			{
				overflow_0 = value;
				Boolean_5 = true;
			}
		}
	}

	[Obsolete("Use 'width' instead")]
	public int Int32_8
	{
		get
		{
			return base.Int32_0;
		}
		set
		{
			base.Int32_0 = value;
		}
	}

	[Obsolete("Use 'height' instead")]
	public int Int32_9
	{
		get
		{
			return base.Int32_1;
		}
		set
		{
			base.Int32_1 = value;
		}
	}

	public bool Boolean_9
	{
		get
		{
			return int_7 != 1;
		}
		set
		{
			if (int_7 != 1 != value)
			{
				int_7 = ((!value) ? 1 : 0);
				Boolean_5 = true;
			}
		}
	}

	public override Vector3[] Vector3_2
	{
		get
		{
			if (Boolean_5)
			{
				ProcessText();
			}
			return base.Vector3_2;
		}
	}

	public override Vector3[] Vector3_3
	{
		get
		{
			if (Boolean_5)
			{
				ProcessText();
			}
			return base.Vector3_3;
		}
	}

	public override Vector4 Vector4_2
	{
		get
		{
			if (Boolean_5)
			{
				ProcessText();
			}
			return base.Vector4_2;
		}
	}

	public int Int32_10
	{
		get
		{
			return int_7;
		}
		set
		{
			if (int_7 != value)
			{
				int_7 = Mathf.Max(value, 0);
				Boolean_5 = true;
				if (Overflow_0 == Overflow.ShrinkContent)
				{
					MakePixelPerfect();
				}
			}
		}
	}

	public Effect Effect_0
	{
		get
		{
			return effect_0;
		}
		set
		{
			if (effect_0 != value)
			{
				effect_0 = value;
				Boolean_5 = true;
			}
		}
	}

	public Color Color_3
	{
		get
		{
			return color_1;
		}
		set
		{
			if (color_1 != value)
			{
				color_1 = value;
				if (effect_0 != 0)
				{
					Boolean_5 = true;
				}
			}
		}
	}

	public Vector2 Vector2_2
	{
		get
		{
			return vector2_0;
		}
		set
		{
			if (vector2_0 != value)
			{
				vector2_0 = value;
				Boolean_5 = true;
			}
		}
	}

	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool Boolean_10
	{
		get
		{
			return overflow_0 == Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				Overflow_0 = Overflow.ShrinkContent;
			}
		}
	}

	public string String_1
	{
		get
		{
			if (int_13 != int_1 || int_14 != int_2)
			{
				int_13 = int_1;
				int_14 = int_2;
				bool_18 = true;
			}
			if (Boolean_5)
			{
				ProcessText();
			}
			return string_1;
		}
	}

	public Vector2 Vector2_3
	{
		get
		{
			if (Boolean_5)
			{
				ProcessText();
			}
			return vector2_1;
		}
	}

	public override Vector2 Vector2_4
	{
		get
		{
			if (Boolean_5)
			{
				ProcessText();
			}
			return base.Vector2_4;
		}
	}

	private bool Boolean_11
	{
		get
		{
			return uifont_0 != null || font_0 != null;
		}
	}

	protected override void OnInit()
	{
		base.OnInit();
		betterList_1.Add(this);
		SetActiveFont(Font_0);
	}

	protected override void OnDisable()
	{
		SetActiveFont(null);
		betterList_1.Remove(this);
		base.OnDisable();
	}

	protected void SetActiveFont(Font font_2)
	{
		if (!(font_1 != font_2))
		{
			return;
		}
		if (font_1 != null)
		{
			int value;
			if (dictionary_0.TryGetValue(font_1, out value))
			{
				value = Mathf.Max(0, --value);
				if (value == 0)
				{
					font_1.textureRebuildCallback = null;
					dictionary_0.Remove(font_1);
				}
				else
				{
					dictionary_0[font_1] = value;
				}
			}
			else
			{
				font_1.textureRebuildCallback = null;
			}
		}
		font_1 = font_2;
		if (font_1 != null)
		{
			int value2 = 0;
			if (!dictionary_0.TryGetValue(font_1, out value2))
			{
				font_1.textureRebuildCallback = OnFontTextureChanged;
			}
			value2 = (dictionary_0[font_1] = value2 + 1);
		}
	}

	private static void OnFontTextureChanged()
	{
		for (int i = 0; i < betterList_1.size; i++)
		{
			UILabel uILabel = betterList_1[i];
			if (uILabel != null)
			{
				Font font = uILabel.Font_0;
				if (font != null)
				{
					font.RequestCharactersInTexture(uILabel.string_0, uILabel.int_12, uILabel.fontStyle_0);
					uILabel.MarkAsChanged();
				}
			}
		}
	}

	public override Vector3[] GetSides(Transform transform_1)
	{
		if (Boolean_5)
		{
			ProcessText();
		}
		return base.GetSides(transform_1);
	}

	protected override void UpgradeFrom265()
	{
		ProcessText(true, true);
		if (bool_16)
		{
			Overflow_0 = Overflow.ShrinkContent;
			int_7 = 0;
		}
		if (int_10 != 0)
		{
			base.Int32_0 = int_10;
			Overflow_0 = ((int_7 > 0) ? Overflow.ResizeHeight : Overflow.ShrinkContent);
		}
		else
		{
			Overflow_0 = Overflow.ResizeFreely;
		}
		if (int_11 != 0)
		{
			base.Int32_1 = int_11;
		}
		if (uifont_0 != null)
		{
			int int32_ = uifont_0.Int32_3;
			if (base.Int32_1 < int32_)
			{
				base.Int32_1 = int32_;
			}
		}
		int_10 = 0;
		int_11 = 0;
		bool_16 = false;
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	protected override void OnAnchor()
	{
		if (overflow_0 == Overflow.ResizeFreely)
		{
			if (base.Boolean_0)
			{
				overflow_0 = Overflow.ShrinkContent;
			}
		}
		else if (overflow_0 == Overflow.ResizeHeight && topAnchor.target != null && bottomAnchor.target != null)
		{
			overflow_0 = Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	private void ProcessAndRequest()
	{
		if (Object_0 != null)
		{
			ProcessText();
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
		if (float_2 > 0f)
		{
			int_10 = Mathf.RoundToInt(float_2);
			float_2 = 0f;
		}
		if (!bool_17)
		{
			int_7 = 1;
			bool_17 = true;
		}
		bool_19 = Material_0 != null && Material_0.shader != null && Material_0.shader.name.Contains("Premultiplied");
		ProcessAndRequest();
	}

	public override void MarkAsChanged()
	{
		Boolean_5 = true;
		base.MarkAsChanged();
	}

	public void ProcessText()
	{
		ProcessText(false, true);
	}

	private void ProcessText(bool bool_20, bool bool_21)
	{
		if (!Boolean_11)
		{
			return;
		}
		bool_0 = true;
		Boolean_5 = false;
		NGUIText.int_1 = ((!bool_20) ? base.Int32_0 : ((int_10 == 0) ? 1000000 : int_10));
		NGUIText.int_2 = ((!bool_20) ? base.Int32_1 : ((int_11 == 0) ? 1000000 : int_11));
		int_12 = Mathf.Abs((!bool_20) ? Int32_4 : Mathf.RoundToInt(base.Transform_0.localScale.x));
		float_4 = 1f;
		if (NGUIText.int_1 >= 1 && NGUIText.int_2 >= 0)
		{
			bool flag;
			if ((flag = Font_0 != null) && Boolean_7)
			{
				UIRoot uIRoot_ = base.UIRoot_0;
				if (uIRoot_ != null)
				{
					float_3 = ((!(uIRoot_ != null)) ? 1f : uIRoot_.Single_0);
				}
			}
			else
			{
				float_3 = 1f;
			}
			if (bool_21)
			{
				UpdateNGUIText();
			}
			if (overflow_0 == Overflow.ResizeFreely)
			{
				NGUIText.int_1 = 1000000;
			}
			if (overflow_0 == Overflow.ResizeFreely || overflow_0 == Overflow.ResizeHeight)
			{
				NGUIText.int_2 = 1000000;
			}
			if (int_12 > 0)
			{
				bool boolean_ = Boolean_7;
				int num = int_12;
				while (num > 0)
				{
					if (boolean_)
					{
						int_12 = num;
						NGUIText.int_0 = int_12;
					}
					else
					{
						float_4 = (float)num / (float)int_12;
						NGUIText.float_0 = ((!flag) ? ((float)int_6 / (float)uifont_0.Int32_3 * float_4) : float_4);
					}
					NGUIText.Update(false);
					bool flag2 = NGUIText.WrapText(string_0, out string_1, true);
					if (overflow_0 == Overflow.ShrinkContent && !flag2)
					{
						if (--num <= 1)
						{
							break;
						}
						num--;
						continue;
					}
					if (overflow_0 == Overflow.ResizeFreely)
					{
						vector2_1 = NGUIText.CalculatePrintedSize(string_1);
						int_1 = Mathf.Max(((UIWidget)this).Int32_4, Mathf.RoundToInt(vector2_1.x));
						int_2 = Mathf.Max(((UIWidget)this).Int32_5, Mathf.RoundToInt(vector2_1.y));
						if ((int_1 & 1) == 1)
						{
							int_1++;
						}
						if ((int_2 & 1) == 1)
						{
							int_2++;
						}
					}
					else if (overflow_0 == Overflow.ResizeHeight)
					{
						vector2_1 = NGUIText.CalculatePrintedSize(string_1);
						int_2 = Mathf.Max(((UIWidget)this).Int32_5, Mathf.RoundToInt(vector2_1.y));
						if ((int_2 & 1) == 1)
						{
							int_2++;
						}
					}
					else
					{
						vector2_1 = NGUIText.CalculatePrintedSize(string_1);
					}
					if (bool_20)
					{
						base.Int32_0 = Mathf.RoundToInt(vector2_1.x);
						base.Int32_1 = Mathf.RoundToInt(vector2_1.y);
						base.Transform_0.localScale = Vector3.one;
					}
					break;
				}
			}
			else
			{
				base.Transform_0.localScale = Vector3.one;
				string_1 = string.Empty;
				float_4 = 1f;
			}
			if (bool_21)
			{
				NGUIText.uifont_0 = null;
				NGUIText.font_0 = null;
			}
		}
		else
		{
			string_1 = string.Empty;
		}
	}

	public override void MakePixelPerfect()
	{
		if (Object_0 != null)
		{
			Vector3 localPosition = base.Transform_0.localPosition;
			localPosition.x = Mathf.RoundToInt(localPosition.x);
			localPosition.y = Mathf.RoundToInt(localPosition.y);
			localPosition.z = Mathf.RoundToInt(localPosition.z);
			base.Transform_0.localPosition = localPosition;
			base.Transform_0.localScale = Vector3.one;
			if (overflow_0 == Overflow.ResizeFreely)
			{
				AssumeNaturalSize();
				return;
			}
			int int32_ = base.Int32_0;
			int int32_2 = base.Int32_1;
			Overflow overflow = overflow_0;
			if (overflow != Overflow.ResizeHeight)
			{
				int_1 = 100000;
			}
			int_2 = 100000;
			overflow_0 = Overflow.ShrinkContent;
			ProcessText(false, true);
			overflow_0 = overflow;
			int a = Mathf.RoundToInt(vector2_1.x);
			int a2 = Mathf.RoundToInt(vector2_1.y);
			a = Mathf.Max(a, base.Int32_4);
			a2 = Mathf.Max(a2, base.Int32_5);
			int_1 = Mathf.Max(int32_, a);
			int_2 = Mathf.Max(int32_2, a2);
			MarkAsChanged();
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	public void AssumeNaturalSize()
	{
		if (Object_0 != null)
		{
			int_1 = 100000;
			int_2 = 100000;
			ProcessText(false, true);
			int_1 = Mathf.RoundToInt(vector2_1.x);
			int_2 = Mathf.RoundToInt(vector2_1.y);
			if ((int_1 & 1) == 1)
			{
				int_1++;
			}
			if ((int_2 & 1) == 1)
			{
				int_2++;
			}
			MarkAsChanged();
		}
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 vector3_4)
	{
		return GetCharacterIndexAtPosition(vector3_4);
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 vector2_2)
	{
		return GetCharacterIndexAtPosition(vector2_2);
	}

	public int GetCharacterIndexAtPosition(Vector3 vector3_4)
	{
		Vector2 vector2_ = base.Transform_0.InverseTransformPoint(vector3_4);
		return GetCharacterIndexAtPosition(vector2_);
	}

	public int GetCharacterIndexAtPosition(Vector2 vector2_2)
	{
		if (Boolean_11)
		{
			string value = String_1;
			if (string.IsNullOrEmpty(value))
			{
				return 0;
			}
			UpdateNGUIText();
			NGUIText.PrintCharacterPositions(value, betterList_2, betterList_3);
			if (betterList_2.size > 0)
			{
				ApplyOffset(betterList_2, 0);
				int closestCharacter = NGUIText.GetClosestCharacter(betterList_2, vector2_2);
				closestCharacter = betterList_3[closestCharacter];
				betterList_2.Clear();
				betterList_3.Clear();
				NGUIText.uifont_0 = null;
				NGUIText.font_0 = null;
				return closestCharacter;
			}
			NGUIText.uifont_0 = null;
			NGUIText.font_0 = null;
		}
		return 0;
	}

	public string GetWordAtPosition(Vector3 vector3_4)
	{
		return GetWordAtCharacterIndex(GetCharacterIndexAtPosition(vector3_4));
	}

	public string GetWordAtPosition(Vector2 vector2_2)
	{
		return GetWordAtCharacterIndex(GetCharacterIndexAtPosition(vector2_2));
	}

	public string GetWordAtCharacterIndex(int int_15)
	{
		if (int_15 != -1 && int_15 < string_0.Length)
		{
			int num = string_0.LastIndexOf(' ', int_15) + 1;
			int num2 = string_0.IndexOf(' ', int_15);
			if (num2 == -1)
			{
				num2 = string_0.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					string text = string_0.Substring(num, num3);
					return NGUIText.StripSymbols(text);
				}
			}
		}
		return null;
	}

	public string GetUrlAtPosition(Vector3 vector3_4)
	{
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(vector3_4));
	}

	public string GetUrlAtPosition(Vector2 vector2_2)
	{
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(vector2_2));
	}

	public string GetUrlAtCharacterIndex(int int_15)
	{
		if (int_15 != -1 && int_15 < string_0.Length)
		{
			int num = string_0.LastIndexOf("[url=", int_15);
			if (num != -1)
			{
				num += 5;
				int num2 = string_0.IndexOf("]", num);
				if (num2 != -1)
				{
					return string_0.Substring(num, num2 - num);
				}
			}
		}
		return null;
	}

	public int GetCharacterIndex(int int_15, KeyCode keyCode_0)
	{
		if (Boolean_11)
		{
			string text = String_1;
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			int int32_ = Int32_4;
			UpdateNGUIText();
			NGUIText.PrintCharacterPositions(text, betterList_2, betterList_3);
			if (betterList_2.size > 0)
			{
				ApplyOffset(betterList_2, 0);
				for (int i = 0; i < betterList_3.size; i++)
				{
					if (betterList_3[i] == int_15)
					{
						Vector2 vector = betterList_2[i];
						switch (keyCode_0)
						{
						case KeyCode.UpArrow:
							vector.y += int32_ + Int32_7;
							break;
						case KeyCode.DownArrow:
							vector.y -= int32_ + Int32_7;
							break;
						case KeyCode.Home:
							vector.x -= 1000f;
							break;
						case KeyCode.End:
							vector.x += 1000f;
							break;
						}
						int closestCharacter = NGUIText.GetClosestCharacter(betterList_2, vector);
						closestCharacter = betterList_3[closestCharacter];
						if (closestCharacter == int_15)
						{
							break;
						}
						betterList_2.Clear();
						betterList_3.Clear();
						return closestCharacter;
					}
				}
				betterList_2.Clear();
				betterList_3.Clear();
			}
			NGUIText.uifont_0 = null;
			NGUIText.font_0 = null;
			switch (keyCode_0)
			{
			case KeyCode.DownArrow:
			case KeyCode.End:
				return text.Length;
			case KeyCode.UpArrow:
			case KeyCode.Home:
				return 0;
			}
		}
		return int_15;
	}

	public void PrintOverlay(int int_15, int int_16, UIGeometry uigeometry_1, UIGeometry uigeometry_2, Color color_4, Color color_5)
	{
		if (uigeometry_1 != null)
		{
			uigeometry_1.Clear();
		}
		if (uigeometry_2 != null)
		{
			uigeometry_2.Clear();
		}
		if (!Boolean_11)
		{
			return;
		}
		string text = String_1;
		UpdateNGUIText();
		int size = uigeometry_1.betterList_0.size;
		Vector2 gparam_ = new Vector2(0.5f, 0.5f);
		float num = finalAlpha;
		if (uigeometry_2 != null && int_15 != int_16)
		{
			int size2 = uigeometry_2.betterList_0.size;
			NGUIText.PrintCaretAndSelection(text, int_15, int_16, uigeometry_1.betterList_0, uigeometry_2.betterList_0);
			if (uigeometry_2.betterList_0.size > size2)
			{
				ApplyOffset(uigeometry_2.betterList_0, size2);
				Color32 gparam_2 = new Color(color_5.r, color_5.g, color_5.b, color_5.a * num);
				for (int i = size2; i < uigeometry_2.betterList_0.size; i++)
				{
					uigeometry_2.betterList_1.Add(gparam_);
					uigeometry_2.betterList_2.Add(gparam_2);
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(text, int_15, int_16, uigeometry_1.betterList_0, null);
		}
		ApplyOffset(uigeometry_1.betterList_0, size);
		Color32 gparam_3 = new Color(color_4.r, color_4.g, color_4.b, color_4.a * num);
		for (int j = size; j < uigeometry_1.betterList_0.size; j++)
		{
			uigeometry_1.betterList_1.Add(gparam_);
			uigeometry_1.betterList_2.Add(gparam_3);
		}
		NGUIText.uifont_0 = null;
		NGUIText.font_0 = null;
	}

	public override void OnFill(BetterList<Vector3> betterList_4, BetterList<Vector2> betterList_5, BetterList<Color32> betterList_6)
	{
		if (!Boolean_11)
		{
			return;
		}
		int size = betterList_4.size;
		Color color = base.Color_0;
		color.a = finalAlpha;
		if (uifont_0 != null && uifont_0.Boolean_1)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string text = String_1;
		int size2 = betterList_4.size;
		UpdateNGUIText();
		NGUIText.color_0 = color;
		NGUIText.Print(text, betterList_4, betterList_5, betterList_6);
		NGUIText.uifont_0 = null;
		NGUIText.font_0 = null;
		Vector2 vector = ApplyOffset(betterList_4, size2);
		if ((!(uifont_0 != null) || !uifont_0.Boolean_2) && Effect_0 != 0)
		{
			int size3 = betterList_4.size;
			vector.x = vector2_0.x;
			vector.y = vector2_0.y;
			ApplyShadow(betterList_4, betterList_5, betterList_6, size, size3, vector.x, 0f - vector.y);
			if (Effect_0 == Effect.Outline)
			{
				size = size3;
				size3 = betterList_4.size;
				ApplyShadow(betterList_4, betterList_5, betterList_6, size, size3, 0f - vector.x, vector.y);
				size = size3;
				size3 = betterList_4.size;
				ApplyShadow(betterList_4, betterList_5, betterList_6, size, size3, vector.x, vector.y);
				size = size3;
				size3 = betterList_4.size;
				ApplyShadow(betterList_4, betterList_5, betterList_6, size, size3, 0f - vector.x, 0f - vector.y);
			}
		}
	}

	protected Vector2 ApplyOffset(BetterList<Vector3> betterList_4, int int_15)
	{
		Vector2 vector = base.Vector2_0;
		float f = Mathf.Lerp(0f, -int_1, vector.x);
		float f2 = Mathf.Lerp(int_2, 0f, vector.y) + Mathf.Lerp(vector2_1.y - (float)int_2, 0f, vector.y);
		f = Mathf.Round(f);
		f2 = Mathf.Round(f2);
		for (int i = int_15; i < betterList_4.size; i++)
		{
			betterList_4.buffer[i].x += f;
			betterList_4.buffer[i].y += f2;
		}
		return new Vector2(f, f2);
	}

	private void ApplyShadow(BetterList<Vector3> betterList_4, BetterList<Vector2> betterList_5, BetterList<Color32> betterList_6, int int_15, int int_16, float float_5, float float_6)
	{
		Color color = color_1;
		color.a *= finalAlpha;
		Color32 color2 = ((!(UIFont_1 != null) || !UIFont_1.Boolean_1) ? color : NGUITools.ApplyPMA(color));
		for (int i = int_15; i < int_16; i++)
		{
			betterList_4.Add(betterList_4.buffer[i]);
			betterList_5.Add(betterList_5.buffer[i]);
			betterList_6.Add(betterList_6.buffer[i]);
			Vector3 vector = betterList_4.buffer[i];
			vector.x += float_5;
			vector.y += float_6;
			betterList_4.buffer[i] = vector;
			Color32 color3 = betterList_6.buffer[i];
			if (color3.a == byte.MaxValue)
			{
				betterList_6.buffer[i] = color2;
				continue;
			}
			Color color4 = color;
			color4.a = (float)(int)color3.a / 255f * color.a;
			betterList_6.buffer[i] = ((!(UIFont_1 != null) || !UIFont_1.Boolean_1) ? color4 : NGUITools.ApplyPMA(color4));
		}
	}

	public int CalculateOffsetToFit(string string_2)
	{
		UpdateNGUIText();
		NGUIText.bool_1 = false;
		NGUIText.symbolStyle_0 = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(string_2);
		NGUIText.uifont_0 = null;
		NGUIText.font_0 = null;
		return result;
	}

	public void SetCurrentProgress()
	{
		if (UIProgressBar.uiprogressBar_0 != null)
		{
			String_0 = UIProgressBar.uiprogressBar_0.Single_0.ToString("F");
		}
	}

	public void SetCurrentPercent()
	{
		if (UIProgressBar.uiprogressBar_0 != null)
		{
			String_0 = Mathf.RoundToInt(UIProgressBar.uiprogressBar_0.Single_0 * 100f) + "%";
		}
	}

	public void SetCurrentSelection()
	{
		if (UIPopupList.uipopupList_0 != null)
		{
			String_0 = ((!UIPopupList.uipopupList_0.isLocalized) ? UIPopupList.uipopupList_0.String_0 : Localization.Get(UIPopupList.uipopupList_0.String_0));
		}
	}

	public bool Wrap(string string_2, out string string_3)
	{
		return Wrap(string_2, out string_3, 1000000);
	}

	public bool Wrap(string string_2, out string string_3, int int_15)
	{
		UpdateNGUIText();
		NGUIText.int_2 = int_15;
		bool result = NGUIText.WrapText(string_2, out string_3);
		NGUIText.uifont_0 = null;
		NGUIText.font_0 = null;
		return result;
	}

	public void UpdateNGUIText()
	{
		Font font = Font_0;
		bool flag = font != null;
		NGUIText.int_0 = int_12;
		NGUIText.fontStyle_0 = fontStyle_0;
		NGUIText.int_1 = int_1;
		NGUIText.int_2 = int_2;
		NGUIText.bool_0 = bool_15 && (uifont_0 == null || !uifont_0.Boolean_2);
		NGUIText.color_2 = color_2;
		NGUIText.color_1 = color_3;
		NGUIText.bool_1 = bool_14;
		NGUIText.bool_2 = bool_19;
		NGUIText.symbolStyle_0 = symbolStyle_0;
		NGUIText.int_3 = int_7;
		NGUIText.float_2 = int_8;
		NGUIText.float_3 = int_9;
		NGUIText.float_0 = ((!flag) ? ((float)int_6 / (float)uifont_0.Int32_3 * float_4) : float_4);
		if (uifont_0 != null)
		{
			NGUIText.uifont_0 = uifont_0;
			while (true)
			{
				UIFont uIFont_ = NGUIText.uifont_0.UIFont_0;
				if (uIFont_ == null)
				{
					break;
				}
				NGUIText.uifont_0 = uIFont_;
			}
			if (NGUIText.uifont_0.Boolean_4)
			{
				NGUIText.font_0 = NGUIText.uifont_0.Font_0;
				NGUIText.uifont_0 = null;
			}
			else
			{
				NGUIText.font_0 = null;
			}
		}
		else
		{
			NGUIText.font_0 = font;
			NGUIText.uifont_0 = null;
		}
		if (flag && Boolean_7)
		{
			UIRoot uIRoot_ = base.UIRoot_0;
			if (uIRoot_ != null)
			{
				NGUIText.float_1 = ((!(uIRoot_ != null)) ? 1f : uIRoot_.Single_0);
			}
		}
		else
		{
			NGUIText.float_1 = 1f;
		}
		if (float_3 != NGUIText.float_1)
		{
			ProcessText(false, false);
			NGUIText.int_1 = int_1;
			NGUIText.int_2 = int_2;
		}
		if (Alignment_0 == NGUIText.Alignment.Automatic)
		{
			switch (base.Pivot_1)
			{
			default:
				NGUIText.alignment_0 = NGUIText.Alignment.Center;
				break;
			case Pivot.TopRight:
			case Pivot.Right:
			case Pivot.BottomRight:
				NGUIText.alignment_0 = NGUIText.Alignment.Right;
				break;
			case Pivot.TopLeft:
			case Pivot.Left:
			case Pivot.BottomLeft:
				NGUIText.alignment_0 = NGUIText.Alignment.Left;
				break;
			}
		}
		else
		{
			NGUIText.alignment_0 = Alignment_0;
		}
		NGUIText.Update();
	}
}
