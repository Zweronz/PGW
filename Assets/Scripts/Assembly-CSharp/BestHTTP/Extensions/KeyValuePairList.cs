using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.Extensions
{
	public class KeyValuePairList
	{
		[CompilerGenerated]
		private List<KeyValuePair> list_0;

		public List<KeyValuePair> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			protected set
			{
				list_0 = value;
			}
		}

		public bool TryGet(string string_0, out KeyValuePair keyValuePair_0)
		{
			keyValuePair_0 = null;
			int num = 0;
			while (true)
			{
				if (num < List_0.Count)
				{
					if (string.CompareOrdinal(List_0[num].String_0, string_0) == 0)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			keyValuePair_0 = List_0[num];
			return true;
		}

		public bool HasAny(string string_0, string string_1 = "")
		{
			int num = 0;
			while (true)
			{
				if (num < List_0.Count)
				{
					if (string.CompareOrdinal(List_0[num].String_0, string_0) == 0 || string.CompareOrdinal(List_0[num].String_0, string_1) == 0)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}
	}
}
