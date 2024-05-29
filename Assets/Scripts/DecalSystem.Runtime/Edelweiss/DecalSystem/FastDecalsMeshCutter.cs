using UnityEngine;

namespace Edelweiss.DecalSystem
{
	public class FastDecalsMeshCutter : GenericDecalsMeshCutter<Decals, DecalProjectorBase, DecalsMesh>
	{
		internal override void InitializeDelegates()
		{
			bool flag = m_DecalsMesh.Decals.CurrentTangentsMode == TangentsMode.Target;
			bool flag2 = m_DecalsMesh.Decals.CurrentUVMode == UVMode.TargetUV || m_DecalsMesh.Decals.CurrentUVMode == UVMode.TargetUV2;
			bool flag3 = m_DecalsMesh.Decals.CurrentUV2Mode == UV2Mode.TargetUV || m_DecalsMesh.Decals.CurrentUV2Mode == UV2Mode.TargetUV2;
			if (!flag && !flag2 && !flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormals;
			}
			else if (!flag && !flag2 && flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsUV2s;
			}
			else if (!flag && flag2 && !flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsUVs;
			}
			else if (!flag && flag2 && flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsUVsUV2s;
			}
			else if (flag && !flag2 && !flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsTangents;
			}
			else if (flag && !flag2 && flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsTangentsUV2s;
			}
			else if (flag && flag2 && !flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsTangentsUVs;
			}
			else if (flag && flag2 && flag3)
			{
				m_CreateCutEdgeDelegate = CutEdgeOptimizedVerticesNormalsTangentsUVsUV2s;
			}
			m_GetCutEdgeDelegate = m_CreateCutEdgeDelegate;
		}

		private int CutEdgeOptimizedVerticesNormals(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsTangents(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.Tangents.Add(Vector4.Lerp(m_DecalsMesh.Tangents[a_IndexA], m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsUVs(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UVs.Add(Vector2.Lerp(m_DecalsMesh.UVs[a_IndexA], m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsTangentsUVs(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.Tangents.Add(Vector4.Lerp(m_DecalsMesh.Tangents[a_IndexA], m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UVs.Add(Vector2.Lerp(m_DecalsMesh.UVs[a_IndexA], m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UV2s.Add(Vector2.Lerp(m_DecalsMesh.UV2s[a_IndexA], m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsTangentsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.Tangents.Add(Vector4.Lerp(m_DecalsMesh.Tangents[a_IndexA], m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UV2s.Add(Vector2.Lerp(m_DecalsMesh.UV2s[a_IndexA], m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsUVsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UVs.Add(Vector2.Lerp(m_DecalsMesh.UVs[a_IndexA], m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UV2s.Add(Vector2.Lerp(m_DecalsMesh.UV2s[a_IndexA], m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeOptimizedVerticesNormalsTangentsUVsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(m_DecalsMesh.Normals[a_IndexA], m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.Tangents.Add(Vector4.Lerp(m_DecalsMesh.Tangents[a_IndexA], m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UVs.Add(Vector2.Lerp(m_DecalsMesh.UVs[a_IndexA], m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			m_DecalsMesh.UV2s.Add(Vector2.Lerp(m_DecalsMesh.UV2s[a_IndexA], m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			return count;
		}

		private int CutEdgeUnoptimized(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			Vector3 from = m_DecalsMesh.Normals[a_IndexA];
			Vector3 to = m_DecalsMesh.Normals[a_IndexB];
			int count = m_DecalsMesh.Vertices.Count;
			m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			m_DecalsMesh.Normals.Add(Vector3.Lerp(from, to, a_IntersectionFactorAB));
			m_ActiveProjector.DecalsMeshUpperVertexIndex += 1;
			if (m_DecalsMesh.Decals.CurrentUVMode == UVMode.TargetUV || m_DecalsMesh.Decals.CurrentUVMode == UVMode.TargetUV2)
			{
				m_DecalsMesh.UVs.Add(Vector2.Lerp(m_DecalsMesh.UVs[a_IndexA], m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			}
			if (m_DecalsMesh.Decals.CurrentUV2Mode == UV2Mode.TargetUV || m_DecalsMesh.Decals.CurrentUV2Mode == UV2Mode.TargetUV2)
			{
				m_DecalsMesh.UV2s.Add(Vector2.Lerp(m_DecalsMesh.UV2s[a_IndexA], m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			}
			if (m_DecalsMesh.Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				m_DecalsMesh.Tangents.Add(Vector4.Lerp(m_DecalsMesh.Tangents[a_IndexA], m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			}
			return count;
		}
	}
}
