using System.IO;
using System.Reflection;
using engine.events;
using engine.helpers;

namespace engine.launcher
{
	public sealed class AppInfoController : BaseEvent
	{
		public enum Events
		{
			GET_APP_INFO_ERROR = 0,
			GET_APP_INFO_COMPLETE = 1,
			LOCAL_APP_INFO_EMPTY = 2,
			LAUNCHER_NEED_UPDATE = 3,
			GAME_NEED_UPDATE = 4,
			UPDATE_NOT_NEED = 5
		}

		private static AppInfoController appInfoController_0;

		private bool bool_0;

		private IPlatformSettings iplatformSettings_0;

		private AppGameVersionInfo appGameVersionInfo_0;

		private AppGameVersionInfo appGameVersionInfo_1;

		private VersionInfo versionInfo_0;

		private DownloaderFileTriggerResourcesUrl downloaderFileTriggerResourcesUrl_0;

		public static AppInfoController AppInfoController_0
		{
			get
			{
				if (appInfoController_0 == null)
				{
					appInfoController_0 = new AppInfoController();
				}
				return appInfoController_0;
			}
		}

		public VersionInfo VersionInfo_0
		{
			get
			{
				return versionInfo_0;
			}
		}

		public AppGameVersionInfo AppGameVersionInfo_0
		{
			get
			{
				return appGameVersionInfo_0;
			}
		}

		public AppGameVersionInfo AppGameVersionInfo_1
		{
			get
			{
				return appGameVersionInfo_1;
			}
		}

		private AppInfoController()
		{
			versionInfo_0 = new VersionInfo(Assembly.GetExecutingAssembly().GetName().Version);
			InitUrls();
		}

		private void InitUrls()
		{
			downloaderFileTriggerResourcesUrl_0 = new DownloaderFileTriggerResourcesUrl();
			downloaderFileTriggerResourcesUrl_0.AddUrl(AbstractPlatformSettings.IPlatformSettings_0.String_1);
			downloaderFileTriggerResourcesUrl_0.AddUrl(AbstractPlatformSettings.IPlatformSettings_0.String_0);
		}

		public void CheckUpdates(bool bool_1 = false)
		{
			if (bool_0)
			{
				Log.AddLine("AppInfoController|Check. Is busy!", Log.LogLevel.WARNING);
				return;
			}
			bool_0 = true;
			appGameVersionInfo_0 = ((!bool_1) ? appGameVersionInfo_0 : null);
			InitLocalAppGameVersion();
			InitServerAppGameVersion();
		}

		private void Process()
		{
			if (appGameVersionInfo_1.Boolean_0)
			{
				Log.AddLine("[AppInfoController. Local game app info empty! Needs reload all app content]");
				Dispatch(Events.LOCAL_APP_INFO_EMPTY);
			}
			Events gparam_ = Events.UPDATE_NOT_NEED;
			string path = Path.Combine(appGameVersionInfo_1.appVersionInfo_1.string_4, appGameVersionInfo_1.appVersionInfo_1.string_3);
			bool boolean_;
			if (((boolean_ = appGameVersionInfo_1.appVersionInfo_0.versionInfo_0.Boolean_0) && versionInfo_0 != appGameVersionInfo_0.appVersionInfo_0.versionInfo_0) || (!boolean_ && appGameVersionInfo_1.appVersionInfo_0.versionInfo_0 != appGameVersionInfo_0.appVersionInfo_0.versionInfo_0))
			{
				Log.AddLine("[AppInfoController. Need update launcher]: ", appGameVersionInfo_0.appVersionInfo_0);
				gparam_ = Events.LAUNCHER_NEED_UPDATE;
			}
			else if (appGameVersionInfo_1.appVersionInfo_1.versionInfo_0.Boolean_0 || !File.Exists(path) || appGameVersionInfo_1.appVersionInfo_1.versionInfo_0 != appGameVersionInfo_0.appVersionInfo_1.versionInfo_0)
			{
				Log.AddLine("[AppInfoController. Need update game]: ", appGameVersionInfo_0.appVersionInfo_1);
				gparam_ = Events.GAME_NEED_UPDATE;
			}
			bool_0 = false;
			Dispatch(gparam_);
		}

		public void LauncherUpdateComplete()
		{
			IPlatformSettings iPlatformSettings_ = AbstractPlatformSettings.IPlatformSettings_0;
			appGameVersionInfo_1.appVersionInfo_0.CopyFrom(appGameVersionInfo_0.appVersionInfo_0);
			appGameVersionInfo_1.Serialize(Path.Combine(iPlatformSettings_.String_3, iPlatformSettings_.String_2));
		}

		public void GameUpdateComplete()
		{
			IPlatformSettings iPlatformSettings_ = AbstractPlatformSettings.IPlatformSettings_0;
			appGameVersionInfo_1.appVersionInfo_1.CopyFrom(appGameVersionInfo_0.appVersionInfo_1);
			appGameVersionInfo_1.Serialize(Path.Combine(iPlatformSettings_.String_3, iPlatformSettings_.String_2));
		}

		private void InitLocalAppGameVersion()
		{
			IPlatformSettings iPlatformSettings_ = AbstractPlatformSettings.IPlatformSettings_0;
			string text = Path.Combine(iPlatformSettings_.String_3, iPlatformSettings_.String_2);
			if (!File.Exists(text))
			{
				appGameVersionInfo_1 = new AppGameVersionInfo();
				appGameVersionInfo_1.Serialize(text);
			}
			else
			{
				appGameVersionInfo_1 = AppGameVersionInfo.Deserialize(text);
			}
		}

		private void InitServerAppGameVersion()
		{
			if (appGameVersionInfo_0 != null)
			{
				Process();
				return;
			}
			Log.AddLine("AppInfoController|InitServerAppGameVersion. Dowload server game version info = " + downloaderFileTriggerResourcesUrl_0.String_0);
			DownloaderFile.DownloaderFile_0.Subscribe(InitServerAppGameVersionComplete, DownloaderFile.Events.COMPLETE);
			DownloaderFile.DownloaderFile_0.Subscribe(InitServerAppGameVersionError, DownloaderFile.Events.ERROR);
			DownloaderFile.DownloaderFile_0.Start(downloaderFileTriggerResourcesUrl_0.String_0, false, string.Empty);
		}

		private void InitServerAppGameVersionComplete(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			Log.AddLine("AppInfoController|InitServerAppGameVersionComplete. Get game version info from server complete!");
			appGameVersionInfo_0 = AppGameVersionInfo.Deserialize(AbstractPlatformSettings.IPlatformSettings_0.String_2);
			downloaderFileTriggerResourcesUrl_0.Int32_0 = 0;
			Dispatch(Events.GET_APP_INFO_COMPLETE);
			Process();
		}

		private void InitServerAppGameVersionError(DownloaderFileEventArgs downloaderFileEventArgs_0)
		{
			DownloaderFile.DownloaderFile_0.UnsubscribeAll();
			Log.AddLine(string.Format("AppInfoController|InitServerAppGameVersionError. Get game version info from server error! {0}", downloaderFileEventArgs_0.string_0));
			bool_0 = false;
			downloaderFileTriggerResourcesUrl_0.Int32_0++;
			Dispatch(Events.GET_APP_INFO_ERROR);
		}
	}
}
