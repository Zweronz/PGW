using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(AdditionalPurchaseType))]
[ProtoContract]
public sealed class AdditionalPurchaseData
{
	[ProtoMember(1)]
	public AdditionalPurchaseType additionalPurchaseType_0;

	[ProtoMember(2)]
	public int int_0;

	[ProtoMember(3)]
	public int int_1;
}
