using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredSByte : IEquatable<ObscuredSByte>, IFormattable
	{
		private static sbyte sbyte_0 = 112;

		private sbyte sbyte_1;

		private sbyte sbyte_2;

		private sbyte sbyte_3;

		private bool bool_0;

		private ObscuredSByte(sbyte sbyte_4)
		{
			sbyte_1 = sbyte_0;
			sbyte_2 = sbyte_4;
			sbyte_3 = 0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(sbyte sbyte_4)
		{
			sbyte_0 = sbyte_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (sbyte_1 != sbyte_0)
			{
				sbyte_2 = EncryptDecrypt(InternalDecrypt(), sbyte_0);
				sbyte_1 = sbyte_0;
			}
		}

		public static sbyte EncryptDecrypt(sbyte sbyte_4)
		{
			return EncryptDecrypt(sbyte_4, 0);
		}

		public static sbyte EncryptDecrypt(sbyte sbyte_4, sbyte sbyte_5)
		{
			if (sbyte_5 == 0)
			{
				return (sbyte)(sbyte_4 ^ sbyte_0);
			}
			return (sbyte)(sbyte_4 ^ sbyte_5);
		}

		public sbyte GetEncrypted()
		{
			ApplyNewCryptoKey();
			return sbyte_2;
		}

		public void SetEncrypted(sbyte sbyte_4)
		{
			bool_0 = true;
			sbyte_2 = sbyte_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				sbyte_3 = InternalDecrypt();
			}
		}

		private sbyte InternalDecrypt()
		{
			if (!bool_0)
			{
				sbyte_1 = sbyte_0;
				sbyte_2 = EncryptDecrypt(0);
				sbyte_3 = 0;
				bool_0 = true;
			}
			sbyte sbyte_ = sbyte_0;
			if (sbyte_1 != sbyte_0)
			{
				sbyte_ = sbyte_1;
			}
			sbyte b = EncryptDecrypt(sbyte_2, sbyte_);
			if (ObscuredCheatingDetector.bool_1 && sbyte_3 != 0 && b != sbyte_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return b;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredSByte))
			{
				return false;
			}
			ObscuredSByte obscuredSByte = (ObscuredSByte)obj;
			return sbyte_2 == obscuredSByte.sbyte_2;
		}

		public bool Equals(ObscuredSByte other)
		{
			return sbyte_2 == other.sbyte_2;
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

		public static implicit operator ObscuredSByte(sbyte sbyte_4)
		{
			ObscuredSByte result = new ObscuredSByte(EncryptDecrypt(sbyte_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.sbyte_3 = sbyte_4;
			}
			return result;
		}

		public static implicit operator sbyte(ObscuredSByte obscuredSByte_0)
		{
			return obscuredSByte_0.InternalDecrypt();
		}

		public static ObscuredSByte operator ++(ObscuredSByte obscuredSByte_0)
		{
			sbyte sbyte_ = (sbyte)(obscuredSByte_0.InternalDecrypt() + 1);
			obscuredSByte_0.sbyte_2 = EncryptDecrypt(sbyte_, obscuredSByte_0.sbyte_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredSByte_0.sbyte_3 = sbyte_;
			}
			return obscuredSByte_0;
		}

		public static ObscuredSByte operator --(ObscuredSByte obscuredSByte_0)
		{
			sbyte sbyte_ = (sbyte)(obscuredSByte_0.InternalDecrypt() - 1);
			obscuredSByte_0.sbyte_2 = EncryptDecrypt(sbyte_, obscuredSByte_0.sbyte_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredSByte_0.sbyte_3 = sbyte_;
			}
			return obscuredSByte_0;
		}
	}
}
