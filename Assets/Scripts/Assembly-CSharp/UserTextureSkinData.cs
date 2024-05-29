using ProtoBuf;

[ProtoContract]
public sealed class UserTextureSkinData
{
	public enum SkinType
	{
		SKIN_PERS = 1,
		SKIN_CAPE = 2
	}

	[ProtoMember(1)]
	public string string_0;

	[ProtoMember(2)]
	public SkinType skinType_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public byte[] byte_0;

	[ProtoMember(5)]
	public int int_0;
}
