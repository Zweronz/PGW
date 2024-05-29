using UnityEngine;

public class ShopMiniSlotGear : MonoBehaviour
{
	public UILabel gearCount;

	public SlotType slotType;

	private void Awake()
	{
		UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
		UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtChange);
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
		UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtChange);
	}

	private void OnSlotChange(UsersData.EventData eventData_0)
	{
		if (slotType == (SlotType)eventData_0.int_0)
		{
			Setup();
		}
	}

	private void OnArtChange(UsersData.EventData eventData_0)
	{
		UserArtikul userArtikulById = UserController.UserController_0.GetUserArtikulById(eventData_0.string_0);
		if (userArtikulById == null)
		{
			Setup();
			return;
		}
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(userArtikulById.int_0);
		if (artikul != null && artikul.SlotType_0 == slotType)
		{
			int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType);
			if (artikulIdFromSlot == artikul.Int32_0)
			{
				Setup();
			}
		}
	}

	public void Setup()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType);
		int userArtikulCount = UserController.UserController_0.GetUserArtikulCount(artikulIdFromSlot);
		SetCount(userArtikulCount);
	}

	private void SetCount(int int_0)
	{
		gearCount.String_0 = int_0.ToString();
	}
}
