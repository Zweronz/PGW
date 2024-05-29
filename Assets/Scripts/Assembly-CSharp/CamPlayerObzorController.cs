using UnityEngine;

internal sealed class CamPlayerObzorController : MonoBehaviour
{
	private bool bool_0;

	public GameObject playerGameObject;

	private void Start()
	{
		if (Defs.bool_2 && Defs.bool_3 && !base.transform.parent.GetComponent<PhotonView>().Boolean_1)
		{
			bool_0 = true;
		}
		if (bool_0)
		{
			SendMessage("SetActiveFalse");
		}
		else
		{
			base.enabled = false;
		}
		playerGameObject = base.transform.parent.GetComponent<SkinName>().playerGameObject;
	}

	private void Update()
	{
		base.transform.rotation = Quaternion.Euler(new Vector3(playerGameObject.transform.rotation.x, base.transform.rotation.y, base.transform.rotation.z));
	}
}
