using UnityEngine;

public class ContrastStretchEffect : MonoBehaviour
{
	public float adaptationSpeed = 0.02f;

	public float limitMinimum = 0.2f;

	public float limitMaximum = 0.6f;

	private RenderTexture[] renderTexture_0 = new RenderTexture[2];

	private int int_0;

	public Shader shaderLum;

	private Material material_0;

	public Shader shaderReduce;

	private Material material_1;

	public Shader shaderAdapt;

	private Material material_2;

	public Shader shaderApply;

	private Material material_3;

	protected Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(shaderLum);
				material_0.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_0;
		}
	}

	protected Material Material_1
	{
		get
		{
			if (material_1 == null)
			{
				material_1 = new Material(shaderReduce);
				material_1.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_1;
		}
	}

	protected Material Material_2
	{
		get
		{
			if (material_2 == null)
			{
				material_2 = new Material(shaderAdapt);
				material_2.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_2;
		}
	}

	protected Material Material_3
	{
		get
		{
			if (material_3 == null)
			{
				material_3 = new Material(shaderApply);
				material_3.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_3;
		}
	}

	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
		else if (!shaderAdapt.isSupported || !shaderApply.isSupported || !shaderLum.isSupported || !shaderReduce.isSupported)
		{
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!renderTexture_0[i])
			{
				renderTexture_0[i] = new RenderTexture(1, 1, 32);
				renderTexture_0[i].hideFlags = HideFlags.HideAndDontSave;
			}
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			Object.DestroyImmediate(renderTexture_0[i]);
			renderTexture_0[i] = null;
		}
		if ((bool)material_0)
		{
			Object.DestroyImmediate(material_0);
		}
		if ((bool)material_1)
		{
			Object.DestroyImmediate(material_1);
		}
		if ((bool)material_2)
		{
			Object.DestroyImmediate(material_2);
		}
		if ((bool)material_3)
		{
			Object.DestroyImmediate(material_3);
		}
	}

	private void OnRenderImage(RenderTexture renderTexture_1, RenderTexture renderTexture_2)
	{
		RenderTexture renderTexture = RenderTexture.GetTemporary(renderTexture_1.width / 1, renderTexture_1.height / 1);
		Graphics.Blit(renderTexture_1, renderTexture, Material_0);
		while (renderTexture.width > 1 || renderTexture.height > 1)
		{
			int num = renderTexture.width / 2;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = renderTexture.height / 2;
			if (num2 < 1)
			{
				num2 = 1;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			Graphics.Blit(renderTexture, temporary, Material_1);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		CalculateAdaptation(renderTexture);
		Material_3.SetTexture("_AdaptTex", renderTexture_0[int_0]);
		Graphics.Blit(renderTexture_1, renderTexture_2, Material_3);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	private void CalculateAdaptation(Texture texture_0)
	{
		int num = int_0;
		int_0 = (int_0 + 1) % 2;
		float value = 1f - Mathf.Pow(1f - adaptationSpeed, 30f * Time.deltaTime);
		value = Mathf.Clamp(value, 0.01f, 1f);
		Material_2.SetTexture("_CurTex", texture_0);
		Material_2.SetVector("_AdaptParams", new Vector4(value, limitMinimum, limitMaximum, 0f));
		Graphics.Blit(renderTexture_0[num], renderTexture_0[int_0], Material_2);
	}
}
