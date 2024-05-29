using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredByte : IEquatable<ObscuredByte>, IFormattable
	{
		private static byte byte_0 = 244;

		private byte byte_1;

		private byte byte_2;

		private byte byte_3;

		private bool bool_0;

		private ObscuredByte(byte byte_4)
		{
			byte_1 = byte_0;
			byte_2 = byte_4;
			byte_3 = 0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(byte byte_4)
		{
			byte_0 = byte_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (byte_1 != byte_0)
			{
				byte_2 = EncryptDecrypt(InternalDecrypt(), byte_0);
				byte_1 = byte_0;
			}
		}

		public static byte EncryptDecrypt(byte byte_4)
		{
			return EncryptDecrypt(byte_4, 0);
		}

		public static byte EncryptDecrypt(byte byte_4, byte byte_5)
		{
			if (byte_5 == 0)
			{
				return (byte)(byte_4 ^ byte_0);
			}
			return (byte)(byte_4 ^ byte_5);
		}

		public byte GetEncrypted()
		{
			ApplyNewCryptoKey();
			return byte_2;
		}

		public void SetEncrypted(byte byte_4)
		{
			bool_0 = true;
			byte_2 = byte_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				byte_3 = InternalDecrypt();
			}
		}

		private byte InternalDecrypt()
		{
			if (!bool_0)
			{
				byte_1 = byte_0;
				byte_2 = EncryptDecrypt(0);
				byte_3 = 0;
				bool_0 = true;
			}
			byte byte_ = byte_0;
			if (byte_1 != byte_0)
			{
				byte_ = byte_1;
			}
			byte b = EncryptDecrypt(byte_2, byte_);
			if (ObscuredCheatingDetector.bool_1 && byte_3 != 0 && b != byte_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return b;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredByte))
			{
				return false;
			}
			ObscuredByte obscuredByte = (ObscuredByte)obj;
			return byte_2 == obscuredByte.byte_2;
		}

		public bool Equals(ObscuredByte other)
		{
			return byte_2 == other.byte_2;
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

		public static implicit operator ObscuredByte(byte byte_4)
		{
			ObscuredByte result = new ObscuredByte(EncryptDecrypt(byte_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.byte_3 = byte_4;
			}
			return result;
		}

		public static implicit operator byte(ObscuredByte obscuredByte_0)
		{
			return obscuredByte_0.InternalDecrypt();
		}

		public static ObscuredByte operator ++(ObscuredByte obscuredByte_0)
		{
			byte byte_ = (byte)(obscuredByte_0.InternalDecrypt() + 1);
			obscuredByte_0.byte_2 = EncryptDecrypt(byte_, obscuredByte_0.byte_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredByte_0.byte_3 = byte_;
			}
			return obscuredByte_0;
		}

		public static ObscuredByte operator --(ObscuredByte obscuredByte_0)
		{
			byte byte_ = (byte)(obscuredByte_0.InternalDecrypt() - 1);
			obscuredByte_0.byte_2 = EncryptDecrypt(byte_, obscuredByte_0.byte_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredByte_0.byte_3 = byte_;
			}
			return obscuredByte_0;
		}
	}
}
