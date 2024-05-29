using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class AntialiasingAsPostEffect : PostEffectsBase
{
	public AAMode mode;

	public bool showGeneratedNormals;

	public float offsetScale;

	public float blurRadius;

	public float edgeThresholdMin;

	public float edgeThreshold;

	public float edgeSharpness;

	public bool dlaaSharp;

	public Shader ssaaShader;

	private Material material_0;

	public Shader dlaaShader;

	private Material material_1;

	public Shader nfaaShader;

	private Material material_2;

	public Shader shaderFXAAPreset2;

	private Material material_3;

	public Shader shaderFXAAPreset3;

	private Material material_4;

	public Shader shaderFXAAII;

	private Material material_5;

	public Shader shaderFXAAIII;

	private Material material_6;

	public AntialiasingAsPostEffect()
	{
		mode = AAMode.FXAA3Console;
		offsetScale = 0.2f;
		blurRadius = 18f;
		edgeThresholdMin = 0.05f;
		edgeThreshold = 0.2f;
		edgeSharpness = 4f;
	}

	public virtual Material CurrentAAMaterial()
	{
		Material material = null;
		switch (mode)
		{
		case AAMode.FXAA3Console:
			return material_6;
		case AAMode.FXAA2:
			return material_5;
		case AAMode.FXAA1PresetA:
			return material_3;
		case AAMode.FXAA1PresetB:
			return material_4;
		case AAMode.NFAA:
			return material_2;
		case AAMode.SSAA:
			return material_0;
		case AAMode.DLAA:
			return material_1;
		default:
			return null;
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_3 = CreateMaterial(shaderFXAAPreset2, material_3);
		material_4 = CreateMaterial(shaderFXAAPreset3, material_4);
		material_5 = CreateMaterial(shaderFXAAII, material_5);
		material_6 = CreateMaterial(shaderFXAAIII, material_6);
		material_2 = CreateMaterial(nfaaShader, material_2);
		material_0 = CreateMaterial(ssaaShader, material_0);
		material_1 = CreateMaterial(dlaaShader, material_1);
		if (!ssaaShader.isSupported)
		{
			NotSupported();
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
		}
		else if (mode == AAMode.FXAA3Console && material_6 != null)
		{
			material_6.SetFloat("_EdgeThresholdMin", edgeThresholdMin);
			material_6.SetFloat("_EdgeThreshold", edgeThreshold);
			material_6.SetFloat("_EdgeSharpness", edgeSharpness);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_6);
		}
		else if (mode == AAMode.FXAA1PresetB && material_4 != null)
		{
			Graphics.Blit(renderTexture_0, renderTexture_1, material_4);
		}
		else if (mode == AAMode.FXAA1PresetA && material_3 != null)
		{
			renderTexture_0.anisoLevel = 4;
			Graphics.Blit(renderTexture_0, renderTexture_1, material_3);
			renderTexture_0.anisoLevel = 0;
		}
		else if (mode == AAMode.FXAA2 && material_5 != null)
		{
			Graphics.Blit(renderTexture_0, renderTexture_1, material_5);
		}
		else if (mode == AAMode.SSAA && material_0 != null)
		{
			Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
		}
		else if (mode == AAMode.DLAA && material_1 != null)
		{
			renderTexture_0.anisoLevel = 0;
			RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height);
			Graphics.Blit(renderTexture_0, temporary, material_1, 0);
			Graphics.Blit(temporary, renderTexture_1, material_1, (!dlaaSharp) ? 1 : 2);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else if (mode == AAMode.NFAA && material_2 != null)
		{
			renderTexture_0.anisoLevel = 0;
			material_2.SetFloat("_OffsetScale", offsetScale);
			material_2.SetFloat("_BlurRadius", blurRadius);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_2, showGeneratedNormals ? 1 : 0);
		}
		else
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
		}
	}

	public override void Main()
	{
	}
}
