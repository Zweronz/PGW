using System;
using UnityEngine;
using engine.unity;

[ScreenParams(GameScreenType.SkinEditorScreen)]
public class SkinEditorScreen : BaseGameScreen
{
	private GameObject gameObject_0;

	private int int_0;

	private static SkinEditorScreen skinEditorScreen_0;

	public static SkinEditorScreen SkinEditorScreen_0
	{
		get
		{
			if (skinEditorScreen_0 == null)
			{
				skinEditorScreen_0 = new SkinEditorScreen();
			}
			return skinEditorScreen_0;
		}
	}

	public void Show(int int_1)
	{
		if (!base.Boolean_0)
		{
			WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_1);
			if (objectByKey != null && (objectByKey.Boolean_1 || objectByKey.Boolean_2) && objectByKey.Boolean_0)
			{
				int_0 = int_1;
				ScreenController.ScreenController_0.ShowScreen(skinEditorScreen_0);
			}
		}
	}

	public override void Init()
	{
		base.Init();
		CursorPGW.CursorPGW_0.SetCursorEnable(false);
		gameObject_0 = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("SkinEditorController")) as GameObject;
		SkinEditorController component = gameObject_0.GetComponent<SkinEditorController>();
		WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
		SkinEditorController.int_0 = int_0;
		component.modeEditor = ((!objectByKey.Boolean_1) ? SkinEditorController.ModeEditor.Cape : SkinEditorController.ModeEditor.SkinPers);
		if (ScreenController.ScreenController_0.Boolean_0)
		{
			ScreenController.ScreenController_0.GameObject_0.SetActive(false);
		}
		Action<string> action_0 = null;
		action_0 = delegate
		{
			SkinEditorController.ExitFromSkinEditor -= action_0;
			if (ScreenController.ScreenController_0.Boolean_0)
			{
				ScreenController.ScreenController_0.GameObject_0.SetActive(true);
			}
			if (ScreenController.ScreenController_0.AbstractScreen_0 == this)
			{
				ScreenController.ScreenController_0.HideActiveScreen();
			}
		};
		SkinEditorController.ExitFromSkinEditor += action_0;
	}

	public override void Release()
	{
		if (gameObject_0 != null)
		{
			UnityEngine.Object.Destroy(gameObject_0);
			gameObject_0 = null;
		}
		int_0 = 0;
		CursorPGW.CursorPGW_0.SetCursorEnable(true);
	}
}
