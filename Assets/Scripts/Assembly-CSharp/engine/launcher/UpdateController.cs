using System.Diagnostics;
using System.IO;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.operations;
using engine.system;

namespace engine.launcher
{
	public sealed class UpdateController : BaseEvent<UpdateControllerEventArgs>
	{
		public static class LauncherWorkMode
		{
			public static readonly string string_0 = "launcher";

			public static readonly string string_1 = "updater";

			public static readonly string string_2 = "updatecomplete";
		}

		public enum Events
		{
			ERROR = 0,
			UPDATE_LAUNCHER = 1,
			UPDATE_LAUNCHER_NEED_UPDATE = 2,
			UPDATE_GAME = 3,
			UPDATE_DECOMPRESSING_GAME = 4,
			PROGRESS_UPDATE = 5,
			UPDATE_NOT_NEED = 6
		}

		public enum States
		{
			NONE = 0,
			DOWNLOADING = 1,
			UNPACKING = 2
		}

		private static UpdateController updateController_0;

		private string string_0 = LauncherWorkMode.string_0;

		private States states_0;

		private UpdateControllerEventArgs updateControllerEventArgs_0 = new UpdateControllerEventArgs();

		private bool bool_0;

		private DownloaderFileTriggerResourcesUrl downloaderFileTriggerResourcesUrl_0;

		private DownloaderFileTriggerResourcesUrl downloaderFileTriggerResourcesUrl_1;

		public States States_0
		{
			get
			{
				return states_0;
			}
		}

		public static UpdateController UpdateController_0
		{
			get
			{
				if (updateController_0 == null)
				{
					updateController_0 = new UpdateController();
				}
				return updateController_0;
			}
		}

		private UpdateController()
		{
			AppInfoController.AppInfoController_0.Subscribe(InitAppInfoComplete, AppInfoController.Events.GET_APP_INFO_COMPLETE);
			AppInfoController.AppInfoController_0.Subscribe(InitAppInfoError, AppInfoController.Events.GET_APP_INFO_ERROR);
			AppInfoController.AppInfoController_0.Subscribe(DeleteAllAppContent, AppInfoController.Events.LOCAL_APP_INFO_EMPTY);
			AppInfoController.AppInfoController_0.Subscribe(UpdateLauncher, AppInfoController.Events.LAUNCHER_NEED_UPDATE);
			AppInfoController.AppInfoController_0.Subscribe(UpdateGame, AppInfoController.Events.GAME_NEED_UPDATE);
			AppInfoController.AppInfoController_0.Subscribe(UpdateNotNeed, AppInfoController.Events.UPDATE_NOT_NEED);
			downloaderFileTriggerResourcesUrl_0 = new DownloaderFileTriggerResourcesUrl();
			downloaderFileTriggerResourcesUrl_1 = new DownloaderFileTriggerResourcesUrl();
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalSubscribe(OnApplicationQuit);
		}

		public bool CheckOnlyLauncherUpdate()
		{
			SetWorkMode(false);
			if (string_0 != LauncherWorkMode.string_1)
			{
				AppController.AppController_0.CheckUniqueProcess();
				Log.AddLine("[UpdateController. Check launcher app update in UPDATER_MODE!!!!]", Log.LogLevel.WARNING);
				return false;
			}
			UnityEngine.Debug.Log("[UpdateController. Check needs update launcher app content.]");
			UpdateLauncherInUpdaterMode();
			return true;
		}

		public void CheckGameUpdate(bool bool_1 = false)
		{
			SetWorkMode(bool_1);
			if (string_0 == LauncherWorkMode.string_1)
			{
				Log.AddLine("[UpdateController. Check game app update in UPDATER_MODE forbidden!!!!]", Log.LogLevel.ERROR);
				return;
			}
			Log.AddLine("[UpdateController. Check needs update game app content.]");
			AppInfoController.AppInfoController_0.CheckUpdates(bool_1);
		}

		public void Stop()
		{
			switch (states_0)
			{
			case States.UNPACKING:
				UnpackerFile.UnpackerFile_0.StopUnpacking();
				UnpackerFile.UnpackerFile_0.UnsubscribeAll();
				Log.AddLine("UpdateController::Stop > stop unpacking");
				break;
			case States.DOWNLOADING:
				if (DownloaderFile.DownloaderFile_0.Boolean_5)
				{
					DownloaderFile.DownloaderFile_0.Stop();
				}
				DownloaderFile.DownloaderFile_0.UnsubscribeAll();
				Log.AddLine("UpdateController::Stop > stop downloading");
				break;
			}
			SetState(States.NONE);
		}

		private void OnApplicationQuit()
		{
			Stop();
		}

		private void SetWorkMode(bool bool_1)
		{
			string customArgument = CommandLineReader.GetCustomArgument("mode");
			string_0 = ((!string.IsNullOrEmpty(customArgument)) ? customArgument : LauncherWorkMode.string_0);
		}

		private void SetState(States states_1)
		{
			states_0 = states_1;
		}

		private void UpdateNotNeed()
		{
			Log.AddLine("[UpdateController. Update app content not needs!]");
			DispatchOfType(Events.UPDATE_NOT_NEED, null);
		}

		private void InitAppInfoComplete()
		{
			Log.AddLine("[UpdateController. Init app info from server complete!]");
			CheckNeedsReloadGame();
			InitUrls();
			if (string_0 == LauncherWorkMode.string_2)
			{
				Log.AddLine("[UpdateController. Fixed in app info launcher updated, and remove update dir.]");
				AppInfoController.AppInfoController_0.LauncherUpdateComplete();
				Utility.DeleteDirectory(UnpackerFile.UnpackerFile_0.string_0);
				string_0 = LauncherWorkMode.string_0;
			}
		}

		private void CheckNeedsReloadGame()
		{
			string customArgument = CommandLineReader.GetCustomArgument("reload");
			if (!string.IsNullOrEmpty(customArgument) && !bool_0)
			{
				bool_0 = true;
				Log.AddLine("[UpdateController::CheckNeedsReloadGame. Need reload game content!]", Log.LogLevel.WARNING);
				DeleteAllAppContent();
			}
		}

		private void InitUrls()
		{
			AppGameVersionInfo appGameVersionInfo_ = AppInfoController.AppInfoController_0.AppGameVersionInfo_0;
			downloaderFileTriggerResourcesUrl_0.Reset();
			downloaderFileTriggerResourcesUrl_1.Reset();
			downloaderFileTriggerResourcesUrl_0.AddUrl(appGameVersionInfo_.appVersionInfo_0.string_2);
			downloaderFileTriggerResourcesUrl_1.AddUrl(appGameVersionInfo_.appVersionInfo_1.string_2);
			downloaderFileTriggerResourcesUrl_0.AddUrl(appGameVersionInfo_.appVersionInfo_0.string_1);
			downloaderFileTriggerResourcesUrl_1.AddUrl(appGameVersionInfo_.appVersionInfo_1.string_1);
		}

		private void InitAppInfoError()
		{
			Log.AddLine("[UpdateController. Init app info error!]");
			DispatchOfType(Events.ERROR, null);
		}

		private void DeleteAllAppContent()
		{
			AppGameVersionInfo appGameVersionInfo_ = AppInfoController.AppInfoController_0.AppGameVersionInfo_0;
			string path = Path.Combine(appGameVersionInfo_.appVersionInfo_1.string_4, appGameVersionInfo_.appVersionInfo_1.string_3);
			if (File.Exists(path))
			{
				Utility.KillAllProcess(appGameVersionInfo_.appVersionInfo_1.string_3);
			}
			if (Directory.Exists(appGameVersionInfo_.appVersionInfo_1.string_4))
			{
				Log.AddLine(string.Format("[UpdateController. Remove game data dir: {0}]", appGameVersionInfo_.appVersionInfo_1.string_4), Log.LogLevel.WARNING);
				Utility.DeleteDirectory(appGameVersionInfo_.appVersionInfo_1.string_4);
			}
		}

		public void StartLauncherUpdater()
		{
			AppVersionInfo appVersionInfo_ = AppInfoController.AppInfoController_0.AppGameVersionInfo_0.appVersionInfo_0;
			File.Delete(appVersionInfo_.GetPackageName());
			string arg = Path.Combine(UnpackerFile.UnpackerFile_0.string_0, appVersionInfo_.string_3);
			Utility.SetChmod(arg, "755");
			Log.AddLine(string.Format("[UpdateController. Starting launcher in updater mode: {0}]", arg));
			Utility.StartApplicationProcess(arg, string.Format("-batchmode -nographics -CustomArgs:mode={0},launcherFile={1}", LauncherWorkMode.string_1, appVersionInfo_.string_3));
			Process.GetCurrentProcess().Kill();
		}

		private void UpdateLauncherInUpdaterMode()
		{
			string customArgument = CommandLineReader.GetCustomArgument("launcherFile");
			string directoryName = Path.GetDirectoryName(BaseAppController.String_0);
			Log.AddLine(string.Format("[UpdateController. Started launcher in updater mode, process file name: {0}]", Path.Combine(directoryName, customArgument)));
			string text = Path.Combine(directoryName, "..");
			Utility.CopyDirectoryAndFiles(directoryName, text, true, true);
			string arg = Path.Combine(text, customArgument);
			Utility.SetChmod(arg, "755");
			Log.AddLine(string.Format("[UpdateController. Starting launcher from updater mode in update complete mode: {0}]", arg));
			Utility.StartApplicationProcess(arg, string.Format("-CustomArgs:mode={0}", LauncherWorkMode.string_2));
			Process.GetCurrentProcess().Kill();
		}

		private void UpdateLauncher()
		{
			Log.AddLine("[UpdateController. Load launcher start!]");
			DispatchOfType(Events.UPDATE_LAUNCHER, null);
			if (DownloaderFile.DownloaderFile_0.Boolean_2)
			{
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateLaucherError, DownloaderFile.Events.ERROR);
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateLaucherProgress, DownloaderFile.Events.PROGRESS);
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateLaucherComplete, DownloaderFile.Events.COMPLETE);
				DownloaderFile.DownloaderFile_0.Start(downloaderFileTriggerResourcesUrl_0.String_0, true, string.Empty);
				SetState(States.DOWNLOADING);
				LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.START_DOWNLOADING_LAUNCHER);
			}
			else
			{
				Log.AddLine("DownloaderFile|UpdateLauncher. Downloader alrady is busy!");
			}
		}

		private void UpdateLaucherError(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			Log.AddLine(string.Format("[UpdateController. Load launcher error: {0}]", downloaderFileEventArgs_0.string_0));
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			downloaderFileTriggerResourcesUrl_0.Int32_0++;
			DispatchOfType(Events.ERROR, null);
			SetState(States.NONE);
			LauncherStatArgs launcherStatArgs = new LauncherStatArgs();
			launcherStatArgs.string_0 = downloaderFileEventArgs_0.string_0;
			LauncherStatArgs gparam_ = launcherStatArgs;
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(gparam_, LauncherStatEvents.END_DOWNLOADING_LAUNCHER);
		}

		private void UpdateLaucherProgress(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			updateControllerEventArgs_0.double_0 = downloaderFileEventArgs_0.double_0;
			updateControllerEventArgs_0.long_0 = downloaderFileEventArgs_0.long_0;
			updateControllerEventArgs_0.long_1 = downloaderFileEventArgs_0.long_1;
			DispatchOfType(Events.PROGRESS_UPDATE, updateControllerEventArgs_0);
		}

		private void UpdateLaucherComplete(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			Log.AddLine("[UpdateController. Load launcher file complete!]");
			updateControllerEventArgs_0.Reset();
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			downloaderFileTriggerResourcesUrl_0.Int32_0 = 0;
			AppVersionInfo appVersionInfo_ = AppInfoController.AppInfoController_0.AppGameVersionInfo_0.appVersionInfo_0;
			Log.AddLine(string.Format("[UpdateController. Unpack launcher start: {0}]", appVersionInfo_.GetPackageName()));
			UnpackerFile.UnpackerFile_0.Subscribe(UnpackLaucherError, UnpackerFile.Events.ERROR);
			UnpackerFile.UnpackerFile_0.Subscribe(UnpackLaucherCompete, UnpackerFile.Events.COMPLETE);
			UnpackerFile.UnpackerFile_0.Unpack(appVersionInfo_, false);
			SetState(States.UNPACKING);
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.END_DOWNLOADING_LAUNCHER);
		}

		private void UnpackLaucherError(UnpackerFileEventArgs unpackerFileEventArgs_0)
		{
			Log.AddLine(string.Format("[UpdateController. Unpack launcher error: {0}]", unpackerFileEventArgs_0.string_0));
			UnpackerFile.UnpackerFile_0.UnsubscribeAll();
			DispatchOfType(Events.ERROR, null);
			SetState(States.NONE);
			LauncherStatArgs launcherStatArgs = new LauncherStatArgs();
			launcherStatArgs.string_0 = unpackerFileEventArgs_0.string_0;
			LauncherStatArgs gparam_ = launcherStatArgs;
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(gparam_, LauncherStatEvents.UNPACKED_LAUNCHER);
		}

		private void UnpackLaucherCompete(UnpackerFileEventArgs unpackerFileEventArgs_0)
		{
			Log.AddLine("[UpdateController. Unpack launcher file complete!]");
			UnpackerFile.UnpackerFile_0.UnsubscribeAll();
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.UNPACKED_LAUNCHER);
			OperationsManager.OperationsManager_0.Add(delegate
			{
				DispatchOfType(Events.UPDATE_LAUNCHER_NEED_UPDATE, null);
			});
			Log.AddLine("[UpdateController. Need move files launcher from update folder in work place! Needs start updater!]");
			SetState(States.NONE);
		}

		private void UpdateGame()
		{
			Log.AddLine("UpdateController. Load game start!");
			DispatchOfType(Events.UPDATE_GAME, null);
			if (DownloaderFile.DownloaderFile_0.Boolean_2)
			{
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateGameError, DownloaderFile.Events.ERROR);
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateGameProgress, DownloaderFile.Events.PROGRESS);
				DownloaderFile.DownloaderFile_0.Subscribe(UpdateGameComplete, DownloaderFile.Events.COMPLETE);
				DownloaderFile.DownloaderFile_0.Start(downloaderFileTriggerResourcesUrl_1.String_0, true, string.Empty);
				SetState(States.DOWNLOADING);
				LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.START_DOWNLOADING_GAME);
			}
			else
			{
				Log.AddLine("DownloaderFile|UpdateGame. Downloader alrady is busy!");
			}
		}

		private void UpdateGameError(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			Log.AddLine(string.Format("[UpdateController. Load game error: {0}]", downloaderFileEventArgs_0.string_0));
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			downloaderFileTriggerResourcesUrl_1.Int32_0++;
			if (!downloaderFileTriggerResourcesUrl_1.Boolean_0)
			{
				UpdateGame();
				return;
			}
			DispatchOfType(Events.ERROR, null);
			SetState(States.NONE);
			LauncherStatArgs launcherStatArgs = new LauncherStatArgs();
			launcherStatArgs.string_0 = downloaderFileEventArgs_0.string_0;
			LauncherStatArgs gparam_ = launcherStatArgs;
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(gparam_, LauncherStatEvents.END_DOWNLOADING_GAME);
		}

		private void UpdateGameProgress(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			updateControllerEventArgs_0.double_0 = downloaderFileEventArgs_0.double_0;
			updateControllerEventArgs_0.long_0 = downloaderFileEventArgs_0.long_0;
			updateControllerEventArgs_0.long_1 = downloaderFileEventArgs_0.long_1;
			DispatchOfType(Events.PROGRESS_UPDATE, updateControllerEventArgs_0);
		}

		private void UpdateGameComplete(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			Log.AddLine("[UpdateController. Load game file complete!]");
			updateControllerEventArgs_0.Reset();
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			DispatchOfType(Events.UPDATE_DECOMPRESSING_GAME, null);
			AppVersionInfo appVersionInfo_ = AppInfoController.AppInfoController_0.AppGameVersionInfo_0.appVersionInfo_1;
			Log.AddLine(string.Format("[UpdateController. Unpack game start: {0}]", appVersionInfo_.GetPackageName()));
			UnpackerFile.UnpackerFile_0.Subscribe(UnpackGameError, UnpackerFile.Events.ERROR);
			UnpackerFile.UnpackerFile_0.Subscribe(UnpackGameProgress, UnpackerFile.Events.PROGRESS);
			UnpackerFile.UnpackerFile_0.Subscribe(UnpackGameComplete, UnpackerFile.Events.COMPLETE);
			UnpackerFile.UnpackerFile_0.Unpack(appVersionInfo_);
			SetState(States.UNPACKING);
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.END_DOWNLOADING_GAME);
		}

		private void UnpackGameError(UnpackerFileEventArgs unpackerFileEventArgs_0)
		{
			Log.AddLine(string.Format("[UpdateController. Unpack game error: {0}]", unpackerFileEventArgs_0.string_0));
			UnpackerFile.UnpackerFile_0.UnsubscribeAll();
			downloaderFileTriggerResourcesUrl_1.Int32_0++;
			if (!downloaderFileTriggerResourcesUrl_1.Boolean_0)
			{
				UpdateGame();
				return;
			}
			DispatchOfType(Events.ERROR, null);
			SetState(States.NONE);
			LauncherStatArgs launcherStatArgs = new LauncherStatArgs();
			launcherStatArgs.string_0 = unpackerFileEventArgs_0.string_0;
			LauncherStatArgs gparam_ = launcherStatArgs;
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(gparam_, LauncherStatEvents.UNPACKED_GAME);
		}

		private void UnpackGameProgress(UnpackerFileEventArgs unpackerFileEventArgs_0)
		{
			updateControllerEventArgs_0.double_0 = unpackerFileEventArgs_0.float_0 / (float)unpackerFileEventArgs_0.long_0 * 100f;
			Log.AddLine(string.Format("[UpdateController. Unpack game progress: {0}]", updateControllerEventArgs_0.double_0));
			DispatchOfType(Events.PROGRESS_UPDATE, updateControllerEventArgs_0);
		}

		private void UnpackGameComplete(UnpackerFileEventArgs unpackerFileEventArgs_0)
		{
			Log.AddLine("[UpdateController. Unpack game file complete!]");
			UnpackerFile.UnpackerFile_0.UnsubscribeAll();
			downloaderFileTriggerResourcesUrl_1.Int32_0 = 0;
			AppInfoController.AppInfoController_0.GameUpdateComplete();
			SetState(States.NONE);
			LauncherStatDispatcher.LauncherStatDispatcher_0.Dispatch(new LauncherStatArgs(), LauncherStatEvents.UNPACKED_GAME);
			CheckGameUpdate();
		}
	}
}
