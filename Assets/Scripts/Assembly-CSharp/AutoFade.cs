using System;
using System.Collections;
using UnityEngine;

public class AutoFade : MonoBehaviour
{
	private static AutoFade autoFade_0;

	private Material material_0;

	private string string_0 = string.Empty;

	private int int_0;

	private bool bool_0;

	private bool bool_1 = true;

	private Action action_0;

	private float float_0;

	private static AutoFade AutoFade_0
	{
		get
		{
			if (autoFade_0 == null)
			{
				autoFade_0 = new GameObject("AutoFade").AddComponent<AutoFade>();
			}
			return autoFade_0;
		}
	}

	public static bool Boolean_0
	{
		get
		{
			return AutoFade_0.bool_0;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		autoFade_0 = this;
		material_0 = new Material(Shader.Find("Plane/No zTest"));
	}

	private void DrawQuad(Color color_0, float float_1)
	{
		color_0.a = float_1;
		if (material_0.SetPass(0))
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(7);
			GL.Color(color_0);
			GL.Vertex3(0f, 0f, -1f);
			GL.Vertex3(0f, 1f, -1f);
			GL.Vertex3(1f, 1f, -1f);
			GL.Vertex3(1f, 0f, -1f);
			GL.End();
			GL.PopMatrix();
		}
		else
		{
			Debug.LogWarning("Couldnot set pass for material.");
		}
	}

	private IEnumerator Fade(float float_1, float float_2, Color color_0)
	{
		float num = 0f;
		while (num < 1f)
		{
			yield return new WaitForEndOfFrame();
			num = Mathf.Clamp01(num + Time.deltaTime / float_1);
			DrawQuad(color_0, num);
		}
		if (bool_1)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				Application.LoadLevel(string_0);
			}
			else
			{
				Application.LoadLevel(int_0);
			}
		}
		else if (action_0 != null)
		{
			action_0();
		}
		else
		{
			while (!(float_0 <= 0f))
			{
				float_0 -= Time.deltaTime;
				DrawQuad(color_0, num);
				yield return new WaitForEndOfFrame();
			}
		}
		while (!(num <= 0f) && Mathf.Abs(float_2) >= 1E-06f)
		{
			num = Mathf.Clamp01(num - Time.deltaTime / float_2);
			DrawQuad(color_0, num);
			yield return new WaitForEndOfFrame();
		}
		bool_0 = false;
	}

	private void StartFade(float float_1, float float_2, Color color_0)
	{
		bool_0 = true;
		StartCoroutine(Fade(float_1, float_2, color_0));
	}

	public static void LoadLevel(string string_1, float float_1, float float_2, Color color_0)
	{
		if (!Boolean_0)
		{
			AutoFade_0.action_0 = null;
			AutoFade_0.bool_1 = true;
			AutoFade_0.string_0 = string_1;
			AutoFade_0.StartFade(float_1, float_2, color_0);
		}
	}

	public static void LoadLevel(int int_1, float float_1, float float_2, Color color_0)
	{
		if (!Boolean_0)
		{
			AutoFade_0.action_0 = null;
			AutoFade_0.bool_1 = true;
			AutoFade_0.string_0 = string.Empty;
			AutoFade_0.int_0 = int_1;
			AutoFade_0.StartFade(float_1, float_2, color_0);
		}
	}

	public static void fadeKilled(float float_1, float float_2, float float_3, Color color_0)
	{
		if (!Boolean_0)
		{
			AutoFade_0.action_0 = null;
			AutoFade_0.bool_1 = false;
			AutoFade_0.float_0 = float_2;
			AutoFade_0.StartFade(float_1, float_3, color_0);
		}
	}

	public static void FadeAction(float float_1, float float_2, Color color_0, Action action_1)
	{
		if (!Boolean_0)
		{
			AutoFade_0.action_0 = action_1;
			AutoFade_0.bool_1 = false;
			AutoFade_0.StartFade(float_1, float_2, color_0);
		}
	}
}
