using System;
using UnityEngine;

[Serializable]
public class Triangles : MonoBehaviour
{
	[NonSerialized]
	public static Mesh[] mesh_0;

	[NonSerialized]
	public static int int_0;

	public static bool HasMeshes()
	{
		int result;
		if (mesh_0 == null)
		{
			result = 0;
		}
		else
		{
			int num = 0;
			Mesh[] array = mesh_0;
			int length = array.Length;
			while (true)
			{
				if (num < length)
				{
					if (!(null == array[num]))
					{
						num++;
						continue;
					}
					result = 0;
					break;
				}
				result = 1;
				break;
			}
		}
		return (byte)result != 0;
	}

	public static void Cleanup()
	{
		if (mesh_0 == null)
		{
			return;
		}
		int i = 0;
		Mesh[] array = mesh_0;
		for (int length = array.Length; i < length; i++)
		{
			if (null != array[i])
			{
				UnityEngine.Object.DestroyImmediate(array[i]);
				array[i] = null;
			}
		}
		mesh_0 = null;
	}

	public static Mesh[] GetMeshes(int int_1, int int_2)
	{
		Mesh[] result;
		if (HasMeshes() && int_0 == int_1 * int_2)
		{
			result = mesh_0;
		}
		else
		{
			int num = 21666;
			int num2 = (int_0 = int_1 * int_2);
			int num3 = Mathf.CeilToInt(1f * (float)num2 / 21666f);
			mesh_0 = new Mesh[num3];
			int num4 = 0;
			int num5 = 0;
			for (num4 = 0; num4 < num2; num4 += num)
			{
				int int_3 = Mathf.FloorToInt(Mathf.Clamp(num2 - num4, 0, num));
				mesh_0[num5] = GetMesh(int_3, num4, int_1, int_2);
				num5++;
			}
			result = mesh_0;
		}
		return result;
	}

	public static Mesh GetMesh(int int_1, int int_2, int int_3, int int_4)
	{
		Mesh mesh = new Mesh();
		mesh.hideFlags = HideFlags.DontSave;
		Vector3[] array = new Vector3[int_1 * 3];
		Vector2[] array2 = new Vector2[int_1 * 3];
		Vector2[] array3 = new Vector2[int_1 * 3];
		int[] array4 = new int[int_1 * 3];
		for (int i = 0; i < int_1; i++)
		{
			int num = i * 3;
			int num2 = int_2 + i;
			float num3 = Mathf.Floor(num2 % int_3) / (float)int_3;
			float num4 = Mathf.Floor(num2 / int_3) / (float)int_4;
			Vector3 vector = new Vector3(num3 * 2f - 1f, num4 * 2f - 1f, 1f);
			array[num + 0] = vector;
			array[num + 1] = vector;
			array[num + 2] = vector;
			array2[num + 0] = new Vector2(0f, 0f);
			array2[num + 1] = new Vector2(1f, 0f);
			array2[num + 2] = new Vector2(0f, 1f);
			array3[num + 0] = new Vector2(num3, num4);
			array3[num + 1] = new Vector2(num3, num4);
			array3[num + 2] = new Vector2(num3, num4);
			array4[num + 0] = num + 0;
			array4[num + 1] = num + 1;
			array4[num + 2] = num + 2;
		}
		mesh.vertices = array;
		mesh.triangles = array4;
		mesh.uv = array2;
		mesh.uv2 = array3;
		return mesh;
	}

	public virtual void Main()
	{
	}
}
