using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredVector3
	{
		[Serializable]
		public struct RawEncryptedVector3
		{
			public int x;

			public int y;

			public int z;
		}

		private static int int_0 = 120207;

		private static readonly Vector3 vector3_0 = Vector3.zero;

		[SerializeField]
		private int int_1;

		[SerializeField]
		private RawEncryptedVector3 rawEncryptedVector3_0;

		[SerializeField]
		private Vector3 vector3_1;

		[SerializeField]
		private bool bool_0;

		public float Single_0
		{
			get
			{
				float num = InternalDecryptField(rawEncryptedVector3_0.x);
				if (ObscuredCheatingDetector.bool_1 && !vector3_1.Equals(vector3_0) && Math.Abs(num - vector3_1.x) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_2)
				{
					ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				rawEncryptedVector3_0.x = InternalEncryptField(value);
				if (ObscuredCheatingDetector.bool_1)
				{
					vector3_1.x = value;
				}
			}
		}

		public float Single_1
		{
			get
			{
				float num = InternalDecryptField(rawEncryptedVector3_0.y);
				if (ObscuredCheatingDetector.bool_1 && !vector3_1.Equals(vector3_0) && Math.Abs(num - vector3_1.y) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_2)
				{
					ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				rawEncryptedVector3_0.y = InternalEncryptField(value);
				if (ObscuredCheatingDetector.bool_1)
				{
					vector3_1.y = value;
				}
			}
		}

		public float Single_2
		{
			get
			{
				float num = InternalDecryptField(rawEncryptedVector3_0.z);
				if (ObscuredCheatingDetector.bool_1 && !vector3_1.Equals(vector3_0) && Math.Abs(num - vector3_1.z) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_2)
				{
					ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				rawEncryptedVector3_0.z = InternalEncryptField(value);
				if (ObscuredCheatingDetector.bool_1)
				{
					vector3_1.z = value;
				}
			}
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				case 0:
					return Single_0;
				case 1:
					return Single_1;
				case 2:
					return Single_2;
				}
			}
			set
			{
				switch (index)
				{
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				case 0:
					Single_0 = value;
					break;
				case 1:
					Single_1 = value;
					break;
				case 2:
					Single_2 = value;
					break;
				}
			}
		}

		private ObscuredVector3(RawEncryptedVector3 rawEncryptedVector3_1)
		{
			int_1 = int_0;
			rawEncryptedVector3_0 = rawEncryptedVector3_1;
			vector3_1 = vector3_0;
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
				rawEncryptedVector3_0 = Encrypt(InternalDecrypt(), int_0);
				int_1 = int_0;
			}
		}

		public static RawEncryptedVector3 Encrypt(Vector3 vector3_2)
		{
			return Encrypt(vector3_2, 0);
		}

		public static RawEncryptedVector3 Encrypt(Vector3 vector3_2, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			RawEncryptedVector3 result = default(RawEncryptedVector3);
			result.x = ObscuredFloat.Encrypt(vector3_2.x, int_2);
			result.y = ObscuredFloat.Encrypt(vector3_2.y, int_2);
			result.z = ObscuredFloat.Encrypt(vector3_2.z, int_2);
			return result;
		}

		public static Vector3 Decrypt(RawEncryptedVector3 rawEncryptedVector3_1)
		{
			return Decrypt(rawEncryptedVector3_1, 0);
		}

		public static Vector3 Decrypt(RawEncryptedVector3 rawEncryptedVector3_1, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			Vector3 result = default(Vector3);
			result.x = ObscuredFloat.Decrypt(rawEncryptedVector3_1.x, int_2);
			result.y = ObscuredFloat.Decrypt(rawEncryptedVector3_1.y, int_2);
			result.z = ObscuredFloat.Decrypt(rawEncryptedVector3_1.z, int_2);
			return result;
		}

		public RawEncryptedVector3 GetEncrypted()
		{
			ApplyNewCryptoKey();
			return rawEncryptedVector3_0;
		}

		public void SetEncrypted(RawEncryptedVector3 rawEncryptedVector3_1)
		{
			bool_0 = true;
			rawEncryptedVector3_0 = rawEncryptedVector3_1;
			if (ObscuredCheatingDetector.bool_1)
			{
				vector3_1 = InternalDecrypt();
			}
		}

		private Vector3 InternalDecrypt()
		{
			if (!bool_0)
			{
				int_1 = int_0;
				rawEncryptedVector3_0 = Encrypt(vector3_0, int_0);
				vector3_1 = vector3_0;
				bool_0 = true;
			}
			int int_ = int_0;
			if (int_1 != int_0)
			{
				int_ = int_1;
			}
			Vector3 vector = default(Vector3);
			vector.x = ObscuredFloat.Decrypt(rawEncryptedVector3_0.x, int_);
			vector.y = ObscuredFloat.Decrypt(rawEncryptedVector3_0.y, int_);
			vector.z = ObscuredFloat.Decrypt(rawEncryptedVector3_0.z, int_);
			if (ObscuredCheatingDetector.bool_1 && !vector3_1.Equals(Vector3.zero) && !CompareVectorsWithTolerance(vector, vector3_1))
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return vector;
		}

		private bool CompareVectorsWithTolerance(Vector3 vector3_2, Vector3 vector3_3)
		{
			float float_ = ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_2;
			return Math.Abs(vector3_2.x - vector3_3.x) < float_ && Math.Abs(vector3_2.y - vector3_3.y) < float_ && Math.Abs(vector3_2.z - vector3_3.z) < float_;
		}

		private float InternalDecryptField(int int_2)
		{
			int int_3 = int_0;
			if (int_1 != int_0)
			{
				int_3 = int_1;
			}
			return ObscuredFloat.Decrypt(int_2, int_3);
		}

		private int InternalEncryptField(float float_0)
		{
			return ObscuredFloat.Encrypt(float_0, int_0);
		}

		public override bool Equals(object obj)
		{
			return InternalDecrypt().Equals(obj);
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

		public static implicit operator ObscuredVector3(Vector3 vector3_2)
		{
			ObscuredVector3 result = new ObscuredVector3(Encrypt(vector3_2, int_0));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.vector3_1 = vector3_2;
			}
			return result;
		}

		public static implicit operator Vector3(ObscuredVector3 obscuredVector3_0)
		{
			return obscuredVector3_0.InternalDecrypt();
		}

		public static ObscuredVector3 operator +(ObscuredVector3 obscuredVector3_0, ObscuredVector3 obscuredVector3_1)
		{
			return obscuredVector3_0.InternalDecrypt() + obscuredVector3_1.InternalDecrypt();
		}

		public static ObscuredVector3 operator +(Vector3 vector3_2, ObscuredVector3 obscuredVector3_0)
		{
			return vector3_2 + obscuredVector3_0.InternalDecrypt();
		}

		public static ObscuredVector3 operator +(ObscuredVector3 obscuredVector3_0, Vector3 vector3_2)
		{
			return obscuredVector3_0.InternalDecrypt() + vector3_2;
		}

		public static ObscuredVector3 operator -(ObscuredVector3 obscuredVector3_0, ObscuredVector3 obscuredVector3_1)
		{
			return obscuredVector3_0.InternalDecrypt() - obscuredVector3_1.InternalDecrypt();
		}

		public static ObscuredVector3 operator -(Vector3 vector3_2, ObscuredVector3 obscuredVector3_0)
		{
			return vector3_2 - obscuredVector3_0.InternalDecrypt();
		}

		public static ObscuredVector3 operator -(ObscuredVector3 obscuredVector3_0, Vector3 vector3_2)
		{
			return obscuredVector3_0.InternalDecrypt() - vector3_2;
		}

		public static ObscuredVector3 operator -(ObscuredVector3 obscuredVector3_0)
		{
			return -obscuredVector3_0.InternalDecrypt();
		}

		public static ObscuredVector3 operator *(ObscuredVector3 obscuredVector3_0, float float_0)
		{
			return obscuredVector3_0.InternalDecrypt() * float_0;
		}

		public static ObscuredVector3 operator *(float float_0, ObscuredVector3 obscuredVector3_0)
		{
			return float_0 * obscuredVector3_0.InternalDecrypt();
		}

		public static ObscuredVector3 operator /(ObscuredVector3 obscuredVector3_0, float float_0)
		{
			return obscuredVector3_0.InternalDecrypt() / float_0;
		}

		public static bool operator ==(ObscuredVector3 obscuredVector3_0, ObscuredVector3 obscuredVector3_1)
		{
			return obscuredVector3_0.InternalDecrypt() == obscuredVector3_1.InternalDecrypt();
		}

		public static bool operator ==(Vector3 vector3_2, ObscuredVector3 obscuredVector3_0)
		{
			return vector3_2 == obscuredVector3_0.InternalDecrypt();
		}

		public static bool operator ==(ObscuredVector3 obscuredVector3_0, Vector3 vector3_2)
		{
			return obscuredVector3_0.InternalDecrypt() == vector3_2;
		}

		public static bool operator !=(ObscuredVector3 obscuredVector3_0, ObscuredVector3 obscuredVector3_1)
		{
			return obscuredVector3_0.InternalDecrypt() != obscuredVector3_1.InternalDecrypt();
		}

		public static bool operator !=(Vector3 vector3_2, ObscuredVector3 obscuredVector3_0)
		{
			return vector3_2 != obscuredVector3_0.InternalDecrypt();
		}

		public static bool operator !=(ObscuredVector3 obscuredVector3_0, Vector3 vector3_2)
		{
			return obscuredVector3_0.InternalDecrypt() != vector3_2;
		}
	}
}
