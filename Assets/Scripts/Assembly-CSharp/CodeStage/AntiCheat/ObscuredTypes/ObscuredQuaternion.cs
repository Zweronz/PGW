using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredQuaternion
	{
		[Serializable]
		public struct RawEncryptedQuaternion
		{
			public int x;

			public int y;

			public int z;

			public int w;
		}

		private static int int_0 = 120205;

		private static readonly Quaternion quaternion_0 = Quaternion.identity;

		[SerializeField]
		private int int_1;

		[SerializeField]
		private RawEncryptedQuaternion rawEncryptedQuaternion_0;

		[SerializeField]
		private Quaternion quaternion_1;

		[SerializeField]
		private bool bool_0;

		private ObscuredQuaternion(RawEncryptedQuaternion rawEncryptedQuaternion_1)
		{
			int_1 = int_0;
			rawEncryptedQuaternion_0 = rawEncryptedQuaternion_1;
			quaternion_1 = quaternion_0;
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
				rawEncryptedQuaternion_0 = Encrypt(InternalDecrypt(), int_0);
				int_1 = int_0;
			}
		}

		public static RawEncryptedQuaternion Encrypt(Quaternion quaternion_2)
		{
			return Encrypt(quaternion_2, 0);
		}

		public static RawEncryptedQuaternion Encrypt(Quaternion quaternion_2, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			RawEncryptedQuaternion result = default(RawEncryptedQuaternion);
			result.x = ObscuredFloat.Encrypt(quaternion_2.x, int_2);
			result.y = ObscuredFloat.Encrypt(quaternion_2.y, int_2);
			result.z = ObscuredFloat.Encrypt(quaternion_2.z, int_2);
			result.w = ObscuredFloat.Encrypt(quaternion_2.w, int_2);
			return result;
		}

		public static Quaternion Decrypt(RawEncryptedQuaternion rawEncryptedQuaternion_1)
		{
			return Decrypt(rawEncryptedQuaternion_1, 0);
		}

		public static Quaternion Decrypt(RawEncryptedQuaternion rawEncryptedQuaternion_1, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			Quaternion result = default(Quaternion);
			result.x = ObscuredFloat.Decrypt(rawEncryptedQuaternion_1.x, int_2);
			result.y = ObscuredFloat.Decrypt(rawEncryptedQuaternion_1.y, int_2);
			result.z = ObscuredFloat.Decrypt(rawEncryptedQuaternion_1.z, int_2);
			result.w = ObscuredFloat.Decrypt(rawEncryptedQuaternion_1.w, int_2);
			return result;
		}

		public RawEncryptedQuaternion GetEncrypted()
		{
			ApplyNewCryptoKey();
			return rawEncryptedQuaternion_0;
		}

		public void SetEncrypted(RawEncryptedQuaternion rawEncryptedQuaternion_1)
		{
			bool_0 = true;
			rawEncryptedQuaternion_0 = rawEncryptedQuaternion_1;
			if (ObscuredCheatingDetector.bool_1)
			{
				quaternion_1 = InternalDecrypt();
			}
		}

		private Quaternion InternalDecrypt()
		{
			if (!bool_0)
			{
				int_1 = int_0;
				rawEncryptedQuaternion_0 = Encrypt(quaternion_0);
				quaternion_1 = quaternion_0;
				bool_0 = true;
			}
			int int_ = int_0;
			if (int_1 != int_0)
			{
				int_ = int_1;
			}
			Quaternion quaternion = default(Quaternion);
			quaternion.x = ObscuredFloat.Decrypt(rawEncryptedQuaternion_0.x, int_);
			quaternion.y = ObscuredFloat.Decrypt(rawEncryptedQuaternion_0.y, int_);
			quaternion.z = ObscuredFloat.Decrypt(rawEncryptedQuaternion_0.z, int_);
			quaternion.w = ObscuredFloat.Decrypt(rawEncryptedQuaternion_0.w, int_);
			if (ObscuredCheatingDetector.bool_1 && !quaternion_1.Equals(quaternion_0) && !CompareQuaternionsWithTolerance(quaternion, quaternion_1))
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return quaternion;
		}

		private bool CompareQuaternionsWithTolerance(Quaternion quaternion_2, Quaternion quaternion_3)
		{
			float float_ = ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_3;
			return Math.Abs(quaternion_2.x - quaternion_3.x) < float_ && Math.Abs(quaternion_2.y - quaternion_3.y) < float_ && Math.Abs(quaternion_2.z - quaternion_3.z) < float_ && Math.Abs(quaternion_2.w - quaternion_3.w) < float_;
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

		public static implicit operator ObscuredQuaternion(Quaternion quaternion_2)
		{
			ObscuredQuaternion result = new ObscuredQuaternion(Encrypt(quaternion_2));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.quaternion_1 = quaternion_2;
			}
			return result;
		}

		public static implicit operator Quaternion(ObscuredQuaternion obscuredQuaternion_0)
		{
			return obscuredQuaternion_0.InternalDecrypt();
		}
	}
}
