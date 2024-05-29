using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class SkillData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private NeedsData needsData_0;

	[CompilerGenerated]
	private string string_1;

	[ProtoMember(1, DataFormat = DataFormat.ZigZag)]
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

	[ProtoMember(2, DataFormat = DataFormat.ZigZag)]
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

	[ProtoMember(3)]
	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		set
		{
			float_0 = value;
		}
	}

	[ProtoMember(4)]
	public float Single_1
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		set
		{
			float_1 = value;
		}
	}

	[ProtoMember(5)]
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

	[ProtoMember(6)]
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

	[ProtoMember(7)]
	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public bool CheckNeeds(NeedData needData_0)
	{
		if (NeedsData_0 == null)
		{
			needData_0 = null;
			return true;
		}
		return NeedsData_0.Check(out needData_0);
	}
}
