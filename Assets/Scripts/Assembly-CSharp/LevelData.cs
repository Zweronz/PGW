using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class LevelData
{
	private ObscuredFloat obscuredFloat_0;

	private ObscuredFloat obscuredFloat_1;

	[ProtoMember(7)]
	public int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private MoneyType moneyType_0;

	[CompilerGenerated]
	private int int_3;

	[ProtoMember(1)]
	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	[ProtoMember(2)]
	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	[ProtoMember(3)]
	public MoneyType MoneyType_0
	{
		[CompilerGenerated]
		get
		{
			return moneyType_0;
		}
		[CompilerGenerated]
		set
		{
			moneyType_0 = value;
		}
	}

	[ProtoMember(4)]
	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	[ProtoMember(5)]
	public float Single_0
	{
		get
		{
			return obscuredFloat_0;
		}
		set
		{
			obscuredFloat_0 = value;
		}
	}

	[ProtoMember(6)]
	public float Single_1
	{
		get
		{
			return obscuredFloat_1;
		}
		set
		{
			obscuredFloat_1 = value;
		}
	}
}
