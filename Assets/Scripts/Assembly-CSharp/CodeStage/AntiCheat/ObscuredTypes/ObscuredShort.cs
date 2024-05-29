using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredShort : IEquatable<ObscuredShort>, IFormattable
	{
		private static short short_0 = 214;

		private short short_1;

		private short short_2;

		private short short_3;

		private bool bool_0;

		private ObscuredShort(short short_4)
		{
			short_1 = short_0;
			short_2 = short_4;
			short_3 = 0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(short short_4)
		{
			short_0 = short_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (short_1 != short_0)
			{
				short_2 = EncryptDecrypt(InternalDecrypt(), short_0);
				short_1 = short_0;
			}
		}

		public static short EncryptDecrypt(short short_4)
		{
			return EncryptDecrypt(short_4, 0);
		}

		public static short EncryptDecrypt(short short_4, short short_5)
		{
			if (short_5 == 0)
			{
				return (short)(short_4 ^ short_0);
			}
			return (short)(short_4 ^ short_5);
		}

		public short GetEncrypted()
		{
			ApplyNewCryptoKey();
			return short_2;
		}

		public void SetEncrypted(short short_4)
		{
			bool_0 = true;
			short_2 = short_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				short_3 = InternalDecrypt();
			}
		}

		private short InternalDecrypt()
		{
			if (!bool_0)
			{
				short_1 = short_0;
				short_2 = EncryptDecrypt(0);
				short_3 = 0;
				bool_0 = true;
			}
			short short_ = short_0;
			if (short_1 != short_0)
			{
				short_ = short_1;
			}
			short num = EncryptDecrypt(short_2, short_);
			if (ObscuredCheatingDetector.bool_1 && short_3 != 0 && num != short_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredShort))
			{
				return false;
			}
			ObscuredShort obscuredShort = (ObscuredShort)obj;
			return short_2 == obscuredShort.short_2;
		}

		public bool Equals(ObscuredShort other)
		{
			return short_2 == other.short_2;
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public string ToString(string string_0)
		{
			return InternalDecrypt().ToString(string_0);
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public string ToString(IFormatProvider iformatProvider_0)
		{
			return InternalDecrypt().ToString(iformatProvider_0);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			return InternalDecrypt().ToString(format, provider);
		}

		public static implicit operator ObscuredShort(short short_4)
		{
			ObscuredShort result = new ObscuredShort(EncryptDecrypt(short_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.short_3 = short_4;
			}
			return result;
		}

		public static implicit operator short(ObscuredShort obscuredShort_0)
		{
			return obscuredShort_0.InternalDecrypt();
		}

		public static ObscuredShort operator ++(ObscuredShort obscuredShort_0)
		{
			short short_ = (short)(obscuredShort_0.InternalDecrypt() + 1);
			obscuredShort_0.short_2 = EncryptDecrypt(short_);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredShort_0.short_3 = short_;
			}
			return obscuredShort_0;
		}

		public static ObscuredShort operator --(ObscuredShort obscuredShort_0)
		{
			short short_ = (short)(obscuredShort_0.InternalDecrypt() - 1);
			obscuredShort_0.short_2 = EncryptDecrypt(short_);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredShort_0.short_3 = short_;
			}
			return obscuredShort_0;
		}
	}
}
