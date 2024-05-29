using Rilisoft;
using UnityEngine;

public class RejectClanInvite : MonoBehaviour
{
	private void OnClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		base.transform.parent.GetComponent<Invitation>().DisableButtons();
	}
}
