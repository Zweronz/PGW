using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class MeshInfo
	{
		private readonly HashSet<MeshFilter> hashSet_0 = new HashSet<MeshFilter>();

		public bool AddMeshFilter(MeshFilter meshFilter_0)
		{
			return hashSet_0.Add(meshFilter_0);
		}

		public IList<MeshFilter> GetRenderers()
		{
			return hashSet_0.ToList();
		}
	}
}
