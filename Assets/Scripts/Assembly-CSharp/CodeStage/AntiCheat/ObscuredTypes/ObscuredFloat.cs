using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredFloat : IEquatable<ObscuredFloat>, IFormattable
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatIntBytesUnion
		{
			[FieldOffset(0)]
			public float float_0;

			[FieldOffset(0)]
			public int int_0;

			[FieldOffset(0)]
			public byte byte_0;

			[FieldOffset(1)]
			public byte byte_1;

			[FieldOffset(2)]
			public byte byte_2;

			[FieldOffset(3)]
			public byte byte_3;
		}

		private static int int_0 = 230887;

		[SerializeField]
		private int int_1;

		[SerializeField]
		private byte[] byte_0;

		[SerializeField]
		private float float_0;

		[SerializeField]
		private bool bool_0;

		private ObscuredFloat(byte[] byte_1)
		{
			int_1 = int_0;
			byte_0 = byte_1;
			float_0 = 0f;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(int int_2)
		{
			int_0 = int_2;
		}

		public void ApplyNewCryptoKey()
		{
			if (int_1 != int_0)
			{
				byte_0 = InternalEncrypt(InternalDecrypt(), int_0);
				int_1 = int_0;
			}
		}

		public static int Encrypt(float float_1)
		{
			return Encrypt(float_1, int_0);
		}

		public static int Encrypt(float float_1, int int_2)
		{
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.float_0 = float_1;
			floatIntBytesUnion.int_0 ^= int_2;
			return floatIntBytesUnion.int_0;
		}

		private static byte[] InternalEncrypt(float float_1)
		{
			return InternalEncrypt(float_1, 0);
		}

		private static byte[] InternalEncrypt(float float_1, int int_2)
		{
			int num = int_2;
			if (num == 0)
			{
				num = int_0;
			}
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.float_0 = float_1;
			floatIntBytesUnion.int_0 ^= num;
			return new byte[4] { floatIntBytesUnion.byte_0, floatIntBytesUnion.byte_1, floatIntBytesUnion.byte_2, floatIntBytesUnion.byte_3 };
		}

		public static float Decrypt(int int_2)
		{
			return Decrypt(int_2, int_0);
		}

		public static float Decrypt(int int_2, int int_3)
		{
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.int_0 = int_2 ^ int_3;
			return floatIntBytesUnion.float_0;
		}

		public int GetEncrypted()
		{
			ApplyNewCryptoKey();
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.byte_0 = byte_0[0];
			floatIntBytesUnion.byte_1 = byte_0[1];
			floatIntBytesUnion.byte_2 = byte_0[2];
			floatIntBytesUnion.byte_3 = byte_0[3];
			return floatIntBytesUnion.int_0;
		}

		public void SetEncrypted(int int_2)
		{
			bool_0 = true;
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.int_0 = int_2;
			byte_0 = new byte[4] { floatIntBytesUnion.byte_0, floatIntBytesUnion.byte_1, floatIntBytesUnion.byte_2, floatIntBytesUnion.byte_3 };
			if (ObscuredCheatingDetector.bool_1)
			{
				float_0 = InternalDecrypt();
			}
		}

		private float InternalDecrypt()
		{
			if (!bool_0)
			{
				int_1 = int_0;
				byte_0 = InternalEncrypt(0f);
				float_0 = 0f;
				bool_0 = true;
			}
			int num = int_0;
			if (int_1 != int_0)
			{
				num = int_1;
			}
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.byte_0 = byte_0[0];
			floatIntBytesUnion.byte_1 = byte_0[1];
			floatIntBytesUnion.byte_2 = byte_0[2];
			floatIntBytesUnion.byte_3 = byte_0[3];
			floatIntBytesUnion.int_0 ^= num;
			float num2 = floatIntBytesUnion.float_0;
			if (ObscuredCheatingDetector.bool_1 && float_0 != 0f && Math.Abs(num2 - float_0) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_0)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num2;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredFloat))
			{
				return false;
			}
			float num = ((ObscuredFloat)obj).InternalDecrypt();
			float num2 = InternalDecrypt();
			if ((double)num == (double)num2)
			{
				return true;
			}
			return float.IsNaN(num) && float.IsNaN(num2);
		}

		public bool Equals(ObscuredFloat other)
		{
			float num = other.InternalDecrypt();
			float num2 = InternalDecrypt();
			if ((double)num == (double)num2)
			{
				return true;
			}
			return float.IsNaN(num) && float.IsNaN(num2);
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

		public static implicit operator ObscuredFloat(float float_1)
		{
			ObscuredFloat result = new ObscuredFloat(InternalEncrypt(float_1));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.float_0 = float_1;
			}
			return result;
		}

		public static implicit operator float(ObscuredFloat obscuredFloat_0)
		{
			return obscuredFloat_0.InternalDecrypt();
		}

		public static ObscuredFloat operator ++(ObscuredFloat obscuredFloat_0)
		{
			float float_ = obscuredFloat_0.InternalDecrypt() + 1f;
			obscuredFloat_0.byte_0 = InternalEncrypt(float_, obscuredFloat_0.int_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredFloat_0.float_0 = float_;
			}
			return obscuredFloat_0;
		}

		public static ObscuredFloat operator --(ObscuredFloat obscuredFloat_0)
		{
			float float_ = obscuredFloat_0.InternalDecrypt() - 1f;
			obscuredFloat_0.byte_0 = InternalEncrypt(float_, obscuredFloat_0.int_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredFloat_0.float_0 = float_;
			}
			return obscuredFloat_0;
		}
	}
}
