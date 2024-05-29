using System;
using UnityEngine;

public class CPMath
{
	public static Vector3 CalculateBezier(float float_0, Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2, Vector3 vector3_3)
	{
		float num = float_0 * float_0;
		float num2 = num * float_0;
		float num3 = 1f - float_0;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * vector3_0 + 3f * num4 * float_0 * vector3_1 + 3f * num3 * num * vector3_2 + num2 * vector3_3;
	}

	public static Vector3 CalculateHermite(Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2, Vector3 vector3_3, float float_0, float float_1, float float_2)
	{
		float num = float_0 * float_0;
		float num2 = num * float_0;
		Vector3 vector = (vector3_1 - vector3_0) * (1f + float_2) * (1f - float_1) / 2f;
		vector += (vector3_2 - vector3_1) * (1f - float_2) * (1f - float_1) / 2f;
		Vector3 vector2 = (vector3_2 - vector3_1) * (1f + float_2) * (1f - float_1) / 2f;
		vector2 += (vector3_3 - vector3_2) * (1f - float_2) * (1f - float_1) / 2f;
		float num3 = 2f * num2 - 3f * num + 1f;
		float num4 = num2 - 2f * num + float_0;
		float num5 = num2 - num;
		float num6 = -2f * num2 + 3f * num;
		return num3 * vector3_1 + num4 * vector + num5 * vector2 + num6 * vector3_2;
	}

	public static Vector3 CalculateCatmullRom(Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2, Vector3 vector3_3, float float_0)
	{
		float num = float_0 * float_0;
		Vector3 vector = -0.5f * vector3_0 + 1.5f * vector3_1 - 1.5f * vector3_2 + 0.5f * vector3_3;
		Vector3 vector2 = vector3_0 - 2.5f * vector3_1 + 2f * vector3_2 - 0.5f * vector3_3;
		Vector3 vector3 = -0.5f * vector3_0 + 0.5f * vector3_2;
		return vector * float_0 * num + vector2 * num + vector3 * float_0 + vector3_1;
	}

	public static Vector2 CalculateBezier(float float_0, Vector2 vector2_0, Vector2 vector2_1, Vector2 vector2_2, Vector2 vector2_3)
	{
		float num = float_0 * float_0;
		float num2 = num * float_0;
		float num3 = 1f - float_0;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * vector2_0 + 3f * num4 * float_0 * vector2_1 + 3f * num3 * num * vector2_2 + num2 * vector2_3;
	}

	public static Vector2 CalculateHermite(Vector2 vector2_0, Vector2 vector2_1, Vector2 vector2_2, Vector2 vector2_3, float float_0, float float_1, float float_2)
	{
		float num = float_0 * float_0;
		float num2 = num * float_0;
		Vector2 vector = (vector2_1 - vector2_0) * (1f + float_2) * (1f - float_1) / 2f;
		vector += (vector2_2 - vector2_1) * (1f - float_2) * (1f - float_1) / 2f;
		Vector2 vector2 = (vector2_2 - vector2_1) * (1f + float_2) * (1f - float_1) / 2f;
		vector2 += (vector2_3 - vector2_2) * (1f - float_2) * (1f - float_1) / 2f;
		float num3 = 2f * num2 - 3f * num + 1f;
		float num4 = num2 - 2f * num + float_0;
		float num5 = num2 - num;
		float num6 = -2f * num2 + 3f * num;
		return num3 * vector2_1 + num4 * vector + num5 * vector2 + num6 * vector2_2;
	}

	public static Vector2 CalculateCatmullRom(Vector2 vector2_0, Vector2 vector2_1, Vector2 vector2_2, Vector2 vector2_3, float float_0)
	{
		float num = float_0 * float_0;
		Vector2 vector = -0.5f * vector2_0 + 1.5f * vector2_1 - 1.5f * vector2_2 + 0.5f * vector2_3;
		Vector2 vector2 = vector2_0 - 2.5f * vector2_1 + 2f * vector2_2 - 0.5f * vector2_3;
		Vector2 vector3 = -0.5f * vector2_0 + 0.5f * vector2_2;
		return vector * float_0 * num + vector2 * num + vector3 * float_0 + vector2_1;
	}

	public static Quaternion CalculateCubic(Quaternion quaternion_0, Quaternion quaternion_1, Quaternion quaternion_2, Quaternion quaternion_3, float float_0)
	{
		if (Quaternion.Dot(quaternion_0, quaternion_3) < 0f)
		{
			quaternion_3 = new Quaternion(0f - quaternion_3.x, 0f - quaternion_3.y, 0f - quaternion_3.z, 0f - quaternion_3.w);
		}
		if (Quaternion.Dot(quaternion_0, quaternion_1) < 0f)
		{
			quaternion_1 = new Quaternion(0f - quaternion_1.x, 0f - quaternion_1.y, 0f - quaternion_1.z, 0f - quaternion_1.w);
		}
		if (Quaternion.Dot(quaternion_0, quaternion_2) < 0f)
		{
			quaternion_2 = new Quaternion(0f - quaternion_2.x, 0f - quaternion_2.y, 0f - quaternion_2.z, 0f - quaternion_2.w);
		}
		Quaternion quaternion_4 = SquadTangent(quaternion_1, quaternion_0, quaternion_3);
		Quaternion quaternion_5 = SquadTangent(quaternion_0, quaternion_3, quaternion_2);
		float float_ = 2f * float_0 * (1f - float_0);
		return Slerp(Slerp(quaternion_0, quaternion_3, float_0), Slerp(quaternion_4, quaternion_5, float_0), float_);
	}

	public static float CalculateCubic(float float_0, float float_1, float float_2, float float_3, float float_4)
	{
		float num = float_4 * float_4;
		float num2 = num * float_4;
		float num3 = 1f - float_4;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * float_0 + 3f * num4 * float_4 * float_3 + 3f * num3 * num * float_1 + num2 * float_2;
	}

	public static float CalculateHermite(float float_0, float float_1, float float_2, float float_3, float float_4, float float_5, float float_6)
	{
		float num = float_4 * float_4;
		float num2 = num * float_4;
		float num3 = (float_1 - float_0) * (1f + float_6) * (1f - float_5) / 2f;
		num3 += (float_2 - float_1) * (1f - float_6) * (1f - float_5) / 2f;
		float num4 = (float_2 - float_1) * (1f + float_6) * (1f - float_5) / 2f;
		num4 += (float_3 - float_2) * (1f - float_6) * (1f - float_5) / 2f;
		float num5 = 2f * num2 - 3f * num + 1f;
		float num6 = num2 - 2f * num + float_4;
		float num7 = num2 - num;
		float num8 = -2f * num2 + 3f * num;
		return num5 * float_1 + num6 * num3 + num7 * num4 + num8 * float_2;
	}

	public static float CalculateCatmullRom(float float_0, float float_1, float float_2, float float_3, float float_4)
	{
		float num = float_4 * float_4;
		float num2 = -0.5f * float_0 + 1.5f * float_1 - 1.5f * float_2 + 0.5f * float_3;
		float num3 = float_0 - 2.5f * float_1 + 2f * float_2 - 0.5f * float_3;
		float num4 = -0.5f * float_0 + 0.5f * float_2;
		return num2 * float_4 * num + num3 * num + num4 * float_4 + float_1;
	}

	public static float SmoothStep(float float_0)
	{
		return float_0 * float_0 * (3f - 2f * float_0);
	}

	public static Quaternion SquadTangent(Quaternion quaternion_0, Quaternion quaternion_1, Quaternion quaternion_2)
	{
		Quaternion quaternion = LnDif(quaternion_1, quaternion_0);
		Quaternion quaternion2 = LnDif(quaternion_1, quaternion_2);
		Quaternion identity = Quaternion.identity;
		for (int i = 0; i < 4; i++)
		{
			identity[i] = -0.25f * (quaternion[i] + quaternion2[i]);
		}
		return quaternion_1 * Exp(identity);
	}

	public static Quaternion LnDif(Quaternion quaternion_0, Quaternion quaternion_1)
	{
		Quaternion quaternion_2 = Quaternion.Inverse(quaternion_0) * quaternion_1;
		Normalize(quaternion_2);
		return Log(quaternion_2);
	}

	public static Quaternion Normalize(Quaternion quaternion_0)
	{
		float num = Mathf.Sqrt(quaternion_0.x * quaternion_0.x + quaternion_0.y * quaternion_0.y + quaternion_0.z * quaternion_0.z + quaternion_0.w * quaternion_0.w);
		if (num > 0f)
		{
			quaternion_0.x /= num;
			quaternion_0.y /= num;
			quaternion_0.z /= num;
			quaternion_0.w /= num;
		}
		else
		{
			quaternion_0.x = 0f;
			quaternion_0.y = 0f;
			quaternion_0.z = 0f;
			quaternion_0.w = 1f;
		}
		return quaternion_0;
	}

	public static Quaternion Exp(Quaternion quaternion_0)
	{
		float num = Mathf.Sqrt(quaternion_0[0] * quaternion_0[0] + quaternion_0[1] * quaternion_0[1] + quaternion_0[2] * quaternion_0[2]);
		if ((double)num < 1E-06)
		{
			return new Quaternion(quaternion_0[0], quaternion_0[1], quaternion_0[2], Mathf.Cos(num));
		}
		float num2 = Mathf.Sin(num) / num;
		return new Quaternion(quaternion_0[0] * num2, quaternion_0[1] * num2, quaternion_0[2] * num2, Mathf.Cos(num));
	}

	public static Quaternion Log(Quaternion quaternion_0)
	{
		float num = Mathf.Sqrt(quaternion_0[0] * quaternion_0[0] + quaternion_0[1] * quaternion_0[1] + quaternion_0[2] * quaternion_0[2]);
		if ((double)num < 1E-06)
		{
			return new Quaternion(quaternion_0[0], quaternion_0[1], quaternion_0[2], 0f);
		}
		float num2 = Mathf.Acos(quaternion_0[3]) / num;
		return new Quaternion(quaternion_0[0] * num2, quaternion_0[1] * num2, quaternion_0[2] * num2, 0f);
	}

	public static Quaternion Slerp(Quaternion quaternion_0, Quaternion quaternion_1, float float_0)
	{
		float num = Quaternion.Dot(quaternion_0, quaternion_1);
		Quaternion result = default(Quaternion);
		if (1f + num > 1E-05f)
		{
			float num5;
			float num6;
			if (1f - num > 1E-05f)
			{
				float num2 = Mathf.Acos(num);
				float num3 = Mathf.Sin(num2);
				float num4 = Mathf.Sign(num3) * 1f / num3;
				num5 = Mathf.Sin((1f - float_0) * num2) * num4;
				num6 = Mathf.Sin(float_0 * num2) * num4;
			}
			else
			{
				num5 = 1f - float_0;
				num6 = float_0;
			}
			result.x = num5 * quaternion_0.x + num6 * quaternion_1.x;
			result.y = num5 * quaternion_0.y + num6 * quaternion_1.y;
			result.z = num5 * quaternion_0.z + num6 * quaternion_1.z;
			result.w = num5 * quaternion_0.w + num6 * quaternion_1.w;
		}
		else
		{
			float num5 = Mathf.Sin((1f - float_0) * (float)Math.PI * 0.5f);
			float num6 = Mathf.Sin(float_0 * (float)Math.PI * 0.5f);
			result.x = num5 * quaternion_0.x - num6 * quaternion_0.y;
			result.y = num5 * quaternion_0.y + num6 * quaternion_0.x;
			result.z = num5 * quaternion_0.z - num6 * quaternion_0.w;
			result.w = quaternion_0.z;
		}
		return result;
	}

	public static Quaternion Nlerp(Quaternion quaternion_0, Quaternion quaternion_1, float float_0)
	{
		float num = 1f - float_0;
		Quaternion quaternion = default(Quaternion);
		quaternion.x = num * quaternion_0.x + float_0 * quaternion_1.x;
		quaternion.y = num * quaternion_0.y + float_0 * quaternion_1.y;
		quaternion.z = num * quaternion_0.z + float_0 * quaternion_1.z;
		quaternion.w = num * quaternion_0.w + float_0 * quaternion_1.w;
		Normalize(quaternion);
		return quaternion;
	}

	public static Quaternion GetQuatConjugate(Quaternion quaternion_0)
	{
		return new Quaternion(0f - quaternion_0.x, 0f - quaternion_0.y, 0f - quaternion_0.z, quaternion_0.w);
	}

	public static float SignedAngle(Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2)
	{
		Vector3 normalized = (vector3_1 - vector3_0).normalized;
		Vector3 rhs = Vector3.Cross(vector3_2, normalized);
		float f = Vector3.Dot(vector3_0, rhs);
		return Vector3.Angle(vector3_0, vector3_1) * Mathf.Sign(f);
	}

	public static float ClampAngle(float float_0, float float_1, float float_2)
	{
		if (float_0 < -360f)
		{
			float_0 += 360f;
		}
		if (float_0 > 360f)
		{
			float_0 -= 360f;
		}
		return Mathf.Clamp(float_0, 0f - float_2, 0f - float_1);
	}
}
