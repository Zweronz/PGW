using Rilisoft;
using UnityEngine;

public class JoinClanPressed : MonoBehaviour
{
	private void OnClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		base.transform.parent.GetComponent<Invitation>().DisableButtons();
		base.transform.parent.GetComponent<Invitation>().KeepClanData();
	}
}
