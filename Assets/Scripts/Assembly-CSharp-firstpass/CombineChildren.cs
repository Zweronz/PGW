using System;
using System.Collections;
using UnityEngine;

public class CombineChildren : MonoBehaviour
{
	public bool generateTriangleStrips = true;

	private void Start()
	{
		Component[] componentsInChildren = GetComponentsInChildren(typeof(MeshFilter));
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshFilter meshFilter = (MeshFilter)componentsInChildren[i];
			Renderer renderer = componentsInChildren[i].GetComponent<Renderer>();
			MeshCombineUtility.MeshInstance meshInstance = default(MeshCombineUtility.MeshInstance);
			meshInstance.mesh_0 = meshFilter.sharedMesh;
			if (!(renderer != null) || !renderer.enabled || !(meshInstance.mesh_0 != null))
			{
				continue;
			}
			meshInstance.matrix4x4_0 = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
			Material[] sharedMaterials = renderer.sharedMaterials;
			for (int j = 0; j < sharedMaterials.Length; j++)
			{
				meshInstance.int_0 = Math.Min(j, meshInstance.mesh_0.subMeshCount - 1);
				ArrayList arrayList = (ArrayList)hashtable[sharedMaterials[j]];
				if (arrayList != null)
				{
					arrayList.Add(meshInstance);
					continue;
				}
				arrayList = new ArrayList();
				arrayList.Add(meshInstance);
				hashtable.Add(sharedMaterials[j], arrayList);
			}
			renderer.enabled = false;
		}
		foreach (DictionaryEntry item in hashtable)
		{
			ArrayList arrayList2 = (ArrayList)item.Value;
			MeshCombineUtility.MeshInstance[] meshInstance_ = (MeshCombineUtility.MeshInstance[])arrayList2.ToArray(typeof(MeshCombineUtility.MeshInstance));
			if (hashtable.Count == 1)
			{
				if (GetComponent(typeof(MeshFilter)) == null)
				{
					base.gameObject.AddComponent(typeof(MeshFilter));
				}
				if (!GetComponent("MeshRenderer"))
				{
					base.gameObject.AddComponent<MeshRenderer>();
				}
				MeshFilter meshFilter2 = (MeshFilter)GetComponent(typeof(MeshFilter));
				meshFilter2.mesh = MeshCombineUtility.Combine(meshInstance_, generateTriangleStrips);
				base.GetComponent<Renderer>().material = (Material)item.Key;
				base.GetComponent<Renderer>().enabled = true;
			}
			else
			{
				GameObject gameObject = new GameObject("Combined mesh");
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.AddComponent(typeof(MeshFilter));
				gameObject.AddComponent<MeshRenderer>();
				gameObject.GetComponent<Renderer>().material = (Material)item.Key;
				MeshFilter meshFilter3 = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
				meshFilter3.mesh = MeshCombineUtility.Combine(meshInstance_, generateTriangleStrips);
			}
		}
	}
}
