using UnityEngine;

public class BattlePlayerItem : MonoBehaviour
{
	public UISprite pos;

	public UILabel posLabel;

	public UISprite posIcon;

	public new UISprite name;

	public UILabel nameLabel;

	public UISprite clan;

	public UILabel clanLabel;

	public UITexture clanIcon;

	public UISprite score;

	public UILabel scoreLabel;

	public UISprite kills;

	public UILabel killsLabel;

	public UISprite assists;

	public UILabel assistsLabel;

	public UISprite deaths;

	public UILabel deathsLabel;

	public UISprite ping;

	public UILabel pingLabel;

	private void Awake()
	{
		NGUITools.SetActive(posIcon.gameObject, false);
		SetVisible(false);
	}

	private void SetVisible(bool bool_0)
	{
		NGUITools.SetActive(posLabel.gameObject, bool_0);
		NGUITools.SetActive(nameLabel.gameObject, bool_0);
		NGUITools.SetActive(clanLabel.gameObject, bool_0);
		NGUITools.SetActive(clanIcon.gameObject, bool_0);
		NGUITools.SetActive(scoreLabel.gameObject, bool_0);
		NGUITools.SetActive(killsLabel.gameObject, bool_0);
		NGUITools.SetActive(assistsLabel.gameObject, bool_0);
		NGUITools.SetActive(deathsLabel.gameObject, bool_0);
		NGUITools.SetActive(pingLabel.gameObject, bool_0);
	}

	private void SetMine(bool bool_0)
	{
		pos.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		name.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		clan.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		score.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		kills.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		assists.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		deaths.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
		ping.String_0 = ((!bool_0) ? "tab_item_grey_bg" : "tab_item_blue_bg");
	}

	public void SetData(BattlePlayerData battlePlayerData_0)
	{
		if (battlePlayerData_0 == null)
		{
			NGUITools.SetActive(posIcon.gameObject, false);
			SetVisible(false);
			SetMine(false);
			return;
		}
		SetVisible(true);
		posLabel.String_0 = battlePlayerData_0.int_0.ToString();
		nameLabel.String_0 = battlePlayerData_0.string_0;
		clanLabel.String_0 = battlePlayerData_0.string_1;
		clanIcon.Texture_0 = battlePlayerData_0.texture_0;
		NGUITools.SetActive(clanIcon.gameObject, battlePlayerData_0.texture_0 != null);
		scoreLabel.String_0 = battlePlayerData_0.int_1.ToString();
		killsLabel.String_0 = battlePlayerData_0.int_2.ToString();
		assistsLabel.String_0 = battlePlayerData_0.int_3.ToString();
		deathsLabel.String_0 = battlePlayerData_0.int_4.ToString();
		pingLabel.String_0 = battlePlayerData_0.int_5.ToString();
		Vector3 localPosition = clanLabel.Transform_0.localPosition;
		localPosition.x = ((!(battlePlayerData_0.texture_0 == null)) ? 10 : 0);
		clanLabel.transform.localPosition = localPosition;
		NGUITools.SetActive(posIcon.gameObject, battlePlayerData_0.int_0 < 4);
		SetMine(battlePlayerData_0.bool_0);
	}
}
