using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[ProtoContract]
[StorageDataKey(typeof(int))]
public sealed class WearData
{
	//id
	[CompilerGenerated]
	private int int_0;

	//skin texture name
	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private bool bool_0;

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

	public bool Boolean_1
	{
		get
		{
			return ArtikulData_0.SlotType_0 == SlotType.SLOT_WEAR_SKIN;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return ArtikulData_0.SlotType_0 == SlotType.SLOT_WEAR_CAPE;
		}
	}

	public ArtikulData ArtikulData_0
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetArtikul(Int32_0);
		}
	}
}
