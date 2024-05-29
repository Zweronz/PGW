using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public class PaymentNotificationData : NotificationViewData
{
	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

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
}
