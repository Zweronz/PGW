using UnityEngine;

public class UIGeometry
{
	public BetterList<Vector3> betterList_0 = new BetterList<Vector3>();

	public BetterList<Vector2> betterList_1 = new BetterList<Vector2>();

	public BetterList<Color32> betterList_2 = new BetterList<Color32>();

	private BetterList<Vector3> betterList_3 = new BetterList<Vector3>();

	private Vector3 vector3_0;

	private Vector4 vector4_0;

	public bool Boolean_0
	{
		get
		{
			return betterList_0.size > 0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return betterList_3 != null && betterList_3.size > 0 && betterList_3.size == betterList_0.size;
		}
	}

	public void Clear()
	{
		betterList_0.Clear();
		betterList_1.Clear();
		betterList_2.Clear();
		betterList_3.Clear();
	}

	public void ApplyTransform(Matrix4x4 matrix4x4_0)
	{
		if (betterList_0.size > 0)
		{
			betterList_3.Clear();
			int i = 0;
			for (int size = betterList_0.size; i < size; i++)
			{
				betterList_3.Add(matrix4x4_0.MultiplyPoint3x4(betterList_0[i]));
			}
			vector3_0 = matrix4x4_0.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = matrix4x4_0.MultiplyVector(Vector3.right).normalized;
			vector4_0 = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
		}
		else
		{
			betterList_3.Clear();
		}
	}

	public void WriteToBuffers(BetterList<Vector3> betterList_4, BetterList<Vector2> betterList_5, BetterList<Color32> betterList_6, BetterList<Vector3> betterList_7, BetterList<Vector4> betterList_8)
	{
		if (betterList_3 == null || betterList_3.size <= 0)
		{
			return;
		}
		if (betterList_7 == null)
		{
			for (int i = 0; i < betterList_3.size; i++)
			{
				betterList_4.Add(betterList_3.buffer[i]);
				betterList_5.Add(betterList_1.buffer[i]);
				betterList_6.Add(betterList_2.buffer[i]);
			}
			return;
		}
		for (int j = 0; j < betterList_3.size; j++)
		{
			betterList_4.Add(betterList_3.buffer[j]);
			betterList_5.Add(betterList_1.buffer[j]);
			betterList_6.Add(betterList_2.buffer[j]);
			betterList_7.Add(vector3_0);
			betterList_8.Add(vector4_0);
		}
	}
}
