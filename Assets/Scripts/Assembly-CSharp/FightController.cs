using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;
using WebSocketSharp;
using engine.controllers;
using engine.helpers;
using engine.network;
using engine.operations;
using engine.unity;

public sealed class FightController : MonoSingleton<FightController>
{
	public enum NetworkStateMode
	{
		None = 0,
		Online = 1,
		Offline = 2
	}

	public enum ConnectionStatus
	{
		Connecting = 0,
		InLobby = 1,
		Creating = 2,
		Joining = 3,
		InBattle = 4,
		EndFight = 5,
		SwitchRoom = 6,
		Exiting = 7,
		ConnectClosed = 8
	}

	private static float float_0 = 1f;

	public bool bool_0;

	public int int_0 = 3;

	public float float_1 = 10f;

	private int int_1;

	private Action action_0;

	private string string_0 = string.Empty;

	private SeveralOperations severalOperations_0;

	private int int_2;

	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	private bool bool_1;

	private PresetMatchMakingData presetMatchMakingData_0;

	private bool bool_2;

	[CompilerGenerated]
	private ModeData modeData_0;

	[CompilerGenerated]
	private string string_3;

	[CompilerGenerated]
	private ConnectionStatus connectionStatus_0;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private PhotonNetworkStatistics photonNetworkStatistics_0;

	[CompilerGenerated]
	private GameFPSStatistics gameFPSStatistics_0;

	[CompilerGenerated]
	private FightStatController fightStatController_0;

	[CompilerGenerated]
	private FightRoomsController fightRoomsController_0;

	[CompilerGenerated]
	private FightTimeController fightTimeController_0;

	[CompilerGenerated]
	private FightMatchMakingController fightMatchMakingController_0;

	[CompilerGenerated]
	private int int_4;

	public static float Single_0
	{
		get
		{
			return float_0;
		}
		set
		{
			float_0 = 1f;
		}
	}

	public ModeData ModeData_0
	{
		[CompilerGenerated]
		get
		{
			return modeData_0;
		}
		[CompilerGenerated]
		private set
		{
			modeData_0 = value;
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_3;
		}
		[CompilerGenerated]
		private set
		{
			string_3 = value;
		}
	}

	public string String_1
	{
		get
		{
			return string_0;
		}
	}

	public ConnectionStatus ConnectionStatus_0
	{
		[CompilerGenerated]
		get
		{
			return connectionStatus_0;
		}
		[CompilerGenerated]
		private set
		{
			connectionStatus_0 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		private set
		{
			int_3 = value;
		}
	}

	public NetworkStateMode NetworkStateMode_0
	{
		get
		{
			if (ModeData_0 == null)
			{
				return NetworkStateMode.None;
			}
			return (!ModeData_0.Boolean_0) ? NetworkStateMode.Online : NetworkStateMode.Offline;
		}
	}

	public PhotonNetworkStatistics PhotonNetworkStatistics_0
	{
		[CompilerGenerated]
		get
		{
			return photonNetworkStatistics_0;
		}
		[CompilerGenerated]
		set
		{
			photonNetworkStatistics_0 = value;
		}
	}

	public GameFPSStatistics GameFPSStatistics_0
	{
		[CompilerGenerated]
		get
		{
			return gameFPSStatistics_0;
		}
		[CompilerGenerated]
		set
		{
			gameFPSStatistics_0 = value;
		}
	}

	public FightStatController FightStatController_0
	{
		[CompilerGenerated]
		get
		{
			return fightStatController_0;
		}
		[CompilerGenerated]
		private set
		{
			fightStatController_0 = value;
		}
	}

	public FightRoomsController FightRoomsController_0
	{
		[CompilerGenerated]
		get
		{
			return fightRoomsController_0;
		}
		[CompilerGenerated]
		private set
		{
			fightRoomsController_0 = value;
		}
	}

	public FightTimeController FightTimeController_0
	{
		[CompilerGenerated]
		get
		{
			return fightTimeController_0;
		}
		[CompilerGenerated]
		private set
		{
			fightTimeController_0 = value;
		}
	}

	public FightMatchMakingController FightMatchMakingController_0
	{
		[CompilerGenerated]
		get
		{
			return fightMatchMakingController_0;
		}
		[CompilerGenerated]
		private set
		{
			fightMatchMakingController_0 = value;
		}
	}

	private int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	protected override void Init()
	{
		base.name = "FightController";
		UnityEngine.Object.DontDestroyOnLoad(this);
		FightStatController_0 = new FightStatController();
		FightRoomsController_0 = new FightRoomsController();
		FightTimeController_0 = new FightTimeController();
		FightMatchMakingController_0 = new FightMatchMakingController();
		Disconnect();
		PhotonPeer.RegisterType(typeof(DamageRPCData), 68, DamageRPCData.SerializePhoton, DamageRPCData.DeserializePhoton);
		PhotonPeer.RegisterType(typeof(UserScoreAndStatRPCData), 83, UserScoreAndStatRPCData.SerializePhoton, UserScoreAndStatRPCData.DeserializePhoton);
	}

	public void Update()
	{
		FightStatController_0.Update();
		FightTimeController_0.Update();
	}

	public void LevelLoaded()
	{
		PhotonNetwork.Boolean_8 = true;
	}

	public override string ToString()
	{
		return FightStatController_0.ToString();
	}

	public void Connect()
	{
		ConnectionStatus_0 = ConnectionStatus.Connecting;
		PhotonNetwork.Boolean_6 = false;
		try
		{
			Log.AddLine("[FightController::Connect. Connect to photon with protocol version]: " + GlobalGameController.String_0);
			PhotonNetwork.ConnectUsingSettings(GlobalGameController.String_0);
		}
		catch (Exception exception_)
		{
			MonoSingleton<Log>.Prop_0.DumpError(exception_);
			Log.AddLine("[FightController::Connect. Error connect to photon network, protocol version]: " + GlobalGameController.String_0);
		}
	}

	public void Disconnect(bool bool_3 = false)
	{
		if (!bool_3)
		{
			ConnectionStatus_0 = ConnectionStatus.ConnectClosed;
		}
		if (PhotonNetwork.Boolean_0)
		{
			Log.AddLine("[FightController::Disconnect. Disconnect from photon netowrk]");
			PhotonNetwork.Disconnect();
		}
	}

	public void JoinRoom(string string_4)
	{
		Log.AddLine(string.Format("[FightController::JoinRoom]  roomName = {0}", string_4));
		ModeData modeData = null;
		FightRoomsController_0.GetRoomFromList(string_4, out modeData);
		if (modeData == null)
		{
			return;
		}
		ModeData_0 = modeData;
		FightScreen.FightScreen_0.Show();
		FightScreen.FightScreen_0.ShowLoadingWindow();
		ConnectionStatus_0 = ConnectionStatus.Joining;
		try
		{
			PhotonNetwork.JoinRoom(string_4);
		}
		catch (Exception)
		{
			Connect();
		}
	}

	public void StartFightForModeType(ModeType modeType_0)
	{
		ModeData randomModeForModeType = FightMatchMakingController_0.GetRandomModeForModeType(modeType_0);
		StartFightForMode(randomModeForModeType);
	}

	public void StartFightForMode(ModeData modeData_1)
	{
		if (ClanController.ClanController_0.UserClanData_0 != null && modeData_1.ModeType_0 != ModeType.DUEL && ClanController.ClanController_0.UserClanData_0.list_0.Count > 1)
		{
			ClanInviteBattleWindowParams clanInviteBattleWindowParams = new ClanInviteBattleWindowParams();
			clanInviteBattleWindowParams.modeData_0 = modeData_1;
			clanInviteBattleWindowParams.bool_0 = true;
			clanInviteBattleWindowParams.int_5 = 3;
			ClanInviteBattleWindow.Show(clanInviteBattleWindowParams);
		}
		else
		{
			if (SelectMapWindow.SelectMapWindow_0 != null)
			{
				SelectMapWindow.SelectMapWindow_0.Hide();
			}
			JoinRandomRoom(modeData_1);
		}
	}

	public void JoinRandomRoom(ModeData modeData_1 = null, Func<int, int, bool> func_0 = null)
	{
		Log.AddLine(string.Format("[FightController::JoinRandomRoom]"));
		if (modeData_1 == null && ModeData_0 == null)
		{
			return;
		}
		ModeData_0 = modeData_1 ?? ModeData_0;
		if (ModeData_0.Boolean_0)
		{
			Disconnect();
			PhotonNetwork.Boolean_3 = true;
		}
		FightScreen.FightScreen_0.Show();
		FightScreen.FightScreen_0.ShowLoadingWindow();
		ConnectionStatus_0 = ConnectionStatus.Joining;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Mode", ModeData_0.Int32_0);
		hashtable.Add("Pass", string.Empty);
		hashtable.Add("IsRanked", true);
		if (ModeData_0.Boolean_2)
		{
			if (presetMatchMakingData_0 == null)
			{
				FightMatchMakingController_0.GetPressetDataFromRating();
				presetMatchMakingData_0 = FightMatchMakingController_0.PresetMatchMakingData_0;
			}
			hashtable.Add("PressetRating", presetMatchMakingData_0.int_1);
		}
		try
		{
			FightMatchMakingController_0.JoinRandomRoom(hashtable, func_0);
		}
		catch (Exception)
		{
			Connect();
		}
	}

	public void CreateRoom(ModeData modeData_1, int int_5 = 0, int int_6 = 0, string string_4 = "", string string_5 = "")
	{
		Log.AddLine(string.Format("[FightController::CreateRoom]"));
		ModeData_0 = modeData_1;
		int_2 = int_5;
		Int32_0 = int_6;
		string_1 = string_4;
		string_2 = string_5;
		bool_1 = !ModeData_0.Boolean_0 && int_5 == 0 && int_6 == 0 && string.IsNullOrEmpty(string_4) && string.IsNullOrEmpty(string_5);
		if (ModeData_0.Boolean_2)
		{
			presetMatchMakingData_0 = FightMatchMakingController_0.PresetMatchMakingData_0;
		}
		if (ModeData_0.Boolean_0)
		{
			Disconnect();
			PhotonNetwork.Boolean_3 = true;
		}
		FightScreen.FightScreen_0.Show();
		FightScreen.FightScreen_0.ShowLoadingWindow();
		ConnectionStatus_0 = ConnectionStatus.Creating;
		action_0 = OnCreateFightAllowed;
		StartFightNetworkCommand startFightNetworkCommand = new StartFightNetworkCommand();
		startFightNetworkCommand.int_1 = ModeData_0.Int32_0;
		startFightNetworkCommand.int_2 = ((int_6 != 0) ? int_6 : ModesController.ModesController_0.GetMinTimeLimit(modeData_1.Int32_0));
		startFightNetworkCommand.bool_0 = bool_1;
		AbstractNetworkCommand.Send(startFightNetworkCommand);
	}

	public void LeaveRoom(bool bool_3 = false)
	{
		bool_2 = bool_3;
		Log.AddLine(string.Format("[FightController::LeaveRoom]"));
		PhotonNetwork.Boolean_8 = true;
		if (ConnectionStatus_0 != ConnectionStatus.SwitchRoom)
		{
			ConnectionStatus_0 = ConnectionStatus.Exiting;
		}
		PhotonNetwork.LeaveRoom();
		AppStateController.AppStateController_0.SetState(AppStateController.States.LEAVING_ROOM);
	}

	public void Quit()
	{
		Log.AddLine(string.Format("[FightController::Quit]"));
		LeaveRoom();
		Reset();
		FightScreen.FightScreen_0.SwitchToMenu(true);
	}

	public void SwitchRoom(ModeData modeData_1)
	{
		Log.AddLine(string.Format("[FightController::SwitchRooms]"));
		ConnectionStatus_0 = ConnectionStatus.SwitchRoom;
		Reset();
		ModeData_0 = modeData_1;
		LeaveRoom();
	}

	public void FinishFight(int int_5, EndFightNetworkCommand.IsWinState isWinState_0, int int_6 = 0, bool bool_3 = false, int int_7 = 0)
	{
		Log.AddLineFormat("[FightController::FinishFight]. FightId={0}, Set Status = ConnectionStatus.EndFight", string_0);
		ConnectionStatus_0 = ConnectionStatus.EndFight;
		action_0 = OnAssignRecreateFightId;
		EndFightNetworkCommand endFightNetworkCommand = new EndFightNetworkCommand();
		endFightNetworkCommand.string_0 = string_0;
		endFightNetworkCommand.int_1 = int_5;
		endFightNetworkCommand.isWinState_0 = isWinState_0;
		endFightNetworkCommand.int_2 = int_6;
		endFightNetworkCommand.photonNetworkStatistics_0 = PhotonNetworkStatistics_0;
		endFightNetworkCommand.gameFPSStatistics_0 = GameFPSStatistics_0;
		endFightNetworkCommand.dictionary_0 = FightStatController_0.Dictionary_0;
		endFightNetworkCommand.bool_0 = bool_3;
		endFightNetworkCommand.int_3 = int_7;
		endFightNetworkCommand.bool_1 = bool_2;
		AbstractNetworkCommand.Send(endFightNetworkCommand);
		bool_2 = false;
	}

	public void OnFinishFight()
	{
		FightStatController_0.Clear();
	}

	public void FakeReconnect()
	{
		OnConnectionFail();
	}

	private void Reset()
	{
		ModeData_0 = null;
		String_0 = string.Empty;
	}

	private string GetFightIdFromPhotonRoom()
	{
		string result = string_0;
		if (PhotonNetwork.Room_0 != null && PhotonNetwork.Room_0.Hashtable_0 != null && PhotonNetwork.Room_0.Hashtable_0.ContainsKey("FightId"))
		{
			result = PhotonNetwork.Room_0.Hashtable_0["FightId"] as string;
		}
		return result;
	}

	public void OnFightAllowed(string string_4)
	{
		string_0 = string_4;
		FightStatController_0.Clear();
		Log.AddLine("[FightController::OnFightAllowed] | FightID from server: " + string_4);
		if (action_0 != null)
		{
			Action action = action_0;
			action_0 = null;
			action();
		}
	}

	public void OnFightDisallowed()
	{
		Quit();
	}

	private void OnCreateFightAllowed()
	{
		Log.AddLine("[FightController::OnCreateFightAllowed]");
		ConnectionStatus_0 = ConnectionStatus.Joining;
		if (ModeData_0 == null)
		{
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.hashtable_0 = new Hashtable();
		roomOptions.hashtable_0.Add("Mode", ModeData_0.Int32_0);
		roomOptions.hashtable_0.Add("Pass", string_2);
		roomOptions.hashtable_0.Add("RoundTime", (Int32_0 == 0) ? ModesController.ModesController_0.GetMinTimeLimit(ModeData_0.Int32_0) : Int32_0);
		roomOptions.hashtable_0.Add("TimeMatchStart", Utility.Double_0);
		roomOptions.hashtable_0.Add("CustomRoomName", string_1);
		roomOptions.hashtable_0.Add("FightId", string_0);
		roomOptions.hashtable_0.Add("IsRanked", bool_1);
		if (presetMatchMakingData_0 != null)
		{
			roomOptions.hashtable_0.Add("PressetRating", presetMatchMakingData_0.int_1);
			Log.AddLine("[OnCreateFightAllowed::PressetRating = " + presetMatchMakingData_0.int_1);
		}
		roomOptions.string_0 = new string[roomOptions.hashtable_0.Keys.Count];
		roomOptions.hashtable_0.Keys.CopyTo(roomOptions.string_0, 0);
		roomOptions.int_0 = ((int_2 == 0) ? ModeData_0.Int32_3 : int_2);
		try
		{
			PhotonNetwork.CreateRoom(null, roomOptions, null);
		}
		catch (Exception)
		{
			Connect();
		}
	}

	private void OnJoinFightAllowed()
	{
		Log.AddLine("[FightController::OnJoinFightAllowed]");
		if (PhotonNetwork.Room_0.Hashtable_0.ContainsKey("RoundTime"))
		{
			object obj = PhotonNetwork.Room_0.Hashtable_0["RoundTime"];
			if (obj is int)
			{
				Int32_0 = Convert.ToInt32(obj);
			}
		}
		if (PhotonNetwork.Room_0.Hashtable_0.ContainsKey("PressetRating"))
		{
			object obj2 = PhotonNetwork.Room_0.Hashtable_0["PressetRating"];
			if (obj2 is int)
			{
				Log.AddLine("[OnJoinFightAllowed::PressetRating = " + obj2);
			}
		}
		if (PhotonNetwork.Room_0 != null && !PhotonNetwork.Room_0.String_1.IsNullOrEmpty() && !string_0.IsNullOrEmpty())
		{
			ClanBattleController.ClanBattleController_0.SendInviteToFight(string_0, PhotonNetwork.Room_0.String_1, ClanStartFightNetworkCommand.int_1);
		}
		DefsCostyling();
		AppStateController.AppStateController_0.SetState(AppStateController.States.JOINED_ROOM);
		ConnectionStatus_0 = ConnectionStatus.InBattle;
		String_0 = PhotonNetwork.Room_0.String_1;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("uid", UserController.UserController_0.UserData_0.user_0.int_0);
		PhotonNetwork.PhotonPlayer_0.SetCustomProperties(hashtable);
		FightScreen.FightScreen_0.SwitchToBattle();
		FightStatController_0.Clear();
		FightTimeController_0.SetFakeTimeEndFight();
	}

	public void OnAssignRecreateFightId()
	{
		if (ModeData_0 != null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["FightId"] = string_0;
			PhotonNetwork.Room_0.SetCustomProperties(hashtable);
			Log.AddLine("[FightController::OnAssignRecreateFightId] FightId" + string_0);
		}
	}

	public void StartFightCommand()
	{
		action_0 = OnReconnectFightAllowed;
		string fightIdFromPhotonRoom = GetFightIdFromPhotonRoom();
		Hashtable hashtable = new Hashtable();
		hashtable["TimeMatchStart"] = Utility.Double_0;
		PhotonNetwork.Room_0.SetCustomProperties(hashtable);
		StartFightNetworkCommand startFightNetworkCommand = new StartFightNetworkCommand();
		startFightNetworkCommand.int_1 = ModeData_0.Int32_0;
		startFightNetworkCommand.string_0 = fightIdFromPhotonRoom;
		AbstractNetworkCommand.Send(startFightNetworkCommand);
	}

	private void OnReconnectFightAllowed()
	{
		Log.AddLineFormat("[FightController::OnReconnectFightAllowed]. FightId={0}, Set Status = ConnectionStatus.InBattle", string_0);
		ConnectionStatus_0 = ConnectionStatus.InBattle;
		FightTimeController_0.SetFakeTimeEndFight();
	}

	public void OnSyncFightGlobalParameters(string string_4, double double_0, int int_5, int int_6)
	{
		if (!(string_0 != string_4))
		{
			if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myScoreController != null)
			{
				WeaponManager.weaponManager_0.myScoreController.TakeGlobalParamsFromServer(int_5, int_6);
			}
			FightTimeController_0.SetTimeToEnd(double_0);
		}
	}

	private void OnConnectedToMaster()
	{
		int_1 = 0;
		TypedLobby typedLobby = new TypedLobby();
		typedLobby.string_0 = string.Format("lobby_pgw_tier_{0}", UserController.UserController_0.GetUserTier());
		Log.AddLine("[FightController::OnConnectedToMaster. Connect to photon lobby]: " + typedLobby.string_0);
		PhotonNetwork.JoinLobby(typedLobby);
	}

	private void OnJoinedLobby()
	{
		Log.AddLine(string.Format("[FightController::OnJoinedLobby. Join to lobby complete, lobby name]: {0}", PhotonNetwork.TypedLobby_0.ToString()));
		ConnectionStatus_0 = ConnectionStatus.InLobby;
		PhotonNetwork.String_2 = Defs.GetPlayerNameOrDefault();
		FightRoomsController_0.OnGetRoomList();
		if (!string.IsNullOrEmpty(String_0) && ModeData_0 != null)
		{
			JoinRoom(String_0);
		}
		if (ModeData_0 != null)
		{
			JoinRandomRoom();
		}
	}

	private void OnJoinedRoom()
	{
		Log.AddLine("[FightController::OnJoinedRoom]");
		Int32_1 = 0;
		FightMatchMakingController_0.OnJoinedRoom();
		if (ConnectionStatus_0 == ConnectionStatus.InBattle)
		{
			bool_0 = true;
			return;
		}
		string fightIdFromPhotonRoom = GetFightIdFromPhotonRoom();
		if (!string.IsNullOrEmpty(fightIdFromPhotonRoom) && ModeData_0 != null)
		{
			PhotonNetwork.Boolean_8 = false;
			Log.AddLine("[FightController::OnJoinedRoom] FightId" + fightIdFromPhotonRoom);
			action_0 = OnJoinFightAllowed;
			StartFightNetworkCommand startFightNetworkCommand = new StartFightNetworkCommand();
			startFightNetworkCommand.int_1 = ModeData_0.Int32_0;
			startFightNetworkCommand.string_0 = fightIdFromPhotonRoom;
			AbstractNetworkCommand.Send(startFightNetworkCommand);
			ClanBattleController.ClanBattleController_0.ClearInvitedRoom();
		}
		else
		{
			string text = ((fightIdFromPhotonRoom == null) ? "null" : ((!string.IsNullOrEmpty(fightIdFromPhotonRoom)) ? fightIdFromPhotonRoom : "string.Empty"));
			string text2 = ((ModeData_0 != null) ? ModeData_0.ToString() : "null");
			Log.AddLineError("[FightController::OnJoinedRoom] fightId = {0} CurrentMode = {1} Status = {2}", text, text2, ConnectionStatus_0.ToString());
			LeaveRoom();
		}
	}

	private void OnLeftRoom()
	{
		Log.AddLine("[FightController::OnLeftRoom]");
		presetMatchMakingData_0 = null;
		if (ConnectionStatus_0 == ConnectionStatus.Exiting)
		{
			if (PhotonNetwork.Boolean_3)
			{
				if (ModeData_0.ModeType_0 == ModeType.ARENA)
				{
					FightOfflineController.FightOfflineController_0.CompleteArenaBattle();
				}
				else if (ModeData_0.ModeType_0 == ModeType.CAMPAIGN)
				{
					FightOfflineController.FightOfflineController_0.CompleteCompaignBattle();
				}
				else
				{
					FightScreen.FightScreen_0.SwitchToMenu(true);
				}
			}
			else
			{
				FightScreen.FightScreen_0.SwitchToMenu(true);
			}
			Reset();
			FinishFight(0, EndFightNetworkCommand.IsWinState.Lose, 0, true);
			if (PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.Boolean_3 = false;
				Connect();
			}
		}
		else if (ConnectionStatus_0 == ConnectionStatus.SwitchRoom)
		{
			if (PhotonNetwork.Boolean_2)
			{
				JoinRandomRoom();
			}
			else
			{
				Connect();
			}
		}
		FightStatController_0.Clear();
	}

	private void OnCreatedRoom()
	{
		Log.AddLine("[FightController::OnCreatedRoom]");
	}

	private void OnPhotonCreateRoomFailed()
	{
		Log.AddLine("[FightController::OnPhotonCreateRoomFailed]");
		if (ClanBattleController.ClanBattleController_0.SendInviteToFight(string_0, PhotonNetwork.Room_0.String_1, ClanStartFightNetworkCommand.int_2))
		{
			JoinRandomRoom();
		}
	}

	private void OnPhotonJoinRoomFailed()
	{
		FightMatchMakingController.State state_ = FightMatchMakingController_0.State_0;
		Log.AddLine("[FightController::OnPhotonJoinRoomFailed]. FightMatchMakingController.State = " + state_);
		if (state_ != 0)
		{
			if (ModeData_0.Boolean_2)
			{
				ProcessRoomByPreset();
				return;
			}
			if (++Int32_1 >= MatchMakingSettings.Get.CountTryJointRandomRoom)
			{
				Log.AddLine("[FightController::OnPhotonJoinRoomFailed]. Count of reconnect Failed. CreateRoom");
				CreateRoom(ModeData_0, 0, 0, string.Empty, string.Empty);
				return;
			}
			ModeData randomModeForModeType = ModeData_0;
			if (state_ == FightMatchMakingController.State.JoinRandomRoomModeType)
			{
				randomModeForModeType = FightMatchMakingController_0.GetRandomModeForModeType(ModeData_0.ModeType_0);
			}
			JoinRandomRoom(randomModeForModeType);
			LoadingWindow.LoadingWindow_0.Hide();
			FightScreen.FightScreen_0.ShowLoadingWindow();
		}
		else if (ClanBattleController.ClanBattleController_0.CheckInvitedRoom())
		{
			Quit();
			ClanBattleController.ClanBattleController_0.ClearInvitedRoom();
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("ui.clan_wait.no_free_place_in_room"), null, "OK", KeyCode.None, WindowController.GameEvent.IN_MAIN_MENU));
		}
		else
		{
			JoinRandomRoom();
		}
	}

	private void ProcessRoomByPreset()
	{
		PresetMatchMakingData presetMatchMakingData_ = null;
		int nextPressetFromCustomRating = FightMatchMakingController_0.GetNextPressetFromCustomRating(presetMatchMakingData_0, out presetMatchMakingData_);
		presetMatchMakingData_0 = presetMatchMakingData_;
		if (nextPressetFromCustomRating < 0)
		{
			CreateRoom(ModeData_0, 0, 0, string.Empty, string.Empty);
		}
		else
		{
			JoinRandomRoom(ModeData_0);
		}
	}

	private void OnPhotonRandomJoinFailed()
	{
		Log.AddLine("[FightController::OnPhotonRandomJoinFailed]");
	}

	private void OnFailedToConnectToPhoton()
	{
		Log.AddLine("[FightController::OnFailedToConnectToPhoton]");
		OnPhotonFailure();
	}

	private void OnConnectionFail()
	{
		Log.AddLine("[FightController::OnConnectionFail]");
		OnPhotonFailure();
	}

	private void OnDisconnectedFromPhoton()
	{
		Log.AddLine("[FightController::OnDisconnectedFromPhoton]");
		OnPhotonFailure();
	}

	private void OnPhotonFailure()
	{
		Log.AddLine("[FightController::OnPhotonFailure]");
		if (ConnectionStatus_0 != ConnectionStatus.ConnectClosed)
		{
			int_1++;
			if (int_1 > int_0)
			{
				Reset();
				FightScreen.FightScreen_0.SwitchToMenu(true);
			}
			else
			{
				Invoke("Connect", 3f);
			}
		}
	}

	private void OnReceivedRoomListUpdate()
	{
		Log.AddLine("[FightController::OnReceivedRoomListUpdate]");
		FightRoomsController_0.OnGetRoomList();
	}

	private void DefsCostyling()
	{
		if (ModeData_0.ModeType_0 == ModeType.DEATH_MATCH)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.TIME_BATTLE)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = true;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = false;
			Defs.bool_5 = true;
			Defs.bool_6 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = true;
		}
		if (ModeData_0.ModeType_0 == ModeType.DEADLY_GAMES)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.ARENA)
		{
			Defs.bool_2 = false;
			Defs.bool_3 = false;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = true;
		}
		if (ModeData_0.ModeType_0 == ModeType.CAMPAIGN)
		{
			Defs.bool_2 = false;
			Defs.bool_3 = false;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.TUTORIAL)
		{
			Defs.bool_2 = false;
			Defs.bool_3 = false;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = false;
		}
		if (ModeData_0.ModeType_0 == ModeType.DUEL)
		{
			Defs.bool_2 = true;
			Defs.bool_3 = true;
			Defs.bool_4 = false;
			Defs.bool_5 = false;
			Defs.bool_6 = false;
			Defs.bool_0 = false;
		}
	}
}
