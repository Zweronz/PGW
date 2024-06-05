using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class BuyArtikulNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public bool bool_0;

	[ProtoMember(4, IsRequired = true)]
	public int int_2;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("BuyArtikulNetworkCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("BuyArtikulNetworkCommand::Answered > OK. Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	public void Spoof()
	{
		int id = int_1;
		UnityEngine.PlayerPrefs.SetInt("Bought_WeaponID" + id, UnityEngine.PlayerPrefs.GetInt("Bought_WeaponID" + id, 0) + 1);
		if (!UserController.userArtikuls.ContainsKey(id))
			UserController.userArtikuls.Add( id, new UserArtikul{ int_0 = id, int_1 = UnityEngine.PlayerPrefs.GetInt("Bought_WeaponID" + id, 0) } );
		else
			UserController.userArtikuls[id].int_1 = UnityEngine.PlayerPrefs.GetInt("Bought_WeaponID" + id, 0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(BuyArtikulNetworkCommand), 114);
	}
}
