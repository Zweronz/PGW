using ProtoBuf;

[ProtoContract]
public sealed class UserArtikul
{
	public enum UserArticulStatus
	{
		STATE_NORMAL = 0,
		STATE_TESTING = 1
	}

	[ProtoMember(1)]
	public string string_0;

	[ProtoMember(2)]
	public int int_0;

	[ProtoMember(3)]
	public int int_1;

	[ProtoMember(4)]
	public double double_0;

	[ProtoMember(5)]
	public int int_2;

	[ProtoMember(6)]
	public int int_3;

	public ArtikulData ArtikulData_0
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetArtikul(int_0);
		}
	}
}