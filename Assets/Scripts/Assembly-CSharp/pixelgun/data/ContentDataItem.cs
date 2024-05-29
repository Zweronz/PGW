using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace pixelgun.data
{
	public sealed class ContentDataItem
	{
		private List<IDataCondition> list_0 = new List<IDataCondition>();

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private ContentDataType contentDataType_0;

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

		public ContentDataType ContentDataType_0
		{
			[CompilerGenerated]
			get
			{
				return contentDataType_0;
			}
			[CompilerGenerated]
			set
			{
				contentDataType_0 = value;
			}
		}

		public ContentDataItem()
			: this(0, string.Empty, ContentDataType.UNKNOWN)
		{
		}

		public ContentDataItem(int int_1, string string_1, ContentDataType contentDataType_1)
		{
			Int32_0 = int_1;
			String_0 = string_1;
			ContentDataType_0 = contentDataType_1;
		}

		public ContentDataItem AddCondition(IDataCondition idataCondition_0)
		{
			list_0.Add(idataCondition_0);
			return this;
		}

		public bool CheckConditions()
		{
			foreach (IDataCondition item in list_0)
			{
				if (!item.Check())
				{
					return false;
				}
			}
			return true;
		}
	}
}
