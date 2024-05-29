using System.Reflection;
using UnityEngine;

internal sealed class RemoveExplosion : MonoBehaviour
{
	private void Start()
	{
		float num;
		float duration;
		if (!(base.GetComponent<ParticleSystem>() != null))
		{
			num = 0.1f;
		}
		else
			duration = base.GetComponent<ParticleSystem>().duration;
		if ((bool)base.GetComponent<AudioSource>() && base.GetComponent<AudioSource>().enabled && Defs.Boolean_0)
		{
			base.GetComponent<AudioSource>().Play();
		}
		Invoke("Remove", 7f);
	}

	[Obfuscation(Exclude = true)]
	private void Remove()
	{
		Object.Destroy(base.gameObject);
	}
}
