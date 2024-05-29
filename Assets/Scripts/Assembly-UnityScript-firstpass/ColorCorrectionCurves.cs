using System;
using UnityEngine;

[Serializable]
public class ColorCorrectionCurves : PostEffectsBase
{
	public AnimationCurve redChannel;

	public AnimationCurve greenChannel;

	public AnimationCurve blueChannel;

	public bool useDepthCorrection;

	public AnimationCurve zCurve;

	public AnimationCurve depthRedChannel;

	public AnimationCurve depthGreenChannel;

	public AnimationCurve depthBlueChannel;

	private Material material_0;

	private Material material_1;

	private Material material_2;

	private Texture2D texture2D_0;

	private Texture2D texture2D_1;

	private Texture2D texture2D_2;

	public float saturation;

	public bool selectiveCc;

	public Color selectiveFromColor;

	public Color selectiveToColor;

	public ColorCorrectionMode mode;

	public bool updateTextures;

	public Shader colorCorrectionCurvesShader;

	public Shader simpleColorCorrectionCurvesShader;

	public Shader colorCorrectionSelectiveShader;

	private bool bool_3;

	public ColorCorrectionCurves()
	{
		saturation = 1f;
		selectiveFromColor = Color.white;
		selectiveToColor = Color.white;
		updateTextures = true;
		bool_3 = true;
	}

	public override void Start()
	{
		base.Start();
		bool_3 = true;
	}

	public virtual void Awake()
	{
	}

	public override bool CheckResources()
	{
		CheckSupport(mode == ColorCorrectionMode.Advanced);
		material_0 = CheckShaderAndCreateMaterial(simpleColorCorrectionCurvesShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(colorCorrectionCurvesShader, material_1);
		material_2 = CheckShaderAndCreateMaterial(colorCorrectionSelectiveShader, material_2);
		if (!texture2D_0)
		{
			texture2D_0 = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!texture2D_1)
		{
			texture2D_1 = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!texture2D_2)
		{
			texture2D_2 = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
		}
		texture2D_0.hideFlags = HideFlags.DontSave;
		texture2D_1.hideFlags = HideFlags.DontSave;
		texture2D_2.hideFlags = HideFlags.DontSave;
		texture2D_0.wrapMode = TextureWrapMode.Clamp;
		texture2D_1.wrapMode = TextureWrapMode.Clamp;
		texture2D_2.wrapMode = TextureWrapMode.Clamp;
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void UpdateParameters()
	{
		if (redChannel != null && greenChannel != null && blueChannel != null)
		{
			for (float num = 0f; num <= 1f; num += 0.003921569f)
			{
				float num2 = Mathf.Clamp(redChannel.Evaluate(num), 0f, 1f);
				float num3 = Mathf.Clamp(greenChannel.Evaluate(num), 0f, 1f);
				float num4 = Mathf.Clamp(blueChannel.Evaluate(num), 0f, 1f);
				texture2D_0.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				texture2D_0.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				texture2D_0.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
				float num5 = Mathf.Clamp(zCurve.Evaluate(num), 0f, 1f);
				texture2D_2.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num5, num5, num5));
				num2 = Mathf.Clamp(depthRedChannel.Evaluate(num), 0f, 1f);
				num3 = Mathf.Clamp(depthGreenChannel.Evaluate(num), 0f, 1f);
				num4 = Mathf.Clamp(depthBlueChannel.Evaluate(num), 0f, 1f);
				texture2D_1.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				texture2D_1.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				texture2D_1.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
			}
			texture2D_0.Apply();
			texture2D_1.Apply();
			texture2D_2.Apply();
		}
	}

	public virtual void UpdateTextures()
	{
		UpdateParameters();
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		if (bool_3)
		{
			UpdateParameters();
			bool_3 = false;
		}
		if (useDepthCorrection)
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
		RenderTexture renderTexture = renderTexture_1;
		if (selectiveCc)
		{
			renderTexture = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height);
		}
		if (useDepthCorrection)
		{
			material_1.SetTexture("_RgbTex", texture2D_0);
			material_1.SetTexture("_ZCurve", texture2D_2);
			material_1.SetTexture("_RgbDepthTex", texture2D_1);
			material_1.SetFloat("_Saturation", saturation);
			Graphics.Blit(renderTexture_0, renderTexture, material_1);
		}
		else
		{
			material_0.SetTexture("_RgbTex", texture2D_0);
			material_0.SetFloat("_Saturation", saturation);
			Graphics.Blit(renderTexture_0, renderTexture, material_0);
		}
		if (selectiveCc)
		{
			material_2.SetColor("selColor", selectiveFromColor);
			material_2.SetColor("targetColor", selectiveToColor);
			Graphics.Blit(renderTexture, renderTexture_1, material_2);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	public override void Main()
	{
	}
}
