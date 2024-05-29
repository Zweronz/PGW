using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public class ClanLevelData
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;
}
