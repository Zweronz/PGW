using System.Collections.Generic;
using UnityEngine;
using engine.controllers;
using engine.helpers;
using engine.network;
using engine.unity;

public class ProfileWindowController
{
	private int int_0;

	private List<GameObject> list_0 = new List<GameObject>();

	private static ProfileWindowController profileWindowController_0;

	public static ProfileWindowController ProfileWindowController_0
	{
		get
		{
			if (profileWindowController_0 == null)
			{
				profileWindowController_0 = new ProfileWindowController();
			}
			return profileWindowController_0;
		}
	}

	public void ShowProfileWindowForPlayer(int int_1)
	{
		if (int_1 == 0 || int_0 != 0)
		{
			return;
		}
		if (int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
		{
			showWindow(int_1, AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU);
			return;
		}
		UserData user = UserController.UserController_0.GetUser(int_1);
		if (user != null && Time.fixedTime - user.user_0.float_0 < 60f)
		{
			showWindow(int_1, AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU);
			return;
		}
		GetAnyUserDataNetworkCommand getAnyUserDataNetworkCommand = new GetAnyUserDataNetworkCommand();
		getAnyUserDataNetworkCommand.int_1 = int_1;
		AbstractNetworkCommand.Send(getAnyUserDataNetworkCommand);
		int_0 = int_1;
		subscribe();
	}

	public void HideProfileWindow()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			if (!NGUITools.GetActive(list_0[i]))
			{
				NGUITools.SetActive(list_0[i], true);
			}
		}
		list_0.Clear();
	}

	private void subscribe()
	{
		UsersData.Subscribe(UsersData.EventType.GET_ANY_USER_CMD_COMPLETE, OnGetUserComplete);
		UsersData.Subscribe(UsersData.EventType.GET_ANY_USER_CMD_ERROR, OnGetUserError);
	}

	private void unsubscribe()
	{
		UsersData.Unsubscribe(UsersData.EventType.GET_ANY_USER_CMD_COMPLETE, OnGetUserComplete);
		UsersData.Unsubscribe(UsersData.EventType.GET_ANY_USER_CMD_ERROR, OnGetUserError);
	}

	private void OnGetUserComplete(UsersData.EventData eventData_0)
	{
		int num = int_0;
		unsubscribe();
		int_0 = 0;
		if (num != eventData_0.int_0)
		{
			Log.AddLine(string.Format("[ProfileWindowController::OnGetUserComplete] ERROR _currentUID = {0} evnt.intValue = {1}", num, eventData_0.int_0), Log.LogLevel.ERROR);
		}
		else
		{
			showWindow(num, AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU);
		}
	}

	private void OnGetUserError(UsersData.EventData eventData_0)
	{
		unsubscribe();
		int_0 = 0;
	}

	private void showWindow(int int_1, bool bool_0 = true)
	{
		GameObject gameObject_ = ScreenController.ScreenController_0.GameObject_0;
		for (int i = 0; i < gameObject_.transform.childCount; i++)
		{
			GameObject gameObject = gameObject_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf && needDisableUIRootObject(gameObject))
			{
				gameObject.SetActive(false);
				list_0.Add(gameObject);
			}
		}
		ProfileWindow.Show(new ProfileWindowParams(int_1, bool_0 ? ProfileWindowParams.OpenType.OTHER_PROFILE_LOBBY : ProfileWindowParams.OpenType.OTHER_PROFILE));
	}

	private bool needDisableUIRootObject(GameObject gameObject_0)
	{
		if (string.Equals(gameObject_0.name, "NGUICamera"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "Cursor"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "TooltipWindow@Common"))
		{
			return false;
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null && gameObject_0.Equals(HeadUpDisplay.HeadUpDisplay_0.gameObject))
		{
			return false;
		}
		return true;
	}
}
