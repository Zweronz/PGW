using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.controllers;
using engine.helpers;
using engine.network;
using engine.unity;
using pixelgun.tutorial;

[ProtoContract]
public sealed class WipePlayerNetworkCommand : AbstractNetworkCommand
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

	public WipePlayerNetworkCommand()
	{
		hashSet_0 = new HashSet<AppStateController.States> { AppStateController.States.MAIN_MENU };
	}

	public override void DeferredRun()
	{
		WipeUser();
	}

	private void WipeUser()
	{
		Log.AddLine("WipePlayerNetworkCommand::WipeUser > User data wipe! Need reload game.", Log.LogLevel.WARNING);
		WindowController.WindowController_0.ForceHideAllWindow();
		SettingsController.ResetSettings();
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			AppController.AppController_0.StartLauncher();
		}
		MessageWindow.Show(new MessageWindowParams(LocalizationStore.Get("window.msg.error.wipe"), delegate
		{
			SettingsController.ResetSettings();
			InitScreen.InitScreen_0.LoadInitScreen();
		}));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(WipePlayerNetworkCommand), 103);
	}
}
