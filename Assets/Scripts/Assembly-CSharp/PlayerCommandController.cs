using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

public sealed class PlayerCommandController
{
	private NetworkStartTable networkStartTable_0;

	[CompilerGenerated]
	private TypeCommand typeCommand_0;

	[CompilerGenerated]
	private TypeCommand typeCommand_1;

	public TypeCommand TypeCommand_0
	{
		[CompilerGenerated]
		get
		{
			return typeCommand_0;
		}
		[CompilerGenerated]
		private set
		{
			typeCommand_0 = value;
		}
	}

	public int Int32_0
	{
		get
		{
			return (int)TypeCommand_0;
		}
		private set
		{
			TypeCommand_0 = (TypeCommand)value;
		}
	}

	public TypeCommand TypeCommand_1
	{
		[CompilerGenerated]
		get
		{
			return typeCommand_1;
		}
		[CompilerGenerated]
		private set
		{
			typeCommand_1 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return (int)TypeCommand_1;
		}
		private set
		{
			TypeCommand_1 = (TypeCommand)value;
		}
	}

	public PlayerCommandController(NetworkStartTable networkStartTable_1)
	{
		networkStartTable_0 = networkStartTable_1;
		ResetCommand();
	}

	public void ResetCommand(bool bool_0 = false)
	{
		TypeCommand_1 = TypeCommand.None;
		if (bool_0)
		{
			SynchCommand();
		}
	}

	public void SynchCommand()
	{
		networkStartTable_0.PhotonView_0.RPC("SynhCommandRPC", PhotonTargets.Others, Int32_1, Int32_0);
	}

	public void SynchCommand(int int_0, int int_1)
	{
		Int32_1 = int_0;
		Int32_0 = int_1;
	}

	public void AutoBalanceCommand()
	{
		ModeData modeData_ = MonoSingleton<FightController>.Prop_0.ModeData_0;
		if (modeData_.ModeType_0 == ModeType.TEAM_FIGHT || modeData_.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			TypeCommand_1 = ((!modeData_.Boolean_3) ? AutoBallance() : AdminCreateFightBallance());
			TypeCommand_0 = TypeCommand_1;
		}
		SynchCommand();
	}

	private TypeCommand AdminCreateFightBallance()
	{
		return UserController.UserController_0.UserData_0.user_0.bool_0 ? TypeCommand.Diggers : TypeCommand.Kritters;
	}

	private TypeCommand AutoBallance()
	{
		TypeCommand typeCommand = TypeCommand.None;
		int num = 0;
		int num2 = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
			num += ((component.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Diggers) ? 1 : 0);
			num2 += ((component.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Kritters) ? 1 : 0);
		}
		if (num != num2 && num + num2 != 0)
		{
			return (num <= num2) ? TypeCommand.Diggers : TypeCommand.Kritters;
		}
		return (TypeCommand)Random.Range(1, 3);
	}
}
