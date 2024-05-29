using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class ContrastEnhance : PostEffectsBase
{
	public float intensity;

	public float threshhold;

	private Material material_0;

	private Material material_1;

	public float blurSpread;

	public Shader separableBlurShader;

	public Shader contrastCompositeShader;

	public ContrastEnhance()
	{
		intensity = 0.5f;
		blurSpread = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_1 = CheckShaderAndCreateMaterial(contrastCompositeShader, material_1);
		material_0 = CheckShaderAndCreateMaterial(separableBlurShader, material_0);
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
		RenderTexture temporary = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / 2f), (int)((float)renderTexture_0.height / 2f), 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / 4f), (int)((float)renderTexture_0.height / 4f), 0);
		RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / 4f), (int)((float)renderTexture_0.height / 4f), 0);
		Graphics.Blit(renderTexture_0, temporary);
		Graphics.Blit(temporary, temporary2);
		material_0.SetVector("offsets", new Vector4(0f, blurSpread * 1f / (float)temporary2.height, 0f, 0f));
		Graphics.Blit(temporary2, temporary3, material_0);
		material_0.SetVector("offsets", new Vector4(blurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
		Graphics.Blit(temporary3, temporary2, material_0);
		material_1.SetTexture("_MainTexBlurred", temporary2);
		material_1.SetFloat("intensity", intensity);
		material_1.SetFloat("threshhold", threshhold);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_1);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
	}

	public override void Main()
	{
	}
}
