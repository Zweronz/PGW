using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ClanMessageWindow)]
public class ClanMessageWindow : BaseGameWindow
{
	public UITable uitable_0;

	public ClanMessageSlot clanMessageSlot_0;

	public ClanMessageBodySlot clanMessageBodySlot_0;

	public GameObject gameObject_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIButton uibutton_2;

	public UIButton uibutton_3;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	private List<ClanMessageData> list_0 = new List<ClanMessageData>();

	private List<GameObject> list_1 = new List<GameObject>();

	private int int_0;

	private int int_1;

	private static ClanMessageWindow clanMessageWindow_0;

	public static ClanMessageWindow ClanMessageWindow_0
	{
		get
		{
			return clanMessageWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanWindow.ClanWindow_0 != null && ClanWindow.ClanWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanMessageWindowParams clanMessageWindowParams_0 = null)
	{
		if (clanMessageWindow_0 != null)
		{
			return;
		}
		clanMessageWindow_0 = BaseWindow.Load("ClanMessageWindow") as ClanMessageWindow;
		clanMessageWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
		clanMessageWindow_0.Parameters_0.bool_5 = true;
		clanMessageWindow_0.Parameters_0.bool_0 = false;
		clanMessageWindow_0.Parameters_0.bool_6 = true;
		clanMessageWindow_0.InternalShow(clanMessageWindowParams_0);
		List<ClanMessageData> list = ClanController.ClanController_0.list_1;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].int_16 != 1)
			{
				clanMessageWindow_0.list_0.Add(list[i]);
			}
		}
		clanMessageWindow_0.list_0.Sort(DateCompare);
		clanMessageWindow_0.Init();
	}

	private static int DateCompare(ClanMessageData clanMessageData_0, ClanMessageData clanMessageData_1)
	{
		return clanMessageData_1.int_19.CompareTo(clanMessageData_0.int_19);
	}

	public override void OnHide()
	{
		base.OnHide();
		clanMessageWindow_0 = null;
	}

	private void Init()
	{
		NGUITools.SetActive(clanMessageSlot_0.gameObject, false);
		NGUITools.SetActive(clanMessageBodySlot_0.gameObject, false);
		FirstInitPage();
		InitPageControls();
	}

	private void InitPageControls()
	{
		int_1 = Mathf.Max(Mathf.CeilToInt((float)list_0.Count / 5f), 1);
		gameObject_0.SetActive(int_1 > 1);
		uilabel_0.String_0 = (int_0 + 1).ToString();
		uilabel_1.String_0 = int_1.ToString();
		uibutton_0.Boolean_0 = int_0 > 0 && int_1 > 1;
		uibutton_1.Boolean_0 = int_0 < int_1 - 1 && int_1 > 1;
		uibutton_3.Boolean_0 = int_0 > 0 && int_1 > 1;
		uibutton_2.Boolean_0 = int_0 < int_1 - 1 && int_1 > 1;
	}

	public void GoNextPage()
	{
		int_0++;
		InitPageControls();
		ReinitCurrentPage();
	}

	public void GoPrevPage()
	{
		if (int_0 != 0)
		{
			int_0--;
			InitPageControls();
			ReinitCurrentPage();
		}
	}

	public void GoLastPage()
	{
		int_0 = int_1 - 1;
		InitPageControls();
		ReinitCurrentPage();
	}

	public void GoFirstPage()
	{
		int_0 = 0;
		InitPageControls();
		ReinitCurrentPage();
	}

	public void DeleteMessage(string string_0)
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i].string_0.Equals(string_0))
			{
				list_0.RemoveAt(i);
			}
		}
		HideAllOpenMessage();
		InitPageControls();
		ReinitCurrentPage();
		ClanController.ClanController_0.UpdateClanMessageList();
	}

	public void ReinitCurrentPage()
	{
		int num = 0;
		int num2 = 0;
		for (int i = int_0 * 5; i < (int_0 + 1) * 5; i++)
		{
			ClanMessageData clanMessageData = null;
			if (i < list_0.Count)
			{
				clanMessageData = list_0[i];
			}
			if (clanMessageData != null)
			{
				num++;
			}
			ClanMessageSlot component = uitable_0.transform.GetChild(num2).GetComponent<ClanMessageSlot>();
			component.SetData(clanMessageData);
			num2++;
		}
		if (num == 0)
		{
			GoPrevPage();
		}
	}

	public void FirstInitPage()
	{
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			ClanMessageData data = null;
			if (i < list_0.Count)
			{
				data = list_0[i];
			}
			GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, clanMessageSlot_0.gameObject);
			gameObject.name = string.Format("{0:0000}", num++);
			ClanMessageSlot component = gameObject.GetComponent<ClanMessageSlot>();
			component.SetData(data);
			list_1.Add(gameObject);
		}
	}

	public void SetFuulMessage(ClanMessageData clanMessageData_0)
	{
		HideAllOpenMessage();
		clanMessageBodySlot_0.SetData(clanMessageData_0);
	}

	private void HideAllOpenMessage()
	{
		for (int i = 0; i < list_1.Count; i++)
		{
			NGUITools.SetActive(list_1[i].GetComponent<ClanMessageSlot>().openMessageAroow.gameObject, false);
		}
		NGUITools.SetActive(clanMessageBodySlot_0.gameObject, false);
	}
}
