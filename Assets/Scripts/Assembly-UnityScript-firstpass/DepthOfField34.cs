using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class DepthOfField34 : PostEffectsBase
{
	[NonSerialized]
	private static int int_0 = 6;

	[NonSerialized]
	private static float float_0 = 2f;

	public Dof34QualitySetting quality;

	public DofResolution resolution;

	public bool simpleTweakMode;

	public float focalPoint;

	public float smoothness;

	public float focalZDistance;

	public float focalZStartCurve;

	public float focalZEndCurve;

	private float float_1;

	private float float_2;

	private float float_3;

	public Transform objectFocus;

	public float focalSize;

	public DofBlurriness bluriness;

	public float maxBlurSpread;

	public float foregroundBlurExtrude;

	public Shader dofBlurShader;

	private Material material_0;

	public Shader dofShader;

	private Material material_1;

	public bool visualize;

	public BokehDestination bokehDestination;

	private float float_4;

	private float float_5;

	public bool bokeh;

	public bool bokehSupport;

	public Shader bokehShader;

	public Texture2D bokehTexture;

	public float bokehScale;

	public float bokehIntensity;

	public float bokehThreshholdContrast;

	public float bokehThreshholdLuminance;

	public int bokehDownsample;

	private Material material_2;

	private RenderTexture renderTexture_0;

	private RenderTexture renderTexture_1;

	private RenderTexture renderTexture_2;

	private RenderTexture renderTexture_3;

	private RenderTexture renderTexture_4;

	private RenderTexture renderTexture_5;

	public DepthOfField34()
	{
		quality = Dof34QualitySetting.OnlyBackground;
		resolution = DofResolution.Low;
		simpleTweakMode = true;
		focalPoint = 1f;
		smoothness = 0.5f;
		focalZStartCurve = 1f;
		focalZEndCurve = 1f;
		float_1 = 2f;
		float_2 = 2f;
		float_3 = 0.1f;
		bluriness = DofBlurriness.High;
		maxBlurSpread = 1.75f;
		foregroundBlurExtrude = 1.15f;
		bokehDestination = BokehDestination.Background;
		float_4 = 1.25f;
		float_5 = 0.001953125f;
		bokehSupport = true;
		bokehScale = 2.4f;
		bokehIntensity = 0.15f;
		bokehThreshholdContrast = 0.1f;
		bokehThreshholdLuminance = 0.55f;
		bokehDownsample = 1;
	}

	public virtual void CreateMaterials()
	{
		material_0 = CheckShaderAndCreateMaterial(dofBlurShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(dofShader, material_1);
		bokehSupport = bokehShader.isSupported;
		if (bokeh && bokehSupport && (bool)bokehShader)
		{
			material_2 = CheckShaderAndCreateMaterial(bokehShader, material_2);
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(dofBlurShader, material_0);
		material_1 = CheckShaderAndCreateMaterial(dofShader, material_1);
		bokehSupport = bokehShader.isSupported;
		if (bokeh && bokehSupport && (bool)bokehShader)
		{
			material_2 = CheckShaderAndCreateMaterial(bokehShader, material_2);
		}
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnDisable()
	{
		Quads.Cleanup();
	}

	public override void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

	public virtual float FocalDistance01(float float_6)
	{
		return GetComponent<Camera>().WorldToViewportPoint((float_6 - GetComponent<Camera>().nearClipPlane) * GetComponent<Camera>().transform.forward + GetComponent<Camera>().transform.position).z / (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
	}

	public virtual int GetDividerBasedOnQuality()
	{
		int result = 1;
		if (resolution == DofResolution.Medium)
		{
			result = 2;
		}
		else if (resolution == DofResolution.Low)
		{
			result = 2;
		}
		return result;
	}

	public virtual int GetLowResolutionDividerBasedOnQuality(int int_1)
	{
		int num = int_1;
		if (resolution == DofResolution.High)
		{
			num *= 2;
		}
		if (resolution == DofResolution.Low)
		{
			num *= 2;
		}
		return num;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_6, RenderTexture renderTexture_7)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_6, renderTexture_7);
			return;
		}
		if (!(smoothness >= 0.1f))
		{
			smoothness = 0.1f;
		}
		bool num = bokeh;
		if (num)
		{
			num = bokehSupport;
		}
		bokeh = num;
		float num2 = ((!bokeh) ? 1f : float_0);
		bool flag = quality > Dof34QualitySetting.OnlyBackground;
		float num3 = focalSize / (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
		if (simpleTweakMode)
		{
			float_3 = ((!objectFocus) ? FocalDistance01(focalPoint) : (GetComponent<Camera>().WorldToViewportPoint(objectFocus.position).z / GetComponent<Camera>().farClipPlane));
			float_1 = float_3 * smoothness;
			float_2 = float_1;
			bool num4 = flag;
			if (num4)
			{
				num4 = focalPoint > GetComponent<Camera>().nearClipPlane + float.Epsilon;
			}
			flag = num4;
		}
		else
		{
			if ((bool)objectFocus)
			{
				Vector3 vector = GetComponent<Camera>().WorldToViewportPoint(objectFocus.position);
				vector.z /= GetComponent<Camera>().farClipPlane;
				float_3 = vector.z;
			}
			else
			{
				float_3 = FocalDistance01(focalZDistance);
			}
			float_1 = focalZStartCurve;
			float_2 = focalZEndCurve;
			bool num5 = flag;
			if (num5)
			{
				num5 = focalPoint > GetComponent<Camera>().nearClipPlane + float.Epsilon;
			}
			flag = num5;
		}
		float_4 = 1f * (float)renderTexture_6.width / (1f * (float)renderTexture_6.height);
		float_5 = 0.001953125f;
		material_1.SetFloat("_ForegroundBlurExtrude", foregroundBlurExtrude);
		material_1.SetVector("_CurveParams", new Vector4((!simpleTweakMode) ? float_1 : (1f / float_1), (!simpleTweakMode) ? float_2 : (1f / float_2), num3 * 0.5f, float_3));
		material_1.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)renderTexture_6.width), 1f / (1f * (float)renderTexture_6.height), 0f, 0f));
		int dividerBasedOnQuality = GetDividerBasedOnQuality();
		int lowResolutionDividerBasedOnQuality = GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
		AllocateTextures(flag, renderTexture_6, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
		Graphics.Blit(renderTexture_6, renderTexture_6, material_1, 3);
		Downsample(renderTexture_6, renderTexture_1);
		Blur(renderTexture_1, renderTexture_1, DofBlurriness.Low, 4, maxBlurSpread);
		if (bokeh && (bokehDestination & BokehDestination.Background) != 0)
		{
			material_1.SetVector("_Threshhold", new Vector4(bokehThreshholdContrast, bokehThreshholdLuminance, 0.95f, 0f));
			Graphics.Blit(renderTexture_1, renderTexture_5, material_1, 11);
			Graphics.Blit(renderTexture_1, renderTexture_3);
			Blur(renderTexture_3, renderTexture_3, bluriness, 0, maxBlurSpread * num2);
		}
		else
		{
			Downsample(renderTexture_1, renderTexture_3);
			Blur(renderTexture_3, renderTexture_3, bluriness, 0, maxBlurSpread);
		}
		material_0.SetTexture("_TapLow", renderTexture_3);
		material_0.SetTexture("_TapMedium", renderTexture_1);
		Graphics.Blit(null, renderTexture_2, material_0, 3);
		if (bokeh && (bokehDestination & BokehDestination.Background) != 0)
		{
			AddBokeh(renderTexture_5, renderTexture_4, renderTexture_2);
		}
		material_1.SetTexture("_TapLowBackground", renderTexture_2);
		material_1.SetTexture("_TapMedium", renderTexture_1);
		Graphics.Blit(renderTexture_6, (!flag) ? renderTexture_7 : renderTexture_0, material_1, visualize ? 2 : 0);
		if (flag)
		{
			Graphics.Blit(renderTexture_0, renderTexture_6, material_1, 5);
			Downsample(renderTexture_6, renderTexture_1);
			BlurFg(renderTexture_1, renderTexture_1, DofBlurriness.Low, 2, maxBlurSpread);
			if (bokeh && (bokehDestination & BokehDestination.Foreground) != 0)
			{
				material_1.SetVector("_Threshhold", new Vector4(bokehThreshholdContrast * 0.5f, bokehThreshholdLuminance, 0f, 0f));
				Graphics.Blit(renderTexture_1, renderTexture_5, material_1, 11);
				Graphics.Blit(renderTexture_1, renderTexture_3);
				BlurFg(renderTexture_3, renderTexture_3, bluriness, 1, maxBlurSpread * num2);
			}
			else
			{
				BlurFg(renderTexture_1, renderTexture_3, bluriness, 1, maxBlurSpread);
			}
			Graphics.Blit(renderTexture_3, renderTexture_2);
			material_1.SetTexture("_TapLowForeground", renderTexture_2);
			Graphics.Blit(renderTexture_6, renderTexture_7, material_1, visualize ? 1 : 4);
			if (bokeh && (bokehDestination & BokehDestination.Foreground) != 0)
			{
				AddBokeh(renderTexture_5, renderTexture_4, renderTexture_7);
			}
		}
		ReleaseTextures();
	}

	public virtual void Blur(RenderTexture renderTexture_6, RenderTexture renderTexture_7, DofBlurriness dofBlurriness_0, int int_1, float float_6)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_7.width, renderTexture_7.height);
		if (dofBlurriness_0 > DofBlurriness.Low)
		{
			BlurHex(renderTexture_6, renderTexture_7, int_1, float_6, temporary);
			if (dofBlurriness_0 > DofBlurriness.High)
			{
				material_0.SetVector("offsets", new Vector4(0f, float_6 * float_5, 0f, 0f));
				Graphics.Blit(renderTexture_7, temporary, material_0, int_1);
				material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, 0f, 0f, 0f));
				Graphics.Blit(temporary, renderTexture_7, material_0, int_1);
			}
		}
		else
		{
			material_0.SetVector("offsets", new Vector4(0f, float_6 * float_5, 0f, 0f));
			Graphics.Blit(renderTexture_6, temporary, material_0, int_1);
			material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, 0f, 0f, 0f));
			Graphics.Blit(temporary, renderTexture_7, material_0, int_1);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	public virtual void BlurFg(RenderTexture renderTexture_6, RenderTexture renderTexture_7, DofBlurriness dofBlurriness_0, int int_1, float float_6)
	{
		material_0.SetTexture("_TapHigh", renderTexture_6);
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_7.width, renderTexture_7.height);
		if (dofBlurriness_0 > DofBlurriness.Low)
		{
			BlurHex(renderTexture_6, renderTexture_7, int_1, float_6, temporary);
			if (dofBlurriness_0 > DofBlurriness.High)
			{
				material_0.SetVector("offsets", new Vector4(0f, float_6 * float_5, 0f, 0f));
				Graphics.Blit(renderTexture_7, temporary, material_0, int_1);
				material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, 0f, 0f, 0f));
				Graphics.Blit(temporary, renderTexture_7, material_0, int_1);
			}
		}
		else
		{
			material_0.SetVector("offsets", new Vector4(0f, float_6 * float_5, 0f, 0f));
			Graphics.Blit(renderTexture_6, temporary, material_0, int_1);
			material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, 0f, 0f, 0f));
			Graphics.Blit(temporary, renderTexture_7, material_0, int_1);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	public virtual void BlurHex(RenderTexture renderTexture_6, RenderTexture renderTexture_7, int int_1, float float_6, RenderTexture renderTexture_8)
	{
		material_0.SetVector("offsets", new Vector4(0f, float_6 * float_5, 0f, 0f));
		Graphics.Blit(renderTexture_6, renderTexture_8, material_0, int_1);
		material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, 0f, 0f, 0f));
		Graphics.Blit(renderTexture_8, renderTexture_7, material_0, int_1);
		material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, float_6 * float_5, 0f, 0f));
		Graphics.Blit(renderTexture_7, renderTexture_8, material_0, int_1);
		material_0.SetVector("offsets", new Vector4(float_6 / float_4 * float_5, (0f - float_6) * float_5, 0f, 0f));
		Graphics.Blit(renderTexture_8, renderTexture_7, material_0, int_1);
	}

	public virtual void Downsample(RenderTexture renderTexture_6, RenderTexture renderTexture_7)
	{
		material_1.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)renderTexture_7.width), 1f / (1f * (float)renderTexture_7.height), 0f, 0f));
		Graphics.Blit(renderTexture_6, renderTexture_7, material_1, int_0);
	}

	public virtual void AddBokeh(RenderTexture renderTexture_6, RenderTexture renderTexture_7, RenderTexture renderTexture_8)
	{
		if (!material_2)
		{
			return;
		}
		Mesh[] meshes = Quads.GetMeshes(renderTexture_7.width, renderTexture_7.height);
		RenderTexture.active = renderTexture_7;
		GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
		GL.PushMatrix();
		GL.LoadIdentity();
		renderTexture_6.filterMode = FilterMode.Point;
		float num = (float)renderTexture_6.width * 1f / ((float)renderTexture_6.height * 1f);
		float num2 = 2f / (1f * (float)renderTexture_6.width);
		num2 += bokehScale * maxBlurSpread * float_0 * float_5;
		material_2.SetTexture("_Source", renderTexture_6);
		material_2.SetTexture("_MainTex", bokehTexture);
		material_2.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
		material_2.SetFloat("_Intensity", bokehIntensity);
		material_2.SetPass(0);
		int i = 0;
		Mesh[] array = meshes;
		for (int length = array.Length; i < length; i++)
		{
			if ((bool)array[i])
			{
				Graphics.DrawMeshNow(array[i], Matrix4x4.identity);
			}
		}
		GL.PopMatrix();
		Graphics.Blit(renderTexture_7, renderTexture_8, material_1, 8);
		renderTexture_6.filterMode = FilterMode.Bilinear;
	}

	public virtual void ReleaseTextures()
	{
		if ((bool)renderTexture_0)
		{
			RenderTexture.ReleaseTemporary(renderTexture_0);
		}
		if ((bool)renderTexture_2)
		{
			RenderTexture.ReleaseTemporary(renderTexture_2);
		}
		if ((bool)renderTexture_1)
		{
			RenderTexture.ReleaseTemporary(renderTexture_1);
		}
		if ((bool)renderTexture_3)
		{
			RenderTexture.ReleaseTemporary(renderTexture_3);
		}
		if ((bool)renderTexture_4)
		{
			RenderTexture.ReleaseTemporary(renderTexture_4);
		}
		if ((bool)renderTexture_5)
		{
			RenderTexture.ReleaseTemporary(renderTexture_5);
		}
	}

	public virtual void AllocateTextures(bool bool_3, RenderTexture renderTexture_6, int int_1, int int_2)
	{
		renderTexture_0 = null;
		if (bool_3)
		{
			renderTexture_0 = RenderTexture.GetTemporary(renderTexture_6.width, renderTexture_6.height, 0);
		}
		renderTexture_1 = RenderTexture.GetTemporary(renderTexture_6.width / int_1, renderTexture_6.height / int_1, 0);
		renderTexture_2 = RenderTexture.GetTemporary(renderTexture_6.width / int_1, renderTexture_6.height / int_1, 0);
		renderTexture_3 = RenderTexture.GetTemporary(renderTexture_6.width / int_2, renderTexture_6.height / int_2, 0);
		renderTexture_4 = null;
		renderTexture_5 = null;
		if (bokeh)
		{
			renderTexture_4 = RenderTexture.GetTemporary(renderTexture_6.width / (int_2 * bokehDownsample), renderTexture_6.height / (int_2 * bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			renderTexture_5 = RenderTexture.GetTemporary(renderTexture_6.width / (int_2 * bokehDownsample), renderTexture_6.height / (int_2 * bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			renderTexture_4.filterMode = FilterMode.Bilinear;
			renderTexture_5.filterMode = FilterMode.Bilinear;
			RenderTexture.active = renderTexture_5;
			GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
		}
		renderTexture_6.filterMode = FilterMode.Bilinear;
		renderTexture_2.filterMode = FilterMode.Bilinear;
		renderTexture_1.filterMode = FilterMode.Bilinear;
		renderTexture_3.filterMode = FilterMode.Bilinear;
		if ((bool)renderTexture_0)
		{
			renderTexture_0.filterMode = FilterMode.Bilinear;
		}
	}

	public override void Main()
	{
	}
}
