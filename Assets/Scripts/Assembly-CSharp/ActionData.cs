using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class ActionData
{
	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public string string_2;

	[ProtoMember(8)]
	public string string_3;

	[ProtoMember(9)]
	public string string_4;

	[ProtoMember(10)]
	public int int_1;

	[ProtoMember(12)]
	public StockWndType stockWndType_0;

	[ProtoMember(13)]
	public StockAligmentType stockAligmentType_0;

	[ProtoMember(14)]
	public int int_2;

	[ProtoMember(15)]
	public bool bool_0;

	[CompilerGenerated]
	private bool bool_1;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}
}
