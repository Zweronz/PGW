using UnityEngine;

public class BlurEffect : MonoBehaviour
{
	public int iterations = 3;

	public float blurSpread = 0.6f;

	public Shader blurShader;

	private static Material material_0;

	protected Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(blurShader);
				material_0.hideFlags = HideFlags.DontSave;
			}
			return material_0;
		}
	}

	protected void OnDisable()
	{
		if ((bool)material_0)
		{
			Object.DestroyImmediate(material_0);
		}
	}

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
		else if (!blurShader || !Material_0.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	public void FourTapCone(RenderTexture renderTexture_0, RenderTexture renderTexture_1, int int_0)
	{
		float num = 0.5f + (float)int_0 * blurSpread;
		Graphics.BlitMultiTap(renderTexture_0, renderTexture_1, Material_0, new Vector2(0f - num, 0f - num), new Vector2(0f - num, num), new Vector2(num, num), new Vector2(num, 0f - num));
	}

	private void DownSample4x(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		float num = 1f;
		Graphics.BlitMultiTap(renderTexture_0, renderTexture_1, Material_0, new Vector2(-1f, -1f), new Vector2(-1f, num), new Vector2(num, num), new Vector2(num, -1f));
	}

	private void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(renderTexture_0.width / 4, renderTexture_0.height / 4, 0);
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
		if (flag)
		{
			Graphics.Blit(temporary, renderTexture_1);
		}
		else
		{
			Graphics.Blit(temporary2, renderTexture_1);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}
}
