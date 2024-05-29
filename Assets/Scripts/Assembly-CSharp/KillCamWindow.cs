using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.RespawnBattle)]
public class KillCamWindow : BaseGameWindow
{
	private static KillCamWindow killCamWindow_0;

	public GameObject gameObject_0;

	public Camera camera_0;

	public UITexture uitexture_0;

	public CharacterView characterView_0;

	public GameObject gameObject_1;

	public GameObject gameObject_2;

	public GameObject gameObject_3;

	public UISprite uisprite_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	public UILabel uilabel_4;

	public UILabel uilabel_5;

	public UILabel uilabel_6;

	public UILabel uilabel_7;

	public UILabel uilabel_8;

	public UISprite uisprite_1;

	public UILabel uilabel_9;

	public UILabel uilabel_10;

	public UISprite uisprite_2;

	public UISprite uisprite_3;

	public GameObject gameObject_4;

	public KillCamShopItem killCamShopItem_0;

	public KillCamShopItem killCamShopItem_1;

	public KillCamShopItem killCamShopItem_2;

	public KillCamShopItem killCamShopItem_3;

	public KillCamShopItem killCamShopItem_4;

	public KillCamConsShopItem[] killCamConsShopItem_0;

	public GameObject gameObject_5;

	public UISprite uisprite_4;

	public GameObject gameObject_6;

	public UITexture uitexture_1;

	public UILabel uilabel_11;

	public UIButton uibutton_0;

	public GameObject gameObject_7;

	public UILabel uilabel_12;

	public UIButton uibutton_1;

	public UILabel uilabel_13;

	public UIWidget uiwidget_0;

	public UILabel uilabel_14;

	public UISprite uisprite_5;

	public UIButton uibutton_2;

	public UISlider uislider_0;

	public UILabel uilabel_15;

	public UISlider uislider_1;

	public UILabel uilabel_16;

	public UIWidget uiwidget_1;

	private KillerInfo killerInfo_0 = new KillerInfo();

	private UserArtikul userArtikul_0;

	private string string_0 = Localizer.Get("ui.day.mini");

	private string string_1 = Localizer.Get("ui.hour.mini");

	private string string_2 = Localizer.Get("ui.min.mini");

	private string string_3 = Localizer.Get("ui.sec.mini");

	private TimerData timerData_0 = new TimerData
	{
		string_0 = "ui.kill_cam.auto_respawn_block",
		string_1 = "ui.kill_cam.press_space_to_respawn_block",
		color_0 = new Color(1f, 0.26f, 0f)
	};

	private TimerData timerData_1 = new TimerData
	{
		string_0 = "ui.kill_cam.auto_respawn_in",
		string_1 = "ui.kill_cam.press_space_to_respawn",
		color_0 = Color.white
	};

	private ShopArtikulData shopArtikulData_0;

	private bool bool_1;

	private float float_0;

	private float float_1;

	private float float_2;

	private bool bool_2;

	private float float_3;

	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private static Comparison<ShopArtikulData> comparison_0;

	public static KillCamWindow KillCamWindow_0
	{
		get
		{
			return killCamWindow_0;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		private set
		{
			bool_3 = value;
		}
	}

	public static bool Boolean_2
	{
		get
		{
			return KillCamWindow_0 != null && KillCamWindow_0.Boolean_0;
		}
	}

	public static void Show(RespawnBattleWindowParams respawnBattleWindowParams_0)
	{
		if (!(BattleOverWindow.BattleOverWindow_0 != null) && !(killCamWindow_0 != null) && respawnBattleWindowParams_0 != null)
		{
			if (respawnBattleWindowParams_0 != null && respawnBattleWindowParams_0.killerInfo_0 != null)
			{
				killCamWindow_0 = BaseWindow.Load("KillCamWindow") as KillCamWindow;
				killCamWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
				killCamWindow_0.Parameters_0.bool_5 = false;
				killCamWindow_0.Parameters_0.bool_0 = false;
				killCamWindow_0.Parameters_0.bool_6 = false;
				killCamWindow_0.InternalShow(respawnBattleWindowParams_0);
			}
		}
	}

	public override void OnShow()
	{
		Player_move_c.SetBlockKeyboardControl(true, true);
		if (BattleStatWindow.Boolean_1)
		{
			BattleStatWindow.BattleStatWindow_0.HideMe();
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.HideElements();
		}
		RespawnBattleWindowParams respawnBattleWindowParams = base.WindowShowParameters_0 as RespawnBattleWindowParams;
		respawnBattleWindowParams.killerInfo_0.CopyTo(killerInfo_0);
		NickLabelStack.nickLabelStack_0.SetCameraActive(false);
		Defs.bool_11 = true;
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		Defs.bool_11 = false;
		Player_move_c.SetBlockKeyboardControl(false, true);
		killCamWindow_0 = null;
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowElements();
		}
		NickLabelStack.nickLabelStack_0.SetCameraActive(true);
	}

	public void OnMoneyButtonClick()
	{
		BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_KILLCAM);
	}

	private void Start()
	{
		KillerInfo killerInfo = (base.WindowShowParameters_0 as RespawnBattleWindowParams).killerInfo_0;
		Boolean_1 = killerInfo.bool_0;
		SetKillType(killerInfo);
		if (killerInfo.bool_0)
		{
			uilabel_8.String_0 = Localizer.Get("ui.kill_cam.nick_suicide");
			gameObject_4.SetActive(false);
			gameObject_3.SetActive(false);
			uilabel_9.gameObject.SetActive(false);
			uisprite_2.gameObject.SetActive(false);
			TopPanelUp();
			gameObject_0.SetActive(false);
			uitexture_0.gameObject.SetActive(false);
			gameObject_1.SetActive(false);
			gameObject_2.SetActive(false);
			uislider_0.gameObject.transform.parent.gameObject.SetActive(false);
			uislider_1.gameObject.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			uilabel_8.String_0 = killerInfo.string_0;
			int iKill = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.GetIKill(killerInfo.int_1);
			int iKilled = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.GetIKilled(killerInfo.int_1);
			uilabel_2.String_0 = iKill.ToString();
			uilabel_3.String_0 = iKilled.ToString();
			uilabel_0.String_0 = UserController.UserController_0.UserData_0.user_0.string_0;
			uilabel_1.String_0 = killerInfo.string_0;
			float num = Mathf.Max(uilabel_0.Int32_0, uilabel_1.Int32_0);
			if (num > 270f)
			{
				float f = num - 270f;
				uisprite_0.Int32_0 += Mathf.FloorToInt(f);
			}
			uislider_0.Single_0 = (float)killerInfo.int_7 / (float)killerInfo.int_8;
			uilabel_15.String_0 = killerInfo.int_7 + "/" + killerInfo.int_8;
			uislider_0.gameObject.SetActive(killerInfo.int_7 > 0);
			uislider_1.Single_0 = (float)killerInfo.int_9 / (float)killerInfo.int_10;
			uilabel_16.String_0 = killerInfo.int_9 + "/" + killerInfo.int_10;
			uislider_1.gameObject.SetActive(killerInfo.int_9 > 0);
			uislider_1.gameObject.transform.parent.gameObject.SetActive(killerInfo.int_10 > 0);
			SetVillain();
			if (!killerInfo.bool_3)
			{
				killCamShopItem_0.SetData(killerInfo.int_0, killerInfo.texture_2);
			}
			else
			{
				killCamShopItem_0.SetData(killerInfo.int_2, killerInfo.texture_2);
			}
			killCamShopItem_1.SetData(killerInfo.int_6, killerInfo.texture_2);
			killCamShopItem_2.SetData(killerInfo.int_4, killerInfo.texture_2);
			killCamShopItem_3.SetData(killerInfo.int_3, killerInfo.texture_2);
			killCamShopItem_4.SetData(killerInfo.int_5, killerInfo.texture_2);
		}
		List<ShopArtikulData> cons = GetCons();
		cons = null;
		SetConsPanel(cons);
		ShopArtikulData superPanel = GetSuper();
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			superPanel = null;
		}
		SetSuperPanel(superPanel);
		timerData_0.Single_0 = ((MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DUEL) ? 6f : 4f);
		timerData_1.Single_0 = ((MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DUEL) ? 31f : 29f);
		uibutton_1.Boolean_0 = false;
		float_0 = uilabel_4.transform.localPosition.y;
		float_1 = uilabel_5.transform.localPosition.y;
		float_2 = uilabel_7.transform.localPosition.y;
		if (uibutton_2 != null)
		{
			uibutton_2.gameObject.SetActive(killerInfo.int_1 != 0);
		}
		InitRankTrophy();
	}

	private new void Update()
	{
		if (!BankController.BankController_0.Boolean_0)
		{
			keyboardUpdate();
			positionsUpdate();
			UpdateRespawnTimer();
		}
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(TickOneSecond);
	}

	private void SetSuperPanel(ShopArtikulData shopArtikulData_1)
	{
		if (shopArtikulData_1 == null)
		{
			gameObject_6.SetActive(false);
			return;
		}
		if (shopArtikulData_1.GetArtikul().Int32_5 > 0)
		{
			uisprite_5.String_0 = "blue_plate_bkg";
			UserArtikul userArtikulByArtikulId = UserController.UserController_0.GetUserArtikulByArtikulId(shopArtikulData_1.GetArtikul().Int32_0);
			if (userArtikulByArtikulId == null)
			{
				gameObject_7.SetActive(false);
				uilabel_12.gameObject.SetActive(true);
			}
			else
			{
				gameObject_7.SetActive(true);
				uilabel_12.gameObject.SetActive(false);
			}
		}
		else
		{
			uisprite_5.String_0 = "green_plate";
			gameObject_7.SetActive(true);
			uilabel_12.gameObject.SetActive(false);
		}
		uitexture_1.Texture_0 = ImageLoader.LoadArtikulTexture(shopArtikulData_1.Int32_1);
		uilabel_11.String_0 = shopArtikulData_1.GetPrice().ToString();
		shopArtikulData_0 = shopArtikulData_1;
		uiwidget_0.gameObject.SetActive(false);
		if (shopArtikulData_0.GetArtikul().Int32_5 > 0)
		{
			uiwidget_0.gameObject.SetActive(true);
			uilabel_14.String_0 = Utility.GetLocalizedTime(shopArtikulData_0.GetArtikul().Int32_5, string_0, string_1, string_2, string_3, false);
		}
		TooltipInfo component = gameObject_6.GetComponent<TooltipInfo>();
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(shopArtikulData_0.GetArtikul().Int32_0);
		if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
		{
			component.weaponID = shopArtikulData_0.GetArtikul().Int32_0;
		}
		else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
		{
			component.wearID = shopArtikulData_0.GetArtikul().Int32_0;
		}
	}

	private void SetKillType(KillerInfo killerInfo_1)
	{
		WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(killerInfo_1.int_0);
		int key = 0;
		if (killerInfo_1.bool_0 && killerInfo_1.bool_1)
		{
			key = 2;
		}
		else if (killerInfo_1.bool_0 && killerInfo_1.bool_4)
		{
			key = 3;
		}
		else if (killerInfo_1.bool_0)
		{
			key = 1;
		}
		else if (killerInfo_1.bool_5 && weapon != null && weapon.Boolean_2 && !weapon.Boolean_3)
		{
			key = 5;
		}
		else if (killerInfo_1.bool_5)
		{
			key = 4;
		}
		else if (weapon != null)
		{
			key = weapon.Int32_1;
		}
		KillTypeData objectByKey = KillTypeStorage.Get.Storage.GetObjectByKey(key);
		if (objectByKey != null)
		{
			uilabel_10.String_0 = Localizer.Get(objectByKey.String_0);
			UISpriteData sprite = uisprite_3.UIAtlas_0.GetSprite(objectByKey.String_1);
			if (sprite != null)
			{
				int width = sprite.width;
				int height = sprite.height;
				uisprite_3.Int32_0 = Mathf.FloorToInt((float)width * 0.4f);
				uisprite_3.Int32_1 = Mathf.FloorToInt((float)height * 0.4f);
				uisprite_3.String_0 = objectByKey.String_1;
			}
			else
			{
				uisprite_3.gameObject.SetActive(false);
			}
		}
		else
		{
			uilabel_10.gameObject.SetActive(false);
			uisprite_3.gameObject.SetActive(false);
		}
	}

	private void SetConsPanel(List<ShopArtikulData> list_0)
	{
		if (list_0 != null && list_0.Count != 0)
		{
			if (list_0.Count == 2)
			{
				killCamConsShopItem_0[1].SetData(list_0[0]);
				killCamConsShopItem_0[2].SetData(list_0[1]);
				killCamConsShopItem_0[0].gameObject.SetActive(false);
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					if (i < list_0.Count)
					{
						killCamConsShopItem_0[i].SetData(list_0[i]);
					}
					else
					{
						killCamConsShopItem_0[i].gameObject.SetActive(false);
					}
				}
			}
			if (list_0.Count < 3)
			{
				uisprite_4.Int32_1 -= killCamConsShopItem_0[0].GetComponent<UIWidget>().Int32_1 + 10;
			}
			if (list_0.Count == 1)
			{
				killCamConsShopItem_0[0].transform.localPosition = new Vector3(killCamConsShopItem_0[0].transform.localPosition.x, killCamConsShopItem_0[0].transform.localPosition.y + (float)killCamConsShopItem_0[0].GetComponent<UIWidget>().Int32_1 + 10f, killCamConsShopItem_0[0].transform.localPosition.z);
			}
		}
		else
		{
			gameObject_5.SetActive(false);
		}
	}

	private void SetVillain()
	{
		KillerInfo killerInfo = (base.WindowShowParameters_0 as RespawnBattleWindowParams).killerInfo_0;
		int iKill = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.GetIKill(killerInfo.int_1);
		int iKilled = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.GetIKilled(killerInfo.int_1);
		int num = iKilled - iKill;
		if (num >= 20)
		{
			uilabel_9.String_0 = string.Format(Localizer.Get("ui.kill_cam.villain5"), killerInfo.string_0);
			uisprite_2.String_0 = "villain5";
		}
		else if (num >= 10 && num < 20)
		{
			uilabel_9.String_0 = string.Format(Localizer.Get("ui.kill_cam.villain4"), killerInfo.string_0);
			uisprite_2.String_0 = "villain4";
		}
		else if (num >= 7 && num < 10)
		{
			uilabel_9.String_0 = string.Format(Localizer.Get("ui.kill_cam.villain3"), killerInfo.string_0);
			uisprite_2.String_0 = "villain3";
		}
		else if (num >= 4 && num < 7)
		{
			uilabel_9.String_0 = string.Format(Localizer.Get("ui.kill_cam.villain2"), killerInfo.string_0);
			uisprite_2.String_0 = "villain2";
		}
		else if (num >= 1 && num < 4)
		{
			uilabel_9.String_0 = string.Format(Localizer.Get("ui.kill_cam.villain1"), killerInfo.string_0);
			uisprite_2.String_0 = "villain1";
		}
		else
		{
			uilabel_9.gameObject.SetActive(false);
			uisprite_2.gameObject.SetActive(false);
			TopPanelUp();
		}
	}

	private void TopPanelUp()
	{
		uisprite_1.Int32_1 = 55;
		uilabel_10.transform.localPosition = new Vector3(uilabel_10.transform.localPosition.x, uilabel_10.transform.localPosition.y + 60f, uilabel_10.transform.localPosition.z);
		uisprite_3.transform.localPosition = new Vector3(uisprite_3.transform.localPosition.x, uisprite_3.transform.localPosition.y + 60f, uisprite_3.transform.localPosition.z);
	}

	private List<ShopArtikulData> GetCons()
	{
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		List<int> list2 = new List<int>();
		list2.Add(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_GRENADE));
		list2.Add(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_POTION));
		list2.Add(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_JETPACK));
		list2.Add(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_TURRET));
		list2.Add(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_MECH));
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] <= 0)
			{
				continue;
			}
			List<ShopArtikulData> shopArtikulsByArtikulId = ShopArtikulController.ShopArtikulController_0.GetShopArtikulsByArtikulId(list2[i]);
			if (shopArtikulsByArtikulId != null && shopArtikulsByArtikulId.Count != 0)
			{
				List<ShopArtikulData> list3 = new List<ShopArtikulData>(shopArtikulsByArtikulId);
				list3.Sort((ShopArtikulData shopArtikulData_1, ShopArtikulData shopArtikulData_2) => shopArtikulData_1.Int32_4.CompareTo(shopArtikulData_2.Int32_4));
				ShopArtikulData shopArtikulData = list3[0];
				if (shopArtikulData.CanBuy() && shopArtikulData.GetArtikul().Int32_3 > UserController.UserController_0.GetUserArtikulCount(shopArtikulData.Int32_1))
				{
					list.Add(shopArtikulData);
				}
			}
		}
		return list;
	}

	private ShopArtikulData GetSuper()
	{
		KillerInfo killerInfo = (base.WindowShowParameters_0 as RespawnBattleWindowParams).killerInfo_0;
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		Dictionary<SlotType, int> dictionary = new Dictionary<SlotType, int>();
		for (int i = 1; i < 7; i++)
		{
			SlotType slotType = (SlotType)i;
			dictionary.Add(slotType, 0);
			List<UserArtikul> userArtikulsBySlotType = UserController.UserController_0.GetUserArtikulsBySlotType(slotType);
			if (userArtikulsBySlotType == null || userArtikulsBySlotType.Count == 0)
			{
				continue;
			}
			for (int j = 0; j < userArtikulsBySlotType.Count; j++)
			{
				UserArtikul userArtikul = userArtikulsBySlotType[j];
				List<ShopArtikulData> shopArtikulsByArtikulId = ShopArtikulController.ShopArtikulController_0.GetShopArtikulsByArtikulId(userArtikul.int_0);
				if (shopArtikulsByArtikulId == null || shopArtikulsByArtikulId.Count == 0)
				{
					continue;
				}
				for (int k = 0; k < shopArtikulsByArtikulId.Count; k++)
				{
					ShopArtikulData shopArtikulData = shopArtikulsByArtikulId[k];
					if (dictionary[slotType] < shopArtikulData.Int32_5)
					{
						dictionary[slotType] = shopArtikulData.Int32_5;
					}
				}
			}
		}
		for (int l = 1; l < 7; l++)
		{
			SlotType slotType2 = (SlotType)l;
			List<ShopArtikulData> shopArtikulsBySlot = ShopArtikulController.ShopArtikulController_0.GetShopArtikulsBySlot(slotType2);
			if (shopArtikulsBySlot == null || shopArtikulsBySlot.Count == 0)
			{
				continue;
			}
			int num = -1;
			for (int m = 0; m < shopArtikulsBySlot.Count; m++)
			{
				ShopArtikulData shopArtikulData2 = shopArtikulsBySlot[m];
				if (shopArtikulData2.Int32_1 != killerInfo.int_0 && shopArtikulData2.Boolean_3 && !shopArtikulData2.Boolean_5 && !shopArtikulData2.GetArtikul().Boolean_0 && shopArtikulData2.Int32_5 > dictionary[slotType2] && (num == -1 || shopArtikulData2.Int32_5 < num))
				{
					num = shopArtikulData2.Int32_5;
				}
			}
			for (int n = 0; n < shopArtikulsBySlot.Count; n++)
			{
				ShopArtikulData shopArtikulData3 = shopArtikulsBySlot[n];
				if (shopArtikulData3.Int32_1 != killerInfo.int_0 && shopArtikulData3.Boolean_3 && !shopArtikulData3.Boolean_5 && !shopArtikulData3.GetArtikul().Boolean_0 && shopArtikulData3.NeedsData_0.Check() && shopArtikulData3.GetArtikul().NeedsData_0.Check() && shopArtikulData3.Int32_5 == num)
				{
					list.Add(shopArtikulData3);
				}
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	private void positionsUpdate()
	{
		if (uisprite_2.gameObject.activeSelf)
		{
			float num = -uilabel_9.Int32_0 / 2;
			float num2 = num - (float)uisprite_2.Int32_0 - 5f;
			UIWidget component = uisprite_2.transform.parent.GetComponent<UIWidget>();
			if (num2 - 5f < (float)(-component.Int32_0 / 2))
			{
				float num3 = (float)(-component.Int32_0 / 2) - num2 + 7f;
				num += num3;
				num2 += num3;
			}
			uilabel_9.transform.localPosition = new Vector3(num, uilabel_9.transform.localPosition.y, uilabel_9.transform.localPosition.z);
			uisprite_2.transform.localPosition = new Vector3(num2, uisprite_2.transform.localPosition.y, uisprite_2.transform.localPosition.z);
		}
	}

	private void keyboardUpdate()
	{
		Player_move_c.SetBlockKeyboardControl(true, true);
		if (!(ConfirmBankWindow.ConfirmBankWindow_0 != null) && !(MessageWindowConfirm.MessageWindowConfirm_0 != null) && (InputManager.GetButtonUp("Next") || InputManager.GetButtonUp("Back")))
		{
			OnBtnGoBattleClick();
		}
	}

	private void RespawnPlayer()
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (myPlayerMoveC != null)
		{
			myPlayerMoveC.RespawnPlayer();
		}
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_1 = false;
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		if (!shopArtikulData_0.CanBuy() || UserController.UserController_0.GetUserArtikulCount(shopArtikulData_0.Int32_1) > 0)
		{
			uibutton_0.gameObject.SetActive(false);
			userArtikul_0 = UserController.UserController_0.GetUserArtikulByArtikulId(shopArtikulData_0.GetArtikul().Int32_0);
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(TickOneSecond);
		}
	}

	private void TickOneSecond()
	{
		if (uiwidget_0.gameObject.activeSelf)
		{
			uilabel_14.String_0 = Utility.GetLocalizedTime((int)(userArtikul_0.double_0 + (double)userArtikul_0.int_3 + (double)shopArtikulData_0.GetArtikul().Int32_5 - Utility.Double_0), string_0, string_1, string_2, string_3);
		}
	}

	private void UpdateRespawnTimer()
	{
		SetRespawnTime();
		SetRespawnLabelState();
		CheckRespawnTime();
		if (!(ConfirmBankWindow.ConfirmBankWindow_0 != null) && !(MessageWindowConfirm.MessageWindowConfirm_0 != null))
		{
			bool_2 = false;
		}
		else if (!bool_2)
		{
			bool_2 = true;
			float_3 = timerData_1.Single_0;
		}
		if (bool_2)
		{
			timerData_1.Single_0 = float_3;
		}
	}

	private void SetRespawnTime()
	{
		int num = Mathf.FloorToInt((!(timerData_0.Single_0 > 1f)) ? timerData_1.Single_0 : timerData_0.Single_0);
		uilabel_5.String_0 = num.ToString();
	}

	public void SetRespawnLabelState()
	{
		if (!uibutton_1.Boolean_0 && IsTimeButtonFightEnabled())
		{
			uibutton_1.Boolean_0 = true;
		}
		if (timerData_0.Single_0 > 1f && !timerData_0.Boolean_0)
		{
			timerData_0.Boolean_0 = true;
			uilabel_4.String_0 = Localizer.Get(timerData_0.string_0);
			uilabel_6.String_0 = Localizer.Get(timerData_0.string_1);
			uilabel_5.Color_0 = timerData_0.color_0;
			uilabel_4.Color_0 = timerData_0.color_0;
			uilabel_7.Color_0 = timerData_0.color_0;
			uilabel_5.Int32_5 = 40;
			uilabel_4.transform.localPosition = new Vector3(uilabel_4.transform.localPosition.x, float_0 - 53f, uilabel_4.transform.localPosition.z);
			uilabel_7.transform.localPosition = new Vector3(uilabel_7.transform.localPosition.x, float_2 - 53f, uilabel_7.transform.localPosition.z);
			uilabel_5.transform.localPosition = new Vector3(uilabel_5.transform.localPosition.x, float_1 - 48f, uilabel_5.transform.localPosition.z);
			uilabel_13.gameObject.SetActive(false);
		}
		else if (timerData_0.Single_0 < 1f && !timerData_1.Boolean_0)
		{
			timerData_1.Boolean_0 = true;
			uilabel_4.String_0 = Localizer.Get(timerData_1.string_0);
			uilabel_6.String_0 = Localizer.Get(timerData_1.string_1);
			uilabel_5.Color_0 = timerData_1.color_0;
			uilabel_4.Color_0 = timerData_1.color_0;
			uilabel_7.Color_0 = timerData_1.color_0;
			uilabel_5.Int32_5 = 30;
			uilabel_4.transform.localPosition = new Vector3(uilabel_4.transform.localPosition.x, float_0, uilabel_4.transform.localPosition.z);
			uilabel_7.transform.localPosition = new Vector3(uilabel_7.transform.localPosition.x, float_2, uilabel_7.transform.localPosition.z);
			uilabel_5.transform.localPosition = new Vector3(uilabel_5.transform.localPosition.x, float_1, uilabel_5.transform.localPosition.z);
			uilabel_13.gameObject.SetActive(true);
		}
	}

	private bool IsTimeButtonFightEnabled()
	{
		return timerData_0.Single_0 <= 1f;
	}

	private void CheckRespawnTime()
	{
		if (timerData_0.Single_0 <= 1f && timerData_1.Single_0 <= 1f)
		{
			OnBtnGoBattleClick();
		}
	}

	private void InitRankTrophy()
	{
		if (RankController.RankController_0.Boolean_0)
		{
			RankController.RankController_0.InitRankTrophy(uiwidget_1.gameObject, false, (!Boolean_1) ? killerInfo_0.int_11 : 0, (!Boolean_1) ? killerInfo_0.int_12 : (-1), false);
		}
	}

	public void ShowElements()
	{
		base.UIPanel_0.Single_2 = 1f;
		killCamShopItem_4.capeContainer.SetActive(true);
	}

	public void HideElements()
	{
		base.UIPanel_0.Single_2 = 0f;
		killCamShopItem_4.capeContainer.SetActive(false);
	}

	public void ShowCharacter(KillerInfo killerInfo_1)
	{
		if (killerInfo_0.bool_2)
		{
			characterView_0.ShowCharacterType(CharacterView.CharacterType.Mech, killerInfo_0);
		}
		else if (killerInfo_0.bool_3)
		{
			characterView_0.ShowCharacterType(CharacterView.CharacterType.Turret, killerInfo_0);
		}
		else
		{
			characterView_0.ShowCharacterType(CharacterView.CharacterType.Player, killerInfo_0);
			characterView_0.SetWeaponAndSkin(killerInfo_0.int_0, killerInfo_0.texture_1);
			if (killerInfo_0.int_3 != 0)
			{
				characterView_0.UpdateHat(killerInfo_0.int_3);
			}
			else
			{
				characterView_0.RemoveHat();
			}
			if (killerInfo_0.int_5 != 0)
			{
				characterView_0.UpdateCape(killerInfo_0.int_5, killerInfo_0.texture_2);
			}
			else
			{
				characterView_0.RemoveCape();
			}
			if (killerInfo_0.int_6 != 0)
			{
				characterView_0.UpdateBoots(killerInfo_0.int_6);
			}
			else
			{
				characterView_0.RemoveBoots();
			}
			if (killerInfo_0.int_4 != 0)
			{
				characterView_0.UpdateArmor(killerInfo_0.int_4);
			}
			else
			{
				characterView_0.RemoveArmor();
			}
		}
		gameObject_0.SetActive(true);
		camera_0.targetTexture = Initializer.Initializer_0.respawnWindowRT;
		camera_0.gameObject.SetActive(true);
		uitexture_0.Texture_0 = Initializer.Initializer_0.respawnWindowRT;
		characterView_0.gameObject.SetActive(true);
	}

	public void OnBtnGoBattleClick()
	{
		if (IsTimeButtonFightEnabled())
		{
			if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
			{
				MessageWindowConfirm.MessageWindowConfirm_0.Hide();
			}
			RespawnPlayer();
			Hide();
		}
	}

	public void OnBuyClick()
	{
		if (!bool_1)
		{
			bool_1 = true;
			UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
			ShopArtikulController.ShopArtikulController_0.BuyArtikul(shopArtikulData_0.Int32_0, true, delegate
			{
				bool_1 = false;
			}, ShopArtikulController.SourceBuyType.TYPE_KILLCAM_WND);
		}
	}

	public void OnNickClick()
	{
		KillerInfo killerInfo = (base.WindowShowParameters_0 as RespawnBattleWindowParams).killerInfo_0;
		ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(killerInfo.int_1);
	}
}
