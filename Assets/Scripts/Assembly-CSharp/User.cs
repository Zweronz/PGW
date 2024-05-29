using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class User
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public bool bool_0;

	[ProtoMember(5)]
	public double double_0;

	[ProtoMember(6)]
	public int int_1;

	[ProtoMember(7)]
	public int int_2;

	[ProtoMember(8)]
	public Dictionary<MoneyType, int> dictionary_0;

	[ProtoMember(9)]
	public int int_3;

	[ProtoMember(10)]
	public bool bool_1;

	[ProtoMember(11)]
	public int int_4;

	[ProtoMember(12)]
	public int int_5;

	[ProtoMember(13)]
	public bool bool_2;

	[ProtoMember(14)]
	public int int_6;

	[ProtoMember(15)]
	public bool bool_3;

	[ProtoMember(16)]
	public bool bool_4;

	[ProtoMember(17)]
	public bool bool_5;

	[ProtoMember(18)]
	public int int_7;

	public float float_0;
}
