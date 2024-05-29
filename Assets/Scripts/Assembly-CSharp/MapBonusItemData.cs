using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class MapBonusItemData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private NeedsData needsData_0;

	[CompilerGenerated]
	private Dictionary<SkillId, SkillData> dictionary_0;

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

	[ProtoMember(4)]
	public Dictionary<SkillId, SkillData> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_0;
		}
		[CompilerGenerated]
		set
		{
			dictionary_0 = value;
		}
	}
}
