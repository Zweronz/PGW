using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public class WarningNotificationData : NotificationViewData
{
	[CompilerGenerated]
	private int int_1;

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
}
