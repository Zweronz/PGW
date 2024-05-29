using System.Collections.Generic;
using UnityEngine;

public class CompoundObjectController : FlashingController
{
	private Transform transform_0;

	private List<GameObject> list_0;

	private int int_0 = 2;

	private string[] string_0 = new string[8] { "Bumped Diffuse", "Bumped Specular", "Diffuse", "Diffuse Detail", "Parallax Diffuse", "Parallax Specular", "Specular", "VertexLit" };

	private int int_1 = -220;

	private int int_2 = 20;

	private void Start()
	{
		transform_0 = GetComponent<Transform>();
		list_0 = new List<GameObject>();
		StartCoroutine(DelayFlashing());
	}

	private void OnGUI()
	{
		float left = Screen.width + int_1;
		GUI.Label(new Rect(left, int_2, 500f, 100f), "Compound object controls:");
		if (GUI.Button(new Rect(left, int_2 + 30, 200f, 30f), "Add Random Primitive"))
		{
			AddObject();
		}
		if (GUI.Button(new Rect(left, int_2 + 70, 200f, 30f), "Change Material"))
		{
			ChangeMaterial();
		}
		if (GUI.Button(new Rect(left, int_2 + 110, 200f, 30f), "Change Shader"))
		{
			ChangeShader();
		}
		if (GUI.Button(new Rect(left, int_2 + 150, 200f, 30f), "Remove Object"))
		{
			RemoveObject();
		}
	}

	private void AddObject()
	{
		int type = Random.Range(0, 4);
		GameObject gameObject = GameObject.CreatePrimitive((PrimitiveType)type);
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = transform_0;
		component.localPosition = Random.insideUnitSphere * 2f;
		list_0.Add(gameObject);
		highlightableObject_0.ReinitMaterials();
	}

	private void ChangeMaterial()
	{
		if (list_0.Count < 1)
		{
			AddObject();
		}
		int_0 = ((int_0 + 1 < string_0.Length) ? (int_0 + 1) : 0);
		foreach (GameObject item in list_0)
		{
			Renderer component = item.GetComponent<Renderer>();
			Shader shader = Shader.Find(string_0[int_0]);
			component.material = new Material(shader);
		}
		highlightableObject_0.ReinitMaterials();
	}

	private void ChangeShader()
	{
		if (list_0.Count < 1)
		{
			AddObject();
		}
		int_0 = ((int_0 + 1 < string_0.Length) ? (int_0 + 1) : 0);
		foreach (GameObject item in list_0)
		{
			Renderer component = item.GetComponent<Renderer>();
			Shader shader = Shader.Find(string_0[int_0]);
			component.material.shader = shader;
		}
		highlightableObject_0.ReinitMaterials();
	}

	private void RemoveObject()
	{
		if (list_0.Count >= 1)
		{
			GameObject gameObject = list_0[list_0.Count - 1];
			list_0.Remove(gameObject);
			Object.Destroy(gameObject);
			highlightableObject_0.ReinitMaterials();
		}
	}
}
