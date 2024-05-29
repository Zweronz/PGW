using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using engine.helpers;

public class ShopItem : MonoBehaviour
{
	public enum ShopItemState
	{
		STATE_BOUGHT = 0,
		STATE_EQUIP = 1,
		STATE_EQUIPED = 2,
		STATE_CAN_BUY = 3,
		STATE_BUY = 4,
		STATE_CAN_UPGRADE = 5,
		STATE_UPGRADE = 6,
		STATE_NEEDS = 7,
		STATE_UNEQUIP = 8,
		STATE_CREATE_CUSTOM_SKIN = 9,
		STATE_EDIT_CUSTOM_SKIN = 10
	}

	public const string string_0 = "[FF7F00]";

	public const string string_1 = "[-]";

	public UILabel count;

	public UITexture image;

	public UITexture lockImage;

	public Transform model;

	public ShopItemStateBought stateBought;

	public ShopItemStateEquip stateEquip;

	public ShopItemStateEquipped stateEquipped;

	public ShopItemStateCanBuy stateCanBuy;

	public ShopItemStateBuy stateBuy;

	public ShopItemStateCanUpgrade stateCanUpgrade;

	public ShopItemStateUpgrade stateUpgrade;

	public ShopItemStateNeeds stateNeeds;

	public ShopItemStateUnequip stateUnequip;

	public ShopItemStateCreateCustom stateCreateCustom;

	public ShopItemEditCustom stateEditCustom;

	public BaseShopItemState[] states;

	public UISprite[] arrowBorders;

	public UISprite[] arrows;

	public UISprite newStiker;

	public UISprite saleStiker;

	public UISprite bestStiker;

	public UISprite actionStiker;

	public UIWidget rentSticker;

	private ArtikulData artikulData_0;

	private ShopArtikulData shopArtikulData_0;

	private ShopItemState shopItemState_0;

	private List<ArtikulData> list_0;

	private bool bool_0;

	private int int_0 = -1;

	private string string_2;

	private string string_3;

	private bool bool_1;

	private List<int> list_1;

	private List<int> list_2;

	private bool bool_2;

	private bool bool_3;

	private Action<ShopItem> action_0;

	private Action<ShopItem> action_1;

	private static GameObject gameObject_0;

	private static Shader shader_0;

	private static Shader shader_1;

	private bool bool_4;

	private bool bool_5;

	private bool bool_6;

	private bool bool_7;

	private UserArtikul userArtikul_0;

	public ArtikulData ArtikulData_0
	{
		get
		{
			return artikulData_0;
		}
	}

	public ShopArtikulData ShopArtikulData_0
	{
		get
		{
			return shopArtikulData_0;
		}
	}

	public GameObject GameObject_0
	{
		get
		{
			if ((int)shopItemState_0 >= states.Length)
			{
				return null;
			}
			return states[(int)shopItemState_0].gameObject;
		}
	}

	public void SetData(ShopArtikulData shopArtikulData_1, List<int> list_3, List<int> list_4, bool bool_8, bool bool_9)
	{
		shopArtikulData_0 = shopArtikulData_1;
		artikulData_0 = shopArtikulData_1.GetArtikul();
		list_1 = list_3;
		list_2 = list_4;
		bool_2 = bool_8;
		bool_3 = bool_9;
		list_0 = ArtikulController.ArtikulController_0.GetDowngrades(artikulData_0.Int32_0);
		UpdateItem();
	}

	public void SetCallbacks(Action<ShopItem> action_2, Action<ShopItem> action_3)
	{
		action_0 = action_2;
		action_1 = action_3;
	}

	public void UpdateItem()
	{
		if (shopArtikulData_0 == null || artikulData_0 == null)
		{
			return;
		}
		string_2 = artikulData_0.String_4;
		string_3 = string.Empty;
		if (SkinsController.CanWearCustomWear(artikulData_0.Int32_0))
		{
			string_2 = SkinsController.GetCustomWearName(artikulData_0.Int32_0);
		}
		if (artikulData_0.Int32_2 > 0)
		{
			Regex regex = new Regex("(UP|LVL)\\d+");
			Match match = regex.Match(string_2);
			if (match != null && !match.Value.Equals(string.Empty))
			{
				string_2 = regex.Replace(string_2, string.Format("{0}{1}{2}", "[FF7F00]", match.Value, "[-]"));
			}
		}
		SetCount();
		if (artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_CAPE && artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_SKIN)
		{
			image.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData_0);
			if (artikulData_0.Int32_0 == 303)
			{
				image.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			}
		}
		if (list_1.Contains(artikulData_0.Int32_0) && bool_3)
		{
			int_0 = 1;
		}
		else if (list_2.Contains(artikulData_0.Int32_0))
		{
			int_0 = 0;
		}
		else
		{
			int_0 = -1;
		}
		if (ShopWindow.ShopWindow_0 != null && ShopWindow.ShopWindow_0.IsBestTabOpen())
		{
			int_0 = -1;
		}
		UpdateLiveTime();
		InitStates();
		UpdateState();
	}

	private void UpdateLiveTime()
	{
		rentSticker.gameObject.SetActive(ShopArtikulData_0.GetArtikul().Int32_5 > 0);
	}

	private void SetCount()
	{
		if (count == null)
		{
			return;
		}
		int userArtikulCount = UserController.UserController_0.GetUserArtikulCount(artikulData_0.Int32_0);
		if (userArtikulCount > 1)
		{
			string text = string.Format("x{0}", userArtikulCount);
			if (artikulData_0.Int32_3 > 0)
			{
				text += string.Format("/{0}", artikulData_0.Int32_3);
			}
			count.String_0 = text;
			count.gameObject.SetActive(true);
		}
		else
		{
			count.String_0 = string.Empty;
			count.gameObject.SetActive(false);
		}
	}

	public void TryShowArrow()
	{
		if (artikulData_0 != null)
		{
			if (artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_CAPE || artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_SKIN)
			{
				NGUITools.SetActive(image.gameObject, false);
				StartCoroutine(LoadModel());
			}
			StartCoroutine(ShowArrow());
		}
	}

	private IEnumerator ShowArrow()
	{
		yield return null;
		NGUITools.SetActive(arrowBorders[0].gameObject, false);
		NGUITools.SetActive(arrowBorders[1].gameObject, false);
		NGUITools.SetActive(arrows[0].gameObject, false);
		NGUITools.SetActive(arrows[1].gameObject, false);
		if (int_0 > -1)
		{
			NGUITools.SetActive(bool_1: !bool_0 && shopItemState_0 != ShopItemState.STATE_EQUIPED, gameObject_0: arrowBorders[int_0].gameObject);
			Vector3 localPosition = arrows[int_0].transform.localPosition;
			localPosition.x = ((int_0 != 1 || !bool_3) ? 107 : 100);
			localPosition.y = ((bool_4 || bool_5 || bool_6 || bool_7) ? (-25) : 0);
			arrows[int_0].transform.localPosition = localPosition;
			NGUITools.SetActive(arrows[int_0].gameObject, true);
		}
	}

	private void InitStates()
	{
		int price = shopArtikulData_0.GetPrice();
		string text = ((shopArtikulData_0.MoneyType_0 != 0) ? "gems_ico" : "coin_ico");
		Vector3 localPosition = image.transform.localPosition;
		localPosition.y = -1f;
		image.transform.localPosition = localPosition;
		NGUITools.SetActive(lockImage.gameObject, false);
		stateBought.SetData(string_2);
		stateEquip.SetData(string_2, shopArtikulData_0);
		stateEquipped.SetData(string_2);
		stateCanBuy.SetData(string_2, text, price);
		stateBuy.SetData(string_2, shopArtikulData_0);
		stateCanUpgrade.SetData(string_2, string_3, text, price);
		stateUpgrade.SetData(string_2, string_3, shopArtikulData_0);
		stateNeeds.SetData(string_2, string.Empty);
		stateUnequip.SetData(string_2, shopArtikulData_0);
		stateCreateCustom.SetData(string_2, shopArtikulData_0);
		stateEditCustom.SetData(string_2, shopArtikulData_0);
	}

	public void UpdateState()
	{
		if (shopArtikulData_0 == null || artikulData_0 == null)
		{
			return;
		}
		ShopItemState shopItemState = shopItemState_0;
		bool_1 = false;
		WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
		if (wear != null && wear.Boolean_0)
		{
			bool_1 = true;
		}
		NeedData needData_ = null;
		bool flag = ArtikulController.ArtikulController_0.CheckNeeds(artikulData_0.Int32_0, out needData_);
		bool flag2 = UserController.UserController_0.HasUserArtikul(artikulData_0.Int32_0);
		shopItemState_0 = ((!flag2) ? ((!flag) ? ShopItemState.STATE_NEEDS : ShopItemState.STATE_CAN_BUY) : ShopItemState.STATE_BOUGHT);
		if (flag2 && UserController.UserController_0.GetArtikulIdFromSlot(artikulData_0.SlotType_0) == artikulData_0.Int32_0)
		{
			shopItemState_0 = ShopItemState.STATE_EQUIPED;
		}
		if (!flag2 && list_2.Contains(artikulData_0.Int32_0))
		{
			shopItemState_0 = ((!flag) ? ShopItemState.STATE_NEEDS : ShopItemState.STATE_CAN_UPGRADE);
		}
		if (bool_0)
		{
			if (shopItemState_0 == ShopItemState.STATE_BOUGHT)
			{
				shopItemState_0 = ShopItemState.STATE_EQUIP;
				if (bool_1 && wear.Boolean_1 && !SkinsController.CanWearCustomSkin(wear.Int32_0))
				{
					shopItemState_0 = ShopItemState.STATE_CREATE_CUSTOM_SKIN;
				}
			}
			else if (shopItemState_0 == ShopItemState.STATE_CAN_BUY)
			{
				shopItemState_0 = ShopItemState.STATE_BUY;
			}
			else if (shopItemState_0 == ShopItemState.STATE_CAN_UPGRADE)
			{
				shopItemState_0 = ShopItemState.STATE_UPGRADE;
			}
			else if (shopItemState_0 == ShopItemState.STATE_EQUIPED)
			{
				if (shopArtikulData_0.CanBuy())
				{
					shopItemState_0 = ShopItemState.STATE_BUY;
				}
				else if (bool_1 && wear.Boolean_1)
				{
					shopItemState_0 = ShopItemState.STATE_EDIT_CUSTOM_SKIN;
				}
				if (bool_1 && wear.Boolean_1 && !SkinsController.CanWearCustomSkin(wear.Int32_0))
				{
					shopItemState_0 = ShopItemState.STATE_CREATE_CUSTOM_SKIN;
				}
			}
		}
		if (shopItemState != shopItemState_0 || shopItemState_0 == ShopItemState.STATE_BOUGHT)
		{
			if (shopItemState_0 == ShopItemState.STATE_EQUIPED && action_1 != null)
			{
				action_1(this);
			}
			if (shopItemState_0 == ShopItemState.STATE_NEEDS && !flag && needData_ != null)
			{
				stateNeeds.SetNeedText(needData_.GetNeedText());
			}
			UpdateStikersState();
			SwitchState(shopItemState, shopItemState_0);
		}
	}

	private void UpdateStikersState()
	{
		bool_4 = false;
		bool_5 = false;
		bool_6 = false;
		UserOverrideContentGroupData activeStockItemByType = StocksController.StocksController_0.GetActiveStockItemByType(shopArtikulData_0.Int32_0, ContentGroupItemType.SHOP);
		int num = ((activeStockItemByType != null) ? activeStockItemByType.int_2 : 0);
		bool_7 = num > 0 && Utility.Double_0 < (double)num;
		actionStiker.transform.GetChild(1).gameObject.GetComponent<ActionUUUUUltraKostyl>().actionTimeEnd = num;
		if (shopItemState_0 == ShopItemState.STATE_CAN_BUY || shopItemState_0 == ShopItemState.STATE_CAN_UPGRADE)
		{
			bool_4 = shopArtikulData_0.Boolean_2;
			bool_5 = shopArtikulData_0.Boolean_0;
			if (ShopWindow.ShopWindow_0 != null && ShopWindow.ShopWindow_0.Int32_0 != 0)
			{
				bool_6 = shopArtikulData_0.Boolean_7;
			}
		}
		if (rentSticker.gameObject.activeSelf)
		{
			bool_7 = false;
			bool_4 = false;
			bool_5 = false;
			bool_6 = false;
		}
		if (bool_7)
		{
			bool_4 = false;
			bool_5 = false;
			bool_6 = false;
		}
		if (bool_4)
		{
			bool_5 = false;
			bool_6 = false;
		}
		if (bool_5)
		{
			bool_6 = false;
		}
		if (UserController.UserController_0.GetUserArtikulCount(artikulData_0.Int32_0) > 0)
		{
			bool_4 = false;
			bool_5 = false;
			bool_6 = false;
			bool_7 = false;
		}
		NGUITools.SetActive(newStiker.gameObject, bool_4);
		NGUITools.SetActive(saleStiker.gameObject, bool_5);
		NGUITools.SetActive(bestStiker.gameObject, bool_6);
		NGUITools.SetActive(actionStiker.gameObject, bool_7);
		if (base.isActiveAndEnabled)
		{
			StartCoroutine(ShowArrow());
		}
	}

	public void SetSelected(bool bool_8)
	{
		bool_0 = bool_8;
		UpdateState();
	}

	private void SwitchState(ShopItemState shopItemState_1, ShopItemState shopItemState_2)
	{
		NGUITools.SetActive(states[(int)shopItemState_1].gameObject, false);
		if (shopItemState_2 != ShopItemState.STATE_EQUIPED)
		{
			states[(int)shopItemState_1].itemTitle.Color_0 = Color.white;
		}
		NGUITools.SetActive(states[(int)shopItemState_2].gameObject, true);
		if (shopItemState_2 != ShopItemState.STATE_EQUIPED)
		{
			states[(int)shopItemState_2].itemTitle.Color_0 = ((!bool_0) ? Color.white : Color.green);
		}
	}

	private IEnumerator LoadModel()
	{
		GameObject gameObject = null;
		Vector3 vector3_ = Vector3.zero;
		Vector3 localPosition = Vector3.zero;
		Vector3 eulerAngles = Vector3.zero;
		Vector3 localScale = Vector3.one;
		switch (artikulData_0.SlotType_0)
		{
		case SlotType.SLOT_WEAPON_PRIMARY:
		case SlotType.SLOT_WEAPON_BACKUP:
		case SlotType.SLOT_WEAPON_MELEE:
		case SlotType.SLOT_WEAPON_SPECIAL:
		case SlotType.SLOT_WEAPON_PREMIUM:
		case SlotType.SLOT_WEAPON_SNIPER:
		{
			GameObject gameObject5 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject innerGameObject = UserController.UserController_0.GetInnerGameObject(artikulData_0.Int32_0);
			if (gameObject5 != null && innerGameObject != null)
			{
				WeaponSounds component5 = gameObject5.GetComponent<WeaponSounds>();
				localPosition = new Vector3(0f, -5f, -100f);
				eulerAngles.x += 15f;
				localScale = new Vector3(130f, 130f, 130f);
				GameObject bonusPrefab = innerGameObject.GetComponent<InnerWeaponPars>().bonusPrefab;
				gameObject = GetMesh(bonusPrefab, out vector3_);
			}
			break;
		}
		case SlotType.SLOT_WEAR_SKIN:
			if ((shopItemState_0 == ShopItemState.STATE_CREATE_CUSTOM_SKIN || shopItemState_0 == ShopItemState.STATE_BOUGHT || shopItemState_0 == ShopItemState.STATE_EQUIPED) && bool_1 && SkinsController.GetSkinTexture(artikulData_0.Int32_0) == null)
			{
				NGUITools.SetActive(lockImage.gameObject, true);
				NGUITools.SetActive(image.gameObject, false);
				lockImage.Texture_0 = ImageLoader.LoadTexture("UI/images/skin_empty");
				Vector3 localPosition2 = image.transform.localPosition;
				localPosition2.y = -7f;
				image.transform.localPosition = localPosition2;
				gameObject = null;
			}
			else if (shopItemState_0 != ShopItemState.STATE_BUY && (shopItemState_0 != ShopItemState.STATE_CAN_BUY || !bool_1))
			{
				if (gameObject_0 == null)
				{
					gameObject_0 = Resources.Load<GameObject>("PixlManForSkins");
				}
				GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject_0) as GameObject;
				Texture skinTexture = SkinsController.GetSkinTexture(artikulData_0.Int32_0);
				Utility.SetLayerRecursive(gameObject4, LayerMask.NameToLayer("NGUIRoot"));
				Utility.SetTextureRecursiveFrom(gameObject4, skinTexture, new GameObject[0]);
				localPosition = new Vector3(0f, -130f, -100f);
				gameObject4.transform.localPosition = new Vector3(0f, -83f, -50f);
				gameObject4.transform.localRotation = Quaternion.Euler(new Vector3(4f, -156f, 0f));
				gameObject4.transform.localScale = new Vector3(85f, 85f, 85f);
				MeshRenderer component4 = gameObject4.transform.GetChild(0).GetComponent<MeshRenderer>();
				if (shader_1 == null)
				{
					shader_1 = Shader.Find("Custom/TextureClipLocal");
				}
				component4.material.shader = shader_1;
				component4.material.SetVector("_ClipBounds", new Vector4(0.556f, -0.456f, 10f, -10f));
				component4.material.SetVector("_ClipLocal", new Vector4(0f, 0.81f, 0f, 0f));
				component4.material.SetFloat("_AlphaFrom", 0f);
				component4.material.SetFloat("_AlphaCoeff", 1f);
				WearData wear2 = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
				if (wear2.Boolean_0)
				{
					Texture skinTexture2 = SkinsController.GetSkinTexture(wear2.Int32_0);
					if (skinTexture2 != null)
					{
						component4.material.SetTexture("_MainTex", skinTexture2);
					}
				}
				vector3_ = new Vector3(0f, -1f, 0f);
				gameObject = gameObject4;
			}
			else
			{
				NGUITools.SetActive(image.gameObject, false);
				NGUITools.SetActive(lockImage.gameObject, true);
				gameObject = null;
			}
			break;
		case SlotType.SLOT_WEAR_CAPE:
		{
			GameObject gameObject3 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject gameObjectInstance = ShopWindow.GetGameObjectInstance(artikulData_0.Int32_0, gameObject3.transform.GetChild(0).gameObject);
			ShopPositionParams component2 = gameObject3.GetComponent<ShopPositionParams>();
			localPosition = new Vector3(0f, -5f, -100f);
			eulerAngles = component2.rotationShop;
			localScale = new Vector3(80f, 80f, 80f);
			gameObject = GetMesh(gameObjectInstance, out vector3_);
			WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear.Boolean_0)
			{
				Texture capeTexture = SkinsController.GetCapeTexture(wear.Int32_0);
				if (capeTexture != null)
				{
					MeshRenderer component3 = gameObject.GetComponent<MeshRenderer>();
					component3.material.SetTexture("_MainTex", capeTexture);
				}
			}
			break;
		}
		case SlotType.SLOT_WEAR_HAT:
		case SlotType.SLOT_WEAR_ARMOR:
		case SlotType.SLOT_WEAR_BOOTS:
		case SlotType.SLOT_CONSUM_POTION:
		case SlotType.SLOT_CONSUM_JETPACK:
		case SlotType.SLOT_CONSUM_MECH:
		case SlotType.SLOT_CONSUM_TURRET:
		{
			GameObject gameObject2 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject gameObject_ = gameObject2.transform.GetChild(0).gameObject;
			ShopPositionParams component = gameObject2.GetComponent<ShopPositionParams>();
			localPosition = new Vector3(0f, -5f, -100f);
			eulerAngles = component.rotationShop;
			localScale = new Vector3(130f, 130f, 130f);
			gameObject = GetMesh(gameObject_, out vector3_);
			break;
		}
		}
		if (gameObject != null)
		{
			GameObject gameObject6 = new GameObject
			{
				name = gameObject.name
			};
			gameObject.transform.localPosition = vector3_;
			gameObject.transform.parent = gameObject6.transform;
			Utility.SetLayerRecursive(gameObject6, LayerMask.NameToLayer("NGUIRoot"));
			gameObject6.transform.parent = base.gameObject.transform;
			model = gameObject6.transform;
			model.localPosition = localPosition;
			model.localScale = localScale;
			model.Rotate(eulerAngles, Space.World);
		}
		yield return null;
	}

	public static GameObject GetMesh(GameObject gameObject_1, out Vector3 vector3_0, bool bool_8 = false)
	{
		GameObject gameObject = null;
		vector3_0 = Vector3.zero;
		if (gameObject_1 != null)
		{
			Material[] array = null;
			Mesh mesh = null;
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject_1.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				SkinnedMeshRenderer[] componentsInChildren = gameObject_1.GetComponentsInChildren<SkinnedMeshRenderer>(true);
				if (componentsInChildren != null && componentsInChildren.Length > 0)
				{
					skinnedMeshRenderer = componentsInChildren[0];
				}
			}
			if (skinnedMeshRenderer != null)
			{
				array = skinnedMeshRenderer.materials;
				mesh = skinnedMeshRenderer.sharedMesh;
			}
			else
			{
				MeshFilter component = gameObject_1.GetComponent<MeshFilter>();
				MeshRenderer component2 = gameObject_1.GetComponent<MeshRenderer>();
				if (component != null)
				{
					mesh = component.mesh;
				}
				if (component2 != null)
				{
					array = component2.materials;
				}
			}
			if (array != null && mesh != null)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				if (array.Length > 0)
				{
					Material material = array[0];
					if (shader_0 == null)
					{
						shader_0 = Shader.Find("Custom/TextureClip");
					}
					material.shader = shader_0;
					material.SetVector("_ClipBounds", new Vector4(0.556f, -0.456f, 10f, -10f));
				}
				meshRenderer.materials = array;
				vector3_0 = -meshRenderer.bounds.center;
			}
			if (bool_8)
			{
				UnityEngine.Object.Destroy(gameObject_1);
			}
		}
		return gameObject;
	}

	public void OnClick()
	{
		if (shopArtikulData_0 != null && artikulData_0 != null && !bool_0)
		{
			ShopArtikulController.ShopArtikulController_0.NotNew(artikulData_0.Int32_0);
			UpdateStikersState();
			SetSelected(true);
			if (action_0 != null)
			{
				action_0(this);
			}
		}
	}
}
