using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class BloomAndLensFlares : PostEffectsBase
{
	public TweakMode34 tweakMode;

	public BloomScreenBlendMode screenBlendMode;

	public HDRBloomMode hdr;

	private bool bool_3;

	public float sepBlurSpread;

	public float useSrcAlphaAsMask;

	public float bloomIntensity;

	public float bloomThreshhold;

	public int bloomBlurIterations;

	public bool lensflares;

	public int hollywoodFlareBlurIterations;

	public LensflareStyle34 lensflareMode;

	public float hollyStretchWidth;

	public float lensflareIntensity;

	public float lensflareThreshhold;

	public Color flareColorA;

	public Color flareColorB;

	public Color flareColorC;

	public Color flareColorD;

	public float blurWidth;

	public Texture2D lensFlareVignetteMask;

	public Shader lensFlareShader;

	private Material material_0;

	public Shader vignetteShader;

	private Material material_1;

	public Shader separableBlurShader;

	private Material material_2;

	public Shader addBrightStuffOneOneShader;

	private Material material_3;

	public Shader screenBlendShader;

	private Material material_4;

	public Shader hollywoodFlaresShader;

	private Material material_5;

	public Shader brightPassFilterShader;

	private Material material_6;

	public BloomAndLensFlares()
	{
		screenBlendMode = BloomScreenBlendMode.Add;
		hdr = HDRBloomMode.Auto;
		sepBlurSpread = 1.5f;
		useSrcAlphaAsMask = 0.5f;
		bloomIntensity = 1f;
		bloomThreshhold = 0.5f;
		bloomBlurIterations = 2;
		hollywoodFlareBlurIterations = 2;
		lensflareMode = LensflareStyle34.Anamorphic;
		hollyStretchWidth = 3.5f;
		lensflareIntensity = 1f;
		lensflareThreshhold = 0.3f;
		flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);
		blurWidth = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_4 = CheckShaderAndCreateMaterial(screenBlendShader, material_4);
		material_0 = CheckShaderAndCreateMaterial(lensFlareShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(vignetteShader, material_1);
		material_2 = CheckShaderAndCreateMaterial(separableBlurShader, material_2);
		material_3 = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, material_3);
		material_5 = CheckShaderAndCreateMaterial(hollywoodFlaresShader, material_5);
		material_6 = CheckShaderAndCreateMaterial(brightPassFilterShader, material_6);
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		bool_3 = false;
		if (hdr == HDRBloomMode.Auto)
		{
			bool num = renderTexture_0.format == RenderTextureFormat.ARGBHalf;
			if (num)
			{
				num = GetComponent<Camera>().hdr;
			}
			bool_3 = num;
		}
		else
		{
			bool_3 = hdr == HDRBloomMode.On;
		}
		bool num2 = bool_3;
		if (num2)
		{
			num2 = bool_0;
		}
		bool_3 = num2;
		BloomScreenBlendMode pass = screenBlendMode;
		if (bool_3)
		{
			pass = BloomScreenBlendMode.Add;
		}
		RenderTextureFormat format = ((!bool_3) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width / 2, renderTexture_0.height / 2, 0, format);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		RenderTexture temporary3 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		RenderTexture temporary4 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		float num3 = 1f * (float)renderTexture_0.width / (1f * (float)renderTexture_0.height);
		float num4 = 0.001953125f;
		Graphics.Blit(renderTexture_0, temporary, material_4, 2);
		Graphics.Blit(temporary, temporary2, material_4, 2);
		RenderTexture.ReleaseTemporary(temporary);
		BrightFilter(bloomThreshhold, useSrcAlphaAsMask, temporary2, temporary3);
		temporary2.DiscardContents();
		if (bloomBlurIterations < 1)
		{
			bloomBlurIterations = 1;
		}
		for (int i = 0; i < bloomBlurIterations; i++)
		{
			float num5 = (1f + (float)i * 0.5f) * sepBlurSpread;
			material_2.SetVector("offsets", new Vector4(0f, num5 * num4, 0f, 0f));
			RenderTexture renderTexture = ((i != 0) ? temporary2 : temporary3);
			Graphics.Blit(renderTexture, temporary4, material_2);
			renderTexture.DiscardContents();
			material_2.SetVector("offsets", new Vector4(num5 / num3 * num4, 0f, 0f, 0f));
			Graphics.Blit(temporary4, temporary2, material_2);
			temporary4.DiscardContents();
		}
		if (lensflares)
		{
			if (lensflareMode == LensflareStyle34.Ghosting)
			{
				BrightFilter(lensflareThreshhold, 0f, temporary2, temporary4);
				temporary2.DiscardContents();
				Vignette(0.975f, temporary4, temporary3);
				temporary4.DiscardContents();
				BlendFlares(temporary3, temporary2);
				temporary3.DiscardContents();
			}
			else
			{
				material_5.SetVector("_Threshhold", new Vector4(lensflareThreshhold, 1f / (1f - lensflareThreshhold), 0f, 0f));
				material_5.SetVector("tintColor", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
				Graphics.Blit(temporary4, temporary3, material_5, 2);
				temporary4.DiscardContents();
				Graphics.Blit(temporary3, temporary4, material_5, 3);
				temporary3.DiscardContents();
				material_5.SetVector("offsets", new Vector4(sepBlurSpread * 1f / num3 * num4, 0f, 0f, 0f));
				material_5.SetFloat("stretchWidth", hollyStretchWidth);
				Graphics.Blit(temporary4, temporary3, material_5, 1);
				temporary4.DiscardContents();
				material_5.SetFloat("stretchWidth", hollyStretchWidth * 2f);
				Graphics.Blit(temporary3, temporary4, material_5, 1);
				temporary3.DiscardContents();
				material_5.SetFloat("stretchWidth", hollyStretchWidth * 4f);
				Graphics.Blit(temporary4, temporary3, material_5, 1);
				temporary4.DiscardContents();
				if (lensflareMode == LensflareStyle34.Anamorphic)
				{
					for (int j = 0; j < hollywoodFlareBlurIterations; j++)
					{
						material_2.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						Graphics.Blit(temporary3, temporary4, material_2);
						temporary3.DiscardContents();
						material_2.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						Graphics.Blit(temporary4, temporary3, material_2);
						temporary4.DiscardContents();
					}
					AddTo(1f, temporary3, temporary2);
					temporary3.DiscardContents();
				}
				else
				{
					for (int k = 0; k < hollywoodFlareBlurIterations; k++)
					{
						material_2.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						Graphics.Blit(temporary3, temporary4, material_2);
						temporary3.DiscardContents();
						material_2.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						Graphics.Blit(temporary4, temporary3, material_2);
						temporary4.DiscardContents();
					}
					Vignette(1f, temporary3, temporary4);
					temporary3.DiscardContents();
					BlendFlares(temporary4, temporary3);
					temporary4.DiscardContents();
					AddTo(1f, temporary3, temporary2);
					temporary3.DiscardContents();
				}
			}
		}
		material_4.SetFloat("_Intensity", bloomIntensity);
		material_4.SetTexture("_ColorBuffer", renderTexture_0);
		Graphics.Blit(temporary2, renderTexture_1, material_4, (int)pass);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
	}

	private void AddTo(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_3.SetFloat("_Intensity", float_0);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_3);
	}

	private void BlendFlares(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_0.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
		material_0.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
		material_0.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
		material_0.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
	}

	private void BrightFilter(float float_0, float float_1, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (bool_3)
		{
			material_6.SetVector("threshhold", new Vector4(float_0, 1f, 0f, 0f));
		}
		else
		{
			material_6.SetVector("threshhold", new Vector4(float_0, 1f / (1f - float_0), 0f, 0f));
		}
		material_6.SetFloat("useSrcAlphaAsMask", float_1);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_6);
	}

	private void Vignette(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if ((bool)lensFlareVignetteMask)
		{
			material_4.SetTexture("_ColorBuffer", lensFlareVignetteMask);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_4, 3);
		}
		else
		{
			material_1.SetFloat("vignetteIntensity", float_0);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_1);
		}
	}

	public override void Main()
	{
	}
}
