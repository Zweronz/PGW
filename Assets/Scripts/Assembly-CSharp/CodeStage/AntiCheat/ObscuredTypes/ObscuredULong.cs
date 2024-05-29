using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredULong : IEquatable<ObscuredULong>, IFormattable
	{
		private static ulong ulong_0 = 444443uL;

		private ulong ulong_1;

		private ulong ulong_2;

		private ulong ulong_3;

		private bool bool_0;

		private ObscuredULong(ulong ulong_4)
		{
			ulong_1 = ulong_0;
			ulong_2 = ulong_4;
			ulong_3 = 0uL;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(ulong ulong_4)
		{
			ulong_0 = ulong_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (ulong_1 != ulong_0)
			{
				ulong_2 = Encrypt(InternalDecrypt(), ulong_0);
				ulong_1 = ulong_0;
			}
		}

		public static ulong Encrypt(ulong ulong_4)
		{
			return Encrypt(ulong_4, 0uL);
		}

		public static ulong Decrypt(ulong ulong_4)
		{
			return Decrypt(ulong_4, 0uL);
		}

		public static ulong Encrypt(ulong ulong_4, ulong ulong_5)
		{
			if (ulong_5 == 0L)
			{
				return ulong_4 ^ ulong_0;
			}
			return ulong_4 ^ ulong_5;
		}

		public static ulong Decrypt(ulong ulong_4, ulong ulong_5)
		{
			if (ulong_5 == 0L)
			{
				return ulong_4 ^ ulong_0;
			}
			return ulong_4 ^ ulong_5;
		}

		public ulong GetEncrypted()
		{
			ApplyNewCryptoKey();
			return ulong_2;
		}

		public void SetEncrypted(ulong ulong_4)
		{
			bool_0 = true;
			ulong_2 = ulong_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				ulong_3 = InternalDecrypt();
			}
		}

		private ulong InternalDecrypt()
		{
			if (!bool_0)
			{
				ulong_1 = ulong_0;
				ulong_2 = Encrypt(0uL);
				ulong_3 = 0uL;
				bool_0 = true;
			}
			ulong ulong_ = ulong_0;
			if (ulong_1 != ulong_0)
			{
				ulong_ = ulong_1;
			}
			ulong num = Decrypt(ulong_2, ulong_);
			if (ObscuredCheatingDetector.bool_1 && ulong_3 != 0L && num != ulong_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredULong))
			{
				return false;
			}
			ObscuredULong obscuredULong = (ObscuredULong)obj;
			return ulong_2 == obscuredULong.ulong_2;
		}

		public bool Equals(ObscuredULong other)
		{
			return ulong_2 == other.ulong_2;
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public string ToString(string string_0)
		{
			return InternalDecrypt().ToString(string_0);
		}

		public string ToString(IFormatProvider iformatProvider_0)
		{
			return InternalDecrypt().ToString(iformatProvider_0);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			return InternalDecrypt().ToString(format, provider);
		}

		public static implicit operator ObscuredULong(ulong ulong_4)
		{
			ObscuredULong result = new ObscuredULong(Encrypt(ulong_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.ulong_3 = ulong_4;
			}
			return result;
		}

		public static implicit operator ulong(ObscuredULong obscuredULong_0)
		{
			return obscuredULong_0.InternalDecrypt();
		}

		public static ObscuredULong operator ++(ObscuredULong obscuredULong_0)
		{
			ulong ulong_ = obscuredULong_0.InternalDecrypt() + 1L;
			obscuredULong_0.ulong_2 = Encrypt(ulong_, obscuredULong_0.ulong_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredULong_0.ulong_3 = ulong_;
			}
			return obscuredULong_0;
		}

		public static ObscuredULong operator --(ObscuredULong obscuredULong_0)
		{
			ulong ulong_ = obscuredULong_0.InternalDecrypt() - 1L;
			obscuredULong_0.ulong_2 = Encrypt(ulong_, obscuredULong_0.ulong_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredULong_0.ulong_3 = ulong_;
			}
			return obscuredULong_0;
		}
	}
}
