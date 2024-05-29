using UnityEngine;

public class SepiaToneEffect : ImageEffectBase
{
	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Graphics.Blit(renderTexture_0, renderTexture_1, base.Material_0);
	}
}
