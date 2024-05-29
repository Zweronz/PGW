using System;
using engine.events;

namespace engine.unity
{
	public abstract class BaseGameScreen : AbstractScreen
	{
		private static BaseEvent baseEvent_0 = new BaseEvent();

		private GameScreenType gameScreenType_0;

		public GameScreenType GameScreenType_0
		{
			get
			{
				if (gameScreenType_0 == GameScreenType.NotInit)
				{
					InitScreenType();
				}
				return gameScreenType_0;
			}
		}

		public static void Subscribe(GameScreenType gameScreenType_1, Action action_0)
		{
			if (!baseEvent_0.Contains(action_0, gameScreenType_1))
			{
				baseEvent_0.Subscribe(action_0, gameScreenType_1);
			}
		}

		public static void Unsubscribe(GameScreenType gameScreenType_1, Action action_0)
		{
			if (baseEvent_0.Contains(action_0, gameScreenType_1))
			{
				baseEvent_0.Unsubscribe(action_0, gameScreenType_1);
			}
		}

		public override void Init()
		{
			baseEvent_0.Dispatch(GameScreenType_0);
		}

		private void InitScreenType()
		{
			object[] customAttributes = GetType().GetCustomAttributes(typeof(ScreenParamsAttribute), true);
			gameScreenType_0 = GameScreenType.None;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				gameScreenType_0 = ((ScreenParamsAttribute)customAttributes[0]).GameScreenType_0;
			}
		}
	}
}
