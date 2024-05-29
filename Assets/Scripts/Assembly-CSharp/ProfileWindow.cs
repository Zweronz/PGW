using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.Profile)]
public class ProfileWindow : BaseGameWindow
{
	private static ProfileWindow profileWindow_0;

	public UILabel[] killValues;

	public UILabel[] winValues;

	public UILabel likes;

	public PersRotator commonRotator;

	public PersRotator persRotator;

	public UITexture background;

	public GameObject pers;

	public NickLabelController myLabel;

	public GameObject AdminBtn;

	public UIWidget LeftPanel;

	public UIWidget RightPanel;

	public UIWidget rankTrophyPlaceholder;

	public KillCamShopItem ShopBoot;

	public KillCamShopItem ShopArmor;

	public KillCamShopItem ShopCap;

	public KillCamShopItem ShopCape;

	public KillCamShopItem ShopWeapon1;

	public KillCamShopItem ShopWeapon2;

	public KillCamShopItem ShopWeapon3;

	public KillCamShopItem ShopWeapon4;

	public KillCamShopItem ShopWeapon5;

	public KillCamShopItem ShopWeapon6;

	private GameObject gameObject_0;

	private UserProfileStatData userProfileStatData_0;

	private PersController persController_0;

	private UserData userData_0;

	public static ProfileWindow ProfileWindow_0
	{
		get
		{
			return profileWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ProfileWindow_0 != null && ProfileWindow_0.Boolean_0;
		}
	}

	public static void Show(ProfileWindowParams profileWindowParams_0 = null)
	{
		if (!(profileWindow_0 != null))
		{
			if (profileWindowParams_0 == null)
			{
				profileWindowParams_0 = new ProfileWindowParams(UserController.UserController_0.UserData_0.user_0.int_0, ProfileWindowParams.OpenType.COMMON);
			}
			UserData user = UserController.UserController_0.GetUser(profileWindowParams_0.int_0);
			if (user != null)
			{
				profileWindow_0 = BaseWindow.Load("ProfileWindow") as ProfileWindow;
				profileWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
				profileWindow_0.Parameters_0.bool_5 = false;
				profileWindow_0.Parameters_0.bool_0 = false;
				profileWindow_0.Parameters_0.bool_6 = true;
				profileWindow_0.InternalShow(profileWindowParams_0);
			}
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		profileWindow_0 = null;
		RankController.RankController_0.Dispatch(null, RankController.EventType.PROFILE_CLOSED);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowElements();
		}
		switch ((base.WindowShowParameters_0 as ProfileWindowParams).openType_0)
		{
		case ProfileWindowParams.OpenType.COMMON:
			AnimateCamera(false);
			Lobby.Lobby_0.Show();
			break;
		case ProfileWindowParams.OpenType.OTHER_PROFILE_LOBBY:
			AnimateCamera(false);
			ProfileWindowController.ProfileWindowController_0.HideProfileWindow();
			ReinitPersToLobby();
			break;
		case ProfileWindowParams.OpenType.OTHER_PROFILE:
			AnimateCamera(false);
			ProfileWindowController.ProfileWindowController_0.HideProfileWindow();
			break;
		}
	}

	private IEnumerator ReinitPersToProfile()
	{
		while (persController_0 == null)
		{
			GameObject gameObject = GameObject.Find("PersConfigurator");
			if (gameObject != null)
			{
				persController_0 = gameObject.GetComponent<PersController>();
			}
			yield return null;
		}
		int int_ = (base.WindowShowParameters_0 as ProfileWindowParams).int_0;
		persController_0.ReinitLobbyToProfile(int_);
		NickLabelController nickLabelController_ = persController_0.NickLabelController_0;
		if (nickLabelController_ != null)
		{
			nickLabelController_.StartShow(int_);
			nickLabelController_.userInfoController.ReinitToAnotherUser(int_);
		}
	}

	private void ReinitPersToLobby()
	{
		persController_0.ReinitProfileToLobby();
		NickLabelController nickLabelController_ = persController_0.NickLabelController_0;
		if (nickLabelController_ != null)
		{
			nickLabelController_.StartShow();
			nickLabelController_.userInfoController.ReinitToAnotherUser(0);
		}
	}

	private IEnumerator ReinitMyPersToProfile()
	{
		while (!persController_0.Boolean_0)
		{
			yield return null;
		}
		int int_ = (base.WindowShowParameters_0 as ProfileWindowParams).int_0;
		persController_0.ReinitLobbyToProfile(int_);
		myLabel.Transform_0 = base.transform;
		myLabel.Boolean_0 = true;
		myLabel.Boolean_3 = false;
		myLabel.Boolean_2 = false;
		myLabel.StartShow(int_);
		myLabel.userInfoController.ReinitToAnotherUser(int_);
	}

	private void Init()
	{
		gameObject_0 = GameObject.Find("Camera_Rotate");
		if (gameObject_0 != null)
		{
			gameObject_0.GetComponent<Animation>().enabled = true;
			AnimateCamera(true);
		}
		int int_ = (base.WindowShowParameters_0 as ProfileWindowParams).int_0;
		userData_0 = UserController.UserController_0.GetUser(int_);
		userProfileStatData_0 = userData_0.userProfileStatData_0;
		ProfileWindowParams.OpenType openType_ = (base.WindowShowParameters_0 as ProfileWindowParams).openType_0;
		RankController.RankController_0.Dispatch(new RankController.EventData
		{
			int_0 = int_
		}, RankController.EventType.PROFILE_OPENED);
		switch (openType_)
		{
		case ProfileWindowParams.OpenType.OTHER_PROFILE_LOBBY:
			StartCoroutine(ReinitPersToProfile());
			myLabel.gameObject.SetActive(false);
			break;
		case ProfileWindowParams.OpenType.OTHER_PROFILE:
			NGUITools.SetActive(background.gameObject, true);
			NGUITools.SetActive(pers, true);
			NGUITools.SetActive(persRotator.gameObject, true);
			persRotator.pers = pers.transform;
			persController_0 = pers.transform.Find("PersConfigurator").GetComponent<PersController>();
			persController_0.ReinitLobbyToProfile(int_);
			StartCoroutine(ReinitMyPersToProfile());
			RankCupController.Show(persRotator.gameObject, int_);
			break;
		default:
			NGUITools.SetActive(background.gameObject, false);
			NGUITools.SetActive(pers, false);
			NGUITools.SetActive(persRotator.gameObject, false);
			myLabel.gameObject.SetActive(false);
			break;
		}
		InitKills();
		InitWins();
		InitLikes();
		InitInventory();
		InitRankTrophy();
		if (UserController.UserController_0.UserData_0.user_0.bool_0)
		{
			AdminBtn.SetActive(true);
		}
		else
		{
			AdminBtn.SetActive(false);
		}
	}

	private void AnimateCamera(bool bool_1)
	{
		if (gameObject_0 != null)
		{
			gameObject_0.GetComponent<Animation>().Play((!bool_1) ? "ProfileCloseCamera" : "ProfileOpenCamera");
		}
	}

	private void InitKills()
	{
		int countKillsInRegim = userProfileStatData_0.GetCountKillsInRegim(ModeType.TEAM_FIGHT);
		killValues[1].String_0 = countKillsInRegim.ToString();
		int countKillsInRegim2 = userProfileStatData_0.GetCountKillsInRegim(ModeType.DEATH_MATCH);
		killValues[2].String_0 = countKillsInRegim2.ToString();
		int countKillsInRegim3 = userProfileStatData_0.GetCountKillsInRegim(ModeType.FLAG_CAPTURE);
		killValues[3].String_0 = countKillsInRegim3.ToString();
		killValues[0].String_0 = (countKillsInRegim + countKillsInRegim2 + countKillsInRegim3).ToString();
		killValues[4].String_0 = string.Format("{0:0.000}", userProfileStatData_0.float_0);
		killValues[5].String_0 = userProfileStatData_0.int_0.ToString();
		killValues[6].String_0 = userProfileStatData_0.int_1.ToString();
		killValues[7].String_0 = userProfileStatData_0.float_1 + "%";
	}

	private void InitWins()
	{
		int countWinsInRegim = userProfileStatData_0.GetCountWinsInRegim(ModeType.TEAM_FIGHT);
		winValues[1].String_0 = countWinsInRegim.ToString();
		int countWinsInRegim2 = userProfileStatData_0.GetCountWinsInRegim(ModeType.DEATH_MATCH);
		winValues[2].String_0 = countWinsInRegim2.ToString();
		int countWinsInRegim3 = userProfileStatData_0.GetCountWinsInRegim(ModeType.FLAG_CAPTURE);
		winValues[3].String_0 = countWinsInRegim3.ToString();
		winValues[0].String_0 = (countWinsInRegim + countWinsInRegim2 + countWinsInRegim3).ToString();
		winValues[4].String_0 = userProfileStatData_0.float_2 + "%";
		winValues[5].String_0 = userProfileStatData_0.int_2.ToString();
		winValues[6].String_0 = userProfileStatData_0.int_3.ToString();
		winValues[7].String_0 = userProfileStatData_0.int_4.ToString();
	}

	private void InitLikes()
	{
		likes.String_0 = userProfileStatData_0.int_5.ToString();
	}

	private void InitInventory()
	{
		int int_ = (base.WindowShowParameters_0 as ProfileWindowParams).int_0;
		ShopBoot.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_BOOTS, int_), null);
		ShopArmor.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR, int_), null);
		ShopCap.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_HAT, int_), null);
		ShopCape.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE, int_), null);
		if (UserController.UserController_0.UserData_0.user_0.int_0.Equals(int_))
		{
			Dictionary<KeyCode, SlotType> dictionary = new Dictionary<KeyCode, SlotType>();
			List<KeyCode> mapKeycodeSlotType = WeaponController.WeaponController_0.GetMapKeycodeSlotType(dictionary);
			ShopWeapon1.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[0]], int_), null);
			ShopWeapon2.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[1]], int_), null);
			ShopWeapon3.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[2]], int_), null);
			ShopWeapon4.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[3]], int_), null);
			ShopWeapon5.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[4]], int_), null);
			ShopWeapon6.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(dictionary[mapKeycodeSlotType[5]], int_), null);
		}
		else
		{
			ShopWeapon1.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_PRIMARY, int_), null);
			ShopWeapon2.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_BACKUP, int_), null);
			ShopWeapon3.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_SPECIAL, int_), null);
			ShopWeapon4.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_PREMIUM, int_), null);
			ShopWeapon5.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_SNIPER, int_), null);
			ShopWeapon6.SetData(UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAPON_MELEE, int_), null);
		}
	}

	public void ToAdmin()
	{
		int int_ = (base.WindowShowParameters_0 as ProfileWindowParams).int_0;
		if (int_ == 0)
		{
			int_ = UserController.UserController_0.UserData_0.user_0.int_0;
		}
		string fileName = string.Format("{0}/index.php?section=users&subsection=accounts&action=edit&object_id={1}", AppController.AppController_0.String_1, int_);
		Process.Start(fileName);
	}

	private void InitRankTrophy()
	{
		if (RankController.RankController_0.Boolean_0 && RankController.RankController_0.Boolean_2)
		{
			bool flag = userData_0.user_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
			RankController.RankController_0.InitRankTrophy(rankTrophyPlaceholder.gameObject, flag, userData_0.userRankData_0.int_1, userData_0.userRankData_0.int_0, flag);
		}
	}

	private void Start()
	{
		if ((float)Screen.width * 1f / ((float)Screen.height * 1f) < 1.59f)
		{
			LeftPanel.leftAnchor.absolute -= 50;
			LeftPanel.rightAnchor.absolute -= 50;
			LeftPanel.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
			RightPanel.leftAnchor.absolute += 50;
			RightPanel.rightAnchor.absolute += 50;
			RightPanel.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
			ShopBoot.widget.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			ShopArmor.widget.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			ShopCap.widget.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			ShopCape.widget.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		}
	}
}
