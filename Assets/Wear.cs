using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wear")]
public class Wear : ScriptableObject
{
	[System.Serializable]
	public class WearData
	{
		public int id;
		
		public string skinName;

		public global::WearData ToWearData()
		{
			return new global::WearData
			{
				Int32_0 = id,
				String_0 = skinName,

				Boolean_0 = true
			};
		}
	}

	public List<WearData> wear = new List<WearData>();
}
