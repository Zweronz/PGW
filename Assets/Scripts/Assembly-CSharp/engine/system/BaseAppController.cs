using System.Diagnostics;
using System.Runtime.CompilerServices;
using engine.events;
using engine.helpers;
using engine.launcher;
using engine.network;

namespace engine.system
{
	public abstract class BaseAppController : BaseEvent
	{
		public enum Events
		{
			GAME_CLOSE = 0,
			GAME_NEED_AUTH = 1
		}

		public ServerInfo serverInfo_0;

		[CompilerGenerated]
		private VersionInfo versionInfo_0;

		[CompilerGenerated]
		private static BaseAppController baseAppController_0;

		public VersionInfo VersionInfo_0
		{
			[CompilerGenerated]
			get
			{
				return versionInfo_0;
			}
			[CompilerGenerated]
			protected set
			{
				versionInfo_0 = value;
			}
		}

		public static string String_0
		{
			get
			{
				return Process.GetCurrentProcess().Modules[0].FileName;
			}
		}

		public static BaseAppController BaseAppController_0
		{
			[CompilerGenerated]
			get
			{
				return baseAppController_0;
			}
			[CompilerGenerated]
			protected set
			{
				baseAppController_0 = value;
			}
		}

		protected BaseAppController()
		{
			Log.AddLine(string.Format("[Application process file name]: {0}", String_0));
		}

		public abstract void InitParams();

		public abstract void Init();

		public virtual void StartGame()
		{
		}

		public virtual void StartLauncher()
		{
		}

		public virtual void CheckUniqueProcess()
		{
			Log.AddLine(string.Format("AppController|CheckDublicateProcess. Check unique process = {0}", String_0));
			if (Utility.GetProcessByName(String_0).Length <= 1)
			{
				Log.AddLine("Check unique process done!");
			}
			else
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}
}
