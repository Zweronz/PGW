using System.Collections.Generic;
using System.Runtime.CompilerServices;
using engine.helpers;

namespace pixelgun.data
{
	public sealed class ContentDataGroup
	{
		private bool bool_0;

		private Dictionary<ContentDataType, Dictionary<string, ContentDataItem>> dictionary_0 = new Dictionary<ContentDataType, Dictionary<string, ContentDataItem>>();

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private int int_1;

		[CompilerGenerated]
		private int int_2;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			set
			{
				int_1 = value;
			}
		}

		public int Int32_2
		{
			[CompilerGenerated]
			get
			{
				return int_2;
			}
			[CompilerGenerated]
			set
			{
				int_2 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
		}

		public ContentDataGroup()
			: this(0, "stub")
		{
		}

		public ContentDataGroup(int int_3, string string_1)
		{
			Int32_0 = int_3;
			String_0 = string_1;
			Int32_1 = 0;
			Int32_2 = 0;
			if (int_3 == 0)
			{
				bool_0 = true;
			}
		}

		public void Add(ContentDataItem contentDataItem_0)
		{
			if (contentDataItem_0 == null)
			{
				Log.AddLine("ContentDataGroup::Add > item is null", Log.LogLevel.WARNING);
				return;
			}
			if (contentDataItem_0.ContentDataType_0 == ContentDataType.UNKNOWN)
			{
				Log.AddLine("ContentDataGroup::Add > item type is UNKNOWN", Log.LogLevel.WARNING);
				return;
			}
			if (!dictionary_0.ContainsKey(contentDataItem_0.ContentDataType_0))
			{
				dictionary_0.Add(contentDataItem_0.ContentDataType_0, new Dictionary<string, ContentDataItem>());
			}
			if (dictionary_0[contentDataItem_0.ContentDataType_0].ContainsKey(contentDataItem_0.String_0))
			{
				Log.AddLine("ContentDataGroup::Add > already has item", Log.LogLevel.WARNING);
			}
			dictionary_0[contentDataItem_0.ContentDataType_0].Add(contentDataItem_0.String_0, contentDataItem_0);
		}

		public bool Has(string string_1, ContentDataType contentDataType_0)
		{
			if (!dictionary_0.ContainsKey(contentDataType_0))
			{
				return false;
			}
			if (contentDataType_0 == ContentDataType.UNKNOWN)
			{
				return HasAny(string_1);
			}
			return dictionary_0[contentDataType_0].ContainsKey(string_1);
		}

		private bool HasAny(string string_1)
		{
			foreach (KeyValuePair<ContentDataType, Dictionary<string, ContentDataItem>> item in dictionary_0)
			{
				if (item.Value.ContainsKey(string_1))
				{
					return true;
				}
			}
			return false;
		}

		public bool CanOpen()
		{
			if (Int32_1 == 0 && Int32_2 == 0)
			{
				return false;
			}
			int userLevel = UserController.UserController_0.GetUserLevel();
			if (Int32_1 != 0 && userLevel >= Int32_1 && Int32_2 != 0 && userLevel <= Int32_2)
			{
				return true;
			}
			if (Int32_1 == 0 && Int32_2 != 0 && userLevel <= Int32_2)
			{
				return true;
			}
			if (Int32_2 == 0 && Int32_1 != 0 && userLevel >= Int32_1)
			{
				return true;
			}
			return false;
		}

		public void Clear()
		{
			dictionary_0.Clear();
		}
	}
}
