using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public sealed class ObscuredString
	{
		private static string string_0 = "4441";

		[SerializeField]
		private string string_1;

		[SerializeField]
		private byte[] byte_0;

		[SerializeField]
		private string string_2;

		[SerializeField]
		private bool bool_0;

		private ObscuredString()
		{
		}

		private ObscuredString(byte[] byte_1)
		{
			string_1 = string_0;
			byte_0 = byte_1;
			string_2 = null;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(string string_3)
		{
			string_0 = string_3;
		}

		public void ApplyNewCryptoKey()
		{
			if (string_1 != string_0)
			{
				byte_0 = InternalEncrypt(InternalDecrypt());
				string_1 = string_0;
			}
		}

		public static string EncryptDecrypt(string string_3)
		{
			return EncryptDecrypt(string_3, string.Empty);
		}

		public static string EncryptDecrypt(string string_3, string string_4)
		{
			if (string.IsNullOrEmpty(string_3))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(string_4))
			{
				string_4 = string_0;
			}
			int length = string_4.Length;
			int length2 = string_3.Length;
			char[] array = new char[length2];
			for (int i = 0; i < length2; i++)
			{
				array[i] = (char)(string_3[i] ^ string_4[i % length]);
			}
			return new string(array);
		}

		public string GetEncrypted()
		{
			ApplyNewCryptoKey();
			return GetString(byte_0);
		}

		public void SetEncrypted(string string_3)
		{
			bool_0 = true;
			byte_0 = GetBytes(string_3);
			if (ObscuredCheatingDetector.bool_1)
			{
				string_2 = InternalDecrypt();
			}
		}

		private static byte[] InternalEncrypt(string string_3)
		{
			return GetBytes(EncryptDecrypt(string_3, string_0));
		}

		private string InternalDecrypt()
		{
			if (!bool_0)
			{
				string_1 = string_0;
				byte_0 = InternalEncrypt(string.Empty);
				string_2 = string.Empty;
				bool_0 = true;
			}
			string text = string_0;
			if (string_1 != string_0)
			{
				text = string_1;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = string_0;
			}
			string text2 = EncryptDecrypt(GetString(byte_0), text);
			if (ObscuredCheatingDetector.bool_1 && !string.IsNullOrEmpty(string_2) && text2 != string_2)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return text2;
		}

		public override string ToString()
		{
			return InternalDecrypt();
		}

		public override bool Equals(object obj)
		{
			ObscuredString obscuredString = obj as ObscuredString;
			string objB = null;
			if (obscuredString != null)
			{
				objB = GetString(obscuredString.byte_0);
			}
			return object.Equals(byte_0, objB);
		}

		public bool Equals(ObscuredString obscuredString_0)
		{
			byte[] byte_ = null;
			if (obscuredString_0 != null)
			{
				byte_ = obscuredString_0.byte_0;
			}
			return ArraysEquals(byte_0, byte_);
		}

		public bool Equals(ObscuredString obscuredString_0, StringComparison stringComparison_0)
		{
			string b = null;
			if (obscuredString_0 != null)
			{
				b = obscuredString_0.InternalDecrypt();
			}
			return string.Equals(InternalDecrypt(), b, stringComparison_0);
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		private static byte[] GetBytes(string string_3)
		{
			byte[] array = new byte[string_3.Length * 2];
			Buffer.BlockCopy(string_3.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		private static string GetString(byte[] byte_1)
		{
			char[] array = new char[byte_1.Length / 2];
			Buffer.BlockCopy(byte_1, 0, array, 0, byte_1.Length);
			return new string(array);
		}

		private static bool ArraysEquals(byte[] byte_1, byte[] byte_2)
		{
			if (byte_1 == byte_2)
			{
				return true;
			}
			if (byte_1 != null && byte_2 != null)
			{
				if (byte_1.Length != byte_2.Length)
				{
					return false;
				}
				int num = 0;
				while (true)
				{
					if (num < byte_1.Length)
					{
						if (byte_1[num] != byte_2[num])
						{
							break;
						}
						num++;
						continue;
					}
					return true;
				}
				return false;
			}
			return false;
		}

		public static implicit operator ObscuredString(string string_3)
		{
			if (string_3 == null)
			{
				return null;
			}
			ObscuredString obscuredString = new ObscuredString(InternalEncrypt(string_3));
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredString.string_2 = string_3;
			}
			return obscuredString;
		}

		public static implicit operator string(ObscuredString obscuredString_0)
		{
			if (obscuredString_0 == null)
			{
				return null;
			}
			return obscuredString_0.InternalDecrypt();
		}

		public static bool operator ==(ObscuredString obscuredString_0, ObscuredString obscuredString_1)
		{
			if (object.ReferenceEquals(obscuredString_0, obscuredString_1))
			{
				return true;
			}
			if ((object)obscuredString_0 != null && (object)obscuredString_1 != null)
			{
				return ArraysEquals(obscuredString_0.byte_0, obscuredString_1.byte_0);
			}
			return false;
		}

		public static bool operator !=(ObscuredString obscuredString_0, ObscuredString obscuredString_1)
		{
			return !(obscuredString_0 == obscuredString_1);
		}
	}
}
