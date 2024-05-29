using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NoiseEffect : MonoBehaviour
{
	public bool monochrome = true;

	private bool bool_0;

	public float grainIntensityMin = 0.1f;

	public float grainIntensityMax = 0.2f;

	public float grainSize = 2f;

	public float scratchIntensityMin = 0.05f;

	public float scratchIntensityMax = 0.25f;

	public float scratchFPS = 10f;

	public float scratchJitter = 0.01f;

	public Texture grainTexture;

	public Texture scratchTexture;

	public Shader shaderRGB;

	public Shader shaderYUV;

	private Material material_0;

	private Material material_1;

	private float float_0;

	private float float_1;

	private float float_2;

	protected Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(shaderRGB);
				material_0.hideFlags = HideFlags.HideAndDontSave;
			}
			if (material_1 == null && !bool_0)
			{
				material_1 = new Material(shaderYUV);
				material_1.hideFlags = HideFlags.HideAndDontSave;
			}
			return (bool_0 || monochrome) ? material_0 : material_1;
		}
	}

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
		else if (!(shaderRGB == null) && !(shaderYUV == null))
		{
			if (!shaderRGB.isSupported)
			{
				base.enabled = false;
			}
			else if (!shaderYUV.isSupported)
			{
				bool_0 = true;
			}
		}
		else
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
		}
	}

	protected void OnDisable()
	{
		if ((bool)material_0)
		{
			Object.DestroyImmediate(material_0);
		}
		if ((bool)material_1)
		{
			Object.DestroyImmediate(material_1);
		}
	}

	private void SanitizeParameters()
	{
		grainIntensityMin = Mathf.Clamp(grainIntensityMin, 0f, 5f);
		grainIntensityMax = Mathf.Clamp(grainIntensityMax, 0f, 5f);
		scratchIntensityMin = Mathf.Clamp(scratchIntensityMin, 0f, 5f);
		scratchIntensityMax = Mathf.Clamp(scratchIntensityMax, 0f, 5f);
		scratchFPS = Mathf.Clamp(scratchFPS, 1f, 30f);
		scratchJitter = Mathf.Clamp(scratchJitter, 0f, 1f);
		grainSize = Mathf.Clamp(grainSize, 0.1f, 50f);
	}

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		SanitizeParameters();
		if (float_0 <= 0f)
		{
			float_0 = Random.value * 2f / scratchFPS;
			float_1 = Random.value;
			float_2 = Random.value;
		}
		float_0 -= Time.deltaTime;
		Material material = Material_0;
		material.SetTexture("_GrainTex", grainTexture);
		material.SetTexture("_ScratchTex", scratchTexture);
		float num = 1f / grainSize;
		material.SetVector("_GrainOffsetScale", new Vector4(Random.value, Random.value, (float)Screen.width / (float)grainTexture.width * num, (float)Screen.height / (float)grainTexture.height * num));
		material.SetVector("_ScratchOffsetScale", new Vector4(float_1 + Random.value * scratchJitter, float_2 + Random.value * scratchJitter, (float)Screen.width / (float)scratchTexture.width, (float)Screen.height / (float)scratchTexture.height));
		material.SetVector("_Intensity", new Vector4(Random.Range(grainIntensityMin, grainIntensityMax), Random.Range(scratchIntensityMin, scratchIntensityMax), 0f, 0f));
		Graphics.Blit(renderTexture_0, renderTexture_1, material);
	}
}
