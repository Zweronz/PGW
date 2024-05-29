using System;
using UnityEngine;

[Serializable]
public class ColorCorrectionLut : PostEffectsBase
{
	public Shader shader;

	private Material material_0;

	public Texture3D converted3DLut;

	public string basedOnTempTex;

	public ColorCorrectionLut()
	{
		basedOnTempTex = string.Empty;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material_0 = CheckShaderAndCreateMaterial(shader, material_0);
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnDisable()
	{
		if ((bool)material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
			material_0 = null;
		}
	}

	public virtual void OnDestroy()
	{
		if ((bool)converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(converted3DLut);
		}
		converted3DLut = null;
	}

	public virtual void SetIdentityLut()
	{
		int num = 16;
		Color[] array = new Color[4096];
		float num2 = 1f / 15f;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if ((bool)converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(converted3DLut);
		}
		converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		converted3DLut.SetPixels(array);
		converted3DLut.Apply();
		basedOnTempTex = string.Empty;
	}

	public virtual bool ValidDimensions(Texture2D texture2D_0)
	{
		int result;
		if (!texture2D_0)
		{
			result = 0;
		}
		else
		{
			int height = texture2D_0.height;
			result = ((height == Mathf.FloorToInt(Mathf.Sqrt(texture2D_0.width))) ? 1 : 0);
		}
		return (byte)result != 0;
	}

	public virtual void Convert(Texture2D texture2D_0, string string_0)
	{
		if ((bool)texture2D_0)
		{
			int num = texture2D_0.width * texture2D_0.height;
			num = texture2D_0.height;
			if (!ValidDimensions(texture2D_0))
			{
				Debug.LogWarning("The given 2D texture " + texture2D_0.name + " cannot be used as a 3D LUT.");
				basedOnTempTex = string.Empty;
				return;
			}
			Color[] pixels = texture2D_0.GetPixels();
			Color[] array = new Color[pixels.Length];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						int num2 = num - j - 1;
						array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
					}
				}
			}
			if ((bool)converted3DLut)
			{
				UnityEngine.Object.DestroyImmediate(converted3DLut);
			}
			converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
			converted3DLut.SetPixels(array);
			converted3DLut.Apply();
			basedOnTempTex = string_0;
		}
		else
		{
			Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		if (converted3DLut == null)
		{
			SetIdentityLut();
		}
		int width = converted3DLut.width;
		converted3DLut.wrapMode = TextureWrapMode.Clamp;
		material_0.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
		material_0.SetFloat("_Offset", 1f / (2f * (float)width));
		material_0.SetTexture("_ClutTex", converted3DLut);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
	}

	public override void Main()
	{
	}
}
