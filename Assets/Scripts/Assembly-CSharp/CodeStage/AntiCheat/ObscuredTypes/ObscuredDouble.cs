using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredDouble : IEquatable<ObscuredDouble>, IFormattable
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct DoubleLongBytesUnion
		{
			[FieldOffset(0)]
			public double double_0;

			[FieldOffset(0)]
			public long long_0;

			[FieldOffset(0)]
			public byte byte_0;

			[FieldOffset(1)]
			public byte byte_1;

			[FieldOffset(2)]
			public byte byte_2;

			[FieldOffset(3)]
			public byte byte_3;

			[FieldOffset(4)]
			public byte byte_4;

			[FieldOffset(5)]
			public byte byte_5;

			[FieldOffset(6)]
			public byte byte_6;

			[FieldOffset(7)]
			public byte byte_7;
		}

		private static long long_0 = 210987L;

		private long long_1;

		private byte[] byte_0;

		private double double_0;

		private bool bool_0;

		private ObscuredDouble(byte[] byte_1)
		{
			long_1 = long_0;
			byte_0 = byte_1;
			double_0 = 0.0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(long long_2)
		{
			long_0 = long_2;
		}

		public void ApplyNewCryptoKey()
		{
			if (long_1 != long_0)
			{
				byte_0 = InternalEncrypt(InternalDecrypt(), long_0);
				long_1 = long_0;
			}
		}

		public static long Encrypt(double double_1)
		{
			return Encrypt(double_1, long_0);
		}

		public static long Encrypt(double double_1, long long_2)
		{
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.double_0 = double_1;
			doubleLongBytesUnion.long_0 ^= long_2;
			return doubleLongBytesUnion.long_0;
		}

		private static byte[] InternalEncrypt(double double_1)
		{
			return InternalEncrypt(double_1, 0L);
		}

		private static byte[] InternalEncrypt(double double_1, long long_2)
		{
			long num = long_2;
			if (num == 0L)
			{
				num = long_0;
			}
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.double_0 = double_1;
			doubleLongBytesUnion.long_0 ^= num;
			return new byte[8] { doubleLongBytesUnion.byte_0, doubleLongBytesUnion.byte_1, doubleLongBytesUnion.byte_2, doubleLongBytesUnion.byte_3, doubleLongBytesUnion.byte_4, doubleLongBytesUnion.byte_5, doubleLongBytesUnion.byte_6, doubleLongBytesUnion.byte_7 };
		}

		public static double Decrypt(long long_2)
		{
			return Decrypt(long_2, long_0);
		}

		public static double Decrypt(long long_2, long long_3)
		{
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.long_0 = long_2 ^ long_3;
			return doubleLongBytesUnion.double_0;
		}

		public long GetEncrypted()
		{
			ApplyNewCryptoKey();
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.byte_0 = byte_0[0];
			doubleLongBytesUnion.byte_1 = byte_0[1];
			doubleLongBytesUnion.byte_2 = byte_0[2];
			doubleLongBytesUnion.byte_3 = byte_0[3];
			doubleLongBytesUnion.byte_4 = byte_0[4];
			doubleLongBytesUnion.byte_5 = byte_0[5];
			doubleLongBytesUnion.byte_6 = byte_0[6];
			doubleLongBytesUnion.byte_7 = byte_0[7];
			return doubleLongBytesUnion.long_0;
		}

		public void SetEncrypted(long long_2)
		{
			bool_0 = true;
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.long_0 = long_2;
			byte_0 = new byte[8] { doubleLongBytesUnion.byte_0, doubleLongBytesUnion.byte_1, doubleLongBytesUnion.byte_2, doubleLongBytesUnion.byte_3, doubleLongBytesUnion.byte_4, doubleLongBytesUnion.byte_5, doubleLongBytesUnion.byte_6, doubleLongBytesUnion.byte_7 };
			if (ObscuredCheatingDetector.bool_1)
			{
				double_0 = InternalDecrypt();
			}
		}

		private double InternalDecrypt()
		{
			if (!bool_0)
			{
				long_1 = long_0;
				byte_0 = InternalEncrypt(0.0);
				double_0 = 0.0;
				bool_0 = true;
			}
			long num = long_0;
			if (long_1 != long_0)
			{
				num = long_1;
			}
			DoubleLongBytesUnion doubleLongBytesUnion = default(DoubleLongBytesUnion);
			doubleLongBytesUnion.byte_0 = byte_0[0];
			doubleLongBytesUnion.byte_1 = byte_0[1];
			doubleLongBytesUnion.byte_2 = byte_0[2];
			doubleLongBytesUnion.byte_3 = byte_0[3];
			doubleLongBytesUnion.byte_4 = byte_0[4];
			doubleLongBytesUnion.byte_5 = byte_0[5];
			doubleLongBytesUnion.byte_6 = byte_0[6];
			doubleLongBytesUnion.byte_7 = byte_0[7];
			doubleLongBytesUnion.long_0 ^= num;
			double num2 = doubleLongBytesUnion.double_0;
			if (ObscuredCheatingDetector.bool_1 && double_0 != 0.0 && Math.Abs(num2 - double_0) > 1E-06)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num2;
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

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredDouble))
			{
				return false;
			}
			double num = ((ObscuredDouble)obj).InternalDecrypt();
			double num2 = InternalDecrypt();
			if (num == num2)
			{
				return true;
			}
			return double.IsNaN(num) && double.IsNaN(num2);
		}

		public bool Equals(ObscuredDouble other)
		{
			double num = other.InternalDecrypt();
			double num2 = InternalDecrypt();
			if (num == num2)
			{
				return true;
			}
			return double.IsNaN(num) && double.IsNaN(num2);
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public static implicit operator ObscuredDouble(double double_1)
		{
			ObscuredDouble result = new ObscuredDouble(InternalEncrypt(double_1));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.double_0 = double_1;
			}
			return result;
		}

		public static implicit operator double(ObscuredDouble obscuredDouble_0)
		{
			return obscuredDouble_0.InternalDecrypt();
		}

		public static ObscuredDouble operator ++(ObscuredDouble obscuredDouble_0)
		{
			double double_ = obscuredDouble_0.InternalDecrypt() + 1.0;
			obscuredDouble_0.byte_0 = InternalEncrypt(double_, obscuredDouble_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredDouble_0.double_0 = double_;
			}
			return obscuredDouble_0;
		}

		public static ObscuredDouble operator --(ObscuredDouble obscuredDouble_0)
		{
			double double_ = obscuredDouble_0.InternalDecrypt() - 1.0;
			obscuredDouble_0.byte_0 = InternalEncrypt(double_, obscuredDouble_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredDouble_0.double_0 = double_;
			}
			return obscuredDouble_0;
		}
	}
}
