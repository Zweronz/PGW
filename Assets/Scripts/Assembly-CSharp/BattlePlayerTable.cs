using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerTable : MonoBehaviour
{
	public UIGrid grid;

	public BattlePlayerItem itemTemp;

	public int amount;

	private List<BattlePlayerItem> list_0 = new List<BattlePlayerItem>();

	private void Awake()
	{
		itemTemp.transform.position = Vector3.zero;
		NGUITools.SetActive(itemTemp.gameObject, false);
		for (int i = 0; i < amount; i++)
		{
			GameObject gameObject = NGUITools.AddChild(grid.gameObject, itemTemp.gameObject);
			gameObject.name = string.Format("{0:00}", i);
			BattlePlayerItem component = gameObject.GetComponent<BattlePlayerItem>();
			list_0.Add(component);
			NGUITools.SetActive(gameObject, true);
		}
	}

	public void SetData(List<BattlePlayerData> list_1)
	{
		for (int i = 0; i < amount; i++)
		{
			BattlePlayerItem battlePlayerItem = list_0[i];
			if (i < list_1.Count)
			{
				battlePlayerItem.SetData(list_1[i]);
			}
			else
			{
				battlePlayerItem.SetData(null);
			}
		}
		grid.Reposition();
	}
}
