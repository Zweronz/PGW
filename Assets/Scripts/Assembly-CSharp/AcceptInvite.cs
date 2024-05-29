using Rilisoft;
using UnityEngine;

internal sealed class AcceptInvite : MonoBehaviour
{
	private void OnClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		Invitation component = base.transform.parent.GetComponent<Invitation>();
		if (component == null)
		{
			Debug.LogWarning("invitation == null");
		}
		else
		{
			component.DisableButtons();
		}
	}
}
