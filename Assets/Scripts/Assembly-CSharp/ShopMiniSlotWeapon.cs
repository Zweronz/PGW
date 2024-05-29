using UnityEngine;

public class ShopMiniSlotWeapon : MonoBehaviour
{
	public UITexture image;

	public UITexture defaultImage;

	public SlotType slotType;

	private int int_0;

	private void Awake()
	{
		UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
		UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArticulChange);
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
		UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArticulChange);
	}

	private void OnClick()
	{
		if (!(ShopWindow.ShopWindow_0 == null))
		{
			if (int_0 > 0)
			{
				ShopWindow.ShopWindow_0.OpenItem(int_0);
			}
			else
			{
				ShopWindow.ShopWindow_0.OpenTab(slotType);
			}
		}
	}

	private void OnArticulChange(UsersData.EventData eventData_0)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(eventData_0.int_0);
		if (slotType == artikul.SlotType_0)
		{
			Setup();
		}
	}

	private void OnSlotChange(UsersData.EventData eventData_0)
	{
		if (slotType == (SlotType)eventData_0.int_0)
		{
			Setup();
		}
	}

	public void Setup()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType);
		bool flag = UserController.UserController_0.GetUserArtikulCount(artikulIdFromSlot) > 0;
		if (artikulIdFromSlot > 0 && flag)
		{
			int_0 = artikulIdFromSlot;
			SetImage(ImageLoader.LoadArtikulTexture(artikulIdFromSlot));
		}
		else
		{
			int_0 = 0;
			SetDefault();
		}
	}

	private void SetImage(Texture texture_0)
	{
		NGUITools.SetActive(image.gameObject, true);
		NGUITools.SetActive(defaultImage.gameObject, false);
		image.Texture_0 = texture_0;
	}

	public void SetDefault()
	{
		NGUITools.SetActive(image.gameObject, false);
		NGUITools.SetActive(defaultImage.gameObject, false);
	}
}
