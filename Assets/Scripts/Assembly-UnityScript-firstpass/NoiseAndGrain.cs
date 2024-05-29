using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class NoiseAndGrain : PostEffectsBase
{
	public float intensityMultiplier;

	public float generalIntensity;

	public float blackIntensity;

	public float whiteIntensity;

	public float midGrey;

	public bool dx11Grain;

	public float softness;

	public bool monochrome;

	public Vector3 intensities;

	public Vector3 tiling;

	public float monochromeTiling;

	public FilterMode filterMode;

	public Texture2D noiseTexture;

	public Shader noiseShader;

	private Material material_0;

	public Shader dx11NoiseShader;

	private Material material_1;

	[NonSerialized]
	private static float float_0 = 64f;

	public NoiseAndGrain()
	{
		intensityMultiplier = 0.25f;
		generalIntensity = 0.5f;
		blackIntensity = 1f;
		whiteIntensity = 1f;
		midGrey = 0.2f;
		intensities = new Vector3(1f, 1f, 1f);
		tiling = new Vector3(64f, 64f, 64f);
		monochromeTiling = 64f;
		filterMode = FilterMode.Bilinear;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_0 = CheckShaderAndCreateMaterial(noiseShader, material_0);
		if (dx11Grain && bool_1)
		{
			material_1 = CheckShaderAndCreateMaterial(dx11NoiseShader, material_1);
		}
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (CheckResources() && !(null == noiseTexture))
		{
			softness = Mathf.Clamp(softness, 0f, 0.99f);
			if (dx11Grain && bool_1)
			{
				material_1.SetFloat("_DX11NoiseTime", Time.frameCount);
				material_1.SetTexture("_NoiseTex", noiseTexture);
				material_1.SetVector("_NoisePerChannel", (!monochrome) ? intensities : Vector3.one);
				material_1.SetVector("_MidGrey", new Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey));
				material_1.SetVector("_NoiseAmount", new Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier);
				if (!(softness <= float.Epsilon))
				{
					RenderTexture temporary = RenderTexture.GetTemporary((int)((float)renderTexture_0.width * (1f - softness)), (int)((float)renderTexture_0.height * (1f - softness)));
					DrawNoiseQuadGrid(renderTexture_0, temporary, material_1, noiseTexture, (!monochrome) ? 2 : 3);
					material_1.SetTexture("_NoiseTex", temporary);
					Graphics.Blit(renderTexture_0, renderTexture_1, material_1, 4);
					RenderTexture.ReleaseTemporary(temporary);
				}
				else
				{
					DrawNoiseQuadGrid(renderTexture_0, renderTexture_1, material_1, noiseTexture, monochrome ? 1 : 0);
				}
				return;
			}
			if ((bool)noiseTexture)
			{
				noiseTexture.wrapMode = TextureWrapMode.Repeat;
				noiseTexture.filterMode = filterMode;
			}
			material_0.SetTexture("_NoiseTex", noiseTexture);
			material_0.SetVector("_NoisePerChannel", (!monochrome) ? intensities : Vector3.one);
			material_0.SetVector("_NoiseTilingPerChannel", (!monochrome) ? tiling : (Vector3.one * monochromeTiling));
			material_0.SetVector("_MidGrey", new Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey));
			material_0.SetVector("_NoiseAmount", new Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier);
			if (!(softness <= float.Epsilon))
			{
				RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width * (1f - softness)), (int)((float)renderTexture_0.height * (1f - softness)));
				DrawNoiseQuadGrid(renderTexture_0, temporary2, material_0, noiseTexture, 2);
				material_0.SetTexture("_NoiseTex", temporary2);
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 1);
				RenderTexture.ReleaseTemporary(temporary2);
			}
			else
			{
				DrawNoiseQuadGrid(renderTexture_0, renderTexture_1, material_0, noiseTexture, 0);
			}
		}
		else
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			if (null == noiseTexture)
			{
				Debug.LogWarning("Noise & Grain effect failing as noise texture is not assigned. please assign.", transform);
			}
		}
	}

	public static void DrawNoiseQuadGrid(RenderTexture renderTexture_0, RenderTexture renderTexture_1, Material material_2, Texture2D texture2D_0, int int_0)
	{
		RenderTexture.active = renderTexture_1;
		float num = (float)texture2D_0.width * 1f;
		float num2 = 1f * (float)renderTexture_0.width / float_0;
		material_2.SetTexture("_MainTex", renderTexture_0);
		GL.PushMatrix();
		GL.LoadOrtho();
		float num3 = 1f * (float)renderTexture_0.width / (1f * (float)renderTexture_0.height);
		float num4 = 1f / num2;
		float num5 = num4 * num3;
		float num6 = num / ((float)texture2D_0.width * 1f);
		material_2.SetPass(int_0);
		GL.Begin(7);
		for (float num7 = 0f; num7 < 1f; num7 += num4)
		{
			for (float num8 = 0f; num8 < 1f; num8 += num5)
			{
				float num9 = UnityEngine.Random.Range(0f, 1f);
				float num10 = UnityEngine.Random.Range(0f, 1f);
				num9 = Mathf.Floor(num9 * num) / num;
				num10 = Mathf.Floor(num10 * num) / num;
				float num11 = 1f / num;
				GL.MultiTexCoord2(0, num9, num10);
				GL.MultiTexCoord2(1, 0f, 0f);
				GL.Vertex3(num7, num8, 0.1f);
				GL.MultiTexCoord2(0, num9 + num6 * num11, num10);
				GL.MultiTexCoord2(1, 1f, 0f);
				GL.Vertex3(num7 + num4, num8, 0.1f);
				GL.MultiTexCoord2(0, num9 + num6 * num11, num10 + num6 * num11);
				GL.MultiTexCoord2(1, 1f, 1f);
				GL.Vertex3(num7 + num4, num8 + num5, 0.1f);
				GL.MultiTexCoord2(0, num9, num10 + num6 * num11);
				GL.MultiTexCoord2(1, 0f, 1f);
				GL.Vertex3(num7, num8 + num5, 0.1f);
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	public override void Main()
	{
	}
}
