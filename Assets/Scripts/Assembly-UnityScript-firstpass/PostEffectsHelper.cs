using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class PostEffectsHelper : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Debug.Log("OnRenderImage in Helper called ...");
	}

	public static void DrawLowLevelPlaneAlignedWithCamera(float float_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1, Material material_0, Camera camera_0)
	{
		RenderTexture.active = renderTexture_1;
		material_0.SetTexture("_MainTex", renderTexture_0);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadIdentity();
		GL.LoadProjectionMatrix(camera_0.projectionMatrix);
		float f = camera_0.fieldOfView * 0.5f * ((float)Math.PI / 180f);
		float num = Mathf.Cos(f) / Mathf.Sin(f);
		float aspect = camera_0.aspect;
		float num2 = aspect / (0f - num);
		float num3 = aspect / num;
		float num4 = 1f / (0f - num);
		float num5 = 1f / num;
		float num6 = 1f;
		num2 *= float_0 * num6;
		num3 *= float_0 * num6;
		num4 *= float_0 * num6;
		num5 *= float_0 * num6;
		float z = 0f - float_0;
		for (int i = 0; i < material_0.passCount; i++)
		{
			material_0.SetPass(i);
			GL.Begin(7);
			float num7 = default(float);
			float num8 = default(float);
			if (flag)
			{
				num7 = 1f;
				num8 = 0f;
			}
			else
			{
				num7 = 0f;
				num8 = 1f;
			}
			GL.TexCoord2(0f, num7);
			GL.Vertex3(num2, num4, z);
			GL.TexCoord2(1f, num7);
			GL.Vertex3(num3, num4, z);
			GL.TexCoord2(1f, num8);
			GL.Vertex3(num3, num5, z);
			GL.TexCoord2(0f, num8);
			GL.Vertex3(num2, num5, z);
			GL.End();
		}
		GL.PopMatrix();
	}

	public static void DrawBorder(RenderTexture renderTexture_0, Material material_0)
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

	public static void DrawLowLevelQuad(float float_0, float float_1, float float_2, float float_3, RenderTexture renderTexture_0, RenderTexture renderTexture_1, Material material_0)
	{
		RenderTexture.active = renderTexture_1;
		material_0.SetTexture("_MainTex", renderTexture_0);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material_0.passCount; i++)
		{
			material_0.SetPass(i);
			GL.Begin(7);
			float num = default(float);
			float num2 = default(float);
			if (flag)
			{
				num = 1f;
				num2 = 0f;
			}
			else
			{
				num = 0f;
				num2 = 1f;
			}
			GL.TexCoord2(0f, num);
			GL.Vertex3(float_0, float_2, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(float_1, float_2, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(float_1, float_3, 0.1f);
			GL.TexCoord2(0f, num2);
			GL.Vertex3(float_0, float_3, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	public virtual void Main()
	{
	}
}
