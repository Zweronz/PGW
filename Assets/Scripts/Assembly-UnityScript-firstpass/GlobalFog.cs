using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class GlobalFog : PostEffectsBase
{
	[Serializable]
	public enum FogMode
	{
		AbsoluteYAndDistance = 0,
		AbsoluteY = 1,
		Distance = 2,
		RelativeYAndDistance = 3
	}

	public FogMode fogMode;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	public float startDistance;

	public float globalDensity;

	public float heightScale;

	public float height;

	public Color globalFogColor;

	public Shader fogShader;

	private Material material_0;

	public GlobalFog()
	{
		fogMode = FogMode.AbsoluteYAndDistance;
		float_0 = 0.5f;
		float_1 = 50f;
		float_2 = 60f;
		float_3 = 1.333333f;
		startDistance = 200f;
		globalDensity = 1f;
		heightScale = 100f;
		globalFogColor = Color.grey;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(fogShader, material_0);
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
		float_0 = GetComponent<Camera>().nearClipPlane;
		float_1 = GetComponent<Camera>().farClipPlane;
		float_2 = GetComponent<Camera>().fieldOfView;
		float_3 = GetComponent<Camera>().aspect;
		Matrix4x4 identity = Matrix4x4.identity;
		Vector4 vector = default(Vector4);
		Vector3 vector2 = default(Vector3);
		float num = float_2 * 0.5f;
		Vector3 vector3 = GetComponent<Camera>().transform.right * float_0 * Mathf.Tan(num * ((float)Math.PI / 180f)) * float_3;
		Vector3 vector4 = GetComponent<Camera>().transform.up * float_0 * Mathf.Tan(num * ((float)Math.PI / 180f));
		Vector3 vector5 = GetComponent<Camera>().transform.forward * float_0 - vector3 + vector4;
		float num2 = vector5.magnitude * float_1 / float_0;
		vector5.Normalize();
		vector5 *= num2;
		Vector3 vector6 = GetComponent<Camera>().transform.forward * float_0 + vector3 + vector4;
		vector6.Normalize();
		vector6 *= num2;
		Vector3 vector7 = GetComponent<Camera>().transform.forward * float_0 + vector3 - vector4;
		vector7.Normalize();
		vector7 *= num2;
		Vector3 vector8 = GetComponent<Camera>().transform.forward * float_0 - vector3 - vector4;
		vector8.Normalize();
		vector8 *= num2;
		identity.SetRow(0, vector5);
		identity.SetRow(1, vector6);
		identity.SetRow(2, vector7);
		identity.SetRow(3, vector8);
		material_0.SetMatrix("_FrustumCornersWS", identity);
		material_0.SetVector("_CameraWS", GetComponent<Camera>().transform.position);
		material_0.SetVector("_StartDistance", new Vector4(1f / startDistance, num2 - startDistance));
		material_0.SetVector("_Y", new Vector4(height, 1f / heightScale));
		material_0.SetFloat("_GlobalDensity", globalDensity * 0.01f);
		material_0.SetColor("_FogColor", globalFogColor);
		CustomGraphicsBlit(renderTexture_0, renderTexture_1, material_0, (int)fogMode);
	}

	public static void CustomGraphicsBlit(RenderTexture renderTexture_0, RenderTexture renderTexture_1, Material material_1, int int_0)
	{
		RenderTexture.active = renderTexture_1;
		material_1.SetTexture("_MainTex", renderTexture_0);
		GL.PushMatrix();
		GL.LoadOrtho();
		material_1.SetPass(int_0);
		GL.Begin(7);
		GL.MultiTexCoord2(0, 0f, 0f);
		GL.Vertex3(0f, 0f, 3f);
		GL.MultiTexCoord2(0, 1f, 0f);
		GL.Vertex3(1f, 0f, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, 0f, 1f);
		GL.Vertex3(0f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	public override void Main()
	{
	}
}
