using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class Fisheye : PostEffectsBase
{
	public float strengthX;

	public float strengthY;

	public Shader fishEyeShader;

	private Material material_0;

	public Fisheye()
	{
		strengthX = 0.05f;
		strengthY = 0.05f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_0 = CheckShaderAndCreateMaterial(fishEyeShader, material_0);
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
		float num = 5f / 32f;
		float num2 = (float)renderTexture_0.width * 1f / ((float)renderTexture_0.height * 1f);
		material_0.SetVector("intensity", new Vector4(strengthX * num2 * num, strengthY * num, strengthX * num2 * num, strengthY * num));
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
	}

	public override void Main()
	{
	}
}
