using System.Collections.Generic;
using ProtoBuf;
using engine.data;

[ProtoContract]
[StorageDataKey(typeof(int))]
public sealed class BonusData
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;

	[ProtoMember(4)]
	public List<BonusItemData> list_0;

	[ProtoMember(5)]
	public NeedsData needsData_0;
}
