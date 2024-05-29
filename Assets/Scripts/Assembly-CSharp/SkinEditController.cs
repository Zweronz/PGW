using System.Collections.Generic;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.network;

public class SkinEditController : BaseEvent<int>
{
	public enum SkinEditorEvent
	{
		COLOR_CHANGED = 0,
		TOOL_CHANGED = 1,
		GRID_CHANGED = 2,
		HISTORY_CHANGED = 3,
		DROPPER_USED = 4,
		SAVE_OK = 5,
		SAVE_ERROR = 6,
		DATA_UPDATE_OK = 7
	}

	public enum SkinEditorState
	{
		STATE_DEFAULT = 0,
		STATE_EDITED = 1
	}

	private const string string_0 = "skin_editor_show_grid";

	private static SkinEditController skinEditController_0;

	public string string_1;

	public int int_0;

	public Color color_0;

	public SkinEditorToolItem.ToolType toolType_0;

	public SkinEditorPartItem.PartItemType partItemType_0;

	public Texture2D texture2D_0;

	public SkinEditorState skinEditorState_0;

	public bool bool_0;

	private Dictionary<SkinEditorPartItem.PartItemType, List<Texture2D>> dictionary_1 = new Dictionary<SkinEditorPartItem.PartItemType, List<Texture2D>>();

	private Dictionary<SkinEditorPartItem.PartItemType, int> dictionary_2 = new Dictionary<SkinEditorPartItem.PartItemType, int>();

	private int int_1;

	private bool bool_1;

	private bool bool_2 = true;

	public static SkinEditController SkinEditController_0
	{
		get
		{
			if (skinEditController_0 == null)
			{
				skinEditController_0 = new SkinEditController();
			}
			return skinEditController_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_2;
		}
		set
		{
			bool_2 = value;
			SharedSettings.SharedSettings_0.SetValue("skin_editor_show_grid", bool_2 ? 1 : 0, true);
		}
	}

	private SkinEditController()
	{
		Init();
	}

	private void Init()
	{
		bool_2 = SharedSettings.SharedSettings_0.GetValue("skin_editor_show_grid", 1) == 1;
		InitHistory();
	}

	public void EditCustomWear(int int_2)
	{
		WearData wear = WearController.WearController_0.GetWear(int_2);
		if (wear != null && wear.Boolean_0)
		{
			if (wear.Boolean_1)
			{
				ShowSelectSkin(int_2);
			}
			else if (wear.Boolean_2)
			{
				SkinsController.editCustomSkin(int_2);
			}
		}
	}

	public void ShowSelectSkin(int int_2 = 0)
	{
		if (texture2D_0 == null)
		{
			int_0 = int_2;
		}
		if (ShopWindow.ShopWindow_0 != null)
		{
			((ShopWindowParams)ShopWindow.ShopWindow_0.WindowShowParameters_0).action_0 = null;
			ShopWindow.ShopWindow_0.Hide();
		}
		SkinSelectWindow.Show();
	}

	public void EditPart(string string_2)
	{
		string_1 = string_2;
		if (SkinSelectWindow.SkinSelectWindow_0 != null)
		{
			SkinSelectWindow.SkinSelectWindow_0.Hide();
		}
		SkinEditorWindow.Show();
	}

	public void Reset()
	{
		int_0 = 0;
		texture2D_0 = null;
		bool_0 = false;
	}

	public bool CanEdit()
	{
		if (SkinSelectWindow.SkinSelectWindow_0 != null)
		{
			int int32_ = SkinSelectWindow.SkinSelectWindow_0.Int32_0;
			return int32_ == 0 || UserController.UserController_0.HasUserArtikul(int32_);
		}
		return false;
	}

	public bool IsEditing()
	{
		return SkinEditorWindow.SkinEditorWindow_0 != null;
	}

	private void InitHistory()
	{
		for (SkinEditorPartItem.PartItemType partItemType = SkinEditorPartItem.PartItemType.LEFT; partItemType <= SkinEditorPartItem.PartItemType.DOWN; partItemType++)
		{
			dictionary_1.Add(partItemType, new List<Texture2D>());
		}
		for (SkinEditorPartItem.PartItemType partItemType2 = SkinEditorPartItem.PartItemType.LEFT; partItemType2 <= SkinEditorPartItem.PartItemType.DOWN; partItemType2++)
		{
			dictionary_2.Add(partItemType2, -1);
		}
	}

	public void ClearHistory()
	{
		foreach (KeyValuePair<SkinEditorPartItem.PartItemType, List<Texture2D>> item in dictionary_1)
		{
			item.Value.Clear();
		}
		dictionary_2.Clear();
		for (SkinEditorPartItem.PartItemType partItemType = SkinEditorPartItem.PartItemType.LEFT; partItemType <= SkinEditorPartItem.PartItemType.DOWN; partItemType++)
		{
			dictionary_2.Add(partItemType, -1);
		}
		bool_1 = false;
	}

	public void ClearPartHistory(Texture2D texture2D_1 = null)
	{
		Texture2D texture_ = Utility.CopyTexture((!(texture2D_1 == null)) ? texture2D_1 : dictionary_1[partItemType_0][0]);
		dictionary_1[partItemType_0].Clear();
		dictionary_2[partItemType_0] = -1;
		StartHistoryFrom(texture_);
		bool_1 = true;
		CheckState();
	}

	public void StartHistoryFrom(Texture texture_0)
	{
		if (dictionary_1[partItemType_0].Count == 0)
		{
			AddToHistory(Utility.CopyTexture(texture_0));
		}
	}

	public void AddToHistory(Texture2D texture2D_1)
	{
		int num = dictionary_2[partItemType_0];
		int count = dictionary_1[partItemType_0].Count;
		if (num == count - 1)
		{
			dictionary_1[partItemType_0].Add(texture2D_1);
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary;
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary2 = (dictionary = dictionary_2);
			SkinEditorPartItem.PartItemType key;
			SkinEditorPartItem.PartItemType key2 = (key = partItemType_0);
			int num2 = dictionary[key];
			dictionary2[key2] = num2 + 1;
		}
		else if (num < count - 1)
		{
			dictionary_1[partItemType_0][num + 1] = texture2D_1;
			dictionary_1[partItemType_0] = dictionary_1[partItemType_0].GetRange(0, num + 2);
			dictionary_2[partItemType_0] = num + 1;
		}
		CheckState();
		Dispatch(0, SkinEditorEvent.HISTORY_CHANGED);
	}

	public Texture2D GetPrevFromHistory()
	{
		int num = dictionary_2[partItemType_0];
		int count = dictionary_1[partItemType_0].Count;
		if (num > 0 && count > num - 1)
		{
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary;
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary2 = (dictionary = dictionary_2);
			SkinEditorPartItem.PartItemType key;
			SkinEditorPartItem.PartItemType key2 = (key = partItemType_0);
			int num2 = dictionary[key];
			dictionary2[key2] = num2 - 1;
			Dispatch(0, SkinEditorEvent.HISTORY_CHANGED);
			return dictionary_1[partItemType_0][num - 1];
		}
		return null;
	}

	public Texture2D GetNextFromHistory()
	{
		int num = dictionary_2[partItemType_0];
		int count = dictionary_1[partItemType_0].Count;
		if (count > 0 && count > num + 1)
		{
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary;
			Dictionary<SkinEditorPartItem.PartItemType, int> dictionary2 = (dictionary = dictionary_2);
			SkinEditorPartItem.PartItemType key;
			SkinEditorPartItem.PartItemType key2 = (key = partItemType_0);
			int num2 = dictionary[key];
			dictionary2[key2] = num2 + 1;
			Dispatch(0, SkinEditorEvent.HISTORY_CHANGED);
			return dictionary_1[partItemType_0][num + 1];
		}
		return null;
	}

	public bool HasHistory()
	{
		return dictionary_1[partItemType_0].Count > 1;
	}

	public bool HasPrevHistory()
	{
		int num = dictionary_2[partItemType_0];
		int count = dictionary_1[partItemType_0].Count;
		return count > 0 && num > 0;
	}

	public bool HasNextHistory()
	{
		int num = dictionary_2[partItemType_0];
		int count = dictionary_1[partItemType_0].Count;
		return count > 0 && num < count - 1;
	}

	private void CheckState()
	{
		foreach (KeyValuePair<SkinEditorPartItem.PartItemType, List<Texture2D>> item in dictionary_1)
		{
			if (item.Value.Count > 1)
			{
				skinEditorState_0 = SkinEditorState.STATE_EDITED;
				return;
			}
		}
		skinEditorState_0 = (bool_1 ? SkinEditorState.STATE_EDITED : SkinEditorState.STATE_DEFAULT);
	}

	public void BuildSkin(SkinEditorPartScheme[] skinEditorPartScheme_0)
	{
		Texture2D texture2D = new Texture2D(texture2D_0.width, texture2D_0.height, TextureFormat.ARGB32, false);
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				texture2D.SetPixel(i, j, Color.clear);
			}
		}
		foreach (SkinEditorPartScheme skinEditorPartScheme in skinEditorPartScheme_0)
		{
			SkinEditorPartItem[] items = skinEditorPartScheme.items;
			foreach (SkinEditorPartItem skinEditorPartItem in items)
			{
				texture2D.SetPixels(Mathf.RoundToInt(skinEditorPartItem.rect.x), Mathf.RoundToInt(skinEditorPartItem.rect.y), Mathf.RoundToInt(skinEditorPartItem.rect.width), Mathf.RoundToInt(skinEditorPartItem.rect.height), ((Texture2D)skinEditorPartItem.itemTexture.Texture_0).GetPixels());
			}
		}
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		texture2D_0 = texture2D;
		bool_0 = true;
	}

	public void Save(string string_2)
	{
		UserTextureSkinData userTextureSkinData = SkinsController.AddUserSkin(string_2, texture2D_0, int_0);
		if (userTextureSkinData != null)
		{
			int_1 = 0;
			Subscribe(UserSave, SkinEditorEvent.SAVE_OK);
			Subscribe(UserSaveCheck, SkinEditorEvent.DATA_UPDATE_OK);
			Subscribe(UserSkinSaveError, SkinEditorEvent.SAVE_ERROR);
			UpdateUserSkinNetworkCommand updateUserSkinNetworkCommand = new UpdateUserSkinNetworkCommand();
			updateUserSkinNetworkCommand.userTextureSkinData_0 = userTextureSkinData;
			updateUserSkinNetworkCommand.bool_0 = false;
			AbstractNetworkCommand.Send(updateUserSkinNetworkCommand);
		}
	}

	private void UserSave(int int_2)
	{
		int_1++;
		if (int_1 == 2)
		{
			Save();
		}
	}

	private void UserSaveCheck(int int_2)
	{
		if (int_0 != int_2)
		{
			ClearWaitAnswer();
			return;
		}
		int_1++;
		if (int_1 == 2)
		{
			Save();
		}
	}

	private void UserSkinSaveError(int int_2)
	{
		ClearWaitAnswer();
	}

	private void ClearWaitAnswer()
	{
		int_1 = 0;
		Unsubscribe(UserSave, SkinEditorEvent.SAVE_OK);
		Unsubscribe(UserSaveCheck, SkinEditorEvent.DATA_UPDATE_OK);
		Unsubscribe(UserSkinSaveError, SkinEditorEvent.SAVE_ERROR);
	}

	private void Save()
	{
		int int32_ = int_0;
		ClearWaitAnswer();
		WearData wear = WearController.WearController_0.GetWear(int32_);
		if (wear != null && wear.Boolean_0)
		{
			if (wear.Boolean_1)
			{
				SkinsController.Int32_0 = int32_;
			}
			else if (wear.Boolean_2)
			{
				WearController.WearController_0.EquipWear(int32_);
			}
			bool_0 = false;
		}
	}
}
