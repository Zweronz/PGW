using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class DepthOfFieldScatter : PostEffectsBase
{
	[Serializable]
	public enum BlurType
	{
		DiscBlur = 0,
		DX11 = 1
	}

	[Serializable]
	public enum BlurSampleCount
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

	public bool visualizeFocus;

	public float focalLength;

	public float focalSize;

	public float aperture;

	public Transform focalTransform;

	public float maxBlurSize;

	public bool highResolution;

	public BlurType blurType;

	public BlurSampleCount blurSampleCount;

	public bool nearBlur;

	public float foregroundOverlap;

	public Shader dofHdrShader;

	private Material material_0;

	public Shader dx11BokehShader;

	private Material material_1;

	public float dx11BokehThreshhold;

	public float dx11SpawnHeuristic;

	public Texture2D dx11BokehTexture;

	public float dx11BokehScale;

	public float dx11BokehIntensity;

	private float float_0;

	private ComputeBuffer computeBuffer_0;

	private ComputeBuffer computeBuffer_1;

	private float float_1;

	public DepthOfFieldScatter()
	{
		focalLength = 10f;
		focalSize = 0.05f;
		aperture = 11.5f;
		maxBlurSize = 2f;
		blurType = BlurType.DiscBlur;
		blurSampleCount = BlurSampleCount.High;
		foregroundOverlap = 1f;
		dx11BokehThreshhold = 0.5f;
		dx11SpawnHeuristic = 0.0875f;
		dx11BokehScale = 1.2f;
		dx11BokehIntensity = 2.5f;
		float_0 = 10f;
		float_1 = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(dofHdrShader, material_0);
		if (bool_1 && blurType == BlurType.DX11)
		{
			material_1 = CheckShaderAndCreateMaterial(dx11BokehShader, material_1);
			CreateComputeResources();
		}
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public override void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

	public virtual void OnDisable()
	{
		ReleaseComputeResources();
		if ((bool)material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
		}
		material_0 = null;
		if ((bool)material_1)
		{
			UnityEngine.Object.DestroyImmediate(material_1);
		}
		material_1 = null;
	}

	public virtual void ReleaseComputeResources()
	{
		if (computeBuffer_0 != null)
		{
			computeBuffer_0.Release();
		}
		computeBuffer_0 = null;
		if (computeBuffer_1 != null)
		{
			computeBuffer_1.Release();
		}
		computeBuffer_1 = null;
	}

	public virtual void CreateComputeResources()
	{
		if (RuntimeServices.EqualityOperator(computeBuffer_0, null))
		{
			computeBuffer_0 = new ComputeBuffer(1, 16, ComputeBufferType.IndirectArguments);
			int[] data = new int[4] { 0, 1, 0, 0 };
			computeBuffer_0.SetData(data);
		}
		if (RuntimeServices.EqualityOperator(computeBuffer_1, null))
		{
			computeBuffer_1 = new ComputeBuffer(90000, 28, ComputeBufferType.Append);
		}
	}

	public virtual float FocalDistance01(float float_2)
	{
		return GetComponent<Camera>().WorldToViewportPoint((float_2 - GetComponent<Camera>().nearClipPlane) * GetComponent<Camera>().transform.forward + GetComponent<Camera>().transform.position).z / (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
	}

	private void WriteCoc(RenderTexture renderTexture_0, RenderTexture renderTexture_1, RenderTexture renderTexture_2, bool bool_3)
	{
		material_0.SetTexture("_FgOverlap", null);
		if (nearBlur && bool_3)
		{
			Graphics.Blit(renderTexture_0, renderTexture_2, material_0, 4);
			float num = float_1 * foregroundOverlap;
			material_0.SetVector("_Offsets", new Vector4(0f, num, 0f, num));
			Graphics.Blit(renderTexture_2, renderTexture_1, material_0, 2);
			material_0.SetVector("_Offsets", new Vector4(num, 0f, 0f, num));
			Graphics.Blit(renderTexture_1, renderTexture_2, material_0, 2);
			material_0.SetTexture("_FgOverlap", renderTexture_2);
			Graphics.Blit(renderTexture_0, renderTexture_0, material_0, 13);
		}
		else
		{
			Graphics.Blit(renderTexture_0, renderTexture_0, material_0, 0);
		}
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		if (!(aperture >= 0f))
		{
			aperture = 0f;
		}
		if (!(maxBlurSize >= 0.1f))
		{
			maxBlurSize = 0.1f;
		}
		focalSize = Mathf.Clamp(focalSize, 0f, 2f);
		float_1 = Mathf.Max(maxBlurSize, 0f);
		float_0 = ((!focalTransform) ? FocalDistance01(focalLength) : (GetComponent<Camera>().WorldToViewportPoint(focalTransform.position).z / GetComponent<Camera>().farClipPlane));
		material_0.SetVector("_CurveParams", new Vector4(1f, focalSize, aperture / 10f, float_0));
		RenderTexture renderTexture = null;
		RenderTexture renderTexture2 = null;
		RenderTexture renderTexture3 = null;
		RenderTexture renderTexture4 = null;
		float num = float_1 * foregroundOverlap;
		if (visualizeFocus)
		{
			renderTexture = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
			renderTexture2 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
			WriteCoc(renderTexture_0, renderTexture, renderTexture2, true);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 16);
		}
		else if (blurType == BlurType.DX11 && (bool)material_1)
		{
			if (highResolution)
			{
				float_1 = ((float_1 >= 0.1f) ? float_1 : 0.1f);
				num = float_1 * foregroundOverlap;
				renderTexture = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0, renderTexture_0.format);
				RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0, renderTexture_0.format);
				WriteCoc(renderTexture_0, null, null, false);
				renderTexture3 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
				renderTexture4 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
				Graphics.Blit(renderTexture_0, renderTexture3, material_0, 15);
				material_0.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
				Graphics.Blit(renderTexture3, renderTexture4, material_0, 19);
				material_0.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
				Graphics.Blit(renderTexture4, renderTexture3, material_0, 19);
				if (nearBlur)
				{
					Graphics.Blit(renderTexture_0, renderTexture4, material_0, 4);
				}
				material_1.SetTexture("_BlurredColor", renderTexture3);
				material_1.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
				material_1.SetVector("_BokehParams", new Vector4(dx11BokehScale, dx11BokehIntensity, Mathf.Clamp(dx11BokehThreshhold, 0.005f, 4f), float_1));
				material_1.SetTexture("_FgCocMask", (!nearBlur) ? null : renderTexture4);
				Graphics.SetRandomWriteTarget(1, computeBuffer_1);
				Graphics.Blit(renderTexture_0, renderTexture, material_1, 0);
				Graphics.ClearRandomWriteTargets();
				if (nearBlur)
				{
					material_0.SetVector("_Offsets", new Vector4(0f, num, 0f, num));
					Graphics.Blit(renderTexture4, renderTexture3, material_0, 2);
					material_0.SetVector("_Offsets", new Vector4(num, 0f, 0f, num));
					Graphics.Blit(renderTexture3, renderTexture4, material_0, 2);
					Graphics.Blit(renderTexture4, renderTexture, material_0, 3);
				}
				Graphics.Blit(renderTexture, temporary, material_0, 20);
				material_0.SetVector("_Offsets", new Vector4(float_1, 0f, 0f, float_1));
				Graphics.Blit(renderTexture, renderTexture_0, material_0, 5);
				material_0.SetVector("_Offsets", new Vector4(0f, float_1, 0f, float_1));
				Graphics.Blit(renderTexture_0, temporary, material_0, 21);
				Graphics.SetRenderTarget(temporary);
				ComputeBuffer.CopyCount(computeBuffer_1, computeBuffer_0, 0);
				material_1.SetBuffer("pointBuffer", computeBuffer_1);
				material_1.SetTexture("_MainTex", dx11BokehTexture);
				material_1.SetVector("_Screen", new Vector3(1f / (1f * (float)renderTexture_0.width), 1f / (1f * (float)renderTexture_0.height), float_1));
				material_1.SetPass(2);
				Graphics.DrawProceduralIndirect(MeshTopology.Points, computeBuffer_0, 0);
				Graphics.Blit(temporary, renderTexture_1);
				RenderTexture.ReleaseTemporary(temporary);
				RenderTexture.ReleaseTemporary(renderTexture3);
				RenderTexture.ReleaseTemporary(renderTexture4);
			}
			else
			{
				renderTexture = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
				renderTexture2 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
				num = float_1 * foregroundOverlap;
				WriteCoc(renderTexture_0, null, null, false);
				renderTexture_0.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture_0, renderTexture, material_0, 6);
				renderTexture3 = RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
				renderTexture4 = RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
				Graphics.Blit(renderTexture, renderTexture3, material_0, 15);
				material_0.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
				Graphics.Blit(renderTexture3, renderTexture4, material_0, 19);
				material_0.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
				Graphics.Blit(renderTexture4, renderTexture3, material_0, 19);
				RenderTexture renderTexture5 = null;
				if (nearBlur)
				{
					renderTexture5 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
					Graphics.Blit(renderTexture_0, renderTexture5, material_0, 4);
				}
				material_1.SetTexture("_BlurredColor", renderTexture3);
				material_1.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
				material_1.SetVector("_BokehParams", new Vector4(dx11BokehScale, dx11BokehIntensity, Mathf.Clamp(dx11BokehThreshhold, 0.005f, 4f), float_1));
				material_1.SetTexture("_FgCocMask", renderTexture5);
				Graphics.SetRandomWriteTarget(1, computeBuffer_1);
				Graphics.Blit(renderTexture, renderTexture2, material_1, 0);
				Graphics.ClearRandomWriteTargets();
				RenderTexture.ReleaseTemporary(renderTexture3);
				RenderTexture.ReleaseTemporary(renderTexture4);
				if (nearBlur)
				{
					material_0.SetVector("_Offsets", new Vector4(0f, num, 0f, num));
					Graphics.Blit(renderTexture5, renderTexture, material_0, 2);
					material_0.SetVector("_Offsets", new Vector4(num, 0f, 0f, num));
					Graphics.Blit(renderTexture, renderTexture5, material_0, 2);
					Graphics.Blit(renderTexture5, renderTexture2, material_0, 3);
				}
				material_0.SetVector("_Offsets", new Vector4(float_1, 0f, 0f, float_1));
				Graphics.Blit(renderTexture2, renderTexture, material_0, 5);
				material_0.SetVector("_Offsets", new Vector4(0f, float_1, 0f, float_1));
				Graphics.Blit(renderTexture, renderTexture2, material_0, 5);
				Graphics.SetRenderTarget(renderTexture2);
				ComputeBuffer.CopyCount(computeBuffer_1, computeBuffer_0, 0);
				material_1.SetBuffer("pointBuffer", computeBuffer_1);
				material_1.SetTexture("_MainTex", dx11BokehTexture);
				material_1.SetVector("_Screen", new Vector3(1f / (1f * (float)renderTexture2.width), 1f / (1f * (float)renderTexture2.height), float_1));
				material_1.SetPass(1);
				Graphics.DrawProceduralIndirect(MeshTopology.Points, computeBuffer_0, 0);
				material_0.SetTexture("_LowRez", renderTexture2);
				material_0.SetTexture("_FgOverlap", renderTexture5);
				material_0.SetVector("_Offsets", 1f * (float)renderTexture_0.width / (1f * (float)renderTexture2.width) * float_1 * Vector4.one);
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 9);
				if ((bool)renderTexture5)
				{
					RenderTexture.ReleaseTemporary(renderTexture5);
				}
			}
		}
		else
		{
			renderTexture = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
			renderTexture2 = RenderTexture.GetTemporary(renderTexture_0.width >> 1, renderTexture_0.height >> 1, 0, renderTexture_0.format);
			renderTexture_0.filterMode = FilterMode.Bilinear;
			if (highResolution)
			{
				float_1 *= 2f;
			}
			WriteCoc(renderTexture_0, renderTexture, renderTexture2, true);
			int pass = ((blurSampleCount == BlurSampleCount.High || blurSampleCount == BlurSampleCount.Medium) ? 17 : 11);
			if (highResolution)
			{
				material_0.SetVector("_Offsets", new Vector4(0f, float_1, 0.025f, float_1));
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, pass);
			}
			else
			{
				material_0.SetVector("_Offsets", new Vector4(0f, float_1, 0.1f, float_1));
				Graphics.Blit(renderTexture_0, renderTexture, material_0, 6);
				Graphics.Blit(renderTexture, renderTexture2, material_0, pass);
				material_0.SetTexture("_LowRez", renderTexture2);
				material_0.SetTexture("_FgOverlap", null);
				material_0.SetVector("_Offsets", Vector4.one * (1f * (float)renderTexture_0.width / (1f * (float)renderTexture2.width)) * float_1);
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (blurSampleCount != BlurSampleCount.High) ? 12 : 18);
			}
		}
		if ((bool)renderTexture)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		if ((bool)renderTexture2)
		{
			RenderTexture.ReleaseTemporary(renderTexture2);
		}
	}

	public override void Main()
	{
	}
}
