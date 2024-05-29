using UnityEngine;
using engine.helpers;

public sealed class ClanTopItem : MonoBehaviour
{
	public UILabel placeValue;

	public UISprite placeGoblet;

	public UILabel placeGobletValue;

	public UITexture clanLogo;

	public UILabel clanTitle;

	public UILabel clanScores;

	public UILabel clanLeader;

	public UILabel clanMemberAmount;

	public UILabel clanMemberMax;

	public UILabel statusByInvite;

	public UILabel statusFull;

	public UIButton statusRequest;

	public UILabel statusYourClan;

	public UILabel statusRequestSended;

	public UILabel statusYouInOtherClan;

	public UISprite[] backs;

	private UserClanData userClanData_0;

	private int int_0;

	private bool bool_0;

	private static Texture texture_0;

	private void Awake()
	{
		if (texture_0 == null)
		{
			texture_0 = Resources.Load<Texture>("UI/images/Clan/clan_logo");
		}
	}

	public void SetData(UserClanData userClanData_1, int int_1, bool bool_1)
	{
		userClanData_0 = userClanData_1;
		int_0 = userClanData_1.int_4;
		bool_0 = bool_1;
		UpdateClan();
	}

	public void UpdateClan()
	{
		if (userClanData_0 == null)
		{
			return;
		}
		if (int_0 <= 3)
		{
			NGUITools.SetActive(placeValue.gameObject, false);
			NGUITools.SetActive(placeGoblet.gameObject, true);
			placeGoblet.String_0 = string.Format("clan_place_{0}", int_0);
			placeGobletValue.String_0 = int_0.ToString();
		}
		else
		{
			NGUITools.SetActive(placeValue.gameObject, true);
			NGUITools.SetActive(placeGoblet.gameObject, false);
			placeValue.String_0 = int_0.ToString();
		}
		Vector3 localPosition = clanTitle.Transform_0.localPosition;
		localPosition.x = ((userClanData_0.byte_0 != null) ? 20 : (-15));
		clanTitle.transform.localPosition = localPosition;
		NGUITools.SetActive(clanLogo.gameObject, userClanData_0.byte_0 != null);
		clanLogo.Texture_0 = ((userClanData_0.byte_0 != null) ? Utility.TextureFromData(userClanData_0.byte_0, 16, 16) : texture_0);
		clanTitle.String_0 = userClanData_0.string_2;
		clanScores.String_0 = userClanData_0.int_3.ToString();
		clanLeader.String_0 = userClanData_0.string_1;
		clanMemberAmount.String_0 = userClanData_0.list_0.Count.ToString();
		clanMemberMax.String_0 = "/" + ClanController.ClanController_0.GetClanMembersMaxByLevel(userClanData_0.int_2);
		NGUITools.SetActive(statusByInvite.gameObject, false);
		NGUITools.SetActive(statusYourClan.gameObject, UserClansData.UserClansData_0.UserClanData_0 != null && userClanData_0.string_0.Equals(UserClansData.UserClansData_0.UserClanData_0.string_0));
		NGUITools.SetActive(statusFull.gameObject, !statusYourClan.gameObject.activeSelf && userClanData_0.list_0.Count >= ClanController.ClanController_0.GetClanMembersMaxByLevel(userClanData_0.int_2));
		NGUITools.SetActive(statusYouInOtherClan.gameObject, !statusFull.gameObject.activeSelf && UserClansData.UserClansData_0.UserClanData_0 != null && !userClanData_0.string_0.Equals(UserClansData.UserClansData_0.UserClanData_0.string_0));
		NGUITools.SetActive(statusRequestSended.gameObject, !statusFull.gameObject.activeSelf && userClanData_0.bool_0 && !statusYouInOtherClan.gameObject.activeSelf);
		NGUITools.SetActive(statusRequest.gameObject, !statusFull.gameObject.activeSelf && !statusYouInOtherClan.gameObject.activeSelf && !statusYourClan.gameObject.activeSelf && !statusRequestSended.gameObject.activeSelf);
		bool flag = UserClansData.UserClansData_0.UserClanData_0 != null && UserClansData.UserClansData_0.UserClanData_0.string_0.Equals(userClanData_0.string_0);
		UISprite[] array = backs;
		foreach (UISprite uISprite in array)
		{
			if (!(uISprite == null))
			{
				uISprite.String_0 = string.Format("clan_item_bg_{0}_{1}", (!flag) ? "gray" : "blue", (int_0 % 2 != 0) ? "dark" : "light");
			}
		}
	}

	public void OnSendJoinRequest()
	{
		if (userClanData_0.list_0.Count >= ClanController.ClanController_0.GetClanMembersMaxByLevel(userClanData_0.int_2))
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.to_many_mambers")));
		}
		else if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			ClanController.ClanController_0.SendClanMessage(0, string.Empty, 0, userClanData_0.string_0);
			ClanController.ClanController_0.UpdateClanTop();
			NGUITools.SetActive(statusRequest.gameObject, false);
			NGUITools.SetActive(statusRequestSended.gameObject, true);
		}
		else
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.you_alredy_in_clan")));
		}
	}

	private void OnClick()
	{
		if (userClanData_0 != null)
		{
			UserClanData userClanData = ((!bool_0) ? null : userClanData_0);
			ClanWindowParams clanWindowParams = new ClanWindowParams();
			clanWindowParams.String_0 = userClanData_0.string_0;
			clanWindowParams.UserClanData_0 = userClanData;
			ClanWindow.Show(clanWindowParams);
		}
	}

	public void OnLeaderNameClick()
	{
		ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(userClanData_0.int_0);
	}
}
