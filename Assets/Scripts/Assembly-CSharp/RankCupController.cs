using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

public class RankCupController : MonoBehaviour
{
	private static readonly string string_0 = "SHOW_CUP";

	private static readonly string string_1 = "Prefabs/Ranks/RankCupPlace";

	private static RankCupController rankCupController_0;

	public Transform placeholder;

	public GameObject rankCupArrowsPrefab;

	private int int_0;

	private int int_1;

	private bool bool_0 = true;

	private RankCupArrows rankCupArrows_0;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private static Comparison<ArtikulData> comparison_0;

	public static RankCupController RankCupController_0
	{
		get
		{
			return rankCupController_0;
		}
	}

	public static bool Boolean_0
	{
		get
		{
			if (RankCupController_0 != null && RankCupController_0.Int32_0 != UserController.UserController_0.UserData_0.user_0.int_0)
			{
				return true;
			}
			return SharedSettings.SharedSettings_0.GetValue(string_0, true);
		}
		set
		{
			SharedSettings.SharedSettings_0.SetValue(string_0, value, true);
			if (RankCupController_0 != null)
			{
				RankCupController_0.UpdateUserCups();
			}
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	private bool Boolean_1
	{
		get
		{
			if (Int32_0 != UserController.UserController_0.UserData_0.user_0.int_0 || int_0 == 0)
			{
				return false;
			}
			if (WindowController.WindowController_0.Int32_0 == 0)
			{
				return true;
			}
			if (RankCupChangeWindow.Boolean_1)
			{
				return false;
			}
			if (ProfileWindow.Boolean_1)
			{
				return true;
			}
			return false;
		}
	}

	private int Int32_1
	{
		get
		{
			List<UserArtikul> anyUserArtikulsBySlotType = UserController.UserController_0.GetAnyUserArtikulsBySlotType(SlotType.SLOT_RANK_CUP, Int32_0);
			return (anyUserArtikulsBySlotType != null && anyUserArtikulsBySlotType.Count != 0) ? anyUserArtikulsBySlotType.Count : 0;
		}
	}

	public static void Show(GameObject gameObject_0, int int_3 = 0)
	{
		if (RankCupController_0 != null)
		{
			return;
		}
		GameObject gameObject = Resources.Load<GameObject>(string_1);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = NGUITools.AddChild(gameObject_0, gameObject);
			gameObject2.transform.localScale = new Vector3(46f, 46f, 46f);
			gameObject2.transform.localPosition = new Vector3(157f, -152f, 424f);
			gameObject2.transform.Rotate(0f, 180f, 0f);
			CursorChanger component = gameObject2.GetComponent<CursorChanger>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
			}
			RankCupController_0.Int32_0 = int_3;
		}
	}

	private void Awake()
	{
		rankCupController_0 = this;
	}

	private void OnDestroy()
	{
		rankCupController_0 = null;
		RankController.RankController_0.Unsubscribe(OnProfileWindowOpened, RankController.EventType.PROFILE_OPENED);
		RankController.RankController_0.Unsubscribe(OnProfileWindowClosed, RankController.EventType.PROFILE_CLOSED);
		UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, OnUserSlotChange);
	}

	private void Start()
	{
		Init();
	}

	private void OnMouseDown()
	{
		if (Boolean_1)
		{
			OnMouseExit();
			RankCupChangeWindow.Show();
		}
	}

	private void OnMouseEnter()
	{
		if (Boolean_1)
		{
			CursorPGW.CursorPGW_0.Func_0 = () => base.gameObject;
		}
	}

	private void OnMouseExit()
	{
		if (Boolean_1)
		{
			CursorPGW.CursorPGW_0.Func_0 = null;
		}
	}

	public void InitRankCupArrows(GameObject gameObject_0)
	{
		if (!(rankCupController_0 == null) && bool_0)
		{
			GameObject gameObject = NGUITools.AddChild(gameObject_0, rankCupArrowsPrefab);
			rankCupArrows_0 = gameObject.GetComponent<RankCupArrows>();
			rankCupArrows_0.Init(placeholder);
			rankCupArrows_0.SetEnableButtons(Int32_1 > 1);
			rankCupArrows_0.SetDescription(int_0);
		}
	}

	public void NextCup()
	{
		List<ArtikulData> listUserCups = GetListUserCups(out int_1);
		if (listUserCups.Count != 1 && int_1 != -1)
		{
			int_1 = (int_1 + 1) % listUserCups.Count;
			ClearCup();
			UserController.UserController_0.EquipArtikul(listUserCups[int_1].Int32_0);
		}
	}

	public void PrevCup()
	{
		List<ArtikulData> listUserCups = GetListUserCups(out int_1);
		if (listUserCups.Count != 1 && int_1 != -1)
		{
			int num = int_1 - 1;
			int_1 = ((num >= 0) ? num : (listUserCups.Count + num));
			ClearCup();
			UserController.UserController_0.EquipArtikul(listUserCups[int_1].Int32_0);
		}
	}

	private void Init()
	{
		int_1 = -1;
		Int32_0 = ((Int32_0 == 0) ? UserController.UserController_0.UserData_0.user_0.int_0 : Int32_0);
		RankController.RankController_0.Subscribe(OnProfileWindowOpened, RankController.EventType.PROFILE_OPENED);
		RankController.RankController_0.Subscribe(OnProfileWindowClosed, RankController.EventType.PROFILE_CLOSED);
		UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, OnUserSlotChange);
		UpdateUserCups();
	}

	private void OnProfileWindowOpened(RankController.EventData eventData_0)
	{
		if (Int32_0 != eventData_0.int_0)
		{
			Int32_0 = eventData_0.int_0;
			UpdateUserCups();
		}
	}

	private void OnProfileWindowClosed(RankController.EventData eventData_0)
	{
		Int32_0 = UserController.UserController_0.UserData_0.user_0.int_0;
		UpdateUserCups();
	}

	private void OnUserSlotChange(UsersData.EventData eventData_0)
	{
		SlotType slotType = (SlotType)eventData_0.int_0;
		if (slotType == SlotType.SLOT_RANK_CUP && Int32_0 == UserController.UserController_0.UserData_0.user_0.int_0)
		{
			UpdateUserCups();
		}
	}

	private void UpdateUserCups()
	{
		int num = 0;
		if (Boolean_0)
		{
			num = UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_RANK_CUP, Int32_0);
		}
		if (num > 0 && int_0 != num)
		{
			int_0 = num;
			ClearCup();
			AddCup();
		}
		else if (num == 0)
		{
			int_0 = 0;
			ClearCup();
		}
	}

	private List<ArtikulData> GetListUserCups(out int int_3)
	{
		int_3 = -1;
		List<UserArtikul> anyUserArtikulsBySlotType = UserController.UserController_0.GetAnyUserArtikulsBySlotType(SlotType.SLOT_RANK_CUP, Int32_0);
		List<ArtikulData> list = new List<ArtikulData>();
		for (int i = 0; i < anyUserArtikulsBySlotType.Count; i++)
		{
			list.Add(anyUserArtikulsBySlotType[i].ArtikulData_0);
		}
		if (list.Count > 0)
		{
			list.Sort((ArtikulData artikulData_0, ArtikulData artikulData_1) => artikulData_0.Int32_4.CompareTo(artikulData_1.Int32_4));
			if (int_0 != 0)
			{
				int_3 = list.FindIndex((ArtikulData artikulData_0) => artikulData_0.Int32_0 == int_0);
			}
		}
		return list;
	}

	private void AddCup()
	{
		GameObject rankCupGameObject = RankController.RankController_0.GetRankCupGameObject(int_0);
		if (placeholder != null && rankCupGameObject != null)
		{
			GameObject gameObject = NGUITools.AddChild(placeholder.gameObject, rankCupGameObject);
			gameObject.name = rankCupGameObject.name;
		}
		if (rankCupArrows_0 != null)
		{
			rankCupArrows_0.SetEnableButtons(Int32_1 > 1);
			rankCupArrows_0.SetDescription(int_0);
		}
	}

	private void ClearCup()
	{
		if (placeholder.childCount > 0)
		{
			UnityEngine.Object.Destroy(placeholder.GetChild(0).gameObject);
		}
		if ((bool)rankCupArrows_0)
		{
			rankCupArrows_0.SetEnableButtons(false);
			rankCupArrows_0.SetDescription();
		}
	}
}
