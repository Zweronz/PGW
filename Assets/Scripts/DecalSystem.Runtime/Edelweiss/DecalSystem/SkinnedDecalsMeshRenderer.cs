using UnityEngine;

namespace Edelweiss.DecalSystem
{
	[RequireComponent(typeof(SkinnedMeshRenderer))]
	public class SkinnedDecalsMeshRenderer : MonoBehaviour
	{
		[HideInInspector]
		[SerializeField]
		private SkinnedMeshRenderer m_SkinnedMeshRenderer;

		public SkinnedMeshRenderer SkinnedMeshRenderer
		{
			get
			{
				if (m_SkinnedMeshRenderer == null)
				{
					m_SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
				}
				return m_SkinnedMeshRenderer;
			}
		}
	}
}
