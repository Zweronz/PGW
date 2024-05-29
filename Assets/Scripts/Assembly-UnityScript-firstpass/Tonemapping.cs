using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[RequireComponent(typeof(Camera))]
public class Tonemapping : PostEffectsBase
{
	[Serializable]
	public enum TonemapperType
	{
		SimpleReinhard = 0,
		UserCurve = 1,
		Hable = 2,
		Photographic = 3,
		OptimizedHejiDawson = 4,
		AdaptiveReinhard = 5,
		AdaptiveReinhardAutoWhite = 6
	}

	[Serializable]
	public enum AdaptiveTexSize
	{
		Square16 = 0x10,
		Square32 = 0x20,
		Square64 = 0x40,
		Square128 = 0x80,
		Square256 = 0x100,
		Square512 = 0x200,
		Square1024 = 0x400
	}

	public TonemapperType type;

	public AdaptiveTexSize adaptiveTextureSize;

	public AnimationCurve remapCurve;

	private Texture2D texture2D_0;

	public float exposureAdjustment;

	public float middleGrey;

	public float white;

	public float adaptionSpeed;

	public Shader tonemapper;

	public bool validRenderTextureFormat;

	private Material material_0;

	private RenderTexture renderTexture_0;

	private RenderTextureFormat renderTextureFormat_0;

	public Tonemapping()
	{
		type = TonemapperType.Photographic;
		adaptiveTextureSize = AdaptiveTexSize.Square256;
		exposureAdjustment = 1.5f;
		middleGrey = 0.4f;
		white = 2f;
		adaptionSpeed = 1.5f;
		validRenderTextureFormat = true;
		renderTextureFormat_0 = RenderTextureFormat.ARGBHalf;
	}

	public override bool CheckResources()
	{
		CheckSupport(false, true);
		material_0 = CheckShaderAndCreateMaterial(tonemapper, material_0);
		if (!texture2D_0 && type == TonemapperType.UserCurve)
		{
			texture2D_0 = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
			texture2D_0.filterMode = FilterMode.Bilinear;
			texture2D_0.wrapMode = TextureWrapMode.Clamp;
			texture2D_0.hideFlags = HideFlags.DontSave;
		}
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual float UpdateCurve()
	{
		float num = 1f;
		if (remapCurve.keys.Length < 1)
		{
			remapCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(2f, 1f));
		}
		if (remapCurve != null)
		{
			if (remapCurve.length != 0)
			{
				num = remapCurve[remapCurve.length - 1].time;
			}
			for (float num2 = 0f; num2 <= 1f; num2 += 0.003921569f)
			{
				float num3 = remapCurve.Evaluate(num2 * 1f * num);
				texture2D_0.SetPixel((int)Mathf.Floor(num2 * 255f), 0, new Color(num3, num3, num3));
			}
			texture2D_0.Apply();
		}
		return 1f / num;
	}

	public virtual void OnDisable()
	{
		if ((bool)renderTexture_0)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = null;
		}
		if ((bool)material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
			material_0 = null;
		}
		if ((bool)texture2D_0)
		{
			UnityEngine.Object.DestroyImmediate(texture2D_0);
			texture2D_0 = null;
		}
	}

	public virtual bool CreateInternalRenderTexture()
	{
		int result;
		if ((bool)renderTexture_0)
		{
			result = 0;
		}
		else
		{
			renderTextureFormat_0 = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf)) ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.RGHalf);
			renderTexture_0 = new RenderTexture(1, 1, 0, renderTextureFormat_0);
			renderTexture_0.hideFlags = HideFlags.DontSave;
			result = 1;
		}
		return (byte)result != 0;
	}

	[ImageEffectTransformsToLDR]
	public virtual void OnRenderImage(RenderTexture renderTexture_1, RenderTexture renderTexture_2)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_1, renderTexture_2);
			return;
		}
		exposureAdjustment = ((exposureAdjustment >= 0.001f) ? exposureAdjustment : 0.001f);
		if (type == TonemapperType.UserCurve)
		{
			float value = UpdateCurve();
			material_0.SetFloat("_RangeScale", value);
			material_0.SetTexture("_Curve", texture2D_0);
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 4);
			return;
		}
		if (type == TonemapperType.SimpleReinhard)
		{
			material_0.SetFloat("_ExposureAdjustment", exposureAdjustment);
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 6);
			return;
		}
		if (type == TonemapperType.Hable)
		{
			material_0.SetFloat("_ExposureAdjustment", exposureAdjustment);
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 5);
			return;
		}
		if (type == TonemapperType.Photographic)
		{
			material_0.SetFloat("_ExposureAdjustment", exposureAdjustment);
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 8);
			return;
		}
		if (type == TonemapperType.OptimizedHejiDawson)
		{
			material_0.SetFloat("_ExposureAdjustment", 0.5f * exposureAdjustment);
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 7);
			return;
		}
		bool flag = CreateInternalRenderTexture();
		RenderTexture temporary = RenderTexture.GetTemporary((int)adaptiveTextureSize, (int)adaptiveTextureSize, 0, renderTextureFormat_0);
		Graphics.Blit(renderTexture_1, temporary);
		int num = (int)Mathf.Log((float)temporary.width * 1f, 2f);
		int num2 = 2;
		RenderTexture[] array = new RenderTexture[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = RenderTexture.GetTemporary(temporary.width / num2, temporary.width / num2, 0, renderTextureFormat_0);
			num2 *= 2;
		}
		float num3 = (float)renderTexture_1.width * 1f / ((float)renderTexture_1.height * 1f);
		RenderTexture source = array[num - 1];
		Graphics.Blit(temporary, array[0], material_0, 1);
		if (type == TonemapperType.AdaptiveReinhardAutoWhite)
		{
			for (int i = 0; i < num - 1; i++)
			{
				Graphics.Blit(array[i], array[i + 1], material_0, 9);
				source = array[i + 1];
			}
		}
		else if (type == TonemapperType.AdaptiveReinhard)
		{
			for (int i = 0; i < num - 1; i++)
			{
				Graphics.Blit(array[i], array[i + 1]);
				source = array[i + 1];
			}
		}
		adaptionSpeed = ((adaptionSpeed >= 0.001f) ? adaptionSpeed : 0.001f);
		material_0.SetFloat("_AdaptionSpeed", adaptionSpeed);
		Graphics.Blit(source, renderTexture_0, material_0, (!flag) ? 2 : 3);
		middleGrey = ((middleGrey >= 0.001f) ? middleGrey : 0.001f);
		material_0.SetVector("_HdrParams", new Vector4(middleGrey, middleGrey, middleGrey, white * white));
		material_0.SetTexture("_SmallTex", renderTexture_0);
		if (type == TonemapperType.AdaptiveReinhard)
		{
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 0);
		}
		else if (type == TonemapperType.AdaptiveReinhardAutoWhite)
		{
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 10);
		}
		else
		{
			Debug.LogError("No valid adaptive tonemapper type found!");
			Graphics.Blit(renderTexture_1, renderTexture_2);
		}
		for (int i = 0; i < num; i++)
		{
			RenderTexture.ReleaseTemporary(array[i]);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	public override void Main()
	{
	}
}
