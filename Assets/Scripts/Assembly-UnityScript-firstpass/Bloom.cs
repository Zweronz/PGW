using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class Bloom : PostEffectsBase
{
	[Serializable]
	public enum LensFlareStyle
	{
		Ghosting = 0,
		Anamorphic = 1,
		Combined = 2
	}

	[Serializable]
	public enum TweakMode
	{
		Basic = 0,
		Complex = 1
	}

	[Serializable]
	public enum HDRBloomMode
	{
		Auto = 0,
		On = 1,
		Off = 2
	}

	[Serializable]
	public enum BloomScreenBlendMode
	{
		Screen = 0,
		Add = 1
	}

	[Serializable]
	public enum BloomQuality
	{
		Cheap = 0,
		High = 1
	}

	public TweakMode tweakMode;

	public BloomScreenBlendMode screenBlendMode;

	public HDRBloomMode hdr;

	private bool bool_3;

	public float sepBlurSpread;

	public BloomQuality quality;

	public float bloomIntensity;

	public float bloomThreshhold;

	public Color bloomThreshholdColor;

	public int bloomBlurIterations;

	public int hollywoodFlareBlurIterations;

	public float flareRotation;

	public LensFlareStyle lensflareMode;

	public float hollyStretchWidth;

	public float lensflareIntensity;

	public float lensflareThreshhold;

	public float lensFlareSaturation;

	public Color flareColorA;

	public Color flareColorB;

	public Color flareColorC;

	public Color flareColorD;

	public float blurWidth;

	public Texture2D lensFlareVignetteMask;

	public Shader lensFlareShader;

	private Material material_0;

	public Shader screenBlendShader;

	private Material material_1;

	public Shader blurAndFlaresShader;

	private Material material_2;

	public Shader brightPassFilterShader;

	private Material material_3;

	public Bloom()
	{
		screenBlendMode = BloomScreenBlendMode.Add;
		hdr = HDRBloomMode.Auto;
		sepBlurSpread = 2.5f;
		quality = BloomQuality.High;
		bloomIntensity = 0.5f;
		bloomThreshhold = 0.5f;
		bloomThreshholdColor = Color.white;
		bloomBlurIterations = 2;
		hollywoodFlareBlurIterations = 2;
		lensflareMode = LensFlareStyle.Anamorphic;
		hollyStretchWidth = 2.5f;
		lensflareThreshhold = 0.3f;
		lensFlareSaturation = 0.75f;
		flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);
		blurWidth = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_1 = CheckShaderAndCreateMaterial(screenBlendShader, material_1);
		material_0 = CheckShaderAndCreateMaterial(lensFlareShader, material_0);
		material_2 = CheckShaderAndCreateMaterial(blurAndFlaresShader, material_2);
		material_3 = CheckShaderAndCreateMaterial(brightPassFilterShader, material_3);
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
		BloomScreenBlendMode bloomScreenBlendMode = screenBlendMode;
		if (bool_3)
		{
			bloomScreenBlendMode = BloomScreenBlendMode.Add;
		}
		RenderTextureFormat format = ((!bool_3) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width / 2, renderTexture_0.height / 2, 0, format);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		RenderTexture temporary3 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		RenderTexture temporary4 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0, format);
		float num3 = 1f * (float)renderTexture_0.width / (1f * (float)renderTexture_0.height);
		float num4 = 0.001953125f;
		if (quality > BloomQuality.Cheap)
		{
			Graphics.Blit(renderTexture_0, temporary, material_1, 2);
			Graphics.Blit(temporary, temporary3, material_1, 2);
			Graphics.Blit(temporary3, temporary2, material_1, 6);
		}
		else
		{
			Graphics.Blit(renderTexture_0, temporary);
			Graphics.Blit(temporary, temporary2, material_1, 6);
		}
		BrightFilter(bloomThreshhold * bloomThreshholdColor, temporary2, temporary3);
		if (bloomBlurIterations < 1)
		{
			bloomBlurIterations = 1;
		}
		else if (bloomBlurIterations > 10)
		{
			bloomBlurIterations = 10;
		}
		for (int i = 0; i < bloomBlurIterations; i++)
		{
			float num5 = (1f + (float)i * 0.25f) * sepBlurSpread;
			material_2.SetVector("_Offsets", new Vector4(0f, num5 * num4, 0f, 0f));
			Graphics.Blit(temporary3, temporary4, material_2, 4);
			if (quality > BloomQuality.Cheap)
			{
				material_2.SetVector("_Offsets", new Vector4(num5 / num3 * num4, 0f, 0f, 0f));
				Graphics.Blit(temporary4, temporary3, material_2, 4);
				if (i == 0)
				{
					Graphics.Blit(temporary3, temporary2);
				}
				else
				{
					Graphics.Blit(temporary3, temporary2, material_1, 10);
				}
			}
			else
			{
				material_2.SetVector("_Offsets", new Vector4(num5 / num3 * num4, 0f, 0f, 0f));
				Graphics.Blit(temporary4, temporary3, material_2, 4);
			}
		}
		if (quality > BloomQuality.Cheap)
		{
			Graphics.Blit(temporary2, temporary3, material_1, 6);
		}
		if (!(lensflareIntensity <= float.Epsilon))
		{
			if (lensflareMode == LensFlareStyle.Ghosting)
			{
				BrightFilter(lensflareThreshhold, temporary3, temporary4);
				if (quality > BloomQuality.Cheap)
				{
					material_2.SetVector("_Offsets", new Vector4(0f, 1.5f / (1f * (float)temporary2.height), 0f, 0f));
					Graphics.Blit(temporary4, temporary2, material_2, 4);
					material_2.SetVector("_Offsets", new Vector4(1.5f / (1f * (float)temporary2.width), 0f, 0f, 0f));
					Graphics.Blit(temporary2, temporary4, material_2, 4);
				}
				Vignette(0.975f, temporary4, temporary4);
				BlendFlares(temporary4, temporary3);
			}
			else
			{
				float num6 = 1f * Mathf.Cos(flareRotation);
				float num7 = 1f * Mathf.Sin(flareRotation);
				float num8 = hollyStretchWidth * 1f / num3 * num4;
				material_2.SetVector("_Offsets", new Vector4(num6, num7, 0f, 0f));
				material_2.SetVector("_Threshhold", new Vector4(lensflareThreshhold, 1f, 0f, 0f));
				material_2.SetVector("_TintColor", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
				material_2.SetFloat("_Saturation", lensFlareSaturation);
				Graphics.Blit(temporary4, temporary2, material_2, 2);
				Graphics.Blit(temporary2, temporary4, material_2, 3);
				material_2.SetVector("_Offsets", new Vector4(num6 * num8, num7 * num8, 0f, 0f));
				material_2.SetFloat("_StretchWidth", hollyStretchWidth);
				Graphics.Blit(temporary4, temporary2, material_2, 1);
				material_2.SetFloat("_StretchWidth", hollyStretchWidth * 2f);
				Graphics.Blit(temporary2, temporary4, material_2, 1);
				material_2.SetFloat("_StretchWidth", hollyStretchWidth * 4f);
				Graphics.Blit(temporary4, temporary2, material_2, 1);
				for (int i = 0; i < hollywoodFlareBlurIterations; i++)
				{
					num8 = hollyStretchWidth * 2f / num3 * num4;
					material_2.SetVector("_Offsets", new Vector4(num8 * num6, num8 * num7, 0f, 0f));
					Graphics.Blit(temporary2, temporary4, material_2, 4);
					material_2.SetVector("_Offsets", new Vector4(num8 * num6, num8 * num7, 0f, 0f));
					Graphics.Blit(temporary4, temporary2, material_2, 4);
				}
				if (lensflareMode == LensFlareStyle.Anamorphic)
				{
					AddTo(1f, temporary2, temporary3);
				}
				else
				{
					Vignette(1f, temporary2, temporary4);
					BlendFlares(temporary4, temporary2);
					AddTo(1f, temporary2, temporary3);
				}
			}
		}
		int pass = (int)bloomScreenBlendMode;
		material_1.SetFloat("_Intensity", bloomIntensity);
		material_1.SetTexture("_ColorBuffer", renderTexture_0);
		if (quality > BloomQuality.Cheap)
		{
			Graphics.Blit(temporary3, temporary);
			Graphics.Blit(temporary, renderTexture_1, material_1, pass);
		}
		else
		{
			Graphics.Blit(temporary3, renderTexture_1, material_1, pass);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
	}

	private void AddTo(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_1.SetFloat("_Intensity", float_0);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_1, 9);
	}

	private void BlendFlares(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_0.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
		material_0.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
		material_0.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
		material_0.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
	}

	private void BrightFilter(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_3.SetVector("_Threshhold", new Vector4(float_0, float_0, float_0, float_0));
		Graphics.Blit(renderTexture_0, renderTexture_1, material_3, 0);
	}

	private void BrightFilter(Color color_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		material_3.SetVector("_Threshhold", color_0);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_3, 1);
	}

	private void Vignette(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if ((bool)lensFlareVignetteMask)
		{
			material_1.SetTexture("_ColorBuffer", lensFlareVignetteMask);
			Graphics.Blit((!(renderTexture_0 == renderTexture_1)) ? renderTexture_0 : null, renderTexture_1, material_1, (!(renderTexture_0 == renderTexture_1)) ? 3 : 7);
		}
		else if (renderTexture_0 != renderTexture_1)
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
		}
	}

	public override void Main()
	{
	}
}
