using UnityEngine;

public class RotatorGun : MonoBehaviour
{
	public GameObject playerGun;

	private void Update()
	{
		if (playerGun != null)
		{
			playerGun.transform.rotation = base.transform.rotation;
		}
	}
}
