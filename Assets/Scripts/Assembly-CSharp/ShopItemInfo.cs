using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;

public class ShopItemInfo : MonoBehaviour
{
	public UITexture image;

	public UILabel title;

	public UILabel capeTitle;

	public ShopItemSkillsPanel skillsPanel;

	public ShopItemInfoUpgrades upgrades;

	public UIWidget modelContainer;

	public UILabel liveTimeTitleLabel;

	public UILabel liveTimeLabel;

	public GameObject rentSticker;

	private ArtikulData artikulData_0;

	private static GameObject gameObject_0;

	private string string_0 = Localizer.Get("ui.day.mini");

	private string string_1 = Localizer.Get("ui.hour.mini");

	private string string_2 = Localizer.Get("ui.min.mini");

	private string string_3 = Localizer.Get("ui.sec.mini");

	[CompilerGenerated]
	private UserArtikul userArtikul_0;

	private UserArtikul UserArtikul_0
	{
		[CompilerGenerated]
		get
		{
			return userArtikul_0;
		}
		[CompilerGenerated]
		set
		{
			userArtikul_0 = value;
		}
	}

	public void Init(ArtikulData artikulData_1)
	{
		artikulData_0 = artikulData_1;
		InitCommon();
		InitSkills();
		InitUpgrades();
	}

	private void OnDestroy()
	{
		DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(TickOneSecond);
	}

	private void InitCommon()
	{
		string text = artikulData_0.String_4;
		if (SkinsController.CanWearCustomWear(artikulData_0.Int32_0))
		{
			text = SkinsController.GetCustomWearName(artikulData_0.Int32_0);
		}
		title.String_0 = text;
		if (capeTitle != null)
		{
			capeTitle.String_0 = string.Empty;
		}
		if (artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_SKIN)
		{
			InitSkin();
		}
		else if (artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_CAPE)
		{
			NGUITools.SetActive(image.gameObject, false);
			if (capeTitle != null)
			{
				capeTitle.String_0 = text;
			}
			InitCape();
		}
		else
		{
			ClearModelContainer();
			NGUITools.SetActive(image.gameObject, true);
			image.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData_0);
		}
		if (rentSticker != null)
		{
			rentSticker.SetActive(false);
			if (artikulData_0.Int32_5 > 0)
			{
				UpdateLiveTime();
			}
		}
	}

	private void UpdateLiveTime()
	{
		UserArtikul_0 = UserController.UserController_0.GetUserArtikulByArtikulId(artikulData_0.Int32_0);
		if (UserArtikul_0 != null && (UserArtikul_0 == null || UserArtikul_0.int_1 != 0))
		{
			if (UserArtikul_0.double_0 > 0.0 && artikulData_0.Int32_5 > 0)
			{
				rentSticker.SetActive(true);
				liveTimeTitleLabel.String_0 = Localizer.Get("ui.shop.left");
				liveTimeLabel.String_0 = Utility.GetLocalizedTime((int)(UserArtikul_0.double_0 + (double)UserArtikul_0.int_3 + (double)artikulData_0.Int32_5 - Utility.Double_0), string_0, string_1, string_2, string_3);
				if (!DependSceneEvent<MainUpdateOneSecond>.Contains(TickOneSecond))
				{
					DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(TickOneSecond);
				}
			}
		}
		else if (artikulData_0.Int32_5 > 0)
		{
			rentSticker.SetActive(true);
			liveTimeTitleLabel.String_0 = Localizer.Get("ui.shop.rent_for");
			liveTimeLabel.String_0 = Utility.GetLocalizedTime(artikulData_0.Int32_5, string_0, string_1, string_2, string_3, false);
			if (DependSceneEvent<MainUpdateOneSecond>.Contains(TickOneSecond))
			{
				DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(TickOneSecond);
			}
		}
	}

	private void TickOneSecond()
	{
		if (rentSticker != null && rentSticker.activeSelf && UserArtikul_0 != null)
		{
			liveTimeLabel.String_0 = Utility.GetLocalizedTime((int)(UserArtikul_0.double_0 + (double)UserArtikul_0.int_3 + (double)artikulData_0.Int32_5 - Utility.Double_0), string_0, string_1, string_2, string_3);
		}
	}

	private void InitSkin()
	{
		if (ClearModelContainer())
		{
			if (gameObject_0 == null)
			{
				gameObject_0 = Resources.Load<GameObject>("PixlManForSkins");
				MeshRenderer component = gameObject_0.transform.GetChild(0).GetComponent<MeshRenderer>();
				component.material.shader = Shader.Find("Unlit/Texture");
			}
			GameObject gameObject = Object.Instantiate(gameObject_0) as GameObject;
			Texture texture = SkinsController.GetSkinTexture(artikulData_0.Int32_0);
			if (texture == null)
			{
				texture = LocalSkinTextureData.Texture2D_1;
			}
			Utility.SetLayerRecursive(gameObject, LayerMask.NameToLayer("NGUIRoot"));
			Utility.SetTextureRecursiveFrom(gameObject, texture, new GameObject[0]);
			gameObject.transform.parent = modelContainer.transform;
			gameObject.transform.localPosition = new Vector3(0f, -78f, -50f);
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(4f, -156f, 0f));
			gameObject.transform.localScale = new Vector3(80f, 80f, 80f);
		}
	}

	private void InitCape()
	{
		if (!ClearModelContainer())
		{
			return;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
		GameObject gameObject2 = Object.Instantiate(gameObject.transform.GetChild(0).gameObject) as GameObject;
		MeshRenderer component = gameObject2.GetComponent<MeshRenderer>();
		component.material.shader = Shader.Find("Unlit/Texture");
		WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
		if (wear != null)
		{
			Texture capeTexture = SkinsController.GetCapeTexture(wear.Int32_0);
			if (capeTexture != null)
			{
				component.material.SetTexture("_MainTex", capeTexture);
			}
		}
		Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
		gameObject2.transform.parent = modelContainer.transform;
		gameObject2.transform.localPosition = new Vector3(6f, 30f, -50f);
		gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(4f, 26f, -8f));
		gameObject2.transform.localScale = new Vector3(70f, 70f, 70f);
	}

	private bool ClearModelContainer()
	{
		if (modelContainer == null)
		{
			return false;
		}
		foreach (Transform item in modelContainer.transform)
		{
			item.parent = null;
			Object.Destroy(item.gameObject);
		}
		return true;
	}

	private void InitSkills()
	{
		if (!(skillsPanel == null))
		{
			if (!artikulData_0.Boolean_1 && !artikulData_0.Boolean_2)
			{
				NGUITools.SetActive(skillsPanel.gameObject, false);
				return;
			}
			NGUITools.SetActive(skillsPanel.gameObject, true);
			skillsPanel.Init(artikulData_0);
		}
	}

	private void InitUpgrades()
	{
		if (!(upgrades == null))
		{
			upgrades.Init(artikulData_0);
		}
	}
}
