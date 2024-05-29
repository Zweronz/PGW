using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class MapData
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private MapSize mapSize_0;

	[CompilerGenerated]
	private string string_2;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private bool bool_3;

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
	public MapSize MapSize_0
	{
		[CompilerGenerated]
		get
		{
			return mapSize_0;
		}
		[CompilerGenerated]
		set
		{
			mapSize_0 = value;
		}
	}

	[ProtoMember(5)]
	public string String_2
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	[ProtoMember(6)]
	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	[ProtoMember(7)]
	public bool Boolean_1
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

	[ProtoMember(8)]
	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	public bool Boolean_3
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		set
		{
			bool_3 = value;
		}
	}
}
