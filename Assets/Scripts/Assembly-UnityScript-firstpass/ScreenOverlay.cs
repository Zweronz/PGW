using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class ScreenOverlay : PostEffectsBase
{
	[Serializable]
	public enum OverlayBlendMode
	{
		Additive = 0,
		ScreenBlend = 1,
		Multiply = 2,
		Overlay = 3,
		AlphaBlend = 4
	}

	public OverlayBlendMode blendMode;

	public float intensity;

	public Texture2D texture;

	public Shader overlayShader;

	private Material material_0;

	public ScreenOverlay()
	{
		blendMode = OverlayBlendMode.Overlay;
		intensity = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_0 = CheckShaderAndCreateMaterial(overlayShader, material_0);
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
		material_0.SetFloat("_Intensity", intensity);
		material_0.SetTexture("_Overlay", texture);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (int)blendMode);
	}

	public override void Main()
	{
	}
}
