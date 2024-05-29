using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlowEffect : MonoBehaviour
{
	public float glowIntensity = 1.5f;

	public int blurIterations = 3;

	public float blurSpread = 0.7f;

	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	public Shader compositeShader;

	private Material material_0;

	public Shader blurShader;

	private Material material_1;

	public Shader downsampleShader;

	private Material material_2;

	protected Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(compositeShader);
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
				material_1 = new Material(blurShader);
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
				material_2 = new Material(downsampleShader);
				material_2.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_2;
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
		if ((bool)material_2)
		{
			Object.DestroyImmediate(material_2);
		}
	}

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.enabled = false;
			return;
		}
		if (!Material_1.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!Material_0.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!Material_2.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	public void FourTapCone(RenderTexture renderTexture_0, RenderTexture renderTexture_1, int int_0)
	{
		float num = 0.5f + (float)int_0 * blurSpread;
		Graphics.BlitMultiTap(renderTexture_0, renderTexture_1, Material_1, new Vector2(num, num), new Vector2(0f - num, num), new Vector2(num, 0f - num), new Vector2(0f - num, 0f - num));
	}

	private void DownSample4x(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Material_2.color = new Color(glowTint.r, glowTint.g, glowTint.b, glowTint.a / 4f);
		Graphics.Blit(renderTexture_0, renderTexture_1, Material_2);
	}

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		glowIntensity = Mathf.Clamp(glowIntensity, 0f, 10f);
		blurIterations = Mathf.Clamp(blurIterations, 0, 30);
		blurSpread = Mathf.Clamp(blurSpread, 0.5f, 1f);
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
		DownSample4x(renderTexture_0, temporary);
		float num = Mathf.Clamp01((glowIntensity - 1f) / 4f);
		Material_1.color = new Color(1f, 1f, 1f, 0.25f + num);
		bool flag = true;
		for (int i = 0; i < blurIterations; i++)
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
		Graphics.Blit(renderTexture_0, renderTexture_1);
		if (flag)
		{
			BlitGlow(temporary, renderTexture_1);
		}
		else
		{
			BlitGlow(temporary2, renderTexture_1);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	public void BlitGlow(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Material_0.color = new Color(1f, 1f, 1f, Mathf.Clamp01(glowIntensity));
		Graphics.Blit(renderTexture_0, renderTexture_1, Material_0);
	}
}
