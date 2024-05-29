using CodeStage.AntiCheat.Detectors;
using UnityEngine;
using engine.controllers;
using engine.helpers;
using engine.network;
using engine.operations;

public sealed class AntiCheatController
{
	public enum CheatType
	{
		MEMORY_HACK = 1,
		SPEED_HACK = 2,
		INJECTION_HACK = 3,
		WALL_HACK = 4,
		DLL_HACK = 5,
		DEPLOY_HACK = 6,
		CHAT_BAN = 100
	}

	private static AntiCheatController antiCheatController_0;

	public static AntiCheatController AntiCheatController_0
	{
		get
		{
			if (antiCheatController_0 == null)
			{
				antiCheatController_0 = new AntiCheatController();
			}
			return antiCheatController_0;
		}
	}

	private AntiCheatController()
	{
	}

	public void Init()
	{
		//StartGameAntiCheats();
		//if (!AppStateController.AppStateController_0.Contains(StartFightAntiCheats, AppStateController.States.JOINED_ROOM))
		//{
		//	AppStateController.AppStateController_0.Subscribe(StartFightAntiCheats, AppStateController.States.JOINED_ROOM);
		//}
		//if (!AppStateController.AppStateController_0.Contains(StopFightAntiCheats, AppStateController.States.LEAVING_ROOM))
		//{
		//	AppStateController.AppStateController_0.Subscribe(StopFightAntiCheats, AppStateController.States.LEAVING_ROOM);
		//}
		//CheckCheatsChecker();
	}

	private void CheckCheatsChecker()
	{
		if (CheatsChecker.CheatsChecker_0.CheckSendDeployCheat())
		{
			SendHackDetected(CheatType.DEPLOY_HACK);
		}
	}

	private void StartGameAntiCheats()
	{
		Log.AddLine("[AntiCheatController::StartGameAntiCheats. Start ObscuredCheatingDetector]");
		ObscuredCheatingDetector.StartDetection(OnObscureCheatingDetected);
		Log.AddLine("[AntiCheatController::StartGameAntiCheats. Start InjectionDetector]");
		InjectionDetector.StartDetection(OnInjectionDetected);
	}

	private void StartFightAntiCheats()
	{
		Log.AddLine("[AntiCheatController::StartFightAntiCheats. Start SpeedHackDetector]");
		SpeedHackDetector.StartDetection(OnSpeedHackDetected);
		Log.AddLine("[AntiCheatController::StartFightAntiCheats. Start WallHackDetector]");
		WallHackDetector.StartDetection(OnWallHackDetected, new Vector3(-1100f, -1100f, -1100f));
	}

	private void StopFightAntiCheats()
	{
		Log.AddLine("[AntiCheatController::StopFightAntiCheats. Stop SpeedHackDetector]");
		SpeedHackDetector.StopDetection();
		Log.AddLine("[AntiCheatController::StopFightAntiCheats. Stop WallHackDetector]");
		WallHackDetector.StopDetection();
	}

	public void Dispose()
	{
		ObscuredCheatingDetector.Dispose();
	}

	public void SendHackDetected(CheatType cheatType_0)
	{
		OperationsManager.OperationsManager_0.Add(delegate
		{
			AbstractNetworkCommand.Send(new HackDetectedNetworkCommand
			{
				cheatType_0 = cheatType_0
			});
		});
	}

	private void OnObscureCheatingDetected()
	{
		SendHackDetected(CheatType.MEMORY_HACK);
		Log.AddLine("AntiCheatController::OnObscureCheatingDetected >> what are you fucking doing, man??? get out here!");
	}

	private void OnSpeedHackDetected()
	{
		SendHackDetected(CheatType.SPEED_HACK);
		Log.AddLine("AntiCheatController::OnSpeedHackDetected >> Oh, man! No so fast!");
	}

	private void OnInjectionDetected()
	{
		SendHackDetected(CheatType.INJECTION_HACK);
		Log.AddLine("AntiCheatController::OnInjectionDetected >> Dude, get out your dirty hands from my DLLs!");
	}

	private void OnWallHackDetected()
	{
		SendHackDetected(CheatType.WALL_HACK);
		Log.AddLine("AntiCheatController::OnWallHackDetected >> You shall not pass!!!");
	}
}
