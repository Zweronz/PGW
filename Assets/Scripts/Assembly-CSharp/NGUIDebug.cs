using System.Collections.Generic;
using UnityEngine;

public class NGUIDebug : MonoBehaviour
{
	private static bool bool_0 = false;

	private static List<string> list_0 = new List<string>();

	private static NGUIDebug nguidebug_0 = null;

	public static bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (Application.isPlaying)
			{
				bool_0 = value;
				if (value)
				{
					CreateInstance();
				}
			}
		}
	}

	public static void CreateInstance()
	{
		if (nguidebug_0 == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			nguidebug_0 = gameObject.AddComponent<NGUIDebug>();
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	private static void LogString(string string_0)
	{
		if (Application.isPlaying)
		{
			if (list_0.Count > 20)
			{
				list_0.RemoveAt(0);
			}
			list_0.Add(string_0);
			CreateInstance();
		}
		else
		{
			Debug.Log(string_0);
		}
	}

	public static void Log(params object[] object_0)
	{
		string text = string.Empty;
		for (int i = 0; i < object_0.Length; i++)
		{
			text = ((i != 0) ? (text + ", " + object_0[i].ToString()) : (text + object_0[i].ToString()));
		}
		LogString(text);
	}

	public static void DrawBounds(Bounds bounds_0)
	{
		Vector3 center = bounds_0.center;
		Vector3 vector = bounds_0.center - bounds_0.extents;
		Vector3 vector2 = bounds_0.center + bounds_0.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	private void OnGUI()
	{
		if (list_0.Count == 0)
		{
			if (bool_0 && UICamera.gameObject_6 != null && Application.isPlaying)
			{
				GUILayout.Label("Last Hit: " + NGUITools.GetHierarchy(UICamera.gameObject_6).Replace("\"", string.Empty));
			}
			return;
		}
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			GUILayout.Label(list_0[i]);
		}
	}
}
