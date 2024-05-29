using UnityEngine;

[RequireComponent(typeof(InputToEvent))]
public class PointedAtGameObjectInfo : MonoBehaviour
{
	private void OnGUI()
	{
		if (InputToEvent.GameObject_0 != null)
		{
			PhotonView photonView = InputToEvent.GameObject_0.GetPhotonView();
			if (photonView != null)
			{
				GUI.Label(new Rect(Input.mousePosition.x + 5f, (float)Screen.height - Input.mousePosition.y - 15f, 300f, 30f), string.Format("ViewID {0} InstID {1} Lvl {2} {3}", photonView.Int32_1, photonView.int_4, photonView.Int32_0, photonView.Boolean_0 ? "scene" : ((!photonView.Boolean_1) ? ("owner: " + photonView.int_1) : "mine")));
			}
		}
	}
}
