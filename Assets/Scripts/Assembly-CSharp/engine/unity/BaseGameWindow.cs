using System;
using engine.events;

namespace engine.unity
{
	public class BaseGameWindow : BaseWindow
	{
		private static BaseEvent<GameWindowEventArg> baseEvent_0 = new BaseEvent<GameWindowEventArg>();

		private GameWindowType gameWindowType_0;

		public GameWindowType GameWindowType_0
		{
			get
			{
				if (gameWindowType_0 == GameWindowType.NotInit)
				{
					InitWindowType();
				}
				return gameWindowType_0;
			}
		}

		public static void Subscribe(GameWindowType gameWindowType_1, Action<GameWindowEventArg> action_0)
		{
			if (!baseEvent_0.Contains(action_0, gameWindowType_1))
			{
				baseEvent_0.Subscribe(action_0, gameWindowType_1);
			}
		}

		public static void Unsubscribe(GameWindowType gameWindowType_1, Action<GameWindowEventArg> action_0)
		{
			if (baseEvent_0.Contains(action_0, gameWindowType_1))
			{
				baseEvent_0.Unsubscribe(action_0, gameWindowType_1);
			}
		}

		public override void OnShow()
		{
			GameWindowEventArg.GameWindowEventArg_0.GameWindowType_0 = GameWindowType_0;
			GameWindowEventArg.GameWindowEventArg_0.Boolean_0 = true;
			baseEvent_0.Dispatch(GameWindowEventArg.GameWindowEventArg_0, GameWindowType_0);
			baseEvent_0.Dispatch(GameWindowEventArg.GameWindowEventArg_0, GameWindowType.None);
		}

		public override void OnHide()
		{
			GameWindowEventArg.GameWindowEventArg_0.GameWindowType_0 = GameWindowType_0;
			GameWindowEventArg.GameWindowEventArg_0.Boolean_0 = false;
			baseEvent_0.Dispatch(GameWindowEventArg.GameWindowEventArg_0, GameWindowType_0);
			baseEvent_0.Dispatch(GameWindowEventArg.GameWindowEventArg_0, GameWindowType.None);
		}

		private void InitWindowType()
		{
			object[] customAttributes = GetType().GetCustomAttributes(typeof(GameWindowParamsAttribute), true);
			gameWindowType_0 = GameWindowType.None;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				gameWindowType_0 = ((GameWindowParamsAttribute)customAttributes[0]).GameWindowType_0;
			}
		}
	}
}
