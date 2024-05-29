using System.Diagnostics;
using UnityEngine;

public static class NGUIMath
{
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float Lerp(float float_0, float float_1, float float_2)
	{
		return float_0 * (1f - float_2) + float_1 * float_2;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int ClampIndex(int int_0, int int_1)
	{
		return (int_0 >= 0) ? ((int_0 >= int_1) ? (int_1 - 1) : int_0) : 0;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static int RepeatIndex(int int_0, int int_1)
	{
		if (int_1 < 1)
		{
			return 0;
		}
		while (int_0 < 0)
		{
			int_0 += int_1;
		}
		while (int_0 >= int_1)
		{
			int_0 -= int_1;
		}
		return int_0;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float WrapAngle(float float_0)
	{
		while (float_0 > 180f)
		{
			float_0 -= 360f;
		}
		while (float_0 < -180f)
		{
			float_0 += 360f;
		}
		return float_0;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float Wrap01(float float_0)
	{
		return float_0 - (float)Mathf.FloorToInt(float_0);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static int HexToDecimal(char char_0)
	{
		switch (char_0)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		default:
			return 15;
		case 'A':
		case 'a':
			return 10;
		case 'B':
		case 'b':
			return 11;
		case 'C':
		case 'c':
			return 12;
		case 'D':
		case 'd':
			return 13;
		case 'E':
		case 'e':
			return 14;
		case 'F':
		case 'f':
			return 15;
		}
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static char DecimalToHexChar(int int_0)
	{
		if (int_0 > 15)
		{
			return 'F';
		}
		if (int_0 < 10)
		{
			return (char)(48 + int_0);
		}
		return (char)(65 + int_0 - 10);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string DecimalToHex8(int int_0)
	{
		int_0 &= 0xFF;
		return int_0.ToString("X2");
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string DecimalToHex24(int int_0)
	{
		int_0 &= 0xFFFFFF;
		return int_0.ToString("X6");
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string DecimalToHex32(int int_0)
	{
		return int_0.ToString("X8");
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static int ColorToInt(Color color_0)
	{
		int num = 0;
		num = 0 | (Mathf.RoundToInt(color_0.r * 255f) << 24);
		num |= Mathf.RoundToInt(color_0.g * 255f) << 16;
		num |= Mathf.RoundToInt(color_0.b * 255f) << 8;
		return num | Mathf.RoundToInt(color_0.a * 255f);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color IntToColor(int int_0)
	{
		float num = 0.003921569f;
		Color black = Color.black;
		black.r = num * (float)((int_0 >> 24) & 0xFF);
		black.g = num * (float)((int_0 >> 16) & 0xFF);
		black.b = num * (float)((int_0 >> 8) & 0xFF);
		black.a = num * (float)(int_0 & 0xFF);
		return black;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string IntToBinary(int int_0, int int_1)
	{
		string text = string.Empty;
		int num = int_1;
		while (num > 0)
		{
			if (num == 8 || num == 16 || num == 24)
			{
				text += " ";
			}
			text += (((int_0 & (1 << --num)) == 0) ? '0' : '1');
		}
		return text;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static Color HexToColor(uint uint_0)
	{
		return IntToColor((int)uint_0);
	}

	public static Rect ConvertToTexCoords(Rect rect_0, int int_0, int int_1)
	{
		Rect result = rect_0;
		if ((float)int_0 != 0f && (float)int_1 != 0f)
		{
			result.xMin = rect_0.xMin / (float)int_0;
			result.xMax = rect_0.xMax / (float)int_0;
			result.yMin = 1f - rect_0.yMax / (float)int_1;
			result.yMax = 1f - rect_0.yMin / (float)int_1;
		}
		return result;
	}

	public static Rect ConvertToPixels(Rect rect_0, int int_0, int int_1, bool bool_0)
	{
		Rect result = rect_0;
		if (bool_0)
		{
			result.xMin = Mathf.RoundToInt(rect_0.xMin * (float)int_0);
			result.xMax = Mathf.RoundToInt(rect_0.xMax * (float)int_0);
			result.yMin = Mathf.RoundToInt((1f - rect_0.yMax) * (float)int_1);
			result.yMax = Mathf.RoundToInt((1f - rect_0.yMin) * (float)int_1);
		}
		else
		{
			result.xMin = rect_0.xMin * (float)int_0;
			result.xMax = rect_0.xMax * (float)int_0;
			result.yMin = (1f - rect_0.yMax) * (float)int_1;
			result.yMax = (1f - rect_0.yMin) * (float)int_1;
		}
		return result;
	}

	public static Rect MakePixelPerfect(Rect rect_0)
	{
		rect_0.xMin = Mathf.RoundToInt(rect_0.xMin);
		rect_0.yMin = Mathf.RoundToInt(rect_0.yMin);
		rect_0.xMax = Mathf.RoundToInt(rect_0.xMax);
		rect_0.yMax = Mathf.RoundToInt(rect_0.yMax);
		return rect_0;
	}

	public static Rect MakePixelPerfect(Rect rect_0, int int_0, int int_1)
	{
		rect_0 = ConvertToPixels(rect_0, int_0, int_1, true);
		rect_0.xMin = Mathf.RoundToInt(rect_0.xMin);
		rect_0.yMin = Mathf.RoundToInt(rect_0.yMin);
		rect_0.xMax = Mathf.RoundToInt(rect_0.xMax);
		rect_0.yMax = Mathf.RoundToInt(rect_0.yMax);
		return ConvertToTexCoords(rect_0, int_0, int_1);
	}

	public static Vector2 ConstrainRect(Vector2 vector2_0, Vector2 vector2_1, Vector2 vector2_2, Vector2 vector2_3)
	{
		Vector2 zero = Vector2.zero;
		float num = vector2_1.x - vector2_0.x;
		float num2 = vector2_1.y - vector2_0.y;
		float num3 = vector2_3.x - vector2_2.x;
		float num4 = vector2_3.y - vector2_2.y;
		if (num > num3)
		{
			float num5 = num - num3;
			vector2_2.x -= num5;
			vector2_3.x += num5;
		}
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			vector2_2.y -= num6;
			vector2_3.y += num6;
		}
		if (vector2_0.x < vector2_2.x)
		{
			zero.x += vector2_2.x - vector2_0.x;
		}
		if (vector2_1.x > vector2_3.x)
		{
			zero.x -= vector2_1.x - vector2_3.x;
		}
		if (vector2_0.y < vector2_2.y)
		{
			zero.y += vector2_2.y - vector2_0.y;
		}
		if (vector2_1.y > vector2_3.y)
		{
			zero.y -= vector2_1.y - vector2_3.y;
		}
		return zero;
	}

	public static Bounds CalculateAbsoluteWidgetBounds(Transform transform_0)
	{
		if (transform_0 != null)
		{
			UIWidget[] componentsInChildren = transform_0.GetComponentsInChildren<UIWidget>();
			if (componentsInChildren.Length == 0)
			{
				return new Bounds(transform_0.position, Vector3.zero);
			}
			Vector3 center = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 point = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				if (!uIWidget.enabled)
				{
					continue;
				}
				Vector3[] vector3_ = uIWidget.Vector3_3;
				for (int j = 0; j < 4; j++)
				{
					Vector3 vector = vector3_[j];
					if (vector.x > point.x)
					{
						point.x = vector.x;
					}
					if (vector.y > point.y)
					{
						point.y = vector.y;
					}
					if (vector.z > point.z)
					{
						point.z = vector.z;
					}
					if (vector.x < center.x)
					{
						center.x = vector.x;
					}
					if (vector.y < center.y)
					{
						center.y = vector.y;
					}
					if (vector.z < center.z)
					{
						center.z = vector.z;
					}
				}
			}
			Bounds result = new Bounds(center, Vector3.zero);
			result.Encapsulate(point);
			return result;
		}
		return new Bounds(Vector3.zero, Vector3.zero);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform transform_0)
	{
		return CalculateRelativeWidgetBounds(transform_0, transform_0, false);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform transform_0, bool bool_0)
	{
		return CalculateRelativeWidgetBounds(transform_0, transform_0, bool_0);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform transform_0, Transform transform_1)
	{
		return CalculateRelativeWidgetBounds(transform_0, transform_1, false);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform transform_0, Transform transform_1, bool bool_0)
	{
		if (transform_1 != null && transform_0 != null)
		{
			bool bool_ = false;
			Matrix4x4 matrix4x4_ = transform_0.worldToLocalMatrix;
			Vector3 vector3_ = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector3_2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			CalculateRelativeWidgetBounds(transform_1, bool_0, true, ref matrix4x4_, ref vector3_, ref vector3_2, ref bool_);
			if (bool_)
			{
				Bounds result = new Bounds(vector3_, Vector3.zero);
				result.Encapsulate(vector3_2);
				return result;
			}
		}
		return new Bounds(Vector3.zero, Vector3.zero);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	private static void CalculateRelativeWidgetBounds(Transform transform_0, bool bool_0, bool bool_1, ref Matrix4x4 matrix4x4_0, ref Vector3 vector3_0, ref Vector3 vector3_1, ref bool bool_2)
	{
		if (transform_0 == null || (!bool_0 && !NGUITools.GetActive(transform_0.gameObject)))
		{
			return;
		}
		UIPanel uIPanel = ((!bool_1) ? transform_0.GetComponent<UIPanel>() : null);
		if (uIPanel != null && !uIPanel.enabled)
		{
			return;
		}
		if (uIPanel != null && uIPanel.Clipping_0 != 0)
		{
			Vector3[] vector3_2 = uIPanel.Vector3_3;
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = matrix4x4_0.MultiplyPoint3x4(vector3_2[i]);
				if (vector.x > vector3_1.x)
				{
					vector3_1.x = vector.x;
				}
				if (vector.y > vector3_1.y)
				{
					vector3_1.y = vector.y;
				}
				if (vector.z > vector3_1.z)
				{
					vector3_1.z = vector.z;
				}
				if (vector.x < vector3_0.x)
				{
					vector3_0.x = vector.x;
				}
				if (vector.y < vector3_0.y)
				{
					vector3_0.y = vector.y;
				}
				if (vector.z < vector3_0.z)
				{
					vector3_0.z = vector.z;
				}
				bool_2 = true;
			}
			return;
		}
		UIWidget component = transform_0.GetComponent<UIWidget>();
		if (component != null && component.enabled)
		{
			Vector3[] vector3_3 = component.Vector3_3;
			for (int j = 0; j < 4; j++)
			{
				Vector3 vector2 = matrix4x4_0.MultiplyPoint3x4(vector3_3[j]);
				if (vector2.x > vector3_1.x)
				{
					vector3_1.x = vector2.x;
				}
				if (vector2.y > vector3_1.y)
				{
					vector3_1.y = vector2.y;
				}
				if (vector2.z > vector3_1.z)
				{
					vector3_1.z = vector2.z;
				}
				if (vector2.x < vector3_0.x)
				{
					vector3_0.x = vector2.x;
				}
				if (vector2.y < vector3_0.y)
				{
					vector3_0.y = vector2.y;
				}
				if (vector2.z < vector3_0.z)
				{
					vector3_0.z = vector2.z;
				}
				bool_2 = true;
			}
		}
		int k = 0;
		for (int childCount = transform_0.childCount; k < childCount; k++)
		{
			CalculateRelativeWidgetBounds(transform_0.GetChild(k), bool_0, false, ref matrix4x4_0, ref vector3_0, ref vector3_1, ref bool_2);
		}
	}

	public static Vector3 SpringDampen(ref Vector3 vector3_0, float float_0, float float_1)
	{
		if (float_1 > 1f)
		{
			float_1 = 1f;
		}
		float f = 1f - float_0 * 0.001f;
		int num = Mathf.RoundToInt(float_1 * 1000f);
		float num2 = Mathf.Pow(f, num);
		Vector3 vector = vector3_0 * ((num2 - 1f) / Mathf.Log(f));
		vector3_0 *= num2;
		return vector * 0.06f;
	}

	public static Vector2 SpringDampen(ref Vector2 vector2_0, float float_0, float float_1)
	{
		if (float_1 > 1f)
		{
			float_1 = 1f;
		}
		float f = 1f - float_0 * 0.001f;
		int num = Mathf.RoundToInt(float_1 * 1000f);
		float num2 = Mathf.Pow(f, num);
		Vector2 vector = vector2_0 * ((num2 - 1f) / Mathf.Log(f));
		vector2_0 *= num2;
		return vector * 0.06f;
	}

	public static float SpringLerp(float float_0, float float_1)
	{
		if (float_1 > 1f)
		{
			float_1 = 1f;
		}
		int num = Mathf.RoundToInt(float_1 * 1000f);
		float_1 = 0.001f * float_0;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, 1f, float_1);
		}
		return num2;
	}

	public static float SpringLerp(float float_0, float float_1, float float_2, float float_3)
	{
		if (float_3 > 1f)
		{
			float_3 = 1f;
		}
		int num = Mathf.RoundToInt(float_3 * 1000f);
		float_3 = 0.001f * float_2;
		for (int i = 0; i < num; i++)
		{
			float_0 = Mathf.Lerp(float_0, float_1, float_3);
		}
		return float_0;
	}

	public static Vector2 SpringLerp(Vector2 vector2_0, Vector2 vector2_1, float float_0, float float_1)
	{
		return Vector2.Lerp(vector2_0, vector2_1, SpringLerp(float_0, float_1));
	}

	public static Vector3 SpringLerp(Vector3 vector3_0, Vector3 vector3_1, float float_0, float float_1)
	{
		return Vector3.Lerp(vector3_0, vector3_1, SpringLerp(float_0, float_1));
	}

	public static Quaternion SpringLerp(Quaternion quaternion_0, Quaternion quaternion_1, float float_0, float float_1)
	{
		return Quaternion.Slerp(quaternion_0, quaternion_1, SpringLerp(float_0, float_1));
	}

	public static float RotateTowards(float float_0, float float_1, float float_2)
	{
		float num = WrapAngle(float_1 - float_0);
		if (Mathf.Abs(num) > float_2)
		{
			num = float_2 * Mathf.Sign(num);
		}
		return float_0 + num;
	}

	private static float DistancePointToLineSegment(Vector2 vector2_0, Vector2 vector2_1, Vector2 vector2_2)
	{
		float sqrMagnitude = (vector2_2 - vector2_1).sqrMagnitude;
		if (sqrMagnitude == 0f)
		{
			return (vector2_0 - vector2_1).magnitude;
		}
		float num = Vector2.Dot(vector2_0 - vector2_1, vector2_2 - vector2_1) / sqrMagnitude;
		if (num < 0f)
		{
			return (vector2_0 - vector2_1).magnitude;
		}
		if (num > 1f)
		{
			return (vector2_0 - vector2_2).magnitude;
		}
		Vector2 vector = vector2_1 + num * (vector2_2 - vector2_1);
		return (vector2_0 - vector).magnitude;
	}

	public static float DistanceToRectangle(Vector2[] vector2_0, Vector2 vector2_1)
	{
		bool flag = false;
		int int_ = 4;
		for (int i = 0; i < 5; i++)
		{
			Vector3 vector = vector2_0[RepeatIndex(i, 4)];
			Vector3 vector2 = vector2_0[RepeatIndex(int_, 4)];
			if (vector.y > vector2_1.y != vector2.y > vector2_1.y && vector2_1.x < (vector2.x - vector.x) * (vector2_1.y - vector.y) / (vector2.y - vector.y) + vector.x)
			{
				flag = !flag;
			}
			int_ = i;
		}
		if (!flag)
		{
			float num = -1f;
			for (int j = 0; j < 4; j++)
			{
				Vector3 vector3 = vector2_0[j];
				Vector3 vector4 = vector2_0[RepeatIndex(j + 1, 4)];
				float num2 = DistancePointToLineSegment(vector2_1, vector3, vector4);
				if (num2 < num || num < 0f)
				{
					num = num2;
				}
			}
			return num;
		}
		return 0f;
	}

	public static float DistanceToRectangle(Vector3[] vector3_0, Vector2 vector2_0, Camera camera_0)
	{
		Vector2[] array = new Vector2[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = camera_0.WorldToScreenPoint(vector3_0[i]);
		}
		return DistanceToRectangle(array, vector2_0);
	}

	public static Vector2 GetPivotOffset(UIWidget.Pivot pivot_0)
	{
		Vector2 zero = Vector2.zero;
		switch (pivot_0)
		{
		default:
			zero.x = 0f;
			break;
		case UIWidget.Pivot.TopRight:
		case UIWidget.Pivot.Right:
		case UIWidget.Pivot.BottomRight:
			zero.x = 1f;
			break;
		case UIWidget.Pivot.Top:
		case UIWidget.Pivot.Center:
		case UIWidget.Pivot.Bottom:
			zero.x = 0.5f;
			break;
		}
		switch (pivot_0)
		{
		default:
			zero.y = 0f;
			break;
		case UIWidget.Pivot.TopLeft:
		case UIWidget.Pivot.Top:
		case UIWidget.Pivot.TopRight:
			zero.y = 1f;
			break;
		case UIWidget.Pivot.Left:
		case UIWidget.Pivot.Center:
		case UIWidget.Pivot.Right:
			zero.y = 0.5f;
			break;
		}
		return zero;
	}

	public static UIWidget.Pivot GetPivot(Vector2 vector2_0)
	{
		if (vector2_0.x == 0f)
		{
			if (vector2_0.y == 0f)
			{
				return UIWidget.Pivot.BottomLeft;
			}
			if (vector2_0.y == 1f)
			{
				return UIWidget.Pivot.TopLeft;
			}
			return UIWidget.Pivot.Left;
		}
		if (vector2_0.x == 1f)
		{
			if (vector2_0.y == 0f)
			{
				return UIWidget.Pivot.BottomRight;
			}
			if (vector2_0.y == 1f)
			{
				return UIWidget.Pivot.TopRight;
			}
			return UIWidget.Pivot.Right;
		}
		if (vector2_0.y == 0f)
		{
			return UIWidget.Pivot.Bottom;
		}
		if (vector2_0.y == 1f)
		{
			return UIWidget.Pivot.Top;
		}
		return UIWidget.Pivot.Center;
	}

	public static void MoveWidget(UIRect uirect_0, float float_0, float float_1)
	{
		MoveRect(uirect_0, float_0, float_1);
	}

	public static void MoveRect(UIRect uirect_0, float float_0, float float_1)
	{
		int num = Mathf.FloorToInt(float_0 + 0.5f);
		int num2 = Mathf.FloorToInt(float_1 + 0.5f);
		uirect_0.Transform_0.localPosition += new Vector3(num, num2);
		int num3 = 0;
		if ((bool)uirect_0.leftAnchor.target)
		{
			num3++;
			uirect_0.leftAnchor.absolute += num;
		}
		if ((bool)uirect_0.rightAnchor.target)
		{
			num3++;
			uirect_0.rightAnchor.absolute += num;
		}
		if ((bool)uirect_0.bottomAnchor.target)
		{
			num3++;
			uirect_0.bottomAnchor.absolute += num2;
		}
		if ((bool)uirect_0.topAnchor.target)
		{
			num3++;
			uirect_0.topAnchor.absolute += num2;
		}
		if (num3 != 0)
		{
			uirect_0.UpdateAnchors();
		}
	}

	public static void ResizeWidget(UIWidget uiwidget_0, UIWidget.Pivot pivot_0, float float_0, float float_1, int int_0, int int_1)
	{
		ResizeWidget(uiwidget_0, pivot_0, float_0, float_1, 2, 2, 100000, 100000);
	}

	public static void ResizeWidget(UIWidget uiwidget_0, UIWidget.Pivot pivot_0, float float_0, float float_1, int int_0, int int_1, int int_2, int int_3)
	{
		if (pivot_0 == UIWidget.Pivot.Center)
		{
			int num = Mathf.RoundToInt(float_0 - (float)uiwidget_0.Int32_0);
			int num2 = Mathf.RoundToInt(float_1 - (float)uiwidget_0.Int32_1);
			num -= num & 1;
			num2 -= num2 & 1;
			if ((num | num2) != 0)
			{
				num >>= 1;
				num2 >>= 1;
				AdjustWidget(uiwidget_0, -num, -num2, num, num2, int_0, int_1);
			}
			return;
		}
		Vector3 vector = new Vector3(float_0, float_1);
		vector = Quaternion.Inverse(uiwidget_0.Transform_0.localRotation) * vector;
		switch (pivot_0)
		{
		case UIWidget.Pivot.TopLeft:
			AdjustWidget(uiwidget_0, vector.x, 0f, 0f, vector.y, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.Top:
			AdjustWidget(uiwidget_0, 0f, 0f, 0f, vector.y, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.TopRight:
			AdjustWidget(uiwidget_0, 0f, 0f, vector.x, vector.y, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.Left:
			AdjustWidget(uiwidget_0, vector.x, 0f, 0f, 0f, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.Right:
			AdjustWidget(uiwidget_0, 0f, 0f, vector.x, 0f, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.BottomLeft:
			AdjustWidget(uiwidget_0, vector.x, vector.y, 0f, 0f, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.Bottom:
			AdjustWidget(uiwidget_0, 0f, vector.y, 0f, 0f, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.BottomRight:
			AdjustWidget(uiwidget_0, 0f, vector.y, vector.x, 0f, int_0, int_1, int_2, int_3);
			break;
		case UIWidget.Pivot.Center:
			break;
		}
	}

	public static void AdjustWidget(UIWidget uiwidget_0, float float_0, float float_1, float float_2, float float_3)
	{
		AdjustWidget(uiwidget_0, float_0, float_1, float_2, float_3, 2, 2, 100000, 100000);
	}

	public static void AdjustWidget(UIWidget uiwidget_0, float float_0, float float_1, float float_2, float float_3, int int_0, int int_1)
	{
		AdjustWidget(uiwidget_0, float_0, float_1, float_2, float_3, int_0, int_1, 100000, 100000);
	}

	public static void AdjustWidget(UIWidget uiwidget_0, float float_0, float float_1, float float_2, float float_3, int int_0, int int_1, int int_2, int int_3)
	{
		Vector2 vector2_ = uiwidget_0.Vector2_0;
		Transform transform_ = uiwidget_0.Transform_0;
		Quaternion localRotation = transform_.localRotation;
		int num = Mathf.FloorToInt(float_0 + 0.5f);
		int num2 = Mathf.FloorToInt(float_1 + 0.5f);
		int num3 = Mathf.FloorToInt(float_2 + 0.5f);
		int num4 = Mathf.FloorToInt(float_3 + 0.5f);
		if (vector2_.x == 0.5f && (num == 0 || num3 == 0))
		{
			num = num >> 1 << 1;
			num3 = num3 >> 1 << 1;
		}
		if (vector2_.y == 0.5f && (num2 == 0 || num4 == 0))
		{
			num2 = num2 >> 1 << 1;
			num4 = num4 >> 1 << 1;
		}
		Vector3 vector = localRotation * new Vector3(num, num4);
		Vector3 vector2 = localRotation * new Vector3(num3, num4);
		Vector3 vector3 = localRotation * new Vector3(num, num2);
		Vector3 vector4 = localRotation * new Vector3(num3, num2);
		Vector3 vector5 = localRotation * new Vector3(num, 0f);
		Vector3 vector6 = localRotation * new Vector3(num3, 0f);
		Vector3 vector7 = localRotation * new Vector3(0f, num4);
		Vector3 vector8 = localRotation * new Vector3(0f, num2);
		Vector3 zero = Vector3.zero;
		if (vector2_.x == 0f && vector2_.y == 1f)
		{
			zero.x = vector.x;
			zero.y = vector.y;
		}
		else if (vector2_.x == 1f && vector2_.y == 0f)
		{
			zero.x = vector4.x;
			zero.y = vector4.y;
		}
		else if (vector2_.x == 0f && vector2_.y == 0f)
		{
			zero.x = vector3.x;
			zero.y = vector3.y;
		}
		else if (vector2_.x == 1f && vector2_.y == 1f)
		{
			zero.x = vector2.x;
			zero.y = vector2.y;
		}
		else if (vector2_.x == 0f && vector2_.y == 0.5f)
		{
			zero.x = vector5.x + (vector7.x + vector8.x) * 0.5f;
			zero.y = vector5.y + (vector7.y + vector8.y) * 0.5f;
		}
		else if (vector2_.x == 1f && vector2_.y == 0.5f)
		{
			zero.x = vector6.x + (vector7.x + vector8.x) * 0.5f;
			zero.y = vector6.y + (vector7.y + vector8.y) * 0.5f;
		}
		else if (vector2_.x == 0.5f && vector2_.y == 1f)
		{
			zero.x = vector7.x + (vector5.x + vector6.x) * 0.5f;
			zero.y = vector7.y + (vector5.y + vector6.y) * 0.5f;
		}
		else if (vector2_.x == 0.5f && vector2_.y == 0f)
		{
			zero.x = vector8.x + (vector5.x + vector6.x) * 0.5f;
			zero.y = vector8.y + (vector5.y + vector6.y) * 0.5f;
		}
		else if (vector2_.x == 0.5f && vector2_.y == 0.5f)
		{
			zero.x = (vector5.x + vector6.x + vector7.x + vector8.x) * 0.5f;
			zero.y = (vector7.y + vector8.y + vector5.y + vector6.y) * 0.5f;
		}
		int_0 = Mathf.Max(int_0, uiwidget_0.Int32_4);
		int_1 = Mathf.Max(int_1, uiwidget_0.Int32_5);
		int num5 = uiwidget_0.Int32_0 + num3 - num;
		int num6 = uiwidget_0.Int32_1 + num4 - num2;
		Vector3 zero2 = Vector3.zero;
		int num7 = num5;
		if (num5 < int_0)
		{
			num7 = int_0;
		}
		else if (num5 > int_2)
		{
			num7 = int_2;
		}
		if (num5 != num7)
		{
			if (num != 0)
			{
				zero2.x -= Mathf.Lerp(num7 - num5, 0f, vector2_.x);
			}
			else
			{
				zero2.x += Mathf.Lerp(0f, num7 - num5, vector2_.x);
			}
			num5 = num7;
		}
		int num8 = num6;
		if (num6 < int_1)
		{
			num8 = int_1;
		}
		else if (num6 > int_3)
		{
			num8 = int_3;
		}
		if (num6 != num8)
		{
			if (num2 != 0)
			{
				zero2.y -= Mathf.Lerp(num8 - num6, 0f, vector2_.y);
			}
			else
			{
				zero2.y += Mathf.Lerp(0f, num8 - num6, vector2_.y);
			}
			num6 = num8;
		}
		if (vector2_.x == 0.5f)
		{
			num5 = num5 >> 1 << 1;
		}
		if (vector2_.y == 0.5f)
		{
			num6 = num6 >> 1 << 1;
		}
		Vector3 vector10 = (transform_.localPosition = transform_.localPosition + zero + localRotation * zero2);
		uiwidget_0.SetDimensions(num5, num6);
		if (uiwidget_0.Boolean_1)
		{
			transform_ = transform_.parent;
			float num9 = vector10.x - vector2_.x * (float)num5;
			float num10 = vector10.y - vector2_.y * (float)num6;
			if ((bool)uiwidget_0.leftAnchor.target)
			{
				uiwidget_0.leftAnchor.SetHorizontal(transform_, num9);
			}
			if ((bool)uiwidget_0.rightAnchor.target)
			{
				uiwidget_0.rightAnchor.SetHorizontal(transform_, num9 + (float)num5);
			}
			if ((bool)uiwidget_0.bottomAnchor.target)
			{
				uiwidget_0.bottomAnchor.SetVertical(transform_, num10);
			}
			if ((bool)uiwidget_0.topAnchor.target)
			{
				uiwidget_0.topAnchor.SetVertical(transform_, num10 + (float)num6);
			}
		}
	}

	public static int AdjustByDPI(float float_0)
	{
		float num = Screen.dpi;
		RuntimePlatform platform = Application.platform;
		if (num == 0f)
		{
			num = ((platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) ? 160f : 96f);
		}
		int num2 = Mathf.RoundToInt(float_0 * (96f / num));
		if ((num2 & 1) == 1)
		{
			num2++;
		}
		return num2;
	}

	public static Vector2 ScreenToPixels(Vector2 vector2_0, Transform transform_0)
	{
		int layer = transform_0.gameObject.layer;
		Camera camera = NGUITools.FindCameraForLayer(layer);
		if (camera == null)
		{
			UnityEngine.Debug.LogWarning("No camera found for layer " + layer);
			return vector2_0;
		}
		Vector3 position = camera.ScreenToWorldPoint(vector2_0);
		return transform_0.InverseTransformPoint(position);
	}

	public static Vector2 ScreenToParentPixels(Vector2 vector2_0, Transform transform_0)
	{
		int layer = transform_0.gameObject.layer;
		if (transform_0.parent != null)
		{
			transform_0 = transform_0.parent;
		}
		Camera camera = NGUITools.FindCameraForLayer(layer);
		if (camera == null)
		{
			UnityEngine.Debug.LogWarning("No camera found for layer " + layer);
			return vector2_0;
		}
		Vector3 vector = camera.ScreenToWorldPoint(vector2_0);
		return (!(transform_0 != null)) ? vector : transform_0.InverseTransformPoint(vector);
	}
}
