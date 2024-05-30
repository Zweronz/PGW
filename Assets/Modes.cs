using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Modes")]
public class Modes : ScriptableObject
{
	[System.Serializable]
	public class Mode
	{
		public int modeId, mapId;

		public ModeType modeType;

		public int time, maxPlayers, minScore, order, monsterCount;
		
		public bool preset;

		public ModeData ToModeData()
		{
			return new ModeData
			{
				Dictionary_0 = new Dictionary<int, ModeRewardData>(),
				List_0 = new List<MapBonusPoint>(),

				Int32_0 = mapId,
				Int32_1 = mapId,

				Int32_2 = time,
				Int32_3 = maxPlayers,
				Int32_4 = minScore,
				Int32_5 = order,
				Int32_6 = monsterCount,

				Boolean_1 = true,
				Boolean_2 = preset,
			};
		}
	}

	public List<Mode> modes = new List<Mode>();
}
