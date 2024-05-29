using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.network;

internal sealed class CoinBonus : MonoBehaviour
{
	public AudioClip CoinItemUpAudioClip;

	private Player_move_c player_move_c_0;

	private GameObject gameObject_0;

	private static Action action_0;

	public static event Action StartBlinkShop
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action)Delegate.Remove(action_0, value);
		}
	}

	public void SetPlayer()
	{
		player_move_c_0 = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
		gameObject_0 = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (player_move_c_0 == null || gameObject_0 == null || !(Vector3.Distance(base.transform.position, gameObject_0.transform.position) < 1.5f))
		{
			return;
		}
		try
		{
			AbstractNetworkCommand.Send<AddMoneyBonusNetworkCommand>();
			if (!player_move_c_0.Boolean_9)
			{
				string[] array = Storager.GetString(Defs.String_8, string.Empty).Split('#');
				List<string> list = new List<string>();
				string[] array2 = array;
				foreach (string item in array2)
				{
					list.Add(item);
				}
				if (!list.Contains(Application.loadedLevelName))
				{
					list.Add(Application.loadedLevelName);
					Storager.SetString(Defs.String_8, string.Join("#", list.ToArray()));
				}
			}
			if (player_move_c_0 != null && Defs.Boolean_0 && CoinItemUpAudioClip != null)
			{
				player_move_c_0.gameObject.GetComponent<AudioSource>().PlayOneShot(CoinItemUpAudioClip);
			}
			DependSceneEvent<EventTakenCoinBonus>.GlobalDispatch();
		}
		finally
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
