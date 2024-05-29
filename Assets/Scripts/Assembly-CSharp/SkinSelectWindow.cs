using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

public class SkinSelectWindow : BaseGameWindow
{
	private static SkinSelectWindow skinSelectWindow_0;

	public UIScrollView uiscrollView_0;

	public UITable uitable_0;

	public SkinSelectItem skinSelectItem_0;

	public SkinEditorPers skinEditorPers_0;

	public UIButton uibutton_0;

	public Light light_0;

	private int int_0;

	private SkinSelectItem skinSelectItem_1;

	private bool bool_1;

	private int int_1;

	[CompilerGenerated]
	private static Action action_0;

	[CompilerGenerated]
	private static Predicate<ShopArtikulData> predicate_0;

	[CompilerGenerated]
	private static Comparison<ShopArtikulData> comparison_0;

	public static SkinSelectWindow SkinSelectWindow_0
	{
		get
		{
			return skinSelectWindow_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_1;
		}
	}

	public static void Show(SkinSelectWindowParams skinSelectWindowParams_0 = null)
	{
		if (!(skinSelectWindow_0 != null))
		{
			skinSelectWindow_0 = BaseWindow.Load("SkinSelectWindow") as SkinSelectWindow;
			skinSelectWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			skinSelectWindow_0.Parameters_0.bool_5 = false;
			skinSelectWindow_0.Parameters_0.bool_0 = false;
			skinSelectWindow_0.Parameters_0.bool_6 = false;
			skinSelectWindow_0.InternalShow(skinSelectWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
		SkinEditController.SkinEditController_0.Subscribe(OnDataUpdateSuccess, SkinEditController.SkinEditorEvent.DATA_UPDATE_OK);
		UsersData.Subscribe(UsersData.EventType.ARTIKULS_CHANGED, OnUserSkinsChanged);
		AddInputKey(KeyCode.Escape, delegate
		{
			if (base.Boolean_0)
			{
				Exit();
			}
		});
	}

	public override void OnHide()
	{
		base.OnHide();
		skinSelectWindow_0 = null;
		SkinEditController.SkinEditController_0.Unsubscribe(OnDataUpdateSuccess, SkinEditController.SkinEditorEvent.DATA_UPDATE_OK);
		UsersData.Unsubscribe(UsersData.EventType.ARTIKULS_CHANGED, OnUserSkinsChanged);
		if (bool_1)
		{
			ShopWindowParams shopWindowParams = new ShopWindowParams();
			shopWindowParams.int_0 = SkinEditController.SkinEditController_0.int_0;
			shopWindowParams.action_0 = delegate
			{
				Lobby.Lobby_0.Show();
			};
			shopWindowParams.openStyle_0 = ShopWindow.OpenStyle.ANIMATED;
			ShopWindow.Show(shopWindowParams);
			SkinEditController.SkinEditController_0.Reset();
		}
	}

	public void Exit()
	{
		if (SkinEditController.SkinEditController_0.skinEditorState_0 == SkinEditController.SkinEditorState.STATE_DEFAULT)
		{
			OnExitCancel();
		}
		else
		{
			MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("window.skin_editor.exit_select.confirm"), OnExitOk, LocalizationStorage.Get.Term("ui.common.btn.yes"), KeyCode.Return, OnExitCancel, LocalizationStorage.Get.Term("ui.common.btn.no")));
		}
	}

	private void OnExitOk()
	{
		if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
		{
			MessageWindowConfirm.MessageWindowConfirm_0.Hide();
		}
		OnSaveButtonClick();
	}

	private void OnExitCancel()
	{
		bool_1 = true;
		SkinEditController.SkinEditController_0.skinEditorState_0 = SkinEditController.SkinEditorState.STATE_DEFAULT;
		if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
		{
			MessageWindowConfirm.MessageWindowConfirm_0.Hide();
		}
		Hide();
	}

	public void Init()
	{
		bool_1 = false;
		base.transform.localPosition = new Vector3(0f, 0f, 300f);
		NGUITools.SetActive(skinSelectItem_0.gameObject, false);
		uitable_0.onReposition_0 = delegate
		{
			uiscrollView_0.UpdateScrollbars(true);
		};
		SetSkinsContent();
		RepositionContent();
		SelectStartItem();
		UpdateSaveButton();
	}

	private void SelectStartItem()
	{
		bool flag = SkinsController.CanWearCustomSkin(SkinEditController.SkinEditController_0.int_0);
		SelectItem((!flag) ? (-5) : SkinEditController.SkinEditController_0.int_0);
	}

	private void SetSkinsContent()
	{
		List<ShopArtikulData> validShopListBySlot = ShopArtikulController.ShopArtikulController_0.GetValidShopListBySlot(SlotType.SLOT_WEAR_SKIN);
		FilterSkins(validShopListBySlot);
		InflateItems(validShopListBySlot);
	}

	private void FilterSkins(List<ShopArtikulData> list_0)
	{
		list_0.RemoveAll(delegate(ShopArtikulData shopArtikulData_0)
		{
			WearData wear = WearController.WearController_0.GetWear(shopArtikulData_0.Int32_1);
			return wear.Boolean_0 && (!UserController.UserController_0.HasUserArtikul(shopArtikulData_0.Int32_1) || !SkinsController.CanWearCustomSkin(shopArtikulData_0.Int32_1));
		});
		Sort(list_0);
		ShopArtikulData shopArtikulData = new ShopArtikulData();
		shopArtikulData.Int32_1 = -5;
		list_0.Insert(0, shopArtikulData);
	}

	private void SetCapesContent()
	{
	}

	private void Sort(List<ShopArtikulData> list_0)
	{
		list_0.Sort(delegate(ShopArtikulData shopArtikulData_0, ShopArtikulData shopArtikulData_1)
		{
			bool value = UserController.UserController_0.HasUserArtikul(shopArtikulData_0.Int32_1);
			return UserController.UserController_0.HasUserArtikul(shopArtikulData_1.Int32_1).CompareTo(value);
		});
	}

	private void InflateItem(ShopArtikulData shopArtikulData_0)
	{
		GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, skinSelectItem_0.gameObject);
		gameObject.name = string.Format("{0:0000}", int_0++);
		SkinSelectItem component = gameObject.GetComponent<SkinSelectItem>();
		component.SetCallbacks(OnSelectedShopItem);
		component.SetData(shopArtikulData_0);
		NGUITools.SetActive(gameObject, true);
		component.TryLoadModel();
	}

	private void InflateItems(List<ShopArtikulData> list_0)
	{
		int_0 = 0;
		foreach (ShopArtikulData item in list_0)
		{
			InflateItem(item);
		}
	}

	private void RepositionContent()
	{
		uitable_0.Reposition();
		uiscrollView_0.ResetPosition();
	}

	private void ClearItems()
	{
		skinSelectItem_1 = null;
		List<Transform> list_ = uitable_0.List_0;
		foreach (Transform item in list_)
		{
			if (!(item == null))
			{
				item.parent = null;
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}

	private void OnSelectedShopItem(SkinSelectItem skinSelectItem_2)
	{
		if (skinSelectItem_1 != null)
		{
			skinSelectItem_1.SetSelected(false);
		}
		if (skinSelectItem_1 != null && skinSelectItem_1.Equals(skinSelectItem_2))
		{
			skinSelectItem_1 = null;
		}
		else
		{
			skinSelectItem_1 = skinSelectItem_2;
		}
		int_1 = ((!(skinSelectItem_1 == null) && skinSelectItem_1.ShopArtikulData_0.Int32_1 != -5) ? skinSelectItem_1.ArtikulData_0.Int32_0 : 0);
		UpdatePreviewSkin();
	}

	private void UpdatePreviewSkin()
	{
		Texture texture = ((int_1 <= 0) ? LocalSkinTextureData.Texture2D_1 : SkinsController.GetSkinTexture(int_1));
		if ((SkinEditController.SkinEditController_0.skinEditorState_0 == SkinEditController.SkinEditorState.STATE_EDITED || SkinEditController.SkinEditController_0.bool_0) && SkinEditController.SkinEditController_0.texture2D_0 != null)
		{
			texture = SkinEditController.SkinEditController_0.texture2D_0;
		}
		Utility.SetTextureRecursiveFrom(skinEditorPers_0.previewPers.gameObject, texture);
		SkinEditController.SkinEditController_0.texture2D_0 = (Texture2D)texture;
	}

	public void SelectItem(int int_2)
	{
		int num = 0;
		if (int_2 != -5)
		{
			num = uitable_0.List_0.FindLastIndex(delegate(Transform transform_0)
			{
				SkinSelectItem component2 = transform_0.gameObject.GetComponent<SkinSelectItem>();
				return component2.ArtikulData_0 != null && component2.ArtikulData_0.Int32_0 == int_2;
			});
		}
		if (num > -1)
		{
			SkinSelectItem component = uitable_0.List_0[num].gameObject.GetComponent<SkinSelectItem>();
			component.ClickProcess();
			float num2 = (float)component.GetComponent<UIWidget>().Int32_1 + uitable_0.vector2_0.y;
			Vector3 vector3_ = new Vector3(0f, (float)(num / uitable_0.int_0) * num2, 0f);
			uiscrollView_0.MoveRelative(vector3_);
		}
	}

	private void OnUserSkinsChanged(UsersData.EventData eventData_0)
	{
		if (skinSelectItem_1 != null)
		{
			skinSelectItem_1.UpdateItem();
		}
	}

	private void UpdateSaveButton()
	{
		uibutton_0.Boolean_0 = SkinEditController.SkinEditController_0.skinEditorState_0 == SkinEditController.SkinEditorState.STATE_EDITED;
	}

	public void OnSaveButtonClick()
	{
		if (SkinEditController.SkinEditController_0.skinEditorState_0 != 0 && !(SkinEditController.SkinEditController_0.texture2D_0 == null))
		{
			SkinSaveWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("window.skin_editor.skin_save.set_name")));
		}
	}

	private void OnDataUpdateSuccess(int int_2)
	{
		SkinEditController.SkinEditController_0.skinEditorState_0 = SkinEditController.SkinEditorState.STATE_DEFAULT;
		ClearItems();
		SetSkinsContent();
		RepositionContent();
		SelectStartItem();
		UpdateSaveButton();
	}
}
