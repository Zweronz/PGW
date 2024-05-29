using System;
using System.Collections;
using UnityEngine;
using engine.helpers;

public sealed class ZombiUpravlenie : MonoBehaviour
{
	public delegate void DelayedCallback();

	public static bool bool_0;

	public GameObject playerKill;

	private Player_move_c player_move_c_0;

	private GameObject gameObject_0;

	private float float_0;

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

	private bool bool_1;

	private UnityEngine.AI.NavMeshAgent navMeshAgent_0;

	private BoxCollider boxCollider_0;

	private ZombiManager zombiManager_0;

	public bool deaded;

	public Transform target;

	public float health;

	private PhotonView photonView_0;

	public Texture hitTexture;

	private Texture texture_0;

	private bool bool_2;

	public int typeZombInMas;

	private float float_1 = 5f;

	private float float_2;

	public int tekAnim = -1;

	private float float_3;

	private IEnumerator resetDeathAudio(float float_4)
	{
		bool_0 = true;
		yield return new WaitForSeconds(float_4);
		bool_0 = false;
	}

	public bool RequestPlayDeath(float float_4)
	{
		if (bool_0)
		{
			return false;
		}
		StartCoroutine(resetDeathAudio(float_4));
		return true;
	}

	private void Awake()
	{
		try
		{
			gameObject_1 = base.transform.GetChild(0).gameObject;
			health = gameObject_1.GetComponent<MonsterParams>().Single_0;
			if (Defs.bool_2 && !Defs.bool_4)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (!Defs.bool_4)
			{
				base.enabled = false;
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	[RPC]
	private void setHealthRPC(float float_4)
	{
		health = float_4;
	}

	[RPC]
	private void flashRPC()
	{
		StartCoroutine(Flash());
	}

	public void SendSlowDownRPC(float float_4)
	{
		photonView_0.RPC("SlowdownRPC", PhotonTargets.All, float_4);
	}

	[RPC]
	public void SlowdownRPC(float float_4)
	{
	}

	public void setHealth(float float_4, bool bool_3)
	{
		photonView_0.RPC("setHealthRPC", PhotonTargets.All, float_4);
		if (bool_3 && !bool_2)
		{
			StartCoroutine(Flash());
			photonView_0.RPC("flashRPC", PhotonTargets.Others);
		}
	}

	private IEnumerator Flash()
	{
		bool_2 = true;
		Utility.SetTextureRecursiveFrom(gameObject_1, hitTexture);
		yield return new WaitForSeconds(0.125f);
		Utility.SetTextureRecursiveFrom(gameObject_1, texture_0);
		bool_2 = false;
	}

	private void Start()
	{
		try
		{
			navMeshAgent_0 = GetComponent<UnityEngine.AI.NavMeshAgent>();
			boxCollider_0 = gameObject_1.GetComponent<BoxCollider>();
			string_7 = string_3;
			gameObject_0 = GameObject.FindGameObjectWithTag("Player");
			zombiManager_0 = GameObject.FindGameObjectWithTag("ZombiCreator").GetComponent<ZombiManager>();
			monsterParams_0 = gameObject_1.GetComponent<MonsterParams>();
			float_0 = monsterParams_0.Single_3;
			target = null;
			gameObject_1.GetComponent<Animation>().Stop();
			Walk();
			photonView_0 = PhotonView.Get(this);
			Texture2D monsterSkinTexture = MonstersController.GetMonsterSkinTexture(monsterParams_0.monsterId);
			if (monsterSkinTexture != null)
			{
				Utility.SetTextureRecursiveFrom(gameObject_1, monsterSkinTexture);
			}
			texture_0 = monsterSkinTexture;
			if (photonView_0.Boolean_1)
			{
				photonView_0.RPC("setHealthRPC", PhotonTargets.All, monsterParams_0.Single_0);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	public void setId(int int_0)
	{
		photonView_0.RPC("setIdRPC", PhotonTargets.All, int_0);
	}

	[RPC]
	public void setIdRPC(int int_0)
	{
		GetComponent<PhotonView>().Int32_1 = int_0;
	}

	private void Update()
	{
		try
		{
			if (!ZombiManager.zombiManager_0.startGame)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				if (!photonView_0.Boolean_1)
				{
					return;
				}
				if (!deaded)
				{
					if (target != null && target.CompareTag("Player") && target.GetComponent<SkinName>().Player_move_c_0.Boolean_14)
					{
						target = null;
					}
					if (target != null && float_1 > 0f)
					{
						float_1 -= Time.deltaTime;
						float num = Vector3.SqrMagnitude(target.position - base.transform.position);
						Vector3 vector = new Vector3(target.position.x, base.transform.position.y, target.position.z);
						if (num >= monsterParams_0.Single_2 * monsterParams_0.Single_2)
						{
							float_2 -= Time.deltaTime;
							if (float_2 < 0f)
							{
								navMeshAgent_0.SetDestination(vector);
								navMeshAgent_0.speed = monsterParams_0.Single_5;
								float_2 = 0.5f;
							}
							float_0 = monsterParams_0.Single_3;
							PlayZombieRun();
						}
						else
						{
							if (navMeshAgent_0.path != null)
							{
								navMeshAgent_0.ResetPath();
							}
							float_0 -= Time.deltaTime;
							base.transform.LookAt(vector);
							if (float_0 <= 0f)
							{
								float_0 = monsterParams_0.Single_3;
								if (Defs.Boolean_0)
								{
									AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.BITE);
									if (monsterAudioClip != null)
									{
										base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
									}
								}
								if (target.CompareTag("Player"))
								{
									SkinName component = target.GetComponent<SkinName>();
									if ((bool)component)
									{
										int num2 = (int)Mathf.Floor(monsterParams_0.Single_1);
										if (num2 > 0)
										{
											component.Player_move_c_0.minusLiveFromZombi(num2, base.transform.position);
										}
									}
								}
								if (target.CompareTag("Turret"))
								{
									target.GetComponent<TurretController>().MinusLive(monsterParams_0.Single_1);
								}
							}
							PlayZombieAttack();
						}
					}
					else
					{
						float_3 -= Time.deltaTime;
						if (float_3 <= 0f)
						{
							float_3 = 5f;
							navMeshAgent_0.ResetPath();
							Vector3 vector2 = new Vector3(-20 + UnityEngine.Random.Range(0, 40), base.transform.position.y, -20 + UnityEngine.Random.Range(0, 40));
							base.transform.LookAt(vector2);
							navMeshAgent_0.SetDestination(vector2);
							navMeshAgent_0.speed = monsterParams_0.Single_6;
						}
						GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
						GameObject[] array2 = GameObject.FindGameObjectsWithTag("Turret");
						if (array.Length > 0)
						{
							float_1 = 5f;
							float num3 = Vector3.SqrMagnitude(base.transform.position - array[0].transform.position);
							target = array[0].transform;
							GameObject[] array3 = array;
							foreach (GameObject gameObject in array3)
							{
								if (!gameObject.GetComponent<SkinName>().Player_move_c_0.Boolean_14)
								{
									float num4 = Vector3.SqrMagnitude(base.transform.position - gameObject.transform.position);
									if (num4 < num3)
									{
										num3 = num4;
										target = gameObject.transform;
									}
								}
							}
							GameObject[] array4 = array2;
							foreach (GameObject gameObject2 in array4)
							{
								float num5 = Vector3.SqrMagnitude(base.transform.position - gameObject2.transform.position);
								if (num5 < num3)
								{
									num3 = num5;
									target = gameObject2.transform;
								}
							}
						}
					}
					if (health <= 0f)
					{
						photonView_0.RPC("Death", PhotonTargets.All);
					}
				}
				else if (bool_1)
				{
					float num6 = 7f;
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - num6 * Time.deltaTime, base.transform.position.z);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	[RPC]
	private void Death()
	{
		if (!Defs.bool_4)
		{
			return;
		}
		if (navMeshAgent_0 != null)
		{
			navMeshAgent_0.enabled = false;
		}
		float num = 0.1f;
		if (Defs.Boolean_0)
		{
			AudioClip monsterAudioClip = MonstersController.GetMonsterAudioClip(monsterParams_0.monsterId, MonstersController.MonsterAudioType.DEATH);
			if (monsterAudioClip != null)
			{
				if (RequestPlayDeath(monsterAudioClip.length))
				{
					base.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
				}
				num = monsterAudioClip.length;
			}
		}
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
		deaded = true;
		SpawnMonster component = GetComponent<SpawnMonster>();
		component.Boolean_0 = false;
	}

	private void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void StartFall()
	{
		bool_1 = true;
	}

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

	public void PlayZombieRun()
	{
		if (tekAnim != 1 && Defs.bool_4)
		{
			if ((bool)gameObject_1.GetComponent<Animation>()[string_2])
			{
				gameObject_1.GetComponent<Animation>().CrossFade(string_2);
			}
			photonView_0.RPC("PlayZombieRunRPC", PhotonTargets.Others);
		}
		tekAnim = 1;
	}

	public void PlayZombieAttack()
	{
		if (tekAnim != 2 && Defs.bool_4)
		{
			if ((bool)gameObject_1.GetComponent<Animation>()[string_6])
			{
				gameObject_1.GetComponent<Animation>().CrossFade(string_6);
			}
			else if ((bool)gameObject_1.GetComponent<Animation>()[string_7])
			{
				gameObject_1.GetComponent<Animation>().CrossFade(string_7);
			}
			photonView_0.RPC("PlayZombieAttackRPC", PhotonTargets.Others);
		}
		tekAnim = 2;
	}

	[RPC]
	public void PlayZombieRunRPC()
	{
		if ((bool)gameObject_1.GetComponent<Animation>()[string_2])
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_2);
		}
		tekAnim = 1;
	}

	[RPC]
	public void PlayZombieAttackRPC()
	{
		if ((bool)gameObject_1.GetComponent<Animation>()[string_6])
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_6);
		}
		else if ((bool)gameObject_1.GetComponent<Animation>()[string_7])
		{
			gameObject_1.GetComponent<Animation>().CrossFade(string_7);
		}
		tekAnim = 2;
	}

	public void CodeAfterDelay(float float_4, DelayedCallback delayedCallback_0)
	{
		StartCoroutine(delayedCallback(float_4, delayedCallback_0));
	}

	private IEnumerator delayedCallback(float float_4, DelayedCallback delayedCallback_0)
	{
		yield return new WaitForSeconds(float_4);
		delayedCallback_0();
	}
}
