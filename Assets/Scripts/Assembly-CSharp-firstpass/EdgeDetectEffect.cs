using UnityEngine;

public class EdgeDetectEffect : ImageEffectBase
{
	public float float_0 = 0.2f;

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		base.Material_0.SetFloat("_Treshold", float_0 * float_0);
		Graphics.Blit(renderTexture_0, renderTexture_1, base.Material_0);
	}
}
