using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class NGUIText
{
	public enum Alignment
	{
		Automatic = 0,
		Left = 1,
		Center = 2,
		Right = 3,
		Justified = 4
	}

	public enum SymbolStyle
	{
		None = 0,
		Normal = 1,
		Colored = 2
	}

	public class GlyphInfo
	{
		public Vector2 vector2_0;

		public Vector2 vector2_1;

		public Vector2 vector2_2;

		public Vector2 vector2_3;

		public float float_0;

		public int int_0;

		public bool bool_0;
	}

	public static UIFont uifont_0;

	public static Font font_0;

	public static GlyphInfo glyphInfo_0 = new GlyphInfo();

	public static int int_0 = 16;

	public static float float_0 = 1f;

	public static float float_1 = 1f;

	public static FontStyle fontStyle_0 = FontStyle.Normal;

	public static Alignment alignment_0 = Alignment.Left;

	public static Color color_0 = Color.white;

	public static int int_1 = 1000000;

	public static int int_2 = 1000000;

	public static int int_3 = 0;

	public static bool bool_0 = false;

	public static Color color_1 = Color.white;

	public static Color color_2 = Color.white;

	public static bool bool_1 = false;

	public static float float_2 = 0f;

	public static float float_3 = 0f;

	public static bool bool_2 = false;

	public static SymbolStyle symbolStyle_0;

	public static int int_4 = 0;

	public static float float_4 = 0f;

	public static float float_5 = 0f;

	public static float float_6 = 0f;

	public static bool bool_3 = false;

	private static Color color_3 = new Color(0f, 0f, 0f, 0f);

	private static BetterList<Color> betterList_0 = new BetterList<Color>();

	private static float float_7 = 1f;

	private static CharacterInfo characterInfo_0;

	private static BetterList<float> betterList_1 = new BetterList<float>();

	private static Color32 color32_0;

	private static Color32 color32_1;

	private static float[] float_8 = new float[8] { -0.5f, 0f, 0.5f, 0f, 0f, -0.5f, 0f, 0.5f };

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_0;

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_1;

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_2;

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_3;

	public static void Update()
	{
		Update(true);
	}

	public static void Update(bool bool_4)
	{
		int_4 = Mathf.RoundToInt((float)int_0 / float_1);
		float_4 = float_2 * float_0;
		float_5 = ((float)int_0 + float_3) * float_0;
		bool_3 = uifont_0 != null && uifont_0.Boolean_0 && bool_1 && symbolStyle_0 != SymbolStyle.None;
		if (!(font_0 != null) || !bool_4)
		{
			return;
		}
		font_0.RequestCharactersInTexture(")_-", int_4, fontStyle_0);
		if (!font_0.GetCharacterInfo(')', out characterInfo_0, int_4, fontStyle_0))
		{
			font_0.RequestCharactersInTexture("A", int_4, fontStyle_0);
			if (!font_0.GetCharacterInfo('A', out characterInfo_0, int_4, fontStyle_0))
			{
				float_6 = 0f;
				return;
			}
		}
		float yMax = characterInfo_0.vert.yMax;
		float yMin = characterInfo_0.vert.yMin;
		float_6 = Mathf.Round(yMax + ((float)int_4 - yMax + yMin) * 0.5f);
	}

	public static void Prepare(string string_0)
	{
		if (font_0 != null)
		{
			font_0.RequestCharactersInTexture(string_0, int_4, fontStyle_0);
		}
	}

	public static BMSymbol GetSymbol(string string_0, int int_5, int int_6)
	{
		return (!(uifont_0 != null)) ? null : uifont_0.MatchSymbol(string_0, int_5, int_6);
	}

	public static float GetGlyphWidth(int int_5, int int_6)
	{
		if (uifont_0 != null)
		{
			BMGlyph glyph = uifont_0.BMFont_0.GetGlyph(int_5);
			if (glyph != null)
			{
				return float_0 * (float)((int_6 == 0) ? glyph.advance : (glyph.advance + glyph.GetKerning(int_6)));
			}
		}
		else if (font_0 != null && font_0.GetCharacterInfo((char)int_5, out characterInfo_0, int_4, fontStyle_0))
		{
			return characterInfo_0.width * float_0 * float_1;
		}
		return 0f;
	}

	public static GlyphInfo GetGlyph(int int_5, int int_6)
	{
		if (uifont_0 != null)
		{
			BMGlyph glyph = uifont_0.BMFont_0.GetGlyph(int_5);
			if (glyph != null)
			{
				int num = ((int_6 != 0) ? glyph.GetKerning(int_6) : 0);
				glyphInfo_0.vector2_0.x = ((int_6 == 0) ? glyph.offsetX : (glyph.offsetX + num));
				glyphInfo_0.vector2_1.y = -glyph.offsetY;
				glyphInfo_0.vector2_1.x = glyphInfo_0.vector2_0.x + (float)glyph.width;
				glyphInfo_0.vector2_0.y = glyphInfo_0.vector2_1.y - (float)glyph.height;
				glyphInfo_0.vector2_2.x = glyph.x;
				glyphInfo_0.vector2_2.y = glyph.y + glyph.height;
				glyphInfo_0.vector2_3.x = glyph.x + glyph.width;
				glyphInfo_0.vector2_3.y = glyph.y;
				glyphInfo_0.float_0 = glyph.advance + num;
				glyphInfo_0.int_0 = glyph.channel;
				glyphInfo_0.bool_0 = false;
				if (float_0 != 1f)
				{
					glyphInfo_0.vector2_0 *= float_0;
					glyphInfo_0.vector2_1 *= float_0;
					glyphInfo_0.float_0 *= float_0;
				}
				return glyphInfo_0;
			}
		}
		else if (font_0 != null && font_0.GetCharacterInfo((char)int_5, out characterInfo_0, int_4, fontStyle_0))
		{
			glyphInfo_0.vector2_0.x = characterInfo_0.vert.xMin;
			glyphInfo_0.vector2_1.x = glyphInfo_0.vector2_0.x + characterInfo_0.vert.width;
			glyphInfo_0.vector2_0.y = characterInfo_0.vert.yMax - float_6;
			glyphInfo_0.vector2_1.y = glyphInfo_0.vector2_0.y - characterInfo_0.vert.height;
			glyphInfo_0.vector2_2.x = characterInfo_0.uv.xMin;
			glyphInfo_0.vector2_2.y = characterInfo_0.uv.yMin;
			glyphInfo_0.vector2_3.x = characterInfo_0.uv.xMax;
			glyphInfo_0.vector2_3.y = characterInfo_0.uv.yMax;
			glyphInfo_0.float_0 = characterInfo_0.width;
			glyphInfo_0.int_0 = 0;
			glyphInfo_0.bool_0 = characterInfo_0.flipped;
			glyphInfo_0.vector2_0.x = Mathf.Round(glyphInfo_0.vector2_0.x);
			glyphInfo_0.vector2_0.y = Mathf.Round(glyphInfo_0.vector2_0.y);
			glyphInfo_0.vector2_1.x = Mathf.Round(glyphInfo_0.vector2_1.x);
			glyphInfo_0.vector2_1.y = Mathf.Round(glyphInfo_0.vector2_1.y);
			float num2 = float_0 * float_1;
			if (num2 != 1f)
			{
				glyphInfo_0.vector2_0 *= num2;
				glyphInfo_0.vector2_1 *= num2;
				glyphInfo_0.float_0 *= num2;
			}
			return glyphInfo_0;
		}
		return null;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static float ParseAlpha(string string_0, int int_5)
	{
		int num = (NGUIMath.HexToDecimal(string_0[int_5 + 1]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 2]);
		return Mathf.Clamp01((float)num / 255f);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor(string string_0, int int_5)
	{
		return ParseColor24(string_0, int_5);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor24(string string_0, int int_5)
	{
		int num = (NGUIMath.HexToDecimal(string_0[int_5]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 1]);
		int num2 = (NGUIMath.HexToDecimal(string_0[int_5 + 2]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 3]);
		int num3 = (NGUIMath.HexToDecimal(string_0[int_5 + 4]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static Color ParseColor32(string string_0, int int_5)
	{
		int num = (NGUIMath.HexToDecimal(string_0[int_5]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 1]);
		int num2 = (NGUIMath.HexToDecimal(string_0[int_5 + 2]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 3]);
		int num3 = (NGUIMath.HexToDecimal(string_0[int_5 + 4]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 5]);
		int num4 = (NGUIMath.HexToDecimal(string_0[int_5 + 6]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string EncodeColor(Color color_4)
	{
		return EncodeColor24(color_4);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeAlpha(float float_9)
	{
		int num = Mathf.Clamp(Mathf.RoundToInt(float_9 * 255f), 0, 255);
		return NGUIMath.DecimalToHex8(num);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor24(Color color_4)
	{
		int num = 0xFFFFFF & (NGUIMath.ColorToInt(color_4) >> 8);
		return NGUIMath.DecimalToHex24(num);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor32(Color color_4)
	{
		int num = NGUIMath.ColorToInt(color_4);
		return NGUIMath.DecimalToHex32(num);
	}

	public static bool ParseSymbol(string string_0, ref int int_5)
	{
		int int_6 = 1;
		bool bool_ = false;
		bool bool_2 = false;
		bool bool_3 = false;
		bool bool_4 = false;
		return ParseSymbol(string_0, ref int_5, null, false, ref int_6, ref bool_, ref bool_2, ref bool_3, ref bool_4);
	}

	public static bool ParseSymbol(string string_0, ref int int_5, BetterList<Color> betterList_2, bool bool_4, ref int int_6, ref bool bool_5, ref bool bool_6, ref bool bool_7, ref bool bool_8)
	{
		int length = string_0.Length;
		if (int_5 + 3 <= length && string_0[int_5] == '[')
		{
			if (string_0[int_5 + 2] == ']')
			{
				if (string_0[int_5 + 1] == '-')
				{
					if (betterList_2 != null && betterList_2.size > 1)
					{
						betterList_2.RemoveAt(betterList_2.size - 1);
					}
					int_5 += 3;
					return true;
				}
				switch (string_0.Substring(int_5, 3))
				{
				case "[b]":
					bool_5 = true;
					int_5 += 3;
					return true;
				case "[i]":
					bool_6 = true;
					int_5 += 3;
					return true;
				case "[u]":
					bool_7 = true;
					int_5 += 3;
					return true;
				case "[s]":
					bool_8 = true;
					int_5 += 3;
					return true;
				}
			}
			if (int_5 + 4 > length)
			{
				return false;
			}
			if (string_0[int_5 + 3] == ']')
			{
				switch (string_0.Substring(int_5, 4))
				{
				case "[/b]":
					bool_5 = false;
					int_5 += 4;
					return true;
				case "[/i]":
					bool_6 = false;
					int_5 += 4;
					return true;
				case "[/u]":
					bool_7 = false;
					int_5 += 4;
					return true;
				case "[/s]":
					bool_8 = false;
					int_5 += 4;
					return true;
				default:
				{
					int num = (NGUIMath.HexToDecimal(string_0[int_5 + 1]) << 4) | NGUIMath.HexToDecimal(string_0[int_5 + 2]);
					float_7 = (float)num / 255f;
					int_5 += 4;
					return true;
				}
				}
			}
			if (int_5 + 5 > length)
			{
				return false;
			}
			if (string_0[int_5 + 4] == ']')
			{
				switch (string_0.Substring(int_5, 5))
				{
				case "[sup]":
					int_6 = 2;
					int_5 += 5;
					return true;
				case "[sub]":
					int_6 = 1;
					int_5 += 5;
					return true;
				}
			}
			if (int_5 + 6 > length)
			{
				return false;
			}
			if (string_0[int_5 + 5] == ']')
			{
				switch (string_0.Substring(int_5, 6))
				{
				case "[/sub]":
					int_6 = 0;
					int_5 += 6;
					return true;
				case "[/sup]":
					int_6 = 0;
					int_5 += 6;
					return true;
				case "[/url]":
					int_5 += 6;
					return true;
				}
			}
			if (string_0[int_5 + 1] == 'u' && string_0[int_5 + 2] == 'r' && string_0[int_5 + 3] == 'l' && string_0[int_5 + 4] == '=')
			{
				int num2 = string_0.IndexOf(']', int_5 + 4);
				if (num2 != -1)
				{
					int_5 = num2 + 1;
					return true;
				}
				int_5 = string_0.Length;
				return true;
			}
			if (int_5 + 8 > length)
			{
				return false;
			}
			if (string_0[int_5 + 7] == ']')
			{
				Color color = ParseColor24(string_0, int_5 + 1);
				if (EncodeColor24(color) != string_0.Substring(int_5 + 1, 6).ToUpper())
				{
					return false;
				}
				if (betterList_2 != null)
				{
					color.a = betterList_2[betterList_2.size - 1].a;
					if (bool_4 && color.a != 1f)
					{
						color = Color.Lerp(color_3, color, color.a);
					}
					betterList_2.Add(color);
				}
				int_5 += 8;
				return true;
			}
			if (int_5 + 10 > length)
			{
				return false;
			}
			if (string_0[int_5 + 9] == ']')
			{
				Color color2 = ParseColor32(string_0, int_5 + 1);
				if (EncodeColor32(color2) != string_0.Substring(int_5 + 1, 8).ToUpper())
				{
					return false;
				}
				if (betterList_2 != null)
				{
					if (bool_4 && color2.a != 1f)
					{
						color2 = Color.Lerp(color_3, color2, color2.a);
					}
					betterList_2.Add(color2);
				}
				int_5 += 10;
				return true;
			}
			return false;
		}
		return false;
	}

	public static string StripSymbols(string string_0)
	{
		if (string_0 != null)
		{
			int num = 0;
			int length = string_0.Length;
			while (num < length)
			{
				char c = string_0[num];
				if (c == '[')
				{
					int int_ = 0;
					bool bool_ = false;
					bool bool_2 = false;
					bool bool_3 = false;
					bool bool_4 = false;
					int int_2 = num;
					if (ParseSymbol(string_0, ref int_2, null, false, ref int_, ref bool_, ref bool_2, ref bool_3, ref bool_4))
					{
						string_0 = string_0.Remove(num, int_2 - num);
						length = string_0.Length;
						continue;
					}
				}
				num++;
			}
		}
		return string_0;
	}

	public static void Align(BetterList<Vector3> betterList_2, int int_5, float float_9)
	{
		switch (alignment_0)
		{
		case Alignment.Center:
		{
			float num11 = ((float)int_1 - float_9) * 0.5f;
			if (!(num11 < 0f))
			{
				int num12 = Mathf.RoundToInt((float)int_1 - float_9);
				int num13 = Mathf.RoundToInt(int_1);
				bool flag = (num12 & 1) == 1;
				bool flag2 = (num13 & 1) == 1;
				if ((flag && !flag2) || (!flag && flag2))
				{
					num11 += 0.5f * float_0;
				}
				for (int j = int_5; j < betterList_2.size; j++)
				{
					betterList_2.buffer[j].x += num11;
				}
			}
			break;
		}
		case Alignment.Right:
		{
			float num10 = (float)int_1 - float_9;
			if (!(num10 < 0f))
			{
				for (int i = int_5; i < betterList_2.size; i++)
				{
					betterList_2.buffer[i].x += num10;
				}
			}
			break;
		}
		case Alignment.Justified:
		{
			if (float_9 < (float)int_1 * 0.65f)
			{
				break;
			}
			float num = ((float)int_1 - float_9) * 0.5f;
			if (num < 1f)
			{
				break;
			}
			int num2 = (betterList_2.size - int_5) / 4;
			if (num2 >= 1)
			{
				float num3 = 1f / (float)(num2 - 1);
				float num4 = (float)int_1 / float_9;
				int num5 = int_5 + 4;
				int num6 = 1;
				while (num5 < betterList_2.size)
				{
					float x = betterList_2.buffer[num5].x;
					float x2 = betterList_2.buffer[num5 + 2].x;
					float num7 = x2 - x;
					float num8 = x * num4;
					float from = num8 + num7;
					float num9 = x2 * num4;
					float to = num9 - num7;
					float t = (float)num6 * num3;
					x = Mathf.Lerp(num8, to, t);
					x2 = Mathf.Lerp(from, num9, t);
					x = Mathf.Round(x);
					x2 = Mathf.Round(x2);
					betterList_2.buffer[num5++].x = x;
					betterList_2.buffer[num5++].x = x;
					betterList_2.buffer[num5++].x = x2;
					betterList_2.buffer[num5++].x = x2;
					num6++;
				}
			}
			break;
		}
		}
	}

	public static int GetClosestCharacter(BetterList<Vector3> betterList_2, Vector2 vector2_0)
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		int result = 0;
		for (int i = 0; i < betterList_2.size; i++)
		{
			float num3 = Mathf.Abs(vector2_0.y - betterList_2[i].y);
			if (num3 <= num2)
			{
				float num4 = Mathf.Abs(vector2_0.x - betterList_2[i].x);
				if (num3 < num2)
				{
					num2 = num3;
					num = num4;
					result = i;
				}
				else if (num4 < num)
				{
					num = num4;
					result = i;
				}
			}
		}
		return result;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	private static bool IsSpace(int int_5)
	{
		return int_5 == 32 || int_5 == 8202 || int_5 == 8203;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static void EndLine(ref StringBuilder stringBuilder_0)
	{
		int num = stringBuilder_0.Length - 1;
		if (num > 0 && IsSpace(stringBuilder_0[num]))
		{
			stringBuilder_0[num] = '\n';
		}
		else
		{
			stringBuilder_0.Append('\n');
		}
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void ReplaceSpaceWithNewline(ref StringBuilder stringBuilder_0)
	{
		int num = stringBuilder_0.Length - 1;
		if (num > 0 && IsSpace(stringBuilder_0[num]))
		{
			stringBuilder_0[num] = '\n';
		}
	}

	public static Vector2 CalculatePrintedSize(string string_0)
	{
		Vector2 zero = Vector2.zero;
		if (!string.IsNullOrEmpty(string_0))
		{
			if (bool_1)
			{
				string_0 = StripSymbols(string_0);
			}
			Prepare(string_0);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = string_0.Length;
			int num4 = 0;
			int int_ = 0;
			for (int i = 0; i < length; i++)
			{
				num4 = string_0[i];
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += float_5;
				}
				else
				{
					if (num4 < 32)
					{
						continue;
					}
					BMSymbol bMSymbol = ((!bool_3) ? null : GetSymbol(string_0, i, length));
					if (bMSymbol == null)
					{
						float glyphWidth = GetGlyphWidth(num4, int_);
						if (glyphWidth == 0f)
						{
							continue;
						}
						glyphWidth += float_4;
						if (Mathf.RoundToInt(num + glyphWidth) > int_1)
						{
							if (num > num3)
							{
								num3 = num - float_4;
							}
							num = glyphWidth;
							num2 += float_5;
						}
						else
						{
							num += glyphWidth;
						}
						int_ = num4;
						continue;
					}
					float num5 = float_4 + (float)bMSymbol.Int32_5 * float_0;
					if (Mathf.RoundToInt(num + num5) > int_1)
					{
						if (num > num3)
						{
							num3 = num - float_4;
						}
						num = num5;
						num2 += float_5;
					}
					else
					{
						num += num5;
					}
					i += bMSymbol.sequence.Length - 1;
					int_ = 0;
				}
			}
			zero.x = ((!(num > num3)) ? num3 : (num - float_4));
			zero.y = num2 + float_5;
		}
		return zero;
	}

	public static int CalculateOffsetToFit(string string_0)
	{
		if (!string.IsNullOrEmpty(string_0) && int_1 >= 1)
		{
			Prepare(string_0);
			int length = string_0.Length;
			int num = 0;
			int int_ = 0;
			int i = 0;
			for (int length2 = string_0.Length; i < length2; i++)
			{
				BMSymbol bMSymbol = ((!bool_3) ? null : GetSymbol(string_0, i, length));
				if (bMSymbol == null)
				{
					num = string_0[i];
					float glyphWidth = GetGlyphWidth(num, int_);
					if (glyphWidth != 0f)
					{
						betterList_1.Add(float_4 + glyphWidth);
					}
					int_ = num;
					continue;
				}
				betterList_1.Add(float_4 + (float)bMSymbol.Int32_5 * float_0);
				int j = 0;
				for (int num2 = bMSymbol.sequence.Length - 1; j < num2; j++)
				{
					betterList_1.Add(0f);
				}
				i += bMSymbol.sequence.Length - 1;
				int_ = 0;
			}
			float num3 = int_1;
			int num4 = betterList_1.size;
			while (num4 > 0 && !(num3 <= 0f))
			{
				num3 -= betterList_1[--num4];
			}
			betterList_1.Clear();
			if (num3 < 0f)
			{
				num4++;
			}
			return num4;
		}
		return 0;
	}

	public static string GetEndOfLineThatFits(string string_0)
	{
		int length = string_0.Length;
		int num = CalculateOffsetToFit(string_0);
		return string_0.Substring(num, length - num);
	}

	public static bool WrapText(string string_0, out string string_1)
	{
		return WrapText(string_0, out string_1, false);
	}

	public static bool WrapText(string string_0, out string string_1, bool bool_4)
	{
		if (int_1 >= 1 && int_2 >= 1 && float_5 >= 1f)
		{
			float num = ((int_3 <= 0) ? ((float)int_2) : Mathf.Min(int_2, float_5 * (float)int_3));
			int num2 = ((int_3 <= 0) ? 1000000 : int_3);
			num2 = Mathf.FloorToInt(Mathf.Min(num2, num / float_5) + 0.01f);
			if (num2 == 0)
			{
				string_1 = string.Empty;
				return false;
			}
			if (string.IsNullOrEmpty(string_0))
			{
				string_0 = " ";
			}
			Prepare(string_0);
			StringBuilder stringBuilder_ = new StringBuilder();
			int length = string_0.Length;
			float num3 = int_1;
			int num4 = 0;
			int i = 0;
			int num5 = 1;
			int int_ = 0;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = false;
			for (; i < length; i++)
			{
				char c = string_0[i];
				if (c > '\u2fff')
				{
					flag3 = true;
				}
				if (c == '\n')
				{
					if (num5 == num2)
					{
						break;
					}
					num3 = int_1;
					if (num4 < i)
					{
						stringBuilder_.Append(string_0.Substring(num4, i - num4 + 1));
					}
					else
					{
						stringBuilder_.Append(c);
					}
					flag = true;
					num5++;
					num4 = i + 1;
					int_ = 0;
					continue;
				}
				if (bool_1 && ParseSymbol(string_0, ref i))
				{
					i--;
					continue;
				}
				BMSymbol bMSymbol = ((!bool_3) ? null : GetSymbol(string_0, i, length));
				float num6;
				if (bMSymbol == null)
				{
					float glyphWidth = GetGlyphWidth(c, int_);
					if (glyphWidth == 0f)
					{
						continue;
					}
					num6 = float_4 + glyphWidth;
				}
				else
				{
					num6 = float_4 + (float)bMSymbol.Int32_5 * float_0;
				}
				num3 -= num6;
				if (IsSpace(c) && !flag3 && num4 < i)
				{
					int num7 = i - num4 + 1;
					if (num5 == num2 && num3 <= 0f && i < length)
					{
						char c2 = string_0[i];
						if (c2 < ' ' || IsSpace(c2))
						{
							num7--;
						}
					}
					stringBuilder_.Append(string_0.Substring(num4, num7));
					flag = false;
					num4 = i + 1;
					int_ = c;
				}
				if (Mathf.RoundToInt(num3) < 0)
				{
					if (!flag && num5 != num2)
					{
						flag = true;
						num3 = int_1;
						i = num4 - 1;
						int_ = 0;
						if (num5++ == num2)
						{
							break;
						}
						if (bool_4)
						{
							ReplaceSpaceWithNewline(ref stringBuilder_);
						}
						else
						{
							EndLine(ref stringBuilder_);
						}
						continue;
					}
					stringBuilder_.Append(string_0.Substring(num4, Mathf.Max(0, i - num4)));
					bool flag4;
					if (!(flag4 = IsSpace(c)) && !flag3)
					{
						flag2 = false;
					}
					if (num5++ == num2)
					{
						num4 = i;
						break;
					}
					if (bool_4)
					{
						ReplaceSpaceWithNewline(ref stringBuilder_);
					}
					else
					{
						EndLine(ref stringBuilder_);
					}
					flag = true;
					if (flag4)
					{
						num4 = i + 1;
						num3 = int_1;
					}
					else
					{
						num4 = i;
						num3 = (float)int_1 - num6;
					}
					int_ = 0;
				}
				else
				{
					int_ = c;
				}
				if (bMSymbol != null)
				{
					i += bMSymbol.Int32_0 - 1;
					int_ = 0;
				}
			}
			if (num4 < i)
			{
				stringBuilder_.Append(string_0.Substring(num4, i - num4));
			}
			string_1 = stringBuilder_.ToString();
			return flag2 && (i == length || num5 <= Mathf.Min(int_3, num2));
		}
		string_1 = string.Empty;
		return false;
	}

	public static void Print(string string_0, BetterList<Vector3> betterList_2, BetterList<Vector2> betterList_3, BetterList<Color32> betterList_4)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			return;
		}
		int size = betterList_2.size;
		Prepare(string_0);
		betterList_0.Add(Color.white);
		float_7 = 1f;
		int num = 0;
		int int_ = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = int_4;
		Color a = color_0 * color_1;
		Color b = color_0 * color_2;
		Color32 color = color_0;
		int length = string_0.Length;
		Rect rect = default(Rect);
		float num6 = 0f;
		float num7 = 0f;
		float num8 = num5 * float_1;
		bool flag = false;
		int int_2 = 0;
		bool bool_ = false;
		bool bool_2 = false;
		bool bool_3 = false;
		bool bool_4 = false;
		float num9 = 0f;
		if (uifont_0 != null)
		{
			rect = uifont_0.Rect_0;
			num6 = rect.width / (float)uifont_0.Int32_0;
			num7 = rect.height / (float)uifont_0.Int32_1;
		}
		for (int i = 0; i < length; i++)
		{
			num = string_0[i];
			num9 = num2;
			if (num == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (alignment_0 != Alignment.Left)
				{
					Align(betterList_2, size, num2 - float_4);
					size = betterList_2.size;
				}
				num2 = 0f;
				num3 += float_5;
				int_ = 0;
				continue;
			}
			if (num < 32)
			{
				int_ = num;
				continue;
			}
			if (bool_1 && ParseSymbol(string_0, ref i, betterList_0, NGUIText.bool_2, ref int_2, ref bool_, ref bool_2, ref bool_3, ref bool_4))
			{
				Color color2 = color_0 * betterList_0[betterList_0.size - 1];
				color2.a *= float_7;
				color = color2;
				int j = 0;
				for (int num10 = betterList_0.size - 2; j < num10; j++)
				{
					color2.a *= betterList_0[j].a;
				}
				if (bool_0)
				{
					a = color_1 * color2;
					b = color_2 * color2;
				}
				i--;
				continue;
			}
			BMSymbol bMSymbol = ((!NGUIText.bool_3) ? null : GetSymbol(string_0, i, length));
			float num11;
			float num12;
			float num14;
			float num13;
			if (bMSymbol != null)
			{
				num11 = num2 + (float)bMSymbol.Int32_1 * float_0;
				num12 = num11 + (float)bMSymbol.Int32_3 * float_0;
				num13 = 0f - (num3 + (float)bMSymbol.Int32_2 * float_0);
				num14 = num13 - (float)bMSymbol.Int32_4 * float_0;
				if (Mathf.RoundToInt(num2 + (float)bMSymbol.Int32_5 * float_0) > int_1)
				{
					if (num2 == 0f)
					{
						return;
					}
					if (alignment_0 != Alignment.Left && size < betterList_2.size)
					{
						Align(betterList_2, size, num2 - float_4);
						size = betterList_2.size;
					}
					num11 -= num2;
					num12 -= num2;
					num14 -= float_5;
					num13 -= float_5;
					num2 = 0f;
					num3 += float_5;
					num9 = 0f;
				}
				betterList_2.Add(new Vector3(num11, num14));
				betterList_2.Add(new Vector3(num11, num13));
				betterList_2.Add(new Vector3(num12, num13));
				betterList_2.Add(new Vector3(num12, num14));
				num2 += float_4 + (float)bMSymbol.Int32_5 * float_0;
				i += bMSymbol.Int32_0 - 1;
				int_ = 0;
				if (betterList_3 != null)
				{
					Rect rect_ = bMSymbol.Rect_0;
					float xMin = rect_.xMin;
					float yMin = rect_.yMin;
					float xMax = rect_.xMax;
					float yMax = rect_.yMax;
					betterList_3.Add(new Vector2(xMin, yMin));
					betterList_3.Add(new Vector2(xMin, yMax));
					betterList_3.Add(new Vector2(xMax, yMax));
					betterList_3.Add(new Vector2(xMax, yMin));
				}
				if (betterList_4 == null)
				{
					continue;
				}
				if (symbolStyle_0 == SymbolStyle.Colored)
				{
					for (int k = 0; k < 4; k++)
					{
						betterList_4.Add(color);
					}
					continue;
				}
				Color32 gparam_ = Color.white;
				gparam_.a = color.a;
				for (int l = 0; l < 4; l++)
				{
					betterList_4.Add(gparam_);
				}
				continue;
			}
			GlyphInfo glyph = GetGlyph(num, int_);
			if (glyph == null)
			{
				continue;
			}
			int_ = num;
			if (int_2 != 0)
			{
				glyph.vector2_0.x *= 0.75f;
				glyph.vector2_0.y *= 0.75f;
				glyph.vector2_1.x *= 0.75f;
				glyph.vector2_1.y *= 0.75f;
				if (int_2 == 1)
				{
					glyph.vector2_0.y -= float_0 * (float)int_0 * 0.4f;
					glyph.vector2_1.y -= float_0 * (float)int_0 * 0.4f;
				}
				else
				{
					glyph.vector2_0.y += float_0 * (float)int_0 * 0.05f;
					glyph.vector2_1.y += float_0 * (float)int_0 * 0.05f;
				}
			}
			num11 = glyph.vector2_0.x + num2;
			num14 = glyph.vector2_0.y - num3;
			num12 = glyph.vector2_1.x + num2;
			num13 = glyph.vector2_1.y - num3;
			float num15 = glyph.float_0;
			if (float_4 < 0f)
			{
				num15 += float_4;
			}
			if (Mathf.RoundToInt(num2 + num15) > int_1)
			{
				if (num2 == 0f)
				{
					return;
				}
				if (alignment_0 != Alignment.Left && size < betterList_2.size)
				{
					Align(betterList_2, size, num2 - float_4);
					size = betterList_2.size;
				}
				num11 -= num2;
				num12 -= num2;
				num14 -= float_5;
				num13 -= float_5;
				num2 = 0f;
				num3 += float_5;
				num9 = 0f;
			}
			if (IsSpace(num))
			{
				if (bool_3)
				{
					num = 95;
				}
				else if (bool_4)
				{
					num = 45;
				}
			}
			num2 += ((int_2 != 0) ? ((float_4 + glyph.float_0) * 0.75f) : (float_4 + glyph.float_0));
			if (IsSpace(num))
			{
				continue;
			}
			if (betterList_3 != null)
			{
				if (uifont_0 != null)
				{
					glyph.vector2_2.x = rect.xMin + num6 * glyph.vector2_2.x;
					glyph.vector2_3.x = rect.xMin + num6 * glyph.vector2_3.x;
					glyph.vector2_2.y = rect.yMax - num7 * glyph.vector2_2.y;
					glyph.vector2_3.y = rect.yMax - num7 * glyph.vector2_3.y;
				}
				int m = 0;
				for (int num16 = ((!bool_) ? 1 : 4); m < num16; m++)
				{
					if (glyph.bool_0)
					{
						betterList_3.Add(glyph.vector2_2);
						betterList_3.Add(new Vector2(glyph.vector2_3.x, glyph.vector2_2.y));
						betterList_3.Add(glyph.vector2_3);
						betterList_3.Add(new Vector2(glyph.vector2_2.x, glyph.vector2_3.y));
					}
					else
					{
						betterList_3.Add(glyph.vector2_2);
						betterList_3.Add(new Vector2(glyph.vector2_2.x, glyph.vector2_3.y));
						betterList_3.Add(glyph.vector2_3);
						betterList_3.Add(new Vector2(glyph.vector2_3.x, glyph.vector2_2.y));
					}
				}
			}
			if (betterList_4 != null)
			{
				if (glyph.int_0 != 0 && glyph.int_0 != 15)
				{
					Color color3 = color;
					color3 *= 0.49f;
					switch (glyph.int_0)
					{
					case 1:
						color3.b += 0.51f;
						break;
					case 2:
						color3.g += 0.51f;
						break;
					case 4:
						color3.r += 0.51f;
						break;
					case 8:
						color3.a += 0.51f;
						break;
					}
					Color32 gparam_2 = color3;
					int n = 0;
					for (int num17 = ((!bool_) ? 4 : 16); n < num17; n++)
					{
						betterList_4.Add(gparam_2);
					}
				}
				else if (bool_0)
				{
					float num18 = num8 + glyph.vector2_0.y / float_0;
					float num19 = num8 + glyph.vector2_1.y / float_0;
					num18 /= num8;
					num19 /= num8;
					color32_0 = Color.Lerp(a, b, num18);
					color32_1 = Color.Lerp(a, b, num19);
					int num20 = 0;
					for (int num21 = ((!bool_) ? 1 : 4); num20 < num21; num20++)
					{
						betterList_4.Add(color32_0);
						betterList_4.Add(color32_1);
						betterList_4.Add(color32_1);
						betterList_4.Add(color32_0);
					}
				}
				else
				{
					int num22 = 0;
					for (int num23 = ((!bool_) ? 4 : 16); num22 < num23; num22++)
					{
						betterList_4.Add(color);
					}
				}
			}
			if (!bool_)
			{
				if (!bool_2)
				{
					betterList_2.Add(new Vector3(num11, num14));
					betterList_2.Add(new Vector3(num11, num13));
					betterList_2.Add(new Vector3(num12, num13));
					betterList_2.Add(new Vector3(num12, num14));
				}
				else
				{
					float num24 = (float)int_0 * 0.1f * ((num13 - num14) / (float)int_0);
					betterList_2.Add(new Vector3(num11 - num24, num14));
					betterList_2.Add(new Vector3(num11 + num24, num13));
					betterList_2.Add(new Vector3(num12 + num24, num13));
					betterList_2.Add(new Vector3(num12 - num24, num14));
				}
			}
			else
			{
				for (int num25 = 0; num25 < 4; num25++)
				{
					float num26 = float_8[num25 * 2];
					float num27 = float_8[num25 * 2 + 1];
					float num28 = num26 + ((!bool_2) ? 0f : ((float)int_0 * 0.1f * ((num13 - num14) / (float)int_0)));
					betterList_2.Add(new Vector3(num11 - num28, num14 + num27));
					betterList_2.Add(new Vector3(num11 + num28, num13 + num27));
					betterList_2.Add(new Vector3(num12 + num28, num13 + num27));
					betterList_2.Add(new Vector3(num12 - num28, num14 + num27));
				}
			}
			if (!bool_3 && !bool_4)
			{
				continue;
			}
			GlyphInfo glyph2 = GetGlyph((!bool_4) ? 95 : 45, int_);
			if (glyph2 == null)
			{
				continue;
			}
			if (betterList_3 != null)
			{
				if (uifont_0 != null)
				{
					glyph2.vector2_2.x = rect.xMin + num6 * glyph2.vector2_2.x;
					glyph2.vector2_3.x = rect.xMin + num6 * glyph2.vector2_3.x;
					glyph2.vector2_2.y = rect.yMax - num7 * glyph2.vector2_2.y;
					glyph2.vector2_3.y = rect.yMax - num7 * glyph2.vector2_3.y;
				}
				float x = (glyph2.vector2_2.x + glyph2.vector2_3.x) * 0.5f;
				betterList_3.Add(new Vector2(x, glyph2.vector2_2.y));
				betterList_3.Add(new Vector2(x, glyph2.vector2_3.y));
				betterList_3.Add(new Vector2(x, glyph2.vector2_3.y));
				betterList_3.Add(new Vector2(x, glyph2.vector2_2.y));
			}
			if (flag && bool_4)
			{
				num14 = (0f - num3 + glyph2.vector2_0.y) * 0.75f;
				num13 = (0f - num3 + glyph2.vector2_1.y) * 0.75f;
			}
			else
			{
				num14 = 0f - num3 + glyph2.vector2_0.y;
				num13 = 0f - num3 + glyph2.vector2_1.y;
			}
			betterList_2.Add(new Vector3(num9, num14));
			betterList_2.Add(new Vector3(num9, num13));
			betterList_2.Add(new Vector3(num2, num13));
			betterList_2.Add(new Vector3(num2, num14));
			if (bool_0)
			{
				float num29 = num8 + glyph2.vector2_0.y / float_0;
				float num30 = num8 + glyph2.vector2_1.y / float_0;
				num29 /= num8;
				num30 /= num8;
				color32_0 = Color.Lerp(a, b, num29);
				color32_1 = Color.Lerp(a, b, num30);
				int num31 = 0;
				for (int num32 = ((!bool_) ? 1 : 4); num31 < num32; num31++)
				{
					betterList_4.Add(color32_0);
					betterList_4.Add(color32_1);
					betterList_4.Add(color32_1);
					betterList_4.Add(color32_0);
				}
			}
			else
			{
				int num33 = 0;
				for (int num34 = ((!bool_) ? 4 : 16); num33 < num34; num33++)
				{
					betterList_4.Add(color);
				}
			}
		}
		if (alignment_0 != Alignment.Left && size < betterList_2.size)
		{
			Align(betterList_2, size, num2 - float_4);
			size = betterList_2.size;
		}
		betterList_0.Clear();
	}

	public static void PrintCharacterPositions(string string_0, BetterList<Vector3> betterList_2, BetterList<int> betterList_3)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			string_0 = " ";
		}
		Prepare(string_0);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)int_0 * float_0 * 0.5f;
		int length = string_0.Length;
		int size = betterList_2.size;
		int num5 = 0;
		int int_ = 0;
		for (int i = 0; i < length; i++)
		{
			num5 = string_0[i];
			betterList_2.Add(new Vector3(num, 0f - num2 - num4));
			betterList_3.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (alignment_0 != Alignment.Left)
				{
					Align(betterList_2, size, num - float_4);
					size = betterList_2.size;
				}
				num = 0f;
				num2 += float_5;
				int_ = 0;
				continue;
			}
			if (num5 < 32)
			{
				int_ = 0;
				continue;
			}
			if (bool_1 && ParseSymbol(string_0, ref i))
			{
				i--;
				continue;
			}
			BMSymbol bMSymbol = ((!bool_3) ? null : GetSymbol(string_0, i, length));
			if (bMSymbol == null)
			{
				float glyphWidth = GetGlyphWidth(num5, int_);
				if (glyphWidth == 0f)
				{
					continue;
				}
				glyphWidth += float_4;
				if (Mathf.RoundToInt(num + glyphWidth) > int_1)
				{
					if (num == 0f)
					{
						return;
					}
					if (alignment_0 != Alignment.Left && size < betterList_2.size)
					{
						Align(betterList_2, size, num - float_4);
						size = betterList_2.size;
					}
					num = glyphWidth;
					num2 += float_5;
				}
				else
				{
					num += glyphWidth;
				}
				betterList_2.Add(new Vector3(num, 0f - num2 - num4));
				betterList_3.Add(i + 1);
				int_ = num5;
				continue;
			}
			float num6 = (float)bMSymbol.Int32_5 * float_0 + float_4;
			if (Mathf.RoundToInt(num + num6) > int_1)
			{
				if (num == 0f)
				{
					return;
				}
				if (alignment_0 != Alignment.Left && size < betterList_2.size)
				{
					Align(betterList_2, size, num - float_4);
					size = betterList_2.size;
				}
				num = num6;
				num2 += float_5;
			}
			else
			{
				num += num6;
			}
			betterList_2.Add(new Vector3(num, 0f - num2 - num4));
			betterList_3.Add(i + 1);
			i += bMSymbol.sequence.Length - 1;
			int_ = 0;
		}
		if (alignment_0 != Alignment.Left && size < betterList_2.size)
		{
			Align(betterList_2, size, num - float_4);
		}
	}

	public static void PrintCaretAndSelection(string string_0, int int_5, int int_6, BetterList<Vector3> betterList_2, BetterList<Vector3> betterList_3)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			string_0 = " ";
		}
		Prepare(string_0);
		int num = int_6;
		if (int_5 > int_6)
		{
			int_6 = int_5;
			int_5 = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)int_0 * float_0;
		int int_7 = ((betterList_2 != null) ? betterList_2.size : 0);
		int num6 = ((betterList_3 != null) ? betterList_3.size : 0);
		int length = string_0.Length;
		int i = 0;
		int num7 = 0;
		int int_8 = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		for (; i < length; i++)
		{
			if (betterList_2 != null && !flag2 && num <= i)
			{
				flag2 = true;
				betterList_2.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				betterList_2.Add(new Vector3(num2 - 1f, 0f - num3));
				betterList_2.Add(new Vector3(num2 + 1f, 0f - num3));
				betterList_2.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			num7 = string_0[i];
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (betterList_2 != null && flag2)
				{
					if (alignment_0 != Alignment.Left)
					{
						Align(betterList_2, int_7, num2 - float_4);
					}
					betterList_2 = null;
				}
				if (betterList_3 != null)
				{
					if (flag)
					{
						flag = false;
						betterList_3.Add(vector2);
						betterList_3.Add(vector);
					}
					else if (int_5 <= i && int_6 > i)
					{
						betterList_3.Add(new Vector3(num2, 0f - num3 - num5));
						betterList_3.Add(new Vector3(num2, 0f - num3));
						betterList_3.Add(new Vector3(num2 + 2f, 0f - num3));
						betterList_3.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
					}
					if (alignment_0 != Alignment.Left && num6 < betterList_3.size)
					{
						Align(betterList_3, num6, num2 - float_4);
						num6 = betterList_3.size;
					}
				}
				num2 = 0f;
				num3 += float_5;
				int_8 = 0;
				continue;
			}
			if (num7 < 32)
			{
				int_8 = 0;
				continue;
			}
			if (bool_1 && ParseSymbol(string_0, ref i))
			{
				i--;
				continue;
			}
			BMSymbol bMSymbol = ((!bool_3) ? null : GetSymbol(string_0, i, length));
			float num8 = ((bMSymbol == null) ? GetGlyphWidth(num7, int_8) : ((float)bMSymbol.Int32_5 * float_0));
			if (num8 == 0f)
			{
				continue;
			}
			float num9 = num2;
			float num10 = num2 + num8;
			float num11 = 0f - num3 - num5;
			float num12 = 0f - num3;
			if (Mathf.RoundToInt(num10 + float_4) > int_1)
			{
				if (num2 == 0f)
				{
					return;
				}
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (betterList_2 != null && flag2)
				{
					if (alignment_0 != Alignment.Left)
					{
						Align(betterList_2, int_7, num2 - float_4);
					}
					betterList_2 = null;
				}
				if (betterList_3 != null)
				{
					if (flag)
					{
						flag = false;
						betterList_3.Add(vector2);
						betterList_3.Add(vector);
					}
					else if (int_5 <= i && int_6 > i)
					{
						betterList_3.Add(new Vector3(num2, 0f - num3 - num5));
						betterList_3.Add(new Vector3(num2, 0f - num3));
						betterList_3.Add(new Vector3(num2 + 2f, 0f - num3));
						betterList_3.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
					}
					if (alignment_0 != Alignment.Left && num6 < betterList_3.size)
					{
						Align(betterList_3, num6, num2 - float_4);
						num6 = betterList_3.size;
					}
				}
				num9 -= num2;
				num10 -= num2;
				num11 -= float_5;
				num12 -= float_5;
				num2 = 0f;
				num3 += float_5;
			}
			num2 += num8 + float_4;
			if (betterList_3 != null)
			{
				if (int_5 <= i && int_6 > i)
				{
					if (!flag)
					{
						flag = true;
						betterList_3.Add(new Vector3(num9, num11));
						betterList_3.Add(new Vector3(num9, num12));
					}
				}
				else if (flag)
				{
					flag = false;
					betterList_3.Add(vector2);
					betterList_3.Add(vector);
				}
			}
			vector = new Vector2(num10, num11);
			vector2 = new Vector2(num10, num12);
			int_8 = num7;
		}
		if (betterList_2 != null)
		{
			if (!flag2)
			{
				betterList_2.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				betterList_2.Add(new Vector3(num2 - 1f, 0f - num3));
				betterList_2.Add(new Vector3(num2 + 1f, 0f - num3));
				betterList_2.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			if (alignment_0 != Alignment.Left)
			{
				Align(betterList_2, int_7, num2 - float_4);
			}
		}
		if (betterList_3 != null)
		{
			if (flag)
			{
				betterList_3.Add(vector2);
				betterList_3.Add(vector);
			}
			else if (int_5 < i && int_6 == i)
			{
				betterList_3.Add(new Vector3(num2, 0f - num3 - num5));
				betterList_3.Add(new Vector3(num2, 0f - num3));
				betterList_3.Add(new Vector3(num2 + 2f, 0f - num3));
				betterList_3.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
			}
			if (alignment_0 != Alignment.Left && num6 < betterList_3.size)
			{
				Align(betterList_3, num6, num2 - float_4);
			}
		}
	}
}
