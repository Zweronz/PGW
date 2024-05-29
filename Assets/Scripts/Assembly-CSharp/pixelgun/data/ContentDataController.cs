using System.Collections.Generic;
using engine.helpers;

namespace pixelgun.data
{
	public sealed class ContentDataController
	{
		private static ContentDataController contentDataController_0;

		private HashSet<ContentDataGroup> hashSet_0 = new HashSet<ContentDataGroup>();

		public static ContentDataController ContentDataController_0
		{
			get
			{
				if (contentDataController_0 == null)
				{
					contentDataController_0 = new ContentDataController();
				}
				return contentDataController_0;
			}
		}

		private ContentDataController()
		{
		}

		public void Add(ContentDataGroup contentDataGroup_0)
		{
			if (hashSet_0.Contains(contentDataGroup_0))
			{
				Log.AddLine("ContentDataGroup::Add > already has group", Log.LogLevel.WARNING);
			}
			else
			{
				hashSet_0.Add(contentDataGroup_0);
			}
		}

		public bool IsBlockedItem(string string_0, ContentDataType contentDataType_0 = ContentDataType.UNKNOWN)
		{
			foreach (ContentDataGroup item in hashSet_0)
			{
				if (!item.Boolean_0 && item.Has(string_0, contentDataType_0))
				{
					return true;
				}
			}
			return !ConditionDataController.ConditionDataController_0.IsValid(string_0, contentDataType_0);
		}

		public void Clear()
		{
			foreach (ContentDataGroup item in hashSet_0)
			{
				item.Clear();
			}
			hashSet_0.Clear();
		}
	}
}
