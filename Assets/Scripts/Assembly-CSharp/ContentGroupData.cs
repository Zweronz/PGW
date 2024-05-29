using System.Collections.Generic;
using ProtoBuf;
using engine.data;

[ProtoContract]
[StorageDataKey(typeof(int))]
public sealed class ContentGroupData
{
	[ProtoMember(1, IsRequired = true)]
	public int int_0;

	[ProtoMember(2)]
	public List<ContentGroupDataItem> list_0;

	private Dictionary<ContentGroupItemType, List<ContentGroupDataItem>> dictionary_0 = new Dictionary<ContentGroupItemType, List<ContentGroupDataItem>>();

	public List<ContentGroupDataItem> GetItemsByType(ContentGroupItemType contentGroupItemType_0)
	{
		List<ContentGroupDataItem> value = null;
		dictionary_0.TryGetValue(contentGroupItemType_0, out value);
		return value;
	}

	public void InitIndex()
	{
		if (list_0 == null || dictionary_0.Count > 0)
		{
			return;
		}
		for (int i = 0; i < list_0.Count; i++)
		{
			ContentGroupDataItem contentGroupDataItem = list_0[i];
			if (!dictionary_0.ContainsKey(contentGroupDataItem.contentGroupItemType_0))
			{
				dictionary_0.Add(contentGroupDataItem.contentGroupItemType_0, new List<ContentGroupDataItem>());
			}
			dictionary_0[contentGroupDataItem.contentGroupItemType_0].Add(contentGroupDataItem);
		}
	}
}
