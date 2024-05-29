using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredDecimal : IEquatable<ObscuredDecimal>, IFormattable
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct DecimalLongBytesUnion
		{
			[FieldOffset(0)]
			public decimal decimal_0;

			[FieldOffset(0)]
			public long long_0;

			[FieldOffset(8)]
			public long long_1;

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

			[FieldOffset(8)]
			public byte byte_8;

			[FieldOffset(9)]
			public byte byte_9;

			[FieldOffset(10)]
			public byte byte_10;

			[FieldOffset(11)]
			public byte byte_11;

			[FieldOffset(12)]
			public byte byte_12;

			[FieldOffset(13)]
			public byte byte_13;

			[FieldOffset(14)]
			public byte byte_14;

			[FieldOffset(15)]
			public byte byte_15;
		}

		private static long long_0 = 209208L;

		private long long_1;

		private byte[] byte_0;

		private decimal decimal_0;

		private bool bool_0;

		private ObscuredDecimal(byte[] byte_1)
		{
			long_1 = long_0;
			byte_0 = byte_1;
			decimal_0 = 0m;
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

		public static decimal Encrypt(decimal decimal_1)
		{
			return Encrypt(decimal_1, long_0);
		}

		public static decimal Encrypt(decimal decimal_1, long long_2)
		{
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.decimal_0 = decimal_1;
			decimalLongBytesUnion.long_0 ^= long_2;
			decimalLongBytesUnion.long_1 ^= long_2;
			return decimalLongBytesUnion.decimal_0;
		}

		private static byte[] InternalEncrypt(decimal decimal_1)
		{
			return InternalEncrypt(decimal_1, 0L);
		}

		private static byte[] InternalEncrypt(decimal decimal_1, long long_2)
		{
			long num = long_2;
			if (num == 0L)
			{
				num = long_0;
			}
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.decimal_0 = decimal_1;
			decimalLongBytesUnion.long_0 ^= num;
			decimalLongBytesUnion.long_1 ^= num;
			return new byte[16]
			{
				decimalLongBytesUnion.byte_0, decimalLongBytesUnion.byte_1, decimalLongBytesUnion.byte_2, decimalLongBytesUnion.byte_3, decimalLongBytesUnion.byte_4, decimalLongBytesUnion.byte_5, decimalLongBytesUnion.byte_6, decimalLongBytesUnion.byte_7, decimalLongBytesUnion.byte_8, decimalLongBytesUnion.byte_9,
				decimalLongBytesUnion.byte_10, decimalLongBytesUnion.byte_11, decimalLongBytesUnion.byte_12, decimalLongBytesUnion.byte_13, decimalLongBytesUnion.byte_14, decimalLongBytesUnion.byte_15
			};
		}

		public static decimal Decrypt(decimal decimal_1)
		{
			return Decrypt(decimal_1, long_0);
		}

		public static decimal Decrypt(decimal decimal_1, long long_2)
		{
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.decimal_0 = decimal_1;
			decimalLongBytesUnion.long_0 ^= long_2;
			decimalLongBytesUnion.long_1 ^= long_2;
			return decimalLongBytesUnion.decimal_0;
		}

		public decimal GetEncrypted()
		{
			ApplyNewCryptoKey();
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.byte_0 = byte_0[0];
			decimalLongBytesUnion.byte_1 = byte_0[1];
			decimalLongBytesUnion.byte_2 = byte_0[2];
			decimalLongBytesUnion.byte_3 = byte_0[3];
			decimalLongBytesUnion.byte_4 = byte_0[4];
			decimalLongBytesUnion.byte_5 = byte_0[5];
			decimalLongBytesUnion.byte_6 = byte_0[6];
			decimalLongBytesUnion.byte_7 = byte_0[7];
			decimalLongBytesUnion.byte_8 = byte_0[8];
			decimalLongBytesUnion.byte_9 = byte_0[9];
			decimalLongBytesUnion.byte_10 = byte_0[10];
			decimalLongBytesUnion.byte_11 = byte_0[11];
			decimalLongBytesUnion.byte_12 = byte_0[12];
			decimalLongBytesUnion.byte_13 = byte_0[13];
			decimalLongBytesUnion.byte_14 = byte_0[14];
			decimalLongBytesUnion.byte_15 = byte_0[15];
			return decimalLongBytesUnion.decimal_0;
		}

		public void SetEncrypted(decimal decimal_1)
		{
			bool_0 = true;
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.decimal_0 = decimal_1;
			byte_0 = new byte[16]
			{
				decimalLongBytesUnion.byte_0, decimalLongBytesUnion.byte_1, decimalLongBytesUnion.byte_2, decimalLongBytesUnion.byte_3, decimalLongBytesUnion.byte_4, decimalLongBytesUnion.byte_5, decimalLongBytesUnion.byte_6, decimalLongBytesUnion.byte_7, decimalLongBytesUnion.byte_8, decimalLongBytesUnion.byte_9,
				decimalLongBytesUnion.byte_10, decimalLongBytesUnion.byte_11, decimalLongBytesUnion.byte_12, decimalLongBytesUnion.byte_13, decimalLongBytesUnion.byte_14, decimalLongBytesUnion.byte_15
			};
			if (ObscuredCheatingDetector.bool_1)
			{
				decimal_0 = InternalDecrypt();
			}
		}

		private decimal InternalDecrypt()
		{
			if (!bool_0)
			{
				long_1 = long_0;
				byte_0 = InternalEncrypt(0m);
				decimal_0 = 0m;
				bool_0 = true;
			}
			long num = long_0;
			if (long_1 != long_0)
			{
				num = long_1;
			}
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.byte_0 = byte_0[0];
			decimalLongBytesUnion.byte_1 = byte_0[1];
			decimalLongBytesUnion.byte_2 = byte_0[2];
			decimalLongBytesUnion.byte_3 = byte_0[3];
			decimalLongBytesUnion.byte_4 = byte_0[4];
			decimalLongBytesUnion.byte_5 = byte_0[5];
			decimalLongBytesUnion.byte_6 = byte_0[6];
			decimalLongBytesUnion.byte_7 = byte_0[7];
			decimalLongBytesUnion.byte_8 = byte_0[8];
			decimalLongBytesUnion.byte_9 = byte_0[9];
			decimalLongBytesUnion.byte_10 = byte_0[10];
			decimalLongBytesUnion.byte_11 = byte_0[11];
			decimalLongBytesUnion.byte_12 = byte_0[12];
			decimalLongBytesUnion.byte_13 = byte_0[13];
			decimalLongBytesUnion.byte_14 = byte_0[14];
			decimalLongBytesUnion.byte_15 = byte_0[15];
			decimalLongBytesUnion.long_0 ^= num;
			decimalLongBytesUnion.long_1 ^= num;
			decimal num2 = decimalLongBytesUnion.decimal_0;
			if (ObscuredCheatingDetector.bool_1 && decimal_0 != 0m && num2 != decimal_0)
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
			if (!(obj is ObscuredDecimal))
			{
				return false;
			}
			return ((ObscuredDecimal)obj).InternalDecrypt().Equals(InternalDecrypt());
		}

		public bool Equals(ObscuredDecimal other)
		{
			return other.InternalDecrypt().Equals(InternalDecrypt());
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public static implicit operator ObscuredDecimal(decimal decimal_1)
		{
			ObscuredDecimal result = new ObscuredDecimal(InternalEncrypt(decimal_1));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.decimal_0 = decimal_1;
			}
			return result;
		}

		public static implicit operator decimal(ObscuredDecimal obscuredDecimal_0)
		{
			return obscuredDecimal_0.InternalDecrypt();
		}

		public static ObscuredDecimal operator ++(ObscuredDecimal obscuredDecimal_0)
		{
			decimal decimal_ = obscuredDecimal_0.InternalDecrypt() + 1m;
			obscuredDecimal_0.byte_0 = InternalEncrypt(decimal_, obscuredDecimal_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredDecimal_0.decimal_0 = decimal_;
			}
			return obscuredDecimal_0;
		}

		public static ObscuredDecimal operator --(ObscuredDecimal obscuredDecimal_0)
		{
			decimal decimal_ = obscuredDecimal_0.InternalDecrypt() - 1m;
			obscuredDecimal_0.byte_0 = InternalEncrypt(decimal_, obscuredDecimal_0.long_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredDecimal_0.decimal_0 = decimal_;
			}
			return obscuredDecimal_0;
		}
	}
}
