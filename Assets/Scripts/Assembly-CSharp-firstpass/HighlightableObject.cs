using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableObject : MonoBehaviour
{
	private class HighlightingRendererCache
	{
		public Renderer renderer_0;

		public GameObject gameObject_0;

		private Material[] material_0;

		private Material[] material_1;

		private List<int> list_0;

		public HighlightingRendererCache(Renderer renderer_1, Material[] material_2, Material material_3, bool bool_0)
		{
			renderer_0 = renderer_1;
			gameObject_0 = renderer_1.gameObject;
			material_0 = material_2;
			material_1 = new Material[material_2.Length];
			list_0 = new List<int>();
			for (int i = 0; i < material_2.Length; i++)
			{
				Material material = material_2[i];
				if (material == null)
				{
					continue;
				}
				string tag = material.GetTag("RenderType", true);
				if (!(tag == "Transparent") && !(tag == "TransparentCutout"))
				{
					material_1[i] = material_3;
					continue;
				}
				Material material2 = new Material((!bool_0) ? Shader_1 : Shader_3);
				if (material.HasProperty("_MainTex"))
				{
					material2.SetTexture("_MainTex", material.mainTexture);
					material2.SetTextureOffset("_MainTex", material.mainTextureOffset);
					material2.SetTextureScale("_MainTex", material.mainTextureScale);
				}
				material2.SetFloat("_Cutoff", (!material.HasProperty("_Cutoff")) ? float_3 : material.GetFloat("_Cutoff"));
				material_1[i] = material2;
				list_0.Add(i);
			}
		}

		public void SetState(bool bool_0)
		{
			renderer_0.sharedMaterials = ((!bool_0) ? material_0 : material_1);
		}

		public void SetColorForTransparent(Color color_0)
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				material_1[list_0[i]].SetColor("_Outline", color_0);
			}
		}
	}

	private const float float_0 = (float)Math.PI * 2f;

	public static int int_0 = 7;

	private static float float_1 = 4.5f;

	private static float float_2 = 4f;

	private static float float_3 = 0.5f;

	private List<HighlightingRendererCache> list_0;

	private int[] int_1;

	private bool bool_0 = true;

	private bool bool_1;

	private Color color_0;

	private bool bool_2;

	private float float_4;

	private float float_5 = 2f;

	private bool bool_3;

	private Color color_1 = Color.red;

	private bool bool_4;

	private Color color_2 = new Color(0f, 1f, 1f, 0f);

	private Color color_3 = new Color(0f, 1f, 1f, 1f);

	private bool bool_5;

	private Color color_4 = Color.yellow;

	private bool bool_6;

	private bool bool_7;

	private readonly Color color_5 = new Color(0f, 0f, 0f, 0.005f);

	private Material material_0;

	private Material material_1;

	private static Shader shader_0;

	private static Shader shader_1;

	private static Shader shader_2;

	private static Shader shader_3;

	private Material Material_0
	{
		get
		{
			return (!bool_7) ? Material_1 : Material_2;
		}
	}

	private Material Material_1
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(Shader_0);
				material_0.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_0;
		}
	}

	private Material Material_2
	{
		get
		{
			if (material_1 == null)
			{
				material_1 = new Material(Shader_2);
				material_1.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_1;
		}
	}

	private static Shader Shader_0
	{
		get
		{
			if (shader_0 == null)
			{
				shader_0 = Shader.Find("Hidden/Highlighted/StencilOpaque");
			}
			return shader_0;
		}
	}

	public static Shader Shader_1
	{
		get
		{
			if (shader_1 == null)
			{
				shader_1 = Shader.Find("Hidden/Highlighted/StencilTransparent");
			}
			return shader_1;
		}
	}

	private static Shader Shader_2
	{
		get
		{
			if (shader_2 == null)
			{
				shader_2 = Shader.Find("Hidden/Highlighted/StencilOpaqueZ");
			}
			return shader_2;
		}
	}

	private static Shader Shader_3
	{
		get
		{
			if (shader_3 == null)
			{
				shader_3 = Shader.Find("Hidden/Highlighted/StencilTransparentZ");
			}
			return shader_3;
		}
	}

	private void OnEnable()
	{
		StartCoroutine(EndOfFrame());
		HighlightingEffect.highlightingEvent += UpdateEventHandler;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		HighlightingEffect.highlightingEvent -= UpdateEventHandler;
		if (list_0 != null)
		{
			list_0.Clear();
		}
		int_1 = null;
		bool_0 = true;
		bool_1 = false;
		color_0 = Color.clear;
		bool_2 = false;
		float_4 = 0f;
		bool_3 = false;
		bool_4 = false;
		bool_5 = false;
		bool_6 = false;
		bool_7 = false;
		if ((bool)material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
		}
		if ((bool)material_1)
		{
			UnityEngine.Object.DestroyImmediate(material_1);
		}
	}

	public void ReinitMaterials()
	{
		bool_0 = true;
	}

	public void RestoreMaterials()
	{
		Debug.LogWarning("HighlightingSystem : RestoreMaterials() is obsolete. Please use ReinitMaterials() instead.");
		ReinitMaterials();
	}

	public void OnParams(Color color_6)
	{
		color_1 = color_6;
	}

	public void On()
	{
		bool_3 = true;
	}

	public void On(Color color_6)
	{
		color_1 = color_6;
		On();
	}

	public void FlashingParams(Color color_6, Color color_7, float float_6)
	{
		color_2 = color_6;
		color_3 = color_7;
		float_5 = float_6;
	}

	public void FlashingOn()
	{
		bool_4 = true;
	}

	public void FlashingOn(Color color_6, Color color_7)
	{
		color_2 = color_6;
		color_3 = color_7;
		FlashingOn();
	}

	public void FlashingOn(Color color_6, Color color_7, float float_6)
	{
		float_5 = float_6;
		FlashingOn(color_6, color_7);
	}

	public void FlashingOn(float float_6)
	{
		float_5 = float_6;
		FlashingOn();
	}

	public void FlashingOff()
	{
		bool_4 = false;
	}

	public void FlashingSwitch()
	{
		bool_4 = !bool_4;
	}

	public void ConstantParams(Color color_6)
	{
		color_4 = color_6;
	}

	public void ConstantOn()
	{
		bool_5 = true;
		bool_2 = true;
	}

	public void ConstantOn(Color color_6)
	{
		color_4 = color_6;
		ConstantOn();
	}

	public void ConstantOff()
	{
		bool_5 = false;
		bool_2 = true;
	}

	public void ConstantSwitch()
	{
		bool_5 = !bool_5;
		bool_2 = true;
	}

	public void ConstantOnImmediate()
	{
		bool_5 = true;
		float_4 = 1f;
		bool_2 = false;
	}

	public void ConstantOnImmediate(Color color_6)
	{
		color_4 = color_6;
		ConstantOnImmediate();
	}

	public void ConstantOffImmediate()
	{
		bool_5 = false;
		float_4 = 0f;
		bool_2 = false;
	}

	public void ConstantSwitchImmediate()
	{
		bool_5 = !bool_5;
		float_4 = ((!bool_5) ? 0f : 1f);
		bool_2 = false;
	}

	public void OccluderOn()
	{
		bool_6 = true;
	}

	public void OccluderOff()
	{
		bool_6 = false;
	}

	public void OccluderSwitch()
	{
		bool_6 = !bool_6;
	}

	public void Off()
	{
		bool_3 = false;
		bool_4 = false;
		bool_5 = false;
		float_4 = 0f;
		bool_2 = false;
	}

	public void Die()
	{
		UnityEngine.Object.Destroy(this);
	}

	private void InitMaterials(bool bool_8)
	{
		bool_1 = false;
		bool_7 = bool_8;
		list_0 = new List<HighlightingRendererCache>();
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		CacheRenderers(componentsInChildren);
		SkinnedMeshRenderer[] componentsInChildren2 = GetComponentsInChildren<SkinnedMeshRenderer>();
		CacheRenderers(componentsInChildren2);
		//ClothRenderer[] componentsInChildren3 = GetComponentsInChildren<ClothRenderer>();
		//CacheRenderers(componentsInChildren3);
		bool_1 = false;
		bool_0 = false;
		color_0 = Color.clear;
	}

	private void CacheRenderers(Renderer[] renderer_0)
	{
		for (int i = 0; i < renderer_0.Length; i++)
		{
			Material[] sharedMaterials = renderer_0[i].sharedMaterials;
			if (sharedMaterials != null)
			{
				list_0.Add(new HighlightingRendererCache(renderer_0[i], sharedMaterials, Material_0, bool_7));
			}
		}
	}

	private void SetColor(Color color_6)
	{
		if (!(color_0 == color_6))
		{
			if (bool_7)
			{
				Material_2.SetColor("_Outline", color_6);
			}
			else
			{
				Material_1.SetColor("_Outline", color_6);
			}
			for (int i = 0; i < list_0.Count; i++)
			{
				list_0[i].SetColorForTransparent(color_6);
			}
			color_0 = color_6;
		}
	}

	private void UpdateColors()
	{
		if (bool_1)
		{
			if (bool_6)
			{
				SetColor(color_5);
			}
			else if (bool_3)
			{
				SetColor(color_1);
			}
			else if (bool_4)
			{
				Color color = Color.Lerp(color_2, color_3, 0.5f * Mathf.Sin(Time.realtimeSinceStartup * float_5 * ((float)Math.PI * 2f)) + 0.5f);
				SetColor(color);
			}
			else if (bool_2)
			{
				Color color2 = new Color(color_4.r, color_4.g, color_4.b, color_4.a * float_4);
				SetColor(color2);
			}
			else if (bool_5)
			{
				SetColor(color_4);
			}
		}
	}

	private void PerformTransition()
	{
		if (bool_2)
		{
			float num = ((!bool_5) ? 0f : 1f);
			if (float_4 == num)
			{
				bool_2 = false;
			}
			else if (Time.timeScale != 0f)
			{
				float num2 = Time.deltaTime / Time.timeScale;
				float_4 += ((!bool_5) ? (0f - float_2) : float_1) * num2;
				float_4 = Mathf.Clamp01(float_4);
			}
		}
	}

	private void UpdateEventHandler(bool bool_8, bool bool_9)
	{
		if (bool_8)
		{
			if (bool_7 != bool_9)
			{
				bool_0 = true;
			}
			if (bool_0)
			{
				InitMaterials(bool_9);
			}
			bool_1 = bool_3 || bool_4 || bool_5 || bool_2 || bool_6;
			if (!bool_1)
			{
				return;
			}
			UpdateColors();
			PerformTransition();
			if (list_0 != null)
			{
				int_1 = new int[list_0.Count];
				for (int i = 0; i < list_0.Count; i++)
				{
					GameObject gameObject_ = list_0[i].gameObject_0;
					int_1[i] = gameObject_.layer;
					gameObject_.layer = int_0;
					list_0[i].SetState(true);
				}
			}
		}
		else if (bool_1 && list_0 != null)
		{
			for (int j = 0; j < list_0.Count; j++)
			{
				list_0[j].gameObject_0.layer = int_1[j];
				list_0[j].SetState(false);
			}
		}
	}

	private IEnumerator EndOfFrame()
	{
		while (base.enabled)
		{
			yield return new WaitForEndOfFrame();
			bool_3 = false;
		}
	}
}
