using ProtoBuf;

[ProtoContract]
public sealed class UserTimerData
{
	public enum UserTimerType
	{
		USER_TIMER_GACHA = 1
	}

	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public double double_0;
}
