using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.GachaRewardWindow)]
public class StockGachaRewardWindow : BaseGameWindow
{
	private static StockGachaRewardWindow stockGachaRewardWindow_0;

	public UIGrid uigrid_0;

	public UIScrollView uiscrollView_0;

	public StockGachaRewardWindowItem stockGachaRewardWindowItem_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	private bool bool_1;

	private bool bool_2 = true;

	private List<StockGachaRewardWindowItem> list_0 = new List<StockGachaRewardWindowItem>();

	private Dictionary<int, GameObject> dictionary_1 = new Dictionary<int, GameObject>();

	public static StockGachaRewardWindow StockGachaRewardWindow_0
	{
		get
		{
			return stockGachaRewardWindow_0;
		}
	}

	public Dictionary<int, GameObject> Dictionary_0
	{
		get
		{
			return dictionary_1;
		}
	}

	public static void Show(StockGachaRewardWindowParams stockGachaRewardWindowParams_0)
	{
		if (!(stockGachaRewardWindow_0 != null))
		{
			stockGachaRewardWindow_0 = BaseWindow.Load("StockGachaRewardWindow") as StockGachaRewardWindow;
			stockGachaRewardWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			stockGachaRewardWindow_0.Parameters_0.bool_5 = true;
			stockGachaRewardWindow_0.Parameters_0.bool_0 = false;
			stockGachaRewardWindow_0.Parameters_0.bool_6 = false;
			stockGachaRewardWindow_0.Parameters_0.gameEvent_0 = WindowController.GameEvent.STOCK_GACHA_WND_HIDE;
			stockGachaRewardWindow_0.Parameters_0.bool_4 = true;
			stockGachaRewardWindow_0.InternalShow(stockGachaRewardWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Invoke("HideMainGacha", 0.1f);
		Init();
		base.OnShow();
		AddInputKey(KeyCode.Escape, delegate
		{
			if (base.Boolean_0 && bool_1)
			{
				Hide();
			}
		});
	}

	public override void OnHide()
	{
		StockGachaRewardWindowParams stockGachaRewardWindowParams = base.WindowShowParameters_0 as StockGachaRewardWindowParams;
		if (stockGachaRewardWindowParams.actionData_0 != null)
		{
			ActionData actionData_ = null;
			ContentGroupData contentGroupData_ = null;
			StocksController.StocksController_0.GetActiveStock(stockGachaRewardWindowParams.actionData_0.int_0, out actionData_, out contentGroupData_);
			StockGachaWindow.Show(new StockWindowParams(actionData_.int_0));
		}
		base.OnHide();
		stockGachaRewardWindow_0 = null;
		foreach (KeyValuePair<int, GameObject> item in dictionary_1)
		{
			item.Value.transform.parent = null;
			Object.Destroy(item.Value);
		}
		dictionary_1.Clear();
	}

	private void HideMainGacha()
	{
		if (StockGachaWindow.StockGachaWindow_0 != null)
		{
			StockGachaWindow.StockGachaWindow_0.Hide();
		}
	}

	private void Init()
	{
		base.transform.localPosition = new Vector3(0f, 0f, 360f);
		WindowController.WindowController_0.WindowFader_0.transform.localPosition = new Vector3(0f, 0f, 400f);
		UpdateItems();
		bool_1 = true;
		SetCanClose();
		base.GetComponent<Animation>().Play("GachaChestReward");
	}

	public void SetCanClose()
	{
		bool_1 = !bool_1;
		uibutton_0.gameObject.SetActive(bool_1);
		uibutton_1.gameObject.SetActive(bool_1);
	}

	private void UpdateItems()
	{
		ClearItems();
		int num = 0;
		StockGachaRewardWindowParams stockGachaRewardWindowParams = base.WindowShowParameters_0 as StockGachaRewardWindowParams;
		ApplyBonusNetworkCommand applyBonusNetworkCommand_ = stockGachaRewardWindowParams.applyBonusNetworkCommand_0;
		stockGachaRewardWindowItem_0.gameObject.SetActive(false);
		if (applyBonusNetworkCommand_.dictionary_1 != null)
		{
			foreach (KeyValuePair<int, int> item in applyBonusNetworkCommand_.dictionary_1)
			{
				GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, stockGachaRewardWindowItem_0.gameObject);
				gameObject.name = string.Format("{0:0000}", num++);
				StockGachaRewardWindowItem component = gameObject.GetComponent<StockGachaRewardWindowItem>();
				component.art = new KeyValuePair<int, int>(item.Key, item.Value);
				NGUITools.SetActive(gameObject, true);
				list_0.Add(component);
				TooltipInfo component2 = gameObject.GetComponent<TooltipInfo>();
				ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(item.Key);
				if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
				{
					component2.weaponID = item.Key;
				}
				else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
				{
					component2.wearID = item.Key;
				}
			}
		}
		if (applyBonusNetworkCommand_.dictionary_0 != null)
		{
			GameObject gameObject2 = NGUITools.AddChild(uigrid_0.gameObject, stockGachaRewardWindowItem_0.gameObject);
			gameObject2.name = string.Format("{0:0000}", num++);
			StockGachaRewardWindowItem component3 = gameObject2.GetComponent<StockGachaRewardWindowItem>();
			component3.money = new KeyValuePair<MoneyType, int>(MoneyType.MONEY_TYPE_COINS, applyBonusNetworkCommand_.dictionary_0[MoneyType.MONEY_TYPE_COINS]);
			NGUITools.SetActive(gameObject2, true);
			list_0.Add(component3);
		}
		if (applyBonusNetworkCommand_.int_1 > 0)
		{
			GameObject gameObject3 = NGUITools.AddChild(uigrid_0.gameObject, stockGachaRewardWindowItem_0.gameObject);
			gameObject3.name = string.Format("{0:0000}", num++);
			StockGachaRewardWindowItem component4 = gameObject3.GetComponent<StockGachaRewardWindowItem>();
			component4.exp = applyBonusNetworkCommand_.int_1;
			NGUITools.SetActive(gameObject3, true);
			list_0.Add(component4);
		}
		if (list_0.Count < 3)
		{
			int int32_ = stockGachaRewardWindowItem_0.gameObject.GetComponent<UIWidget>().Int32_0;
			Vector3 localPosition = uiscrollView_0.transform.localPosition;
			localPosition.x += ((float)uiscrollView_0.transform.parent.gameObject.GetComponent<UIWidget>().Int32_0 - (float)(list_0.Count * int32_)) * 0.5f;
			uiscrollView_0.transform.localPosition = localPosition;
		}
		RepositionContent();
	}

	private void ClearItems()
	{
		list_0.Clear();
		BetterList<Transform> childList = uigrid_0.GetChildList();
		foreach (Transform item in childList)
		{
			if (!(item == null))
			{
				item.parent = null;
				Object.Destroy(item.gameObject);
			}
		}
	}

	private void RepositionContent()
	{
		uigrid_0.Reposition();
		if (bool_2)
		{
			bool_2 = false;
		}
		else
		{
			uiscrollView_0.ResetPosition();
		}
	}

	public static GameObject GetGameObjectInstance(int int_0, GameObject gameObject_0)
	{
		if (StockWindow.StockWindow_0 == null)
		{
			return gameObject_0;
		}
		GameObject value = null;
		if (!StockGachaRewardWindow_0.Dictionary_0.TryGetValue(int_0, out value))
		{
			value = Object.Instantiate(gameObject_0) as GameObject;
			StockGachaRewardWindow_0.Dictionary_0.Add(int_0, value);
		}
		return value;
	}
}
