using System.Collections;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
	public float Time = 30f;

	private void OnEnable()
	{
		StartCoroutine(Remove());
	}

	private IEnumerator Remove()
	{
		yield return new WaitForSeconds(Time);
		ParticleEmitter component = GetComponent<ParticleEmitter>();
		if (component != null)
		{
			component.emit = false;
		}
	}
}
