using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class PresetMatchMakingData
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;

	[ProtoMember(4)]
	public int int_3;

	public bool CheckRating(int int_4)
	{
		return int_4 >= int_1 && int_4 <= int_2;
	}
}
