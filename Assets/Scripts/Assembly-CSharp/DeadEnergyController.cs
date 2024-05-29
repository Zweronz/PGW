using UnityEngine;

public class DeadEnergyController : MonoBehaviour
{
	public SkinnedMeshRenderer mySkinRenderer;

	public ParticleSystem myParticle;

	public float timeAfteAnim;

	public float startTimerAnim = 1f;

	private float float_0 = -1f;

	public void StartAnim(Color color_0, Texture texture_0)
	{
		float_0 = startTimerAnim + timeAfteAnim;
		mySkinRenderer.material.SetColor("_BurnColor", color_0);
		mySkinRenderer.material.SetTexture("_MainTex", texture_0);
		myParticle.startColor = color_0;
	}

	private void Update()
	{
		if (float_0 > 0f)
		{
			float value = 1.25f;
			float_0 -= Time.deltaTime;
			if (float_0 < startTimerAnim)
			{
				value = -0.25f + 1.5f * float_0 / startTimerAnim;
			}
			mySkinRenderer.material.SetFloat("_Burn", value);
		}
	}
}
