using ProtoBuf;

[ProtoContract]
public sealed class UserRankData
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;
}
