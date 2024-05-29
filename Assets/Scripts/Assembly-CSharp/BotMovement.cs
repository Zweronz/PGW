using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

internal sealed class BotMovement : MonoBehaviour
{
	public delegate void DelayedCallback();

	public static bool bool_0;

	private Transform transform_0;

	private float float_0;

	public ZombieCreator _gameController;

	private bool bool_1;

	private bool bool_2;

	private Player_move_c player_move_c_0;

	private GameObject gameObject_0;

	private float float_1;

	private string string_0 = "Idle";

	private string string_1 = "Norm_Walk";

	private string string_2 = "Zombie_Walk";

	private string string_3 = "Zombie_Off";

	private string string_4 = "Zombie_Dead";

	private string string_5 = "Zombie_On";

	private string string_6 = "Zombie_Attack";

	private string string_7;

	private GameObject gameObject_1;

	private MonsterParams monsterParams_0;

	private bool bool_3;

	private UnityEngine.AI.NavMeshAgent navMeshAgent_0;

	private BoxCollider boxCollider_0;

	private Transform transform_1;

	private IEnumerator resetDeathAudio(float float_2)
	{
		bool_0 = true;
		yield return new WaitForSeconds(float_2);
		bool_0 = false;
	}

	public bool RequestPlayDeath(float float_2)
	{
		if (bool_0)
		{
			return false;
		}
		StartCoroutine(resetDeathAudio(float_2));
		return true;
	}

	private void Awake()
	{
		if (Defs.bool_4)
		{
			base.enabled = false;
			return;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				gameObject_1 = transform.gameObject;
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
	}

	private void Start()
	{
		transform_1 = base.transform;
		navMeshAgent_0 = GetComponent<UnityEngine.AI.NavMeshAgent>();
		boxCollider_0 = gameObject_1.GetComponent<BoxCollider>();
		string_7 = string_3;
		player_move_c_0 = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
		gameObject_0 = GameObject.FindGameObjectWithTag("Player");
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ZombieCreator>();
		_gameController.Int32_1++;
		monsterParams_0 = gameObject_1.GetComponent<MonsterParams>();
		float_0 = monsterParams_0.Single_3;
		float_1 = float_0;
		transform_0 = null;
		gameObject_1.GetComponent<Animation>().Stop();
		Walk();
		if (!Defs.bool_0 && !base.gameObject.name.Contains("Boss"))
		{
			ZombieCreator.LastEnemy += IncreaseRange;
			if (_gameController.Boolean_0)
			{
				IncreaseRange();
			}
		}
	}

	private void IncreaseRange()
	{
		monsterParams_0.LastMonster();
	}

	public static float AngleSigned(Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2)
	{
		return Mathf.Atan2(Vector3.Dot(vector3_2, Vector3.Cross(vector3_0, vector3_1)), Vector3.Dot(vector3_0, vector3_1)) * 57.29578f;
	}

	public void Slowdown(float float_2)
	{
	}

	private void Update()
	{
		if (!bool_2)
		{
			if (!(transform_0 != null))
			{
				return;
			}
			float num = Vector3.SqrMagnitude(transform_0.position - transform_1.position);
			Vector3 vector = new Vector3(transform_0.position.x, transform_1.position.y, transform_0.position.z);
			if (num >= monsterParams_0.Single_2 * monsterParams_0.Single_2)
			{
				navMeshAgent_0.SetDestination(vector);
				navMeshAgent_0.speed = monsterParams_0.Single_5;
				float_1 = float_0;
				PlayZombieRun();
				return;
			}
			if (navMeshAgent_0.path != null)
			{
				navMeshAgent_0.ResetPath();
			}
			float_1 -= Time.deltaTime;
			transform_1.LookAt(vector);
			if (float_1 <= 0f)
			{
				if (transform_0.CompareTag("Player"))
				{
					player_move_c_0.hit(monsterParams_0.Single_1, transform_1.position);
				}
				if (transform_0.CompareTag("Turret"))
				{
					transform_0.GetComponent<TurretController>().MinusLive(monsterParams_0.Single_1);
				}
				float_1 = float_0;
				if (Defs.Boolean_0)
				{
					AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.BITE);
					if (monsterAudioClip != null)
					{
						base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
					}
				}
			}
			if ((bool)gameObject_1.GetComponent<Animation>()[string_6])
			{
				gameObject_1.GetComponent<Animation>().CrossFade(string_6);
			}
			else if ((bool)gameObject_1.GetComponent<Animation>()[string_7])
			{
				gameObject_1.GetComponent<Animation>().CrossFade(string_7);
			}
		}
		else if (bool_3)
		{
			float num2 = 7f;
			transform_1.position = new Vector3(transform_1.position.x, transform_1.position.y - num2 * Time.deltaTime, transform_1.position.z);
		}
	}

	public void PlayZombieRun()
	{
		if ((bool)gameObject_1.GetComponent<Animation>()[string_2])
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_2);
		}
	}

	public void SetTarget(Transform transform_2, bool bool_4)
	{
		bool_1 = bool_4;
		if ((bool)transform_2 && transform_0 != transform_2)
		{
			navMeshAgent_0.ResetPath();
			if (Defs.Boolean_0)
			{
				AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.VOICE);
				if (monsterAudioClip != null)
				{
					base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
				}
			}
			PlayZombieRun();
		}
		else if (!transform_2 && transform_0 != transform_2)
		{
			navMeshAgent_0.ResetPath();
			Walk();
		}
		transform_0 = transform_2;
		SpawnMonster component = GetComponent<SpawnMonster>();
		component.Boolean_0 = transform_2 == null;
	}

	private void Run()
	{
	}

	private void Stop()
	{
	}

	private void Attack()
	{
	}

	[Obfuscation(Exclude = true)]
	private void Death()
	{
		ZombieCreator.LastEnemy -= IncreaseRange;
		navMeshAgent_0.enabled = false;
		AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.DEATH);
		if (Defs.Boolean_0 && monsterAudioClip != null && RequestPlayDeath(monsterAudioClip.length))
		{
			base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
		}
		float num = ((!(monsterAudioClip != null)) ? 0f : monsterAudioClip.length);
		gameObject_1.GetComponent<Animation>().Stop();
		if ((bool)gameObject_1.GetComponent<Animation>()[string_4])
		{
			gameObject_1.GetComponent<Animation>().Play(string_4);
			num = Mathf.Max(num, gameObject_1.GetComponent<Animation>()[string_4].length);
			CodeAfterDelay(gameObject_1.GetComponent<Animation>()[string_4].length * 1.25f, StartFall);
		}
		else
		{
			StartFall();
		}
		CodeAfterDelay(num, DestroySelf);
		gameObject_1.GetComponent<BoxCollider>().enabled = false;
		bool_2 = true;
		SpawnMonster component = GetComponent<SpawnMonster>();
		component.Boolean_0 = false;
		_gameController.Int32_2++;
		++UserController.UserController_0.UserData_0.localUserData_0.ObscuredInt_1;
		LocalUserData localUserData_ = UserController.UserController_0.UserData_0.localUserData_0;
		localUserData_.ObscuredInt_0 = (int)localUserData_.ObscuredInt_0 + monsterParams_0.Int32_0;
	}

	private void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void StartFall()
	{
		bool_3 = true;
	}

	[Obfuscation(Exclude = true)]
	private void Walk()
	{
		gameObject_1.GetComponent<Animation>().Stop();
		if ((bool)gameObject_1.GetComponent<Animation>()[string_1])
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_1);
		}
		else
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_2);
		}
	}

	private void FixedUpdate()
	{
		if ((bool)base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}

	public void CodeAfterDelay(float float_2, DelayedCallback delayedCallback_0)
	{
		StartCoroutine(delayedCallback(float_2, delayedCallback_0));
	}

	private IEnumerator delayedCallback(float float_2, DelayedCallback delayedCallback_0)
	{
		yield return new WaitForSeconds(float_2);
		delayedCallback_0();
	}

	private void OnDestroy()
	{
		ZombieCreator.LastEnemy -= IncreaseRange;
	}
}
