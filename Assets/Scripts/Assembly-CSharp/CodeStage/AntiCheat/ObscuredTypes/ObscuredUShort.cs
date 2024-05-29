using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredUShort : IEquatable<ObscuredUShort>, IFormattable
	{
		private static ushort ushort_0 = 224;

		private ushort ushort_1;

		private ushort ushort_2;

		private ushort ushort_3;

		private bool bool_0;

		private ObscuredUShort(ushort ushort_4)
		{
			ushort_1 = ushort_0;
			ushort_2 = ushort_4;
			ushort_3 = 0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(ushort ushort_4)
		{
			ushort_0 = ushort_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (ushort_1 != ushort_0)
			{
				ushort_2 = EncryptDecrypt(InternalDecrypt(), ushort_0);
				ushort_1 = ushort_0;
			}
		}

		public static ushort EncryptDecrypt(ushort ushort_4)
		{
			return EncryptDecrypt(ushort_4, 0);
		}

		public static ushort EncryptDecrypt(ushort ushort_4, ushort ushort_5)
		{
			if (ushort_5 == 0)
			{
				return (ushort)(ushort_4 ^ ushort_0);
			}
			return (ushort)(ushort_4 ^ ushort_5);
		}

		public ushort GetEncrypted()
		{
			ApplyNewCryptoKey();
			return ushort_2;
		}

		public void SetEncrypted(ushort ushort_4)
		{
			bool_0 = true;
			ushort_2 = ushort_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				ushort_3 = InternalDecrypt();
			}
		}

		private ushort InternalDecrypt()
		{
			if (!bool_0)
			{
				ushort_1 = ushort_0;
				ushort_2 = EncryptDecrypt(0);
				ushort_3 = 0;
				bool_0 = true;
			}
			ushort ushort_ = ushort_0;
			if (ushort_1 != ushort_0)
			{
				ushort_ = ushort_1;
			}
			ushort num = EncryptDecrypt(ushort_2, ushort_);
			if (ObscuredCheatingDetector.bool_1 && ushort_3 != 0 && num != ushort_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredUShort))
			{
				return false;
			}
			ObscuredUShort obscuredUShort = (ObscuredUShort)obj;
			return ushort_2 == obscuredUShort.ushort_2;
		}

		public bool Equals(ObscuredUShort other)
		{
			return ushort_2 == other.ushort_2;
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

		public static implicit operator ObscuredUShort(ushort ushort_4)
		{
			ObscuredUShort result = new ObscuredUShort(EncryptDecrypt(ushort_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.ushort_3 = ushort_4;
			}
			return result;
		}

		public static implicit operator ushort(ObscuredUShort obscuredUShort_0)
		{
			return obscuredUShort_0.InternalDecrypt();
		}

		public static ObscuredUShort operator ++(ObscuredUShort obscuredUShort_0)
		{
			ushort ushort_ = (ushort)(obscuredUShort_0.InternalDecrypt() + 1);
			obscuredUShort_0.ushort_2 = EncryptDecrypt(ushort_, obscuredUShort_0.ushort_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredUShort_0.ushort_3 = ushort_;
			}
			return obscuredUShort_0;
		}

		public static ObscuredUShort operator --(ObscuredUShort obscuredUShort_0)
		{
			ushort ushort_ = (ushort)(obscuredUShort_0.InternalDecrypt() - 1);
			obscuredUShort_0.ushort_2 = EncryptDecrypt(ushort_, obscuredUShort_0.ushort_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredUShort_0.ushort_3 = ushort_;
			}
			return obscuredUShort_0;
		}
	}
}
