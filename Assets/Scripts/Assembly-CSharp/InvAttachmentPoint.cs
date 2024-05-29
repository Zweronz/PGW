using UnityEngine;

public class InvAttachmentPoint : MonoBehaviour
{
	public InvBaseItem.Slot slot;

	private GameObject gameObject_0;

	private GameObject gameObject_1;

	public GameObject Attach(GameObject gameObject_2)
	{
		if (gameObject_0 != gameObject_2)
		{
			gameObject_0 = gameObject_2;
			if (gameObject_1 != null)
			{
				Object.Destroy(gameObject_1);
			}
			if (gameObject_0 != null)
			{
				Transform transform = base.transform;
				gameObject_1 = Object.Instantiate(gameObject_0, transform.position, transform.rotation) as GameObject;
				Transform transform2 = gameObject_1.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return gameObject_1;
	}
}
