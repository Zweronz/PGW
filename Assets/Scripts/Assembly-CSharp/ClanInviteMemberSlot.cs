using UnityEngine;

public class ClanInviteMemberSlot : MonoBehaviour
{
	public UILabel NickLabel;

	public UILabel NickLabelReady;

	public UIWidget[] statuses;

	public UISprite nameReadyBKG;

	public UISprite statusReadyBKG;

	public int status;

	private UserClanMemberData userClanMemberData_0;

	private int int_0;

	private bool bool_0;

	public UserClanMemberData UserClanMemberData_0
	{
		get
		{
			return userClanMemberData_0;
		}
	}

	public void SetData(UserClanMemberData userClanMemberData_1, int int_1, bool bool_1)
	{
		bool_0 = bool_1;
		int_0 = int_1;
		userClanMemberData_0 = userClanMemberData_1;
		HideStatuses();
		NGUITools.SetActive(statuses[1].gameObject, userClanMemberData_1.bool_0);
		NGUITools.SetActive(statuses[0].gameObject, !userClanMemberData_1.bool_0);
		NickLabel.String_0 = userClanMemberData_1.string_0;
		NickLabelReady.String_0 = userClanMemberData_1.string_0;
		if (!LevelStorage.Get.GetTier(userClanMemberData_1.int_1).Equals(LevelStorage.Get.GetTier(UserController.UserController_0.GetUserLevel())))
		{
			HideStatuses();
			NGUITools.SetActive(statuses[6].gameObject, true);
		}
	}

	public void HideStatuses()
	{
		status = 0;
		for (int i = 0; i < statuses.Length; i++)
		{
			NGUITools.SetActive(statuses[i].gameObject, false);
		}
	}

	public void SendRequest()
	{
		ClanBattleController.ClanBattleController_0.SendRequestToUser(1, userClanMemberData_0.int_0, UserController.UserController_0.UserData_0.user_0.string_0, int_0, bool_0);
		HideStatuses();
		status = 1;
		NGUITools.SetActive(statuses[5].gameObject, true);
	}

	public void SetStatus(int int_1)
	{
		status = int_1;
		NGUITools.SetActive(statuses[int_1].gameObject, true);
		NGUITools.SetActive(nameReadyBKG.gameObject, status == 2);
		NGUITools.SetActive(statusReadyBKG.gameObject, status == 2);
		NGUITools.SetActive(NickLabelReady.gameObject, status == 2);
		NGUITools.SetActive(NickLabel.gameObject, status != 2);
	}
}
