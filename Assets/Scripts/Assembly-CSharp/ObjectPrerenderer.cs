using UnityEngine;

public class ObjectPrerenderer : MonoBehaviour
{
	public Camera activeCamera;

	private RenderTexture renderTexture_0;

	public bool FinishPrerendering;

	private GameObject gameObject_0;

	private void Awake()
	{
		renderTexture_0 = new RenderTexture(32, 32, 24);
		renderTexture_0.Create();
		activeCamera.targetTexture = renderTexture_0;
		activeCamera.useOcclusionCulling = false;
	}

	public void Render_()
	{
		activeCamera.Render();
		RenderTexture.active = renderTexture_0;
		activeCamera.targetTexture = null;
		RenderTexture.active = null;
	}
}
