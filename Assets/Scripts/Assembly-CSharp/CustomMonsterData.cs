using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public sealed class CustomMonsterData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private NeedsData needsData_0;

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
	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	[ProtoMember(3)]
	public NeedsData NeedsData_0
	{
		[CompilerGenerated]
		get
		{
			return needsData_0;
		}
		[CompilerGenerated]
		set
		{
			needsData_0 = value;
		}
	}
}
