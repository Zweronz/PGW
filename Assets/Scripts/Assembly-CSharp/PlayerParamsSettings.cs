using System.Reflection;
using ProtoBuf;

[ProtoContract]
[Obfuscation(Exclude = true)]
public class PlayerParamsSettings
{
	[ProtoMember(1)]
	public float PlayerForwardSpeed;

	[ProtoMember(2)]
	public float PlayerBackwardSpeed;

	[ProtoMember(3)]
	public float PlayerSideStepSpeed;

	[ProtoMember(4)]
	public float PlayerJumpSpeed;

	[ProtoMember(5)]
	public int PlayerFOV;

	[ProtoMember(6)]
	public int GunFOV;

	[ProtoMember(7)]
	public float ForwardAcc;

	[ProtoMember(8)]
	public float ForwardDeacc;

	[ProtoMember(9)]
	public float BackwardAcc;

	[ProtoMember(10)]
	public float BackwardDeacc;

	[ProtoMember(11)]
	public float SideAcc;

	[ProtoMember(12)]
	public float SideDeacc;

	static PlayerParamsSettings()
	{
		Get = new PlayerParamsSettings
		{
			PlayerForwardSpeed = 1f,
			PlayerBackwardSpeed = 1f,
			PlayerSideStepSpeed = 1f,
			PlayerJumpSpeed = 7f,
			ForwardAcc = 0f,
			BackwardAcc = 0f,
			PlayerFOV = 75,
			GunFOV = 75,
		};
	}

	public static PlayerParamsSettings Get { get; private set; }
}
