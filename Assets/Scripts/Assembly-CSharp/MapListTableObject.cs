using System;
using UnityEngine;
using engine.helpers;

public class MapListTableObject : MonoBehaviour
{
	public UISprite Border;

	public UISprite[] ModeSprites;

	public UILabel Time;

	public UILabel MapName;

	public GameObject[] People;

	public UILabel[] PeopleTexts;

	public UILabel GreenText;

	public UILabel GoldenText;

	public UISprite GreenLock;

	public UISprite GoldenLock;

	private Action<MapListTableObject> action_0;

	private Action<MapListTableObject> action_1;

	private Action<MapListTableObject> action_2;

	private RoomItemData roomItemData_0;

	private bool bool_0;

	public RoomItemData RoomItemData_0
	{
		get
		{
			return roomItemData_0;
		}
	}

	public void SetData(RoomItemData roomItemData_1)
	{
		if (roomItemData_1 == null)
		{
			Log.AddLine(string.Format("[MapListTableObject::SetData] _data == null", roomItemData_0.int_0), Log.LogLevel.ERROR);
			return;
		}
		roomItemData_0 = roomItemData_1;
		setModeSprite(roomItemData_0.modeData_0);
		Time.String_0 = roomItemData_0.int_1.ToString();
		MapName.String_0 = roomItemData_0.string_3;
		setPopularity(roomItemData_0.int_3, roomItemData_0.int_2);
		bool flag = !string.IsNullOrEmpty(roomItemData_0.string_4) || roomItemData_0.bool_1;
		GreenText.gameObject.SetActive(!flag);
		GreenLock.gameObject.SetActive(!flag);
		GoldenText.gameObject.SetActive(flag);
		GoldenLock.gameObject.SetActive(flag);
		if (roomItemData_0.bool_1)
		{
			GoldenLock.gameObject.SetActive(false);
		}
		if (flag)
		{
			GoldenText.String_0 = roomItemData_0.string_2;
		}
		else
		{
			GreenText.String_0 = roomItemData_0.string_2;
		}
		Border.gameObject.SetActive(bool_0);
	}

	public void SetCallbacks(Action<MapListTableObject> action_3, Action<MapListTableObject> action_4, Action<MapListTableObject> action_5)
	{
		action_0 = action_3;
		action_1 = action_4;
		action_2 = action_5;
	}

	public void SetSelected(bool bool_1)
	{
		if (bool_1 && bool_1 != bool_0 && action_0 != null)
		{
			action_0(this);
		}
		if (!bool_1 && bool_1 != bool_0 && action_1 != null)
		{
			action_1(this);
		}
		bool_0 = bool_1;
		Border.gameObject.SetActive(bool_0);
	}

	private void OnClick()
	{
		if (roomItemData_0 != null)
		{
			SetSelected(!bool_0);
		}
	}

	private void OnDoubleClick()
	{
		if (action_2 != null)
		{
			action_2(this);
		}
	}

	private void setModeSprite(ModeData modeData_0)
	{
		for (int i = 0; i < ModeSprites.Length; i++)
		{
			ModeSprites[i].gameObject.SetActive(false);
		}
		if (modeData_0.ModeType_0 != ModeType.DEATH_MATCH && modeData_0.ModeType_0 != ModeType.DUEL)
		{
			if (modeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
			{
				ModeSprites[0].gameObject.SetActive(true);
			}
			else if (modeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
			{
				ModeSprites[2].gameObject.SetActive(true);
			}
		}
		else
		{
			ModeSprites[1].gameObject.SetActive(true);
		}
	}

	private void setPopularity(int int_0, int int_1)
	{
		int num = 0;
		if (int_0 > 0 && int_1 > 0)
		{
			float num2 = (float)int_0 * 100f / (float)int_1;
			if (num2 > 0f && num2 <= 25f)
			{
				num = 1;
			}
			else if (num2 > 25f && num2 <= 50f)
			{
				num = 2;
			}
			else if (num2 > 50f && num2 <= 75f)
			{
				num = 3;
			}
			else if (num2 > 75f && num2 <= 99f)
			{
				num = 4;
			}
			else if (num2 > 99f)
			{
				num = 5;
			}
		}
		for (int i = 0; i < People.Length; i++)
		{
			if (i == num)
			{
				People[i].SetActive(true);
				PeopleTexts[i].String_0 = string.Format("{0}/{1}", int_0, int_1);
			}
			else
			{
				People[i].SetActive(false);
			}
		}
	}
}
