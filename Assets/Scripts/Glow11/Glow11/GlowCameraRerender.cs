using UnityEngine;

namespace Glow11
{
	[AddComponentMenu("")]
	internal class GlowCameraRerender : BaseGlowCamera
	{
		private void OnPreCull()
		{
			base.GetComponent<Camera>().CopyFrom(parentCamera);
			base.GetComponent<Camera>().backgroundColor = Color.black;
			base.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
			base.GetComponent<Camera>().SetReplacementShader(base.glowOnly, "RenderType");
			base.GetComponent<Camera>().renderingPath = RenderingPath.VertexLit;
			base.GetComponent<Camera>().depth = parentCamera.depth + 0.1f;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.blur.BlurAndBlitBuffer(source, destination, settings, highPrecision);
		}
	}
}
