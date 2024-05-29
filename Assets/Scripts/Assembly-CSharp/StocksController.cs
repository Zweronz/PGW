using System.Collections.Generic;

public sealed class StocksController
{
	private static StocksController stocksController_0;

	public static StocksController StocksController_0
	{
		get
		{
			return stocksController_0 ?? (stocksController_0 = new StocksController());
		}
	}

	private StocksController()
	{
	}

	public bool IsStockActive(int int_0)
	{
		ActionData actionData_ = null;
		ContentGroupData contentGroupData_ = null;
		return GetActiveStock(int_0, out actionData_, out contentGroupData_) != null;
	}

	public UserOverrideContentGroupData GetActiveStock(int int_0, out ActionData actionData_0, out ContentGroupData contentGroupData_0)
	{
		actionData_0 = null;
		contentGroupData_0 = null;
		UserOverrideContentGroupData userOverrideContentGroupData = UserOverrideContentGroupStorage.UserOverrideContentGroupStorage_0.SearchUnique(1, int_0);
		if (userOverrideContentGroupData != null)
		{
			actionData_0 = StockDataStorage.Get.Storage.GetObjectByKey(userOverrideContentGroupData.int_4);
			contentGroupData_0 = ContentGroupStorage.Get.Storage.GetObjectByKey(userOverrideContentGroupData.int_3);
		}
		return userOverrideContentGroupData;
	}

	public List<UserOverrideContentGroupData> GetActiveStocksByAligmentType(StockAligmentType stockAligmentType_0)
	{
		return UserOverrideContentGroupStorage.UserOverrideContentGroupStorage_0.Search(0, stockAligmentType_0);
	}

	public UserOverrideContentGroupData GetActiveStockItemByType(int int_0, ContentGroupItemType contentGroupItemType_0)
	{
		ActionData actionData_ = null;
		ContentGroupData contentGroupData_ = null;
		UserOverrideContentGroupData userOverrideContentGroupData = null;
		foreach (KeyValuePair<int, UserOverrideContentGroupData> item in (IEnumerable<KeyValuePair<int, UserOverrideContentGroupData>>)UserOverrideContentGroupStorage.UserOverrideContentGroupStorage_0)
		{
			userOverrideContentGroupData = GetActiveStock(item.Value.int_4, out actionData_, out contentGroupData_);
			if (userOverrideContentGroupData == null || contentGroupData_ == null)
			{
				continue;
			}
			List<ContentGroupDataItem> itemsByType = contentGroupData_.GetItemsByType(contentGroupItemType_0);
			if (itemsByType == null || itemsByType.Count == 0)
			{
				continue;
			}
			for (int i = 0; i < itemsByType.Count; i++)
			{
				ContentGroupDataItem contentGroupDataItem = itemsByType[i];
				if (contentGroupDataItem.int_0 == int_0)
				{
					return userOverrideContentGroupData;
				}
			}
		}
		return null;
	}
}
