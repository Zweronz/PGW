using UnityEngine;

public class OnClickInstantiate : MonoBehaviour
{
	public GameObject Prefab;

	public int InstantiateType;

	private string[] string_0 = new string[2] { "Mine", "Scene" };

	public bool showGui;

	private void OnClick()
	{
		if (PhotonNetwork.PeerState_0 == PeerState.Joined)
		{
			switch (InstantiateType)
			{
			case 1:
				PhotonNetwork.InstantiateSceneObject(Prefab.name, InputToEvent.vector3_0 + new Vector3(0f, 5f, 0f), Quaternion.identity, 0, null);
				break;
			case 0:
				PhotonNetwork.Instantiate(Prefab.name, InputToEvent.vector3_0 + new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
				break;
			}
		}
	}

	private void OnGUI()
	{
		if (showGui)
		{
			GUILayout.BeginArea(new Rect(Screen.width - 180, 0f, 180f, 50f));
			InstantiateType = GUILayout.Toolbar(InstantiateType, string_0);
			GUILayout.EndArea();
		}
	}
}
