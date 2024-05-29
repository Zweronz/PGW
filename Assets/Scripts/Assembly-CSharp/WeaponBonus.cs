using UnityEngine;

public class WeaponBonus : MonoBehaviour
{
	private void Update()
	{
		float num = 120f;
		base.transform.Rotate(base.transform.InverseTransformDirection(Vector3.up), num * Time.deltaTime);
	}
}
