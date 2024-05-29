using System;
using System.Runtime.CompilerServices;
using ProtoBuf;
using UnityEngine;
using engine.controllers;
using engine.helpers;
using engine.unity;

namespace engine.network
{
	[ProtoContract]
	public sealed class DataSchemeUpdateNetworkCommand : AbstractNetworkCommand
	{
		[ProtoMember(1)]
		public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

		[CompilerGenerated]
		private static Action action_0;

		public override NetworkCommandInfo NetworkCommandInfo_0
		{
			get
			{
				return networkCommandInfo_0;
			}
		}

		private new static void Init()
		{
			NetworkCommands.Register(typeof(DataSchemeUpdateNetworkCommand), 5);
		}

		public override void Run()
		{
			Log.AddLine("DATA SCEME UPDATE RECEIVED");
			if (AppStateController.AppStateController_0.States_0 != AppStateController.States.MAIN_MENU)
			{
				AppStateController.AppStateController_0.Subscribe(OnMainMenuAppState, AppStateController.States.MAIN_MENU);
			}
			else
			{
				ReinitGame();
			}
		}

		private void OnMainMenuAppState()
		{
			AppStateController.AppStateController_0.Unsubscribe(OnMainMenuAppState);
			ReinitGame();
		}

		private void ReinitGame()
		{
			Log.AddLine("Data scheme update! Needs reinit game!", Log.LogLevel.WARNING);
			WindowController.WindowController_0.HideAllWindow(true, true);
			Screen.lockCursor = false;
			MessageWindow.Show(new MessageWindowParams(LocalizationStore.Get("data.scheme_update"), delegate
			{
				Screen.lockCursor = false;
				InitScreen.InitScreen_0.LoadInitScreen();
			}, "OK", KeyCode.Return));
		}
	}
}
