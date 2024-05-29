using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredLong : IEquatable<ObscuredLong>, IFormattable
	{
		private static long long_0 = 444442L;

		private long long_1;

		private long long_2;

		private long long_3;

		private bool bool_0;

		private ObscuredLong(long long_4)
		{
			long_1 = long_0;
			long_2 = long_4;
			long_3 = 0L;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(long long_4)
		{
			long_0 = long_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (long_1 != long_0)
			{
				long_2 = Encrypt(InternalDecrypt(), long_0);
				long_1 = long_0;
			}
		}

		public static long Encrypt(long long_4)
		{
			return Encrypt(long_4, 0L);
		}

		public static long Decrypt(long long_4)
		{
			return Decrypt(long_4, 0L);
		}

		public static long Encrypt(long long_4, long long_5)
		{
			if (long_5 == 0L)
			{
				return long_4 ^ long_0;
			}
			return long_4 ^ long_5;
		}

		public static long Decrypt(long long_4, long long_5)
		{
			if (long_5 == 0L)
			{
				return long_4 ^ long_0;
			}
			return long_4 ^ long_5;
		}

		public long GetEncrypted()
		{
			ApplyNewCryptoKey();
			return long_2;
		}

		public void SetEncrypted(long long_4)
		{
			bool_0 = true;
			long_2 = long_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				long_3 = InternalDecrypt();
			}
		}

		private long InternalDecrypt()
		{
			if (!bool_0)
			{
				long_1 = long_0;
				long_2 = Encrypt(0L);
				long_3 = 0L;
				bool_0 = true;
			}
			long long_ = long_0;
			if (long_1 != long_0)
			{
				long_ = long_1;
			}
			long num = Decrypt(long_2, long_);
			if (ObscuredCheatingDetector.bool_1 && long_3 != 0L && num != long_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredLong))
			{
				return false;
			}
			ObscuredLong obscuredLong = (ObscuredLong)obj;
			return long_2 == obscuredLong.long_2;
		}

		public bool Equals(ObscuredLong other)
		{
			return long_2 == other.long_2;
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

		public static implicit operator ObscuredLong(long long_4)
		{
			ObscuredLong result = new ObscuredLong(Encrypt(long_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.long_3 = long_4;
			}
			return result;
		}

		public static implicit operator long(ObscuredLong obscuredLong_0)
		{
			return obscuredLong_0.InternalDecrypt();
		}

		public static ObscuredLong operator ++(ObscuredLong obscuredLong_0)
		{
			long long_ = obscuredLong_0.InternalDecrypt() + 1L;
			obscuredLong_0.long_2 = Encrypt(long_, obscuredLong_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredLong_0.long_3 = long_;
			}
			return obscuredLong_0;
		}

		public static ObscuredLong operator --(ObscuredLong obscuredLong_0)
		{
			long long_ = obscuredLong_0.InternalDecrypt() - 1L;
			obscuredLong_0.long_2 = Encrypt(long_, obscuredLong_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredLong_0.long_3 = long_;
			}
			return obscuredLong_0;
		}
	}
}
