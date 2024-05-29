using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public class GotToNextLevel : MonoBehaviour
{
	private Action action_0;

	private GameObject gameObject_0;

	private Player_move_c player_move_c_0;

	private bool bool_0;

	[CompilerGenerated]
	private static Action action_1;

	private void Awake()
	{
		action_0 = delegate
		{
			gameObject_0 = GameObject.FindGameObjectWithTag("Player");
			player_move_c_0 = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
		};
		Initializer.PlayerAddedEvent += action_0;
	}

	private void OnDestroy()
	{
		Initializer.PlayerAddedEvent -= action_0;
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetEnablePerfectLabel(false);
		}
	}

	private void Update()
	{
		if (!(gameObject_0 == null) && !(player_move_c_0 == null) && !bool_0 && Vector3.SqrMagnitude(base.transform.position - gameObject_0.transform.position) < 2.25f)
		{
			bool_0 = true;
			GoToNextLevel();
		}
	}

	public static void GoToNextLevel()
	{
		if (TutorialController.TutorialController_0 != null)
		{
			TutorialController.TutorialController_0.HideWaypointArrow();
		}
		AutoFade.FadeAction(2f, 0f, Color.white, delegate
		{
			MonoSingleton<FightController>.Prop_0.LeaveRoom();
		});
	}
}
