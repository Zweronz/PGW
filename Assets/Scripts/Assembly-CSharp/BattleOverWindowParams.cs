using System.Collections.Generic;
using engine.unity;

public sealed class BattleOverWindowParams : WindowShowParameters
{
	public int int_0;

	public int int_1;

	public string string_0;

	public int int_2;

	public EndFightNetworkCommand.IsWinState isWinState_0 = EndFightNetworkCommand.IsWinState.Win;

	public EndFightNetworkCommand.IsWinState isWinState_1 = EndFightNetworkCommand.IsWinState.Win;

	public bool bool_0;

	public List<BattleOverPlayerData> list_0;

	public List<BattleOverPlayerData> list_1;
}
