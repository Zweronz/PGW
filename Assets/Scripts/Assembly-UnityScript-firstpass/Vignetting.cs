using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class Vignetting : PostEffectsBase
{
	[Serializable]
	public enum AberrationMode
	{
		Simple = 0,
		Advanced = 1
	}

	public AberrationMode mode;

	public float intensity;

	public float chromaticAberration;

	public float axialAberration;

	public float blur;

	public float blurSpread;

	public float luminanceDependency;

	public Shader vignetteShader;

	private Material material_0;

	public Shader separableBlurShader;

	private Material material_1;

	public Shader chromAberrationShader;

	private Material material_2;

	public Vignetting()
	{
		mode = AberrationMode.Simple;
		intensity = 0.375f;
		chromaticAberration = 0.2f;
		axialAberration = 0.5f;
		blurSpread = 0.75f;
		luminanceDependency = 0.25f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_0 = CheckShaderAndCreateMaterial(vignetteShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(separableBlurShader, material_1);
		material_2 = CheckShaderAndCreateMaterial(chromAberrationShader, material_2);
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
		bool num = Mathf.Abs(blur) > 0f;
		if (!num)
		{
			num = Mathf.Abs(intensity) > 0f;
		}
		bool flag = num;
		float num2 = 1f * (float)renderTexture_0.width / (1f * (float)renderTexture_0.height);
		float num3 = 0.001953125f;
		RenderTexture renderTexture = null;
		RenderTexture renderTexture2 = null;
		RenderTexture renderTexture3 = null;
		if (flag)
		{
			renderTexture = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0, renderTexture_0.format);
			if (!(Mathf.Abs(blur) <= 0f))
			{
				renderTexture2 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / 2f), (int)((float)renderTexture_0.height / 2f), 0, renderTexture_0.format);
				renderTexture3 = RenderTexture.GetTemporary((int)((float)renderTexture_0.width / 2f), (int)((float)renderTexture_0.height / 2f), 0, renderTexture_0.format);
				Graphics.Blit(renderTexture_0, renderTexture2, material_2, 0);
				for (int i = 0; i < 2; i++)
				{
					material_1.SetVector("offsets", new Vector4(0f, blurSpread * num3, 0f, 0f));
					Graphics.Blit(renderTexture2, renderTexture3, material_1);
					material_1.SetVector("offsets", new Vector4(blurSpread * num3 / num2, 0f, 0f, 0f));
					Graphics.Blit(renderTexture3, renderTexture2, material_1);
				}
			}
			material_0.SetFloat("_Intensity", intensity);
			material_0.SetFloat("_Blur", blur);
			material_0.SetTexture("_VignetteTex", renderTexture2);
			Graphics.Blit(renderTexture_0, renderTexture, material_0, 0);
		}
		material_2.SetFloat("_ChromaticAberration", chromaticAberration);
		material_2.SetFloat("_AxialAberration", axialAberration);
		material_2.SetFloat("_Luminance", 1f / (float.Epsilon + luminanceDependency));
		if (flag)
		{
			renderTexture.wrapMode = TextureWrapMode.Clamp;
		}
		else
		{
			renderTexture_0.wrapMode = TextureWrapMode.Clamp;
		}
		Graphics.Blit((!flag) ? renderTexture_0 : renderTexture, renderTexture_1, material_2, (mode != AberrationMode.Advanced) ? 1 : 2);
		if ((bool)renderTexture)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		if ((bool)renderTexture2)
		{
			RenderTexture.ReleaseTemporary(renderTexture2);
		}
		if ((bool)renderTexture3)
		{
			RenderTexture.ReleaseTemporary(renderTexture3);
		}
	}

	public override void Main()
	{
	}
}
