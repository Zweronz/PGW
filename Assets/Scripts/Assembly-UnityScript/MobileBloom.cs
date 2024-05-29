using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class MobileBloom : MonoBehaviour
{
	public float intensity;

	public float threshhold;

	public float blurWidth;

	public bool extraBlurry;

	public Material bloomMaterial;

	private bool bool_0;

	private RenderTexture renderTexture_0;

	private RenderTexture renderTexture_1;

	public MobileBloom()
	{
		intensity = 0.7f;
		threshhold = 0.75f;
		blurWidth = 1f;
	}

	public virtual bool Supported()
	{
		int result;
		if (bool_0)
		{
			result = 1;
			goto IL_003e;
		}
		bool num = SystemInfo.supportsImageEffects;
		if (num)
		{
			num = SystemInfo.supportsRenderTextures;
			if (num)
			{
				goto IL_0022;
			}
		}
		else if (num)
		{
			goto IL_0022;
		}
		goto IL_0033;
		IL_003e:
		return (byte)result != 0;
		IL_0022:
		num = bloomMaterial.shader.isSupported;
		goto IL_0033;
		IL_0033:
		bool_0 = num;
		result = (bool_0 ? 1 : 0);
		goto IL_003e;
	}

	public virtual void CreateBuffers()
	{
		if (!renderTexture_0)
		{
			renderTexture_0 = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
			renderTexture_0.hideFlags = HideFlags.DontSave;
		}
		if (!renderTexture_1)
		{
			renderTexture_1 = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
			renderTexture_1.hideFlags = HideFlags.DontSave;
		}
	}

	public virtual void OnDisable()
	{
		if ((bool)renderTexture_0)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = null;
		}
		if ((bool)renderTexture_1)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture_1);
			renderTexture_1 = null;
		}
	}

	public virtual bool EarlyOutIfNotSupported(RenderTexture renderTexture_2, RenderTexture renderTexture_3)
	{
		int result;
		if (!Supported())
		{
			enabled = false;
			Graphics.Blit(renderTexture_2, renderTexture_3);
			result = 1;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_2, RenderTexture renderTexture_3)
	{
		CreateBuffers();
		if (!EarlyOutIfNotSupported(renderTexture_2, renderTexture_3))
		{
			bloomMaterial.SetVector("_Parameter", new Vector4(0f, 0f, threshhold, intensity / (1f - threshhold)));
			float num = 1f / ((float)renderTexture_2.width * 1f);
			float num2 = 1f / ((float)renderTexture_2.height * 1f);
			bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, 1.5f * num2, -1.5f * num, 1.5f * num2));
			bloomMaterial.SetVector("_OffsetsB", new Vector4(-1.5f * num, -1.5f * num2, 1.5f * num, -1.5f * num2));
			Graphics.Blit(renderTexture_2, renderTexture_1, bloomMaterial, 1);
			num *= 4f * blurWidth;
			num2 *= 4f * blurWidth;
			bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, 0f, -1.5f * num, 0f));
			bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * num, 0f, -0.5f * num, 0f));
			Graphics.Blit(renderTexture_1, renderTexture_0, bloomMaterial, 2);
			bloomMaterial.SetVector("_OffsetsA", new Vector4(0f, 1.5f * num2, 0f, -1.5f * num2));
			bloomMaterial.SetVector("_OffsetsB", new Vector4(0f, 0.5f * num2, 0f, -0.5f * num2));
			Graphics.Blit(renderTexture_0, renderTexture_1, bloomMaterial, 2);
			if (extraBlurry)
			{
				bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, 0f, -1.5f * num, 0f));
				bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * num, 0f, -0.5f * num, 0f));
				Graphics.Blit(renderTexture_1, renderTexture_0, bloomMaterial, 2);
				bloomMaterial.SetVector("_OffsetsA", new Vector4(0f, 1.5f * num2, 0f, -1.5f * num2));
				bloomMaterial.SetVector("_OffsetsB", new Vector4(0f, 0.5f * num2, 0f, -0.5f * num2));
				Graphics.Blit(renderTexture_0, renderTexture_1, bloomMaterial, 2);
			}
			bloomMaterial.SetTexture("_Bloom", renderTexture_1);
			Graphics.Blit(renderTexture_2, renderTexture_3, bloomMaterial, 0);
		}
	}

	public virtual void Main()
	{
	}
}
