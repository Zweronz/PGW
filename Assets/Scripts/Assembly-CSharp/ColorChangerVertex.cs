using UnityEngine;

public class ColorChangerVertex : MonoBehaviour
{
	private Mesh mesh_0;

	private Color[] color_0;

	private void Start()
	{
		mesh_0 = GetComponent<MeshFilter>().mesh;
		color_0 = new Color[mesh_0.vertices.Length];
	}

	private void Update()
	{
		for (int i = 0; i < color_0.Length; i++)
		{
			float magnitude = mesh_0.vertices[i].magnitude;
			float r = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad + magnitude));
			float g = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * 0.45f + magnitude));
			float b = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * 1.2f + magnitude));
			Color color = new Color(r, g, b);
			color_0[i] = color;
		}
		mesh_0.colors = color_0;
	}
}
