using System;
using System.Text;
using CodeStage.AntiCheat.Utils;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	public static class ObscuredPrefs
	{
		private enum DataType : byte
		{
			Int = 5,
			UInt = 10,
			String = 15,
			Float = 20,
			Double = 25,
			Long = 30,
			Bool = 35,
			ByteArray = 40,
			Vector2 = 45,
			Vector3 = 50,
			Quaternion = 55,
			Color = 60,
			Rect = 65
		}

		public enum DeviceLockLevel : byte
		{
			None = 0,
			Soft = 1,
			Strict = 2
		}

		private const byte byte_0 = 2;

		private const string string_0 = "{not_found}";

		private const string string_1 = "|";

		private const char char_0 = ':';

		private static string string_2 = "e806f6";

		private static bool bool_0;

		private static string string_3;

		private static uint uint_0;

		public static Action action_0;

		public static bool bool_1;

		public static Action action_1;

		public static DeviceLockLevel deviceLockLevel_0;

		public static bool bool_2;

		public static bool bool_3;

		private static string string_4;

		public static string String_0
		{
			get
			{
				if (string.IsNullOrEmpty(string_3))
				{
					string_3 = GetDeviceID();
				}
				return string_3;
			}
			set
			{
				string_3 = value;
				uint_0 = CalculateChecksum(string_3);
			}
		}

		private static uint UInt32_0
		{
			get
			{
				if (uint_0 == 0)
				{
					uint_0 = CalculateChecksum(String_0);
				}
				return uint_0;
			}
		}

		private static string String_1
		{
			get
			{
				if (string.IsNullOrEmpty(string_4))
				{
					string_4 = DeprecatedCalculateChecksum(String_0);
				}
				return string_4;
			}
		}

		public static void ForceLockToDeviceInit()
		{
			if (string.IsNullOrEmpty(string_3))
			{
				string_3 = GetDeviceID();
				uint_0 = CalculateChecksum(string_3);
			}
			else
			{
				Debug.LogWarning("[ACTk] ObscuredPrefs.ForceLockToDeviceInit() is called, but device ID is already obtained!");
			}
		}

		public static void SetNewCryptoKey(string string_5)
		{
			string_2 = string_5;
			uint_0 = CalculateChecksum(string_3);
		}

		public static void SetInt(string string_5, int int_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptIntValue(string_5, int_0));
		}

		public static int GetInt(string string_5)
		{
			return GetInt(string_5, 0);
		}

		public static int GetInt(string string_5, int int_0)
		{
			string text = EncryptKey(string_5);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(string_5))
			{
				int @int = PlayerPrefs.GetInt(string_5, int_0);
				if (!bool_1)
				{
					SetInt(string_5, @int);
					PlayerPrefs.DeleteKey(string_5);
				}
				return @int;
			}
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, text);
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptIntValue(string_5, encryptedPrefsString, int_0) : int_0;
		}

		private static string EncryptIntValue(string string_5, int int_0)
		{
			byte[] bytes = BitConverter.GetBytes(int_0);
			return EncryptData(string_5, bytes, DataType.Int);
		}

		private static int DecryptIntValue(string string_5, string string_6, int int_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return int_0;
				}
				int result;
				int.TryParse(text, out result);
				SetInt(string_5, result);
				return result;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return int_0;
			}
			return BitConverter.ToInt32(array, 0);
		}

		public static void SetUInt(string string_5, uint uint_1)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptUIntValue(string_5, uint_1));
		}

		public static uint GetUInt(string string_5)
		{
			return GetUInt(string_5, 0u);
		}

		public static uint GetUInt(string string_5, uint uint_1)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptUIntValue(string_5, encryptedPrefsString, uint_1) : uint_1;
		}

		private static string EncryptUIntValue(string string_5, uint uint_1)
		{
			byte[] bytes = BitConverter.GetBytes(uint_1);
			return EncryptData(string_5, bytes, DataType.UInt);
		}

		private static uint DecryptUIntValue(string string_5, string string_6, uint uint_1)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return uint_1;
				}
				uint result;
				uint.TryParse(text, out result);
				SetUInt(string_5, result);
				return result;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return uint_1;
			}
			return BitConverter.ToUInt32(array, 0);
		}

		public static void SetString(string string_5, string string_6)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptStringValue(string_5, string_6));
		}

		public static string GetString(string string_5)
		{
			return GetString(string_5, string.Empty);
		}

		public static string GetString(string string_5, string string_6)
		{
			string text = EncryptKey(string_5);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(string_5))
			{
				string @string = PlayerPrefs.GetString(string_5, string_6);
				if (!bool_1)
				{
					SetString(string_5, @string);
					PlayerPrefs.DeleteKey(string_5);
				}
				return @string;
			}
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, text);
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptStringValue(string_5, encryptedPrefsString, string_6) : string_6;
		}

		private static string EncryptStringValue(string string_5, string string_6)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_6);
			return EncryptData(string_5, bytes, DataType.String);
		}

		private static string DecryptStringValue(string string_5, string string_6, string string_7)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return string_7;
				}
				SetString(string_5, text);
				return text;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return string_7;
			}
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		public static void SetFloat(string string_5, float float_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptFloatValue(string_5, float_0));
		}

		public static float GetFloat(string string_5)
		{
			return GetFloat(string_5, 0f);
		}

		public static float GetFloat(string string_5, float float_0)
		{
			string text = EncryptKey(string_5);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(string_5))
			{
				float @float = PlayerPrefs.GetFloat(string_5, float_0);
				if (!bool_1)
				{
					SetFloat(string_5, @float);
					PlayerPrefs.DeleteKey(string_5);
				}
				return @float;
			}
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, text);
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptFloatValue(string_5, encryptedPrefsString, float_0) : float_0;
		}

		private static string EncryptFloatValue(string string_5, float float_0)
		{
			byte[] bytes = BitConverter.GetBytes(float_0);
			return EncryptData(string_5, bytes, DataType.Float);
		}

		private static float DecryptFloatValue(string string_5, string string_6, float float_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return float_0;
				}
				float result;
				float.TryParse(text, out result);
				SetFloat(string_5, result);
				return result;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return float_0;
			}
			return BitConverter.ToSingle(array, 0);
		}

		public static void SetDouble(string string_5, double double_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptDoubleValue(string_5, double_0));
		}

		public static double GetDouble(string string_5)
		{
			return GetDouble(string_5, 0.0);
		}

		public static double GetDouble(string string_5, double double_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptDoubleValue(string_5, encryptedPrefsString, double_0) : double_0;
		}

		private static string EncryptDoubleValue(string string_5, double double_0)
		{
			byte[] bytes = BitConverter.GetBytes(double_0);
			return EncryptData(string_5, bytes, DataType.Double);
		}

		private static double DecryptDoubleValue(string string_5, string string_6, double double_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return double_0;
				}
				double result;
				double.TryParse(text, out result);
				SetDouble(string_5, result);
				return result;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return double_0;
			}
			return BitConverter.ToDouble(array, 0);
		}

		public static void SetLong(string string_5, long long_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptLongValue(string_5, long_0));
		}

		public static long GetLong(string string_5)
		{
			return GetLong(string_5, 0L);
		}

		public static long GetLong(string string_5, long long_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptLongValue(string_5, encryptedPrefsString, long_0) : long_0;
		}

		private static string EncryptLongValue(string string_5, long long_0)
		{
			byte[] bytes = BitConverter.GetBytes(long_0);
			return EncryptData(string_5, bytes, DataType.Long);
		}

		private static long DecryptLongValue(string string_5, string string_6, long long_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return long_0;
				}
				long result;
				long.TryParse(text, out result);
				SetLong(string_5, result);
				return result;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return long_0;
			}
			return BitConverter.ToInt64(array, 0);
		}

		public static void SetBool(string string_5, bool bool_4)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptBoolValue(string_5, bool_4));
		}

		public static bool GetBool(string string_5)
		{
			return GetBool(string_5, false);
		}

		public static bool GetBool(string string_5, bool bool_4)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptBoolValue(string_5, encryptedPrefsString, bool_4) : bool_4;
		}

		private static string EncryptBoolValue(string string_5, bool bool_4)
		{
			byte[] bytes = BitConverter.GetBytes(bool_4);
			return EncryptData(string_5, bytes, DataType.Bool);
		}

		private static bool DecryptBoolValue(string string_5, string string_6, bool bool_4)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return bool_4;
				}
				int result;
				int.TryParse(text, out result);
				SetBool(string_5, result == 1);
				return result == 1;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return bool_4;
			}
			return BitConverter.ToBoolean(array, 0);
		}

		public static void SetByteArray(string string_5, byte[] byte_1)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptByteArrayValue(string_5, byte_1));
		}

		public static byte[] GetByteArray(string string_5)
		{
			return GetByteArray(string_5, 0, 0);
		}

		public static byte[] GetByteArray(string string_5, byte byte_1, int int_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			if (encryptedPrefsString == "{not_found}")
			{
				return ConstructByteArray(byte_1, int_0);
			}
			return DecryptByteArrayValue(string_5, encryptedPrefsString, byte_1, int_0);
		}

		private static string EncryptByteArrayValue(string string_5, byte[] byte_1)
		{
			return EncryptData(string_5, byte_1, DataType.ByteArray);
		}

		private static byte[] DecryptByteArrayValue(string string_5, string string_6, byte byte_1, int int_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return ConstructByteArray(byte_1, int_0);
				}
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				SetByteArray(string_5, bytes);
				return bytes;
			}
			byte[] array = DecryptData(string_5, string_6);
			if (array == null)
			{
				return ConstructByteArray(byte_1, int_0);
			}
			return array;
		}

		private static byte[] ConstructByteArray(byte byte_1, int int_0)
		{
			byte[] array = new byte[int_0];
			for (int i = 0; i < int_0; i++)
			{
				array[i] = byte_1;
			}
			return array;
		}

		public static void SetVector2(string string_5, Vector2 vector2_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptVector2Value(string_5, vector2_0));
		}

		public static Vector2 GetVector2(string string_5)
		{
			return GetVector2(string_5, Vector2.zero);
		}

		public static Vector2 GetVector2(string string_5, Vector2 vector2_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptVector2Value(string_5, encryptedPrefsString, vector2_0) : vector2_0;
		}

		private static string EncryptVector2Value(string string_5, Vector2 vector2_0)
		{
			byte[] array = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(vector2_0.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector2_0.y), 0, array, 4, 4);
			return EncryptData(string_5, array, DataType.Vector2);
		}

		private static Vector2 DecryptVector2Value(string string_5, string string_6, Vector2 vector2_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return vector2_0;
				}
				string[] array = text.Split("|"[0]);
				float result;
				float.TryParse(array[0], out result);
				float result2;
				float.TryParse(array[1], out result2);
				Vector2 vector = new Vector2(result, result2);
				SetVector2(string_5, vector);
				return vector;
			}
			byte[] array2 = DecryptData(string_5, string_6);
			if (array2 == null)
			{
				return vector2_0;
			}
			Vector2 result3 = default(Vector2);
			result3.x = BitConverter.ToSingle(array2, 0);
			result3.y = BitConverter.ToSingle(array2, 4);
			return result3;
		}

		public static void SetVector3(string string_5, Vector3 vector3_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptVector3Value(string_5, vector3_0));
		}

		public static Vector3 GetVector3(string string_5)
		{
			return GetVector3(string_5, Vector3.zero);
		}

		public static Vector3 GetVector3(string string_5, Vector3 vector3_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptVector3Value(string_5, encryptedPrefsString, vector3_0) : vector3_0;
		}

		private static string EncryptVector3Value(string string_5, Vector3 vector3_0)
		{
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(vector3_0.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3_0.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3_0.z), 0, array, 8, 4);
			return EncryptData(string_5, array, DataType.Vector3);
		}

		private static Vector3 DecryptVector3Value(string string_5, string string_6, Vector3 vector3_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return vector3_0;
				}
				string[] array = text.Split("|"[0]);
				float result;
				float.TryParse(array[0], out result);
				float result2;
				float.TryParse(array[1], out result2);
				float result3;
				float.TryParse(array[2], out result3);
				Vector3 vector = new Vector3(result, result2, result3);
				SetVector3(string_5, vector);
				return vector;
			}
			byte[] array2 = DecryptData(string_5, string_6);
			if (array2 == null)
			{
				return vector3_0;
			}
			Vector3 result4 = default(Vector3);
			result4.x = BitConverter.ToSingle(array2, 0);
			result4.y = BitConverter.ToSingle(array2, 4);
			result4.z = BitConverter.ToSingle(array2, 8);
			return result4;
		}

		public static void SetQuaternion(string string_5, Quaternion quaternion_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptQuaternionValue(string_5, quaternion_0));
		}

		public static Quaternion GetQuaternion(string string_5)
		{
			return GetQuaternion(string_5, Quaternion.identity);
		}

		public static Quaternion GetQuaternion(string string_5, Quaternion quaternion_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptQuaternionValue(string_5, encryptedPrefsString, quaternion_0) : quaternion_0;
		}

		private static string EncryptQuaternionValue(string string_5, Quaternion quaternion_0)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(BitConverter.GetBytes(quaternion_0.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(quaternion_0.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(quaternion_0.z), 0, array, 8, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(quaternion_0.w), 0, array, 12, 4);
			return EncryptData(string_5, array, DataType.Quaternion);
		}

		private static Quaternion DecryptQuaternionValue(string string_5, string string_6, Quaternion quaternion_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return quaternion_0;
				}
				string[] array = text.Split("|"[0]);
				float result;
				float.TryParse(array[0], out result);
				float result2;
				float.TryParse(array[1], out result2);
				float result3;
				float.TryParse(array[2], out result3);
				float result4;
				float.TryParse(array[3], out result4);
				Quaternion quaternion = new Quaternion(result, result2, result3, result4);
				SetQuaternion(string_5, quaternion);
				return quaternion;
			}
			byte[] array2 = DecryptData(string_5, string_6);
			if (array2 == null)
			{
				return quaternion_0;
			}
			Quaternion result5 = default(Quaternion);
			result5.x = BitConverter.ToSingle(array2, 0);
			result5.y = BitConverter.ToSingle(array2, 4);
			result5.z = BitConverter.ToSingle(array2, 8);
			result5.w = BitConverter.ToSingle(array2, 12);
			return result5;
		}

		public static void SetColor(string string_5, Color32 color32_0)
		{
			uint uint_ = (uint)((color32_0.a << 24) | (color32_0.r << 16) | (color32_0.g << 8) | color32_0.b);
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptColorValue(string_5, uint_));
		}

		public static Color32 GetColor(string string_5)
		{
			return GetColor(string_5, new Color32(0, 0, 0, 1));
		}

		public static Color32 GetColor(string string_5, Color32 color32_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			if (encryptedPrefsString == "{not_found}")
			{
				return color32_0;
			}
			uint num = DecryptUIntValue(string_5, encryptedPrefsString, 16777216u);
			byte a = (byte)(num >> 24);
			byte r = (byte)(num >> 16);
			byte g = (byte)(num >> 8);
			byte b = (byte)num;
			return new Color32(r, g, b, a);
		}

		private static string EncryptColorValue(string string_5, uint uint_1)
		{
			byte[] bytes = BitConverter.GetBytes(uint_1);
			return EncryptData(string_5, bytes, DataType.Color);
		}

		public static void SetRect(string string_5, Rect rect_0)
		{
			PlayerPrefs.SetString(EncryptKey(string_5), EncryptRectValue(string_5, rect_0));
		}

		public static Rect GetRect(string string_5)
		{
			return GetRect(string_5, new Rect(0f, 0f, 0f, 0f));
		}

		public static Rect GetRect(string string_5, Rect rect_0)
		{
			string encryptedPrefsString = GetEncryptedPrefsString(string_5, EncryptKey(string_5));
			return (!(encryptedPrefsString == "{not_found}")) ? DecryptRectValue(string_5, encryptedPrefsString, rect_0) : rect_0;
		}

		private static string EncryptRectValue(string string_5, Rect rect_0)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(BitConverter.GetBytes(rect_0.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(rect_0.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(rect_0.width), 0, array, 8, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(rect_0.height), 0, array, 12, 4);
			return EncryptData(string_5, array, DataType.Rect);
		}

		private static Rect DecryptRectValue(string string_5, string string_6, Rect rect_0)
		{
			if (string_6.IndexOf(':') > -1)
			{
				string text = DeprecatedDecryptValue(string_6);
				if (text == string.Empty)
				{
					return rect_0;
				}
				string[] array = text.Split("|"[0]);
				float result;
				float.TryParse(array[0], out result);
				float result2;
				float.TryParse(array[1], out result2);
				float result3;
				float.TryParse(array[2], out result3);
				float result4;
				float.TryParse(array[3], out result4);
				Rect rect = new Rect(result, result2, result3, result4);
				SetRect(string_5, rect);
				return rect;
			}
			byte[] array2 = DecryptData(string_5, string_6);
			if (array2 == null)
			{
				return rect_0;
			}
			Rect result5 = default(Rect);
			result5.x = BitConverter.ToSingle(array2, 0);
			result5.y = BitConverter.ToSingle(array2, 4);
			result5.width = BitConverter.ToSingle(array2, 8);
			result5.height = BitConverter.ToSingle(array2, 12);
			return result5;
		}

		public static bool HasKey(string string_5)
		{
			return PlayerPrefs.HasKey(string_5) || PlayerPrefs.HasKey(EncryptKey(string_5));
		}

		public static void DeleteKey(string string_5)
		{
			PlayerPrefs.DeleteKey(EncryptKey(string_5));
			PlayerPrefs.DeleteKey(string_5);
		}

		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		public static void Save()
		{
			PlayerPrefs.Save();
		}

		private static string GetEncryptedPrefsString(string string_5, string string_6)
		{
			string @string = PlayerPrefs.GetString(string_6, "{not_found}");
			if (@string == "{not_found}" && PlayerPrefs.HasKey(string_5))
			{
				Debug.LogWarning("[ACTk] Are you trying to read regular PlayerPrefs data using ObscuredPrefs (key = " + string_5 + ")?");
			}
			return @string;
		}

		private static string EncryptKey(string string_5)
		{
			string_5 = ObscuredString.EncryptDecrypt(string_5, string_2);
			string_5 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string_5));
			return string_5;
		}

		private static string EncryptData(string string_5, byte[] byte_1, DataType dataType_0)
		{
			int num = byte_1.Length;
			byte[] src = EncryptDecryptBytes(byte_1, num, string_5 + string_2);
			uint num2 = xxHash.CalculateHash(byte_1, num, 0u);
			byte[] src2 = new byte[4]
			{
				(byte)(num2 & 0xFFu),
				(byte)((num2 >> 8) & 0xFFu),
				(byte)((num2 >> 16) & 0xFFu),
				(byte)((num2 >> 24) & 0xFFu)
			};
			byte[] array = null;
			int num3;
			if (deviceLockLevel_0 != 0)
			{
				num3 = num + 11;
				uint uInt32_ = UInt32_0;
				array = new byte[4]
				{
					(byte)(uInt32_ & 0xFFu),
					(byte)((uInt32_ >> 8) & 0xFFu),
					(byte)((uInt32_ >> 16) & 0xFFu),
					(byte)((uInt32_ >> 24) & 0xFFu)
				};
			}
			else
			{
				num3 = num + 7;
			}
			byte[] array2 = new byte[num3];
			Buffer.BlockCopy(src, 0, array2, 0, num);
			if (array != null)
			{
				Buffer.BlockCopy(array, 0, array2, num, 4);
			}
			array2[num3 - 7] = (byte)dataType_0;
			array2[num3 - 6] = 2;
			array2[num3 - 5] = (byte)deviceLockLevel_0;
			Buffer.BlockCopy(src2, 0, array2, num3 - 4, 4);
			return Convert.ToBase64String(array2);
		}

		private static byte[] DecryptData(string string_5, string string_6)
		{
			byte[] array;
			try
			{
				array = Convert.FromBase64String(string_6);
			}
			catch (Exception)
			{
				SavesTampered();
				return null;
			}
			if (array.Length <= 0)
			{
				SavesTampered();
				return null;
			}
			int num = array.Length;
			byte b = array[num - 6];
			if (b != 2)
			{
				SavesTampered();
				return null;
			}
			DeviceLockLevel deviceLockLevel = (DeviceLockLevel)array[num - 5];
			byte[] array2 = new byte[4];
			Buffer.BlockCopy(array, num - 4, array2, 0, 4);
			uint num2 = (uint)(array2[0] | (array2[1] << 8) | (array2[2] << 16) | (array2[3] << 24));
			int num3 = 0;
			uint num4 = 0u;
			if (deviceLockLevel != 0)
			{
				num3 = num - 11;
				if (deviceLockLevel_0 != 0)
				{
					byte[] array3 = new byte[4];
					Buffer.BlockCopy(array, num3, array3, 0, 4);
					num4 = (uint)(array3[0] | (array3[1] << 8) | (array3[2] << 16) | (array3[3] << 24));
				}
			}
			else
			{
				num3 = num - 7;
			}
			byte[] array4 = new byte[num3];
			Buffer.BlockCopy(array, 0, array4, 0, num3);
			byte[] result = EncryptDecryptBytes(array4, num3, string_5 + string_2);
			uint num5 = xxHash.CalculateHash(result, num3, 0u);
			if (num5 != num2)
			{
				SavesTampered();
				return null;
			}
			if (deviceLockLevel_0 == DeviceLockLevel.Strict && num4 == 0 && !bool_3 && !bool_2)
			{
				return null;
			}
			if (num4 != 0 && !bool_3)
			{
				uint uInt32_ = UInt32_0;
				if (num4 != uInt32_)
				{
					PossibleForeignSavesDetected();
					if (!bool_2)
					{
						return null;
					}
				}
			}
			return result;
		}

		private static uint CalculateChecksum(string string_5)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_5 + string_2);
			return xxHash.CalculateHash(bytes, bytes.Length, 0u);
		}

		private static void SavesTampered()
		{
			if (action_0 != null)
			{
				action_0();
				action_0 = null;
			}
		}

		private static void PossibleForeignSavesDetected()
		{
			if (action_1 != null && !bool_0)
			{
				bool_0 = true;
				action_1();
			}
		}

		private static string GetDeviceID()
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				text = SystemInfo.deviceUniqueIdentifier;
			}
			return text;
		}

		private static byte[] EncryptDecryptBytes(byte[] byte_1, int int_0, string string_5)
		{
			int length = string_5.Length;
			byte[] array = new byte[int_0];
			for (int i = 0; i < int_0; i++)
			{
				array[i] = (byte)(byte_1[i] ^ string_5[i % length]);
			}
			return array;
		}

		private static string DeprecatedDecryptValue(string string_5)
		{
			string[] array = string_5.Split(':');
			if (array.Length < 2)
			{
				SavesTampered();
				return string.Empty;
			}
			string text = array[0];
			string text2 = array[1];
			byte[] array2;
			try
			{
				array2 = Convert.FromBase64String(text);
			}
			catch
			{
				SavesTampered();
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(array2, 0, array2.Length);
			string result = ObscuredString.EncryptDecrypt(@string, string_2);
			if (array.Length == 3)
			{
				if (text2 != DeprecatedCalculateChecksum(text + String_1))
				{
					SavesTampered();
				}
			}
			else if (array.Length == 2)
			{
				if (text2 != DeprecatedCalculateChecksum(text))
				{
					SavesTampered();
				}
			}
			else
			{
				SavesTampered();
			}
			if (deviceLockLevel_0 != 0 && !bool_3)
			{
				if (array.Length >= 3)
				{
					string text3 = array[2];
					if (text3 != String_1)
					{
						if (!bool_2)
						{
							result = string.Empty;
						}
						PossibleForeignSavesDetected();
					}
				}
				else if (deviceLockLevel_0 == DeviceLockLevel.Strict)
				{
					if (!bool_2)
					{
						result = string.Empty;
					}
					PossibleForeignSavesDetected();
				}
				else if (text2 != DeprecatedCalculateChecksum(text))
				{
					if (!bool_2)
					{
						result = string.Empty;
					}
					PossibleForeignSavesDetected();
				}
			}
			return result;
		}

		private static string DeprecatedCalculateChecksum(string string_5)
		{
			int num = 0;
			byte[] bytes = Encoding.UTF8.GetBytes(string_5 + string_2);
			int num2 = bytes.Length;
			int num3 = string_2.Length ^ 0x40;
			for (int i = 0; i < num2; i++)
			{
				byte b = bytes[i];
				num += b + b * (i + num3) % 3;
			}
			return num.ToString("X2");
		}
	}
}
