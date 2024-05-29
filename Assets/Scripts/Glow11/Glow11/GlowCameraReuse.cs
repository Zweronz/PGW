using UnityEngine;

namespace Glow11
{
	[ExecuteInEditMode]
	[AddComponentMenu("")]
	internal class GlowCameraReuse : BaseGlowCamera
	{
		private GlowCameraReuseHelper helper;

		public RenderTexture screenRt;

		private RenderTexture tmpRt;

		private void ActivateHelper()
		{
			if ((bool)parentCamera && !helper)
			{
				helper = parentCamera.gameObject.AddComponent<GlowCameraReuseHelper>();
				helper.hideFlags = HideFlags.HideInInspector;
				helper.glowCam = this;
			}
		}

		private void OnDisable()
		{
			if (!Application.isEditor)
			{
				helper.glowCam = null;
				Object.Destroy(helper);
			}
			else
			{
				helper.glowCam = null;
			}
		}

		private void OnEnable()
		{
			ActivateHelper();
		}

		internal override void Init()
		{
			ActivateHelper();
		}

		private void OnPreCull()
		{
			ActivateHelper();
			base.GetComponent<Camera>().CopyFrom(parentCamera);
			base.GetComponent<Camera>().backgroundColor = Color.black;
			base.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
			base.GetComponent<Camera>().SetReplacementShader(base.glowOnly, "RenderEffect");
			base.GetComponent<Camera>().renderingPath = RenderingPath.VertexLit;
			tmpRt = RenderTexture.GetTemporary(screenRt.width, screenRt.height);
			RenderTexture.active = tmpRt;
			GL.Clear(false, true, Color.black);
			base.GetComponent<Camera>().targetTexture = tmpRt;
			base.GetComponent<Camera>().SetTargetBuffers(tmpRt.colorBuffer, screenRt.depthBuffer);
		}

		private void OnPostRender()
		{
			base.blur.BlurAndBlitBuffer(tmpRt, screenRt, settings, highPrecision);
			RenderTexture.ReleaseTemporary(tmpRt);
		}
	}
}
