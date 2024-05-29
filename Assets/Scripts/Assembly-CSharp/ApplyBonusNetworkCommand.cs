using System.Collections.Generic;
using ProtoBuf;
using engine.events;
using engine.network;

[ProtoContract]
public class ApplyBonusNetworkCommand : AbstractNetworkCommand
{
	public enum BonusType
	{
		NONE = 0,
		TUTORIAL_COMPLETE = 1,
		MAIL_VALIDITY = 2,
		GACHA = 3
	}

	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public Dictionary<MoneyType, int> dictionary_0;

	[ProtoMember(4)]
	public Dictionary<int, int> dictionary_1;

	[ProtoMember(5)]
	public BonusType bonusType_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Run()
	{
		base.Run();
		DependSceneEvent<EventApplyBonus, ApplyBonusNetworkCommand>.GlobalDispatch(this);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ApplyBonusNetworkCommand), 123);
	}
}
