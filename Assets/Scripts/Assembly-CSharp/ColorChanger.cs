using UnityEngine;

public class ColorChanger : MonoBehaviour
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
		float num = base.transform.position.magnitude / 3f;
		float r = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad + num));
		float g = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * 0.45f + num));
		float b = Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * 1.2f + num));
		Color color = new Color(r, g, b);
		for (int i = 0; i < color_0.Length; i++)
		{
			color_0[i] = color;
		}
		mesh_0.colors = color_0;
	}
}
