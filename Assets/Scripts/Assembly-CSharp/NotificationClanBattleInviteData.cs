using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public class NotificationClanBattleInviteData : NotificationViewData
{
	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private int int_3;

	[ProtoMember(1)]
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

	[ProtoMember(2)]
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

	[ProtoMember(3)]
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

	[ProtoMember(4)]
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
}
