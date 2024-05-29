using UnityEngine;

public class ClickCatcher : MonoBehaviour
{
	public ComplaintPanelItem obj;

	public void OnClick()
	{
		obj.Click();
	}
}
