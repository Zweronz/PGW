using UnityEngine;

public class CapePrerenderer : MonoBehaviour
{
	public Camera activeCamera;

	private RenderTexture renderTexture_0;

	public bool FinishPrerendering;

	private GameObject gameObject_0;

	private void Awake()
	{
		renderTexture_0 = new RenderTexture(512, 512, 24);
		renderTexture_0.Create();
		activeCamera.targetTexture = renderTexture_0;
		activeCamera.useOcclusionCulling = false;
	}

	public Texture Render_()
	{
		activeCamera.Render();
		RenderTexture.active = renderTexture_0;
		activeCamera.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(base.transform.parent.gameObject);
		return renderTexture_0;
	}
}
