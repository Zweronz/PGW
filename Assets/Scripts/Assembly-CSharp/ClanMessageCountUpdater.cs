using UnityEngine;

public class ClanMessageCountUpdater : MonoBehaviour
{
	public UILabel CountLabel;

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
		int newClanMessagesCount = ClanController.ClanController_0.GetNewClanMessagesCount();
		CountLabel.String_0 = newClanMessagesCount.ToString();
		NGUITools.SetActive(base.gameObject, newClanMessagesCount > 0);
	}
}
