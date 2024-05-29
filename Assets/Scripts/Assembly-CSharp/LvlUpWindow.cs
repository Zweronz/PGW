using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.LevelUp)]
public class LvlUpWindow : BaseGameWindow
{
	public UILabel uilabel_0;

	public GameObject gameObject_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	private int int_0 = 1;

	private Action action_0;

	private Texture texture_0;

	[CompilerGenerated]
	private static LvlUpWindow lvlUpWindow_0;

	public static LvlUpWindow LvlUpWindow_0
	{
		[CompilerGenerated]
		get
		{
			return lvlUpWindow_0;
		}
		[CompilerGenerated]
		private set
		{
			lvlUpWindow_0 = value;
		}
	}

	public static void Show(LvlUpWindowParams lvlUpWindowParams_0)
	{
		LvlUpWindow lvlUpWindow = LvlUpWindow_0;
		if (!(lvlUpWindow != null))
		{
			lvlUpWindow = BaseWindow.Load("LvlUpWindow") as LvlUpWindow;
			lvlUpWindow.Parameters_0.type_0 = WindowQueue.Type.New;
			lvlUpWindow.Parameters_0.bool_5 = false;
			lvlUpWindow.Parameters_0.bool_0 = false;
			lvlUpWindow.Parameters_0.bool_6 = false;
			lvlUpWindow.InternalShow(lvlUpWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Init();
		base.OnShow();
		NickLabelStack.nickLabelStack_0.SetCameraActive(false);
	}

	public override void OnHide()
	{
		base.OnHide();
		if (action_0 != null)
		{
			action_0();
		}
		CameraSceneController.SetState(CameraSceneController.States.None);
		NickLabelStack.nickLabelStack_0.SetCameraActive(true);
		LvlUpWindow_0 = null;
	}

	public void OnOpenShop()
	{
		NGUITools.SetActive(base.gameObject, false);
		ShopWindowParams shopWindowParams = new ShopWindowParams();
		shopWindowParams.action_0 = HideLvlUp;
		shopWindowParams.sourceBuyType_0 = ShopArtikulController.SourceBuyType.TYPE_LVLUP_WND;
		ShopWindow.Show(shopWindowParams);
	}

	private void Init()
	{
		LvlUpWindowParams lvlUpWindowParams = base.WindowShowParameters_0 as LvlUpWindowParams;
		if (lvlUpWindowParams != null)
		{
			int_0 = lvlUpWindowParams.int_0;
			action_0 = lvlUpWindowParams.action_0;
			texture_0 = lvlUpWindowParams.texture_0;
		}
		SetLevel();
		SetReward();
		SetShop();
		CameraSceneController.SetState(CameraSceneController.States.LevelUp);
		if (lvlUpWindowParams.texture_0 != null)
		{
			Transform child = base.transform.GetChild(0);
			child.gameObject.GetComponent<MeshRenderer>().material.mainTexture = lvlUpWindowParams.texture_0;
			Camera main = Camera.main;
			float num = main.nearClipPlane + 10f;
			child.position = main.transform.position + main.transform.forward * num;
			Vector3 position = child.position;
			position.y = 0f;
			position.z *= 2.5f;
			child.position = position;
			child.Rotate(90f, -180f, 0f);
			float num2 = Mathf.Tan(main.fov * ((float)Math.PI / 180f) * 0.5f) * num * 2f / 10f;
			child.localScale = new Vector3(num2 * main.aspect, 1f, num2) * 100f;
		}
		else
		{
			NGUITools.SetActive(base.transform.GetChild(0).gameObject, false);
		}
	}

	private void SetLevel()
	{
		uilabel_0.String_0 = int_0.ToString();
	}

	private void SetReward()
	{
		LevelData levelData = LevelStorage.Get.GetlLevelData(int_0);
		uilabel_1.String_0 = levelData.Int32_2.ToString();
		uilabel_2.String_0 = LevelStorage.Get.GetHpDiffPrevLevel(int_0).ToString("0");
	}

	private void SetShop()
	{
		getNewLevelArtikuls();
		NGUITools.SetActive(gameObject_0, true);
	}

	private List<int> getNewLevelArtikuls()
	{
		List<int> list = new List<int>();
		List<ShopArtikulData> list2 = ShopArtikulStorage.Get.Storage.Search(2, int_0);
		if (list2 != null && list2.Count > 0)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list2.Count; i++)
			{
				hashSet.Add(list2[i].Int32_1);
			}
			foreach (int item in hashSet)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public void HideLvlUp()
	{
		LevelData levelData = LevelStorage.Get.GetlLevelData(int_0);
		List<BonusItemData> list = new List<BonusItemData>();
		BonusController.BonusController_0.GetAllItemsFromBonus(levelData.int_0, list);
		int num = 0;
		foreach (BonusItemData item in list)
		{
			if (item.bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_ARTICUL)
			{
				num = item.int_1;
				break;
			}
		}
		if (num > 0)
		{
			RebuyArticulWindow.Show(new RebuyArticulWindowParams(num, RebuyArticulWindowParams.RebuyWndType.NEW_ITEM_WND, base.Hide));
		}
		else
		{
			Hide();
		}
	}
}
