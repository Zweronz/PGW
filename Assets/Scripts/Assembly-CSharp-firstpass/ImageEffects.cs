using System;
using UnityEngine;

public class ImageEffects
{
	public static void RenderDistortion(Material material_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1, float float_0, Vector2 vector2_0, Vector2 vector2_1)
	{
		if (renderTexture_0.texelSize.y < 0f)
		{
			vector2_0.y = 1f - vector2_0.y;
			float_0 = 0f - float_0;
		}
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, float_0), Vector3.one);
		material_0.SetMatrix("_RotationMatrix", matrix);
		material_0.SetVector("_CenterRadius", new Vector4(vector2_0.x, vector2_0.y, vector2_1.x, vector2_1.y));
		material_0.SetFloat("_Angle", float_0 * ((float)Math.PI / 180f));
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
	}

	[Obsolete("Use Graphics.Blit(source,dest) instead")]
	public static void Blit(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Graphics.Blit(renderTexture_0, renderTexture_1);
	}

	[Obsolete("Use Graphics.Blit(source, destination, material) instead")]
	public static void BlitWithMaterial(Material material_0, RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		Graphics.Blit(renderTexture_0, renderTexture_1, material_0);
	}
}
