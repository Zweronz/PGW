using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class MaterialInfo
	{
		private readonly HashSet<Renderer> hashSet_0 = new HashSet<Renderer>();

		public bool AddRenderer(Renderer renderer_0)
		{
			return hashSet_0.Add(renderer_0);
		}

		public IList<Renderer> GetRenderers()
		{
			return hashSet_0.ToList();
		}
	}
}
