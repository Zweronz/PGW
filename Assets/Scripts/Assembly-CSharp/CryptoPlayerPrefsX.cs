using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptoPlayerPrefsX
{
	private enum ArrayType
	{
		Float = 0,
		Int32 = 1,
		Bool = 2,
		String = 3,
		Vector2 = 4,
		Vector3 = 5,
		Quaternion = 6,
		Color = 7
	}

	private static int int_0;

	private static int int_1;

	private static int int_2;

	private static byte[] byte_0;

	public static bool SetBool(string string_0, bool bool_0)
	{
		try
		{
			CryptoPlayerPrefs.SetInt(string_0, bool_0 ? 1 : 0);
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool GetBool(string string_0)
	{
		return CryptoPlayerPrefs.GetInt(string_0) == 1;
	}

	public static bool GetBool(string string_0, bool bool_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetBool(string_0);
		}
		return bool_0;
	}

	public static bool SetVector2(string string_0, Vector2 vector2_0)
	{
		return SetFloatArray(string_0, new float[2] { vector2_0.x, vector2_0.y });
	}

	private static Vector2 GetVector2(string string_0)
	{
		float[] floatArray = GetFloatArray(string_0);
		if (floatArray.Length < 2)
		{
			return Vector2.zero;
		}
		return new Vector2(floatArray[0], floatArray[1]);
	}

	public static Vector2 GetVector2(string string_0, Vector2 vector2_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetVector2(string_0);
		}
		return vector2_0;
	}

	public static bool SetVector3(string string_0, Vector3 vector3_0)
	{
		return SetFloatArray(string_0, new float[3] { vector3_0.x, vector3_0.y, vector3_0.z });
	}

	public static Vector3 GetVector3(string string_0)
	{
		float[] floatArray = GetFloatArray(string_0);
		if (floatArray.Length < 3)
		{
			return Vector3.zero;
		}
		return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}

	public static Vector3 GetVector3(string string_0, Vector3 vector3_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetVector3(string_0);
		}
		return vector3_0;
	}

	public static bool SetQuaternion(string string_0, Quaternion quaternion_0)
	{
		return SetFloatArray(string_0, new float[4] { quaternion_0.x, quaternion_0.y, quaternion_0.z, quaternion_0.w });
	}

	public static Quaternion GetQuaternion(string string_0)
	{
		float[] floatArray = GetFloatArray(string_0);
		if (floatArray.Length < 4)
		{
			return Quaternion.identity;
		}
		return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
	}

	public static Quaternion GetQuaternion(string string_0, Quaternion quaternion_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetQuaternion(string_0);
		}
		return quaternion_0;
	}

	public static bool SetColor(string string_0, Color color_0)
	{
		return SetFloatArray(string_0, new float[4] { color_0.r, color_0.g, color_0.b, color_0.a });
	}

	public static Color GetColor(string string_0)
	{
		float[] floatArray = GetFloatArray(string_0);
		if (floatArray.Length < 4)
		{
			return new Color(0f, 0f, 0f, 0f);
		}
		return new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
	}

	public static Color GetColor(string string_0, Color color_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetColor(string_0);
		}
		return color_0;
	}

	public static bool SetBoolArray(string string_0, bool[] bool_0)
	{
		if (bool_0.Length == 0)
		{
			Debug.LogError("The bool array cannot have 0 entries when setting " + string_0);
			return false;
		}
		byte[] array = new byte[(bool_0.Length + 7) / 8 + 5];
		array[0] = Convert.ToByte(ArrayType.Bool);
		BitArray bitArray = new BitArray(bool_0);
		bitArray.CopyTo(array, 5);
		Initialize();
		ConvertInt32ToBytes(bool_0.Length, array);
		return SaveBytes(string_0, array);
	}

	public static bool[] GetBoolArray(string string_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			byte[] array = Convert.FromBase64String(CryptoPlayerPrefs.GetString(string_0, string.Empty));
			if (array.Length < 6)
			{
				Debug.LogError("Corrupt preference file for " + string_0);
				return new bool[0];
			}
			if (array[0] != 2)
			{
				Debug.LogError(string_0 + " is not a boolean array");
				return new bool[0];
			}
			Initialize();
			byte[] array2 = new byte[array.Length - 5];
			Array.Copy(array, 5, array2, 0, array2.Length);
			BitArray bitArray = new BitArray(array2);
			bitArray.Length = ConvertBytesToInt32(array);
			bool[] array3 = new bool[bitArray.Count];
			bitArray.CopyTo(array3, 0);
			return array3;
		}
		return new bool[0];
	}

	public static bool[] GetBoolArray(string string_0, bool bool_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetBoolArray(string_0);
		}
		bool[] array = new bool[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = bool_0;
		}
		return array;
	}

	public static bool SetStringArray(string string_0, string[] string_1)
	{
		if (string_1.Length == 0)
		{
			Debug.LogError("The string array cannot have 0 entries when setting " + string_0);
			return false;
		}
		byte[] array = new byte[string_1.Length + 1];
		array[0] = Convert.ToByte(ArrayType.String);
		Initialize();
		int num = 0;
		while (true)
		{
			if (num < string_1.Length)
			{
				if (string_1[num] == null)
				{
					break;
				}
				if (string_1[num].Length <= 255)
				{
					array[int_2++] = (byte)string_1[num].Length;
					num++;
					continue;
				}
				Debug.LogError("Strings cannot be longer than 255 characters when setting " + string_0);
				return false;
			}
			try
			{
				CryptoPlayerPrefs.SetString(string_0, Convert.ToBase64String(array) + "|" + string.Join(string.Empty, string_1));
			}
			catch
			{
				return false;
			}
			return true;
		}
		Debug.LogError("Can't save null entries in the string array when setting " + string_0);
		return false;
	}

	public static string[] GetStringArray(string string_0)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			string @string = CryptoPlayerPrefs.GetString(string_0, string.Empty);
			int num = @string.IndexOf("|"[0]);
			if (num < 4)
			{
				Debug.LogError("Corrupt preference file for " + string_0);
				return new string[0];
			}
			byte[] array = Convert.FromBase64String(@string.Substring(0, num));
			if (array[0] != 3)
			{
				Debug.LogError(string_0 + " is not a string array");
				return new string[0];
			}
			Initialize();
			int num2 = array.Length - 1;
			string[] array2 = new string[num2];
			int num3 = num + 1;
			int num4 = 0;
			while (true)
			{
				if (num4 < num2)
				{
					int num5 = array[int_2++];
					if (num3 + num5 > @string.Length)
					{
						break;
					}
					array2[num4] = @string.Substring(num3, num5);
					num3 += num5;
					num4++;
					continue;
				}
				return array2;
			}
			Debug.LogError("Corrupt preference file for " + string_0);
			return new string[0];
		}
		return new string[0];
	}

	public static string[] GetStringArray(string string_0, string string_1, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetStringArray(string_0);
		}
		string[] array = new string[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = string_1;
		}
		return array;
	}

	public static bool SetIntArray(string string_0, int[] int_3)
	{
		return SetValue(string_0, int_3, ArrayType.Int32, 1, ConvertFromInt);
	}

	public static bool SetFloatArray(string string_0, float[] float_0)
	{
		return SetValue(string_0, float_0, ArrayType.Float, 1, ConvertFromFloat);
	}

	public static bool SetVector2Array(string string_0, Vector2[] vector2_0)
	{
		return SetValue(string_0, vector2_0, ArrayType.Vector2, 2, ConvertFromVector2);
	}

	public static bool SetVector3Array(string string_0, Vector3[] vector3_0)
	{
		return SetValue(string_0, vector3_0, ArrayType.Vector3, 3, ConvertFromVector3);
	}

	public static bool SetQuaternionArray(string string_0, Quaternion[] quaternion_0)
	{
		return SetValue(string_0, quaternion_0, ArrayType.Quaternion, 4, ConvertFromQuaternion);
	}

	public static bool SetColorArray(string string_0, Color[] color_0)
	{
		return SetValue(string_0, color_0, ArrayType.Color, 4, ConvertFromColor);
	}

	public static int[] GetIntArray(string string_0)
	{
		List<int> list = new List<int>();
		GetValue(string_0, list, ArrayType.Int32, 1, ConvertToInt);
		return list.ToArray();
	}

	public static int[] GetIntArray(string string_0, int int_3, int int_4)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetIntArray(string_0);
		}
		int[] array = new int[int_4];
		for (int i = 0; i < int_4; i++)
		{
			array[i] = int_3;
		}
		return array;
	}

	public static float[] GetFloatArray(string string_0)
	{
		List<float> list = new List<float>();
		GetValue(string_0, list, ArrayType.Float, 1, ConvertToFloat);
		return list.ToArray();
	}

	public static float[] GetFloatArray(string string_0, float float_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetFloatArray(string_0);
		}
		float[] array = new float[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = float_0;
		}
		return array;
	}

	public static Vector2[] GetVector2Array(string string_0)
	{
		List<Vector2> list = new List<Vector2>();
		GetValue(string_0, list, ArrayType.Vector2, 2, ConvertToVector2);
		return list.ToArray();
	}

	public static Vector2[] GetVector2Array(string string_0, Vector2 vector2_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetVector2Array(string_0);
		}
		Vector2[] array = new Vector2[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = vector2_0;
		}
		return array;
	}

	public static Vector3[] GetVector3Array(string string_0)
	{
		List<Vector3> list = new List<Vector3>();
		GetValue(string_0, list, ArrayType.Vector3, 3, ConvertToVector3);
		return list.ToArray();
	}

	public static Vector3[] GetVector3Array(string string_0, Vector3 vector3_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetVector3Array(string_0);
		}
		Vector3[] array = new Vector3[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = vector3_0;
		}
		return array;
	}

	public static Quaternion[] GetQuaternionArray(string string_0)
	{
		List<Quaternion> list = new List<Quaternion>();
		GetValue(string_0, list, ArrayType.Quaternion, 4, ConvertToQuaternion);
		return list.ToArray();
	}

	public static Quaternion[] GetQuaternionArray(string string_0, Quaternion quaternion_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetQuaternionArray(string_0);
		}
		Quaternion[] array = new Quaternion[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = quaternion_0;
		}
		return array;
	}

	public static Color[] GetColorArray(string string_0)
	{
		List<Color> list = new List<Color>();
		GetValue(string_0, list, ArrayType.Color, 4, ConvertToColor);
		return list.ToArray();
	}

	public static Color[] GetColorArray(string string_0, Color color_0, int int_3)
	{
		if (CryptoPlayerPrefs.HasKey(string_0))
		{
			return GetColorArray(string_0);
		}
		Color[] array = new Color[int_3];
		for (int i = 0; i < int_3; i++)
		{
			array[i] = color_0;
		}
		return array;
	}

	private static bool SetValue<T>(string string_0, T gparam_0, ArrayType arrayType_0, int int_3, Action<T, byte[], int> action_0) where T : IList
	{
		if (gparam_0.Count == 0)
		{
			Debug.LogError("The " + arrayType_0.ToString() + " array cannot have 0 entries when setting " + string_0);
			return false;
		}
		byte[] array = new byte[4 * gparam_0.Count * int_3 + 1];
		array[0] = Convert.ToByte(arrayType_0);
		Initialize();
		for (int i = 0; i < gparam_0.Count; i++)
		{
			action_0(gparam_0, array, i);
		}
		return SaveBytes(string_0, array);
	}

	private static void ConvertFromInt(int[] int_3, byte[] byte_1, int int_4)
	{
		ConvertInt32ToBytes(int_3[int_4], byte_1);
	}

	private static void ConvertFromFloat(float[] float_0, byte[] byte_1, int int_3)
	{
		ConvertFloatToBytes(float_0[int_3], byte_1);
	}

	private static void ConvertFromVector2(Vector2[] vector2_0, byte[] byte_1, int int_3)
	{
		ConvertFloatToBytes(vector2_0[int_3].x, byte_1);
		ConvertFloatToBytes(vector2_0[int_3].y, byte_1);
	}

	private static void ConvertFromVector3(Vector3[] vector3_0, byte[] byte_1, int int_3)
	{
		ConvertFloatToBytes(vector3_0[int_3].x, byte_1);
		ConvertFloatToBytes(vector3_0[int_3].y, byte_1);
		ConvertFloatToBytes(vector3_0[int_3].z, byte_1);
	}

	private static void ConvertFromQuaternion(Quaternion[] quaternion_0, byte[] byte_1, int int_3)
	{
		ConvertFloatToBytes(quaternion_0[int_3].x, byte_1);
		ConvertFloatToBytes(quaternion_0[int_3].y, byte_1);
		ConvertFloatToBytes(quaternion_0[int_3].z, byte_1);
		ConvertFloatToBytes(quaternion_0[int_3].w, byte_1);
	}

	private static void ConvertFromColor(Color[] color_0, byte[] byte_1, int int_3)
	{
		ConvertFloatToBytes(color_0[int_3].r, byte_1);
		ConvertFloatToBytes(color_0[int_3].g, byte_1);
		ConvertFloatToBytes(color_0[int_3].b, byte_1);
		ConvertFloatToBytes(color_0[int_3].a, byte_1);
	}

	private static void GetValue<T>(string string_0, T gparam_0, ArrayType arrayType_0, int int_3, Action<T, byte[]> action_0) where T : IList
	{
		if (!CryptoPlayerPrefs.HasKey(string_0))
		{
			return;
		}
		byte[] array = Convert.FromBase64String(CryptoPlayerPrefs.GetString(string_0, string.Empty));
		if ((array.Length - 1) % (int_3 * 4) != 0)
		{
			Debug.LogError("Corrupt preference file for " + string_0);
			return;
		}
		if ((ArrayType)array[0] != arrayType_0)
		{
			Debug.LogError(string_0 + " is not a " + arrayType_0.ToString() + " array");
			return;
		}
		Initialize();
		int num = (array.Length - 1) / (int_3 * 4);
		for (int i = 0; i < num; i++)
		{
			action_0(gparam_0, array);
		}
	}

	private static void ConvertToInt(List<int> list_0, byte[] byte_1)
	{
		list_0.Add(ConvertBytesToInt32(byte_1));
	}

	private static void ConvertToFloat(List<float> list_0, byte[] byte_1)
	{
		list_0.Add(ConvertBytesToFloat(byte_1));
	}

	private static void ConvertToVector2(List<Vector2> list_0, byte[] byte_1)
	{
		list_0.Add(new Vector2(ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1)));
	}

	private static void ConvertToVector3(List<Vector3> list_0, byte[] byte_1)
	{
		list_0.Add(new Vector3(ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1)));
	}

	private static void ConvertToQuaternion(List<Quaternion> list_0, byte[] byte_1)
	{
		list_0.Add(new Quaternion(ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1)));
	}

	private static void ConvertToColor(List<Color> list_0, byte[] byte_1)
	{
		list_0.Add(new Color(ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1), ConvertBytesToFloat(byte_1)));
	}

	public static void ShowArrayType(string string_0)
	{
		byte[] array = Convert.FromBase64String(CryptoPlayerPrefs.GetString(string_0, string.Empty));
		if (array.Length > 0)
		{
			ArrayType arrayType = (ArrayType)array[0];
			Debug.Log(string_0 + " is a " + arrayType.ToString() + " array");
		}
	}

	private static void Initialize()
	{
		if (BitConverter.IsLittleEndian)
		{
			int_0 = 0;
			int_1 = 0;
		}
		else
		{
			int_0 = 3;
			int_1 = 1;
		}
		if (byte_0 == null)
		{
			byte_0 = new byte[4];
		}
		int_2 = 1;
	}

	private static bool SaveBytes(string string_0, byte[] byte_1)
	{
		try
		{
			CryptoPlayerPrefs.SetString(string_0, Convert.ToBase64String(byte_1));
		}
		catch
		{
			return false;
		}
		return true;
	}

	private static void ConvertFloatToBytes(float float_0, byte[] byte_1)
	{
		byte_0 = BitConverter.GetBytes(float_0);
		ConvertTo4Bytes(byte_1);
	}

	private static float ConvertBytesToFloat(byte[] byte_1)
	{
		ConvertFrom4Bytes(byte_1);
		return BitConverter.ToSingle(byte_0, 0);
	}

	private static void ConvertInt32ToBytes(int int_3, byte[] byte_1)
	{
		byte_0 = BitConverter.GetBytes(int_3);
		ConvertTo4Bytes(byte_1);
	}

	private static int ConvertBytesToInt32(byte[] byte_1)
	{
		ConvertFrom4Bytes(byte_1);
		return BitConverter.ToInt32(byte_0, 0);
	}

	private static void ConvertTo4Bytes(byte[] byte_1)
	{
		byte_1[int_2] = byte_0[int_0];
		byte_1[int_2 + 1] = byte_0[1 + int_1];
		byte_1[int_2 + 2] = byte_0[2 - int_1];
		byte_1[int_2 + 3] = byte_0[3 - int_0];
		int_2 += 4;
	}

	private static void ConvertFrom4Bytes(byte[] byte_1)
	{
		byte_0[int_0] = byte_1[int_2];
		byte_0[1 + int_1] = byte_1[int_2 + 1];
		byte_0[2 - int_1] = byte_1[int_2 + 2];
		byte_0[3 - int_0] = byte_1[int_2 + 3];
		int_2 += 4;
	}
}
