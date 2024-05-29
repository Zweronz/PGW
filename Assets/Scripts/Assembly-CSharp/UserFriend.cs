using ProtoBuf;

[ProtoContract]
public sealed class UserFriend
{
	[ProtoMember(1)]
	public string string_0;

	[ProtoMember(2)]
	public int int_0;
}
