using ProtoBuf;

[ProtoContract]
public sealed class ContentGroupDataItem
{
	[ProtoMember(1, IsRequired = true)]
	public ContentGroupItemType contentGroupItemType_0;

	[ProtoMember(2, IsRequired = true)]
	public int int_0;
}
