using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MotionBlur : ImageEffectBase
{
	public float float_0 = 0.8f;

	public bool bool_0;

	private RenderTexture renderTexture_0;

	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
		}
		else
		{
			base.Start();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Object.DestroyImmediate(renderTexture_0);
	}

	private void OnRenderImage(RenderTexture renderTexture_1, RenderTexture renderTexture_2)
	{
		if (renderTexture_0 == null || renderTexture_0.width != renderTexture_1.width || renderTexture_0.height != renderTexture_1.height)
		{
			Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = new RenderTexture(renderTexture_1.width, renderTexture_1.height, 0);
			renderTexture_0.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit(renderTexture_1, renderTexture_0);
		}
		if (bool_0)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_1.width / 4, renderTexture_1.height / 4, 0);
			Graphics.Blit(renderTexture_0, temporary);
			Graphics.Blit(temporary, renderTexture_0);
			RenderTexture.ReleaseTemporary(temporary);
		}
		float_0 = Mathf.Clamp(float_0, 0f, 0.92f);
		base.Material_0.SetTexture("_MainTex", renderTexture_0);
		base.Material_0.SetFloat("_AccumOrig", 1f - float_0);
		Graphics.Blit(renderTexture_1, renderTexture_0, base.Material_0);
		Graphics.Blit(renderTexture_0, renderTexture_2);
	}
}
