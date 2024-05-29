using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.controllers;
using engine.events;
using engine.helpers;
using engine.network;
using engine.operations;
using engine.unity;

namespace pixelgun.tutorial
{
	public sealed class TutorialController
	{
		private static TutorialController tutorialController_0;

		private bool bool_0;

		[CompilerGenerated]
		private static Action action_0;

		public static TutorialController TutorialController_0
		{
			get
			{
				if (tutorialController_0 == null)
				{
					tutorialController_0 = new TutorialController();
				}
				return tutorialController_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0 && LocalTutorialData.Int32_0 != 0;
			}
		}

		public void Start()
		{
			bool_0 = true;
			LocalTutorialData.Int32_0 = 0;
			tutorialController_0.bool_0 &= AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU;
			if (bool_0)
			{
				MenuBackgroundMusic.menuBackgroundMusic_0.Stop();
				Step001();
				Weapon weaponFromSlot = WeaponManager.weaponManager_0.GetWeaponFromSlot(SlotType.SLOT_WEAPON_PRIMARY);
				if (weaponFromSlot != null)
				{
					UserController.UserController_0.UnequipArtikul(weaponFromSlot.WeaponSounds_0.WeaponData_0.Int32_0);
				}
			}
		}

		public void RegisterUIWidget(TUTORIAL_UI_IDS tutorial_UI_IDS_0, GameObject gameObject_0)
		{
			LocalTutorialData.SetTutorialUIElement(tutorial_UI_IDS_0, gameObject_0);
		}

		private void Step001()
		{
			PresetGameSettings.PresetGameSettings_0.Single_0 = 50f;
			PresetGameSettings.PresetGameSettings_0.Save();
			Log.AddLine("[TutorialController::Step001]");
			if (!LocalTutorialData.Boolean_0)
			{
				FinalStep();
				return;
			}
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(BlockUserInput), new ActionOperation(IncrementStepCounter), new ActionOperation(StartTutorialFight), new WaitLoadSceneOperation(LocalTutorialData.String_0), new ActionOperation(SetPlayerPosition), new ActionOperation(HideGrenadeBonus), new ActionOperation(Step002));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step002()
		{
			Log.AddLine("[TutorialController::Step002]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(delegate
			{
				Tutorial.Show();
				Tutorial.Tutorial_0.Step002();
			}), new WaitUserInputAxisOperation(WaitUserInputAxisOperation.AXIS_ROTATES.Left), new WaitOperation(1f), new ActionOperation(Step002_1));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step002_1()
		{
			Log.AddLine("[TutorialController::Step002]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step002_1), new WaitUserInputAxisOperation(WaitUserInputAxisOperation.AXIS_ROTATES.Right), new WaitOperation(1f), new ActionOperation(Step002_2));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step002_2()
		{
			Log.AddLine("[TutorialController::Step002]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step002_2), new WaitUserInputAxisOperation(WaitUserInputAxisOperation.AXIS_ROTATES.Up), new WaitOperation(1f), new ActionOperation(Step002_3));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step002_3()
		{
			Log.AddLine("[TutorialController::Step002]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step002_3), new WaitUserInputAxisOperation(WaitUserInputAxisOperation.AXIS_ROTATES.Down), new WaitOperation(1f), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1f), new ActionOperation(Step003));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step003()
		{
			Log.AddLine("[TutorialController::Step003]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step003), new ActionOperation(UnblockUserInputWASD), new ActionOperation(ShowWalkPoint), new CheckDistanceOperation(LocalTutorialData.GameObject_0, LocalTutorialData.GameObject_3, 2.5f), new ActionOperation(HideWaypointArrow), new ActionOperation(DeleteTutorialArrow), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1f), new ActionOperation(Step004));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step004()
		{
			Log.AddLine("[TutorialController::Step004]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step004), new ActionOperation(CreateWeaponBonus), new WaitTakeWeaponBonusOperation(LocalTutorialData.int_2), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new ActionOperation(HideWaypointArrow), new ActionOperation(DeleteTutorialArrow), new WaitOperation(1f), new ActionOperation(Step005));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step005()
		{
			Log.AddLine("[TutorialController::Step005]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step005), new ActionOperation(ShowBoxPoint1), new CheckDistanceOperation(LocalTutorialData.GameObject_0, LocalTutorialData.GameObject_4, 2f), new ActionOperation(HideWaypointArrow), new ActionOperation(DeleteTutorialArrow), new ActionOperation(Tutorial.Tutorial_0.HideAllSteps), new WaitOperation(0.5f), new ActionOperation(UnblockUserInputJump), new ActionOperation(Tutorial.Tutorial_0.Step005_1), new ActionOperation(ShowBoxPoint2), new CheckDistanceOperation(LocalTutorialData.GameObject_0, LocalTutorialData.GameObject_5, 2f), new ActionOperation(HideWaypointArrow), new ActionOperation(DeleteTutorialArrow), new ActionOperation(Tutorial.Tutorial_0.Step005_1_1), new ActionOperation(ShowGrenadeBonus), new WaitTakeCoinBonusOperation(), new ActionOperation(HideWaypointArrow), new ActionOperation(AddGrenade), new ActionOperation(DeleteTutorialArrow), new ActionOperation(Tutorial.Tutorial_0.Step005_2), new WaitOperation(1f), new ActionOperation(Step006));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step006()
		{
			Log.AddLine("[TutorialController::Step006]");
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0.Subscribe(StartSpawnAmmoBonus, PlayerEvents.NoAmmo);
			DependSceneEvent<EventCreateEnemy, GameObject>.GlobalSubscribe(OnZombieCreated, true);
			DependSceneEvent<EventDeathEnemy, GameObject>.GlobalSubscribe(OnZombieDeath, true);
			LocalTutorialData.dictionary_1.Clear();
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step006), new ActionOperation(StartSpawnFirstWaveEnemy), new ActionOperation(UnblockUserInputShoot), new WaitNextWaveOperation(), new ActionOperation(BlockUserInputShoot), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1.5f), new ActionOperation(Step007));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step007()
		{
			Log.AddLine("[TutorialController::Step007]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step007), new ActionOperation(UnblockUserInputChangeWeapon), new WaitUserInputButtonOperation(WaitUserInputButtonOperation.ButtonState.Up, "Weapon2"), new ActionOperation(Tutorial.Tutorial_0.HideAllSteps), new ActionOperation(StartSpawnAmmoBonus), new ActionOperation(ShowAmmoPoint), new ActionOperation(Tutorial.Tutorial_0.Step007_1), new CheckDistanceOperation(LocalTutorialData.GameObject_0, LocalTutorialData.GameObject_6, 1.5f), new ActionOperation(HideWaypointArrow), new ActionOperation(DeleteTutorialArrow), new ActionOperation(Tutorial.Tutorial_0.Step006_1), new ActionOperation(UnblockUserInputReload), new WaitUserInputButtonOperation(WaitUserInputButtonOperation.ButtonState.Up, "Reload"), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1f), new ActionOperation(Step008));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step008()
		{
			Log.AddLine("[TutorialController::Step008]");
			LocalTutorialData.dictionary_1.Clear();
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(UnblockUserInputShoot), new ActionOperation(Tutorial.Tutorial_0.Step008), new ActionOperation(StartSpawnSecondtWaveEnemy), new WaitNextWaveOperation(), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1f), new ActionOperation(Step009));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step009()
		{
			Log.AddLine("[TutorialController::Step009]");
			if (WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_16)
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.ZoomPress();
			}
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0.Unsubscribe(StartSpawnAmmoBonus, PlayerEvents.NoAmmo);
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0.Subscribe(AddGrenade, PlayerEvents.GrenadeFire);
			LocalTutorialData.dictionary_1.Clear();
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step009), new ActionOperation(StartSpawnThirdtWaveEnemy), new ActionOperation(UnblockUserInputGrenadeWeapon), new WaitFinalLastEnemyWaveOperation(), new ActionOperation(Tutorial.Tutorial_0.Step004_1), new WaitOperation(1f), new ActionOperation(Step010));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step010()
		{
			Log.AddLine("[TutorialController::Step010]");
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0.Unsubscribe(AddGrenade, PlayerEvents.GrenadeFire);
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step010), new ActionOperation(ShowPortal), new WaitLoadSceneOperation(Defs.String_11), new ActionOperation(DestroyWaypointArrow), new ActionOperation(Step011));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step011()
		{
			Log.AddLine("[TutorialController::Step011]");
			DependSceneEvent<EventCreateEnemy, GameObject>.GlobalUnsubscribe(OnZombieCreated);
			DependSceneEvent<EventDeathEnemy, GameObject>.GlobalUnsubscribe(OnZombieDeath);
			LocalTutorialData.dictionary_1.Clear();
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(BlockInputProcessUIInTutorialMode), new ActionOperation(Tutorial.Tutorial_0.Step011), new ActionOperation(UnblockInputForOpenShopButtonInTutorialMode), new WaitShowWindowOperation(GameWindowType.Shop, true), new ActionOperation(Step012));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void Step012()
		{
			Log.AddLine("[TutorialController::Step012]");
			SeveralOperations operation_ = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.Step012), new ActionOperation(OpenShopRifleItem), new WaitOperation(0.1f), new ActionOperation(UnblockInputForShopRifleItemInTutorialMode), new WaitArtikulUserInvOperation(LocalTutorialData.int_0), new ActionOperation(UnblockInputForShopRifleItemInNGUIMode), new ActionOperation(Tutorial.Tutorial_0.Step012_1), new ActionOperation(UnblockInputForCloseShopButtonInTutorialMode), new WaitShowWindowOperation(GameWindowType.Shop, false), new ActionOperation(UnblockInputForCloseShopButtonInNGUIMode), new ActionOperation(FinalStep));
			OperationsManager.OperationsManager_0.Add(operation_);
		}

		private void FinalStep()
		{
			UnblockUserInputBack();
			Log.AddLine("[TutorialController::FinalStep]");
			LocalTutorialData.severalOperations_0 = new SeveralOperations(new ActionOperation(IncrementStepCounter), new ActionOperation(Tutorial.Tutorial_0.FinalStep), new ActionOperation(FinalTutorial), new WaitOperation(4.5f), new ActionOperation(UnblockInputForOpenSelectMapButtonInTutorialMode), new ActionOperation(Tutorial.Tutorial_0.PostFinalStep001), new WaitShowWindowOperation(GameWindowType.SelectMap, true), new WaitOperation(1f), new ActionOperation(UnblockInputForFightBtnInTutorialMode), new ActionOperation(Tutorial.Tutorial_0.PostFinalStep002), new WaitShowWindowOperation(GameWindowType.Loading, true), new ActionOperation(HideTutor));
			OperationsManager.OperationsManager_0.Add(LocalTutorialData.severalOperations_0);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.BATTLE_BUTTON);
				GameObject tutorialUIElement2 = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.STAR_RANDOM_BATTLE_BUTTON);
				if (!(UICamera.gameObject_6 == tutorialUIElement) && !(UICamera.gameObject_6 == tutorialUIElement2))
				{
					HideTutor();
				}
				else
				{
					Tutorial.Tutorial_0.HideAllSteps();
				}
			}
		}

		private void StartTutorialFight()
		{
			FightOfflineController.FightOfflineController_0.StartFight(LocalTutorialData.WaveMonstersData_0.Int32_0);
		}

		public void FinalTutorial()
		{
			if (Storager.GetBool(Defs.String_1, true))
			{
				MenuBackgroundMusic.menuBackgroundMusic_0.Play();
			}
			WeaponManager.weaponManager_0.Reset();
			LocalTutorialData.Int32_0++;
			SendTutorProgress(LocalTutorialData.Int32_0, true);
			UsersData.UsersData_0.UserData_0.user_0.bool_1 = true;
			WindowController.WindowController_0.DispatchEvent(WindowController.GameEvent.TUTORIAL_END);
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}

		public void HideTutor()
		{
			if (LocalTutorialData.severalOperations_0 != null)
			{
				LocalTutorialData.severalOperations_0.Boolean_4 = true;
				LocalTutorialData.severalOperations_0.Complete();
				LocalTutorialData.severalOperations_0 = null;
			}
			UnblockInputForOpenShopButtonInNGUIMode();
			UnblockInputForOpenSelectMapButtonInNGUIMode();
			UnblockInputForFightBtnInNGUIMode();
			UnblockInputProcessUIInNGUIMode();
			UnblockUserInput();
			Tutorial.Hide();
			bool_0 = false;
			LocalTutorialData.Int32_0 = 0;
			if (DependSceneEvent<MainUpdate>.Contains(Update))
			{
				DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			}
		}

		private void SendTutorProgress(int int_0, bool bool_1)
		{
			TutorialStepNetworkCommand tutorialStepNetworkCommand = new TutorialStepNetworkCommand();
			tutorialStepNetworkCommand.int_1 = int_0;
			tutorialStepNetworkCommand.bool_0 = bool_1;
			AbstractNetworkCommand.Send(tutorialStepNetworkCommand);
		}

		private void SetPlayerPosition()
		{
			LocalTutorialData.GameObject_0 = WeaponManager.weaponManager_0.myPlayer;
			Transform transform = LocalTutorialData.GameObject_0.transform;
			transform.position = new Vector3(9f, 1.75f, -13f);
			transform.rotation = Quaternion.LookRotation(Vector3.left);
			TutorialWayPointArrow.TutorialWayPointArrow_0.InitArrow();
			Weapon weaponFromSlot = WeaponManager.weaponManager_0.GetWeaponFromSlot(SlotType.SLOT_WEAPON_BACKUP);
			if (weaponFromSlot != null)
			{
				weaponFromSlot.Int32_1 = 1;
				weaponFromSlot.Int32_0 = 0;
				StopSpawnAmmoBonus();
			}
		}

		private void ShowGrenadeBonus()
		{
			LocalTutorialData.GameObject_2.SetActive(true);
			GameObject gameObject_ = LocalTutorialData.GameObject_2;
			Vector3 position = gameObject_.transform.position;
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject_, position);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject_);
		}

		private void ShowWalkPoint()
		{
			LocalTutorialData.GameObject_3.SetActive(true);
			GameObject gameObject_ = LocalTutorialData.GameObject_3;
			Vector3 position = gameObject_.transform.position;
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject_, position);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject_);
		}

		private void ShowBoxPoint1()
		{
			LocalTutorialData.GameObject_4.SetActive(true);
			GameObject gameObject_ = LocalTutorialData.GameObject_4;
			Vector3 position = gameObject_.transform.position;
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject_, position);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject_);
		}

		private void ShowBoxPoint2()
		{
			LocalTutorialData.GameObject_5.SetActive(true);
			GameObject gameObject_ = LocalTutorialData.GameObject_5;
			Vector3 position = gameObject_.transform.position;
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject_, position);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject_);
		}

		private void ShowAmmoPoint()
		{
			LocalTutorialData.GameObject_3.SetActive(true);
			GameObject gameObject_ = LocalTutorialData.GameObject_6;
			Vector3 position = gameObject_.transform.position;
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject_, position);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject_);
		}

		private void ShowPortal()
		{
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(GameObject.FindGameObjectWithTag("Portal"));
		}

		public void HideWaypointArrow()
		{
			if (TutorialWayPointArrow.TutorialWayPointArrow_0 != null)
			{
				TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(false);
			}
		}

		private void DestroyWaypointArrow()
		{
			TutorialWayPointArrow.TutorialWayPointArrow_0.DestroyArrow();
		}

		private void HideGrenadeBonus()
		{
			LocalTutorialData.GameObject_2.SetActive(false);
		}

		private void StartSpawnAmmoBonus()
		{
			BonusMapController.bonusMapController_0.GetPoint("BonusCreationZone1").enabled = true;
			BonusMapController.bonusMapController_0.GetPoint("BonusCreationZone1").SetZeroTime();
		}

		private void StopSpawnAmmoBonus()
		{
			BonusMapController.bonusMapController_0.GetPoint("BonusCreationZone1").enabled = false;
		}

		private void CreateWeaponBonus()
		{
			Vector3 vector3_ = new Vector3(-8.5f, 0.25f, -13f);
			GameObject gameObject = BonusMapController.bonusMapController_0.AddBonus(vector3_, LocalTutorialData.int_3);
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject, vector3_);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject);
		}

		private void CreateWeaponSniperBonus()
		{
			Vector3 vector3_ = new Vector3(0f, 0.25f, -13f);
			MapBonusItemStorage.Get.Storage.GetObjectByKey(8);
			GameObject gameObject = BonusMapController.bonusMapController_0.AddBonus(vector3_, 8);
			LocalTutorialData.gameObject_5 = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetBonuses", gameObject, vector3_);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(gameObject);
		}

		private void DeleteTutorialArrow()
		{
			UnityEngine.Object.Destroy(LocalTutorialData.gameObject_5);
		}

		private void IncrementStepCounter()
		{
			LocalTutorialData.Int32_0++;
			SendTutorProgress(LocalTutorialData.Int32_0, false);
		}

		private void BlockUserInput()
		{
			InputManager.SetActiveButton("Back", false);
			InputManager.SetActiveButton("Weapon1", false);
			InputManager.SetActiveButton("Weapon2", false);
			InputManager.SetActiveButton("Weapon3", false);
			InputManager.SetActiveButton("Weapon4", false);
			InputManager.SetActiveButton("Weapon5", false);
			InputManager.SetActiveButton("Weapon6", false);
			InputManager.SetActiveButton("Attack", false);
			InputManager.SetActiveButton("Grenade", false);
			InputManager.SetActiveButton("Next", false);
			InputManager.SetActiveButton("Accept", false);
			InputManager.SetActiveButton("Chat", false);
			InputManager.SetActiveButton("Mouse ScrollWheel", false);
			InputManager.SetActiveButton("ShowBugReport", false);
			InputManager.SetActiveButton("Jump", false);
			InputManager.SetActiveButton("Reload", false);
			InputManager.SetActiveButton("WeaponChange", false);
			InputManager.SetActiveAxis("Vertical", false);
			InputManager.SetActiveAxis("Horizontal", false);
		}

		private void UnblockUserInput()
		{
			InputManager.SetActiveButton("Back", true);
			InputManager.SetActiveButton("Weapon1", true);
			InputManager.SetActiveButton("Weapon2", true);
			InputManager.SetActiveButton("Weapon3", true);
			InputManager.SetActiveButton("Weapon4", true);
			InputManager.SetActiveButton("Weapon5", true);
			InputManager.SetActiveButton("Weapon6", true);
			InputManager.SetActiveButton("Attack", true);
			InputManager.SetActiveButton("Grenade", true);
			InputManager.SetActiveButton("Next", true);
			InputManager.SetActiveButton("Accept", true);
			InputManager.SetActiveButton("Chat", true);
			InputManager.SetActiveButton("Mouse ScrollWheel", true);
			InputManager.SetActiveButton("ShowBugReport", true);
			InputManager.SetActiveButton("Jump", true);
			InputManager.SetActiveButton("Reload", true);
			InputManager.SetActiveButton("WeaponChange", true);
			InputManager.SetActiveAxis("Vertical", true);
			InputManager.SetActiveAxis("Horizontal", true);
		}

		private void UnblockUserInputWASD()
		{
			InputManager.SetActiveAxis("Vertical", true);
			InputManager.SetActiveAxis("Horizontal", true);
		}

		private void UnblockUserInputJump()
		{
			InputManager.SetActiveButton("Jump", true);
		}

		private void UnblockUserInputReload()
		{
			InputManager.SetActiveButton("Reload", true);
		}

		private void UnblockUserInputChangeWeapon()
		{
			InputManager.SetActiveButton("Weapon2", true);
		}

		private void UnblockUserInputBack()
		{
			InputManager.SetActiveButton("Back", true);
		}

		private void UnblockUserInputShoot()
		{
			InputManager.SetActiveButton("Attack", true);
		}

		private void BlockUserInputShoot()
		{
			InputManager.SetActiveButton("Attack", false);
		}

		private void UnblockUserInputGrenadeWeapon()
		{
			InputManager.SetActiveButton("Attack", false);
			InputManager.SetActiveButton("Reload", false);
			InputManager.SetActiveButton("Grenade", true);
		}

		private void StartSpawnFirstWaveEnemy()
		{
			LocalTutorialData.int_1 = 1;
			ZombieCreator.zombieCreator_0.StateNextWaveType_0 = ZombieCreator.StateNextWaveType.Manual;
			ZombieCreator.zombieCreator_0.BeganCreateEnemies();
		}

		private void OnZombieCreated(GameObject gameObject_0)
		{
			GameObject gameObject = Tutorial3DArrowCreator.Tutorial3DArrowCreator_0.CreateArrow("TargetZombieHead", gameObject_0, gameObject_0.transform.position);
			gameObject.transform.parent = gameObject_0.transform;
			BotHealth component = gameObject_0.GetComponent<BotHealth>();
			LocalTutorialData.dictionary_1.Add(component.gameObject, gameObject);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetArrowActive(true);
			TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(component.gameObject);
		}

		private void OnZombieDeath(GameObject gameObject_0)
		{
			List<GameObject> list = new List<GameObject>(LocalTutorialData.dictionary_1.Keys);
			foreach (GameObject item in list)
			{
				if (item != null && item.gameObject == gameObject_0)
				{
					UnityEngine.Object.Destroy(LocalTutorialData.dictionary_1[item]);
					LocalTutorialData.dictionary_1.Remove(item);
				}
			}
			if (LocalTutorialData.dictionary_1.Count == 0)
			{
				DependSceneEvent<EventKillAllEnemyInWave>.GlobalDispatch();
				HideWaypointArrow();
			}
			else
			{
				TutorialWayPointArrow.TutorialWayPointArrow_0.SetTarget(list.First());
			}
		}

		private void StartSpawnSecondtWaveEnemy()
		{
			LocalTutorialData.int_1 = 2;
			ZombieCreator.zombieCreator_0.StartNextWave();
		}

		private void StartSpawnThirdtWaveEnemy()
		{
			LocalTutorialData.int_1 = 3;
			ZombieCreator.zombieCreator_0.StartNextWave();
		}

		private void StartSpawnFourWaveEnemy()
		{
			LocalTutorialData.int_1 = 4;
			ZombieCreator.zombieCreator_0.StartNextWave();
		}

		private void BlockInputProcessUIInTutorialMode()
		{
			LocalTutorialData.InitUIElements();
			LocalTutorialData.UICamera_0.eventReceiverMask = LayerMask.GetMask(LocalTutorialData.string_1);
		}

		private void UnblockInputProcessUIInNGUIMode()
		{
			LocalTutorialData.UICamera_0.eventReceiverMask = LayerMask.GetMask(LocalTutorialData.string_0);
		}

		private void UnblockInputForOpenShopButtonInTutorialMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_SHOP_BUTTON);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_1));
		}

		private void UnblockInputForOpenShopButtonInNGUIMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_SHOP_BUTTON);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_0));
		}

		private void UnblockInputForOpenSelectMapButtonInTutorialMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_LOBBY);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_1));
		}

		private void UnblockInputForOpenSelectMapButtonInNGUIMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_LOBBY);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_0));
		}

		private void UnblockInputForFightBtnInTutorialMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_SELECT_MAP);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_1));
		}

		private void UnblockInputForFightBtnInNGUIMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.MAIN_MENU_OPEN_SELECT_MAP);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_0));
		}

		private void OpenShopRifleItem()
		{
			GameObject gameObject = ShopWindow.ShopWindow_0.OpenItem(LocalTutorialData.int_0);
			ShopItem component = gameObject.GetComponent<ShopItem>();
			if (component.GameObject_0.GetComponent<ShopItemStateBuy>() != null)
			{
				LocalTutorialData.GameObject_1 = component.GameObject_0.GetComponent<ShopItemStateBuy>().shopItemButton_0.gameObject;
			}
		}

		private void UnblockInputForShopRifleItemInTutorialMode()
		{
			Utility.SetLayerRecursive(LocalTutorialData.GameObject_1, LayerMask.NameToLayer(LocalTutorialData.string_1));
		}

		private void UnblockInputForShopRifleItemInNGUIMode()
		{
			Utility.SetLayerRecursive(LocalTutorialData.GameObject_1, LayerMask.NameToLayer(LocalTutorialData.string_0));
		}

		private void UnblockInputForCloseShopButtonInTutorialMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.SHOP_WINDOW_CLOSE_BUTTON);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_1));
		}

		private void UnblockInputForCloseShopButtonInNGUIMode()
		{
			GameObject tutorialUIElement = LocalTutorialData.GetTutorialUIElement(TUTORIAL_UI_IDS.SHOP_WINDOW_CLOSE_BUTTON);
			Utility.SetLayerRecursive(tutorialUIElement, LayerMask.NameToLayer(LocalTutorialData.string_0));
		}

		private void AddGrenade()
		{
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpGrenade(UserController.UserController_0.UserData_0.user_0.int_0, 1);
		}

		public void CheatCompleteTutor()
		{
			bool_0 = false;
			LocalTutorialData.Int32_0++;
			FinalTutorial();
		}
	}
}
