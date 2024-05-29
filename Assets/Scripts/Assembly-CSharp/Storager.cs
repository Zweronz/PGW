using System;
using System.Collections.Generic;
using Rilisoft;
using UnityEngine;

public static class Storager
{
	private const bool bool_0 = true;

	private static readonly IDictionary<string, SaltedInt> idictionary_0 = new Dictionary<string, SaltedInt>();

	private static readonly System.Random random_0 = new System.Random();

	private static string string_0 = string.Empty;

	public static void Init(string string_1)
	{
		if (!string.IsNullOrEmpty(string_1))
		{
			string_1 += "_";
		}
		string_0 = string_1;
	}

	private static string GetKey(string string_1)
	{
		return string_0 + string_1;
	}

	public static bool Save()
	{
		PlayerPrefs.Save();
		return true;
	}

	public static bool HasKey(string string_1)
	{
		string_1 = GetKey(string_1);
		return CryptoPlayerPrefs.HasKey(string_1);
	}

	public static void DeleteKey(string string_1)
	{
		string_1 = GetKey(string_1);
		CryptoPlayerPrefs.DeleteKey(string_1);
	}

	public static void DeleteAll()
	{
		idictionary_0.Clear();
		CryptoPlayerPrefs.DeleteAll();
		CampaignProgress.ResetProgress();
	}

	public static void SetFloat(string string_1, float float_0)
	{
		string_1 = GetKey(string_1);
		CryptoPlayerPrefs.SetFloat(string_1, float_0);
	}

	public static float GetFloat(string string_1, float float_0 = 0f)
	{
		if (HasKey(string_1))
		{
			string_1 = GetKey(string_1);
			return CryptoPlayerPrefs.GetFloat(string_1, 0f);
		}
		return float_0;
	}

	public static void SetInt(string string_1, int int_0)
	{
		string_1 = GetKey(string_1);
		CryptoPlayerPrefs.SetInt(string_1, int_0);
		idictionary_0[string_1] = new SaltedInt(random_0.Next(), int_0);
	}

	public static int GetInt(string string_1, int int_0 = 0)
	{
		SaltedInt value;
		if (idictionary_0.TryGetValue(GetKey(string_1), out value))
		{
			return value.Int32_0;
		}
		if (HasKey(string_1))
		{
			string_1 = GetKey(string_1);
			int @int = CryptoPlayerPrefs.GetInt(string_1);
			idictionary_0.Add(string_1, new SaltedInt(random_0.Next(), @int));
			return @int;
		}
		return int_0;
	}

	public static void SetString(string string_1, string string_2)
	{
		string_1 = GetKey(string_1);
		CryptoPlayerPrefs.SetString(string_1, string_2);
	}

	public static string GetString(string string_1, string string_2 = "")
	{
		if (HasKey(string_1))
		{
			string_1 = GetKey(string_1);
			return CryptoPlayerPrefs.GetString(string_1, string.Empty);
		}
		return string_2;
	}

	public static void SetBool(string string_1, bool bool_1)
	{
		SetInt(string_1, bool_1 ? 1 : 0);
	}

	public static bool GetBool(string string_1, bool bool_1 = false)
	{
		if (HasKey(string_1))
		{
			return GetInt(string_1) == 1;
		}
		return bool_1;
	}

	public static bool IsInitialized(string string_1)
	{
		return HasKey(string_1);
	}

	public static void SetInitialized(string string_1)
	{
		SetInt(string_1, 0);
	}

	public static void SavePos(string string_1, GameObject gameObject_0)
	{
		SetString(string_1, gameObject_0.transform.position.x + "&" + gameObject_0.transform.position.y + "&" + gameObject_0.transform.position.z);
	}

	public static void SaveString(string string_1, string string_2)
	{
		SetString(string_1, string_2);
	}

	public static void SaveStringArray(string string_1, string[] string_2)
	{
		SetString(string_1, string.Join("#", string_2));
	}

	public static void SaveStringArray(string string_1, string[] string_2, char char_0)
	{
		SetString(string_1, string.Join(char_0.ToString(), string_2));
	}

	public static void SaveInt(string string_1, int int_0)
	{
		SetInt(string_1, int_0);
	}

	public static void SaveIntArray(string string_1, int[] int_0)
	{
		string text = string.Empty;
		for (int i = 0; i < int_0.Length; i++)
		{
			text = text + int_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveUInt(string string_1, uint uint_0)
	{
		SetString(string_1, uint_0.ToString());
	}

	public static void SaveUIntArray(string string_1, uint[] uint_0)
	{
		string text = string.Empty;
		for (int i = 0; i < uint_0.Length; i++)
		{
			text = text + uint_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveLong(string string_1, long long_0)
	{
		SetString(string_1, long_0.ToString());
	}

	public static void SaveLongArray(string string_1, long[] long_0)
	{
		string text = string.Empty;
		for (int i = 0; i < long_0.Length; i++)
		{
			text = text + long_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveULong(string string_1, ulong ulong_0)
	{
		SetString(string_1, ulong_0.ToString());
	}

	public static void SaveULongArray(string string_1, ulong[] ulong_0)
	{
		string text = string.Empty;
		for (int i = 0; i < ulong_0.Length; i++)
		{
			text = text + ulong_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveShort(string string_1, short short_0)
	{
		SetString(string_1, short_0.ToString());
	}

	public static void SaveShortArray(string string_1, short[] short_0)
	{
		string text = string.Empty;
		for (int i = 0; i < short_0.Length; i++)
		{
			text = text + short_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveUShort(string string_1, ushort ushort_0)
	{
		SetString(string_1, ushort_0.ToString());
	}

	public static void SaveUShortArray(string string_1, ushort[] ushort_0)
	{
		string text = string.Empty;
		for (int i = 0; i < ushort_0.Length; i++)
		{
			text = text + ushort_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveFloat(string string_1, float float_0)
	{
		SetFloat(string_1, float_0);
	}

	public static void SaveFloatArray(string string_1, float[] float_0)
	{
		string text = string.Empty;
		for (int i = 0; i < float_0.Length; i++)
		{
			text = text + float_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveDouble(string string_1, double double_0)
	{
		SetString(string_1, double_0.ToString());
	}

	public static void SaveDoubleArray(string string_1, double[] double_0)
	{
		string text = string.Empty;
		for (int i = 0; i < double_0.Length; i++)
		{
			text = text + double_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveBool(string string_1, bool bool_1)
	{
		SetString(string_1, bool_1.ToString());
	}

	public static void SaveBoolArray(string string_1, bool[] bool_1)
	{
		string text = string.Empty;
		for (int i = 0; i < bool_1.Length; i++)
		{
			text = text + bool_1[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveChar(string string_1, char char_0)
	{
		SetString(string_1, char_0.ToString());
	}

	public static void SaveCharArray(string string_1, char[] char_0)
	{
		string text = string.Empty;
		for (int i = 0; i < char_0.Length; i++)
		{
			text = text + char_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveCharArray(string string_1, char[] char_0, char char_1)
	{
		string text = string.Empty;
		for (int i = 0; i < char_0.Length; i++)
		{
			text = text + char_0[i] + char_1;
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveDecimal(string string_1, decimal decimal_0)
	{
		SetString(string_1, decimal_0.ToString());
	}

	public static void SaveDecimalArray(string string_1, decimal[] decimal_0)
	{
		string text = string.Empty;
		for (int i = 0; i < decimal_0.Length; i++)
		{
			text = text + decimal_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveByte(string string_1, byte byte_0)
	{
		SetString(string_1, byte_0.ToString());
	}

	public static void SaveByteArray(string string_1, byte[] byte_0)
	{
		string text = string.Empty;
		for (int i = 0; i < byte_0.Length; i++)
		{
			text = text + byte_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveSByte(string string_1, sbyte sbyte_0)
	{
		SetString(string_1, sbyte_0.ToString());
	}

	public static void SaveSByteArray(string string_1, sbyte[] sbyte_0)
	{
		string text = string.Empty;
		for (int i = 0; i < sbyte_0.Length; i++)
		{
			text = text + sbyte_0[i] + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveVector4(string string_1, Vector4 vector4_0)
	{
		SetString(string_1, vector4_0.x + "&" + vector4_0.y + "&" + vector4_0.z + "&" + vector4_0.w);
	}

	public static void SaveVector4Array(string string_1, Vector4[] vector4_0)
	{
		string text = string.Empty;
		for (int i = 0; i < vector4_0.Length; i++)
		{
			string text2 = text;
			text = text2 + vector4_0[i].x + "&" + vector4_0[i].y + "&" + vector4_0[i].z + "&" + vector4_0[i].w + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveVector3(string string_1, Vector3 vector3_0)
	{
		SetString(string_1, vector3_0.x + "&" + vector3_0.y + "&" + vector3_0.z);
	}

	public static void SaveVector3Array(string string_1, Vector3[] vector3_0)
	{
		string text = string.Empty;
		for (int i = 0; i < vector3_0.Length; i++)
		{
			string text2 = text;
			text = text2 + vector3_0[i].x + "&" + vector3_0[i].y + "&" + vector3_0[i].z + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveVector2(string string_1, Vector2 vector2_0)
	{
		SetString(string_1, vector2_0.x + "&" + vector2_0.y);
	}

	public static void SaveVector2Array(string string_1, Vector2[] vector2_0)
	{
		string text = string.Empty;
		for (int i = 0; i < vector2_0.Length; i++)
		{
			string text2 = text;
			text = text2 + vector2_0[i].x + "&" + vector2_0[i].y + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveQuaternion(string string_1, Quaternion quaternion_0)
	{
		SetString(string_1, quaternion_0.x + "&" + quaternion_0.y + "&" + quaternion_0.z + "&" + quaternion_0.w);
	}

	public static void SaveQuaternionArray(string string_1, Quaternion[] quaternion_0)
	{
		string text = string.Empty;
		for (int i = 0; i < quaternion_0.Length; i++)
		{
			string text2 = text;
			text = text2 + quaternion_0[i].x + "&" + quaternion_0[i].y + "&" + quaternion_0[i].z + "&" + quaternion_0[i].w + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveColor(string string_1, Color color_0)
	{
		SetString(string_1, color_0.r + "&" + color_0.g + "&" + color_0.b + "&" + color_0.a);
	}

	public static void SaveColorArray(string string_1, Color[] color_0)
	{
		string text = string.Empty;
		for (int i = 0; i < color_0.Length; i++)
		{
			string text2 = text;
			text = text2 + color_0[i].r + "&" + color_0[i].g + "&" + color_0[i].b + "&" + color_0[i].a + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveKeyCode(string string_1, KeyCode keyCode_0)
	{
		SetString(string_1, keyCode_0.ToString());
	}

	public static void SaveKeyCodeArray(string string_1, KeyCode[] keyCode_0)
	{
		string text = string.Empty;
		for (int i = 0; i < keyCode_0.Length; i++)
		{
			text = text + keyCode_0[i].ToString() + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveRect(string string_1, Rect rect_0)
	{
		SetString(string_1, rect_0.x + "&" + rect_0.y + "&" + rect_0.width + "&" + rect_0.height);
	}

	public static void SaveRectArray(string string_1, Rect[] rect_0)
	{
		string text = string.Empty;
		for (int i = 0; i < rect_0.Length; i++)
		{
			string text2 = text;
			text = text2 + rect_0[i].x + "&" + rect_0[i].y + "&" + rect_0[i].width + "&" + rect_0[i].height + "#";
		}
		SetString(string_1, text.ToString());
	}

	public static void SaveTexture2D(string string_1, Texture2D texture2D_0)
	{
		byte[] inArray = texture2D_0.EncodeToPNG();
		int width = texture2D_0.width;
		int height = texture2D_0.height;
		string string_2 = width + "&" + height + "&" + Convert.ToBase64String(inArray);
		SetString(string_1, string_2);
	}

	public static void LoadPos(string string_1, GameObject gameObject_0)
	{
		if (!HasKey(string_1))
		{
			gameObject_0.transform.position = new Vector3(0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		Vector3 position = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		gameObject_0.transform.position = position;
	}

	public static string LoadString(string string_1)
	{
		if (!HasKey(string_1))
		{
			return string.Empty;
		}
		return GetString(string_1, string.Empty);
	}

	public static string[] LoadStringArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		return GetString(string_1, string.Empty).Split('#');
	}

	public static string[] LoadStringArray(string string_1, char char_0)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		return GetString(string_1, string.Empty).Split(char_0);
	}

	public static int LoadInt(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0;
		}
		return GetInt(string_1);
	}

	public static int[] LoadIntArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		int[] array2 = new int[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = Convert.ToInt32(array[i]);
		}
		return array2;
	}

	public static uint LoadUInt(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0u;
		}
		return uint.Parse(GetString(string_1, string.Empty));
	}

	public static uint[] LoadUIntArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		uint[] array2 = new uint[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = Convert.ToUInt32(array[i]);
		}
		return array2;
	}

	public static long LoadLong(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0L;
		}
		return long.Parse(GetString(string_1, string.Empty));
	}

	public static long[] LoadLongArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		long[] array2 = new long[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = long.Parse(array[i]);
		}
		return array2;
	}

	public static ulong LoadULong(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0uL;
		}
		return ulong.Parse(GetString(string_1, string.Empty));
	}

	public static ulong[] LoadULongArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		ulong[] array2 = new ulong[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = ulong.Parse(array[i]);
		}
		return array2;
	}

	public static short LoadShort(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0;
		}
		return short.Parse(GetString(string_1, string.Empty));
	}

	public static short[] LoadShortArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		short[] array2 = new short[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = short.Parse(array[i]);
		}
		return array2;
	}

	public static ushort LoadUShort(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0;
		}
		return ushort.Parse(GetString(string_1, string.Empty));
	}

	public static ushort[] LoadUShortArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		ushort[] array2 = new ushort[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = ushort.Parse(array[i]);
		}
		return array2;
	}

	public static float LoadFloat(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0f;
		}
		return GetFloat(string_1, 0f);
	}

	public static float[] LoadFloatArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		float[] array2 = new float[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = float.Parse(array[i]);
		}
		return array2;
	}

	public static double LoadDouble(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0.0;
		}
		return double.Parse(GetString(string_1, string.Empty));
	}

	public static double[] LoadDoubleArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		double[] array2 = new double[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = double.Parse(array[i]);
		}
		return array2;
	}

	public static bool LoadBool(string string_1)
	{
		if (!HasKey(string_1))
		{
			return false;
		}
		string @string = GetString(string_1, string.Empty);
		return bool.Parse(@string);
	}

	public static bool[] LoadBoolArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		bool[] array2 = new bool[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = bool.Parse(array[i]);
		}
		return array2;
	}

	public static char LoadChar(string string_1)
	{
		if (!HasKey(string_1))
		{
			return '\0';
		}
		char result = '\0';
		char.TryParse(GetString(string_1, string.Empty), out result);
		return result;
	}

	public static char[] LoadCharArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		char[] array2 = new char[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			char.TryParse(array[i], out array2[i]);
		}
		return array2;
	}

	public static decimal LoadDecimal(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0m;
		}
		return decimal.Parse(GetString(string_1, string.Empty));
	}

	public static decimal[] LoadDecimalArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		decimal[] array2 = new decimal[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = decimal.Parse(array[i]);
		}
		return array2;
	}

	public static byte LoadByte(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0;
		}
		return byte.Parse(GetString(string_1, string.Empty));
	}

	public static byte[] LoadByteArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		byte[] array2 = new byte[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = byte.Parse(array[i]);
		}
		return array2;
	}

	public static sbyte LoadSByte(string string_1)
	{
		if (!HasKey(string_1))
		{
			return 0;
		}
		return sbyte.Parse(GetString(string_1, string.Empty));
	}

	public static sbyte[] LoadSByteArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		sbyte[] array2 = new sbyte[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = sbyte.Parse(array[i]);
		}
		return array2;
	}

	public static Vector4 LoadVector4(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Vector4(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
	}

	public static Vector4[] LoadVector4Array(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		Vector4[] array2 = new Vector4[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split("&"[0]);
			array2[i] = new Vector4(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]), float.Parse(array3[3]));
		}
		return array2;
	}

	public static Vector3 LoadVector3(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Vector3(0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
	}

	public static Vector3[] LoadVector3Array(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split('#');
		Vector3[] array2 = new Vector3[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split('&');
			array2[i] = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
		}
		return array2;
	}

	public static Vector2 LoadVector2(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Vector2(0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Vector2(float.Parse(array[0]), float.Parse(array[1]));
	}

	public static Vector2[] LoadVector2Array(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		Vector2[] array2 = new Vector2[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split("&"[0]);
			array2[i] = new Vector2(float.Parse(array3[0]), float.Parse(array3[1]));
		}
		return array2;
	}

	public static Quaternion LoadQuaternion(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Quaternion(0f, 0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Quaternion(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
	}

	public static Quaternion[] LoadQuaternionArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		Quaternion[] array2 = new Quaternion[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split("&"[0]);
			array2[i] = new Quaternion(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]), float.Parse(array3[3]));
		}
		return array2;
	}

	public static Color LoadColor(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Color(0f, 0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
	}

	public static Color[] LoadColorArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		Color[] array2 = new Color[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split("&"[0]);
			array2[i] = new Color(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]), float.Parse(array3[3]));
		}
		return array2;
	}

	public static KeyCode LoadKeyCode(string string_1)
	{
		if (!HasKey(string_1))
		{
			return KeyCode.Space;
		}
		return (KeyCode)(int)Enum.Parse(typeof(KeyCode), GetString(string_1, string.Empty));
	}

	public static KeyCode[] LoadKeyCodeArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		KeyCode[] array2 = new KeyCode[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array2[i] = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[i]);
		}
		return array2;
	}

	public static Rect LoadRect(string string_1)
	{
		if (!HasKey(string_1))
		{
			return new Rect(0f, 0f, 0f, 0f);
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		return new Rect(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
	}

	public static Rect[] LoadRectArray(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("#"[0]);
		Rect[] array2 = new Rect[array.Length - 1];
		for (int i = 0; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split("&"[0]);
			array2[i] = new Rect(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]), float.Parse(array3[3]));
		}
		return array2;
	}

	public static Texture2D LoadTexture2D(string string_1)
	{
		if (!HasKey(string_1))
		{
			return null;
		}
		string[] array = GetString(string_1, string.Empty).Split("&"[0]);
		byte[] data = Convert.FromBase64String(array[2]);
		Texture2D texture2D = new Texture2D(int.Parse(array[0]), int.Parse(array[1]));
		texture2D.LoadImage(data);
		return texture2D;
	}

	public static Texture2D LoadTexture2DURL(string string_1)
	{
		WWW wWW = new WWW(string_1);
		while (!wWW.isDone)
		{
		}
		return wWW.texture;
	}

	public static void LoadTexture2DURL(string string_1, GameObject gameObject_0)
	{
		WWW wWW = new WWW(string_1);
		while (!wWW.isDone)
		{
		}
		Texture2D texture = wWW.texture;
		gameObject_0.GetComponent<Renderer>().material.mainTexture = texture;
	}
}
