using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using engine.unity;

public class SkinEditorWindow : BaseGameWindow
{
	private static SkinEditorWindow skinEditorWindow_0 = null;

	public static readonly Dictionary<string, SkinEditorPartScheme.PartType> dictionary_1 = new Dictionary<string, SkinEditorPartScheme.PartType>
	{
		{
			"Head",
			SkinEditorPartScheme.PartType.HEAD
		},
		{
			"Body",
			SkinEditorPartScheme.PartType.BODY
		},
		{
			"Arm_left",
			SkinEditorPartScheme.PartType.ARM
		},
		{
			"Arm_right",
			SkinEditorPartScheme.PartType.ARM
		},
		{
			"Foot_left",
			SkinEditorPartScheme.PartType.FOOT
		},
		{
			"Foot_right",
			SkinEditorPartScheme.PartType.FOOT
		}
	};

	public SkinEditorPartScheme[] skinEditorPartScheme_0;

	public SkinEditorCanvas skinEditorCanvas_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIButton uibutton_2;

	private SkinEditorPartItem skinEditorPartItem_0;

	public static SkinEditorWindow SkinEditorWindow_0
	{
		get
		{
			return skinEditorWindow_0;
		}
	}

	public static void Show(SkinEditorWindowParams skinEditorWindowParams_0 = null)
	{
		if (!(skinEditorWindow_0 != null))
		{
			skinEditorWindow_0 = BaseWindow.Load("SkinEditorWindow") as SkinEditorWindow;
			skinEditorWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			skinEditorWindow_0.Parameters_0.bool_5 = false;
			skinEditorWindow_0.Parameters_0.bool_0 = false;
			skinEditorWindow_0.Parameters_0.bool_6 = false;
			skinEditorWindow_0.InternalShow(skinEditorWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
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
		skinEditorWindow_0 = null;
		SkinEditController.SkinEditController_0.Unsubscribe(UpdateButtons, SkinEditController.SkinEditorEvent.HISTORY_CHANGED);
		SkinEditController.SkinEditController_0.ClearHistory();
	}

	public void Exit()
	{
		if (SkinEditController.SkinEditController_0.skinEditorState_0 == SkinEditController.SkinEditorState.STATE_DEFAULT)
		{
			OnExitCancel();
		}
		else
		{
			MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("window.skin_editor.exit_editor.confirm"), OnExitOk, LocalizationStorage.Get.Term("ui.common.btn.yes"), KeyCode.Return, OnExitCancel, LocalizationStorage.Get.Term("ui.common.btn.no")));
		}
	}

	private void OnExitOk()
	{
		SkinEditController.SkinEditController_0.BuildSkin(skinEditorPartScheme_0);
		Hide();
		SkinEditController.SkinEditController_0.ShowSelectSkin();
	}

	private void OnExitCancel()
	{
		SkinEditController.SkinEditController_0.skinEditorState_0 = SkinEditController.SkinEditorState.STATE_DEFAULT;
		Hide();
		SkinEditController.SkinEditController_0.ShowSelectSkin();
	}

	public void Init()
	{
		InitScheme();
		SkinEditController.SkinEditController_0.toolType_0 = SkinEditorToolItem.ToolType.PENCIL;
		SkinEditController.SkinEditController_0.Subscribe(UpdateButtons, SkinEditController.SkinEditorEvent.HISTORY_CHANGED);
	}

	private void InitScheme()
	{
		SkinEditorPartScheme[] array = skinEditorPartScheme_0;
		foreach (SkinEditorPartScheme skinEditorPartScheme in array)
		{
			NGUITools.SetActive(skinEditorPartScheme.gameObject, false);
			skinEditorPartScheme.SetOnSelectPartCallback(OnSelectedPartItem);
			skinEditorPartScheme.InitTextures();
		}
		SkinEditorPartScheme.PartType partType = dictionary_1[SkinEditController.SkinEditController_0.string_1];
		int num = (int)partType;
		NGUITools.SetActive(skinEditorPartScheme_0[num].gameObject, true);
	}

	private void OnSelectedPartItem(SkinEditorPartItem skinEditorPartItem_1)
	{
		skinEditorPartItem_0 = skinEditorPartItem_1;
		SkinEditController.SkinEditController_0.partItemType_0 = skinEditorPartItem_1.itemType;
		SkinEditController.SkinEditController_0.StartHistoryFrom(skinEditorPartItem_1.itemTexture.Texture_0);
		skinEditorCanvas_0.SetCanvas(skinEditorPartItem_1.itemTexture.Texture_0);
		UpdateButtons();
	}

	public void OnUndoButtonClick()
	{
		Texture2D texture2D = Utility.CopyTexture(SkinEditController.SkinEditController_0.GetPrevFromHistory());
		if (texture2D != null)
		{
			skinEditorCanvas_0.SetCanvas(texture2D);
			skinEditorPartItem_0.itemTexture.Texture_0 = texture2D;
		}
	}

	public void OnRedoButtonClick()
	{
		Texture2D texture2D = Utility.CopyTexture(SkinEditController.SkinEditController_0.GetNextFromHistory());
		if (texture2D != null)
		{
			skinEditorCanvas_0.SetCanvas(texture2D);
			skinEditorPartItem_0.itemTexture.Texture_0 = texture2D;
		}
	}

	public void OnClearButtonClick()
	{
		MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("window.skin_editor.clear_canvas.confirm"), delegate
		{
			skinEditorCanvas_0.Clear();
			SkinEditController.SkinEditController_0.ClearPartHistory((Texture2D)skinEditorCanvas_0.canvasTexture.Texture_0);
			UpdateButtons();
		}, string.Empty, KeyCode.Return, null, string.Empty));
	}

	private void UpdateButtons(int int_0 = 0)
	{
		uibutton_0.Boolean_0 = SkinEditController.SkinEditController_0.HasPrevHistory();
		uibutton_1.Boolean_0 = SkinEditController.SkinEditController_0.HasNextHistory();
		uibutton_2.Boolean_0 = SkinEditController.SkinEditController_0.HasHistory();
	}
}
