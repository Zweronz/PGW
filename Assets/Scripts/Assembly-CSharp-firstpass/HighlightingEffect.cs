using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HighlightingEffect : MonoBehaviour
{
	public int stencilZBufferDepth;

	public int _downsampleFactor = 4;

	public int iterations = 2;

	public float blurMinSpread = 0.65f;

	public float blurSpread = 0.25f;

	public float _blurIntensity = 0.3f;

	private int int_0 = 1 << HighlightableObject.int_0;

	private GameObject gameObject_0;

	private GameObject gameObject_1;

	private Camera camera_0;

	private RenderTexture renderTexture_0;

	private Camera camera_1;

	private static Shader shader_0;

	private static Shader shader_1;

	private static Material material_0;

	private static Material material_1;

	private static HighlightingEventHandler highlightingEventHandler_0;

	private static Shader Shader_0
	{
		get
		{
			if (shader_0 == null)
			{
				shader_0 = Shader.Find("Hidden/Highlighted/Blur");
			}
			return shader_0;
		}
	}

	private static Shader Shader_1
	{
		get
		{
			if (shader_1 == null)
			{
				shader_1 = Shader.Find("Hidden/Highlighted/Composite");
			}
			return shader_1;
		}
	}

	private static Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(Shader_0);
				material_0.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_0;
		}
	}

	private static Material Material_1
	{
		get
		{
			if (material_1 == null)
			{
				material_1 = new Material(Shader_1);
				material_1.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_1;
		}
	}

	public static event HighlightingEventHandler highlightingEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			highlightingEventHandler_0 = (HighlightingEventHandler)Delegate.Combine(highlightingEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			highlightingEventHandler_0 = (HighlightingEventHandler)Delegate.Remove(highlightingEventHandler_0, value);
		}
	}

	private void Awake()
	{
		gameObject_0 = base.gameObject;
		camera_1 = GetComponent<Camera>();
	}

	private void OnDisable()
	{
		if (gameObject_1 != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject_1);
		}
		if ((bool)shader_0)
		{
			shader_0 = null;
		}
		if ((bool)shader_1)
		{
			shader_1 = null;
		}
		if ((bool)material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
		}
		if ((bool)material_1)
		{
			UnityEngine.Object.DestroyImmediate(material_1);
		}
		if (renderTexture_0 != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture_0);
			renderTexture_0 = null;
		}
	}

	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarning("HighlightingSystem : Image effects is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!SystemInfo.supportsRenderTextures)
		{
			Debug.LogWarning("HighlightingSystem : RenderTextures is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
		{
			Debug.LogWarning("HighlightingSystem : RenderTextureFormat.ARGB32 is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader.Find("Hidden/Highlighted/StencilOpaque").isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingStencilOpaque shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader.Find("Hidden/Highlighted/StencilTransparent").isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingStencilTransparent shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader.Find("Hidden/Highlighted/StencilOpaqueZ").isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingStencilOpaqueZ shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader.Find("Hidden/Highlighted/StencilTransparentZ").isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingStencilTransparentZ shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader_0.isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingBlur shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else if (!Shader_1.isSupported)
		{
			Debug.LogWarning("HighlightingSystem : HighlightingComposite shader is not supported on this platform! Disabling.");
			base.enabled = false;
		}
		else
		{
			Material_0.SetFloat("_Intensity", _blurIntensity);
		}
	}

	public void FourTapCone(RenderTexture renderTexture_1, RenderTexture renderTexture_2, int int_1)
	{
		float value = blurMinSpread + (float)int_1 * blurSpread;
		Material_0.SetFloat("_OffsetScale", value);
		Graphics.Blit(renderTexture_1, renderTexture_2, Material_0);
	}

	private void DownSample4x(RenderTexture renderTexture_1, RenderTexture renderTexture_2)
	{
		float value = 1f;
		Material_0.SetFloat("_OffsetScale", value);
		Graphics.Blit(renderTexture_1, renderTexture_2, Material_0);
	}

	private void OnPreRender()
	{
		if (!base.enabled || !gameObject_0.activeInHierarchy)
		{
			return;
		}
		if (renderTexture_0 != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture_0);
			renderTexture_0 = null;
		}
		if (highlightingEventHandler_0 != null)
		{
			highlightingEventHandler_0(true, stencilZBufferDepth > 0);
			renderTexture_0 = RenderTexture.GetTemporary((int)base.GetComponent<Camera>().pixelWidth, (int)base.GetComponent<Camera>().pixelHeight, stencilZBufferDepth, RenderTextureFormat.ARGB32);
			if (!gameObject_1)
			{
				gameObject_1 = new GameObject("HighlightingCamera", typeof(Camera));
				gameObject_1.GetComponent<Camera>().enabled = false;
				gameObject_1.hideFlags = HideFlags.HideAndDontSave;
			}
			if (!camera_0)
			{
				camera_0 = gameObject_1.GetComponent<Camera>();
			}
			camera_0.CopyFrom(camera_1);
			camera_0.cullingMask = int_0;
			camera_0.rect = new Rect(0f, 0f, 1f, 1f);
			camera_0.renderingPath = RenderingPath.VertexLit;
			camera_0.hdr = false;
			camera_0.useOcclusionCulling = false;
			camera_0.backgroundColor = new Color(0f, 0f, 0f, 0f);
			camera_0.clearFlags = CameraClearFlags.Color;
			camera_0.targetTexture = renderTexture_0;
			camera_0.Render();
			if (highlightingEventHandler_0 != null)
			{
				highlightingEventHandler_0(false, false);
			}
		}
	}

	private void OnRenderImage(RenderTexture renderTexture_1, RenderTexture renderTexture_2)
	{
		if (renderTexture_0 == null)
		{
			Graphics.Blit(renderTexture_1, renderTexture_2);
			return;
		}
		int width = renderTexture_1.width / _downsampleFactor;
		int height = renderTexture_1.height / _downsampleFactor;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, stencilZBufferDepth, RenderTextureFormat.ARGB32);
		RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, stencilZBufferDepth, RenderTextureFormat.ARGB32);
		DownSample4x(renderTexture_0, temporary);
		bool flag = true;
		for (int i = 0; i < iterations; i++)
		{
			if (flag)
			{
				FourTapCone(temporary, temporary2, i);
			}
			else
			{
				FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		Material_1.SetTexture("_StencilTex", renderTexture_0);
		Material_1.SetTexture("_BlurTex", (!flag) ? temporary2 : temporary);
		Graphics.Blit(renderTexture_1, renderTexture_2, Material_1);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		if (renderTexture_0 != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture_0);
			renderTexture_0 = null;
		}
	}
}
