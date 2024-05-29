using UnityEngine;

public class WeaponAliveParticleEffect : WeaponCustomEffect
{
	private ParticleSystem particleSystem_0;

	private void Awake()
	{
		particleSystem_0 = GetComponent<ParticleSystem>();
		particleSystem_0.playOnAwake = false;
		particleSystem_0.Stop();
	}

	public override void SetActiveEffect(bool bool_0)
	{
		if (bool_0)
		{
			particleSystem_0.Play();
		}
	}
}
