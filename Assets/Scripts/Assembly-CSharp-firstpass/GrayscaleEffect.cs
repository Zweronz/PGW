using UnityEngine;

public class GrayscaleEffect : ImageEffectBase
{
	public Texture texture_0;

	public float float_0;

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		base.Material_0.SetTexture("_RampTex", texture_0);
		base.Material_0.SetFloat("_RampOffset", float_0);
		Graphics.Blit(renderTexture_0, renderTexture_1, base.Material_0);
	}
}
