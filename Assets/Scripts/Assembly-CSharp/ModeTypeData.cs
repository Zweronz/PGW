using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(ModeType))]
[ProtoContract]
public sealed class ModeTypeData
{
	[CompilerGenerated]
	private ModeType modeType_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[ProtoMember(1)]
	public ModeType ModeType_0
	{
		[CompilerGenerated]
		get
		{
			return modeType_0;
		}
		[CompilerGenerated]
		set
		{
			modeType_0 = value;
		}
	}

	[ProtoMember(2)]
	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	[ProtoMember(3)]
	public int Int32_1
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

	[ProtoMember(4)]
	public int Int32_2
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
}
