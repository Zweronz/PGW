using System;
using UnityEngine;
using engine.helpers;
using engine.network;

public sealed class BugReportController
{
	private static BugReportController bugReportController_0;

	private byte[] byte_0;

	public static BugReportController BugReportController_0
	{
		get
		{
			if (bugReportController_0 == null)
			{
				bugReportController_0 = new BugReportController();
			}
			return bugReportController_0;
		}
	}

	public void SwitchWindow()
	{
		if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
		{
			MessageWindowConfirm.MessageWindowConfirm_0.Hide();
		}
		if (BugReportWindow.Boolean_1)
		{
			HideWindow();
			SwitchPauseFight(false);
		}
		else
		{
			GrabScreenShot();
			SwitchPauseFight(true);
			BugReportWindow.Show();
		}
	}

	public void OnSendReport(int int_0, string string_0)
	{
		SendReport(int_0, string_0);
		SwitchWindow();
	}

	private void SwitchPauseFight(bool bool_0)
	{
		if ((!bool_0 || !PauseBattleWindow.Boolean_1) && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.SetPause();
		}
	}

	private void HideWindow()
	{
		byte_0 = null;
		BugReportWindow.BugReportWindow_0.Hide();
	}

	private void GrabScreenShot()
	{
		Texture2D texture2D = RenderScreenToTexture();
		if (texture2D != null)
		{
			byte_0 = texture2D.EncodeToJPG();
		}
		UnityEngine.Object.Destroy(texture2D);
	}

	private Texture2D RenderScreenToTexture()
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
		Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			camera.targetTexture = renderTexture;
			camera.Render();
			camera.targetTexture = null;
		}
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		return texture2D;
	}

	private void SendReport(int int_0, string string_0)
	{
		byte[] byte_ = null;
		try
		{
			byte_ = Utility.LoadCompressedApplicationLogFile();
		}
		catch (Exception ex)
		{
			Log.AddLine("[BugReportController::SendReport. Error get game log file for game report! Error]: " + ex.Message, Log.LogLevel.WARNING);
		}
		BugReportNetworkCommand bugReportNetworkCommand = new BugReportNetworkCommand();
		bugReportNetworkCommand.byte_1 = byte_;
		bugReportNetworkCommand.string_0 = string_0;
		bugReportNetworkCommand.int_1 = int_0;
		bugReportNetworkCommand.byte_0 = byte_0;
		AbstractNetworkCommand.Send(bugReportNetworkCommand);
	}
}
