using UnityEngine;

public class OnClanWindowClick : MonoBehaviour
{
	public ClanWindow obj;

	public void OnClick()
	{
		obj.OnMissClick();
	}
}
