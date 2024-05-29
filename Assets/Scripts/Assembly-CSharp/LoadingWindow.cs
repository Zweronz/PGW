using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

[GameWindowParams(GameWindowType.Loading)]
public class LoadingWindow : BaseGameWindow
{
	public UITexture uitexture_0;

	public UILabel uilabel_0;

	public GameObject gameObject_0;

	public GameObject gameObject_1;

	public GameObject gameObject_2;

	public UIButton uibutton_0;

	public float float_0 = 10f;

	private int int_0 = 20;

	private static LoadingWindow loadingWindow_0;

	[CompilerGenerated]
	private static Action action_0;

	public static LoadingWindow LoadingWindow_0
	{
		get
		{
			return loadingWindow_0;
		}
	}

	public static void Show(LoadingWindowParams loadingWindowParams_0)
	{
		if (!(loadingWindow_0 != null))
		{
			loadingWindow_0 = BaseWindow.Load("LoadingWindow") as LoadingWindow;
			loadingWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			loadingWindow_0.Parameters_0.bool_5 = false;
			loadingWindow_0.Parameters_0.bool_0 = true;
			loadingWindow_0.Parameters_0.bool_6 = false;
			loadingWindow_0.InternalShow(loadingWindowParams_0);
		}
	}

	public override void OnShow()
	{
		if (base.WindowShowParameters_0 == null)
		{
			return;
		}
		Lobby.Lobby_0.Hide();
		DeactivateAllObjects();
		LoadingWindowParams loadingWindowParams = (LoadingWindowParams)base.WindowShowParameters_0;
		if (loadingWindowParams.modeData_0 != null)
		{
			MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(loadingWindowParams.modeData_0.Int32_1);
			string path = "LevelLoadings/Hi/Loading_" + objectByKey.String_0;
			uitexture_0.Texture_0 = Resources.Load(path) as Texture2D;
			uilabel_0.gameObject.SetActive(true);
			uilabel_0.String_0 = Localizer.Get(objectByKey.String_1);
			if (loadingWindowParams.modeData_0.ModeType_0 == ModeType.ARENA)
			{
				gameObject_0.SetActive(true);
			}
		}
		else
		{
			string path2 = ((!(loadingWindowParams.string_0 == Defs.String_11)) ? ("LevelLoadings/Hi/Loading_" + loadingWindowParams.string_0) : "UI/images/SelectMapWindowBkg");
			uitexture_0.Texture_0 = Resources.Load(path2) as Texture2D;
		}
		WindowController.WindowController_0.WindowFader_0.OnFadeOutComplete();
		Loader.Loader_0.Show();
		int num = int_0;
		if (MonoSingleton<FightController>.Prop_0.ModeData_0 != null && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.ARENA)
		{
			num = int_0 + 100;
		}
		Vector3 localPosition = Loader.Loader_0.LoaderContainer.transform.localPosition;
		Loader.Loader_0.LoaderContainer.transform.localPosition = new Vector3(localPosition.x, num, localPosition.z);
		NGUITools.SetActive(uibutton_0.gameObject, false);
		Invoke("EnableCancelButton", float_0);
		base.OnShow();
	}

	private void DeactivateAllObjects()
	{
		uilabel_0.gameObject.SetActive(false);
		gameObject_0.SetActive(false);
		gameObject_1.SetActive(false);
		gameObject_2.SetActive(false);
	}

	private void OnLevelWasLoaded(int int_1)
	{
		LoadingWindowParams loadingWindowParams = (LoadingWindowParams)base.WindowShowParameters_0;
		if (loadingWindowParams.bool_0)
		{
			Hide();
		}
	}

	public override void OnHide()
	{
		base.OnHide();
		Loader.Loader_0.Hide();
		uibutton_0 = null;
		CancelInvoke("EnableCancelButton");
		loadingWindow_0 = null;
	}

	public void OnCancelButtonClick()
	{
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			Loader.Loader_0.Hide();
			MessageWindow.Show(new MessageWindowParams(LocalizationStore.Get("bad_connection.reload_the_game"), delegate
			{
				InitScreen.InitScreen_0.LoadInitScreen();
			}));
		}
		else
		{
			MonoSingleton<FightController>.Prop_0.Quit();
		}
	}

	private void EnableCancelButton()
	{
		if (uibutton_0 != null)
		{
			Screen.lockCursor = false;
			NGUITools.SetActive(uibutton_0.gameObject, true);
		}
	}
}
