using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TeamPlaceUIController : MonoBehaviour
{
	private const float float_0 = 0.2f;

	public UISprite cup;

	public UILabel place;

	public new Animation animation;

	private int int_0;

	private float float_1;

	[CompilerGenerated]
	private static Comparison<NetworkStartTable> comparison_0;

	private void Update()
	{
		float_1 -= Time.deltaTime;
		if (float_1 > 0f)
		{
			return;
		}
		float_1 = 0.2f;
		Player_move_c playerMoveC = HeadUpDisplay.GetPlayerMoveC();
		if (playerMoveC == null || playerMoveC.NetworkStartTable_0 == null)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		List<NetworkStartTable> list = new List<NetworkStartTable>();
		for (int i = 0; i < array.Length; i++)
		{
			NetworkStartTable component = array[i].GetComponent<NetworkStartTable>();
			if (!(component == null) && !(component.Player_move_c_0 == null) && component.PlayerCommandController_0.Int32_1 == playerMoveC.Int32_2)
			{
				list.Add(component);
			}
		}
		list.Sort((NetworkStartTable networkStartTable_0, NetworkStartTable networkStartTable_1) => networkStartTable_1.Int32_4.CompareTo(networkStartTable_0.Int32_4));
		int num = 1;
		for (int j = 0; j < list.Count && !list[j].Equals(playerMoveC.NetworkStartTable_0); j++)
		{
			num++;
		}
		setPlace(num);
	}

	private void setPlace(int int_1)
	{
		if (int_1 >= 1 && int_1 <= 3)
		{
			if (!cup.gameObject.activeSelf)
			{
				cup.gameObject.SetActive(true);
			}
			cup.String_0 = string.Format("icon_place_{0}", int_1);
		}
		else if (cup.gameObject.activeSelf)
		{
			cup.gameObject.SetActive(false);
		}
		place.String_0 = int_1.ToString();
		if (!(animation != null))
		{
			return;
		}
		if (int_1 == 1)
		{
			if (int_0 != int_1)
			{
				animation.gameObject.SetActive(true);
				animation.enabled = true;
			}
		}
		else
		{
			if (animation.gameObject.activeSelf)
			{
				animation.gameObject.SetActive(false);
			}
			animation.enabled = false;
		}
		int_0 = int_1;
	}
}
