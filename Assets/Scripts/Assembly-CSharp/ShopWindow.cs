using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

[GameWindowParams(GameWindowType.Shop)]
public sealed class ShopWindow : BaseGameWindow
{
	public enum OpenStyle
	{
		SIMPLE = 0,
		ANIMATED = 1
	}

	private static readonly Dictionary<int, Dictionary<int, SlotType>> dictionary_1 = new Dictionary<int, Dictionary<int, SlotType>>
	{
		{
			0,
			new Dictionary<int, SlotType> { 
			{
				0,
				SlotType.SLOT_NONE
			} }
		},
		{
			1,
			new Dictionary<int, SlotType>
			{
				{
					0,
					SlotType.SLOT_WEAPON_PRIMARY
				},
				{
					1,
					SlotType.SLOT_WEAPON_BACKUP
				},
				{
					2,
					SlotType.SLOT_WEAPON_SPECIAL
				},
				{
					3,
					SlotType.SLOT_WEAPON_PREMIUM
				},
				{
					4,
					SlotType.SLOT_WEAPON_SNIPER
				},
				{
					5,
					SlotType.SLOT_WEAPON_MELEE
				}
			}
		},
		{
			2,
			new Dictionary<int, SlotType>
			{
				{
					0,
					SlotType.SLOT_WEAR_ARMOR
				},
				{
					1,
					SlotType.SLOT_WEAR_HAT
				},
				{
					2,
					SlotType.SLOT_WEAR_CAPE
				},
				{
					3,
					SlotType.SLOT_WEAR_BOOTS
				}
			}
		},
		{
			3,
			new Dictionary<int, SlotType>
			{
				{
					0,
					SlotType.SLOT_CONSUM_GRENADE
				},
				{
					1,
					SlotType.SLOT_CONSUM_POTION
				},
				{
					2,
					SlotType.SLOT_CONSUM_TURRET
				},
				{
					3,
					SlotType.SLOT_CONSUM_JETPACK
				},
				{
					4,
					SlotType.SLOT_CONSUM_MECH
				}
			}
		},
		{
			4,
			new Dictionary<int, SlotType>
			{
				{
					0,
					SlotType.SLOT_WEAR_SKIN
				},
				{
					1,
					SlotType.SLOT_NONE
				}
			}
		}
	};

	private static ShopWindow shopWindow_0 = null;

	public UITabsContentController headTabsSD;

	public UITabsContentController[] detailTabsSD;

	public UITabsContentController headTabsHD;

	public UITabsContentController[] detailTabsHD;

	public UIScrollView scroll;

	public UIWidget scrollBar;

	public UITable table;

	public ShopItem template;

	public UIWidget shopPanel;

	public ShopCompareController comparer;

	public ShopMiniSlots miniSlots;

	public PersController persController;

	public Transform[] tabsMenu;

	public HideStuffShopChecker[] hideStuffCheckers;

	public PersRotator persRotator;

	public UITexture background;

	public Transform pers;

	private UITabsContentController uitabsContentController_0;

	private UITabsContentController[] uitabsContentController_1;

	private UIRoot uiroot_0;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private ShopItem shopItem_0;

	private ShopItem shopItem_1;

	private bool bool_1 = true;

	private bool bool_2;

	private bool bool_3;

	private Action action_0;

	private List<int> list_0 = new List<int>();

	private List<int> list_1 = new List<int>();

	private Dictionary<int, GameObject> dictionary_2 = new Dictionary<int, GameObject>();

	private GameObject gameObject_0;

	public ShopArtikulController.SourceBuyType sourceBuyType;

	[CompilerGenerated]
	private static Comparison<ShopArtikulData> comparison_0;

	[CompilerGenerated]
	private static Comparison<ShopArtikulData> comparison_1;

	[CompilerGenerated]
	private static Comparison<ShopArtikulData> comparison_2;

	public static ShopWindow ShopWindow_0
	{
		get
		{
			return shopWindow_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_1;
		}
	}

	public Dictionary<int, GameObject> Dictionary_0
	{
		get
		{
			return dictionary_2;
		}
	}

	public static void Show(ShopWindowParams shopWindowParams_0 = null)
	{
		if (!(shopWindow_0 != null))
		{
			shopWindow_0 = BaseWindow.Load("Shop") as ShopWindow;
			shopWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			shopWindow_0.Parameters_0.bool_5 = false;
			shopWindow_0.Parameters_0.bool_0 = false;
			shopWindow_0.Parameters_0.bool_6 = !TutorialController.TutorialController_0.Boolean_0;
			shopWindow_0.InternalShow(shopWindowParams_0);
			shopWindow_0.sourceBuyType = ((shopWindowParams_0 != null) ? shopWindowParams_0.sourceBuyType_0 : ShopArtikulController.SourceBuyType.TYPE_SHOP_WND);
		}
	}

	public override void OnShow()
	{
		Init();
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, OnUserOverrideUpdate);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, OnUserOverrideUpdate);
		UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtikulBought);
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.UPDATE_ALL, OnUserOverrideUpdate);
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.REMOVE_ALL, OnUserOverrideUpdate);
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtikulBought);
		shopWindow_0 = null;
		foreach (KeyValuePair<int, GameObject> item in dictionary_2)
		{
			item.Value.transform.parent = null;
			UnityEngine.Object.Destroy(item.Value);
		}
		dictionary_2.Clear();
		ShopWindowParams shopWindowParams = base.WindowShowParameters_0 as ShopWindowParams;
		if (shopWindowParams != null && shopWindowParams.action_0 != null)
		{
			shopWindowParams.action_0();
		}
		if (shopWindowParams != null && shopWindowParams.openStyle_0 == OpenStyle.ANIMATED)
		{
			AnimateCamera(false);
		}
		persController.InitInLobby();
	}

	private void OnUserOverrideUpdate(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		OnInventoryUpdate(null);
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_3 = true;
		SetDetailTabContent(int_2);
		bool_3 = false;
	}

	private void OnArtikulBought(UsersData.EventData eventData_0)
	{
		action_0 = delegate
		{
			int num = eventData_0.int_0;
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(num);
			if (artikul != null && artikul.Int32_1 > 0)
			{
				OpenItem(artikul.Int32_1);
			}
		};
	}

	private void Init()
	{
		BoundsInit();
		InitOpenStyle();
		NGUITools.SetActive(template.gameObject, false);
		table.onReposition_0 = delegate
		{
			scroll.UpdateScrollbars(true);
		};
		LocalInit();
		uitabsContentController_0.onTabActive = SetHeadTabContent;
		for (int i = 0; i < uitabsContentController_1.Length; i++)
		{
			if (uitabsContentController_1[i] != null)
			{
				uitabsContentController_1[i].onTabActive = SetDetailTabContent;
			}
		}
		ShopWindowParams shopWindowParams = base.WindowShowParameters_0 as ShopWindowParams;
		if (shopWindowParams != null && shopWindowParams.int_0 > 0)
		{
			Invoke("OpenItemFromWndParam", 0.6f);
		}
		HideStuffCheckersInit();
	}

	private void OpenItemFromWndParam()
	{
		ShopWindowParams shopWindowParams = base.WindowShowParameters_0 as ShopWindowParams;
		if (OpenItem(shopWindowParams.int_0) != null)
		{
			uitabsContentController_0.activateDefaultOnStart = false;
		}
	}

	private void InitOpenStyle()
	{
		ShopWindowParams shopWindowParams = base.WindowShowParameters_0 as ShopWindowParams;
		if (shopWindowParams != null)
		{
			if (shopWindowParams.openStyle_0 == OpenStyle.SIMPLE)
			{
				NGUITools.SetActive(background.gameObject, true);
				NGUITools.SetActive(pers.gameObject, true);
				persRotator.pers = pers;
			}
			else
			{
				InitCamera();
				persController = null;
				persRotator.pers = null;
				StartCoroutine(InitPers());
			}
		}
	}

	private void HideStuffCheckersInit()
	{
		hideStuffCheckers[0].GetComponent<UIToggle>().Boolean_0 = !HideStuffSettings.HideStuffSettings_0.GetShowArmor();
		hideStuffCheckers[1].GetComponent<UIToggle>().Boolean_0 = !HideStuffSettings.HideStuffSettings_0.GetShowHat();
		hideStuffCheckers[2].GetComponent<UIToggle>().Boolean_0 = !HideStuffSettings.HideStuffSettings_0.GetShowBoots();
		hideStuffCheckers[3].GetComponent<UIToggle>().Boolean_0 = !HideStuffSettings.HideStuffSettings_0.GetShowCape();
	}

	private void BoundsInit()
	{
		base.transform.localPosition = new Vector3(0f, 0f, 300f);
		Vector3 localPosition = shopPanel.transform.localPosition;
		float single_ = ScreenController.ScreenController_0.Single_0;
		int num = (int)((float)Screen.width * single_);
		int num4 = (int)((float)Screen.height * single_);
		bool boolean_ = GraphicsController.GraphicsController_0.Boolean_1;
		NGUITools.SetActive(tabsMenu[0].gameObject, !boolean_);
		NGUITools.SetActive(tabsMenu[1].gameObject, boolean_);
		uitabsContentController_0 = ((!boolean_) ? headTabsSD : headTabsHD);
		uitabsContentController_1 = ((!boolean_) ? detailTabsSD : detailTabsHD);
		table.int_0 = ((!boolean_) ? 3 : 4);
		table.vector2_0.x = ((!boolean_) ? 4 : 3);
		shopPanel.Int32_0 = ((!boolean_) ? 746 : 964);
		int num2 = (int)((double)(num - shopPanel.Int32_0) * 0.5);
		int num3 = num2 - 5;
		localPosition.x = num3 * -1;
		shopPanel.transform.localPosition = localPosition;
	}

	public bool IsBestTabOpen()
	{
		return int_1 == 0 && int_2 == 0;
	}

	private void LocalInit()
	{
		int_1 = -1;
		int_2 = -1;
		bool_1 = true;
		bool_2 = false;
		bool_3 = false;
		miniSlots.Init();
	}

	private void InitCamera()
	{
		gameObject_0 = GameObject.Find("Camera_Rotate");
		gameObject_0.GetComponent<Animation>().enabled = true;
		AnimateCamera(true);
	}

	private void AnimateCamera(bool bool_4)
	{
		string arg = ((!GraphicsController.GraphicsController_0.Boolean_1) ? string.Empty : "Wide");
		gameObject_0.GetComponent<Animation>().Play(string.Format("{0}{1}", (!bool_4) ? "ShopCloseCamera" : "ShopOpenCamera", arg));
	}

	private IEnumerator InitPers()
	{
		while (persController == null)
		{
			GameObject gameObject = GameObject.Find("PersConfigurator");
			if (gameObject != null)
			{
				persController = gameObject.GetComponent<PersController>();
			}
			yield return null;
		}
	}

	private void SetHeadTabContent(int int_4)
	{
		if (int_1 == int_4)
		{
			return;
		}
		int_1 = int_4;
		int_2 = -1;
		if (!bool_2)
		{
			UITabsContentController uITabsContentController = uitabsContentController_1[int_1];
			if (uITabsContentController != null)
			{
				uITabsContentController.Activate(uITabsContentController.defaultTab);
			}
		}
	}

	private void SetDetailTabContent(int int_4)
	{
		if (int_2 != int_4 || bool_3)
		{
			int int_5 = ((bool_3 && shopItem_0 != null) ? shopItem_0.ArtikulData_0.Int32_0 : 0);
			list_0.Clear();
			list_1.Clear();
			ClearItems();
			ResetHideCkeckersVisible();
			int_2 = int_4;
			bool_3 = false;
			switch (int_1)
			{
			case 0:
				SetBestOfContent(int_2);
				break;
			case 1:
				SetWeaponsContent(int_2);
				break;
			case 2:
				SetArmorsContent(int_2);
				break;
			case 3:
				SetGearsContent(int_2);
				break;
			case 4:
				SetSkinsContent(int_2);
				break;
			}
			ResetPers();
			RepositionContent();
			PostSetDetailTabContent(int_5);
		}
	}

	private void PostSetDetailTabContent(int int_4)
	{
		if (shopItem_1 == null && shopItem_0 == null)
		{
			SelectFirstItem();
		}
		if (int_4 != 0 && int_1 != 0)
		{
			OpenItem(int_4);
		}
		if (action_0 != null)
		{
			action_0();
			action_0 = null;
		}
	}

	private void SetBestOfContent(int int_4)
	{
		List<ShopArtikulData> shopArtikulsBest = ShopArtikulController.ShopArtikulController_0.GetShopArtikulsBest();
		List<ShopArtikulData> validShopList = ShopArtikulController.ShopArtikulController_0.GetValidShopList(shopArtikulsBest);
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		NeedData needData_ = null;
		for (int i = 0; i < validShopList.Count; i++)
		{
			ShopArtikulData shopArtikulData = validShopList[i];
			if (shopArtikulData != null && shopArtikulData.GetArtikul() != null && shopArtikulData.Boolean_3 && (shopArtikulData.GetArtikul().NeedsData_0 == null || shopArtikulData.GetArtikul().NeedsData_0.Check(out needData_)))
			{
				list.Add(shopArtikulData);
			}
		}
		HashSet<int> hashSet = new HashSet<int>();
		for (int j = 0; j < list.Count; j++)
		{
			hashSet.Add(list[j].Int32_1);
		}
		for (int k = 0; k < list.Count; k++)
		{
			List<ArtikulData> downgrades = ArtikulController.ArtikulController_0.GetDowngrades(list[k].Int32_1);
			for (int l = 0; l < downgrades.Count; l++)
			{
				hashSet.Remove(downgrades[l].Int32_0);
			}
		}
		List<ShopArtikulData> list_ = new List<ShopArtikulData>();
		for (int m = 0; m < list.Count; m++)
		{
			if (hashSet.Contains(list[m].Int32_1))
			{
				list_.Add(list[m]);
			}
		}
		SortSimple(list_);
		SortByUpgrades(ref list_);
		for (int n = 0; n < list_.Count; n++)
		{
			int int32_ = list_[n].GetArtikul().Int32_2;
			if (int32_ > 0 && UserController.UserController_0.GetUserArtikulCount(int32_) > 0)
			{
				list_1.Add(list_[n].Int32_1);
			}
		}
		InflateItems(list_);
	}

	private void SetWeaponsContent(int int_4)
	{
		SlotType slotType_ = dictionary_1[int_1][int_2];
		List<ShopArtikulData> list_ = ShopArtikulController.ShopArtikulController_0.GetValidShopListBySlot(slotType_);
		SortSimple(list_);
		SortByUpgrades(ref list_);
		InflateItems(list_);
	}

	private void SetArmorsContent(int int_4)
	{
		SlotType slotType = dictionary_1[int_1][int_2];
		List<ShopArtikulData> list_ = ShopArtikulController.ShopArtikulController_0.GetValidShopListBySlot(slotType);
		SortSimple(list_);
		SortByUpgrades(ref list_);
		InflateItems(list_);
		SetHideStuffCheckersVisible(slotType);
	}

	private void SetGearsContent(int int_4)
	{
		SlotType slotType_ = dictionary_1[int_1][int_2];
		List<ShopArtikulData> list_ = ShopArtikulController.ShopArtikulController_0.GetValidShopListBySlot(slotType_);
		SortSimple(list_);
		SortByUpgrades(ref list_);
		InflateItems(list_);
	}

	private void SetSkinsContent(int int_4)
	{
		bool bool_0 = int_2 == 1;
		List<ShopArtikulData> validShopListBySlot = ShopArtikulController.ShopArtikulController_0.GetValidShopListBySlot(SlotType.SLOT_WEAR_SKIN);
		List<ShopArtikulData> list_ = validShopListBySlot.FindAll(delegate(ShopArtikulData shopArtikulData_0)
		{
			WearData wear = WearController.WearController_0.GetWear(shopArtikulData_0.Int32_1);
			return (!bool_0) ? (!wear.Boolean_0) : wear.Boolean_0;
		});
		SortSimple(list_);
		InflateItems(list_);
	}

	private void SortByCost(List<ShopArtikulData> list_2)
	{
		list_2.Sort((ShopArtikulData shopArtikulData_0, ShopArtikulData shopArtikulData_1) => shopArtikulData_0.Int32_2.CompareTo(shopArtikulData_1.Int32_2));
	}

	private void SortByDPS(List<ShopArtikulData> list_2)
	{
		list_2.Sort(delegate(ShopArtikulData shopArtikulData_0, ShopArtikulData shopArtikulData_1)
		{
			WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(shopArtikulData_0.Int32_1);
			WeaponData weapon2 = WeaponController.WeaponController_0.GetWeapon(shopArtikulData_1.Int32_1);
			return weapon.Single_3.CompareTo(weapon2.Single_3);
		});
	}

	private void SortSimple(List<ShopArtikulData> list_2)
	{
		list_2.Sort(delegate(ShopArtikulData shopArtikulData_0, ShopArtikulData shopArtikulData_1)
		{
			if (shopArtikulData_0.Int32_5 == 0 && shopArtikulData_1.Int32_5 == 0)
			{
				return 0;
			}
			if (shopArtikulData_0.Int32_5 == 0 && shopArtikulData_1.Int32_5 > 0)
			{
				return 1;
			}
			return (shopArtikulData_1.Int32_5 == 0 && shopArtikulData_0.Int32_5 > 0) ? (-1) : shopArtikulData_0.Int32_5.CompareTo(shopArtikulData_1.Int32_5);
		});
	}

	private void SortByUpgrades(ref List<ShopArtikulData> list_2)
	{
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		List<ShopArtikulData> list2 = new List<ShopArtikulData>(list_2);
		ShopArtikulData shopArtikulData_2;
		foreach (ShopArtikulData item in list_2)
		{
			shopArtikulData_2 = item;
			if (list2.Count == 0)
			{
				break;
			}
			if (list.Contains(shopArtikulData_2))
			{
				continue;
			}
			ShopArtikulData shopArtikulData = ((shopArtikulData_2.GetArtikul().Int32_2 != 0) ? list2.Find((ShopArtikulData shopArtikulData_1) => shopArtikulData_1.GetArtikul().Int32_1 == shopArtikulData_2.Int32_1) : null);
			if (shopArtikulData != null)
			{
				list.Add(shopArtikulData);
				list2.Remove(shopArtikulData);
				list_0.Add(shopArtikulData.Int32_1);
				list_1.Add(shopArtikulData_2.Int32_1);
			}
			list.Add(shopArtikulData_2);
			list2.Remove(shopArtikulData_2);
			if (shopArtikulData == null)
			{
				ShopArtikulData shopArtikulData2 = ((shopArtikulData_2.GetArtikul().Int32_1 != 0) ? list2.Find((ShopArtikulData shopArtikulData_1) => shopArtikulData_1.GetArtikul().Int32_2 == shopArtikulData_2.Int32_1) : null);
				if (shopArtikulData2 != null)
				{
					list.Add(shopArtikulData2);
					list2.Remove(shopArtikulData2);
					list_0.Add(shopArtikulData_2.Int32_1);
					list_1.Add(shopArtikulData2.Int32_1);
				}
			}
		}
		list_2 = list;
		list2.Clear();
	}

	private void ClearItems()
	{
		shopItem_0 = null;
		shopItem_1 = null;
		List<Transform> list = table.List_0;
		foreach (Transform item in list)
		{
			if (!(item == null))
			{
				item.parent = null;
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}

	private void InflateItem(ShopArtikulData shopArtikulData_0)
	{
		bool bool_ = int_3 % table.int_0 == 0;
		bool bool_2 = (int_3 + 1) % table.int_0 == 0;
		GameObject gameObject = NGUITools.AddChild(table.gameObject, template.gameObject);
		gameObject.name = string.Format("{0:0000}", int_3++);
		ShopItem component = gameObject.GetComponent<ShopItem>();
		component.SetCallbacks(OnSelectedShopItem, OnEqippedShopItem);
		component.SetData(shopArtikulData_0, list_0, list_1, bool_, bool_2);
		TooltipInfo component2 = gameObject.GetComponent<TooltipInfo>();
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(shopArtikulData_0.GetArtikul().Int32_0);
		if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
		{
			component2.weaponID = shopArtikulData_0.GetArtikul().Int32_0;
		}
		else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
		{
			component2.wearID = shopArtikulData_0.GetArtikul().Int32_0;
		}
		NGUITools.SetActive(gameObject, true);
		component.TryShowArrow();
	}

	private void InflateItems(List<ShopArtikulData> list_2)
	{
		int_3 = 0;
		foreach (ShopArtikulData item in list_2)
		{
			InflateItem(item);
		}
		NGUITools.SetActive(scrollBar.gameObject, (float)int_3 / (float)table.int_0 > 2f);
	}

	private void OnSelectedShopItem(ShopItem shopItem_2)
	{
		if (shopItem_0 != null)
		{
			shopItem_0.SetSelected(false);
		}
		if (shopItem_0 != null && shopItem_0.Equals(shopItem_2))
		{
			shopItem_0 = null;
		}
		else
		{
			shopItem_0 = shopItem_2;
			SetHideStuffCheckersVisible(SlotType.SLOT_NONE);
		}
		ShowItemInfo();
		SetItemOnPers(shopItem_2.ArtikulData_0.Int32_0, true);
	}

	private void OnEqippedShopItem(ShopItem shopItem_2)
	{
		shopItem_1 = shopItem_2;
		ShowItemInfo();
		SetItemOnPers(shopItem_2.ArtikulData_0.Int32_0);
	}

	private void ShowItemInfo()
	{
		if (int_1 == 0 && shopItem_0 != null)
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(shopItem_0.ArtikulData_0.Int32_0);
			ArtikulData artikulDataFromSlot = UserController.UserController_0.GetArtikulDataFromSlot(artikul.SlotType_0);
			if (artikulDataFromSlot != null && (artikulDataFromSlot == null || artikulDataFromSlot.Int32_0 != artikul.Int32_0))
			{
				comparer.ShowCompareInfo(artikulDataFromSlot.Int32_0, artikul.Int32_0, int_1);
			}
			else
			{
				comparer.ShowInfo(artikul.Int32_0, int_1);
			}
		}
		else if (shopItem_1 == shopItem_0 && shopItem_1 != null)
		{
			comparer.ShowInfo(shopItem_1.ArtikulData_0.Int32_0, int_1);
		}
		else if (shopItem_0 == null && shopItem_1 != null)
		{
			comparer.ShowInfo(shopItem_1.ArtikulData_0.Int32_0, int_1);
		}
		else if (shopItem_1 == null && shopItem_0 != null)
		{
			comparer.ShowInfo(shopItem_0.ArtikulData_0.Int32_0, int_1);
		}
		else if (shopItem_1 != null && shopItem_0 != null)
		{
			comparer.ShowCompareInfo(shopItem_1.ArtikulData_0.Int32_0, shopItem_0.ArtikulData_0.Int32_0, int_1);
		}
	}

	private void RepositionContent()
	{
		table.Reposition();
		if (bool_1)
		{
			bool_1 = false;
		}
		else
		{
			scroll.ResetPosition();
		}
	}

	public GameObject OpenItem(int int_4)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_4);
		if (artikul == null)
		{
			return null;
		}
		int num = -1;
		int num2 = -1;
		ShopWindowParams shopWindowParams = base.WindowShowParameters_0 as ShopWindowParams;
		ShopArtikulData shopArtikul = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(shopWindowParams.int_1);
		if (shopArtikul != null && shopArtikul.Boolean_7)
		{
			num = 0;
			num2 = 0;
			(base.WindowShowParameters_0 as ShopWindowParams).int_1 = 0;
		}
		foreach (KeyValuePair<int, Dictionary<int, SlotType>> item in dictionary_1)
		{
			if (num2 > -1)
			{
				break;
			}
			num = item.Key;
			foreach (KeyValuePair<int, SlotType> item2 in item.Value)
			{
				if (item2.Value == artikul.SlotType_0)
				{
					num2 = item2.Key;
					break;
				}
			}
		}
		if (artikul.SlotType_0 == SlotType.SLOT_WEAR_SKIN)
		{
			WearData wear = WearController.WearController_0.GetWear(artikul.Int32_0);
			if (wear.Boolean_0)
			{
				num2 = 1;
			}
		}
		if (num != int_1)
		{
			bool_2 = true;
			uitabsContentController_0.Activate(num);
			uitabsContentController_1[num].Activate(num2);
		}
		else if (num2 != int_2)
		{
			uitabsContentController_1[int_1].Activate(num2);
		}
		else
		{
			scroll.ResetPosition();
		}
		bool_2 = false;
		int num3 = table.List_0.FindLastIndex(delegate(Transform transform_0)
		{
			ShopItem component2 = transform_0.gameObject.GetComponent<ShopItem>();
			return component2.ArtikulData_0.Int32_0 == int_4;
		});
		GameObject gameObject = null;
		if (num3 > -1)
		{
			gameObject = table.List_0[num3].gameObject;
			ShopItem component = gameObject.GetComponent<ShopItem>();
			OnSelectedShopItem(component);
			component.OnClick();
			component.OnClick();
			ScrollToItem(component, num3);
		}
		return gameObject;
	}

	private void ScrollToItem(ShopItem shopItem_2, int int_4)
	{
		float num = (float)shopItem_2.GetComponent<UIWidget>().Int32_1 + table.vector2_0.y;
		Vector3 zero = Vector3.zero;
		int num2 = int_4 / table.int_0;
		int num3 = table.List_0.Count / table.int_0;
		if (num2 > 1 && num2 < num3)
		{
			zero.y = (float)(num2 - 1) * num;
		}
		else if (num2 == num3)
		{
			scroll.verticalScrollBar.Single_0 = 1f;
		}
		scroll.MoveRelative(zero);
	}

	public void OpenTab(SlotType slotType_0)
	{
		int num = -1;
		int num2 = -1;
		foreach (KeyValuePair<int, Dictionary<int, SlotType>> item in dictionary_1)
		{
			if (num2 > -1)
			{
				break;
			}
			num = item.Key;
			foreach (KeyValuePair<int, SlotType> item2 in item.Value)
			{
				if (item2.Value == slotType_0)
				{
					num2 = item2.Key;
					break;
				}
			}
		}
		if (num != int_1)
		{
			uitabsContentController_0.Activate(num);
			uitabsContentController_1[num].Activate(num2);
		}
		else if (num2 != int_2)
		{
			uitabsContentController_1[int_1].Activate(num2);
		}
		else
		{
			scroll.ResetPosition();
		}
	}

	private void ResetHideCkeckersVisible()
	{
		for (int i = 0; i < hideStuffCheckers.Length; i++)
		{
			hideStuffCheckers[i].SetVisible(false);
		}
	}

	private void SetHideStuffCheckersVisible(SlotType slotType_0)
	{
		ResetHideCkeckersVisible();
		SlotType slotType = SlotType.SLOT_NONE;
		if (slotType_0 != 0)
		{
			slotType = slotType_0;
		}
		else
		{
			if (shopItem_0 == null || shopItem_0.ArtikulData_0 == null)
			{
				return;
			}
			slotType = shopItem_0.ArtikulData_0.SlotType_0;
		}
		switch (slotType)
		{
		case SlotType.SLOT_WEAR_HAT:
			hideStuffCheckers[1].SetVisible(true);
			break;
		case SlotType.SLOT_WEAR_ARMOR:
			hideStuffCheckers[0].SetVisible(true);
			break;
		case SlotType.SLOT_WEAR_CAPE:
			hideStuffCheckers[3].SetVisible(true);
			break;
		case SlotType.SLOT_WEAR_BOOTS:
			hideStuffCheckers[2].SetVisible(true);
			break;
		case SlotType.SLOT_WEAR_SKIN:
			break;
		}
	}

	public void SetItemOnPers(int int_4, bool bool_4 = false)
	{
		if (persController != null)
		{
			persController.SetItem(int_4, bool_4);
		}
	}

	public void ResetPers()
	{
		if (persController != null && !bool_1)
		{
			persController.ResetTryOnStuffs();
			persController.Reset();
		}
	}

	private void SelectFirstItem()
	{
		if (table.List_0.Count > 0)
		{
			Transform transform = table.List_0[0];
			ShopItem component = transform.gameObject.GetComponent<ShopItem>();
			if (component != null)
			{
				component.OnClick();
			}
		}
	}

	public int GetHeadTabByArtikul(int int_4)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_4);
		if (artikul == null)
		{
			return 0;
		}
		foreach (KeyValuePair<int, Dictionary<int, SlotType>> item in dictionary_1)
		{
			foreach (KeyValuePair<int, SlotType> item2 in item.Value)
			{
				if (item2.Value == artikul.SlotType_0)
				{
					return item.Key;
				}
			}
		}
		return 0;
	}

	public void OnMoneyButtonClick()
	{
		BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_SHOP);
	}

	public static GameObject GetGameObjectInstance(int int_4, GameObject gameObject_1)
	{
		if (ShopWindow_0 == null)
		{
			return gameObject_1;
		}
		GameObject value = null;
		if (!ShopWindow_0.Dictionary_0.TryGetValue(int_4, out value))
		{
			value = UnityEngine.Object.Instantiate(gameObject_1) as GameObject;
			ShopWindow_0.Dictionary_0.Add(int_4, value);
		}
		return value;
	}
}
