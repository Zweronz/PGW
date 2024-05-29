using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SSAOEffect : MonoBehaviour
{
	public enum SSAOSamples
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

	public float m_Radius = 0.4f;

	public SSAOSamples m_SampleCount = SSAOSamples.Medium;

	public float m_OcclusionIntensity = 1.5f;

	public int m_Blur = 2;

	public int m_Downsampling = 2;

	public float m_OcclusionAttenuation = 1f;

	public float m_MinZ = 0.01f;

	public Shader m_SSAOShader;

	private Material material_0;

	public Texture2D m_RandomTexture;

	private bool bool_0;

	private static Material CreateMaterial(Shader shader_0)
	{
		if (!shader_0)
		{
			return null;
		}
		Material material = new Material(shader_0);
		material.hideFlags = HideFlags.HideAndDontSave;
		return material;
	}

	private static void DestroyMaterial(Material material_1)
	{
		if ((bool)material_1)
		{
			UnityEngine.Object.DestroyImmediate(material_1);
			material_1 = null;
		}
	}

	private void OnDisable()
	{
		DestroyMaterial(material_0);
	}

	private void Start()
	{
		if (SystemInfo.supportsImageEffects && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			CreateMaterials();
			if ((bool)material_0 && material_0.passCount == 5)
			{
				bool_0 = true;
				return;
			}
			bool_0 = false;
			base.enabled = false;
		}
		else
		{
			bool_0 = false;
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	private void CreateMaterials()
	{
		if (!material_0 && m_SSAOShader.isSupported)
		{
			material_0 = CreateMaterial(m_SSAOShader);
			material_0.SetTexture("_RandomTexture", m_RandomTexture);
		}
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (bool_0 && m_SSAOShader.isSupported)
		{
			CreateMaterials();
			m_Downsampling = Mathf.Clamp(m_Downsampling, 1, 6);
			m_Radius = Mathf.Clamp(m_Radius, 0.05f, 1f);
			m_MinZ = Mathf.Clamp(m_MinZ, 1E-05f, 0.5f);
			m_OcclusionIntensity = Mathf.Clamp(m_OcclusionIntensity, 0.5f, 4f);
			m_OcclusionAttenuation = Mathf.Clamp(m_OcclusionAttenuation, 0.2f, 2f);
			m_Blur = Mathf.Clamp(m_Blur, 0, 4);
			RenderTexture renderTexture = RenderTexture.GetTemporary(renderTexture_0.width / m_Downsampling, renderTexture_0.height / m_Downsampling, 0);
			float fieldOfView = base.GetComponent<Camera>().fieldOfView;
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			float num = Mathf.Tan(fieldOfView * ((float)Math.PI / 180f) * 0.5f) * farClipPlane;
			float x = num * base.GetComponent<Camera>().aspect;
			material_0.SetVector("_FarCorner", new Vector3(x, num, farClipPlane));
			int num2;
			int num3;
			if ((bool)m_RandomTexture)
			{
				num2 = m_RandomTexture.width;
				num3 = m_RandomTexture.height;
			}
			else
			{
				num2 = 1;
				num3 = 1;
			}
			material_0.SetVector("_NoiseScale", new Vector3((float)renderTexture.width / (float)num2, (float)renderTexture.height / (float)num3, 0f));
			material_0.SetVector("_Params", new Vector4(m_Radius, m_MinZ, 1f / m_OcclusionAttenuation, m_OcclusionIntensity));
			bool flag;
			Graphics.Blit((!(flag = m_Blur > 0)) ? renderTexture_0 : null, renderTexture, material_0, (int)m_SampleCount);
			if (flag)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0);
				material_0.SetVector("_TexelOffsetScale", new Vector4((float)m_Blur / (float)renderTexture_0.width, 0f, 0f, 0f));
				material_0.SetTexture("_SSAO", renderTexture);
				Graphics.Blit(null, temporary, material_0, 3);
				RenderTexture.ReleaseTemporary(renderTexture);
				RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width, renderTexture_0.height, 0);
				material_0.SetVector("_TexelOffsetScale", new Vector4(0f, (float)m_Blur / (float)renderTexture_0.height, 0f, 0f));
				material_0.SetTexture("_SSAO", temporary);
				Graphics.Blit(renderTexture_0, temporary2, material_0, 3);
				RenderTexture.ReleaseTemporary(temporary);
				renderTexture = temporary2;
			}
			material_0.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 4);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		else
		{
			base.enabled = false;
		}
	}
}
