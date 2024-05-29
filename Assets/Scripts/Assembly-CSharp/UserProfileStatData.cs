using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class UserProfileStatData
{
	[ProtoMember(1)]
	public Dictionary<ModeType, int> dictionary_0;

	[ProtoMember(2)]
	public float float_0;

	[ProtoMember(3)]
	public int int_0;

	[ProtoMember(4)]
	public int int_1;

	[ProtoMember(5)]
	public float float_1;

	[ProtoMember(6)]
	public Dictionary<ModeType, int> dictionary_1;

	[ProtoMember(7)]
	public float float_2;

	[ProtoMember(8)]
	public int int_2;

	[ProtoMember(9)]
	public int int_3;

	[ProtoMember(10)]
	public int int_4;

	[ProtoMember(11)]
	public int int_5;

	public int GetCountKillsInRegim(ModeType modeType_0)
	{
		int value = 0;
		dictionary_0.TryGetValue(modeType_0, out value);
		return value;
	}

	public int GetCountWinsInRegim(ModeType modeType_0)
	{
		int value = 0;
		dictionary_1.TryGetValue(modeType_0, out value);
		return value;
	}
}
