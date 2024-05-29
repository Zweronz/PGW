using System.Collections.Generic;

public class TestDataSource : IPScrollViewDataSource
{
	private List<int> list_0 = new List<int>();

	protected override int GetLastItemId()
	{
		if (list_0 != null && list_0.Count != 0)
		{
			return list_0[list_0.Count - 1];
		}
		return 0;
	}

	public void SetData(List<int> list_1, int int_4 = 0)
	{
		list_0 = list_1;
		SetItemsCount(list_0.Count);
		if (list_0 != null && uiwidget_0 != null)
		{
			for (int i = 0; i < uiwidget_0.Length; i++)
			{
				NGUITools.SetActive(uiwidget_0[i].gameObject, i < int_0);
			}
		}
		if (list_0 == null || uiwidget_0 != null)
		{
		}
		scrollView.UIScrollView_0.restrictWithinPanel = false;
		Reset(list_0.Count);
	}

	public override void UpdateWidget(int int_4, int int_5)
	{
		TestItem componentInChildren = uiwidget_0[int_4].GetComponentInChildren<TestItem>();
		if (int_5 < list_0.Count)
		{
			int num = list_0[int_5];
			componentInChildren.UpdateItem(num);
		}
		base.UpdateWidget(int_4, int_5);
	}
}
