using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using ZeichenKraftwerk;
using engine.helpers;
using engine.unity;

public sealed class TurretController : MonoBehaviour
{
	public enum TurrateState
	{
		IN_HAND = 0,
		BUILDING = 1,
		FIGHT = 2
	}

	public SkinnedMeshRenderer turretRenderer;

	public SkinnedMeshRenderer turretExplosionRenderer;

	public Rotator rotator;

	public Material turretMaterial;

	public GameObject killedParticle;

	public GameObject explosionAnimObj;

	public GameObject buildingAnimObj;

	public GameObject turretObj;

	public GameObject turretMainObj;

	public Transform tower;

	public Transform gun;

	public AudioClip shotClip;

	public AudioClip buildingClip;

	public ParticleSystem gunFlash1;

	public ParticleSystem gunFlash2;

	public Transform myTransform;

	public Transform spherePoint;

	public Transform rayGroundPoint;

	public BoxCollider myCollider;

	public Transform shotPoint;

	public Transform shotPoint2;

	private Collider collider_0;

	private TurrateState turrateState_0;

	private float float_0;

	private float float_1 = -1000f;

	private float float_2 = 500f;

	private float float_3 = 220f;

	private float float_4 = 30f;

	private float float_5;

	private float float_6 = 20f;

	private float float_7 = 60f;

	private float float_8 = 75f;

	private float float_9 = -60f;

	private float float_10 = -1f;

	private float float_11 = 1f;

	private float float_12 = -1f;

	private float float_13 = 0.5f;

	private float float_14;

	private float float_15 = 0.015f;

	private Transform transform_0;

	private Transform transform_1;

	private GameObject gameObject_0;

	private bool bool_0;

	private PhotonView photonView_0;

	private Transform transform_2;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5;

	private ConsumableData consumableData_0;

	[CompilerGenerated]
	private float float_16;

	[CompilerGenerated]
	private float float_17;

	[CompilerGenerated]
	private float float_18;

	[CompilerGenerated]
	private GameObject gameObject_1;

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private NickLabelController nickLabelController_0;

	[CompilerGenerated]
	private bool bool_6;

	[CompilerGenerated]
	private bool bool_7;

	[CompilerGenerated]
	private int int_0;

	private float Single_0
	{
		get
		{
			if (ConsumableData_0 != null)
			{
				SkillData skill = ArtikulController.ArtikulController_0.GetSkill(ConsumableData_0.Int32_0, SkillId.SKILL_WEAPON_RANGE);
				if (skill != null)
				{
					return skill.Single_1;
				}
			}
			return 30f;
		}
	}

	private float Single_1
	{
		get
		{
			if (ConsumableData_0 != null && ConsumableData_0.Single_2 > 0f)
			{
				return ConsumableData_0.Single_2;
			}
			return 0.1f;
		}
	}

	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_16;
		}
		[CompilerGenerated]
		private set
		{
			float_16 = value;
		}
	}

	public float Single_3
	{
		[CompilerGenerated]
		get
		{
			return float_17;
		}
		[CompilerGenerated]
		private set
		{
			float_17 = value;
		}
	}

	public float Single_4
	{
		[CompilerGenerated]
		get
		{
			return float_18;
		}
		[CompilerGenerated]
		private set
		{
			float_18 = value;
		}
	}

	public GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_1;
		}
		[CompilerGenerated]
		private set
		{
			gameObject_1 = value;
		}
	}

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_0;
		}
		[CompilerGenerated]
		private set
		{
			player_move_c_0 = value;
		}
	}

	public NickLabelController NickLabelController_0
	{
		[CompilerGenerated]
		get
		{
			return nickLabelController_0;
		}
		[CompilerGenerated]
		private set
		{
			nickLabelController_0 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_6;
		}
		[CompilerGenerated]
		private set
		{
			bool_6 = value;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return turrateState_0 == TurrateState.FIGHT;
		}
	}

	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_7;
		}
		[CompilerGenerated]
		private set
		{
			bool_7 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public ConsumableData ConsumableData_0
	{
		get
		{
			if (consumableData_0 != null)
			{
				return consumableData_0;
			}
			consumableData_0 = ConsumablesController.ConsumablesController_0.GetConsumable(Int32_0);
			return consumableData_0;
		}
	}

	private void Awake()
	{
		photonView_0 = PhotonView.Get(this);
		collider_0 = spherePoint.GetComponent<Collider>();
		collider_0.isTrigger = true;
	}

	private IEnumerator Start()
	{
		if (bool_0)
		{
			yield break;
		}
		bool_2 = Defs.bool_2;
		bool_1 = bool_2 && photonView_0.Boolean_1;
		if (bool_2)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
			for (int i = 0; i < array.Length; i++)
			{
				if (photonView_0.int_1 == array[i].GetComponent<PhotonView>().int_1)
				{
					GameObject_0 = array[i];
					break;
				}
			}
		}
		else
		{
			GameObject_0 = GameObject.FindGameObjectWithTag("Player");
		}
		Player_move_c_0 = GameObject_0.GetComponent<SkinName>().Player_move_c_0;
		bool_3 = Player_move_c_0.Boolean_8;
		bool_4 = Player_move_c_0.Boolean_6;
		bool_5 = Player_move_c_0.Boolean_7;
		turretRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		if (bool_2 && !bool_1)
		{
			if (bool_2 && !bool_1)
			{
				collider_0.gameObject.layer = LayerMask.NameToLayer("TurretSphereCollider");
			}
		}
		else
		{
			if (Int32_0 == 0)
			{
				Log.AddLine("TurretController::Start. Consumable id == 0, turrent not consistent.", Log.LogLevel.ERROR);
				yield break;
			}
			Player_move_c.SetLayerRecursively(base.gameObject, LayerMask.NameToLayer("Gun"));
			collider_0.enabled = false;
			InitParamsRPC(Int32_0);
			if (bool_2)
			{
				photonView_0.RPC("InitParamsRPC", PhotonTargets.AllBuffered, Int32_0);
			}
		}
		NickLabelController_0 = NickLabelStack.nickLabelStack_0.GetNextCurrentLabel();
		NickLabelController_0.Transform_0 = base.transform;
		NickLabelController_0.Boolean_3 = true;
		NickLabelController_0.Boolean_2 = false;
		NickLabelController_0.StartShow();
		if (turrateState_0 == TurrateState.IN_HAND)
		{
			while (GameObject_0 == null || Player_move_c_0.PlayerTurretController_0.turretPoint == null)
			{
				yield return null;
			}
			base.transform.parent = Player_move_c_0.PlayerTurretController_0.turretPoint.transform;
			yield return null;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
		}
	}

	private void OnDestroy()
	{
		if (!bool_2 || bool_1)
		{
			Player_move_c_0.PlayerTurretController_0.CancelTurret();
		}
	}

	private void Update()
	{
		if (bool_0)
		{
			return;
		}
		if (turrateState_0 == TurrateState.BUILDING && (!bool_2 || bool_1))
		{
			float_0 -= Time.deltaTime;
			if (float_0 <= 0f)
			{
				float_0 = 0f;
				EndBuilding();
				return;
			}
		}
		turretObj.SetActive(turrateState_0 == TurrateState.FIGHT);
		SetStateIsEnemyTurret();
		if (!Boolean_0)
		{
			NickLabelController_0.ResetTimeShow();
		}
		if (rotator.eulersPerSecond.z < -200f)
		{
			rotator.eulersPerSecond = new Vector3(0f, 0f, rotator.eulersPerSecond.z + this.float_2 * Time.deltaTime);
		}
		if (bool_2 && !bool_1)
		{
			if (turrateState_0 == TurrateState.IN_HAND)
			{
				if (!collider_0.enabled)
				{
					collider_0.enabled = true;
				}
			}
			else if (collider_0.enabled)
			{
				collider_0.enabled = false;
			}
		}
		else if (bool_2 && bool_1 && WeaponManager.weaponManager_0.myPlayer == null)
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
		else
		{
			if (turrateState_0 == TurrateState.BUILDING)
			{
				return;
			}
			if (turrateState_0 == TurrateState.IN_HAND)
			{
				Ray ray = new Ray(rayGroundPoint.position, Vector3.down);
				Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
				bool flag = false;
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, 1f, Defs.Int32_0) && hitInfo.distance > 0.05f && hitInfo.distance < 0.7f && !Physics.CheckSphere(spherePoint.position, 0.71f, Defs.Int32_0))
				{
					ray = new Ray(spherePoint.position, myPlayerMoveC.myTransform.position - spherePoint.position);
					if (!Physics.Raycast(ray, out hitInfo, (myPlayerMoveC.myTransform.position - spherePoint.position).magnitude, Defs.Int32_0))
					{
						ray = new Ray(myPlayerMoveC.myTransform.position, spherePoint.position - myPlayerMoveC.myTransform.position);
						if (!Physics.Raycast(ray, out hitInfo, (myPlayerMoveC.myTransform.position - spherePoint.position).magnitude, Defs.Int32_0))
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					turretRenderer.materials[0].SetColor("_TintColor", new Color(1f, 1f, 1f, 0.08f));
					myPlayerMoveC.PlayerTurretController_0.Boolean_2 = true;
				}
				else
				{
					turretRenderer.materials[0].SetColor("_TintColor", new Color(1f, 0f, 0f, 0.08f));
					myPlayerMoveC.PlayerTurretController_0.Boolean_2 = false;
				}
				if (NickLabelController_0.gameObject.activeSelf)
				{
					NickLabelController_0.gameObject.SetActive(false);
				}
				return;
			}
			if (Boolean_2)
			{
				if (gun.rotation.x > float_9)
				{
					gun.Rotate(float_4 * Time.deltaTime, 0f, 0f);
				}
				return;
			}
			if (!NickLabelController_0.gameObject.activeSelf)
			{
				NickLabelController_0.gameObject.SetActive(true);
			}
			if (transform_2 != null && (transform_2.position.y < -500f || (transform_2.CompareTag("Player") && transform_2.GetComponent<SkinName>().Player_move_c_0.Boolean_14)))
			{
				transform_2 = null;
			}
			if (transform_2 == null)
			{
				float_12 -= Time.deltaTime;
				if (float_12 < 0f)
				{
					float_12 = float_13;
					transform_2 = ScanTarget();
				}
				if (Mathf.Abs(float_5) < 0.5f)
				{
					float_5 = UnityEngine.Random.Range(-1f * float_7 / 2f, float_7 / 2f);
				}
				else
				{
					float num = Time.deltaTime * float_6 * Mathf.Abs(float_5) / float_5;
					float_5 -= num;
					tower.localRotation = Quaternion.Euler(new Vector3(0f, tower.localRotation.eulerAngles.y + num, 0f));
				}
				if (Mathf.Abs(gun.localRotation.eulerAngles.x) > 1f)
				{
					gun.Rotate((float)((!(gun.localRotation.eulerAngles.x < 180f)) ? 1 : (-1)) * float_4 * Time.deltaTime, 0f, 0f);
				}
				return;
			}
			bool flag2 = false;
			Vector3 position = transform_2.position;
			if (transform_2.CompareTag("Enemy") && transform_2.transform.childCount > 0)
			{
				Transform child = transform_2.transform.GetChild(0);
				BoxCollider component = child.GetComponent<BoxCollider>();
				if (component != null)
				{
					position += component.center;
				}
			}
			Vector2 to = new Vector2(position.x, position.z) - new Vector2(tower.position.x, tower.position.z);
			float num2 = Mathf.Abs(to.x) / to.x;
			float float_ = num2 * Vector2.Angle(Vector2.up, to);
			float deltaAngles = GetDeltaAngles(tower.rotation.eulerAngles.y, float_);
			float num3 = (0f - float_3) * Time.deltaTime * Mathf.Abs(deltaAngles) / deltaAngles;
			if (Mathf.Abs(deltaAngles) < 10f)
			{
				flag2 = true;
			}
			if (Mathf.Abs(num3) > Mathf.Abs(deltaAngles))
			{
				num3 = 0f - deltaAngles;
			}
			if (Mathf.Abs(num3) > 0.001f)
			{
				tower.Rotate(0f, num3, 0f);
			}
			float float_2 = -180f / (float)Math.PI * Mathf.Asin((position.y - shotPoint.position.y) / Vector3.Distance(position, shotPoint.position));
			float deltaAngles2 = GetDeltaAngles(gun.rotation.eulerAngles.x, float_2);
			num3 = (0f - float_4) * Time.deltaTime * Mathf.Abs(deltaAngles2) / deltaAngles2;
			if (Mathf.Abs(num3) > Mathf.Abs(deltaAngles2))
			{
				num3 = 0f - deltaAngles2;
			}
			if (num3 > 0f && gun.rotation.x > float_8)
			{
				num3 = 0f;
			}
			if (num3 < 0f && gun.rotation.x < float_9)
			{
				num3 = 0f;
			}
			if (Mathf.Abs(num3) > 0.001f)
			{
				gun.Rotate(num3, 0f, 0f);
			}
			if (flag2)
			{
				float_14 -= Time.deltaTime;
				if (float_14 < 0f)
				{
					photonView_0.RPC("ShotRPC", PhotonTargets.All);
					float_14 = Single_1;
					float_10 = 0f;
				}
			}
			float_10 -= Time.deltaTime;
			if (float_10 < 0f)
			{
				float_10 = float_11;
				transform_2 = ScanTarget();
			}
		}
	}

	private Transform ScanTarget()
	{
		GameObject[] array = null;
		GameObject[] array2 = null;
		GameObject[] array3 = null;
		if ((bool_2 && bool_3) || !bool_2)
		{
			array3 = GameObject.FindGameObjectsWithTag("Enemy");
		}
		else
		{
			array3 = GameObject.FindGameObjectsWithTag("HeadCollider");
			array2 = GameObject.FindGameObjectsWithTag("Turret");
		}
		int num = ((array3 != null) ? array3.Length : 0) + ((array2 != null) ? array2.Length : 0);
		array = new GameObject[num];
		if (array3 != null)
		{
			array3.CopyTo(array, 0);
		}
		if (array2 != null)
		{
			array2.CopyTo(array, (array3 != null) ? array3.Length : 0);
		}
		Transform result = null;
		GameObject gameObject = null;
		for (int i = 0; i < array.Length; i++)
		{
			gameObject = array[i];
			if (!CheckTarget(gameObject))
			{
				continue;
			}
			Transform transform = gameObject.transform;
			float num2 = Vector3.Distance(transform.position, myTransform.position);
			float num3 = Mathf.Sqrt((transform.position.x - myTransform.position.x) * (transform.position.x - myTransform.position.x) + (transform.position.z - myTransform.position.z) * (transform.position.z - myTransform.position.z));
			float num4 = Mathf.Acos(num3 / num2);
			float num5 = num4 * 180f / (float)Math.PI;
			if (!(num2 < Single_0) || !(num5 < float_8))
			{
				continue;
			}
			Ray ray = new Ray(tower.position, transform.position - tower.position);
			RaycastHit hitInfo;
			bool flag = Physics.Raycast(ray, out hitInfo, Single_0);
			transform_1 = hitInfo.transform;
			transform_0 = transform;
			bool flag2 = false;
			if (flag)
			{
				if (hitInfo.collider.gameObject.transform.Equals(transform))
				{
					flag2 = true;
				}
				else if (hitInfo.collider.gameObject.transform.parent != null)
				{
					Transform parent = hitInfo.collider.gameObject.transform.parent;
					if (parent.Equals(transform) || parent.Equals(transform.parent))
					{
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				result = transform;
			}
		}
		return result;
	}

	private bool CheckTarget(GameObject gameObject_2)
	{
		if (!bool_2)
		{
			return true;
		}
		if (bool_2 && bool_3)
		{
			return true;
		}
		if (gameObject_2.CompareTag("Turret") && gameObject_2.GetComponent<TurretController>().Boolean_0)
		{
			return true;
		}
		if (gameObject_2.transform.parent != null && gameObject_2.transform.parent.gameObject.CompareTag("Player") && !gameObject_2.transform.parent.gameObject.Equals(WeaponManager.weaponManager_0.myPlayer))
		{
			Player_move_c player_move_c = gameObject_2.transform.parent.GetComponent<SkinName>().Player_move_c_0;
			if (((!bool_4 && !bool_5) || player_move_c.Int32_2 != WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2) && !player_move_c.Boolean_20 && gameObject_2.transform.position.y > -500f && !player_move_c.Boolean_14)
			{
				return true;
			}
		}
		return false;
	}

	private void SetStateIsEnemyTurret()
	{
		bool boolean_ = Boolean_0;
		Boolean_0 = false;
		if (bool_2 && (bool_5 || bool_4))
		{
			if (GameObject_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && Player_move_c_0.Int32_2 != WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2)
			{
				Boolean_0 = true;
			}
		}
		else if (bool_2 && !bool_1)
		{
			Boolean_0 = true;
		}
		if (Boolean_0 != boolean_)
		{
			NickLabelController_0.isEnemySprite.SetActive(Boolean_0);
		}
	}

	[Obfuscation(Exclude = true)]
	private void StopGunFlash()
	{
		gunFlash1.enableEmission = false;
	}

	private float GetDeltaAngles(float float_19, float float_20)
	{
		if (float_19 < 0f)
		{
			float_19 += 360f;
		}
		if (float_20 < 0f)
		{
			float_20 += 360f;
		}
		float num = float_19 - float_20;
		if (Mathf.Abs(num) > 180f)
		{
			num = ((!(float_19 > float_20)) ? (num + 360f) : (num - 360f));
		}
		return num;
	}

	public void StartTurret()
	{
		if (bool_2 && bool_1)
		{
			photonView_0.RPC("StartTurretRPC", PhotonTargets.AllBuffered);
		}
		else if (!bool_2)
		{
			StartTurretRPC();
		}
		myCollider.enabled = true;
		base.transform.GetComponent<Rigidbody>().isKinematic = false;
		base.transform.GetComponent<Rigidbody>().useGravity = true;
		Invoke("SetNoUseGravityInvoke", 5f);
		int int32_ = ConsumableData_0.Int32_3;
		if (int32_ > 0)
		{
			StartBuilding();
		}
		else
		{
			EndBuilding();
		}
	}

	[Obfuscation(Exclude = true)]
	private void SetNoUseGravityInvoke()
	{
		base.transform.GetComponent<Rigidbody>().useGravity = false;
		base.transform.GetComponent<Rigidbody>().isKinematic = true;
	}

	private void StartBuilding()
	{
		float_0 = ConsumableData_0.Int32_3;
		HeadUpDisplay.HeadUpDisplay_0.ShowTurretBuilding(float_0);
		if (bool_2 && bool_1)
		{
			StartBuildingTurretRPC();
			photonView_0.RPC("StartBuildingTurretRPC", PhotonTargets.OthersBuffered);
		}
		else if (!bool_2)
		{
			StartBuildingTurretRPC();
		}
	}

	private void EndBuilding()
	{
		if (bool_2 && bool_1)
		{
			BuildEndTurretRPC();
			photonView_0.RPC("BuildEndTurretRPC", PhotonTargets.OthersBuffered);
		}
		else if (!bool_2)
		{
			BuildEndTurretRPC();
		}
		if (ConsumableData_0.Int32_3 > 0)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerTurretController_0.TurretEndBuilding();
		}
	}

	private IEnumerator FlashRed()
	{
		turretRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		yield return null;
		turretRenderer.material.SetColor("_ColorRili", new Color(1f, 0f, 0f, 1f));
		yield return new WaitForSeconds(0.1f);
		turretRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
	}

	public void MinusLive(float float_19, int int_1 = 0)
	{
		MinusLive(float_19, false, int_1);
	}

	public void MinusLive(float float_19, bool bool_8, int int_1 = 0)
	{
		if (turrateState_0 != TurrateState.FIGHT)
		{
			return;
		}
		if (bool_2)
		{
			Single_3 -= float_19;
			if (Single_3 < 0f)
			{
				ImKilledRPCWithExplosion(bool_8);
				float_19 = 10000f;
			}
			photonView_0.RPC("MinusLiveRPC", PhotonTargets.All, float_19, bool_8, int_1);
		}
		else
		{
			MinusLiveReal(float_19, bool_8);
		}
	}

	public void MinusLiveReal(float float_19, bool bool_8, int int_1 = 0)
	{
		StopCoroutine(FlashRed());
		StartCoroutine(FlashRed());
		if (Boolean_2 || (bool_2 && !bool_1))
		{
			return;
		}
		Single_3 -= float_19;
		if (bool_2)
		{
			photonView_0.RPC("SynchHealth", PhotonTargets.Others, Single_3);
		}
		photonView_0.RPC("OnDamageObtained", PhotonTargets.All, (!(Single_3 >= 0f)) ? (float_19 + Single_3) : float_19, int_1);
		if (Single_3 < 0f)
		{
			Single_3 = 0f;
			if (bool_2)
			{
				photonView_0.RPC("ImKilledRPCWithExplosion", PhotonTargets.AllBuffered, bool_8);
				photonView_0.RPC("MeKillRPC", PhotonTargets.All, int_1);
			}
			else
			{
				ImKilledRPCWithExplosion(bool_8);
			}
		}
	}

	public void MeKill(string string_0)
	{
		if (WeaponManager.weaponManager_0.myPlayerMoveC != null && GameObject_0 != null)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(string_0, 9, Player_move_c_0.mySkinName.NickName, string.Empty, WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2);
		}
	}

	[Obfuscation(Exclude = true)]
	private void DestroyTurrel()
	{
		if (bool_2)
		{
			if (bool_1)
			{
				PhotonNetwork.Destroy(base.gameObject);
			}
			else
			{
				base.transform.position = new Vector3(-1000f, -1000f, -1000f);
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (bool_1)
		{
			photonView_0.RPC("InitParamsRPC", PhotonTargets.Others, Int32_0);
		}
	}

	[RPC]
	private void InitParamsRPC(int int_1)
	{
		if (int_1 != 0)
		{
			Int32_0 = int_1;
			ConsumableData consumableData = ConsumableData_0;
			Single_3 = consumableData.Single_0;
			Single_4 = consumableData.Single_0;
			Single_2 = consumableData.Single_1;
		}
	}

	[RPC]
	public void ShotRPC()
	{
		rotator.eulersPerSecond = new Vector3(0f, 0f, float_1);
		if (Defs.Boolean_0)
		{
			base.GetComponent<AudioSource>().PlayOneShot(shotClip);
		}
		gunFlash1.enableEmission = true;
		CancelInvoke("StopGunFlash");
		Invoke("StopGunFlash", Single_1 * 1.1f);
		if ((bool_2 && !bool_1) || WeaponManager.weaponManager_0.myPlayer == null)
		{
			return;
		}
		Vector3 normalized = (shotPoint2.position - shotPoint.position).normalized;
		Vector3 position = transform_2.position;
		if (transform_2.CompareTag("Enemy") && transform_2.transform.childCount > 0)
		{
			Transform child = transform_2.transform.GetChild(0);
			BoxCollider component = child.GetComponent<BoxCollider>();
			if (component != null)
			{
				position += component.center;
			}
		}
		normalized *= (position - shotPoint.position).magnitude;
		float num = 0.45f;
		Vector3 to;
		if (UnityEngine.Random.Range(0, 100) > 90)
		{
			float num2 = (float)Math.Asin(num / normalized.magnitude) * 57.29578f;
			float num3 = 0f;
			do
			{
				to = new Vector3(UnityEngine.Random.Range(0f - num, num), UnityEngine.Random.Range(0f, num), UnityEngine.Random.Range(0f - num, num));
				num3 = Vector3.Angle(normalized, to);
			}
			while (!(num3 > 0f) || !(num3 < 90f) || num3 <= num2);
			float num4 = num3;
			num4 = 180f - num4;
			double d = 1.0 - Math.Pow(num / normalized.magnitude, 2.0);
			d = Math.Sqrt(d);
			d *= (double)Mathf.Sin(num4 * ((float)Math.PI / 180f));
			d += (double)(num / normalized.magnitude * Mathf.Cos(num4 * ((float)Math.PI / 180f)));
			d = (double)num / d;
			to = to.normalized * (float)d * 1.2f;
		}
		else
		{
			to = new Vector3(UnityEngine.Random.Range(0f - num, num), UnityEngine.Random.Range(0f - num, num), UnityEngine.Random.Range(0f - num, num));
		}
		normalized += to;
		HitStruct hitStruct_ = HitStruct.GenerateHitStructTurret(Int32_0, photonView_0.Int32_1, Single_2, Single_0, shotPoint.position, normalized);
		PlayerDamageController.PlayerDamageController_0.RayShoot(hitStruct_, bool_2, 0);
	}

	[RPC]
	public void SynchHealth(float float_19)
	{
		if (Single_3 > float_19)
		{
			Single_3 = float_19;
		}
	}

	[RPC]
	public void MinusLiveRPC(float float_19, bool bool_8, int int_1)
	{
		MinusLiveReal(float_19, bool_8, int_1);
	}

	[RPC]
	private void OnDamageObtained(float float_19, int int_1)
	{
		if (int_1 == 0)
		{
			return;
		}
		Player_move_c player_move_c = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			PhotonView photonView = PhotonView.Get(gameObject);
			if ((bool)photonView && photonView.Int32_1 == int_1)
			{
				player_move_c = gameObject.GetComponent<SkinName>().Player_move_c_0;
				break;
			}
		}
		if (!(player_move_c == null))
		{
			int int_2 = (int)player_move_c.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"];
			int int_3 = ((!(player_move_c.WeaponSounds_0 == null)) ? player_move_c.WeaponSounds_0.WeaponData_0.Int32_0 : 0);
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnHit(int_2, int_3, false, float_19);
		}
	}

	[RPC]
	public void MeKillRPC(int int_1)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		string string_ = string.Empty;
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			PhotonView photonView = PhotonView.Get(gameObject);
			if (photonView != null && photonView.Int32_1 == int_1)
			{
				SkinName component = gameObject.GetComponent<SkinName>();
				string_ = component.NickName;
				break;
			}
		}
		MeKill(string_);
	}

	[RPC]
	public void ImKilledRPC()
	{
		ImKilledRPCWithExplosion(false);
	}

	[RPC]
	public void ImKilledRPCWithExplosion(bool bool_8)
	{
		Boolean_2 = true;
		bool_8 = true;
		explosionAnimObj.SetActive(true);
		turretObj.SetActive(false);
		if (!bool_2 || bool_1)
		{
			ConsumablesController.ConsumablesController_0.ForceStopDurationConsumable(SlotType.SLOT_CONSUM_TURRET);
		}
		Invoke("DestroyTurrel", 2f);
	}

	[RPC]
	public void StartTurretRPC()
	{
		myCollider.enabled = true;
		base.transform.parent = null;
		Player_move_c.SetLayerRecursively(base.gameObject, LayerMask.NameToLayer("Default"));
		photonView_0.viewSynchronization_0 = ViewSynchronization.UnreliableOnChange;
		collider_0.enabled = false;
		turrateState_0 = TurrateState.BUILDING;
	}

	[RPC]
	public void StartBuildingTurretRPC()
	{
		buildingAnimObj.SetActive(true);
		turretObj.SetActive(false);
		turretMainObj.SetActive(false);
		Animation component = buildingAnimObj.GetComponent<Animation>();
		if (component != null)
		{
			component["TurretSetupAnimation"].speed = component["TurretSetupAnimation"].length / (float)ConsumableData_0.Int32_3;
		}
		if (Defs.Boolean_0 && buildingClip != null)
		{
			base.GetComponent<AudioSource>().PlayOneShot(buildingClip);
		}
	}

	[RPC]
	public void BuildEndTurretRPC()
	{
		turrateState_0 = TurrateState.FIGHT;
		turretRenderer.material = turretMaterial;
		turretExplosionRenderer.material = turretMaterial;
		buildingAnimObj.SetActive(false);
		turretObj.SetActive(true);
		turretMainObj.SetActive(true);
	}
}
