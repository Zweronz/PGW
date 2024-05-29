using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public class StockWindowItem : MonoBehaviour
{
	public UILabel title;

	public UITexture icon;

	public Transform model;

	public UILabel needLabel;

	public UISprite border;

	public UISprite timeIcon;

	public ShopArtikulData articul;

	public BonusItemData bonusArticul;

	public StockWndType wndType;

	private ArtikulData artikulData_0;

	private bool bool_0;

	[CompilerGenerated]
	private static Action action_0;

	private void Start()
	{
		if (articul != null)
		{
			artikulData_0 = articul.GetArtikul();
			title.String_0 = artikulData_0.String_4;
			if (artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_CAPE && artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_SKIN)
			{
				icon.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData_0.Int32_0);
			}
			else
			{
				NGUITools.SetActive(icon.gameObject, false);
				StartCoroutine(LoadModel());
			}
			NeedData needData_ = null;
			bool_0 = articul.NeedsData_0.Check(out needData_);
			needLabel.gameObject.SetActive(!bool_0);
			if (needData_ != null)
			{
				needLabel.String_0 = needData_.GetNeedText();
			}
			StockWndType stockWndType = wndType;
			if (stockWndType != StockWndType.BATMAN)
			{
				border.String_0 = "item_border";
			}
			else
			{
				border.String_0 = "batman_item_border";
			}
		}
		else
		{
			if (bonusArticul == null)
			{
				return;
			}
			title.String_0 = string.Empty;
			artikulData_0 = ArtikulController.ArtikulController_0.GetArtikul(bonusArticul.int_1);
			switch (bonusArticul.bonusItemType_0)
			{
			case BonusItemData.BonusItemType.BONUS_ITEM_ARTICUL:
				title.String_0 = artikulData_0.String_4;
				if (artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_CAPE && artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_SKIN)
				{
					icon.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData_0.Int32_0);
				}
				else
				{
					NGUITools.SetActive(icon.gameObject, false);
					StartCoroutine(LoadModel());
				}
				if (timeIcon != null)
				{
					timeIcon.gameObject.SetActive(artikulData_0.Int32_5 > 0);
				}
				break;
			case BonusItemData.BonusItemType.BONUS_ITEM_MONEY:
				title.String_0 = Localizer.Get("ui.gacha_item.money");
				icon.Texture_0 = ImageLoader.LoadTexture("UI/images/coins");
				break;
			case BonusItemData.BonusItemType.BONUS_ITEM_EXPIRIENCE:
				title.String_0 = Localizer.Get("ui.gacha_item.exp");
				icon.Texture_0 = ImageLoader.LoadTexture("UI/images/exp");
				break;
			case BonusItemData.BonusItemType.BONUS_ITEM_SKILL:
			case BonusItemData.BonusItemType.BONUS_ITEM_BONUS:
				break;
			}
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
		case SlotType.SLOT_WEAR_CAPE:
		{
			GameObject gameObject3 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject gameObject_ = UnityEngine.Object.Instantiate(gameObject3.transform.GetChild(0).gameObject) as GameObject;
			ShopPositionParams component2 = gameObject3.GetComponent<ShopPositionParams>();
			localPosition = new Vector3(80f, -55f, -100f);
			eulerAngles = component2.rotationShop;
			localScale = new Vector3(80f, 80f, 80f);
			gameObject = GetMesh(gameObject_, out vector3_);
			WearData wear2 = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear2.Boolean_0)
			{
				Texture capeTexture = SkinsController.GetCapeTexture(wear2.Int32_0);
				if (capeTexture != null)
				{
					MeshRenderer component3 = gameObject.GetComponent<MeshRenderer>();
					component3.material.SetTexture("_MainTex", capeTexture);
				}
			}
			break;
		}
		case SlotType.SLOT_WEAR_SKIN:
		{
			GameObject original = Resources.Load<GameObject>("PixlManForSkins");
			GameObject gameObject2 = UnityEngine.Object.Instantiate(original) as GameObject;
			Texture skinTexture = SkinsController.GetSkinTexture(artikulData_0.Int32_0);
			Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
			Utility.SetTextureRecursiveFrom(gameObject2, skinTexture, new GameObject[0]);
			Vector3 vector;
			if (bonusArticul != null)
			{
				vector = new Vector3(60f, -180f, -100f);
			}
			else
			{
				Vector3 vector2 = new Vector3(80f, -180f, -100f);
				vector = vector2;
			}
			localPosition = vector;
			gameObject2.transform.localPosition = new Vector3(0f, -83f, -50f);
			gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(4f, -156f, 0f));
			gameObject2.transform.localScale = new Vector3(85f, 85f, 85f);
			MeshRenderer component = gameObject2.transform.GetChild(0).GetComponent<MeshRenderer>();
			Shader shader = Shader.Find("Custom/TextureClipLocal");
			component.material.shader = shader;
			component.material.SetVector("_ClipBounds", (bonusArticul == null) ? new Vector4(-0.26f, -0.52f, 0.77f, -0.77f) : new Vector4(-0.26f, -0.52f, 0.9f, -0.9f));
			component.material.SetVector("_ClipLocal", new Vector4(0f, 0.81f, 0f, 0f));
			component.material.SetFloat("_AlphaFrom", 0f);
			component.material.SetFloat("_AlphaCoeff", 1f);
			WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear.Boolean_0)
			{
				Texture skinTexture2 = SkinsController.GetSkinTexture(wear.Int32_0);
				if (skinTexture2 != null)
				{
					component.material.SetTexture("_MainTex", skinTexture2);
				}
			}
			vector3_ = new Vector3(0f, -1f, 0f);
			gameObject = gameObject2;
			break;
		}
		}
		if (gameObject != null)
		{
			GameObject gameObject4 = new GameObject
			{
				name = gameObject.name
			};
			gameObject.transform.localPosition = vector3_;
			gameObject.transform.parent = gameObject4.transform;
			Utility.SetLayerRecursive(gameObject4, LayerMask.NameToLayer("NGUIRoot"));
			gameObject4.transform.parent = base.gameObject.transform;
			model = gameObject4.transform;
			model.localPosition = localPosition;
			model.localScale = localScale;
			model.Rotate(eulerAngles, Space.World);
		}
		yield return null;
	}

	public static GameObject GetMesh(GameObject gameObject_0, out Vector3 vector3_0, bool bool_1 = false)
	{
		GameObject gameObject = null;
		vector3_0 = Vector3.zero;
		if (gameObject_0 != null)
		{
			Material[] array = null;
			Mesh mesh = null;
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject_0.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				SkinnedMeshRenderer[] componentsInChildren = gameObject_0.GetComponentsInChildren<SkinnedMeshRenderer>(true);
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
				MeshFilter component = gameObject_0.GetComponent<MeshFilter>();
				MeshRenderer component2 = gameObject_0.GetComponent<MeshRenderer>();
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
					Shader shader = Shader.Find("Custom/TextureClip");
					material.shader = shader;
					material.SetVector("_ClipBounds", new Vector4(-0.26f, -0.52f, 0.77f, -0.77f));
				}
				meshRenderer.materials = array;
				vector3_0 = -meshRenderer.bounds.center;
			}
			if (bool_1)
			{
				UnityEngine.Object.Destroy(gameObject_0);
			}
		}
		return gameObject;
	}

	private void OnClick()
	{
		if (articul != null && bool_0)
		{
			StockWindow.StockWindow_0.Hide();
			Lobby.Lobby_0.Hide();
			ShopWindowParams shopWindowParams = new ShopWindowParams();
			shopWindowParams.int_0 = artikulData_0.Int32_0;
			shopWindowParams.int_1 = articul.Int32_0;
			shopWindowParams.action_0 = delegate
			{
				Lobby.Lobby_0.Show();
			};
			shopWindowParams.openStyle_0 = ShopWindow.OpenStyle.ANIMATED;
			shopWindowParams.sourceBuyType_0 = ShopArtikulController.SourceBuyType.TYPE_SHOP_WND;
			ShopWindow.Show(shopWindowParams);
		}
	}
}
