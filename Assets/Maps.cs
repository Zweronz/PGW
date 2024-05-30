using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Maps")]
public class Maps : ScriptableObject
{
	[System.Serializable]
	public class Map
	{
		public int mapId;

		public string sceneName, localizationKey;

		public MapSize mapSize;

		public string previewTexture;

		public bool online, isNew, survival;

		public MapData ToMapData()
		{
			return new MapData
			{
				Int32_0 = mapId,
				String_0 = sceneName,

				String_1 = localizationKey,
				MapSize_0 = mapSize,

				String_2 = previewTexture,

				Boolean_0 = online,
				Boolean_1 = isNew,
				Boolean_3 = survival
			};
		}
	}

	public List<Map> maps;
}
