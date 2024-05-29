using System.Collections;
using UnityEngine;

public class DelayedDestroyController : MonoBehaviour
{
	public float destroyDelay = 2.5f;

	private void Start()
	{
		StartCoroutine(DelayedDestroy());
	}

	protected IEnumerator DelayedDestroy()
	{
		yield return new WaitForSeconds(destroyDelay);
		Object.Destroy(base.gameObject);
	}
}
