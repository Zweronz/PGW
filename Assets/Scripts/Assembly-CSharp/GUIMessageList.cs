using System.Collections.Generic;
using UnityEngine;

public class GUIMessageList
{
	private List<string> list_0 = new List<string>();

	private Vector2 vector2_0;

	public void Draw()
	{
		Draw(Screen.width, 0f);
	}

	public void Draw(float float_0, float float_1)
	{
		vector2_0 = GUILayout.BeginScrollView(vector2_0, false, false, GUILayout.MinHeight(float_1));
		for (int i = 0; i < list_0.Count; i++)
		{
			GUILayout.Label(list_0[i], GUILayout.MinWidth(float_0));
		}
		GUILayout.EndScrollView();
	}

	public void Add(string string_0)
	{
		list_0.Add(string_0);
		vector2_0 = new Vector2(vector2_0.x, float.MaxValue);
	}

	public void Clear()
	{
		list_0.Clear();
	}
}
