using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class PostEffectsBase : MonoBehaviour
{
	protected bool bool_0;

	protected bool bool_1;

	protected bool bool_2;

	public PostEffectsBase()
	{
		bool_0 = true;
		bool_2 = true;
	}

	public virtual Material CheckShaderAndCreateMaterial(Shader shader_0, Material material_0)
	{
		object result;
		if (!shader_0)
		{
			Debug.Log("Missing shader in " + ToString());
			enabled = false;
			result = null;
		}
		else if (shader_0.isSupported && (bool)material_0 && material_0.shader == shader_0)
		{
			result = material_0;
		}
		else if (!shader_0.isSupported)
		{
			NotSupported();
			Debug.Log("The shader " + shader_0.ToString() + " on effect " + ToString() + " is not supported on this platform!");
			result = null;
		}
		else
		{
			material_0 = new Material(shader_0);
			material_0.hideFlags = HideFlags.DontSave;
			result = ((!material_0) ? null : material_0);
		}
		return (Material)result;
	}

	public virtual Material CreateMaterial(Shader shader_0, Material material_0)
	{
		object result;
		if (!shader_0)
		{
			Debug.Log("Missing shader in " + ToString());
			result = null;
		}
		else if ((bool)material_0 && material_0.shader == shader_0 && shader_0.isSupported)
		{
			result = material_0;
		}
		else if (!shader_0.isSupported)
		{
			result = null;
		}
		else
		{
			material_0 = new Material(shader_0);
			material_0.hideFlags = HideFlags.DontSave;
			result = ((!material_0) ? null : material_0);
		}
		return (Material)result;
	}

	public virtual void OnEnable()
	{
		bool_2 = true;
	}

	public virtual bool CheckSupport()
	{
		return CheckSupport(false);
	}

	public virtual bool CheckResources()
	{
		Debug.LogWarning("CheckResources () for " + ToString() + " should be overwritten.");
		return bool_2;
	}

	public virtual void Start()
	{
		CheckResources();
	}

	public virtual bool CheckSupport(bool bool_3)
	{
		bool_2 = true;
		bool_0 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		bool num = SystemInfo.graphicsShaderLevel >= 50;
		if (num)
		{
			num = SystemInfo.supportsComputeShaders;
		}
		bool_1 = num;
		int result;
		if (SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures)
		{
			if (bool_3 && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				NotSupported();
				result = 0;
			}
			else
			{
				if (bool_3)
				{
					GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
				}
				result = 1;
			}
		}
		else
		{
			NotSupported();
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual bool CheckSupport(bool bool_3, bool bool_4)
	{
		int result;
		if (!CheckSupport(bool_3))
		{
			result = 0;
		}
		else if (bool_4 && !bool_0)
		{
			NotSupported();
			result = 0;
		}
		else
		{
			result = 1;
		}
		return (byte)result != 0;
	}

	public virtual bool Dx11Support()
	{
		return bool_1;
	}

	public virtual void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + ToString() + " has been disabled as it's not supported on the current platform.");
	}

	public virtual bool CheckShader(Shader shader_0)
	{
		Debug.Log("The shader " + shader_0.ToString() + " on effect " + ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
		int result;
		if (!shader_0.isSupported)
		{
			NotSupported();
			result = 0;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual void NotSupported()
	{
		enabled = false;
		bool_2 = false;
	}

	public virtual void DrawBorder(RenderTexture renderTexture_0, Material material_0)
	{
		float num = default(float);
		float num2 = default(float);
		float num3 = default(float);
		float num4 = default(float);
		RenderTexture.active = renderTexture_0;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material_0.passCount; i++)
		{
			material_0.SetPass(i);
			float num5 = default(float);
			float num6 = default(float);
			if (flag)
			{
				num5 = 1f;
				num6 = 0f;
			}
			else
			{
				num5 = 0f;
				num6 = 1f;
			}
			num = 0f;
			num2 = 0f + 1f / ((float)renderTexture_0.width * 1f);
			num3 = 0f;
			num4 = 1f;
			GL.Begin(7);
			GL.TexCoord2(0f, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2(0f, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = 1f - 1f / ((float)renderTexture_0.width * 1f);
			num2 = 1f;
			num3 = 0f;
			num4 = 1f;
			GL.TexCoord2(0f, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2(0f, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = 0f;
			num2 = 1f;
			num3 = 0f;
			num4 = 0f + 1f / ((float)renderTexture_0.height * 1f);
			GL.TexCoord2(0f, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2(0f, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = 0f;
			num2 = 1f;
			num3 = 1f - 1f / ((float)renderTexture_0.height * 1f);
			num4 = 1f;
			GL.TexCoord2(0f, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2(0f, num6);
			GL.Vertex3(num, num4, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	public virtual void Main()
	{
	}
}
