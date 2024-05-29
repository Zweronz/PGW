using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public class MapBonusData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private List<MapBonusWeightItemData> list_0;

	[ProtoMember(1)]
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

	[ProtoMember(2)]
	public List<MapBonusWeightItemData> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public MapBonusItemData GetItem()
	{
		return BonusMapController.GetItem(List_0);
	}
}
