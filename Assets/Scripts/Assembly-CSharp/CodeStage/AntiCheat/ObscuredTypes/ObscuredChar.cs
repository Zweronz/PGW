using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredChar : IEquatable<ObscuredChar>
	{
		private static char char_0 = '—';

		private char char_1;

		private char char_2;

		private char char_3;

		private bool bool_0;

		private ObscuredChar(char char_4)
		{
			char_1 = char_0;
			char_2 = char_4;
			char_3 = '\0';
			bool_0 = true;
		}

		public static void SetNewCryptoKey(char char_4)
		{
			char_0 = char_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (char_1 != char_0)
			{
				char_2 = EncryptDecrypt(InternalDecrypt(), char_0);
				char_1 = char_0;
			}
		}

		public static char EncryptDecrypt(char char_4)
		{
			return EncryptDecrypt(char_4, '\0');
		}

		public static char EncryptDecrypt(char char_4, char char_5)
		{
			if (char_5 == '\0')
			{
				return (char)(char_4 ^ char_0);
			}
			return (char)(char_4 ^ char_5);
		}

		public char GetEncrypted()
		{
			ApplyNewCryptoKey();
			return char_2;
		}

		public void SetEncrypted(char char_4)
		{
			bool_0 = true;
			char_2 = char_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				char_3 = InternalDecrypt();
			}
		}

		private char InternalDecrypt()
		{
			if (!bool_0)
			{
				char_1 = char_0;
				char_2 = EncryptDecrypt('\0');
				char_3 = '\0';
				bool_0 = true;
			}
			char char_ = char_0;
			if (char_1 != char_0)
			{
				char_ = char_1;
			}
			char c = EncryptDecrypt(char_2, char_);
			if (ObscuredCheatingDetector.bool_1 && char_3 != 0 && c != char_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return c;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredChar))
			{
				return false;
			}
			ObscuredChar obscuredChar = (ObscuredChar)obj;
			return char_2 == obscuredChar.char_2;
		}

		public bool Equals(ObscuredChar other)
		{
			return char_2 == other.char_2;
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public string ToString(IFormatProvider iformatProvider_0)
		{
			return InternalDecrypt().ToString(iformatProvider_0);
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public static implicit operator ObscuredChar(char char_4)
		{
			ObscuredChar result = new ObscuredChar(EncryptDecrypt(char_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.char_3 = char_4;
			}
			return result;
		}

		public static implicit operator char(ObscuredChar obscuredChar_0)
		{
			return obscuredChar_0.InternalDecrypt();
		}

		public static ObscuredChar operator ++(ObscuredChar obscuredChar_0)
		{
			char char_ = (char)(obscuredChar_0.InternalDecrypt() + 1);
			obscuredChar_0.char_2 = EncryptDecrypt(char_, obscuredChar_0.char_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredChar_0.char_3 = char_;
			}
			return obscuredChar_0;
		}

		public static ObscuredChar operator --(ObscuredChar obscuredChar_0)
		{
			char char_ = (char)(obscuredChar_0.InternalDecrypt() - 1);
			obscuredChar_0.char_2 = EncryptDecrypt(char_, obscuredChar_0.char_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredChar_0.char_3 = char_;
			}
			return obscuredChar_0;
		}
	}
}
