using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlowThresholdEffect : MonoBehaviour
{
	public float glowThreshold = 0.25f;

	public float glowIntensity = 1.5f;

	public int blurIterations = 3;

	public float blurSpread = 0.7f;

	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	private static string string_0 = "Shader \"GlowCompose\" {\r\n\tProperties {\r\n\t\t_Color (\"Glow Amount\", Color) = (1,1,1,1)\r\n\t\t_MainTex (\"\", RECT) = \"white\" {}\r\n\t}\r\n\tSubShader {\r\n\t\tPass {\r\n\t\t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\r\n\t\t\tBlend One One\r\n\t\t\tSetTexture [_MainTex] {constantColor [_Color] combine constant * texture DOUBLE}\r\n\t\t}\r\n\t}\r\n\tFallback off\r\n}";

	private static Material material_0;

	private static string string_1 = "Shader \"GlowConeTap\" {\r\n\tProperties {\r\n\t\t_Color (\"Blur Boost\", Color) = (0,0,0,0.25)\r\n\t\t_MainTex (\"\", RECT) = \"white\" {}\r\n\t}\r\n\tSubShader {\r\n\t\tPass {\r\n\t\t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\r\n\t\t\tSetTexture [_MainTex] {constantColor [_Color] combine texture * constant alpha}\r\n\t\t\tSetTexture [_MainTex] {constantColor [_Color] combine texture * constant + previous}\r\n\t\t\tSetTexture [_MainTex] {constantColor [_Color] combine texture * constant + previous}\r\n\t\t\tSetTexture [_MainTex] {constantColor [_Color] combine texture * constant + previous}\r\n\t\t}\r\n\t}\r\n\tFallback off\r\n}";

	private static Material material_1;

	public Shader downsampleShader;

	private Material material_2;

	protected static Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(string_0);
				material_0.hideFlags = HideFlags.HideAndDontSave;
				material_0.shader.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_0;
		}
	}

	protected static Material Material_1
	{
		get
		{
			if (material_1 == null)
			{
				material_1 = new Material(string_1);
				material_1.hideFlags = HideFlags.HideAndDontSave;
				material_1.shader.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_1;
		}
	}

	protected Material Material_2
	{
		get
		{
			if (material_2 == null)
			{
				material_2 = new Material(downsampleShader);
				material_2.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_2;
		}
	}

	protected void OnDisable()
	{
		if ((bool)material_0)
		{
			Object.DestroyImmediate(material_0.shader);
			Object.DestroyImmediate(material_0);
		}
		if ((bool)material_1)
		{
			Object.DestroyImmediate(material_1.shader);
			Object.DestroyImmediate(material_1);
		}
		if ((bool)material_2)
		{
			Object.DestroyImmediate(material_2);
		}
	}

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.enabled = false;
			return;
		}
		if (!Material_1.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!Material_0.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!Material_2.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	public void FourTapCone(RenderTexture renderTexture_0, RenderTexture renderTexture_1, int int_0)
	{
		RenderTexture.active = renderTexture_1;
		Material_1.SetTexture("_MainTex", renderTexture_0);
		float float_ = (0.5f + (float)int_0 * blurSpread) / (float)renderTexture_0.width;
		float float_2 = (0.5f + (float)int_0 * blurSpread) / (float)renderTexture_0.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < Material_1.passCount; i++)
		{
			Material_1.SetPass(i);
			Render4TapQuad(renderTexture_1, float_, float_2);
		}
		GL.PopMatrix();
	}

	private void DownSample4x(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Material_2.SetFloat("_GlowThreshold", glowThreshold);
		Material_2.color = new Color(glowTint.r, glowTint.g, glowTint.b, glowTint.a / 4f);
		Graphics.Blit(renderTexture_0, renderTexture_1, Material_2);
	}

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		glowThreshold = Mathf.Clamp(glowThreshold, 0f, 1f);
		glowIntensity = Mathf.Clamp(glowIntensity, 0f, 10f);
		blurIterations = Mathf.Clamp(blurIterations, 0, 30);
		blurSpread = Mathf.Clamp(blurSpread, 0.5f, 1f);
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
		DownSample4x(renderTexture_0, temporary);
		float num = Mathf.Clamp01((glowIntensity - 1f) / 4f);
		Material_1.color = new Color(1f, 1f, 1f, 0.25f + num);
		bool flag = true;
		for (int i = 0; i < blurIterations; i++)
		{
			if (flag)
			{
				FourTapCone(temporary, temporary2, i);
			}
			else
			{
				FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		Graphics.Blit(renderTexture_0, renderTexture_1);
		if (flag)
		{
			BlitGlow(temporary, renderTexture_1);
		}
		else
		{
			BlitGlow(temporary2, renderTexture_1);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	public void BlitGlow(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Material_0.color = new Color(1f, 1f, 1f, Mathf.Clamp01(glowIntensity));
		Graphics.Blit(renderTexture_0, renderTexture_1, Material_0);
	}

	private static void Render4TapQuad(RenderTexture renderTexture_0, float float_0, float float_1)
	{
		GL.Begin(7);
		Vector2 vector = Vector2.zero;
		if (renderTexture_0 != null)
		{
			vector = renderTexture_0.GetTexelOffset() * 0.75f;
		}
		Set4TexCoords(vector.x, vector.y, float_0, float_1);
		GL.Vertex3(0f, 0f, 0.1f);
		Set4TexCoords(1f + vector.x, vector.y, float_0, float_1);
		GL.Vertex3(1f, 0f, 0.1f);
		Set4TexCoords(1f + vector.x, 1f + vector.y, float_0, float_1);
		GL.Vertex3(1f, 1f, 0.1f);
		Set4TexCoords(vector.x, 1f + vector.y, float_0, float_1);
		GL.Vertex3(0f, 1f, 0.1f);
		GL.End();
	}

	private static void Set4TexCoords(float float_0, float float_1, float float_2, float float_3)
	{
		GL.MultiTexCoord2(0, float_0 - float_2, float_1 - float_3);
		GL.MultiTexCoord2(1, float_0 + float_2, float_1 - float_3);
		GL.MultiTexCoord2(2, float_0 + float_2, float_1 + float_3);
		GL.MultiTexCoord2(3, float_0 - float_2, float_1 + float_3);
	}
}
