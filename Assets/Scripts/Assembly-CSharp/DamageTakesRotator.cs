using UnityEngine;

public class DamageTakesRotator : MonoBehaviour
{
	private Transform transform_0;

	private GameObject gameObject_0;

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void Update()
	{
		if (gameObject_0 == null)
		{
			if (Defs.bool_2)
			{
				gameObject_0 = WeaponManager.weaponManager_0.myPlayer;
			}
			else
			{
				gameObject_0 = GameObject.FindGameObjectWithTag("Player");
			}
		}
		if (!(gameObject_0 == null))
		{
			transform_0.localRotation = Quaternion.Euler(new Vector3(0f, 0f, gameObject_0.transform.localRotation.eulerAngles.y));
		}
	}
}
