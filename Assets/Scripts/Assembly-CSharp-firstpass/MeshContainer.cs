using UnityEngine;

public class MeshContainer
{
	public Mesh mesh_0;

	public Vector3[] vector3_0;

	public Vector3[] vector3_1;

	public MeshContainer(Mesh mesh_1)
	{
		mesh_0 = mesh_1;
		vector3_0 = mesh_1.vertices;
		vector3_1 = mesh_1.normals;
	}

	public void Update()
	{
		mesh_0.vertices = vector3_0;
		mesh_0.normals = vector3_1;
	}
}
