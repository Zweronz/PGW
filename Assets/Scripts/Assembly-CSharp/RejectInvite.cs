using Rilisoft;
using UnityEngine;

internal sealed class RejectInvite : MonoBehaviour
{
	private void OnClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		Invitation component = base.transform.parent.GetComponent<Invitation>();
		if (component != null)
		{
			base.transform.parent.GetComponent<Invitation>().DisableButtons();
		}
		else
		{
			Debug.LogWarning("invitation == null");
		}
	}
}
