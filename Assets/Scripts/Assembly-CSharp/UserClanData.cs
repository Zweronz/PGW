using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class UserClanData
{
	[ProtoMember(1)]
	public string string_0;

	[ProtoMember(2)]
	public int int_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public string string_2;

	[ProtoMember(5)]
	public byte[] byte_0;

	[ProtoMember(6)]
	public int int_1;

	[ProtoMember(7)]
	public int int_2;

	[ProtoMember(8)]
	public int int_3;

	[ProtoMember(9)]
	public List<UserClanMemberData> list_0;

	[ProtoMember(10)]
	public int int_4;

	[ProtoMember(11)]
	public bool bool_0;

	public bool IsMemderOfClan(int int_5)
	{
		int num = 0;
		while (true)
		{
			if (num < list_0.Count)
			{
				if (list_0[num].int_0 == int_5)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}
}
