using UnityEngine;
using engine.helpers;

public class ProfileSlotItem : MonoBehaviour
{
	public UIWidget widget;

	public UITexture icon;

	public GameObject capeContainer;

	public Texture DefaultTexture;

	private Texture texture_0;

	public void SetData(SlotType slotType_0, int int_0)
	{
		int anyUserArtikulIdFromSlot = UserController.UserController_0.GetAnyUserArtikulIdFromSlot(slotType_0, int_0);
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(anyUserArtikulIdFromSlot);
		if (artikul == null)
		{
			return;
		}
		if (artikul.SlotType_0 != SlotType.SLOT_WEAR_CAPE)
		{
			Texture texture = ImageLoader.LoadArtikulTexture(anyUserArtikulIdFromSlot);
			if (texture != null)
			{
				icon.Texture_0 = texture;
				icon.Single_2 = 1f;
			}
		}
		else
		{
			texture_0 = SkinsController.GetOtherUserSkinTexture(anyUserArtikulIdFromSlot, int_0);
			SetCape(artikul);
		}
		TooltipInfo component = GetComponent<TooltipInfo>();
		ArtikulData artikul2 = ArtikulController.ArtikulController_0.GetArtikul(artikul.Int32_0);
		if (artikul2.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul2.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
		{
			component.weaponID = artikul.Int32_0;
		}
		else if (artikul2.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul2.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
		{
			component.wearID = artikul.Int32_0;
		}
	}

	private void SetCape(ArtikulData artikulData_0)
	{
		icon.gameObject.SetActive(false);
		GameObject gameObject = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
		GameObject gameObject_ = Object.Instantiate(gameObject.transform.GetChild(0).gameObject) as GameObject;
		ShopPositionParams component = gameObject.GetComponent<ShopPositionParams>();
		Vector3 localPosition = new Vector3(0f, -2f, -100f);
		Vector3 rotationShop = component.rotationShop;
		Vector3 localScale = new Vector3(55f, 55f, 55f);
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
