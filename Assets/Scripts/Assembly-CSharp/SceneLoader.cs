using UnityEngine;

public class SceneLoader : MonoBehaviour
{
	private int int_0 = 20;

	private int int_1 = 100;

	private void OnGUI()
	{
		GUI.Label(new Rect(int_0, int_1 + 10, 500f, 100f), "Load demo scene:");
		if (GUI.Button(new Rect(int_0, int_1 + 30, 120f, 20f), "Welcome"))
		{
			Application.LoadLevel("Welcome");
		}
		if (GUI.Button(new Rect(int_0, int_1 + 60, 120f, 20f), "Colors"))
		{
			Application.LoadLevel("Colors");
		}
		if (GUI.Button(new Rect(int_0, int_1 + 90, 120f, 20f), "Transparency"))
		{
			Application.LoadLevel("Transparency");
		}
		if (GUI.Button(new Rect(int_0, int_1 + 120, 120f, 20f), "Occluders"))
		{
			Application.LoadLevel("Occluders");
		}
		if (GUI.Button(new Rect(int_0, int_1 + 150, 120f, 20f), "Scripting"))
		{
			Application.LoadLevel("Scripting");
		}
		if (GUI.Button(new Rect(int_0, int_1 + 180, 120f, 20f), "Compound"))
		{
			Application.LoadLevel("Compound");
		}
	}
}
