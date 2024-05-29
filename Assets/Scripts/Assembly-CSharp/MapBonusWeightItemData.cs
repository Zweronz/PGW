using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public sealed class MapBonusWeightItemData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

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

	public MapBonusItemData MapBonusItemData_0
	{
		get
		{
			return MapBonusItemStorage.Get.Storage.GetObjectByKey(Int32_0);
		}
	}
}
