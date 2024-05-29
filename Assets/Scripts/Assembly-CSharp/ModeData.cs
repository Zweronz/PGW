using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;
using engine.unity;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class ModeData
{
	private Dictionary<string, MapBonusPoint> dictionary_0 = new Dictionary<string, MapBonusPoint>();

	//modeId
	[CompilerGenerated]
	private int int_0;

	//mapId
	[CompilerGenerated]
	private int int_1;

	//modeType
	[CompilerGenerated]
	private ModeType modeType_0;

	//time
	[CompilerGenerated]
	private int int_2;

	//default max players(?)
	[CompilerGenerated]
	private int int_3;

	//min score for reward
	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private Dictionary<int, ModeRewardData> dictionary_1;

	//survival only (?)
	[CompilerGenerated]
	private bool bool_0;

	//appear order
	[CompilerGenerated]
	private int int_5;

	//something survival related
	[CompilerGenerated]
	private bool bool_1;

	//monster count?
	[CompilerGenerated]
	private int int_6;

	//something else monster count related
	[CompilerGenerated]
	private int int_7;

	//is preset
	[CompilerGenerated]
	private bool bool_2;

	//something admin related???????
	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private List<MapBonusPoint> list_0;

	//is mode?
	[CompilerGenerated]
	private bool bool_4;

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

	[ProtoMember(5)]
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

	[ProtoMember(6)]
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

	[ProtoMember(7)]
	public Dictionary<int, ModeRewardData> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_1;
		}
		[CompilerGenerated]
		set
		{
			dictionary_1 = value;
		}
	}

	[ProtoMember(8)]
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

	[ProtoMember(9)]
	public int Int32_5
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	[ProtoMember(10)]
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

	[ProtoMember(11)]
	public int Int32_6
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	[ProtoMember(12)]
	public int Int32_7
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		set
		{
			int_7 = value;
		}
	}

	[ProtoMember(13)]
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

	[ProtoMember(14)]
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

	[ProtoMember(15)]
	public List<MapBonusPoint> List_0
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

	public bool Boolean_4
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	public ModePopularityType ModePopularityType_0
	{
		get
		{
			return MonoSingleton<FightController>.Prop_0.FightRoomsController_0.GetPopularityType(this);
		}
	}

	public MapBonusPoint GetMapBonusPoint(string string_0)
	{
		return BonusMapController.GetMapBonusPoint(string_0, dictionary_0, List_0, Int32_0, false);
	}
}
