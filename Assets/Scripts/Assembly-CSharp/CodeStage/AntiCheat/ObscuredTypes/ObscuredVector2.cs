using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredVector2
	{
		[Serializable]
		public struct RawEncryptedVector2
		{
			public int x;

			public int y;
		}

		private static int int_0 = 120206;

		private static readonly Vector2 vector2_0 = Vector2.zero;

		[SerializeField]
		private int int_1;

		[SerializeField]
		private RawEncryptedVector2 rawEncryptedVector2_0;

		[SerializeField]
		private Vector2 vector2_1;

		[SerializeField]
		private bool bool_0;

		public float Single_0
		{
			get
			{
				float num = InternalDecryptField(rawEncryptedVector2_0.x);
				if (ObscuredCheatingDetector.bool_1 && !vector2_1.Equals(vector2_0) && Math.Abs(num - vector2_1.x) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_1)
				{
					ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				rawEncryptedVector2_0.x = InternalEncryptField(value);
				if (ObscuredCheatingDetector.bool_1)
				{
					vector2_1.x = value;
				}
			}
		}

		public float Single_1
		{
			get
			{
				float num = InternalDecryptField(rawEncryptedVector2_0.y);
				if (ObscuredCheatingDetector.bool_1 && !vector2_1.Equals(vector2_0) && Math.Abs(num - vector2_1.y) > ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_1)
				{
					ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				rawEncryptedVector2_0.y = InternalEncryptField(value);
				if (ObscuredCheatingDetector.bool_1)
				{
					vector2_1.y = value;
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
					throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
				case 1:
					return Single_1;
				case 0:
					return Single_0;
				}
			}
			set
			{
				switch (index)
				{
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
				case 1:
					Single_1 = value;
					break;
				case 0:
					Single_0 = value;
					break;
				}
			}
		}

		private ObscuredVector2(RawEncryptedVector2 rawEncryptedVector2_1)
		{
			int_1 = int_0;
			rawEncryptedVector2_0 = rawEncryptedVector2_1;
			vector2_1 = vector2_0;
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
				rawEncryptedVector2_0 = Encrypt(InternalDecrypt(), int_0);
				int_1 = int_0;
			}
		}

		public static RawEncryptedVector2 Encrypt(Vector2 vector2_2)
		{
			return Encrypt(vector2_2, 0);
		}

		public static RawEncryptedVector2 Encrypt(Vector2 vector2_2, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			RawEncryptedVector2 result = default(RawEncryptedVector2);
			result.x = ObscuredFloat.Encrypt(vector2_2.x, int_2);
			result.y = ObscuredFloat.Encrypt(vector2_2.y, int_2);
			return result;
		}

		public static Vector2 Decrypt(RawEncryptedVector2 rawEncryptedVector2_1)
		{
			return Decrypt(rawEncryptedVector2_1, 0);
		}

		public static Vector2 Decrypt(RawEncryptedVector2 rawEncryptedVector2_1, int int_2)
		{
			if (int_2 == 0)
			{
				int_2 = int_0;
			}
			Vector2 result = default(Vector2);
			result.x = ObscuredFloat.Decrypt(rawEncryptedVector2_1.x, int_2);
			result.y = ObscuredFloat.Decrypt(rawEncryptedVector2_1.y, int_2);
			return result;
		}

		public RawEncryptedVector2 GetEncrypted()
		{
			ApplyNewCryptoKey();
			return rawEncryptedVector2_0;
		}

		public void SetEncrypted(RawEncryptedVector2 rawEncryptedVector2_1)
		{
			bool_0 = true;
			rawEncryptedVector2_0 = rawEncryptedVector2_1;
			if (ObscuredCheatingDetector.bool_1)
			{
				vector2_1 = InternalDecrypt();
			}
		}

		private Vector2 InternalDecrypt()
		{
			if (!bool_0)
			{
				int_1 = int_0;
				rawEncryptedVector2_0 = Encrypt(vector2_0);
				vector2_1 = vector2_0;
				bool_0 = true;
			}
			int int_ = int_0;
			if (int_1 != int_0)
			{
				int_ = int_1;
			}
			Vector2 vector = default(Vector2);
			vector.x = ObscuredFloat.Decrypt(rawEncryptedVector2_0.x, int_);
			vector.y = ObscuredFloat.Decrypt(rawEncryptedVector2_0.y, int_);
			if (ObscuredCheatingDetector.bool_1 && !vector2_1.Equals(vector2_0) && !CompareVectorsWithTolerance(vector, vector2_1))
			{
				ObscuredCheatingDetector.ObscuredCheatingDetector_0.OnCheatingDetected();
			}
			return vector;
		}

		private bool CompareVectorsWithTolerance(Vector2 vector2_2, Vector2 vector2_3)
		{
			float float_ = ObscuredCheatingDetector.ObscuredCheatingDetector_0.float_1;
			return Math.Abs(vector2_2.x - vector2_3.x) < float_ && Math.Abs(vector2_2.y - vector2_3.y) < float_;
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

		public static implicit operator ObscuredVector2(Vector2 vector2_2)
		{
			ObscuredVector2 result = new ObscuredVector2(Encrypt(vector2_2));
			if (ObscuredCheatingDetector.bool_1)
			{
				result.vector2_1 = vector2_2;
			}
			return result;
		}

		public static implicit operator Vector2(ObscuredVector2 obscuredVector2_0)
		{
			return obscuredVector2_0.InternalDecrypt();
		}
	}
}
