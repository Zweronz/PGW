using UnityEngine;

public class MeshCombineUtility
{
	public struct MeshInstance
	{
		public Mesh mesh_0;

		public int int_0;

		public Matrix4x4 matrix4x4_0;
	}

	public static Mesh Combine(MeshInstance[] meshInstance_0, bool bool_0)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < meshInstance_0.Length; i++)
		{
			MeshInstance meshInstance = meshInstance_0[i];
			if (!meshInstance.mesh_0)
			{
				continue;
			}
			num += meshInstance.mesh_0.vertexCount;
			if (!bool_0)
			{
				continue;
			}
			int num4 = meshInstance.mesh_0.GetTriangles(meshInstance.int_0).Length;
			if (num4 != 0)
			{
				if (num3 != 0)
				{
					num3 = (((num3 & 1) != 1) ? (num3 + 2) : (num3 + 3));
				}
				num3 += num4;
			}
			else
			{
				bool_0 = false;
			}
		}
		if (!bool_0)
		{
			for (int j = 0; j < meshInstance_0.Length; j++)
			{
				MeshInstance meshInstance2 = meshInstance_0[j];
				if ((bool)meshInstance2.mesh_0)
				{
					num2 += meshInstance2.mesh_0.GetTriangles(meshInstance2.int_0).Length;
				}
			}
		}
		Vector3[] array = new Vector3[num];
		Vector3[] array2 = new Vector3[num];
		Vector4[] array3 = new Vector4[num];
		Vector2[] array4 = new Vector2[num];
		Vector2[] array5 = new Vector2[num];
		Color[] array6 = new Color[num];
		int[] array7 = new int[num2];
		int[] array8 = new int[num3];
		int int_ = 0;
		for (int k = 0; k < meshInstance_0.Length; k++)
		{
			MeshInstance meshInstance3 = meshInstance_0[k];
			if ((bool)meshInstance3.mesh_0)
			{
				Copy(meshInstance3.mesh_0.vertexCount, meshInstance3.mesh_0.vertices, array, ref int_, meshInstance3.matrix4x4_0);
			}
		}
		int_ = 0;
		for (int l = 0; l < meshInstance_0.Length; l++)
		{
			MeshInstance meshInstance4 = meshInstance_0[l];
			if ((bool)meshInstance4.mesh_0)
			{
				Matrix4x4 matrix4x4_ = meshInstance4.matrix4x4_0;
				matrix4x4_ = matrix4x4_.inverse.transpose;
				CopyNormal(meshInstance4.mesh_0.vertexCount, meshInstance4.mesh_0.normals, array2, ref int_, matrix4x4_);
			}
		}
		int_ = 0;
		for (int m = 0; m < meshInstance_0.Length; m++)
		{
			MeshInstance meshInstance5 = meshInstance_0[m];
			if ((bool)meshInstance5.mesh_0)
			{
				Matrix4x4 matrix4x4_2 = meshInstance5.matrix4x4_0;
				matrix4x4_2 = matrix4x4_2.inverse.transpose;
				CopyTangents(meshInstance5.mesh_0.vertexCount, meshInstance5.mesh_0.tangents, array3, ref int_, matrix4x4_2);
			}
		}
		int_ = 0;
		for (int n = 0; n < meshInstance_0.Length; n++)
		{
			MeshInstance meshInstance6 = meshInstance_0[n];
			if ((bool)meshInstance6.mesh_0)
			{
				Copy(meshInstance6.mesh_0.vertexCount, meshInstance6.mesh_0.uv, array4, ref int_);
			}
		}
		int_ = 0;
		for (int num5 = 0; num5 < meshInstance_0.Length; num5++)
		{
			MeshInstance meshInstance7 = meshInstance_0[num5];
			if ((bool)meshInstance7.mesh_0)
			{
				Copy(meshInstance7.mesh_0.vertexCount, meshInstance7.mesh_0.uv2, array5, ref int_);
			}
		}
		int_ = 0;
		for (int num6 = 0; num6 < meshInstance_0.Length; num6++)
		{
			MeshInstance meshInstance8 = meshInstance_0[num6];
			if ((bool)meshInstance8.mesh_0)
			{
				CopyColors(meshInstance8.mesh_0.vertexCount, meshInstance8.mesh_0.colors, array6, ref int_);
			}
		}
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		for (int num10 = 0; num10 < meshInstance_0.Length; num10++)
		{
			MeshInstance meshInstance9 = meshInstance_0[num10];
			if (!meshInstance9.mesh_0)
			{
				continue;
			}
			if (bool_0)
			{
				int[] triangleStrip = meshInstance9.mesh_0.GetTriangles(meshInstance9.int_0);
				if (num8 != 0)
				{
					if ((num8 & 1) == 1)
					{
						array8[num8] = array8[num8 - 1];
						array8[num8 + 1] = triangleStrip[0] + num9;
						array8[num8 + 2] = triangleStrip[0] + num9;
						num8 += 3;
					}
					else
					{
						array8[num8] = array8[num8 - 1];
						array8[num8 + 1] = triangleStrip[0] + num9;
						num8 += 2;
					}
				}
				for (int num11 = 0; num11 < triangleStrip.Length; num11++)
				{
					array8[num11 + num8] = triangleStrip[num11] + num9;
				}
				num8 += triangleStrip.Length;
			}
			else
			{
				int[] triangles = meshInstance9.mesh_0.GetTriangles(meshInstance9.int_0);
				for (int num12 = 0; num12 < triangles.Length; num12++)
				{
					array7[num12 + num7] = triangles[num12] + num9;
				}
				num7 += triangles.Length;
			}
			num9 += meshInstance9.mesh_0.vertexCount;
		}
		Mesh mesh = new Mesh();
		mesh.name = "Combined Mesh";
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.colors = array6;
		mesh.uv = array4;
		mesh.uv2 = array5;
		mesh.tangents = array3;
		if (bool_0)
		{
			mesh.SetTriangles(array8, 0);
		}
		else
		{
			mesh.triangles = array7;
		}
		return mesh;
	}

	private static void Copy(int int_0, Vector3[] vector3_0, Vector3[] vector3_1, ref int int_1, Matrix4x4 matrix4x4_0)
	{
		for (int i = 0; i < vector3_0.Length; i++)
		{
			vector3_1[i + int_1] = matrix4x4_0.MultiplyPoint(vector3_0[i]);
		}
		int_1 += int_0;
	}

	private static void CopyNormal(int int_0, Vector3[] vector3_0, Vector3[] vector3_1, ref int int_1, Matrix4x4 matrix4x4_0)
	{
		for (int i = 0; i < vector3_0.Length; i++)
		{
			vector3_1[i + int_1] = matrix4x4_0.MultiplyVector(vector3_0[i]).normalized;
		}
		int_1 += int_0;
	}

	private static void Copy(int int_0, Vector2[] vector2_0, Vector2[] vector2_1, ref int int_1)
	{
		for (int i = 0; i < vector2_0.Length; i++)
		{
			vector2_1[i + int_1] = vector2_0[i];
		}
		int_1 += int_0;
	}

	private static void CopyColors(int int_0, Color[] color_0, Color[] color_1, ref int int_1)
	{
		for (int i = 0; i < color_0.Length; i++)
		{
			color_1[i + int_1] = color_0[i];
		}
		int_1 += int_0;
	}

	private static void CopyTangents(int int_0, Vector4[] vector4_0, Vector4[] vector4_1, ref int int_1, Matrix4x4 matrix4x4_0)
	{
		for (int i = 0; i < vector4_0.Length; i++)
		{
			Vector4 vector = vector4_0[i];
			Vector3 v = new Vector3(vector.x, vector.y, vector.z);
			v = matrix4x4_0.MultiplyVector(v).normalized;
			vector4_1[i + int_1] = new Vector4(v.x, v.y, v.z, vector.w);
		}
		int_1 += int_0;
	}
}
