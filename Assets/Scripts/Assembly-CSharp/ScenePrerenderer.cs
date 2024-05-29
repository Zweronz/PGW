using UnityEngine;

public class ScenePrerenderer : MonoBehaviour
{
	public Camera activeCamera;

	private RenderTexture renderTexture_0;

	public bool FinishPrerendering;

	private void Awake()
	{
		renderTexture_0 = new RenderTexture(32, 32, 24);
		renderTexture_0.Create();
		activeCamera.targetTexture = renderTexture_0;
		activeCamera.useOcclusionCulling = false;
	}

	private void Start()
	{
		activeCamera.Render();
		RenderTexture.active = renderTexture_0;
		activeCamera.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(base.transform.parent.parent.gameObject);
		Object.Destroy(activeCamera);
	}
}
