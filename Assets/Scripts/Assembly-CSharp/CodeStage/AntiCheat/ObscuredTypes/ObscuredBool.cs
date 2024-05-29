using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredBool : IEquatable<ObscuredBool>
	{
		private static byte byte_0 = 215;

		[SerializeField]
		private byte byte_1;

		[SerializeField]
		private int int_0;

		[SerializeField]
		private bool bool_0;

		[SerializeField]
		private bool bool_1;

		[SerializeField]
		private bool bool_2;

		private ObscuredBool(int int_1)
		{
			byte_1 = byte_0;
			int_0 = int_1;
			bool_0 = false;
			bool_1 = false;
			bool_2 = true;
		}

		public static void SetNewCryptoKey(byte byte_2)
		{
			byte_0 = byte_2;
		}

		public void ApplyNewCryptoKey()
		{
			if (byte_1 != byte_0)
			{
				int_0 = Encrypt(InternalDecrypt(), byte_0);
				byte_1 = byte_0;
			}
		}

		public static int Encrypt(bool bool_3)
		{
			return Encrypt(bool_3, 0);
		}

		public static int Encrypt(bool bool_3, byte byte_2)
		{
			if (byte_2 == 0)
			{
				byte_2 = byte_0;
			}
			int num = ((!bool_3) ? 181 : 213);
			return num ^ byte_2;
		}

		public static bool Decrypt(int int_1)
		{
			return Decrypt(int_1, 0);
		}

		public static bool Decrypt(int int_1, byte byte_2)
		{
			if (byte_2 == 0)
			{
				byte_2 = byte_0;
			}
			int_1 ^= byte_2;
			return int_1 != 181;
		}

		public int GetEncrypted()
		{
			ApplyNewCryptoKey();
			return int_0;
		}

		public void SetEncrypted(int int_1)
		{
			bool_2 = true;
			int_0 = int_1;
			if (ObscuredCheatingDetector.bool_1)
			{
				bool_0 = InternalDecrypt();
				bool_1 = true;
			}
		}

		private bool InternalDecrypt()
		{
			if (!bool_2)
			{
				byte_1 = byte_0;
				int_0 = Encrypt(false);
				bool_0 = false;
				bool_1 = true;
				bool_2 = true;
			}
			byte b = byte_0;
			if (byte_1 != byte_0)
			{
				b = byte_1;
			}
			int num = int_0;
			num ^= b;
			bool flag = num != 181;
			if (ObscuredCheatingDetector.bool_1 && bool_1 && flag != bool_0)
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return flag;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredBool))
			{
				return false;
			}
			ObscuredBool obscuredBool = (ObscuredBool)obj;
			return int_0 == obscuredBool.int_0;
		}

		public bool Equals(ObscuredBool other)
		{
			return int_0 == other.int_0;
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public static implicit operator ObscuredBool(bool bool_3)
		{
			ObscuredBool result = new ObscuredBool(Encrypt(bool_3));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.bool_0 = bool_3;
				result.bool_1 = true;
			}
			return result;
		}

		public static implicit operator bool(ObscuredBool obscuredBool_0)
		{
			return obscuredBool_0.InternalDecrypt();
		}
	}
}
