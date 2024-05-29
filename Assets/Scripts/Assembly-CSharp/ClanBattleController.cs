using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using engine.controllers;
using engine.events;
using engine.helpers;
using engine.network;
using engine.unity;

public sealed class ClanBattleController : BaseEvent<ClanBattleController.EventData>, IBasePartyController
{
	public enum InviteType
	{
		NONE = 0,
		REQUEST = 1,
		ACCEPT = 2,
		REJECT = 3,
		NO_AVAILABLE = 4
	}

	public enum InvitingStatus
	{
		STATUS_NONE = 0,
		STATUS_WAIT_ANSW = 1,
		STATUS_READY = 2
	}

	public sealed class EventData
	{
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private int int_1;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			set
			{
				int_1 = value;
			}
		}
	}

	public enum EventType
	{
		INVITE_RESPONSE = 0,
		HIDE_INVITE_NOTIF = 1
	}

	public List<int> list_1 = new List<int>();

	private static ClanBattleController clanBattleController_0;

	private string string_0 = string.Empty;

	public static ClanBattleController ClanBattleController_0
	{
		get
		{
			if (clanBattleController_0 == null)
			{
				clanBattleController_0 = new ClanBattleController();
			}
			return clanBattleController_0;
		}
	}

	public void GetResponseFromUser(int type, int userId, string from_user_nick, int mode_id = 0, bool is_ranked = false)
	{
		if (type > 1)
		{
			EventData eventData = new EventData();
			eventData.Int32_1 = userId;
			eventData.Int32_0 = type;
			EventData gparam_ = eventData;
			Dispatch(gparam_, EventType.INVITE_RESPONSE);
		}
		else if (AppStateController.AppStateController_0.States_0 <= AppStateController.States.MAIN_MENU && !ClanInviteBattleWindow.Boolean_1 && !ClanWaitBattleWindow.Boolean_1)
		{
			NotificationClanBattleInviteData notificationClanBattleInviteData = new NotificationClanBattleInviteData();
			notificationClanBattleInviteData.Int32_3 = mode_id;
			notificationClanBattleInviteData.Int32_2 = userId;
			notificationClanBattleInviteData.String_0 = from_user_nick;
			NotificationController.NotificationController_0.Push((!is_ranked) ? NotificationType.NOTIFICATION_CLAN_FRIENDLY_BATTLE_INVITE : NotificationType.NOTIFICATION_CLAN_BATTLE_INVITE, notificationClanBattleInviteData);
		}
		else
		{
			ClanBattleController_0.SendRequestToUser(4, userId, from_user_nick);
		}
	}

	public void SendRequestToUser(int type, int userId, string from_user_nick = "", int mode_id = 0, bool is_ranked = false)
	{
		ClanInviteToBattleNetworkCommand clanInviteToBattleNetworkCommand = new ClanInviteToBattleNetworkCommand();
		clanInviteToBattleNetworkCommand.int_4 = type;
		clanInviteToBattleNetworkCommand.int_1 = userId;
		clanInviteToBattleNetworkCommand.int_3 = mode_id;
		clanInviteToBattleNetworkCommand.string_0 = from_user_nick;
		clanInviteToBattleNetworkCommand.bool_0 = is_ranked;
		AbstractNetworkCommand.Send(clanInviteToBattleNetworkCommand);
	}

	public void SetInviteList(List<int> list_2)
	{
		list_1 = list_2;
		ClanStartFightNetworkCommand clanStartFightNetworkCommand = new ClanStartFightNetworkCommand();
		clanStartFightNetworkCommand.int_4 = ClanStartFightNetworkCommand.int_3;
		clanStartFightNetworkCommand.list_0 = list_1;
		AbstractNetworkCommand.Send(clanStartFightNetworkCommand);
	}

	public bool SendInviteToFight(string fightId, string roomName, int type)
	{
		if (!list_1.IsEmpty())
		{
			ClanStartFightNetworkCommand clanStartFightNetworkCommand = new ClanStartFightNetworkCommand();
			clanStartFightNetworkCommand.int_4 = type;
			clanStartFightNetworkCommand.list_0 = list_1;
			clanStartFightNetworkCommand.string_1 = fightId;
			clanStartFightNetworkCommand.string_0 = roomName;
			AbstractNetworkCommand.Send(clanStartFightNetworkCommand);
			list_1.Clear();
			return true;
		}
		return false;
	}

	public void GetResponseToFight(string fightId, string roomName, int type, int from_user_id)
	{
		if (ClanWaitBattleWindow.ClanWaitBattleWindow_0 != null)
		{
			if (type == ClanStartFightNetworkCommand.int_2)
			{
				ClanWaitBattleWindow.ClanWaitBattleWindow_0.Hide();
				MessageWindow.Show(new MessageWindowParams(Localizer.Get("ui.clan_wait.fight_canceled")));
			}
			else if (type == ClanStartFightNetworkCommand.int_1)
			{
				string_0 = roomName;
				ModeData modeData_ = null;
				if (!MonoSingleton<FightController>.Prop_0.FightRoomsController_0.GetRoomFromList(roomName, out modeData_))
				{
					if (!MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Contains(OnUpdRoomList, RoomsEventType.UpdateMaps))
					{
						MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Subscribe(OnUpdRoomList, RoomsEventType.UpdateMaps);
					}
				}
				else
				{
					OnUpdRoomList();
				}
			}
		}
		EventData eventData = new EventData();
		eventData.Int32_1 = from_user_id;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.HIDE_INVITE_NOTIF);
	}

	private void OnUpdRoomList()
	{
		if (ClanWaitBattleWindow.ClanWaitBattleWindow_0 != null)
		{
			ClanWaitBattleWindow.ClanWaitBattleWindow_0.Hide();
		}
		if (MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Contains(OnUpdRoomList, RoomsEventType.UpdateMaps))
		{
			MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Unsubscribe(OnUpdRoomList, RoomsEventType.UpdateMaps);
		}
		if (!MonoSingleton<FightController>.Prop_0.FightRoomsController_0.GetRoomFreePlaceFromList(string_0))
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("ui.clan_wait.no_free_place_in_room"), delegate
			{
				ClearInvitedRoom();
			}));
		}
		else
		{
			MonoSingleton<FightController>.Prop_0.JoinRoom(string_0);
		}
	}

	public bool CheckInvitedRoom()
	{
		return !string_0.IsNullOrEmpty();
	}

	public void ClearInvitedRoom()
	{
		string_0 = string.Empty;
	}
}
