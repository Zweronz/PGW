using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class WaveMonstersData
{
	private Dictionary<string, MapBonusPoint> dictionary_0 = new Dictionary<string, MapBonusPoint>();

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	[CompilerGenerated]
	private float float_3;

	[CompilerGenerated]
	private float float_4;

	[CompilerGenerated]
	private float float_5;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private List<CustomMonsterData> list_0;

	[CompilerGenerated]
	private List<int> list_1;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private List<MapBonusPoint> list_2;

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

	[ProtoMember(5)]
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

	[ProtoMember(6)]
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

	[ProtoMember(7)]
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

	[ProtoMember(8)]
	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		set
		{
			float_2 = value;
		}
	}

	[ProtoMember(9)]
	public float Single_3
	{
		[CompilerGenerated]
		get
		{
			return float_3;
		}
		[CompilerGenerated]
		set
		{
			float_3 = value;
		}
	}

	[ProtoMember(10)]
	public float Single_4
	{
		[CompilerGenerated]
		get
		{
			return float_4;
		}
		[CompilerGenerated]
		set
		{
			float_4 = value;
		}
	}

	[ProtoMember(11)]
	public float Single_5
	{
		[CompilerGenerated]
		get
		{
			return float_5;
		}
		[CompilerGenerated]
		set
		{
			float_5 = value;
		}
	}

	[ProtoMember(12)]
	public int Int32_3
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

	[ProtoMember(13)]
	public List<CustomMonsterData> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	[ProtoMember(14)]
	public List<int> List_1
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	[ProtoMember(15)]
	public int Int32_4
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	[ProtoMember(16)]
	public List<MapBonusPoint> List_2
	{
		[CompilerGenerated]
		get
		{
			return list_2;
		}
		[CompilerGenerated]
		set
		{
			list_2 = value;
		}
	}

	public MapBonusPoint GetMapBonusPoint(string string_0)
	{
		return BonusMapController.GetMapBonusPoint(string_0, dictionary_0, List_2, Int32_0, true);
	}
}
