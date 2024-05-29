using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using engine.helpers;

public class KillCamShopItem : MonoBehaviour
{
	public UIWidget widget;

	public UITexture icon;

	public new UILabel name;

	public UIButton button;

	public UILabel price;

	public GameObject capeContainer;

	public Texture DefaultTexture;

	private UIPlaySound uiplaySound_0;

	private bool bool_0;

	private ShopArtikulData shopArtikulData_0;

	private Texture texture_0;

	private void Awake()
	{
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
	}

	public void SetData(int int_0, Texture texture_1)
	{
		texture_0 = texture_1;
		button.gameObject.SetActive(false);
		shopArtikulData_0 = null;
		List<ShopArtikulData> shopArtikulsByArtikulId = ShopArtikulController.ShopArtikulController_0.GetShopArtikulsByArtikulId(int_0);
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_0);
		if (shopArtikulsByArtikulId != null && shopArtikulsByArtikulId.Count != 0)
		{
			shopArtikulData_0 = shopArtikulsByArtikulId[0];
		}
		else
		{
			if (artikul == null || (artikul.SlotType_0 != SlotType.SLOT_CONSUM_GRENADE && artikul.SlotType_0 != SlotType.SLOT_CONSUM_TURRET))
			{
				SetDefault();
				return;
			}
			shopArtikulData_0 = null;
		}
		if (artikul == null)
		{
			SetDefault();
			return;
		}
		if (artikul.SlotType_0 != SlotType.SLOT_WEAR_CAPE)
		{
			Texture texture = ImageLoader.LoadArtikulTexture(int_0);
			if (texture != null)
			{
				icon.Texture_0 = texture;
			}
			else
			{
				SetDefaultTexture();
			}
		}
		else
		{
			SetCape(artikul);
		}
		string text = artikul.String_4;
		if (artikul.Int32_2 > 0)
		{
			Regex regex = new Regex("(UP|LVL)\\d+");
			Match match = regex.Match(text);
			if (match != null && !match.Value.Equals(string.Empty))
			{
				text = regex.Replace(text, string.Format("{0}{1}{2}", "[FF7F00]", match.Value, "[-]"));
			}
		}
		if (name != null)
		{
			name.String_0 = text;
		}
		updateButton();
		TooltipInfo component = base.gameObject.GetComponent<TooltipInfo>();
		if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
		{
			component.weaponID = int_0;
		}
		else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
		{
			component.wearID = int_0;
		}
	}

	private void updateButton()
	{
		if (shopArtikulData_0 != null && UserController.UserController_0.GetUserArtikulCount(shopArtikulData_0.Int32_1) <= 0 && shopArtikulData_0.GetArtikul().Int32_5 <= 0)
		{
			int num = shopArtikulData_0.GetPrice();
			if (!shopArtikulData_0.Boolean_3 || shopArtikulData_0.Boolean_5 || shopArtikulData_0.GetArtikul().Boolean_0)
			{
				return;
			}
			if (shopArtikulData_0.GetArtikul().Int32_1 != 0)
			{
				List<ArtikulData> upgrades = ArtikulController.ArtikulController_0.GetUpgrades(shopArtikulData_0.Int32_1);
				if (upgrades.Count > 0)
				{
					for (int i = 0; i < upgrades.Count; i++)
					{
						if (UserController.UserController_0.GetUserArtikulCount(upgrades[i].Int32_0) > 0)
						{
							button.gameObject.SetActive(false);
							return;
						}
					}
				}
			}
			if (!shopArtikulData_0.CanBuy(false))
			{
				button.gameObject.SetActive(false);
				return;
			}
			price.String_0 = num.ToString();
			button.gameObject.SetActive(true);
		}
		else
		{
			button.gameObject.SetActive(false);
		}
	}

	public void OnBuyClick()
	{
		if (!bool_0)
		{
			bool_0 = true;
			if (uiplaySound_0 != null && Defs.Boolean_0)
			{
				uiplaySound_0.Play();
			}
			UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
			ShopArtikulController.ShopArtikulController_0.BuyArtikul(shopArtikulData_0.Int32_0, true, delegate
			{
				bool_0 = false;
			}, ShopArtikulController.SourceBuyType.TYPE_KILLCAM_WND);
		}
	}

	private void SetDefault()
	{
		if (name != null)
		{
			name.String_0 = string.Empty;
		}
		SetDefaultTexture();
	}

	private void SetDefaultTexture()
	{
		icon.Texture_0 = DefaultTexture;
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_0 = false;
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		updateButton();
	}

	private void SetCape(ArtikulData artikulData_0)
	{
		icon.gameObject.SetActive(false);
		GameObject gameObject = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
		GameObject gameObject_ = Object.Instantiate(gameObject.transform.GetChild(0).gameObject) as GameObject;
		ShopPositionParams component = gameObject.GetComponent<ShopPositionParams>();
		Vector3 localPosition = new Vector3(0f, -5f, -100f);
		Vector3 rotationShop = component.rotationShop;
		Vector3 localScale = ((!(ProfileWindow.ProfileWindow_0 == null)) ? new Vector3(60f, 60f, 60f) : new Vector3(80f, 80f, 80f));
		Vector3 vector3_;
		GameObject mesh = ShopItem.GetMesh(gameObject_, out vector3_, true);
		MeshRenderer component2 = mesh.GetComponent<MeshRenderer>();
		if (component2 != null)
		{
			Shader shader = Shader.Find("Unlit/Texture");
			if (shader != null)
			{
				component2.material.shader = shader;
			}
		}
		WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
		if (wear.Boolean_0)
		{
			Texture texture = texture_0;
			if (texture != null)
			{
				MeshRenderer component3 = mesh.GetComponent<MeshRenderer>();
				component3.material.SetTexture("_MainTex", texture);
			}
		}
		if (mesh != null)
		{
			GameObject gameObject2 = new GameObject();
			gameObject2.name = mesh.name;
			mesh.transform.localPosition = vector3_;
			mesh.transform.parent = gameObject2.transform;
			Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
			gameObject2.transform.parent = capeContainer.transform;
			Transform transform = gameObject2.transform;
			transform.localPosition = localPosition;
			transform.localScale = localScale;
			transform.Rotate(rotationShop, Space.World);
		}
	}
}
