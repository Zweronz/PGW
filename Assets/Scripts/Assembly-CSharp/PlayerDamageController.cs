using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController
{
	private const float float_0 = 0.75f;

	private const float float_1 = 0.5f;

	private static PlayerDamageController playerDamageController_0;

	public static PlayerDamageController PlayerDamageController_0
	{
		get
		{
			if (playerDamageController_0 == null)
			{
				playerDamageController_0 = new PlayerDamageController();
			}
			return playerDamageController_0;
		}
	}

	public Vector3 RayShoot(HitStruct hitStruct_0, bool bool_0, int int_0, HashSet<GameObject> hashSet_0 = null)
	{
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			bool bool_ = hitStruct_0.bool_0;
			Ray ray = (hitStruct_0.bool_2 ? new Ray(hitStruct_0.vector3_0, hitStruct_0.vector3_1) : GetRandomRay(hitStruct_0));
			RaycastHit[] array = null;
			if (!hitStruct_0.bool_10 && !hitStruct_0.bool_14 && !hitStruct_0.bool_11)
			{
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, hitStruct_0.float_11, myPlayerMoveC.Int32_4))
				{
					array = new RaycastHit[1] { hitInfo };
				}
			}
			else
			{
				array = Physics.RaycastAll(ray, hitStruct_0.float_11, myPlayerMoveC.Int32_4);
			}
			if (!hitStruct_0.bool_2)
			{
				if (hitStruct_0.weaponSounds_0 != null)
				{
					BulletProcess(hitStruct_0.weaponSounds_0, bool_, ray.GetPoint(200f), int_0, hitStruct_0.Int32_0);
				}
			}
			else
			{
				BulletProcessTurret(hitStruct_0.vector3_0, ray.GetPoint(hitStruct_0.float_11));
			}
			if (array != null && array.Length != 0)
			{
				Vector3 vector3_0 = default(Vector3);
				if ((hitStruct_0.bool_11 || hitStruct_0.bool_14) && hitStruct_0.weaponSounds_0 != null)
				{
					vector3_0 = hitStruct_0.weaponSounds_0.WeaponEffectsController_0.List_0[0].position;
					Array.Sort(array, delegate(RaycastHit raycastHit_0, RaycastHit raycastHit_1)
					{
						float num = (raycastHit_0.point - vector3_0).sqrMagnitude - (raycastHit_1.point - vector3_0).sqrMagnitude;
						return (num > 0f) ? 1 : ((num != 0f) ? (-1) : 0);
					});
				}
				if (hashSet_0 == null)
				{
					hashSet_0 = new HashSet<GameObject>();
				}
				bool flag = !hitStruct_0.bool_10 && !hitStruct_0.bool_11 && !hitStruct_0.bool_8;
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit_2 = array2[i];
					GameObject gameObject = DoHit(raycastHit_2, hitStruct_0, hashSet_0);
					if (flag)
					{
						BloodAndHoleProcess(hitStruct_0.weaponSounds_0, raycastHit_2, bool_0);
					}
					if (gameObject != null || (!hitStruct_0.bool_14 && !hitStruct_0.bool_11))
					{
						continue;
					}
					if (hitStruct_0.bool_11)
					{
						float magnitude = (raycastHit_2.point - vector3_0).magnitude;
						myPlayerMoveC.AddFreezerRayWithLength(magnitude);
						if (bool_0)
						{
							myPlayerMoveC.SendAddFreezerRayWithLength(magnitude);
						}
					}
					break;
				}
				if (hitStruct_0.bool_16 && FirstPersonPlayerController.FirstPersonPlayerController_0 != null)
				{
					UseHook(hitStruct_0, array[0].point);
				}
				return array[0].point;
			}
			if (hitStruct_0.bool_16 && FirstPersonPlayerController.FirstPersonPlayerController_0 != null)
			{
				UseHook(hitStruct_0, ray.GetPoint(hitStruct_0.float_11), true);
			}
			return new Vector3(-999f, -999f, -999f);
		}
		return new Vector3(-999f, -999f, -999f);
	}

	public void ConeShoot(HitStruct hitStruct_0)
	{
		if (WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myPlayerMoveC == null)
		{
			return;
		}
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		List<GameObject> allAvailableAims = GetAllAvailableAims();
		GameObject gameObject = null;
		HashSet<GameObject> hashSet_ = new HashSet<GameObject>();
		for (int i = 0; i < allAvailableAims.Count; i++)
		{
			gameObject = allAvailableAims[i];
			if (gameObject.transform.position.Equals(myPlayerMoveC.GameObject_0.transform.position))
			{
				continue;
			}
			Vector3 to = gameObject.transform.position - myPlayerMoveC.GameObject_0.transform.position;
			if ((double)to.sqrMagnitude < Math.Pow(hitStruct_0.float_11, 2.0) && Vector3.Angle(myPlayerMoveC.gameObject.transform.forward, to) < hitStruct_0.float_12)
			{
				List<GameObject> colliderObject = getColliderObject(gameObject, !hitStruct_0.bool_8);
				for (int j = 0; j < colliderObject.Count; j++)
				{
					DoHitObject(hitStruct_0, colliderObject[j], hashSet_);
				}
			}
		}
	}

	public void RadiusShoot(HitStruct hitStruct_0, Vector3 vector3_0, RocketSettings rocketSettings_0 = null, HashSet<GameObject> hashSet_0 = null)
	{
		if (WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myPlayerMoveC == null)
		{
			return;
		}
		bool bool_ = Defs.bool_5;
		bool bool_2 = Defs.bool_6;
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		List<GameObject> allAvailableAims = GetAllAvailableAims();
		GameObject gameObject = null;
		if (hashSet_0 == null)
		{
			hashSet_0 = new HashSet<GameObject>();
		}
		for (int i = 0; i < allAvailableAims.Count; i++)
		{
			gameObject = allAvailableAims[i];
			bool flag = false;
			Player_move_c player = GetPlayer(gameObject);
			if (player != null && player.Boolean_5)
			{
				flag = true;
			}
			if ((bool_ || bool_2) && !flag && player != null && player.Int32_2 == myPlayerMoveC.Int32_2)
			{
				continue;
			}
			if (player == null && gameObject.CompareTag("Turret"))
			{
				TurretController component = gameObject.GetComponent<TurretController>();
				if (component == null || !component.Boolean_0)
				{
					continue;
				}
			}
			if (flag && !(hitStruct_0.float_8 > 0f))
			{
				continue;
			}
			float num = ((!flag) ? hitStruct_0.float_7 : hitStruct_0.float_8);
			Transform transform = gameObject.transform;
			Vector3 to = transform.position - vector3_0;
			if (!(to.sqrMagnitude <= num * num) || (hitStruct_0.float_12 > 1f && !(Vector3.Angle(myPlayerMoveC.gameObject.transform.forward, to) <= hitStruct_0.float_12)))
			{
				continue;
			}
			float float_ = 0f;
			if (num > 0.5f)
			{
				float num2 = Mathf.Abs((transform.position - vector3_0).magnitude);
				if (num2 > 0.5f)
				{
					float_ = (num2 - 0.5f) / (num - 0.5f);
				}
			}
			List<GameObject> colliderObject = getColliderObject(gameObject, false);
			for (int j = 0; j < colliderObject.Count; j++)
			{
				hitStruct_0.float_13 = float_;
				DoHitObject(hitStruct_0, colliderObject[j], hashSet_0, rocketSettings_0);
			}
		}
	}

	public void CollisionShoot(HitStruct hitStruct_0, GameObject gameObject_0, RocketSettings rocketSettings_0 = null)
	{
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			Player_move_c player = GetPlayer(gameObject_0);
			if (!(player != null) || !player.Equals(myPlayerMoveC))
			{
				HashSet<GameObject> hashSet_ = new HashSet<GameObject>();
				DoHitObject(hitStruct_0, gameObject_0, hashSet_, rocketSettings_0);
			}
		}
	}

	public GameObject GetMileeAim(HitStruct hitStruct_0)
	{
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			Ray randomRay = GetRandomRay();
			GameObject gameObject = null;
			RaycastHit hitInfo;
			if (Physics.Raycast(randomRay, out hitInfo, hitStruct_0.float_11, myPlayerMoveC.Int32_4) && hitInfo.collider != null && hitInfo.collider.gameObject != null)
			{
				gameObject = hitInfo.collider.gameObject;
			}
			if (gameObject == null)
			{
				List<GameObject> allAvailableAims = GetAllAvailableAims();
				GameObject gameObject2 = null;
				float num = float.PositiveInfinity;
				GameObject gameObject3 = null;
				for (int i = 0; i < allAvailableAims.Count; i++)
				{
					gameObject2 = allAvailableAims[i];
					if (!gameObject2.transform.position.Equals(myPlayerMoveC.GameObject_0.transform.position))
					{
						Vector3 to = gameObject2.transform.position - myPlayerMoveC.GameObject_0.transform.position;
						float sqrMagnitude = to.sqrMagnitude;
						if (sqrMagnitude < num && (((double)sqrMagnitude < Math.Pow(hitStruct_0.float_11, 2.0) && Vector3.Angle(myPlayerMoveC.gameObject.transform.forward, to) < hitStruct_0.float_12) || sqrMagnitude < 2.25f))
						{
							num = sqrMagnitude;
							gameObject3 = gameObject2;
						}
					}
				}
				if (gameObject3 != null)
				{
					List<GameObject> colliderObject = getColliderObject(gameObject3, false);
					if (colliderObject.Count > 0)
					{
						gameObject = colliderObject[0];
					}
				}
			}
			return gameObject;
		}
		return null;
	}

	public void ExplosionImpact(float float_2, Vector3 vector3_0)
	{
		GameObject myPlayer = WeaponManager.weaponManager_0.myPlayer;
		if (!(myPlayer == null))
		{
			ImpactReceiver impactReceiver = myPlayer.GetComponent<ImpactReceiver>();
			if (impactReceiver == null)
			{
				impactReceiver = myPlayer.AddComponent<ImpactReceiver>();
			}
			float num = 100f;
			if (float_2 != 0f)
			{
				num = (myPlayer.transform.position - vector3_0).magnitude / float_2;
			}
			float num2 = Mathf.Max(0f, 1f - num);
			float num3 = 133.4f * num2;
			impactReceiver.AddImpact(myPlayer.transform.position - vector3_0, num3);
		}
	}

	private GameObject tryHitTurret(HitStruct hitStruct_0, GameObject gameObject_0, HashSet<GameObject> hashSet_0 = null, bool bool_0 = false)
	{
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			if (gameObject_0.CompareTag("Turret"))
			{
				if (hashSet_0 != null && hashSet_0.Contains(gameObject_0))
				{
					return gameObject_0;
				}
				if (!bool_0)
				{
					if (hashSet_0 != null)
					{
						hashSet_0.Add(gameObject_0);
					}
					myPlayerMoveC.HitTurret(hitStruct_0, gameObject_0);
				}
				return gameObject_0;
			}
			return null;
		}
		return null;
	}

	private GameObject tryHitMonster(HitStruct hitStruct_0, GameObject gameObject_0, HashSet<GameObject> hashSet_0 = null, bool bool_0 = false)
	{
		if (gameObject_0.transform.parent == null)
		{
			return null;
		}
		Transform parent = gameObject_0.transform.parent;
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			if (parent.CompareTag("Enemy"))
			{
				if (hashSet_0 != null && hashSet_0.Contains(gameObject_0))
				{
					return gameObject_0;
				}
				if (!bool_0)
				{
					if (hashSet_0 != null)
					{
						hashSet_0.Add(gameObject_0);
					}
					myPlayerMoveC.HitZombie(hitStruct_0, gameObject_0);
				}
				return gameObject_0;
			}
			return null;
		}
		return null;
	}

	private GameObject tryHitPlayer(HitStruct hitStruct_0, GameObject gameObject_0, HashSet<GameObject> hashSet_0 = null, RocketSettings rocketSettings_0 = null, bool bool_0 = false)
	{
		if (gameObject_0.transform.parent == null)
		{
			return null;
		}
		if (!(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			Transform parent = gameObject_0.transform.parent;
			GameObject gameObject = parent.gameObject;
			if (gameObject.CompareTag("Player"))
			{
				if (hashSet_0 != null && hashSet_0.Contains(gameObject))
				{
					return gameObject;
				}
				if (!bool_0)
				{
					if (hashSet_0 != null)
					{
						hashSet_0.Add(gameObject);
					}
					myPlayerMoveC.HitPlayer(hitStruct_0, gameObject, gameObject_0, rocketSettings_0);
				}
				return gameObject;
			}
			return null;
		}
		return null;
	}

	public GameObject DoHitObject(HitStruct hitStruct_0, GameObject gameObject_0, HashSet<GameObject> hashSet_0 = null, RocketSettings rocketSettings_0 = null, bool bool_0 = false)
	{
		GameObject gameObject = tryHitTurret(hitStruct_0, gameObject_0, hashSet_0, bool_0);
		if (gameObject != null)
		{
			return gameObject;
		}
		gameObject = tryHitMonster(hitStruct_0, gameObject_0, hashSet_0, bool_0);
		if (gameObject != null)
		{
			return gameObject;
		}
		gameObject = tryHitPlayer(hitStruct_0, gameObject_0, hashSet_0, rocketSettings_0, bool_0);
		if (gameObject != null)
		{
			return gameObject;
		}
		return null;
	}

	public GameObject DoHit(RaycastHit raycastHit_0, HitStruct hitStruct_0, HashSet<GameObject> hashSet_0 = null, bool bool_0 = false)
	{
		if (!(raycastHit_0.collider == null) && !(raycastHit_0.collider.gameObject == null))
		{
			return DoHitObject(hitStruct_0, raycastHit_0.collider.gameObject, hashSet_0, null, bool_0);
		}
		return null;
	}

	private Player_move_c GetPlayer(GameObject gameObject_0)
	{
		if (gameObject_0.CompareTag("Player"))
		{
			SkinName component = gameObject_0.GetComponent<SkinName>();
			if (component != null)
			{
				return component.Player_move_c_0;
			}
		}
		return null;
	}

	private List<GameObject> getColliderObject(GameObject gameObject_0, bool bool_0)
	{
		List<GameObject> list = new List<GameObject>();
		if (gameObject_0.CompareTag("Turret"))
		{
			list.Add(gameObject_0);
		}
		else if (gameObject_0.CompareTag("Enemy"))
		{
			if (gameObject_0.transform.childCount > 0)
			{
				gameObject_0 = gameObject_0.transform.GetChild(0).gameObject;
				if (gameObject_0 != null && gameObject_0.GetComponent<BoxCollider>() != null)
				{
					list.Add(gameObject_0);
				}
			}
		}
		else if (gameObject_0.CompareTag("Player"))
		{
			Transform transform = gameObject_0.transform.Find("BodyCollider");
			if (transform != null && transform.gameObject.CompareTag("BodyCollider"))
			{
				list.Add(transform.gameObject);
			}
			if (bool_0)
			{
				transform = gameObject_0.transform.Find("HeadCollider");
				if (transform != null && transform.gameObject.CompareTag("HeadCollider"))
				{
					list.Add(transform.gameObject);
				}
			}
		}
		return list;
	}

	private Ray GetRandomRay(HitStruct hitStruct_0 = null)
	{
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		if (hitStruct_0 != null)
		{
			vector = hitStruct_0.vector2_0;
			vector2 = hitStruct_0.Vector2_0;
		}
		float x = (float)Screen.width * 0.5f + vector2.x - vector.x * Defs.Single_1 + (float)UnityEngine.Random.Range(0, Mathf.RoundToInt(vector.x * Defs.Single_0));
		float y = (float)Screen.height * 0.5f + vector2.y - vector.y * Defs.Single_1 + (float)UnityEngine.Random.Range(0, Mathf.RoundToInt(vector.y * Defs.Single_0));
		return Camera.main.ScreenPointToRay(new Vector3(x, y, 0f));
	}

	private void BloodAndHoleProcess(WeaponSounds weaponSounds_0, RaycastHit raycastHit_0, bool bool_0)
	{
		BulletType bulletType_ = BulletType.COMMON;
		if (weaponSounds_0 != null)
		{
			bulletType_ = weaponSounds_0.WeaponData_0.BulletType_0;
		}
		bool flag = false;
		Transform parent = raycastHit_0.collider.gameObject.transform.parent;
		if (raycastHit_0.collider.gameObject.CompareTag("Turret") || (parent != null && (parent.CompareTag("Enemy") || parent.CompareTag("Player"))))
		{
			flag = true;
		}
		Vector3 vector3_ = raycastHit_0.point + raycastHit_0.normal * 0.001f;
		Quaternion quaternion_ = Quaternion.FromToRotation(Vector3.up, raycastHit_0.normal);
		if (!flag)
		{
			HoleScript currentHole = HoleBulletStackController.holeBulletStackController_0.GetCurrentHole(bulletType_, true);
			if (currentHole != null)
			{
				currentHole.StartShowHole(vector3_, quaternion_, true);
			}
			WallBloodParticle currentParticle = WallParticleStackController.wallParticleStackController_0.GetCurrentParticle(bulletType_, true);
			if (currentParticle != null)
			{
				currentParticle.StartShowParticle(vector3_, quaternion_, true);
			}
		}
		else
		{
			WallBloodParticle currentParticle2 = BloodParticleStackController.bloodParticleStackController_0.GetCurrentParticle(bulletType_, true);
			if (currentParticle2 != null)
			{
				currentParticle2.StartShowParticle(vector3_, quaternion_, true);
			}
		}
		if (bool_0)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.SendHoleRPC(bulletType_, flag, vector3_, quaternion_);
		}
	}

	private void BulletProcess(WeaponSounds weaponSounds_0, bool bool_0, Vector3 vector3_0, int int_0, int int_1)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		WeaponData weaponData_ = weaponSounds_0.WeaponData_0;
		int num = ((!weaponSounds_0.WeaponData_0.Boolean_5) ? int_1 : (myPlayerMoveC.Int32_0 - 1));
		if (!weaponData_.Boolean_9 && !weaponData_.Boolean_10 && !weaponData_.Boolean_14)
		{
			WeaponEffectsController weaponEffectsController_ = weaponSounds_0.WeaponEffectsController_0;
			Transform transform_ = weaponEffectsController_.List_0[num];
			GameObject currentBullet = BulletStackController.bulletStackController_0.GetCurrentBullet(weaponData_.BulletType_0);
			if (currentBullet != null)
			{
				Bullet component = currentBullet.GetComponent<Bullet>();
				component.StartBullet(myPlayerMoveC.myTransform.rotation, transform_, vector3_0);
			}
		}
		myPlayerMoveC.SetActiveWeaponEffect(true, num);
		weaponSounds_0.Fire();
		if (!weaponData_.Boolean_9 && !weaponData_.Boolean_10)
		{
			bool boolean_ = weaponData_.Boolean_5;
			myPlayerMoveC.SetActiveEffects((!boolean_) ? int_1 : myPlayerMoveC.Int32_0);
		}
		else
		{
			myPlayerMoveC.SetActiveEffects();
		}
	}

	private void BulletProcessTurret(Vector3 vector3_0, Vector3 vector3_1)
	{
		GameObject currentBullet = BulletStackController.bulletStackController_0.GetCurrentBullet(BulletType.COMMON);
		if (currentBullet != null)
		{
			Bullet component = currentBullet.GetComponent<Bullet>();
			component.StartBullet(Quaternion.LookRotation(vector3_0, vector3_1), vector3_0, vector3_1);
		}
	}

	private List<GameObject> GetAllAvailableAims()
	{
		List<GameObject> list = new List<GameObject>();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
		}
		array = GameObject.FindGameObjectsWithTag("Enemy");
		if (array != null)
		{
			for (int j = 0; j < array.Length; j++)
			{
				list.Add(array[j]);
			}
		}
		array = GameObject.FindGameObjectsWithTag("Turret");
		if (array != null)
		{
			for (int k = 0; k < array.Length; k++)
			{
				list.Add(array[k]);
			}
		}
		return list;
	}

	private void UseHook(HitStruct hitStruct_0, Vector3 vector3_0, bool bool_0 = false)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		float magnitude = (myPlayerMoveC.myTransform.position - vector3_0).magnitude;
		if (!((double)magnitude < 0.5))
		{
			if (bool_0 && HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowHookMiss();
			}
			StartHookBullet(myPlayerMoveC, vector3_0, bool_0);
			if (myPlayerMoveC.Boolean_4)
			{
				myPlayerMoveC.PhotonView_0.RPC("StartHookRPC", PhotonTargets.Others, vector3_0, bool_0);
			}
		}
	}

	public void StartHookBullet(Player_move_c player_move_c_0, Vector3 vector3_0, bool bool_0 = false)
	{
		FirstPersonPlayerController firstPersonPlayerController_ = player_move_c_0.FirstPersonPlayerController_0;
		firstPersonPlayerController_.SetState(FirstPersonPlayerController.State.Default);
		WeaponSounds weaponSounds_ = player_move_c_0.WeaponSounds_0;
		WeaponEffectsController weaponEffectsController_ = weaponSounds_.WeaponEffectsController_0;
		Transform transform = weaponEffectsController_.List_0[0];
		GameObject currentBullet = BulletStackController.bulletStackController_0.GetCurrentBullet(BulletType.HOOK);
		if (!bool_0)
		{
			firstPersonPlayerController_.SetState(FirstPersonPlayerController.State.Hook);
			firstPersonPlayerController_.Vector3_0 = vector3_0 + (player_move_c_0.myPlayerTransform.position - transform.position);
		}
		if (currentBullet != null)
		{
			HookBullet component = currentBullet.GetComponent<HookBullet>();
			if (component != null)
			{
				component.StartBullet(player_move_c_0.myTransform.rotation, transform, vector3_0, player_move_c_0, bool_0);
			}
		}
	}
}
