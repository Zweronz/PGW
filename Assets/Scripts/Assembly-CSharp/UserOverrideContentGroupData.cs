using ProtoBuf;

[ProtoContract]
public sealed class UserOverrideContentGroupData
{
	[ProtoMember(1, IsRequired = true)]
	public int int_0;

	[ProtoMember(2, IsRequired = true)]
	public int int_1;

	[ProtoMember(3, IsRequired = true)]
	public int int_2;

	[ProtoMember(4)]
	public int int_3;

	[ProtoMember(5)]
	public int int_4;

	public OverrideContentGroupChangeType IsChange(UserOverrideContentGroupData userOverrideContentGroupData_0)
	{
		if (int_0 != userOverrideContentGroupData_0.int_0)
		{
			return OverrideContentGroupChangeType.Error;
		}
		if (int_3 != userOverrideContentGroupData_0.int_3)
		{
			return OverrideContentGroupChangeType.All;
		}
		return (int_1 == userOverrideContentGroupData_0.int_1 && int_2 == userOverrideContentGroupData_0.int_2 && int_4 == userOverrideContentGroupData_0.int_4) ? OverrideContentGroupChangeType.No : OverrideContentGroupChangeType.OnlyStock;
	}
}
