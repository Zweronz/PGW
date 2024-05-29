using System;
using CodeStage.AntiCheat.Detectors;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredUInt : IEquatable<ObscuredUInt>, IFormattable
	{
		private static uint uint_0 = 240513u;

		private uint uint_1;

		private uint uint_2;

		private uint uint_3;

		private bool bool_0;

		private ObscuredUInt(uint uint_4)
		{
			uint_1 = uint_0;
			uint_2 = uint_4;
			uint_3 = 0u;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(uint uint_4)
		{
			uint_0 = uint_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (uint_1 != uint_0)
			{
				uint_2 = Encrypt(InternalDecrypt(), uint_0);
				uint_1 = uint_0;
			}
		}

		public static uint Encrypt(uint uint_4)
		{
			return Encrypt(uint_4, 0u);
		}

		public static uint Decrypt(uint uint_4)
		{
			return Decrypt(uint_4, 0u);
		}

		public static uint Encrypt(uint uint_4, uint uint_5)
		{
			if (uint_5 == 0)
			{
				return uint_4 ^ uint_0;
			}
			return uint_4 ^ uint_5;
		}

		public static uint Decrypt(uint uint_4, uint uint_5)
		{
			if (uint_5 == 0)
			{
				return uint_4 ^ uint_0;
			}
			return uint_4 ^ uint_5;
		}

		public uint GetEncrypted()
		{
			ApplyNewCryptoKey();
			return uint_2;
		}

		public void SetEncrypted(uint uint_4)
		{
			bool_0 = true;
			uint_2 = uint_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				uint_3 = InternalDecrypt();
			}
		}

		private uint InternalDecrypt()
		{
			if (!bool_0)
			{
				uint_1 = uint_0;
				uint_2 = Encrypt(0u);
				uint_3 = 0u;
				bool_0 = true;
			}
			uint uint_ = uint_0;
			if (uint_1 != uint_0)
			{
				uint_ = uint_1;
			}
			uint num = Decrypt(uint_2, uint_);
			if (ObscuredCheatingDetector.bool_1 && uint_3 != 0 && num != uint_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredUInt))
			{
				return false;
			}
			ObscuredUInt obscuredUInt = (ObscuredUInt)obj;
			return uint_2 == obscuredUInt.uint_2;
		}

		public bool Equals(ObscuredUInt other)
		{
			return uint_2 == other.uint_2;
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

		public static implicit operator ObscuredUInt(uint uint_4)
		{
			ObscuredUInt result = new ObscuredUInt(Encrypt(uint_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.uint_3 = uint_4;
			}
			return result;
		}

		public static implicit operator uint(ObscuredUInt obscuredUInt_0)
		{
			return obscuredUInt_0.InternalDecrypt();
		}

		public static ObscuredUInt operator ++(ObscuredUInt obscuredUInt_0)
		{
			uint uint_ = obscuredUInt_0.InternalDecrypt() + 1;
			obscuredUInt_0.uint_2 = Encrypt(uint_, obscuredUInt_0.uint_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredUInt_0.uint_3 = uint_;
			}
			return obscuredUInt_0;
		}

		public static ObscuredUInt operator --(ObscuredUInt obscuredUInt_0)
		{
			uint uint_ = obscuredUInt_0.InternalDecrypt() - 1;
			obscuredUInt_0.uint_2 = Encrypt(uint_, obscuredUInt_0.uint_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredUInt_0.uint_3 = uint_;
			}
			return obscuredUInt_0;
		}
	}
}
