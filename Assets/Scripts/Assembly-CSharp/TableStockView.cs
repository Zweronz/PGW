using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TableStockView : BaseStockView
{
	public UITable uitable_0;

	public UIScrollView uiscrollView_0;

	public GameObject gameObject_0;

	public StockAligmentType stockAligmentType_0;

	private List<GameObject> list_0 = new List<GameObject>();

	private List<ActionData> list_1 = new List<ActionData>();

	private ActionData actionData_0;

	private int int_0;

	[CompilerGenerated]
	private static Comparison<ActionData> comparison_0;

	public override void UpdateStock()
	{
		List<UserOverrideContentGroupData> activeStocksByAligmentType = StocksController.StocksController_0.GetActiveStocksByAligmentType(stockAligmentType_0);
		list_1.Clear();
		foreach (UserOverrideContentGroupData item in activeStocksByAligmentType)
		{
			ActionData actionData = null;
			ContentGroupData contentGroupData_ = null;
			StocksController.StocksController_0.GetActiveStock(item.int_4, out actionData, out contentGroupData_);
			if (actionData.stockWndType_0 != StockWndType.MAIL_VERIFICATION || !UserController.UserController_0.UserData_0.user_0.bool_5)
			{
				list_1.Add(actionData);
			}
		}
		list_1.Sort((ActionData actionData_1, ActionData actionData_2) => actionData_1.int_1.CompareTo(actionData_1.int_1));
		UpdatStockObjectsCount();
		StockSetData();
		RepositionStocksView();
		Invoke("RepositionStockContent", 0.1f);
	}

	private void RepositionStockContent()
	{
		uitable_0.Reposition();
		uiscrollView_0.ResetPosition();
	}

	private void UpdatStockObjectsCount()
	{
		int num = ((list_1 != null) ? list_1.Count : 0);
		if (num > list_0.Count)
		{
			while (num > list_0.Count)
			{
				GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, gameObject_0);
				gameObject.name = string.Format("{0:0000}", list_0.Count);
				list_0.Add(gameObject);
			}
		}
		else if (num < list_0.Count)
		{
			while (num < list_0.Count)
			{
				GameObject gameObject2 = list_0[list_0.Count - 1];
				list_0.RemoveAt(list_0.Count - 1);
				gameObject2.transform.parent = null;
				UnityEngine.Object.Destroy(gameObject2);
			}
		}
	}

	private void StockSetData()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			StocksBaseTableObject component = list_0[i].GetComponent<StocksBaseTableObject>();
			component.SetData(list_1[i].int_0);
			NGUITools.SetActive(list_0[i], true);
		}
	}

	private void RepositionStocksView()
	{
		int num = ((list_1 != null) ? list_1.Count : 0);
		NGUITools.SetActive(base.gameObject, num > 0);
		int int32_ = gameObject_0.transform.GetComponent<UIWidget>().Int32_1;
		int num2 = Mathf.Min(int32_ * num, int32_ * 4);
		Vector4 vector4_ = uiscrollView_0.GetComponent<UIPanel>().Vector4_1;
		vector4_.w = num2;
		uiscrollView_0.GetComponent<UIPanel>().Vector4_1 = vector4_;
		base.gameObject.GetComponent<UIWidget>().UpdateAnchors();
	}
}
