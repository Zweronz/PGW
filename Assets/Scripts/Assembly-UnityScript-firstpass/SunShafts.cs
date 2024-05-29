using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class SunShafts : PostEffectsBase
{
	public SunShaftsResolution resolution;

	public ShaftsScreenBlendMode screenBlendMode;

	public Transform sunTransform;

	public int radialBlurIterations;

	public Color sunColor;

	public float sunShaftBlurRadius;

	public float sunShaftIntensity;

	public float useSkyBoxAlpha;

	public float maxRadius;

	public bool useDepthTexture;

	public Shader sunShaftsShader;

	private Material material_0;

	public Shader simpleClearShader;

	private Material material_1;

	public SunShafts()
	{
		resolution = SunShaftsResolution.Normal;
		screenBlendMode = ShaftsScreenBlendMode.Screen;
		radialBlurIterations = 2;
		sunColor = Color.white;
		sunShaftBlurRadius = 2.5f;
		sunShaftIntensity = 1.15f;
		useSkyBoxAlpha = 0.75f;
		maxRadius = 0.75f;
		useDepthTexture = true;
	}

	public override bool CheckResources()
	{
		CheckSupport(useDepthTexture);
		material_0 = CheckShaderAndCreateMaterial(sunShaftsShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(simpleClearShader, material_1);
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
		if (useDepthTexture)
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
		float num = 4f;
		if (resolution == SunShaftsResolution.Normal)
		{
			num = 2f;
		}
		else if (resolution == SunShaftsResolution.High)
		{
			num = 1f;
		}
		Vector3 vector = Vector3.one * 0.5f;
		vector = ((!sunTransform) ? new Vector3(0.5f, 0.5f, 0f) : GetComponent<Camera>().WorldToViewportPoint(sunTransform.position));
		RenderTexture temporary = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / num), (int)((float)renderTexture_0.height / num), 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / num), (int)((float)renderTexture_0.height / num), 0);
		material_0.SetVector("_BlurRadius4", new Vector4(1f, 1f, 0f, 0f) * sunShaftBlurRadius);
		material_0.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, maxRadius));
		material_0.SetFloat("_NoSkyBoxMask", 1f - useSkyBoxAlpha);
		if (!useDepthTexture)
		{
			RenderTexture renderTexture = (RenderTexture.active = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0));
			GL.ClearWithSkybox(false, GetComponent<Camera>());
			material_0.SetTexture("_Skybox", renderTexture);
			Graphics.Blit(renderTexture_0, temporary2, material_0, 3);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		else
		{
			Graphics.Blit(renderTexture_0, temporary2, material_0, 2);
		}
		DrawBorder(temporary2, material_1);
		radialBlurIterations = ClampBlurIterationsToSomethingThatMakesSense(radialBlurIterations);
		float num2 = sunShaftBlurRadius * 0.0013020834f;
		material_0.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
		material_0.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, maxRadius));
		for (int i = 0; i < radialBlurIterations; i++)
		{
			Graphics.Blit(temporary2, temporary, material_0, 1);
			num2 = sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
			material_0.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
			Graphics.Blit(temporary, temporary2, material_0, 1);
			num2 = sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
			material_0.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
		}
		if (!(vector.z < 0f))
		{
			material_0.SetVector("_SunColor", new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * sunShaftIntensity);
		}
		else
		{
			material_0.SetVector("_SunColor", Vector4.zero);
		}
		material_0.SetTexture("_ColorBuffer", temporary2);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (screenBlendMode != 0) ? 4 : 0);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary);
	}

	private int ClampBlurIterationsToSomethingThatMakesSense(int int_0)
	{
		return (int_0 < 1) ? 1 : ((int_0 <= 4) ? int_0 : 4);
	}

	public override void Main()
	{
	}
}
