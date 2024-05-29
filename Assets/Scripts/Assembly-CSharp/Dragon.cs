using System.Collections;
using UnityEngine;

public class Dragon : MonoBehaviour
{
	public GameObject child;

	public GameObject wingsFirst;

	public GameObject wingsSecond;

	private void Start()
	{
		if (!(child == null) && !(wingsFirst == null) && !(wingsSecond == null))
		{
			StartCoroutine(dragonfly());
		}
	}

	private IEnumerator dragonfly()
	{
		while (true)
		{
			yield return new WaitForSeconds(6.6666665f);
			wingsFirst.SetActive(true);
			yield return new WaitForSeconds(3.2333333f);
			wingsFirst.SetActive(false);
			yield return new WaitForSeconds(6.6666665f);
			child.SetActive(true);
			yield return new WaitForSeconds(5f);
			child.SetActive(false);
			wingsSecond.SetActive(true);
			yield return new WaitForSeconds(4f);
			wingsSecond.SetActive(false);
			yield return new WaitForSeconds(23.71f);
		}
	}
}
