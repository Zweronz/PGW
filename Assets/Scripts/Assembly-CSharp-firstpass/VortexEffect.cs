using UnityEngine;

public class VortexEffect : ImageEffectBase
{
	public Vector2 vector2_0 = new Vector2(0.4f, 0.4f);

	public float float_0 = 50f;

	public Vector2 vector2_1 = new Vector2(0.5f, 0.5f);

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		ImageEffects.RenderDistortion(base.Material_0, renderTexture_0, renderTexture_1, float_0, vector2_1, vector2_0);
	}
}
