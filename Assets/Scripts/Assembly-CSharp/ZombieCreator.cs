using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using pixelgun.tutorial;

public sealed class ZombieCreator : MonoBehaviour
{
	public enum StateNextWaveType
	{
		Auto = 0,
		Manual = 1
	}

	public static ZombieCreator zombieCreator_0;

	public GUIStyle labelStyle;

	public bool bossShowm;

	public bool stopGeneratingBonuses;

	private GameObject[] gameObject_0;

	private GameObject[] gameObject_1;

	private AudioClip audioClip_0;

	private GameObject gameObject_2;

	private PauseONGuiDrawer pauseONGuiDrawer_0;

	private bool bool_0;

	private bool bool_1;

	private int int_0;

	private int int_1;

	private float float_0 = 10f;

	private int[] int_2 = new int[3] { 4, 3, 2 };

	private int int_3;

	private int int_4;

	private int int_5;

	private int int_6;

	private int int_7;

	private static Action action_0;

	private static Action action_1;

	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private StateNextWaveType stateNextWaveType_0;

	private static int Int32_0
	{
		get
		{
			return MonstersController.Int32_0;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		set
		{
			int_8 = value;
		}
	}

	public StateNextWaveType StateNextWaveType_0
	{
		[CompilerGenerated]
		get
		{
			return stateNextWaveType_0;
		}
		[CompilerGenerated]
		set
		{
			stateNextWaveType_0 = value;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return Int32_0 - Int32_2 == 1 && !bossShowm;
		}
	}

	public int Int32_2
	{
		get
		{
			return int_0;
		}
		set
		{
			setNumOfDeadMonster(value);
		}
	}

	public static event Action LastEnemy
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

	public static event Action BossKilled
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_1 = (Action)Delegate.Combine(action_1, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_1 = (Action)Delegate.Remove(action_1, value);
		}
	}

	private void OnDestroy()
	{
		zombieCreator_0 = null;
	}

	private void updateParameters()
	{
		int_3 = 0;
		float_0 = int_2[0];
		ModeData modeData_ = FightOfflineController.FightOfflineController_0.ModeData_0;
		if (modeData_ != null)
		{
			int_4 = modeData_.Int32_7;
			int_5 = modeData_.Int32_6;
		}
	}

	private bool NextWave()
	{
		bool flag;
		if ((flag = StateNextWaveType_0 == StateNextWaveType.Manual) && !MonstersController.Boolean_0)
		{
			return false;
		}
		DependSceneEvent<EventNextEnemyWave, StateNextWaveType>.GlobalDispatch(StateNextWaveType_0);
		if (!flag)
		{
			StartNextWave();
		}
		return true;
	}

	public void StartNextWave()
	{
		UsersData.Dispatch(UsersData.EventType.OFFLINE_WAVES_COMPLETE);
		StartCoroutine(DrawWaveMessage(delegate
		{
			updateParameters();
			int_0 = 0;
			int_1 = 0;
			bool_0 = false;
			stopGeneratingBonuses = false;
			FightOfflineController.FightOfflineController_0.StartNextWave();
			ResetWaveMonstersCounter();
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowArenaBossLabel(false);
			}
		}, (!Defs.bool_0) ? 0f : 0.01f));
		if (Application.loadedLevelName.Equals("Pizza"))
		{
			Vector3 vector = new Vector3(-7.83f, 0.46f, -2.44f);
		}
		else
		{
			Vector3 vector = new Vector3(0f, 1f, 0f);
		}
		if (FightOfflineController.FightOfflineController_0.IsNextWaveBoss() && HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowArenaBossLabel(true);
		}
	}

	private void setNumOfDeadMonster(int int_9)
	{
		int num = int_9 - int_0;
		int_0 = int_9;
		Int32_1 -= num;
		if (!Defs.bool_0)
		{
			int_1 += num;
			if (int_1 == 12)
			{
				if (float_0 > 5f)
				{
					float_0 -= 5f;
				}
				int_1 = 0;
			}
			if (Boolean_0 && action_0 != null)
			{
				action_0();
			}
		}
		if (Defs.bool_0 && Int32_2 == Int32_0 - 1)
		{
			stopGeneratingBonuses = true;
		}
		if (int_0 >= Int32_0 && !NextWave())
		{
			GameObject[] array = gameObject_1;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(true);
			}
		}
	}

	public static void SetLayerRecursively(GameObject gameObject_3, int int_9)
	{
		Utility.SetLayerRecursive(gameObject_3, int_9);
	}

	private void Awake()
	{
		if (!Defs.bool_2)
		{
			StateNextWaveType_0 = StateNextWaveType.Auto;
			stopGeneratingBonuses = false;
			zombieCreator_0 = this;
			if (!Defs.bool_0 && CurrentCampaignGame.Int32_0 != 0)
			{
				audioClip_0 = Resources.Load("Snd/boss_campaign") as AudioClip;
			}
			updateParameters();
			StartCoroutine(_DrawFirstMessage());
		}
	}

	private void Start()
	{
		labelStyle.fontSize = Mathf.RoundToInt(50f * Defs.Single_0);
		labelStyle.font = LocalizationStore.GetFontByLocalize("Key_04B_03");
		if (Defs.bool_2)
		{
			bool_1 = true;
		}
		else
		{
			bool_1 = false;
		}
		gameObject_1 = GameObject.FindGameObjectsWithTag("Portal");
		GameObject[] array = gameObject_1;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(false);
		}
		if (!bool_1)
		{
			gameObject_0 = GameObject.FindGameObjectsWithTag("EnemyCreationZone");
			if (!Defs.bool_0)
			{
				resetInterval();
			}
		}
	}

	private IEnumerator _DrawFirstMessage()
	{
		while (HeadUpDisplay.HeadUpDisplay_0 == null)
		{
			yield return new WaitForEndOfFrame();
		}
		HeadUpDisplay headUpDisplay_ = HeadUpDisplay.HeadUpDisplay_0;
		headUpDisplay_.ArenaWaves.SetActive(TutorialController.TutorialController_0 == null || ((!TutorialController.TutorialController_0.Boolean_0) ? true : false));
		headUpDisplay_.ArenaWavesTxt.gameObject.SetActive(true);
		headUpDisplay_.ArenaWavesNum.String_0 = string.Format("{0}", 1);
		yield return new WaitForSeconds(2f);
		if (headUpDisplay_ != null)
		{
			headUpDisplay_.ArenaWaves.SetActive(false);
		}
	}

	private IEnumerator DrawWaveMessage(Action action_2, float float_1)
	{
		if (float_1 == 0f)
		{
			action_2();
			yield break;
		}
		yield return new WaitForSeconds(float_1);
		while (HeadUpDisplay.HeadUpDisplay_0 == null)
		{
			yield return new WaitForEndOfFrame();
		}
		HeadUpDisplay headUpDisplay_ = HeadUpDisplay.HeadUpDisplay_0;
		headUpDisplay_.ArenaWaves.SetActive(true);
		headUpDisplay_.ArenaWavesTxt.gameObject.SetActive(false);
		for (int num = 5; num > 0; num--)
		{
			headUpDisplay_.ArenaWavesNum.String_0 = num.ToString();
			yield return new WaitForSeconds(1f);
		}
		headUpDisplay_.ArenaWaves.SetActive(false);
		headUpDisplay_.ArenaFirstWave.SetActive(true);
		action_2();
		yield return new WaitForSeconds(1f);
		headUpDisplay_.ArenaFirstWave.SetActive(false);
	}

	private void resetInterval()
	{
		float_0 = Mathf.Max(1f, float_0);
	}

	public void BeganCreateEnemies()
	{
		ResetWaveMonstersCounter();
		StartCoroutine(AddZombies());
	}

	private IEnumerator AddZombies()
	{
		while (true)
		{
			int a = Mathf.Min(int_4, int_5 - Int32_1);
			a = Mathf.Min(a, Int32_0 - (Int32_2 + Int32_1));
			for (int i = 0; i < a; i++)
			{
				int num = NextMonsterInWave();
				GameObject monsterObject = MonstersController.GetMonsterObject(num);
				if (!(monsterObject == null))
				{
					GameObject gameObject = null;
					gameObject = ((!Defs.bool_0) ? gameObject_0[UnityEngine.Random.Range(0, gameObject_0.Length)] : gameObject_0[i % gameObject_0.Length]);
					Vector3 position = createPos(gameObject);
					GameObject gparam_ = (GameObject)UnityEngine.Object.Instantiate(monsterObject, position, Quaternion.identity);
					DependSceneEvent<EventCreateEnemy, GameObject>.GlobalDispatch(gparam_);
					continue;
				}
				Log.AddLine(string.Format("[ZombieCreator::AddZombies ERROR no prefab for MonstersController.GetRandomMonster()= {0}]", num), Log.LogLevel.ERROR);
				break;
			}
			if (Int32_2 + Int32_1 >= Int32_0)
			{
				if (!Defs.bool_0 && !MonstersController.Boolean_0)
				{
					break;
				}
				bool_0 = true;
				do
				{
					yield return new WaitForEndOfFrame();
				}
				while (bool_0);
			}
			else
			{
				yield return new WaitForSeconds(float_0);
			}
			if (Defs.bool_0)
			{
				int_3++;
				if (int_3 >= int_2.Length)
				{
					int_3 = int_2.Length - 1;
				}
				float_0 = int_2[int_3];
			}
		}
		DependSceneEvent<EventLastEnemyDeadInWave>.GlobalDispatch();
	}

	private Vector3 createPos(GameObject gameObject_3)
	{
		BoxCollider component = gameObject_3.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject_3.transform.localScale.x, component.size.z * gameObject_3.transform.localScale.z);
		Rect rect = new Rect(gameObject_3.transform.position.x - vector.x / 2f, gameObject_3.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 result = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), (!Defs.list_0.Contains(CurrentCampaignGame.Int32_0) || Defs.bool_0) ? 0f : gameObject_3.transform.position.y, rect.y + UnityEngine.Random.Range(0f, rect.height));
		return result;
	}

	private int NextMonsterInWave()
	{
		if (int_7 >= int_6)
		{
			return MonstersController.GetRandomMonster();
		}
		return MonstersController.GetMonsterByNumberInWave(int_7++);
	}

	private void ResetWaveMonstersCounter()
	{
		int_7 = 0;
		int_6 = MonstersController.GetMonsterCountInWave();
	}
}
