using System.Runtime.CompilerServices;
using engine.events;
using engine.helpers;

namespace engine.controllers
{
	public sealed class AppStateController : BaseEvent
	{
		public enum States
		{
			NONE = -1,
			APP_INIT = 0,
			ENTER = 1,
			DATA_INIT = 2,
			GAME_LOADING = 3,
			GAME_LOADED = 4,
			MAIN_MENU = 5,
			JOINED_ROOM = 6,
			IN_BATTLE = 7,
			IN_BATTLE_OVER_WINDOW = 8,
			LEAVING_ROOM = 9
		}

		public bool bool_0 = true;

		private static AppStateController appStateController_0;

		[CompilerGenerated]
		private States states_0;

		public static AppStateController AppStateController_0
		{
			get
			{
				if (appStateController_0 == null)
				{
					appStateController_0 = new AppStateController();
					DependSceneEvent<ApplicationFocusUnityEvent, bool>.GlobalSubscribe(appStateController_0.OnFocusChanged);
				}
				return appStateController_0;
			}
		}

		public States States_0
		{
			[CompilerGenerated]
			get
			{
				return states_0;
			}
			[CompilerGenerated]
			private set
			{
				states_0 = value;
			}
		}

		public void SetState(States states_1)
		{
			if (States_0 != states_1)
			{
				States_0 = states_1;
				Log.AddLine("[AppStateController::SetState. Current App State is]: " + States_0);
				Dispatch(States_0);
			}
		}

		private void OnFocusChanged(bool bool_1)
		{
			bool_0 = bool_1;
		}
	}
}
