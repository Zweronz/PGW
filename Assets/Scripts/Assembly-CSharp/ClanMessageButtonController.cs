using UnityEngine;

public class ClanMessageButtonController : MonoBehaviour
{
	public GameObject OpenButton;

	public GameObject CloseButton;

	public GameObject Counter;

	public UILabel CounterLabel;

	private void Start()
	{
		ClanController.ClanController_0.Subscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
		InitMessageCount();
	}

	private void OnDestroy()
	{
		ClanController.ClanController_0.Unsubscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
	}

	private void InitMessageCount(ClanController.EventData eventData_0 = null)
	{
		if (!ClanController.ClanController_0.Boolean_0)
		{
			NGUITools.SetActive(Counter, false);
			NGUITools.SetActive(OpenButton, false);
			NGUITools.SetActive(CloseButton, false);
			return;
		}
		int newClanMessagesCount = ClanController.ClanController_0.GetNewClanMessagesCount();
		NGUITools.SetActive(Counter, newClanMessagesCount > 0);
		NGUITools.SetActive(OpenButton, newClanMessagesCount <= 0);
		NGUITools.SetActive(CloseButton, newClanMessagesCount > 0);
		if (newClanMessagesCount > 0)
		{
			CounterLabel.String_0 = newClanMessagesCount.ToString();
		}
	}

	public void OnMessageButtonClick()
	{
		ClanMessageWindow.Show();
	}
}
