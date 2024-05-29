using UnityEngine;

public class MissClickInWindow : MonoBehaviour
{
	public ChatItem obj;

	public void OnClick()
	{
		obj.OnCloseActionPanel();
	}
}
