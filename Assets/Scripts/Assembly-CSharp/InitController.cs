using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using UnityEngine;
using engine.controllers;
using engine.unity;

public sealed class InitController : MonoBehaviour
{
	public List<GameObject> DontDestroyObjects;

	private IEnumerator Start()
	{
		Debug.Log("[InitController::Start]");
		AddDontDestroyObject();
		CleanScene();
		yield return null;
		UnityThreadHelper.EnsureHelper();
		AppController.AppController_0.InitParams();
		SharedSettings.SharedSettings_0.Init(AppController.AppController_0.ProcessArguments_0.String_1);
		CrashController.CrashController_0.Init(SharedSettings.SharedSettings_0);
		CrashHandler.Init(SharedSettings.SharedSettings_0);
		AppsMenu.SetCurrentLanguage();
		AppController.AppController_0.Init();
		ScreenController.Reset();
		Storager.Init(AppController.AppController_0.ProcessArguments_0.String_1);
		AppStateController.AppStateController_0.SetState(AppStateController.States.APP_INIT);
		InitScreen.InitScreen_0.Show();
		PhotonNetwork.Int32_3 = 20;
	}

	private void AddDontDestroyObject()
	{
		PhotonHandler photonHandler = (PhotonHandler)UnityEngine.Object.FindObjectOfType(typeof(PhotonHandler));
		if (photonHandler != null && photonHandler.gameObject != null)
		{
			DontDestroyObjects.Add(photonHandler.gameObject);
		}
		HTTPUpdateDelegator hTTPUpdateDelegator = (HTTPUpdateDelegator)UnityEngine.Object.FindObjectOfType(typeof(HTTPUpdateDelegator));
		if (hTTPUpdateDelegator != null && hTTPUpdateDelegator.gameObject != null)
		{
			DontDestroyObjects.Add(hTTPUpdateDelegator.gameObject);
		}
	}

	private void CleanScene()
	{
		ActionAllObjectsInScene(true, false, delegate(GameObject gameObject_0)
		{
			if (!(gameObject_0 == base.gameObject) && !DontDestroyObjects.Contains(gameObject_0))
			{
				gameObject_0.SetActive(true);
			}
		});
		ActionAllObjectsInScene(true, true, delegate(GameObject gameObject_0)
		{
			if (!(gameObject_0 == base.gameObject) && !DontDestroyObjects.Contains(gameObject_0))
			{
				UnityEngine.Object.Destroy(gameObject_0);
			}
		});
	}

	public static void ActionAllObjectsInScene(bool bool_0, bool bool_1, Action<GameObject> action_0)
	{
		if (action_0 == null)
		{
			return;
		}
		GameObject[] array = ((!bool_1) ? Resources.FindObjectsOfTypeAll<GameObject>() : UnityEngine.Object.FindObjectsOfType<GameObject>());
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if ((!bool_0 || !(gameObject.transform.parent != null)) && gameObject.hideFlags != HideFlags.NotEditable && gameObject.hideFlags != HideFlags.HideAndDontSave)
			{
				action_0(gameObject);
			}
		}
	}
}
