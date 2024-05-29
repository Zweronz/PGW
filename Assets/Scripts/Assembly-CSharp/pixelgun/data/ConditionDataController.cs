using System.Collections.Generic;
using engine.helpers;

namespace pixelgun.data
{
	public sealed class ConditionDataController
	{
		private static ConditionDataController conditionDataController_0;

		private Dictionary<ContentDataType, Dictionary<string, ContentDataItem>> dictionary_0 = new Dictionary<ContentDataType, Dictionary<string, ContentDataItem>>();

		public static ConditionDataController ConditionDataController_0
		{
			get
			{
				if (conditionDataController_0 == null)
				{
					conditionDataController_0 = new ConditionDataController();
				}
				return conditionDataController_0;
			}
		}

		private ConditionDataController()
		{
		}

		public void Add(ContentDataItem contentDataItem_0)
		{
			if (contentDataItem_0 == null)
			{
				Log.AddLine("ConditionDataController::Add > item is null", Log.LogLevel.WARNING);
				return;
			}
			if (contentDataItem_0.ContentDataType_0 == ContentDataType.UNKNOWN)
			{
				Log.AddLine("ConditionDataController::Add > item type is UNKNOWN", Log.LogLevel.WARNING);
				return;
			}
			if (!dictionary_0.ContainsKey(contentDataItem_0.ContentDataType_0))
			{
				dictionary_0.Add(contentDataItem_0.ContentDataType_0, new Dictionary<string, ContentDataItem>());
			}
			if (dictionary_0[contentDataItem_0.ContentDataType_0].ContainsKey(contentDataItem_0.String_0))
			{
				Log.AddLine("ConditionDataController::Add > already has item", Log.LogLevel.WARNING);
			}
			else
			{
				dictionary_0[contentDataItem_0.ContentDataType_0].Add(contentDataItem_0.String_0, contentDataItem_0);
			}
		}

		public void AddList(List<ContentDataItem> list_0)
		{
			foreach (ContentDataItem item in list_0)
			{
				Add(item);
			}
		}

		public bool IsValid(string string_0, ContentDataType contentDataType_0 = ContentDataType.UNKNOWN)
		{
			if (!dictionary_0.ContainsKey(contentDataType_0))
			{
				return true;
			}
			if (contentDataType_0 == ContentDataType.UNKNOWN)
			{
				return IsValidAny(string_0);
			}
			if (dictionary_0[contentDataType_0].ContainsKey(string_0))
			{
				ContentDataItem contentDataItem = dictionary_0[contentDataType_0][string_0];
				return contentDataItem.CheckConditions();
			}
			return true;
		}

		private bool IsValidAny(string string_0)
		{
			foreach (KeyValuePair<ContentDataType, Dictionary<string, ContentDataItem>> item in dictionary_0)
			{
				if (item.Value.ContainsKey(string_0))
				{
					ContentDataItem contentDataItem = item.Value[string_0];
					return contentDataItem.CheckConditions();
				}
			}
			return true;
		}

		public void Clear()
		{
			dictionary_0.Clear();
		}
	}
}
