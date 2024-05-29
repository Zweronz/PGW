using UnityEngine;

public class DeadExplosionController : MonoBehaviour
{
	public SkinnedMeshRenderer mySkinRenderer;

	public float timeAfteAnim = 0.5f;

	public float startTimerAnim = 0.5f;

	private float float_0 = -1f;

	public void StartAnim()
	{
		float_0 = startTimerAnim + timeAfteAnim;
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
