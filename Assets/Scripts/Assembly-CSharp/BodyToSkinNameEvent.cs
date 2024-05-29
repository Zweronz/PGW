using UnityEngine;

public class BodyToSkinNameEvent : MonoBehaviour
{
	public SkinName skinName;

	private void OnCollisionEnter(Collision collision_0)
	{
		if (skinName != null)
		{
			skinName.OnCollisionEnter(collision_0);
		}
	}

	private void OnTriggerEnter(Collider collider_0)
	{
		if (skinName != null)
		{
			skinName.OnTriggerEnter(collider_0);
		}
	}
}
