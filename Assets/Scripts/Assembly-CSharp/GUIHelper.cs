using System;
using UnityEngine;

public static class GUIHelper
{
	private static GUIStyle guistyle_0;

	private static GUIStyle guistyle_1;

	public static Rect rect_0;

	private static void Setup()
	{
		if (guistyle_0 == null)
		{
			guistyle_0 = new GUIStyle(GUI.skin.label);
			guistyle_0.alignment = TextAnchor.MiddleCenter;
			guistyle_1 = new GUIStyle(GUI.skin.label);
			guistyle_1.alignment = TextAnchor.MiddleRight;
		}
	}

	public static void DrawArea(Rect rect_1, bool bool_0, Action action_0)
	{
		Setup();
		GUI.Box(rect_1, string.Empty);
		GUILayout.BeginArea(rect_1);
		if (bool_0)
		{
			DrawCenteredText(SampleSelector.sampleDescriptor_0.String_0);
			GUILayout.Space(5f);
		}
		if (action_0 != null)
		{
			action_0();
		}
		GUILayout.EndArea();
	}

	public static void DrawCenteredText(string string_0)
	{
		Setup();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(string_0, guistyle_0);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}

	public static void DrawRow(string string_0, string string_1)
	{
		Setup();
		GUILayout.BeginHorizontal();
		GUILayout.Label(string_0);
		GUILayout.FlexibleSpace();
		GUILayout.Label(string_1, guistyle_1);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}
}
