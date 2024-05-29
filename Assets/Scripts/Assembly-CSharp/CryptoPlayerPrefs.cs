using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class CryptoPlayerPrefs
{
	private static Dictionary<string, string> dictionary_0;

	private static Dictionary<string, int> dictionary_1;

	private static Dictionary<string, SymmetricAlgorithm> dictionary_2;

	private static int int_0 = int.MaxValue;

	private static bool bool_0 = true;

	private static bool bool_1 = true;

	public static bool HasKey(string string_0)
	{
		string key = hashedKey(string_0);
		return PlayerPrefs.HasKey(key);
	}

	public static void DeleteKey(string string_0)
	{
		string key = hashedKey(string_0);
		PlayerPrefs.DeleteKey(key);
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	public static void Save()
	{
		PlayerPrefs.Save();
	}

	public static void SetInt(string string_0, int int_1)
	{
		string text = hashedKey(string_0);
		int num = int_1;
		if (bool_1)
		{
			int num2 = computeXorOperand(string_0, text);
			int num3 = computePlusOperand(num2);
			num = (int_1 + num3) ^ num2;
		}
		if (bool_0)
		{
			PlayerPrefs.SetString(text, encrypt(text, string.Empty + num));
		}
		else
		{
			PlayerPrefs.SetInt(text, num);
		}
	}

	public static void SetLong(string string_0, long long_0)
	{
		SetString(string_0, long_0.ToString());
	}

	public static void SetString(string string_0, string string_1)
	{
		string text = hashedKey(string_0);
		string text2 = string_1;
		if (bool_1)
		{
			int num = computeXorOperand(string_0, text);
			int num2 = computePlusOperand(num);
			text2 = string.Empty;
			foreach (char c in string_1)
			{
				char c2 = (char)((c + num2) ^ num);
				text2 += c2;
			}
		}
		if (bool_0)
		{
			PlayerPrefs.SetString(text, encrypt(text, text2));
		}
		else
		{
			PlayerPrefs.SetString(text, text2);
		}
	}

	public static void SetFloat(string string_0, float float_0)
	{
		SetString(string_0, float_0.ToString());
	}

	public static int GetInt(string string_0, int int_1 = 0)
	{
		string text = hashedKey(string_0);
		if (!PlayerPrefs.HasKey(text))
		{
			return int_1;
		}
		int num = ((!bool_0) ? PlayerPrefs.GetInt(text) : int.Parse(decrypt(text)));
		int result = num;
		if (bool_1)
		{
			int num2 = computeXorOperand(string_0, text);
			int num3 = computePlusOperand(num2);
			result = (num2 ^ num) - num3;
		}
		return result;
	}

	public static long GetLong(string string_0, long long_0 = 0)
	{
		return long.Parse(GetString(string_0, long_0.ToString()));
	}

	public static string GetString(string string_0, string string_1 = "")
	{
		string text = hashedKey(string_0);
		if (!PlayerPrefs.HasKey(text))
		{
			return string_1;
		}
		string text2 = ((!bool_0) ? PlayerPrefs.GetString(text) : decrypt(text));
		string text3 = text2;
		if (bool_1)
		{
			int num = computeXorOperand(string_0, text);
			int num2 = computePlusOperand(num);
			text3 = string.Empty;
			string text4 = text2;
			foreach (char c in text4)
			{
				char c2 = (char)((num ^ c) - num2);
				text3 += c2;
			}
		}
		return text3;
	}

	public static float GetFloat(string string_0, float float_0 = 0f)
	{
		return float.Parse(GetString(string_0, float_0.ToString()));
	}

	private static string encrypt(string string_0, string string_1)
	{
		return EncryptString(string_1, getEncryptionPassword(string_0));
	}

	private static string decrypt(string string_0)
	{
		return DecryptString(PlayerPrefs.GetString(string_0), getEncryptionPassword(string_0));
	}

	private static string hashedKey(string string_0)
	{
		if (dictionary_0 == null)
		{
			dictionary_0 = new Dictionary<string, string>();
		}
		if (dictionary_0.ContainsKey(string_0))
		{
			return dictionary_0[string_0];
		}
		string text = Md5Sum(string_0);
		dictionary_0.Add(string_0, text);
		return text;
	}

	private static int computeXorOperand(string string_0, string string_1)
	{
		if (dictionary_1 == null)
		{
			dictionary_1 = new Dictionary<string, int>();
		}
		if (dictionary_1.ContainsKey(string_0))
		{
			return dictionary_1[string_0];
		}
		int num = 0;
		foreach (char c in string_1)
		{
			num += c;
		}
		num += int_0;
		dictionary_1.Add(string_0, num);
		return num;
	}

	private static int computePlusOperand(int int_1)
	{
		return int_1 & (int_1 << 1);
	}

	public static string Md5Sum(string string_0)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(string_0);
		HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
		byte[] array = hashAlgorithm.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	private static byte[] EncryptString(byte[] byte_0, SymmetricAlgorithm symmetricAlgorithm_0)
	{
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm_0.CreateEncryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(byte_0, 0, byte_0.Length);
		cryptoStream.Close();
		return memoryStream.ToArray();
	}

	private static string EncryptString(string string_0, string string_1)
	{
		SymmetricAlgorithm rijndaelForKey = getRijndaelForKey(string_1);
		byte[] bytes = Encoding.Unicode.GetBytes(string_0);
		byte[] inArray = EncryptString(bytes, rijndaelForKey);
		return Convert.ToBase64String(inArray);
	}

	private static byte[] DecryptString(byte[] byte_0, SymmetricAlgorithm symmetricAlgorithm_0)
	{
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm_0.CreateDecryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(byte_0, 0, byte_0.Length);
		cryptoStream.Close();
		return memoryStream.ToArray();
	}

	private static string DecryptString(string string_0, string string_1)
	{
		if (dictionary_2 == null)
		{
			dictionary_2 = new Dictionary<string, SymmetricAlgorithm>();
		}
		byte[] byte_ = Convert.FromBase64String(string_0);
		SymmetricAlgorithm rijndaelForKey = getRijndaelForKey(string_1);
		byte[] array = DecryptString(byte_, rijndaelForKey);
		return Encoding.Unicode.GetString(array, 0, array.Length);
	}

	private static SymmetricAlgorithm getRijndaelForKey(string string_0)
	{
		if (dictionary_2 == null)
		{
			dictionary_2 = new Dictionary<string, SymmetricAlgorithm>();
		}
		SymmetricAlgorithm symmetricAlgorithm;
		if (dictionary_2.ContainsKey(string_0))
		{
			symmetricAlgorithm = dictionary_2[string_0];
		}
		else
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(string_0, new byte[13]
			{
				73, 97, 110, 32, 77, 100, 118, 101, 101, 100,
				101, 118, 118
			});
			symmetricAlgorithm = Rijndael.Create();
			symmetricAlgorithm.Key = rfc2898DeriveBytes.GetBytes(32);
			symmetricAlgorithm.IV = rfc2898DeriveBytes.GetBytes(16);
			dictionary_2.Add(string_0, symmetricAlgorithm);
		}
		return symmetricAlgorithm;
	}

	private static string getEncryptionPassword(string string_0)
	{
		return Md5Sum(string_0 + int_0);
	}

	private static bool test(bool bool_2, bool bool_3)
	{
		bool flag = true;
		bool bool_4 = bool_0;
		bool bool_5 = bool_1;
		useRijndael(bool_2);
		useXor(bool_3);
		int num = 0;
		string text = "cryptotest_int";
		string text2 = hashedKey(text);
		SetInt(text, 0);
		int @int = GetInt(text);
		bool flag2 = 0 == @int;
		flag = true && flag2;
		Debug.Log("INT Bordertest Zero: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @int + ")");
		num = int.MaxValue;
		text = "cryptotest_intmax";
		text2 = hashedKey(text);
		SetInt(text, int.MaxValue);
		@int = GetInt(text);
		flag2 = int.MaxValue == @int;
		flag = flag && flag2;
		Debug.Log("INT Bordertest Max: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @int + ")");
		num = int.MinValue;
		text = "cryptotest_intmin";
		text2 = hashedKey(text);
		SetInt(text, int.MinValue);
		@int = GetInt(text);
		flag2 = int.MinValue == @int;
		flag = flag && flag2;
		Debug.Log("INT Bordertest Min: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @int + ")");
		text = "cryptotest_intrand";
		text2 = hashedKey(text);
		bool flag3 = true;
		for (int i = 0; i < 100; i++)
		{
			int num2 = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			num = num2;
			SetInt(text, num);
			@int = GetInt(text);
			flag2 = num == @int;
			flag3 = flag3 && flag2;
			flag = flag && flag2;
		}
		Debug.Log("INT Test Random: " + ((!flag3) ? "fail" : "ok"));
		float num3 = 0f;
		text = "cryptotest_float";
		text2 = hashedKey(text);
		SetFloat(text, num3);
		float @float = GetFloat(text, 0f);
		flag2 = num3.ToString().Equals(@float.ToString());
		flag = flag && flag2;
		Debug.Log("FLOAT Bordertest Zero: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num3 + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @float + ")");
		num3 = float.MaxValue;
		text = "cryptotest_floatmax";
		text2 = hashedKey(text);
		SetFloat(text, num3);
		@float = GetFloat(text, 0f);
		flag2 = num3.ToString().Equals(@float.ToString());
		flag = flag && flag2;
		Debug.Log("FLOAT Bordertest Max: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num3 + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @float + ")");
		num3 = float.MinValue;
		text = "cryptotest_floatmin";
		text2 = hashedKey(text);
		SetFloat(text, num3);
		@float = GetFloat(text, 0f);
		flag2 = num3.ToString().Equals(@float.ToString());
		flag = flag && flag2;
		Debug.Log("FLOAT Bordertest Min: " + ((!flag2) ? "fail" : "ok"));
		Debug.Log("(Key: " + text + "; Crypted Key: " + text2 + "; Input value: " + num3 + "; Saved value: " + PlayerPrefs.GetString(text2) + "; Return value: " + @float + ")");
		text = "cryptotest_floatrand";
		text2 = hashedKey(text);
		flag3 = true;
		for (int j = 0; j < 100; j++)
		{
			float num4 = (float)UnityEngine.Random.Range(int.MinValue, int.MaxValue) * UnityEngine.Random.value;
			num3 = num4;
			SetFloat(text, num3);
			@float = GetFloat(text, 0f);
			flag2 = num3.ToString().Equals(@float.ToString());
			flag3 = flag3 && flag2;
			flag = flag && flag2;
		}
		Debug.Log("FLOAT Test Random: " + ((!flag3) ? "fail" : "ok"));
		DeleteKey("cryptotest_int");
		DeleteKey("cryptotest_intmax");
		DeleteKey("cryptotest_intmin");
		DeleteKey("cryptotest_intrandom");
		DeleteKey("cryptotest_float");
		DeleteKey("cryptotest_floatmax");
		DeleteKey("cryptotest_floatmin");
		DeleteKey("cryptotest_floatrandom");
		useRijndael(bool_4);
		useXor(bool_5);
		return flag;
	}

	public static bool test()
	{
		bool flag = test(true, true);
		bool flag2 = test(true, false);
		bool flag3 = test(false, true);
		bool flag4 = test(false, false);
		return flag && flag2 && flag3 && flag4;
	}

	public static void setSalt(int int_1)
	{
		int_0 = int_1;
	}

	public static void useRijndael(bool bool_2)
	{
		bool_0 = bool_2;
	}

	public static void useXor(bool bool_2)
	{
		bool_1 = bool_2;
	}
}
