using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class Crease : PostEffectsBase
{
	public float intensity;

	public int softness;

	public float spread;

	public Shader blurShader;

	private Material material_0;

	public Shader depthFetchShader;

	private Material material_1;

	public Shader creaseApplyShader;

	private Material material_2;

	public Crease()
	{
		intensity = 0.5f;
		softness = 1;
		spread = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(blurShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(depthFetchShader, material_1);
		material_2 = CheckShaderAndCreateMaterial(creaseApplyShader, material_2);
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
		float num = 1f * (float)renderTexture_0.width / (1f * (float)renderTexture_0.height);
		float num2 = 0.001953125f;
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 2, renderTexture_0.height / 2, 0);
		RenderTexture temporary3 = RenderTexture.GetTemporary(renderTexture_0.width / 2, renderTexture_0.height / 2, 0);
		Graphics.Blit(renderTexture_0, temporary, material_1);
		Graphics.Blit(temporary, temporary2);
		for (int i = 0; i < softness; i++)
		{
			material_0.SetVector("offsets", new Vector4(0f, spread * num2, 0f, 0f));
			Graphics.Blit(temporary2, temporary3, material_0);
			material_0.SetVector("offsets", new Vector4(spread * num2 / num, 0f, 0f, 0f));
			Graphics.Blit(temporary3, temporary2, material_0);
		}
		material_2.SetTexture("_HrDepthTex", temporary);
		material_2.SetTexture("_LrDepthTex", temporary2);
		material_2.SetFloat("intensity", intensity);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_2);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
	}

	public override void Main()
	{
	}
}
