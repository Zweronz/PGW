using UnityEngine;

internal sealed class synchCamAndPlayer : MonoBehaviour
{
	private bool bool_0;

	private PhotonView photonView_0;

	public Transform gameObjectPlayerTrasform;

	private bool bool_1;

	private bool bool_2;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
		bool_1 = Defs.bool_2;
		bool_2 = Defs.bool_3;
		photonView_0 = base.transform.parent.GetComponent<PhotonView>();
		if (bool_1)
		{
			if (!bool_2)
			{
				bool_0 = base.transform.parent.GetComponent<NetworkView>().isMine;
			}
			else
			{
				bool_0 = photonView_0.Boolean_1;
			}
		}
		if (bool_1 && !bool_0)
		{
			SendMessage("SetActiveFalse");
		}
		else
		{
			base.enabled = false;
		}
	}

	private void invokeStart()
	{
	}

	public void setSynh(bool bool_3)
	{
	}

	private void Update()
	{
		transform_0.rotation = gameObjectPlayerTrasform.rotation;
	}
}
