using System.Collections.Generic;
using engine.data;

public sealed class ContentGroupController
{
	private static ContentGroupController contentGroupController_0;

	public static ContentGroupController ContentGroupController_0
	{
		get
		{
			return contentGroupController_0 ?? (contentGroupController_0 = new ContentGroupController());
		}
	}

	private ContentGroupController()
	{
	}

	private ContentGroupData GetContentGroupData(int int_0)
	{
		return ContentGroupStorage.Get.Storage.GetObjectByKey(int_0);
	}

	private List<ContentGroupDataItem> GetContentGroupDataItems(int int_0)
	{
		ContentGroupData contentGroupData = GetContentGroupData(int_0);
		if (contentGroupData == null)
		{
			return null;
		}
		return contentGroupData.list_0;
	}

	public void Init()
	{
		if (!StorageManager.StorageManager_0.Contains(GetStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE))
		{
			StorageManager.StorageManager_0.Subscribe(GetStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE);
		}
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE, UpdateOverride);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE, RemoveOverride);
	}

	public void UpdateOverride(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		if (overrideContentGroupEventData_0.overrideContentGroupChangeType_0 == OverrideContentGroupChangeType.All)
		{
			ChangeContentGroupState(overrideContentGroupEventData_0.userOverrideContentGroupData_0.int_3, false);
		}
	}

	public void RemoveOverride(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		ChangeContentGroupState(overrideContentGroupEventData_0.userOverrideContentGroupData_0.int_3, true);
	}

	private void GetStoragesDataComplete()
	{
		foreach (KeyValuePair<int, ContentGroupData> item in (IEnumerable<KeyValuePair<int, ContentGroupData>>)ContentGroupStorage.Get.Storage)
		{
			ChangeContentGroupState(item.Key, true);
		}
	}

	private void ChangeContentGroupState(int int_0, bool bool_0)
	{
		List<ContentGroupDataItem> contentGroupDataItems = GetContentGroupDataItems(int_0);
		if (contentGroupDataItems == null || contentGroupDataItems.Count == 0)
		{
			return;
		}
		for (int i = 0; i < contentGroupDataItems.Count; i++)
		{
			ContentGroupDataItem contentGroupDataItem = contentGroupDataItems[i];
			switch (contentGroupDataItem.contentGroupItemType_0)
			{
			case ContentGroupItemType.ARTIKUL:
			{
				ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(contentGroupDataItem.int_0);
				artikul.Boolean_0 = bool_0;
				break;
			}
			case ContentGroupItemType.MAP:
			{
				MapData objectByKey2 = MapStorage.Get.Storage.GetObjectByKey(contentGroupDataItem.int_0);
				objectByKey2.Boolean_3 = bool_0;
				break;
			}
			case ContentGroupItemType.MODE:
			{
				ModeData mode = ModesController.ModesController_0.GetMode(contentGroupDataItem.int_0);
				mode.Boolean_4 = bool_0;
				break;
			}
			case ContentGroupItemType.STOCK:
			{
				ActionData objectByKey = StockDataStorage.Get.Storage.GetObjectByKey(contentGroupDataItem.int_0);
				objectByKey.Boolean_0 = bool_0;
				break;
			}
			case ContentGroupItemType.SHOP:
			{
				ShopArtikulData shopArtikul = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(contentGroupDataItem.int_0);
				shopArtikul.Boolean_5 = bool_0;
				break;
			}
			}
		}
	}
}
