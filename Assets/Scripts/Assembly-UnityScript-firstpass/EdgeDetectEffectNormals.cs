using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	public EdgeDetectMode mode;

	public float sensitivityDepth;

	public float sensitivityNormals;

	public float edgeExp;

	public float sampleDist;

	public float edgesOnly;

	public Color edgesOnlyBgColor;

	public Shader edgeDetectShader;

	private Material material_0;

	private EdgeDetectMode edgeDetectMode_0;

	public EdgeDetectEffectNormals()
	{
		mode = EdgeDetectMode.SobelDepthThin;
		sensitivityDepth = 1f;
		sensitivityNormals = 1f;
		edgeExp = 1f;
		sampleDist = 1f;
		edgesOnlyBgColor = Color.white;
		edgeDetectMode_0 = EdgeDetectMode.SobelDepthThin;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		material_0 = CheckShaderAndCreateMaterial(edgeDetectShader, material_0);
		if (mode != edgeDetectMode_0)
		{
			SetCameraFlag();
		}
		edgeDetectMode_0 = mode;
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public override void Start()
	{
		edgeDetectMode_0 = mode;
	}

	public virtual void SetCameraFlag()
	{
		if (mode > EdgeDetectMode.RobertsCrossDepthNormals)
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
		else
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
		}
	}

	public override void OnEnable()
	{
		SetCameraFlag();
	}

	[ImageEffectOpaque]
	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		Vector2 vector = new Vector2(sensitivityDepth, sensitivityNormals);
		material_0.SetVector("_Sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
		material_0.SetFloat("_BgFade", edgesOnly);
		material_0.SetFloat("_SampleDistance", sampleDist);
		material_0.SetVector("_BgColor", edgesOnlyBgColor);
		material_0.SetFloat("_Exponent", edgeExp);
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0, (int)mode);
	}

	public override void Main()
	{
	}
}
