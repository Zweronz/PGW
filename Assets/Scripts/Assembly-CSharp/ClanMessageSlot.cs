using System;
using UnityEngine;
using WebSocketSharp;

public class ClanMessageSlot : MonoBehaviour
{
	public UILabel mainTextLabel;

	public UILabel icomeDateLabel;

	public UILabel outcomeDateLabel;

	public UISprite openMessageAroow;

	public UIWidget incomeWidget;

	public UIWidget outcomeWidget;

	public UISprite newIconincome;

	public UISprite oldIconincome;

	public UISprite newIconoutcome;

	public UISprite oldIconoutcome;

	private static Color color_0 = new Color(0.26f, 0.69f, 0f);

	private static Color color_1 = new Color(1f, 0.38f, 0f);

	private static Color color_2 = new Color(0.21f, 0.52f, 0.7f);

	private static Color color_3 = new Color(1f, 0f, 0f);

	private static Color color_4 = new Color(1f, 0.3f, 0f);

	private ClanMessageData clanMessageData_0;

	public void SetData(ClanMessageData clanMessageData_1)
	{
		clanMessageData_0 = clanMessageData_1;
		NGUITools.SetActive(base.gameObject, clanMessageData_0 != null);
		if (clanMessageData_0 == null)
		{
			return;
		}
		UserClanData clan = ClanController.ClanController_0.GetClan(clanMessageData_0.string_1);
		if (!clanMessageData_0.string_1.IsNullOrEmpty())
		{
			mainTextLabel.String_0 = string.Format(Localizer.Get("clan.message.short." + clanMessageData_0.int_16 + ".clan"), (clan == null) ? string.Empty : clan.string_2);
		}
		else if (clanMessageData_0.int_18 > 0)
		{
			mainTextLabel.String_0 = string.Format(Localizer.Get("clan.message.short." + clanMessageData_0.int_16 + ".user"), clanMessageData_0.string_2);
		}
		if (clanMessageData_0.int_16 >= 1000)
		{
			mainTextLabel.String_0 = Localizer.Get("clan.message.short." + clanMessageData_0.int_16 + ".clan");
		}
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(clanMessageData_0.int_19);
		icomeDateLabel.String_0 = dateTime.ToString("M");
		outcomeDateLabel.String_0 = icomeDateLabel.String_0;
		NGUITools.SetActive(incomeWidget.gameObject, clanMessageData_0.int_16 == 2 || clanMessageData_0.int_16 == 4 || clanMessageData_0.int_16 == 0);
		NGUITools.SetActive(outcomeWidget.gameObject, !incomeWidget.gameObject.activeSelf);
		Color color = GetColor(clanMessageData_0.int_16);
		if (incomeWidget.gameObject.activeSelf)
		{
			NGUITools.SetActive(newIconincome.gameObject, clanMessageData_0.int_20 == 0);
			NGUITools.SetActive(oldIconincome.gameObject, clanMessageData_0.int_20 != 0);
			for (int i = 0; i < incomeWidget.transform.childCount; i++)
			{
				if (incomeWidget.transform.GetChild(i).GetComponent<UISprite>() != null)
				{
					incomeWidget.transform.GetChild(i).GetComponent<UISprite>().Color_0 = ((!newIconincome.gameObject.activeSelf) ? color : color_3);
				}
				if (incomeWidget.transform.GetChild(i).GetComponent<UILabel>() != null)
				{
					incomeWidget.transform.GetChild(i).GetComponent<UILabel>().Color_0 = ((!newIconincome.gameObject.activeSelf) ? color : color_3);
				}
			}
		}
		else
		{
			for (int j = 0; j < outcomeWidget.transform.childCount; j++)
			{
				if (outcomeWidget.transform.GetChild(j).GetComponent<UISprite>() != null)
				{
					outcomeWidget.transform.GetChild(j).GetComponent<UISprite>().Color_0 = color;
				}
				if (outcomeWidget.transform.GetChild(j).GetComponent<UILabel>() != null)
				{
					outcomeWidget.transform.GetChild(j).GetComponent<UILabel>().Color_0 = color;
				}
			}
			NGUITools.SetActive(newIconoutcome.gameObject, clanMessageData_0.int_20 == 0);
			NGUITools.SetActive(oldIconoutcome.gameObject, clanMessageData_0.int_20 != 0);
		}
		openMessageAroow.Color_0 = color;
		NGUITools.SetActive(openMessageAroow.gameObject, false);
		RedrawMessageStatus();
	}

	private void RedrawMessageStatus()
	{
		if (incomeWidget.gameObject.activeSelf)
		{
			Color color = GetColor(clanMessageData_0.int_16);
			NGUITools.SetActive(newIconincome.gameObject, clanMessageData_0.int_20 == 0);
			NGUITools.SetActive(oldIconincome.gameObject, clanMessageData_0.int_20 != 0);
			for (int i = 0; i < incomeWidget.transform.childCount; i++)
			{
				if (incomeWidget.transform.GetChild(i).GetComponent<UISprite>() != null)
				{
					incomeWidget.transform.GetChild(i).GetComponent<UISprite>().Color_0 = ((!newIconincome.gameObject.activeSelf) ? color : color_3);
				}
				if (incomeWidget.transform.GetChild(i).GetComponent<UILabel>() != null)
				{
					incomeWidget.transform.GetChild(i).GetComponent<UILabel>().Color_0 = ((!newIconincome.gameObject.activeSelf) ? color : color_3);
				}
			}
		}
		else
		{
			NGUITools.SetActive(newIconoutcome.gameObject, clanMessageData_0.int_20 == 0);
			NGUITools.SetActive(oldIconoutcome.gameObject, clanMessageData_0.int_20 != 0);
		}
	}

	public void OnClick()
	{
		ClanMessageWindow.ClanMessageWindow_0.SetFuulMessage(clanMessageData_0);
		NGUITools.SetActive(openMessageAroow.gameObject, true);
		if (clanMessageData_0.int_20 == 0)
		{
			ClanController.ClanController_0.ClanMessageSeen(clanMessageData_0.string_0);
			clanMessageData_0.int_20 = 1;
			RedrawMessageStatus();
		}
	}

	private Color GetColor(int int_0)
	{
		Color white = Color.white;
		switch (clanMessageData_0.int_16)
		{
		case 1000:
		case 1001:
		case 1002:
			white = color_4;
			mainTextLabel.Color_0 = white;
			break;
		case 0:
		case 1:
			white = color_2;
			mainTextLabel.Color_0 = Color.white;
			break;
		case 2:
		case 3:
			white = color_0;
			mainTextLabel.Color_0 = white;
			break;
		case 4:
		case 5:
		case 6:
		case 7:
			white = color_1;
			mainTextLabel.Color_0 = white;
			break;
		}
		return white;
	}
}
