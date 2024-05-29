using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredInt : IEquatable<ObscuredInt>, IFormattable
	{
		private static int int_0 = 444444;

		[SerializeField]
		private int int_1;

		[SerializeField]
		private int int_2;

		[SerializeField]
		private int int_3;

		[SerializeField]
		private bool bool_0;

		private ObscuredInt(int int_4)
		{
			int_1 = int_0;
			int_2 = int_4;
			int_3 = 0;
			bool_0 = true;
		}

		public static void SetNewCryptoKey(int int_4)
		{
			int_0 = int_4;
		}

		public void ApplyNewCryptoKey()
		{
			if (int_1 != int_0)
			{
				int_2 = Encrypt(InternalDecrypt(), int_0);
				int_1 = int_0;
			}
		}

		public static int Encrypt(int int_4)
		{
			return Encrypt(int_4, 0);
		}

		public static int Encrypt(int int_4, int int_5)
		{
			if (int_5 == 0)
			{
				return int_4 ^ int_0;
			}
			return int_4 ^ int_5;
		}

		public static int Decrypt(int int_4)
		{
			return Decrypt(int_4, 0);
		}

		public static int Decrypt(int int_4, int int_5)
		{
			if (int_5 == 0)
			{
				return int_4 ^ int_0;
			}
			return int_4 ^ int_5;
		}

		public int GetEncrypted()
		{
			ApplyNewCryptoKey();
			return int_2;
		}

		public void SetEncrypted(int int_4)
		{
			bool_0 = true;
			int_2 = int_4;
			if (ObscuredCheatingDetector.bool_1)
			{
				int_3 = InternalDecrypt();
			}
		}

		private int InternalDecrypt()
		{
			if (!bool_0)
			{
				int_1 = int_0;
				int_2 = Encrypt(0);
				int_3 = 0;
				bool_0 = true;
			}
			int int_ = int_0;
			if (int_1 != int_0)
			{
				int_ = int_1;
			}
			int num = Decrypt(int_2, int_);
			if (ObscuredCheatingDetector.bool_1 && int_3 != 0 && num != int_3)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredInt))
			{
				return false;
			}
			ObscuredInt obscuredInt = (ObscuredInt)obj;
			return int_2 == obscuredInt.int_2;
		}

		public bool Equals(ObscuredInt other)
		{
			return int_2 == other.int_2;
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

		public static implicit operator ObscuredInt(int int_4)
		{
			ObscuredInt result = new ObscuredInt(Encrypt(int_4));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.int_3 = int_4;
			}
			return result;
		}

		public static implicit operator int(ObscuredInt obscuredInt_0)
		{
			return obscuredInt_0.InternalDecrypt();
		}

		public static ObscuredInt operator ++(ObscuredInt obscuredInt_0)
		{
			int int_ = obscuredInt_0.InternalDecrypt() + 1;
			obscuredInt_0.int_2 = Encrypt(int_, obscuredInt_0.int_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredInt_0.int_3 = int_;
			}
			return obscuredInt_0;
		}

		public static ObscuredInt operator --(ObscuredInt obscuredInt_0)
		{
			int int_ = obscuredInt_0.InternalDecrypt() - 1;
			obscuredInt_0.int_2 = Encrypt(int_, obscuredInt_0.int_1);
			if (ObscuredCheatingDetector.bool_1)
			{
				obscuredInt_0.int_3 = int_;
			}
			return obscuredInt_0;
		}
	}
}
