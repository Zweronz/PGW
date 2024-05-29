using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class LocalSkinTextureData
{
	public enum TextureType
	{
		SKIN_PERS = 0,
		CAPE = 1,
		MAX_TYPE = 2
	}

	private const string string_0 = "SkinsTextures";

	private const string string_1 = "Clear_Skin";

	private const string string_2 = "cape_CustomTexture";

	private static Dictionary<TextureType, Dictionary<int, Texture2D>> dictionary_0;

	private static Dictionary<TextureType, Dictionary<int, UserTextureSkinData>> dictionary_1;

	private static Dictionary<TextureType, Dictionary<int, Dictionary<int, Texture2D>>> dictionary_2;

	private static Dictionary<TextureType, Dictionary<int, Dictionary<int, UserTextureSkinData>>> dictionary_3;

	private static Texture2D texture2D_0;

	private static Texture2D texture2D_1;

	public static Texture2D Texture2D_0
	{
		get
		{
			return texture2D_0;
		}
	}

	public static Texture2D Texture2D_1
	{
		get
		{
			return texture2D_1;
		}
	}

	public static int Int32_0
	{
		get
		{
			return dictionary_0[TextureType.SKIN_PERS].Count;
		}
	}

	public static int Int32_1
	{
		get
		{
			return 42;
		}
	}

	static LocalSkinTextureData()
	{
		dictionary_0 = new Dictionary<TextureType, Dictionary<int, Texture2D>>();
		dictionary_1 = new Dictionary<TextureType, Dictionary<int, UserTextureSkinData>>();
		dictionary_2 = new Dictionary<TextureType, Dictionary<int, Dictionary<int, Texture2D>>>();
		dictionary_3 = new Dictionary<TextureType, Dictionary<int, Dictionary<int, UserTextureSkinData>>>();
		texture2D_0 = null;
		texture2D_1 = null;
		for (TextureType textureType = TextureType.SKIN_PERS; textureType < TextureType.MAX_TYPE; textureType++)
		{
			dictionary_0.Add(textureType, new Dictionary<int, Texture2D>());
			dictionary_1.Add(textureType, new Dictionary<int, UserTextureSkinData>());
			dictionary_2.Add(textureType, new Dictionary<int, Dictionary<int, Texture2D>>());
			dictionary_3.Add(textureType, new Dictionary<int, Dictionary<int, UserTextureSkinData>>());
		}
	}

	public static void Clear()
	{
		texture2D_0 = null;
		texture2D_1 = null;
		foreach (KeyValuePair<TextureType, Dictionary<int, Texture2D>> item in dictionary_0)
		{
			item.Value.Clear();
		}
		foreach (KeyValuePair<TextureType, Dictionary<int, UserTextureSkinData>> item2 in dictionary_1)
		{
			item2.Value.Clear();
		}
	}

	private static List<WearData> wear
	{
		get
		{
			if (_wear == null)
			{
				List<WearData> wearData = new List<WearData>();
				Wear wear = Resources.Load<Wear>("Wear");

				foreach (Wear.WearData _wearData in wear.wear)
				{
					wearData.Add(_wearData.ToWearData());
				}

				_wear = wearData;
			}

			return _wear;
		}
	}

	private static List<WearData> _wear;

	public static void Init()
	{
		Clear();
		Dictionary<int, UserTextureSkinData> dictionary = new Dictionary<int, UserTextureSkinData>();
		foreach (KeyValuePair<string, UserTextureSkinData> item in UserController.UserController_0.UserData_0.dictionary_4)
		{
			dictionary.Add(item.Value.int_0, item.Value);
		}
		foreach (WearData @object in wear)
		{
			if (@object.Boolean_0)
			{
				if (!dictionary.ContainsKey(@object.Int32_0))
				{
					continue;
				}
				if (@object.Boolean_1)
				{
					UserTextureSkinData userTextureSkinData = dictionary[@object.Int32_0];
					Log.AddLine(string.Format("Load Custom Skin id = {0}  bite = {1}", @object.Int32_0, userTextureSkinData.byte_0.Length));
					Texture2D texture2D = SkinsController.TextureFromData(userTextureSkinData.byte_0);
					if ((bool)texture2D)
					{
						dictionary_0[TextureType.SKIN_PERS].Add(@object.Int32_0, texture2D);
						dictionary_1[TextureType.SKIN_PERS].Add(@object.Int32_0, userTextureSkinData);
					}
				}
				else if (@object.Boolean_2)
				{
					UserTextureSkinData userTextureSkinData2 = dictionary[@object.Int32_0];
					Texture2D texture2D2 = SkinsController.TextureFromData(userTextureSkinData2.byte_0);
					if ((bool)texture2D2)
					{
						dictionary_0[TextureType.CAPE].Add(@object.Int32_0, texture2D2);
						dictionary_1[TextureType.CAPE].Add(@object.Int32_0, userTextureSkinData2);
					}
				}
			}
			else if (@object.Boolean_1)
			{
				string text = @object.String_0;
				if (text.Contains(".png"))
				{
					text = text.Remove(text.IndexOf(".png"));
				}
				string path = "SkinsTextures/" + text;
				Texture2D texture2D3 = Resources.Load(path) as Texture2D;
				if (texture2D3 != null)
				{
					dictionary_0[TextureType.SKIN_PERS].Add(@object.Int32_0, texture2D3);
				}
				else
				{
					Log.AddLine(string.Format("WARNING: skin id= {0} name= {1} can not load texture name= {2}", @object.Int32_0, @object.ArtikulData_0.String_4, @object.String_0), LogType.Warning);
				}
			}
		}
		texture2D_0 = Resources.Load("cape_CustomTexture") as Texture2D;
		texture2D_0.filterMode = FilterMode.Point;
		texture2D_0.Apply();
		texture2D_1 = Resources.Load("Clear_Skin") as Texture2D;
		texture2D_1.filterMode = FilterMode.Point;
		texture2D_1.Apply();
		UsersData.Subscribe(UsersData.EventType.SKIN_CHANGED, onChangeUserSkin);
	}

	public static void onChangeUserSkin(UsersData.EventData eventData_0)
	{
		UserTextureSkinData userTextureSkinData = null;
		if (UserController.UserController_0.UserData_0.dictionary_4.ContainsKey(eventData_0.string_0))
		{
			userTextureSkinData = UserController.UserController_0.UserData_0.dictionary_4[eventData_0.string_0];
		}
		if (userTextureSkinData != null)
		{
			changeUserSkin(userTextureSkinData);
		}
		else
		{
			removeUserSkin(eventData_0.string_0);
		}
	}

	private static void changeUserSkin(UserTextureSkinData userTextureSkinData_0)
	{
		int int_ = userTextureSkinData_0.int_0;
		foreach (KeyValuePair<TextureType, Dictionary<int, Texture2D>> item in dictionary_0)
		{
			item.Value.Remove(int_);
		}
		foreach (KeyValuePair<TextureType, Dictionary<int, UserTextureSkinData>> item2 in dictionary_1)
		{
			item2.Value.Remove(int_);
		}
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_);
		if (objectByKey == null || !objectByKey.Boolean_0 || (!objectByKey.Boolean_2 && !objectByKey.Boolean_1))
		{
			return;
		}
		Texture2D texture2D = SkinsController.TextureFromData(userTextureSkinData_0.byte_0);
		if (!(texture2D == null))
		{
			if (objectByKey.Boolean_1)
			{
				dictionary_0[TextureType.SKIN_PERS].Add(objectByKey.Int32_0, texture2D);
				dictionary_1[TextureType.SKIN_PERS].Add(objectByKey.Int32_0, userTextureSkinData_0);
			}
			else if (objectByKey.Boolean_2)
			{
				dictionary_0[TextureType.CAPE].Add(objectByKey.Int32_0, texture2D);
				dictionary_1[TextureType.CAPE].Add(objectByKey.Int32_0, userTextureSkinData_0);
			}
			SkinEditorController.Dispatch(SkinEditorController.EventType.DATA_UPDATE_OK, objectByKey.Int32_0);
			SkinEditController.SkinEditController_0.Dispatch(objectByKey.Int32_0, SkinEditController.SkinEditorEvent.DATA_UPDATE_OK);
		}
	}

	public static void removeUserSkin(string string_3)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		foreach (KeyValuePair<string, UserTextureSkinData> item in UserController.UserController_0.UserData_0.dictionary_4)
		{
			hashSet2.Add(item.Value.int_0);
		}
		foreach (KeyValuePair<TextureType, Dictionary<int, Texture2D>> item2 in dictionary_0)
		{
			foreach (KeyValuePair<int, Texture2D> item3 in item2.Value)
			{
				int key = item3.Key;
				WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(key);
				if (objectByKey == null)
				{
					hashSet.Add(key);
				}
				else if (objectByKey.Boolean_0 && !hashSet2.Contains(objectByKey.Int32_0))
				{
					hashSet.Add(key);
				}
			}
		}
		foreach (int item4 in hashSet)
		{
			foreach (KeyValuePair<TextureType, Dictionary<int, Texture2D>> item5 in dictionary_0)
			{
				item5.Value.Remove(item4);
			}
			foreach (KeyValuePair<TextureType, Dictionary<int, UserTextureSkinData>> item6 in dictionary_1)
			{
				item6.Value.Remove(item4);
			}
		}
	}

	public static Texture2D GetTextureById(int int_0)
	{
		foreach (KeyValuePair<TextureType, Dictionary<int, Texture2D>> item in dictionary_0)
		{
			if (item.Value.ContainsKey(int_0))
			{
				return item.Value[int_0];
			}
		}
		return null;
	}

	public static UserTextureSkinData GetUserDataById(int int_0)
	{
		foreach (KeyValuePair<TextureType, Dictionary<int, UserTextureSkinData>> item in dictionary_1)
		{
			if (item.Value.ContainsKey(int_0))
			{
				return item.Value[int_0];
			}
		}
		return null;
	}

	public static Texture2D GetTextureByIdAndType(int int_0, TextureType textureType_0)
	{
		if (dictionary_0.ContainsKey(textureType_0) && dictionary_0[textureType_0].ContainsKey(int_0))
		{
			return dictionary_0[textureType_0][int_0];
		}
		return null;
	}

	public static UserTextureSkinData GetUserDataByIdAndType(int int_0, TextureType textureType_0)
	{
		if (dictionary_1.ContainsKey(textureType_0) && dictionary_1[textureType_0].ContainsKey(int_0))
		{
			return dictionary_1[textureType_0][int_0];
		}
		return null;
	}

	public static bool HaveCustomWear(int int_0)
	{
		WearData objectByKey = wear.Find(x => x.Int32_0 == int_0);
		if (objectByKey != null && objectByKey.Boolean_0)
		{
			if (objectByKey.Boolean_1 && dictionary_0[TextureType.SKIN_PERS].ContainsKey(int_0) && dictionary_1[TextureType.SKIN_PERS].ContainsKey(int_0))
			{
				return true;
			}
			if (objectByKey.Boolean_2 && dictionary_0[TextureType.CAPE].ContainsKey(int_0) && dictionary_1[TextureType.CAPE].ContainsKey(int_0))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public static bool HaveOtherCustomWear(int int_0, int int_1)
	{
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null && objectByKey.Boolean_0)
		{
			if (objectByKey.Boolean_1 && dictionary_2[TextureType.SKIN_PERS].ContainsKey(int_1) && dictionary_3[TextureType.SKIN_PERS].ContainsKey(int_1) && dictionary_2[TextureType.SKIN_PERS][int_1].ContainsKey(int_0) && dictionary_3[TextureType.SKIN_PERS][int_1].ContainsKey(int_0))
			{
				return true;
			}
			if (objectByKey.Boolean_2 && dictionary_2[TextureType.CAPE].ContainsKey(int_1) && dictionary_3[TextureType.CAPE].ContainsKey(int_1) && dictionary_2[TextureType.CAPE][int_1].ContainsKey(int_0) && dictionary_3[TextureType.CAPE][int_1].ContainsKey(int_0))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public static void UpdateOtherUserSkins(Dictionary<string, UserTextureSkinData> dictionary_4, int int_0)
	{
		foreach (KeyValuePair<TextureType, Dictionary<int, Dictionary<int, Texture2D>>> item in dictionary_2)
		{
			item.Value.Remove(int_0);
		}
		foreach (KeyValuePair<TextureType, Dictionary<int, Dictionary<int, UserTextureSkinData>>> item2 in dictionary_3)
		{
			item2.Value.Remove(int_0);
		}
		Dictionary<int, UserTextureSkinData> dictionary = new Dictionary<int, UserTextureSkinData>();
		foreach (KeyValuePair<string, UserTextureSkinData> item3 in dictionary_4)
		{
			dictionary.Add(item3.Value.int_0, item3.Value);
		}
		foreach (WearData @object in WearStorage.Get.Storage.GetObjects())
		{
			if (!@object.Boolean_0 || !dictionary.ContainsKey(@object.Int32_0))
			{
				continue;
			}
			TextureType key = ((!@object.Boolean_1) ? TextureType.CAPE : TextureType.SKIN_PERS);
			UserTextureSkinData userTextureSkinData = dictionary[@object.Int32_0];
			Texture2D texture2D = SkinsController.TextureFromData(userTextureSkinData.byte_0);
			if ((bool)texture2D)
			{
				if (!dictionary_2[key].ContainsKey(int_0))
				{
					dictionary_2[key].Add(int_0, new Dictionary<int, Texture2D>());
				}
				dictionary_2[key][int_0].Add(@object.Int32_0, texture2D);
				if (!dictionary_3[key].ContainsKey(int_0))
				{
					dictionary_3[key].Add(int_0, new Dictionary<int, UserTextureSkinData>());
				}
				dictionary_3[key][int_0].Add(@object.Int32_0, userTextureSkinData);
			}
		}
	}

	public static Texture2D GetTextureByIdTypeUid(int int_0, TextureType textureType_0, int int_1)
	{
		if (dictionary_2.ContainsKey(textureType_0) && dictionary_2[textureType_0].ContainsKey(int_1) && dictionary_2[textureType_0][int_1].ContainsKey(int_0))
		{
			return dictionary_2[textureType_0][int_1][int_0];
		}
		return null;
	}

	public static UserTextureSkinData GetUserDataByIdTypeUid(int int_0, TextureType textureType_0, int int_1)
	{
		if (dictionary_3.ContainsKey(textureType_0) && dictionary_3[textureType_0].ContainsKey(int_1) && dictionary_3[textureType_0][int_1].ContainsKey(int_0))
		{
			return dictionary_3[textureType_0][int_1][int_0];
		}
		return null;
	}
}
