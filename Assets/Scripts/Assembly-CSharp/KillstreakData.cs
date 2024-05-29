using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public class KillstreakData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private KillStreakType killStreakType_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[ProtoMember(1)]
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

	[ProtoMember(2)]
	public KillStreakType KillStreakType_0
	{
		[CompilerGenerated]
		get
		{
			return killStreakType_0;
		}
		[CompilerGenerated]
		set
		{
			killStreakType_0 = value;
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
