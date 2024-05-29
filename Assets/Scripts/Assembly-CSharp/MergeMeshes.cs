using System.Collections.Generic;
using UnityEngine;

public class MergeMeshes : MonoBehaviour
{
	public enum PostMerge
	{
		DisableRenderers = 0,
		DestroyRenderers = 1,
		DisableGameObjects = 2,
		DestroyGameObjects = 3
	}

	public Material material;

	public PostMerge afterMerging;

	private string string_0;

	private Transform transform_0;

	private Mesh mesh_0;

	private MeshFilter meshFilter_0;

	private MeshRenderer meshRenderer_0;

	private List<GameObject> list_0 = new List<GameObject>();

	private List<Renderer> list_1 = new List<Renderer>();

	private bool bool_0 = true;

	private void Start()
	{
		if (bool_0)
		{
			Merge(true);
		}
		base.enabled = false;
	}

	private void Update()
	{
		if (bool_0)
		{
			Merge(true);
		}
		base.enabled = false;
	}

	public void Merge(bool bool_1)
	{
		if (!bool_1)
		{
			bool_0 = true;
			base.enabled = true;
			return;
		}
		bool_0 = false;
		string_0 = base.name;
		meshFilter_0 = GetComponent<MeshFilter>();
		transform_0 = base.transform;
		Clear();
		MeshFilter[] componentsInChildren = GetComponentsInChildren<MeshFilter>();
		if (componentsInChildren.Length == 0 || (meshFilter_0 != null && componentsInChildren.Length == 1))
		{
			return;
		}
		GameObject gameObject = base.gameObject;
		Matrix4x4 worldToLocalMatrix = gameObject.transform.worldToLocalMatrix;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		MeshFilter[] array = componentsInChildren;
		foreach (MeshFilter meshFilter in array)
		{
			if (meshFilter == meshFilter_0)
			{
				continue;
			}
			if (meshFilter.gameObject.isStatic)
			{
				Debug.LogError("MergeMeshes can't merge objects marked as static", meshFilter.gameObject);
				continue;
			}
			Mesh sharedMesh = meshFilter.sharedMesh;
			if (material == null)
			{
				material = meshFilter.GetComponent<Renderer>().sharedMaterial;
			}
			num += sharedMesh.vertexCount;
			num2 += sharedMesh.triangles.Length;
			if (sharedMesh.normals != null)
			{
				num3 += sharedMesh.normals.Length;
			}
			if (sharedMesh.tangents != null)
			{
				num4 += sharedMesh.tangents.Length;
			}
			if (sharedMesh.colors != null)
			{
				num5 += sharedMesh.colors.Length;
			}
			if (sharedMesh.uv != null)
			{
				num6 += sharedMesh.uv.Length;
			}
			if (sharedMesh.uv2 != null)
			{
				num7 += sharedMesh.uv2.Length;
			}
		}
		if (num == 0)
		{
			Debug.LogWarning("Unable to find any non-static objects to merge", this);
			return;
		}
		Vector3[] array2 = new Vector3[num];
		int[] array3 = new int[num2];
		Vector2[] array4 = ((num6 != num) ? null : new Vector2[num]);
		Vector2[] array5 = ((num7 != num) ? null : new Vector2[num]);
		Vector3[] array6 = ((num3 != num) ? null : new Vector3[num]);
		Vector4[] array7 = ((num4 != num) ? null : new Vector4[num]);
		Color[] array8 = ((num5 != num) ? null : new Color[num]);
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		MeshFilter[] array9 = componentsInChildren;
		foreach (MeshFilter meshFilter2 in array9)
		{
			if (meshFilter2 == meshFilter_0 || meshFilter2.gameObject.isStatic)
			{
				continue;
			}
			Mesh sharedMesh2 = meshFilter2.sharedMesh;
			if (sharedMesh2.vertexCount == 0)
			{
				continue;
			}
			Matrix4x4 localToWorldMatrix = meshFilter2.transform.localToWorldMatrix;
			Renderer renderer = meshFilter2.GetComponent<Renderer>();
			if (afterMerging != PostMerge.DestroyRenderers)
			{
				renderer.enabled = false;
				list_1.Add(renderer);
			}
			if (afterMerging == PostMerge.DisableGameObjects)
			{
				GameObject gameObject2 = meshFilter2.gameObject;
				Transform parent = gameObject2.transform;
				while (parent != transform_0)
				{
					if (!(parent.GetComponent<Rigidbody>() != null))
					{
						parent = parent.parent;
						continue;
					}
					gameObject2 = parent.gameObject;
					break;
				}
				list_0.Add(gameObject2);
				TWTools.SetActive(gameObject2, false);
			}
			Vector3[] vertices = sharedMesh2.vertices;
			Vector3[] array10 = ((array6 == null) ? null : sharedMesh2.normals);
			Vector4[] array11 = ((array7 == null) ? null : sharedMesh2.tangents);
			Vector2[] array12 = ((array4 == null) ? null : sharedMesh2.uv);
			Vector2[] array13 = ((array5 == null) ? null : sharedMesh2.uv2);
			Color[] array14 = ((array8 == null) ? null : sharedMesh2.colors);
			int[] triangles = sharedMesh2.triangles;
			int k = 0;
			for (int num11 = vertices.Length; k < num11; k++)
			{
				array2[num10] = worldToLocalMatrix.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(vertices[k]));
				if (array6 != null)
				{
					array6[num10] = worldToLocalMatrix.MultiplyVector(localToWorldMatrix.MultiplyVector(array10[k]));
				}
				if (array8 != null)
				{
					array8[num10] = array14[k];
				}
				if (array4 != null)
				{
					array4[num10] = array12[k];
				}
				if (array5 != null)
				{
					array5[num10] = array13[k];
				}
				if (array7 != null)
				{
					Vector4 vector = array11[k];
					Vector3 v = new Vector3(vector.x, vector.y, vector.z);
					v = worldToLocalMatrix.MultiplyVector(localToWorldMatrix.MultiplyVector(v));
					vector.x = v.x;
					vector.y = v.y;
					vector.z = v.z;
					array7[num10] = vector;
				}
				num10++;
			}
			int l = 0;
			for (int num12 = triangles.Length; l < num12; l++)
			{
				array3[num9++] = num8 + triangles[l];
			}
			num8 = num10;
			if (afterMerging == PostMerge.DestroyGameObjects)
			{
				Object.Destroy(meshFilter2.gameObject);
			}
			else if (afterMerging == PostMerge.DestroyRenderers)
			{
				Object.Destroy(renderer);
			}
		}
		if (afterMerging == PostMerge.DestroyGameObjects)
		{
			componentsInChildren = null;
			list_0.Clear();
		}
		if (array2.Length > 0)
		{
			if (mesh_0 == null)
			{
				mesh_0 = new Mesh();
				mesh_0.hideFlags = HideFlags.DontSave;
			}
			else
			{
				mesh_0.Clear();
			}
			mesh_0.name = string_0;
			mesh_0.vertices = array2;
			mesh_0.normals = array6;
			mesh_0.tangents = array7;
			mesh_0.colors = array8;
			mesh_0.uv = array4;
			mesh_0.uv2 = array5;
			mesh_0.triangles = array3;
			mesh_0.RecalculateBounds();
			if (meshFilter_0 == null)
			{
				meshFilter_0 = gameObject.AddComponent<MeshFilter>();
				meshFilter_0.mesh = mesh_0;
			}
			if (meshRenderer_0 == null)
			{
				meshRenderer_0 = gameObject.AddComponent<MeshRenderer>();
			}
			meshRenderer_0.sharedMaterial = material;
			meshRenderer_0.enabled = true;
			gameObject.name = string_0 + " (" + array3.Length / 3 + " tri)";
		}
		else
		{
			Release();
		}
		base.enabled = false;
	}

	public void Clear()
	{
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			GameObject gameObject = list_0[i];
			if ((bool)gameObject)
			{
				TWTools.SetActive(gameObject, true);
			}
		}
		int j = 0;
		for (int count2 = list_1.Count; j < count2; j++)
		{
			Renderer renderer = list_1[j];
			if ((bool)renderer)
			{
				renderer.enabled = true;
			}
		}
		list_0.Clear();
		list_1.Clear();
		if (meshRenderer_0 != null)
		{
			meshRenderer_0.enabled = false;
		}
	}

	public void Release()
	{
		Clear();
		TWTools.Destroy(meshRenderer_0);
		TWTools.Destroy(meshFilter_0);
		TWTools.Destroy(mesh_0);
		meshFilter_0 = null;
		mesh_0 = null;
		meshRenderer_0 = null;
	}
}
