using System;
using System.Collections;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

internal sealed class BotHealth : MonoBehaviour
{
	public static bool bool_0;

	private bool bool_1 = true;

	public Texture hitTexture;

	private BotAI botAI_0;

	private Player_move_c player_move_c_0;

	private bool bool_2;

	private GameObject gameObject_0;

	private MonsterParams monsterParams_0;

	private Texture texture_0;

	private bool bool_3;

	private void Awake()
	{
		if (Defs.bool_4)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				gameObject_0 = transform.gameObject;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		monsterParams_0 = gameObject_0.GetComponent<MonsterParams>();
		if (monsterParams_0 == null)
		{
			Log.AddLine(string.Format("[BotHealth::Start()] monster do not have Component MonsterParams !!! name = {0}", base.gameObject.name), Log.LogLevel.ERROR);
			return;
		}
		botAI_0 = GetComponent<BotAI>();
		player_move_c_0 = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
		if (base.gameObject.name.IndexOf("Boss") == -1)
		{
			Texture2D monsterSkinTexture = MonstersController.GetMonsterSkinTexture(monsterParams_0.monsterId);
			if (monsterSkinTexture != null)
			{
				Utility.SetTextureRecursiveFrom(gameObject_0, monsterSkinTexture);
			}
			texture_0 = monsterSkinTexture;
		}
		else
		{
			Renderer componentInChildren = gameObject_0.GetComponentInChildren<Renderer>();
			texture_0 = componentInChildren.material.mainTexture;
		}
		if (!(texture_0 == null))
		{
		}
	}

	private IEnumerator resetHurtAudio(float float_0)
	{
		bool_0 = true;
		yield return new WaitForSeconds(float_0);
		bool_0 = false;
	}

	public bool RequestPlayHurt(float float_0)
	{
		if (bool_0)
		{
			return false;
		}
		StartCoroutine(resetHurtAudio(float_0));
		return true;
	}

	private IEnumerator Flash()
	{
		if (texture_0 != null)
		{
			bool_2 = true;
			Utility.SetTextureRecursiveFrom(gameObject_0, hitTexture);
			yield return new WaitForSeconds(0.125f);
			Utility.SetTextureRecursiveFrom(gameObject_0, texture_0);
			bool_2 = false;
		}
	}

	public void adjustHealth(int int_0, float float_0, Transform transform_0)
	{
		if (float_0 < 0f && !bool_2)
		{
			StartCoroutine(Flash());
		}
		if (float_0 > 0f)
		{
			Log.AddLine(string.Format("[BotHealth::adjustHealth] damage = {0} (лечим моба при атаке!!!)", float_0), Log.LogLevel.WARNING);
			float_0 = 0f;
		}
		monsterParams_0.changeHealth(float_0);
		OnDamageObtained(int_0, 0f - float_0);
		if (monsterParams_0.Single_0 == 0f)
		{
			bool_1 = false;
			DependSceneEvent<EventDeathEnemy, GameObject>.GlobalDispatch(base.gameObject);
		}
		else
		{
			LocalUserData localUserData_ = UserController.UserController_0.UserData_0.localUserData_0;
			localUserData_.ObscuredInt_0 = (int)localUserData_.ObscuredInt_0 + monsterParams_0.Int32_1;
		}
		AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.HURT);
		if (monsterAudioClip != null && RequestPlayHurt(monsterAudioClip.length) && Defs.Boolean_0)
		{
			base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
		}
		if ((transform_0.CompareTag("Player") && !transform_0.GetComponent<SkinName>().Player_move_c_0.Boolean_14) || transform_0.CompareTag("Turret"))
		{
			botAI_0.SetTarget(transform_0, true);
		}
	}

	public bool getIsLife()
	{
		return bool_1;
	}

	private void OnDamageObtained(int int_0, float float_0)
	{
		if (int_0 == 0)
		{
			return;
		}
		Player_move_c player_move_c = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			PhotonView photonView = PhotonView.Get(gameObject);
			if (photonView != null && photonView.PhotonPlayer_0 != null && photonView.PhotonPlayer_0.Hashtable_0 != null && photonView.PhotonPlayer_0.Hashtable_0.ContainsKey("uid") && (int)photonView.PhotonPlayer_0.Hashtable_0["uid"] == int_0)
			{
				player_move_c = gameObject.GetComponent<SkinName>().Player_move_c_0;
				break;
			}
		}
		if (!(player_move_c == null))
		{
			int int_ = ((!(player_move_c.WeaponSounds_0 == null)) ? player_move_c.WeaponSounds_0.WeaponData_0.Int32_0 : 0);
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnHit(int_0, int_, false, float_0);
		}
	}
}
