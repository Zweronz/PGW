using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.Bank)]
public class BankWindow : BaseGameWindow
{
	private static BankWindow bankWindow_0;

	public UITabsContentController uitabsContentController_0;

	public BankItem bankItem_0;

	public UIWidget uiwidget_0;

	public Vector2[] vector2_0;

	private List<BankPositionData> list_0;

	private bool bool_1;

	public static BankWindow BankWindow_0
	{
		get
		{
			return bankWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return BankWindow_0 != null && BankWindow_0.Boolean_0;
		}
	}

	public static void Show(BankWindowParams bankWindowParams_0 = null)
	{
		if (!(bankWindow_0 != null))
		{
			bankWindow_0 = BaseWindow.Load("BankWindow") as BankWindow;
			bankWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			bankWindow_0.Parameters_0.bool_5 = false;
			bankWindow_0.Parameters_0.bool_0 = true;
			bankWindow_0.Parameters_0.bool_6 = true;
			bankWindow_0.InternalShow(bankWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		bankWindow_0 = null;
	}

	private void Init()
	{
		BankWindowParams bankWindowParams = base.WindowShowParameters_0 as BankWindowParams;
		if (bankWindowParams != null)
		{
			list_0 = bankWindowParams.List_0;
			NGUITools.SetActive(bankItem_0.gameObject, false);
			uitabsContentController_0.onTabActive = OnTabActive;
		}
	}

	private void OnTabActive(int int_0)
	{
		if (int_0 == 0)
		{
			if (bool_1)
			{
				NGUITools.SetActive(uiwidget_0.gameObject, true);
			}
			else
			{
				InflateCoins();
			}
		}
	}

	private void InflateCoins()
	{
		int num = 0;
		while (true)
		{
			if (num < list_0.Count)
			{
				if (num < vector2_0.Length)
				{
					GameObject gameObject = NGUITools.AddChild(uiwidget_0.gameObject, bankItem_0.gameObject);
					gameObject.name = string.Format("{0:00}", num);
					BankItem component = gameObject.GetComponent<BankItem>();
					component.SetData(list_0[num]);
					Vector3 localPosition = gameObject.transform.localPosition;
					localPosition.x = vector2_0[num].x;
					localPosition.y = vector2_0[num].y;
					gameObject.transform.localPosition = localPosition;
					NGUITools.SetActive(gameObject, true);
					num++;
					continue;
				}
				break;
			}
			bool_1 = true;
			break;
		}
	}
}
