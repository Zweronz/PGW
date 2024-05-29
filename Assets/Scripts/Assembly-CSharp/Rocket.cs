using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class Rocket : MonoBehaviour
{
	private enum StateCollision
	{
		NONE = 0,
		REBOUND = 1,
		KILL_ROCKET = 2,
		HIT_ROCKET = 3
	}

	public GameObject[] rockets;

	public bool dontExecStart;

	public RocketSettings currentRocketSettings;

	private PhotonView photonView_0;

	private int int_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3 = true;

	private Transform transform_0;

	private GameObject gameObject_0;

	private Vector3 vector3_0 = Vector3.zero;

	private Collider collider_0;

	private Vector3 vector3_1;

	private bool bool_4;

	private int int_1;

	private LayerMask layerMask_0 = LayerMask.NameToLayer("Enemy");

	private string string_0 = "Enemy";

	[CompilerGenerated]
	private HitStruct hitStruct_0;

	[CompilerGenerated]
	private float float_0;

	public HitStruct HitStruct_0
	{
		[CompilerGenerated]
		get
		{
			return hitStruct_0;
		}
		[CompilerGenerated]
		set
		{
			hitStruct_0 = value;
		}
	}

	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		set
		{
			float_0 = value;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_0;
		}
		set
		{
			int_0 = value;
			currentRocketSettings = rockets[value].GetComponent<RocketSettings>();
		}
	}

	private void Awake()
	{
		photonView_0 = PhotonView.Get(this);
		transform_0 = base.transform;
	}

	private IEnumerator Start()
	{
		if (dontExecStart)
		{
			yield break;
		}
		if (Defs.bool_2 && photonView_0.Boolean_1)
		{
			photonView_0.RPC("SetRocketActive", PhotonTargets.AllBuffered, Int32_0, Single_0);
		}
		else if (!Defs.bool_2)
		{
			SetRocketActive(Int32_0, Single_0);
		}
		if (Defs.bool_2 && !photonView_0.Boolean_1)
		{
			base.transform.GetComponent<Rigidbody>().isKinematic = true;
		}
		else if (Int32_0 != 10)
		{
			StartRocketRPC();
		}
		if (!bool_0)
		{
			if (!Defs.bool_2 || photonView_0.Boolean_1)
			{
				Player_move_c.SetLayerRecursively(base.gameObject, LayerMask.NameToLayer("Gun"));
			}
			if (Defs.bool_2)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
				for (int i = 0; i < array.Length; i++)
				{
					if (photonView_0.int_1 == array[i].GetComponent<PhotonView>().int_1)
					{
						gameObject_0 = array[i];
						break;
					}
				}
			}
			else
			{
				gameObject_0 = GameObject.FindGameObjectWithTag("Player");
			}
		}
		while (gameObject_0 == null || gameObject_0.GetComponent<SkinName>().Player_move_c_0.transform.childCount == 0 || gameObject_0.GetComponent<SkinName>().Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>().Transform_2 == null)
		{
			yield return null;
		}
		base.transform.parent = gameObject_0.GetComponent<SkinName>().Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>().Transform_2;
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.identity;
		rockets[10].transform.localPosition = Vector3.zero;
	}

	private void LateUpdate()
	{
		if (!bool_0 || Int32_0 == 10 || (Defs.bool_2 && !photonView_0.Boolean_1))
		{
			return;
		}
		if (!bool_4)
		{
			bool_4 = true;
			vector3_1 = transform_0.position;
			return;
		}
		Vector3 direction = transform_0.position - vector3_1;
		RaycastHit hitInfo;
		if (Physics.Raycast(vector3_1, direction, out hitInfo, direction.magnitude, -5))
		{
			StateCollision stateCollision = GetStateCollision(hitInfo);
			switch (stateCollision)
			{
			default:
				collider_0 = null;
				break;
			case StateCollision.REBOUND:
				ProcessRebound(hitInfo);
				break;
			case StateCollision.KILL_ROCKET:
			case StateCollision.HIT_ROCKET:
				ProcessCollision(hitInfo, stateCollision);
				break;
			}
		}
		vector3_1 = transform_0.position;
	}

	private void Update()
	{
		if (Defs.bool_2 && bool_2 && photonView_0 != null && !photonView_0.Boolean_1)
		{
			transform_0.position = Vector3.Lerp(transform_0.position, vector3_0, Time.deltaTime * 5f);
		}
	}

	public void StartRocket()
	{
		if (Defs.bool_2 && photonView_0.Boolean_1)
		{
			photonView_0.RPC("StartRocketRPC", PhotonTargets.AllBuffered);
		}
		else if (!Defs.bool_2)
		{
			StartRocketRPC();
		}
		base.transform.GetComponent<Rigidbody>().isKinematic = false;
	}

	[Obfuscation(Exclude = true)]
	private void Remove()
	{
		if (!Defs.bool_2)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
	}

	[Obfuscation(Exclude = true)]
	private void KillRocket()
	{
		KillRocket(null);
	}

	private void KillRocket(GameObject gameObject_1)
	{
		if (bool_1)
		{
			return;
		}
		bool_1 = true;
		Hit(gameObject_1);
		if (Defs.bool_2)
		{
			if (photonView_0 != null)
			{
				photonView_0.RPC("Collide", PhotonTargets.All, HitStruct_0.string_0, transform_0.position);
			}
		}
		else
		{
			Collide(HitStruct_0.string_0, transform_0.position);
		}
	}

	private StateCollision GetStateCollision(RaycastHit raycastHit_0)
	{
		if (raycastHit_0.collider == null)
		{
			return StateCollision.NONE;
		}
		GameObject gameObject = raycastHit_0.collider.gameObject;
		Transform parent = gameObject.transform.parent;
		GameObject gameObject2 = ((!(parent != null)) ? null : parent.gameObject);
		WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
		bool flag = false;
		if (Defs.bool_2 ? ((gameObject.tag.Equals("Player") && gameObject == weaponManager_.myPlayer) || (gameObject2 != null && gameObject2.tag.Equals("Player") && gameObject2 == weaponManager_.myPlayer)) : (gameObject.tag.Equals("Player") || (gameObject2 != null && gameObject2.tag.Equals("Player"))))
		{
			return StateCollision.NONE;
		}
		if (currentRocketSettings.typeFly == RocketSettings.TypeFlyRocket.MegaBullet)
		{
			return StateCollision.HIT_ROCKET;
		}
		Transform transform = gameObject.transform;
		if (gameObject.layer == (int)layerMask_0 || (transform.parent != null && transform.parent.tag == string_0))
		{
			return StateCollision.KILL_ROCKET;
		}
		if ((!(gameObject2 != null) || !gameObject2.CompareTag("Untagged")) && (!(gameObject2 == null) || !gameObject.CompareTag("Untagged")))
		{
			return StateCollision.NONE;
		}
		SkillData skill = ArtikulController.ArtikulController_0.GetSkill(HitStruct_0.int_0, SkillId.SKILL_WEAPON_REBOUND);
		if (skill != null && skill.Int32_0 != 0 && int_1++ < skill.Int32_0)
		{
			return StateCollision.REBOUND;
		}
		return StateCollision.KILL_ROCKET;
	}

	public void ProcessRebound(RaycastHit raycastHit_0)
	{
		transform_0.position = raycastHit_0.point;
		base.GetComponent<Rigidbody>().velocity = Vector3.Reflect(base.GetComponent<Rigidbody>().velocity, raycastHit_0.normal);
	}

	private void ProcessCollision(RaycastHit raycastHit_0, StateCollision stateCollision_0)
	{
		if (!(raycastHit_0.collider == collider_0))
		{
			collider_0 = raycastHit_0.collider;
			transform_0.position = raycastHit_0.point;
			if (stateCollision_0 == StateCollision.KILL_ROCKET)
			{
				KillRocket(collider_0.gameObject);
			}
			else
			{
				Hit(collider_0.gameObject);
			}
		}
	}

	private void Hit(GameObject gameObject_1)
	{
		Vector3 position = base.transform.position;
		if (Defs.bool_2 && !photonView_0.Boolean_1)
		{
			return;
		}
		WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
		if (Defs.bool_2 && weaponManager_.myPlayer == null)
		{
			return;
		}
		WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(HitStruct_0.int_0);
		if (weapon == null)
		{
			return;
		}
		weaponManager_.myPlayerMoveC.Boolean_1 = false;
		weaponManager_.myPlayerMoveC.Boolean_2 = false;
		if (currentRocketSettings.typeFly != RocketSettings.TypeFlyRocket.Grenade && currentRocketSettings.typeFly != 0)
		{
			PlayerDamageController.PlayerDamageController_0.CollisionShoot(HitStruct_0, gameObject_1, currentRocketSettings);
		}
		else
		{
			PlayerDamageController.PlayerDamageController_0.RadiusShoot(HitStruct_0, position, currentRocketSettings);
		}
		if (weaponManager_.myPlayerMoveC.Boolean_1)
		{
			weaponManager_.myPlayerMoveC.PlayerScoreController_0.Int32_2++;
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.AimHitAnimPlay();
			}
		}
		if (weaponManager_.myPlayerMoveC.Boolean_2)
		{
			weaponManager_.myPlayerMoveC.PlayerScoreController_0.Int32_3++;
		}
		weaponManager_.myPlayerMoveC.Boolean_1 = false;
		weaponManager_.myPlayerMoveC.Boolean_2 = false;
	}

	[Obfuscation(Exclude = true)]
	private void DestroyRocket()
	{
		PhotonNetwork.Destroy(base.gameObject);
	}

	public void BazookaExplosion(string string_1, Vector3 vector3_2)
	{
		string path = ResPath.Combine("Explosions", string_1);
		GameObject gameObject = Resources.Load(path) as GameObject;
		if (gameObject == null)
		{
			gameObject = Resources.Load(ResPath.Combine("Explosions", "Weapon75")) as GameObject;
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = Object.Instantiate(gameObject, vector3_2, Quaternion.identity) as GameObject;
			gameObject2.GetComponent<AudioSource>().enabled = Defs.Boolean_0;
		}
		PlayerDamageController.PlayerDamageController_0.ExplosionImpact(Single_0, vector3_2);
	}

	[RPC]
	public void StartRocketRPC()
	{
		if (Defs.bool_2 && photonView_0 != null)
		{
			photonView_0.viewSynchronization_0 = ViewSynchronization.Unreliable;
		}
		base.transform.parent = null;
		Player_move_c.SetLayerRecursively(base.gameObject, LayerMask.NameToLayer("Default"));
		bool_0 = true;
		if (!Defs.bool_2 || photonView_0.Boolean_1)
		{
			float time = ((Int32_0 != 10) ? 7f : 1.8f);
			WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(HitStruct_0.int_0);
			if (weapon != null && weapon.Single_6 != 0f)
			{
				time = weapon.Single_6;
			}
			Invoke("KillRocket", time);
		}
	}

	[RPC]
	public void SetRocketActive(int int_2, float float_1)
	{
		Single_0 = float_1;
		if (rockets.Length != 0)
		{
			if (int_2 >= rockets.Length)
			{
				int_2 = 0;
			}
			rockets[int_2].SetActive(true);
		}
	}

	[RPC]
	private void Collide(string string_1, Vector3 vector3_2)
	{
		BazookaExplosion(string_1, vector3_2);
		if (!Defs.bool_2)
		{
			Object.Destroy(base.gameObject);
		}
		else if (photonView_0.Boolean_1)
		{
			Invoke("DestroyRocket", 0.1f);
		}
	}

	private void OnPhotonSerializeView(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		bool_2 = true;
		if (photonStream_0.Boolean_0)
		{
			photonStream_0.SendNext(transform_0.position);
			photonStream_0.SendNext(transform_0.rotation);
		}
		else
		{
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
			transform_0.rotation = (Quaternion)photonStream_0.ReceiveNext();
		}
	}

	private void OnSerializeNetworkView(BitStream bitStream_0, NetworkMessageInfo networkMessageInfo_0)
	{
		bool_2 = true;
		if (bitStream_0.isWriting)
		{
			Vector3 value = transform_0.position;
			Quaternion value2 = transform_0.rotation;
			bitStream_0.Serialize(ref value);
			bitStream_0.Serialize(ref value2);
			return;
		}
		Vector3 value3 = Vector3.zero;
		Quaternion value4 = Quaternion.identity;
		bitStream_0.Serialize(ref value3);
		bitStream_0.Serialize(ref value4);
		vector3_0 = value3;
		transform_0.rotation = value4;
		if (bool_3)
		{
			bool_3 = false;
			transform_0.position = value3;
			transform_0.rotation = value4;
		}
	}
}
