using System.Collections.Generic;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class ResourceProfiler
	{
		private readonly IDictionary<Material, MaterialInfo> idictionary_0 = new Dictionary<Material, MaterialInfo>();

		private readonly IDictionary<Mesh, MeshInfo> idictionary_1 = new Dictionary<Mesh, MeshInfo>();

		public void Update()
		{
			Renderer[] array = Object.FindObjectsOfType<Renderer>();
			Renderer[] array2 = array;
			foreach (Renderer renderer in array2)
			{
				Material[] sharedMaterials = renderer.sharedMaterials;
				foreach (Material key in sharedMaterials)
				{
					MaterialInfo value = null;
					if (!idictionary_0.TryGetValue(key, out value))
					{
						value = new MaterialInfo();
						idictionary_0.Add(key, value);
					}
					value.AddRenderer(renderer);
				}
			}
			MeshFilter[] array3 = Object.FindObjectsOfType<MeshFilter>();
			MeshFilter[] array4 = array3;
			foreach (MeshFilter meshFilter in array4)
			{
				Mesh sharedMesh = meshFilter.sharedMesh;
				if (sharedMesh != null)
				{
					MeshInfo value2 = null;
					if (!idictionary_1.TryGetValue(sharedMesh, out value2))
					{
						value2 = new MeshInfo();
						idictionary_1.Add(sharedMesh, value2);
					}
					value2.AddMeshFilter(meshFilter);
				}
			}
		}
	}
}
