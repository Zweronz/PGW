using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[ProtoContract]
[StorageDataKey(typeof(int))]
public class KillTypeData
{
	[CompilerGenerated]
	private int int_0;

	//localization key
	[CompilerGenerated]
	private string string_0;

	//sprite
	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private DeadType deadType_0;

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

	[ProtoMember(4)]
	public DeadType DeadType_0
	{
		[CompilerGenerated]
		get
		{
			return deadType_0;
		}
		[CompilerGenerated]
		set
		{
			deadType_0 = value;
		}
	}
}
