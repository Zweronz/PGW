using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class TiltShift : PostEffectsBase
{
	public Shader tiltShiftShader;

	private Material material_0;

	public int renderTextureDivider;

	public int blurIterations;

	public bool enableForegroundBlur;

	public int foregroundBlurIterations;

	public float maxBlurSpread;

	public float focalPoint;

	public float smoothness;

	public bool visualizeCoc;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	public TiltShift()
	{
		renderTextureDivider = 2;
		blurIterations = 2;
		enableForegroundBlur = true;
		foregroundBlurIterations = 2;
		maxBlurSpread = 1.5f;
		focalPoint = 30f;
		smoothness = 1.65f;
		float_1 = 0.2f;
		float_2 = 1f;
		float_3 = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(tiltShiftShader, material_0);
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
		renderTextureDivider = ((renderTextureDivider < 1) ? 1 : renderTextureDivider);
		renderTextureDivider = ((renderTextureDivider <= 4) ? renderTextureDivider : 4);
		blurIterations = ((blurIterations >= 1) ? blurIterations : 0);
		blurIterations = ((blurIterations <= 4) ? blurIterations : 4);
		float num3 = (float_1 = GetComponent<Camera>().WorldToViewportPoint(focalPoint * GetComponent<Camera>().transform.forward + GetComponent<Camera>().transform.position).z / GetComponent<Camera>().farClipPlane);
		float_0 = 0f;
		float_2 = 1f;
		float_0 = Mathf.Min(num3 - float.Epsilon, float_0);
		float_2 = Mathf.Max(num3 + float.Epsilon, float_2);
		float_3 = smoothness * float_1;
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0);
		RenderTexture temporary3 = RenderTexture.GetTemporary(renderTexture_0.width / renderTextureDivider, renderTexture_0.height / renderTextureDivider, 0);
		RenderTexture temporary4 = RenderTexture.GetTemporary(renderTexture_0.width / renderTextureDivider, renderTexture_0.height / renderTextureDivider, 0);
		material_0.SetVector("_SimpleDofParams", new Vector4(float_0, float_1, float_2, float_3));
		material_0.SetTexture("_Coc", temporary);
		if (enableForegroundBlur)
		{
			Graphics.Blit(renderTexture_0, temporary, material_0, 0);
			Graphics.Blit(temporary, temporary3);
			for (int i = 0; i < foregroundBlurIterations; i++)
			{
				material_0.SetVector("offsets", new Vector4(0f, maxBlurSpread * 0.75f * num2, 0f, 0f));
				Graphics.Blit(temporary3, temporary4, material_0, 3);
				material_0.SetVector("offsets", new Vector4(maxBlurSpread * 0.75f / num * num2, 0f, 0f, 0f));
				Graphics.Blit(temporary4, temporary3, material_0, 3);
			}
			Graphics.Blit(temporary3, temporary2, material_0, 7);
			material_0.SetTexture("_Coc", temporary2);
		}
		else
		{
			RenderTexture.active = temporary;
			GL.Clear(false, true, Color.black);
		}
		Graphics.Blit(renderTexture_0, temporary, material_0, 5);
		material_0.SetTexture("_Coc", temporary);
		Graphics.Blit(renderTexture_0, temporary4);
		for (int j = 0; j < blurIterations; j++)
		{
			material_0.SetVector("offsets", new Vector4(0f, maxBlurSpread * 1f * num2, 0f, 0f));
			Graphics.Blit(temporary4, temporary3, material_0, 6);
			material_0.SetVector("offsets", new Vector4(maxBlurSpread * 1f / num * num2, 0f, 0f, 0f));
			Graphics.Blit(temporary3, temporary4, material_0, 6);
		}
		material_0.SetTexture("_Blurred", temporary4);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (!visualizeCoc) ? 1 : 4);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
	}

	public override void Main()
	{
	}
}
