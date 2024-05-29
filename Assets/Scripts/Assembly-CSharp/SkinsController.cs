using System;
using UnityEngine;
using engine.helpers;
using engine.network;

public static class SkinsController
{
	public static int Int32_0
	{
		get
		{
			ArtikulData artikulDataFromSlot = UserController.UserController_0.GetArtikulDataFromSlot(SlotType.SLOT_WEAR_SKIN);
			if (artikulDataFromSlot != null)
			{
				WearData wear = WearController.WearController_0.GetWear(artikulDataFromSlot.Int32_0);
				if (wear != null && wear.Boolean_1)
				{
					if (wear.Boolean_0)
					{
						if (LocalSkinTextureData.HaveCustomWear(artikulDataFromSlot.Int32_0))
						{
							return artikulDataFromSlot.Int32_0;
						}
					}
					else if (LocalSkinTextureData.GetTextureByIdAndType(artikulDataFromSlot.Int32_0, LocalSkinTextureData.TextureType.SKIN_PERS) != null)
					{
						return artikulDataFromSlot.Int32_0;
					}
				}
			}
			return LocalSkinTextureData.Int32_1;
		}
		set
		{
			WearData wear = WearController.WearController_0.GetWear(value);
			if (wear != null && wear.Boolean_1 && UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_SKIN) != value)
			{
				Texture2D textureById = LocalSkinTextureData.GetTextureById(value);
				if (!(textureById == null))
				{
					UserController.UserController_0.EquipArtikul(value);
				}
			}
		}
	}

	public static WearData WearData_0
	{
		get
		{
			return WearStorage.Get.Storage.GetObjectByKey(Int32_0);
		}
		set
		{
			if (value != null)
			{
				Int32_0 = value.Int32_0;
			}
		}
	}

	public static Texture2D Texture2D_0
	{
		get
		{
			return GetSkinTexture(Int32_0);
		}
	}

	public static WearData WearData_1
	{
		get
		{
			int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE);
			WearData wear = WearController.WearController_0.GetWear(artikulIdFromSlot);
			if (wear != null && wear.Boolean_2)
			{
				return wear;
			}
			return null;
		}
	}

	public static Texture2D GetSkinTexture(int int_0)
	{
		return LocalSkinTextureData.GetTextureByIdAndType(int_0, LocalSkinTextureData.TextureType.SKIN_PERS);
	}

	public static bool CanWearCustomSkin(int int_0)
	{
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null && objectByKey.Boolean_0 && objectByKey.Boolean_1)
		{
			return LocalSkinTextureData.HaveCustomWear(int_0);
		}
		return false;
	}

	public static bool CanWearCustomCape(int int_0)
	{
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null && objectByKey.Boolean_0 && objectByKey.Boolean_2)
		{
			return LocalSkinTextureData.HaveCustomWear(int_0);
		}
		return false;
	}

	public static bool CanWearCustomWear(int int_0)
	{
		return LocalSkinTextureData.HaveCustomWear(int_0);
	}

	public static string GetCustomWearName(int int_0)
	{
		UserTextureSkinData userDataById = LocalSkinTextureData.GetUserDataById(int_0);
		if (userDataById == null)
		{
			return string.Empty;
		}
		return userDataById.string_1;
	}

	public static string CustomSkinName(int int_0)
	{
		UserTextureSkinData userDataByIdAndType = LocalSkinTextureData.GetUserDataByIdAndType(int_0, LocalSkinTextureData.TextureType.SKIN_PERS);
		if (userDataByIdAndType != null)
		{
			return userDataByIdAndType.string_1;
		}
		return string.Empty;
	}

	public static Texture2D GetCapeTexture(int int_0)
	{
		Texture2D textureByIdAndType = LocalSkinTextureData.GetTextureByIdAndType(int_0, LocalSkinTextureData.TextureType.CAPE);
		if ((bool)textureByIdAndType)
		{
			return textureByIdAndType;
		}
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null && objectByKey.Boolean_0 && objectByKey.Boolean_2)
		{
			return LocalSkinTextureData.Texture2D_0;
		}
		return null;
	}

	public static Texture2D TextureFromString(string string_0, int int_0 = 64, int int_1 = 32)
	{
		byte[] byte_ = Convert.FromBase64String(string_0);
		return TextureFromData(byte_, int_0, int_1);
	}

	public static Texture2D TextureFromData(byte[] byte_0, int int_0 = 64, int int_1 = 32)
	{
		Texture2D texture2D = new Texture2D(int_0, int_1, TextureFormat.ARGB32, false);
		texture2D.LoadImage(byte_0);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}

	public static string StringFromTexture(Texture2D texture2D_0)
	{
		byte[] inArray = texture2D_0.EncodeToPNG();
		return Convert.ToBase64String(inArray);
	}

	public static void Init()
	{
		LocalSkinTextureData.Init();
	}

	public static void editCustomSkin(int int_0)
	{
		SkinEditorScreen.SkinEditorScreen_0.Show(int_0);
	}

	public static string byteToString(byte[] byte_0)
	{
		string text = string.Empty;
		for (int i = 0; i < byte_0.Length; i++)
		{
			text += byte_0[i];
		}
		return text;
	}

	public static UserTextureSkinData AddUserSkin(string string_0, Texture2D texture2D_0, int int_0)
	{
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null && (objectByKey.Boolean_1 || objectByKey.Boolean_2) && objectByKey.Boolean_0)
		{
			byte[] array = texture2D_0.EncodeToPNG();
			if (array != null && array.Length != 0)
			{
				UserTextureSkinData userTextureSkinData = LocalSkinTextureData.GetUserDataById(int_0);
				if (userTextureSkinData == null)
				{
					userTextureSkinData = new UserTextureSkinData();
				}
				userTextureSkinData.string_1 = string_0;
				userTextureSkinData.byte_0 = array;
				userTextureSkinData.skinType_0 = (objectByKey.Boolean_1 ? UserTextureSkinData.SkinType.SKIN_PERS : UserTextureSkinData.SkinType.SKIN_CAPE);
				userTextureSkinData.int_0 = int_0;
				return userTextureSkinData;
			}
			return null;
		}
		return null;
	}

	public static void DeleteUserSkin(int int_0)
	{
		WearData wear = WearController.WearController_0.GetWear(int_0);
		UserTextureSkinData userDataById = LocalSkinTextureData.GetUserDataById(int_0);
		if (wear == null || !wear.Boolean_0 || userDataById == null || (!wear.Boolean_1 && !wear.Boolean_2))
		{
			return;
		}
		bool flag = false;
		if (wear.Boolean_1 && wear.Int32_0 == Int32_0)
		{
			flag = true;
		}
		else if (wear.Boolean_2 && UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE) == wear.Int32_0)
		{
			flag = true;
		}
		if (flag)
		{
			if (wear.Boolean_1)
			{
				WearController.WearController_0.EquipWearByArtikulId(LocalSkinTextureData.Int32_1);
			}
			else
			{
				WearController.WearController_0.UnequipWearByArtikulId(wear.Int32_0);
			}
		}
		UpdateUserSkinNetworkCommand updateUserSkinNetworkCommand = new UpdateUserSkinNetworkCommand();
		updateUserSkinNetworkCommand.userTextureSkinData_0 = userDataById;
		updateUserSkinNetworkCommand.bool_0 = true;
		AbstractNetworkCommand.Send(updateUserSkinNetworkCommand);
	}

	public static void SetTextureRecursivelyFrom(GameObject gameObject_0, Texture texture_0, GameObject[] gameObject_1 = null)
	{
		Utility.SetTextureRecursiveFrom(gameObject_0, texture_0, gameObject_1);
	}

	public static int GetOtherUserCurrentSkinId(int int_0)
	{
		ArtikulData anyUserArtikulDataFromSlot = UserController.UserController_0.GetAnyUserArtikulDataFromSlot(SlotType.SLOT_WEAR_SKIN, int_0);
		if (anyUserArtikulDataFromSlot == null)
		{
			return LocalSkinTextureData.Int32_1;
		}
		WearData wear = WearController.WearController_0.GetWear(anyUserArtikulDataFromSlot.Int32_0);
		if (wear != null && wear.Boolean_1)
		{
			if (wear.Boolean_0)
			{
				if (LocalSkinTextureData.HaveOtherCustomWear(anyUserArtikulDataFromSlot.Int32_0, int_0))
				{
					return anyUserArtikulDataFromSlot.Int32_0;
				}
			}
			else if (LocalSkinTextureData.GetTextureByIdAndType(anyUserArtikulDataFromSlot.Int32_0, LocalSkinTextureData.TextureType.SKIN_PERS) != null)
			{
				return anyUserArtikulDataFromSlot.Int32_0;
			}
		}
		return LocalSkinTextureData.Int32_1;
	}

	public static Texture2D GetOtherUserCurrentSkinTexture(int int_0)
	{
		return GetOtherUserSkinTexture(GetOtherUserCurrentSkinId(int_0), int_0);
	}

	public static Texture2D GetOtherUserSkinTexture(int int_0, int int_1)
	{
		WearData wear = WearController.WearController_0.GetWear(int_0);
		if (wear == null)
		{
			return null;
		}
		if (!wear.Boolean_0)
		{
			return LocalSkinTextureData.GetTextureByIdAndType(int_0, LocalSkinTextureData.TextureType.SKIN_PERS);
		}
		return LocalSkinTextureData.GetTextureByIdTypeUid(int_0, LocalSkinTextureData.TextureType.SKIN_PERS, int_1);
	}
}
