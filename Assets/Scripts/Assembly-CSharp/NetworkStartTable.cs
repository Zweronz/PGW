using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using engine.controllers;
using engine.helpers;
using engine.unity;

public sealed class NetworkStartTable : MonoBehaviour
{
	public GameObject guiObj;

	public GameObject tempCam;

	public GameObject zombieManagerPrefab;

	public Texture2D serverLeftTheGame;

	public GUIStyle labelStyle;

	public GUIStyle messagesStyle;

	public GUIStyle ozidanieStyle;

	private HeadUpDisplay headUpDisplay_0;

	private WeaponManager weaponManager_0;

	private Player_move_c player_move_c_0;

	private GameObject gameObject_0;

	private GameObject gameObject_1;

	private GameObject gameObject_2;

	private GameObject gameObject_3;

	private GameObject[] gameObject_4;

	private GameObject gameObject_5;

	private ObscuredInt obscuredInt_0 = default(ObscuredInt);

	private int int_0;

	private int int_1;

	private bool bool_0;

	private float float_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private float float_1;

	private float float_2 = (float)Screen.height / 768f;

	private bool bool_4;

	private bool bool_5;

	private bool bool_6;

	private bool bool_7;

	private int int_2;

	private string string_0;

	private string string_1;

	private string string_2;

	[CompilerGenerated]
	private Player_move_c player_move_c_1;

	[CompilerGenerated]
	private PhotonView photonView_0;

	[CompilerGenerated]
	private PlayerScoreController playerScoreController_0;

	[CompilerGenerated]
	private PlayerCommandController playerCommandController_0;

	[CompilerGenerated]
	private bool bool_8;

	[CompilerGenerated]
	private bool bool_9;

	[CompilerGenerated]
	private string string_3;

	[CompilerGenerated]
	private string string_4;

	[CompilerGenerated]
	private Texture texture_0;

	[CompilerGenerated]
	private Texture texture_1;

	[CompilerGenerated]
	private Texture texture_2;

	[CompilerGenerated]
	private string string_5;

	[CompilerGenerated]
	private string string_6;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private string string_7;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private string string_8;

	[CompilerGenerated]
	private string string_9;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private bool bool_10;

	[CompilerGenerated]
	private bool bool_11;

	[CompilerGenerated]
	private bool bool_12;

	[CompilerGenerated]
	private bool bool_13;

	[CompilerGenerated]
	private bool bool_14;

	[CompilerGenerated]
	private bool bool_15;

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_1;
		}
		[CompilerGenerated]
		set
		{
			player_move_c_1 = value;
		}
	}

	public PhotonView PhotonView_0
	{
		[CompilerGenerated]
		get
		{
			return photonView_0;
		}
		[CompilerGenerated]
		private set
		{
			photonView_0 = value;
		}
	}

	public PlayerScoreController PlayerScoreController_0
	{
		[CompilerGenerated]
		get
		{
			return playerScoreController_0;
		}
		[CompilerGenerated]
		set
		{
			playerScoreController_0 = value;
		}
	}

	public PlayerCommandController PlayerCommandController_0
	{
		[CompilerGenerated]
		get
		{
			return playerCommandController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerCommandController_0 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_8;
		}
		[CompilerGenerated]
		set
		{
			bool_8 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_9;
		}
		[CompilerGenerated]
		set
		{
			bool_9 = value;
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
		set
		{
			string_3 = value;
		}
	}

	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_4;
		}
		[CompilerGenerated]
		set
		{
			string_4 = value;
		}
	}

	public Texture Texture_0
	{
		[CompilerGenerated]
		get
		{
			return texture_0;
		}
		[CompilerGenerated]
		set
		{
			texture_0 = value;
		}
	}

	public Texture Texture_1
	{
		[CompilerGenerated]
		get
		{
			return texture_1;
		}
		[CompilerGenerated]
		private set
		{
			texture_1 = value;
		}
	}

	public Texture Texture_2
	{
		[CompilerGenerated]
		get
		{
			return texture_2;
		}
		[CompilerGenerated]
		private set
		{
			texture_2 = value;
		}
	}

	public string String_2
	{
		[CompilerGenerated]
		get
		{
			return string_5;
		}
		[CompilerGenerated]
		private set
		{
			string_5 = value;
		}
	}

	public string String_3
	{
		[CompilerGenerated]
		get
		{
			return string_6;
		}
		[CompilerGenerated]
		private set
		{
			string_6 = value;
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

	public string String_4
	{
		[CompilerGenerated]
		get
		{
			return string_7;
		}
		[CompilerGenerated]
		private set
		{
			string_7 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		private set
		{
			int_4 = value;
		}
	}

	public string String_5
	{
		[CompilerGenerated]
		get
		{
			return string_8;
		}
		[CompilerGenerated]
		private set
		{
			string_8 = value;
		}
	}

	public string String_6
	{
		[CompilerGenerated]
		get
		{
			return string_9;
		}
		[CompilerGenerated]
		private set
		{
			string_9 = value;
		}
	}

	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		private set
		{
			int_5 = value;
		}
	}

	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		private set
		{
			int_6 = value;
		}
	}

	public int Int32_4
	{
		get
		{
			if (PlayerScoreController_0 != null)
			{
				return PlayerScoreController_0.Int32_0;
			}
			return 0;
		}
	}

	public int Int32_5
	{
		get
		{
			if (PlayerScoreController_0 != null)
			{
				return PlayerScoreController_0.Int16_2;
			}
			return 0;
		}
	}

	public int Int32_6
	{
		get
		{
			if (PlayerScoreController_0 != null)
			{
				return PlayerScoreController_0.Int16_3;
			}
			return 0;
		}
	}

	public int Int32_7
	{
		get
		{
			if (PlayerScoreController_0 != null)
			{
				return PlayerScoreController_0.Int16_0;
			}
			return 0;
		}
	}

	public int Int32_8
	{
		get
		{
			if (PlayerScoreController_0 != null)
			{
				return PlayerScoreController_0.Int16_1;
			}
			return 0;
		}
	}

	public int Int32_9
	{
		get
		{
			return obscuredInt_0;
		}
		set
		{
			obscuredInt_0 = value;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_4;
		}
		set
		{
			bool_4 = value;
			if (Boolean_4)
			{
				Defs.bool_9 = value;
			}
		}
	}

	public bool Boolean_3
	{
		get
		{
			return bool_5;
		}
		set
		{
			bool_5 = value;
			if (Boolean_4)
			{
				Defs.bool_10 = value;
			}
		}
	}

	public bool Boolean_4
	{
		[CompilerGenerated]
		get
		{
			return bool_10;
		}
		[CompilerGenerated]
		private set
		{
			bool_10 = value;
		}
	}

	public bool Boolean_5
	{
		[CompilerGenerated]
		get
		{
			return bool_11;
		}
		[CompilerGenerated]
		private set
		{
			bool_11 = value;
		}
	}

	public bool Boolean_6
	{
		[CompilerGenerated]
		get
		{
			return bool_12;
		}
		[CompilerGenerated]
		private set
		{
			bool_12 = value;
		}
	}

	public bool Boolean_7
	{
		[CompilerGenerated]
		get
		{
			return bool_13;
		}
		[CompilerGenerated]
		private set
		{
			bool_13 = value;
		}
	}

	public bool Boolean_8
	{
		[CompilerGenerated]
		get
		{
			return bool_14;
		}
		[CompilerGenerated]
		private set
		{
			bool_14 = value;
		}
	}

	public bool Boolean_9
	{
		[CompilerGenerated]
		get
		{
			return bool_15;
		}
		[CompilerGenerated]
		private set
		{
			bool_15 = value;
		}
	}

	public NetworkStartTable()
	{
		String_2 = string.Empty;
		String_3 = string.Empty;
		Int32_0 = 0;
		String_5 = "Player";
		String_6 = "-1";
	}

	private void Awake()
	{
		PhotonView_0 = PhotonView.Get(this);
		Boolean_5 = Defs.bool_4;
		Boolean_6 = PhotonNetwork.Boolean_9;
		Boolean_9 = Defs.bool_2;
		Boolean_7 = Defs.bool_5;
		Boolean_8 = Defs.bool_6;
		if (Boolean_9)
		{
			Boolean_4 = PhotonView_0.Boolean_1;
		}
		else
		{
			Boolean_4 = true;
		}
		PlayerCommandController_0 = new PlayerCommandController(this);
	}

	public void CreatePlayerInFight()
	{
		if (Boolean_9 && Boolean_4)
		{
			StartCoroutine(CreatePlayer());
		}
	}

	private IEnumerator CreatePlayer()
	{
		yield return null;
		PlayerCommandController_0.AutoBalanceCommand();
		MenuBackgroundMusic.menuBackgroundMusic_0.Stop();
		GameStatHelper.GameStatHelper_0.BattleStart();
		UserController.UserController_0.ClearActiveSlots();
		Boolean_3 = false;
		StartCoroutine(startPlayer());
	}

	public IEnumerator startPlayer()
	{
		CancelInvoke("DestroyMyPlayer");
		DestroyMyPlayer();
		while (PhotonNetwork.Double_0 <= 0.0)
		{
			yield return new WaitForEndOfFrame();
		}
		int userLevel = UserController.UserController_0.GetUserLevel();
		if (Int32_9 != userLevel)
		{
			SetRanks();
		}
		GameObject gameObject = Resources.Load("Player") as GameObject;
		gameObject_4 = null;
		if (Boolean_5)
		{
			gameObject_4 = GameObject.FindGameObjectsWithTag("MultyPlayerCreateZoneCOOP");
		}
		else if (Boolean_7)
		{
			gameObject_4 = GameObject.FindGameObjectsWithTag("MultyPlayerCreateZoneCommand" + PlayerCommandController_0.Int32_1);
		}
		else if (Boolean_8)
		{
			gameObject_4 = GameObject.FindGameObjectsWithTag("MultyPlayerCreateZoneFlagCommand" + PlayerCommandController_0.Int32_1);
		}
		else
		{
			gameObject_4 = GameObject.FindGameObjectsWithTag("MultyPlayerCreateZone");
		}
		GameObject gameObject2 = gameObject_4[UnityEngine.Random.Range(0, gameObject_4.Length)];
		BoxCollider component = gameObject2.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject2.transform.localScale.x, component.size.z * gameObject2.transform.localScale.z);
		Rect rect = new Rect(gameObject2.transform.position.x - vector.x / 2f, gameObject2.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 vector3_ = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), gameObject2.transform.position.y, rect.y + UnityEngine.Random.Range(0f, rect.height));
		Quaternion rotation = gameObject2.transform.rotation;
		GameObject gameObject3 = PhotonNetwork.Instantiate("Player", vector3_, rotation, 0);
		NickLabelController.camera_0 = gameObject3.GetComponent<SkinName>().camPlayer.GetComponent<Camera>();
		weaponManager_0.myPlayer = gameObject3;
		weaponManager_0.myPlayerMoveC = gameObject3.GetComponent<SkinName>().Player_move_c_0;
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().SetupObjectThatNeedsPlayer();
		Boolean_2 = false;
		AppStateController.AppStateController_0.SetState(AppStateController.States.IN_BATTLE);
	}

	public void addZombiManager()
	{
		int num = PhotonNetwork.AllocateViewID();
		PhotonView_0.RPC("addZombiManagerRPC", PhotonTargets.All, base.transform.position, base.transform.rotation, num);
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (!(PhotonView_0 == null) && PhotonView_0.Boolean_1)
		{
			PlayerCommandController_0.SynchCommand();
		}
	}

	public void SetNewNick()
	{
		String_5 = Defs.GetPlayerNameOrDefault();
		PhotonNetwork.String_2 = String_5;
		PhotonView_0.RPC("SynhNickNameRPC", PhotonTargets.OthersBuffered, String_5);
	}

	public void SetRanks()
	{
		Int32_9 = UserController.UserController_0.GetUserLevel();
		PhotonView_0.RPC("SynhRanksRPC", PhotonTargets.OthersBuffered, Int32_9);
	}

	private void SendSyncPing()
	{
		Int32_1 = PhotonNetwork.GetPing();
		PhotonView_0.RPC("SendSyncPingRPC", PhotonTargets.Others, Int32_1);
	}

	private void Start()
	{
		string_0 = LocalizationStore.String_33;
		string_1 = LocalizationStore.String_34;
		string_2 = LocalizationStore.String_35;
		try
		{
			StartUnsafe();
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	private void StartUnsafe()
	{
		if (Boolean_4)
		{
			gameObject_5 = GameObject.FindGameObjectWithTag("CamTemp");
			gameObject_5.SetActive(false);
		}
		if (Boolean_5 && Boolean_4 && Boolean_6)
		{
			Debug.Log("isCOOP && isMine && isServer");
			addZombiManager();
		}
		if (Defs.bool_6 && Boolean_4)
		{
			if (Boolean_6)
			{
				AddFlag();
			}
			NGUITools.SetActive(ScreenController.ScreenController_0.LoadUI("FlagIndicators").gameObject, true);
		}
		weaponManager_0 = WeaponManager.weaponManager_0;
		if (Boolean_9 && Boolean_4)
		{
			CreatePlayerInFight();
			NickLabelController.camera_0 = GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().tc.GetComponent<Camera>();
			string text = FilterBadWorld.FilterString(Defs.GetPlayerNameOrDefault());
			String_5 = text;
			PhotonView_0.RPC("SetPixelBookID", PhotonTargets.OthersBuffered, String_6);
			SetNewNick();
			SetRanks();
			sendMySkin();
			SendMyClan();
			SendMyUserInfo();
		}
		else
		{
			Boolean_2 = false;
		}
	}

	public void sendMySkin()
	{
		Texture2D texture2D_ = SkinsController.Texture2D_0;
		byte[] inArray = texture2D_.EncodeToPNG();
		string text = Convert.ToBase64String(inArray);
		PhotonView_0.RPC("setMySkin", PhotonTargets.AllBuffered, text);
	}

	public void SendMyClan()
	{
		UserClanData userClanData_ = ClanController.ClanController_0.UserClanData_0;
		if (userClanData_ != null)
		{
			string empty = string.Empty;
			empty = ((userClanData_.byte_0 == null) ? string.Empty : Convert.ToBase64String(userClanData_.byte_0));
			PhotonView_0.RPC("SetMyClanTexture", PhotonTargets.AllBuffered, empty, userClanData_.string_0, userClanData_.string_2, userClanData_.int_0, userClanData_.string_1);
		}
	}

	public void SendMyUserInfo()
	{
		if (RankController.RankController_0.Boolean_0)
		{
			PhotonView_0.RPC("SyncUserInfoRPC", PhotonTargets.AllBuffered, RankController.RankController_0.Int32_0, RankController.RankController_0.Int32_1);
		}
	}

	private void Update()
	{
		if (!Boolean_4)
		{
			return;
		}
		if (float_1 <= Time.time)
		{
			SendSyncPing();
			float_1 = Time.time + 1f;
		}
		bool flag = Boolean_3 || bool_6 || bool_7 || Boolean_2;
		if (guiObj.activeSelf != flag)
		{
			guiObj.SetActive(flag);
		}
		headUpDisplay_0 = HeadUpDisplay.HeadUpDisplay_0;
		if (!(headUpDisplay_0 != null))
		{
			return;
		}
		if (MonoSingleton<FightController>.Prop_0.FightTimeController_0.Boolean_0)
		{
			if (!headUpDisplay_0.message_draw.activeSelf)
			{
				headUpDisplay_0.message_draw.SetActive(true);
				headUpDisplay_0.HideArenaTimeAttention();
			}
		}
		else if (headUpDisplay_0.message_draw.activeSelf)
		{
			headUpDisplay_0.message_draw.SetActive(false);
		}
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer_0)
	{
		NickLabelController[] lables = NickLabelStack.nickLabelStack_0.lables;
		NickLabelController[] array = lables;
		foreach (NickLabelController nickLabelController in array)
		{
			string value = ((!nickLabelController.Player_move_c_0 || !nickLabelController.Player_move_c_0.mySkinName) ? string.Empty : nickLabelController.Player_move_c_0.mySkinName.NickName);
			if (photonPlayer_0.String_0.Equals(value))
			{
				nickLabelController.Transform_0 = null;
				nickLabelController.Color_0 = Color.white;
				nickLabelController.Boolean_2 = false;
			}
		}
	}

	public void BattleOver(int int_7 = 0)
	{
		Log.AddLineFormat("[NetworkStartTable::BattleOver] _commandWin={0}  modeId={1}  modeType={2}  FightId={3}", int_7.ToString(), MonoSingleton<FightController>.Prop_0.ModeData_0.Int32_0, MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0.ToString(), MonoSingleton<FightController>.Prop_0.String_1);
		GameStatHelper.GameStatHelper_0.BattleStop();
		MonoSingleton<FightController>.Prop_0.FightStatController_0.FixMaxKills((int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"]);
		BattleOverWindowParams fightRewards = BattleOverRewardCntroller.GetFightRewards();
		PlayerCommandController_0.ResetCommand(true);
		NickLabelController.camera_0 = GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().tc.GetComponent<Camera>();
		if (Boolean_8 && PhotonNetwork.Boolean_9)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Flag1");
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("Flag2");
			if (gameObject != null)
			{
				FlagController component = gameObject.GetComponent<FlagController>();
				if (component != null)
				{
					component.GoBaza();
				}
			}
			if (gameObject2 != null)
			{
				FlagController component2 = gameObject2.GetComponent<FlagController>();
				if (component2 != null)
				{
					component2.GoBaza();
				}
			}
		}
		if ((bool)weaponManager_0 && (bool)weaponManager_0.myPlayer)
		{
			if ((bool)weaponManager_0.myPlayerMoveC && weaponManager_0.myPlayerMoveC.Boolean_7 && weaponManager_0.myPlayerMoveC.Boolean_15)
			{
				weaponManager_0.myPlayerMoveC.FlagController_3.GoBaza();
			}
			if (weaponManager_0.myPlayerMoveC != null && weaponManager_0.myPlayerMoveC.gunCamera != null)
			{
				weaponManager_0.myPlayerMoveC.gunCamera.gameObject.SetActive(false);
			}
			DestroyMyPlayer();
		}
		if (Boolean_5)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i]);
			}
		}
		GameObject gameObject3 = GameObject.FindGameObjectWithTag("DamageFrame");
		if (gameObject3 != null)
		{
			UnityEngine.Object.Destroy(gameObject3);
		}
		if (gameObject_5 != null)
		{
			gameObject_5.SetActive(true);
			gameObject_5.GetComponent<RPG_Camera>().enabled = false;
		}
		Boolean_0 = false;
		Boolean_2 = false;
		Boolean_3 = true;
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("NetworkScoreController");
		for (int j = 0; j < array2.Length; j++)
		{
			PlayerScoreController component3 = array2[j].GetComponent<PlayerScoreController>();
			if (component3 != null)
			{
				component3.Clear();
			}
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.Hide();
		}
		if (KillCamWindow.KillCamWindow_0 != null)
		{
			KillCamWindow.KillCamWindow_0.Hide();
		}
		if (BattleStatWindow.BattleStatWindow_0 != null)
		{
			BattleStatWindow.BattleStatWindow_0.HideMe();
		}
		BattleOverWindow.Show(fightRewards);
		Player_move_c.SetBlockKeyboardControl(true, true);
		AppStateController.AppStateController_0.SetState(AppStateController.States.IN_BATTLE_OVER_WINDOW);
	}

	[Obfuscation(Exclude = true)]
	private void DestroyMyPlayer()
	{
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayer != null)
		{
			PhotonNetwork.Destroy(weaponManager_0.myPlayer);
		}
	}

	private void AddFlag()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("BazaZoneCommand1");
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("BazaZoneCommand2");
		PhotonNetwork.InstantiateSceneObject("Prefabs/Fight/Flags/Flag1", gameObject.transform.position, gameObject.transform.rotation, 0, null);
		PhotonNetwork.InstantiateSceneObject("Prefabs/Fight/Flags/Flag2", gameObject2.transform.position, gameObject2.transform.rotation, 0, null);
	}

	private void OnDestroy()
	{
		if (Boolean_4 && FlagIndicators.FlagIndicators_0 != null)
		{
			UnityEngine.Object.Destroy(FlagIndicators.FlagIndicators_0.gameObject);
		}
	}

	public void ForceFinalBattle()
	{
		PhotonNetwork.Room_0.Boolean_4 = false;
		PhotonNetwork.Room_0.Boolean_5 = false;
		MonoSingleton<FightController>.Prop_0.FightTimeController_0.SendPingCommand(true);
	}

	public void Kick(string string_10)
	{
		int result = 0;
		if (int.TryParse(string_10, out result))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
				int num = (int)component.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"];
				if (num == result)
				{
					PhotonView_0.RPC("KickByAdminRPC", component.PhotonView_0.PhotonPlayer_0);
					return;
				}
			}
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("NetworkTable");
		GameObject[] array4 = array3;
		int num2 = 0;
		NetworkStartTable component2;
		while (true)
		{
			if (num2 < array4.Length)
			{
				GameObject gameObject2 = array4[num2];
				component2 = gameObject2.GetComponent<NetworkStartTable>();
				if (string.Equals(component2.String_5, string_10))
				{
					break;
				}
				num2++;
				continue;
			}
			return;
		}
		PhotonView_0.RPC("KickByAdminRPC", component2.PhotonView_0.PhotonPlayer_0);
	}

	public void KickAll()
	{
		PhotonView_0.RPC("KickByAdminRPC", PhotonTargets.Others);
	}

	public void SendSystemMessegeFromFlagReturned(bool bool_16)
	{
		PhotonView_0.RPC("SendSystemMessegeFromFlagReturnedRPC", PhotonTargets.All, bool_16);
	}

	[RPC]
	public void KickByAdminRPC()
	{
		Log.AddLine("[NetworkStartTable::KickByAdminRPC] I was kicked by admin", Log.LogLevel.WARNING);
		MonoSingleton<FightController>.Prop_0.LeaveRoom();
	}

	[RPC]
	private void SetPixelBookID(string string_10)
	{
		String_6 = string_10;
	}

	[RPC]
	private void addZombiManagerRPC(Vector3 vector3_0, Quaternion quaternion_0, int int_7)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("ZombiCreator");
		if (gameObject == null)
		{
			gameObject = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, vector3_0, quaternion_0);
		}
		PhotonView component = gameObject.GetComponent<PhotonView>();
		component.Int32_1 = int_7;
	}

	[RPC]
	private void addZombiManagerNewClientRPC(int int_7, Vector3 vector3_0, Quaternion quaternion_0, int int_8)
	{
		if (int_7 == PhotonNetwork.PhotonPlayer_0.Int32_0)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("ZombiCreator");
			if (!(gameObject != null) || int_8 != gameObject.GetComponent<PhotonView>().Int32_1)
			{
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, vector3_0, quaternion_0);
				PhotonView component = gameObject2.GetComponent<PhotonView>();
				component.Int32_1 = int_8;
			}
		}
	}

	[RPC]
	private void SynhNickNameRPC(string string_10)
	{
		String_5 = string_10;
	}

	[RPC]
	private void SynhRanksRPC(int int_7)
	{
		Int32_9 = int_7;
	}

	[RPC]
	private void SynhCommandRPC(int int_7, int int_8)
	{
		PlayerCommandController_0.SynchCommand(int_7, int_8);
	}

	[RPC]
	private void SendSyncPingRPC(int int_7)
	{
		Int32_1 = int_7;
	}

	[RPC]
	private void SetMyClanTexture(string string_10, string string_11, string string_12, int int_7, string string_13)
	{
		try
		{
			if (string.IsNullOrEmpty(string_10))
			{
				Texture_2 = null;
			}
			else
			{
				byte[] byte_ = Convert.FromBase64String(string_10);
				Texture_2 = Utility.TextureFromData(byte_, Defs.int_16, Defs.int_16);
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		String_2 = string_11;
		String_3 = string_12;
		Int32_0 = int_7;
		String_4 = string_13;
	}

	[RPC]
	private void SyncUserInfoRPC(int int_7, int int_8)
	{
		Int32_2 = int_7;
		Int32_3 = int_8;
	}

	[RPC]
	private void setMySkin(string string_10)
	{
		if (base.transform.GetComponent<PhotonView>() == null)
		{
			return;
		}
		Texture2D texture2D = SkinsController.TextureFromString(string_10);
		Texture_1 = texture2D;
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		if (array == null)
		{
			return;
		}
		GameObject[] array2 = array;
		int num = 0;
		GameObject gameObject;
		while (true)
		{
			if (num >= array2.Length)
			{
				return;
			}
			gameObject = array2[num];
			if (!(gameObject == null) && (bool)gameObject && !(base.transform == null) && (bool)base.transform)
			{
				PhotonView component = gameObject.GetComponent<PhotonView>();
				PhotonView component2 = base.transform.GetComponent<PhotonView>();
				if (!(component == null) && !(component2 == null) && component.PhotonPlayer_0 != null && component2.PhotonPlayer_0 != null && component.PhotonPlayer_0.Equals(component2.PhotonPlayer_0))
				{
					break;
				}
			}
			num++;
		}
		gameObject.GetComponent<Player_move_c>().setMyTable(base.gameObject);
	}

	[RPC]
	private void ZombiManagerZamenaIdRPC(int int_7)
	{
		if (ZombiManager.zombiManager_0 == null)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, base.transform.position, base.transform.rotation);
			PhotonView component = gameObject.GetComponent<PhotonView>();
			component.Int32_1 = int_7;
		}
		else
		{
			ZombiManager.zombiManager_0.photonView.Int32_1 = int_7;
		}
	}

	[RPC]
	private void ZombiZamenaIdRPC(int int_7, int int_8)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] array2 = array;
		int num = 0;
		GameObject gameObject;
		while (true)
		{
			if (num < array2.Length)
			{
				gameObject = array2[num];
				if (gameObject.GetComponent<PhotonView>().Int32_1 == int_7)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		gameObject.GetComponent<PhotonView>().Int32_1 = int_8;
	}

	[RPC]
	private void zaprosZombiManager()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("ZombiCreator");
		if (gameObject != null && gameObject.GetComponent<PhotonView>().PhotonPlayer_0 != null && PhotonView_0 != null)
		{
			PhotonView_0.RPC("otvetNAzaprosZombiManager", PhotonTargets.MasterClient, gameObject.GetComponent<PhotonView>().Int32_1);
		}
	}

	[RPC]
	private void otvetNAzaprosZombiManager(int int_7)
	{
		if (!bool_2)
		{
			bool_2 = false;
			addZombiManagerRPC(base.transform.position, base.transform.rotation, int_7);
		}
	}

	[RPC]
	public void SendSystemMessegeFromFlagReturnedRPC(bool bool_16)
	{
		if (!(WeaponManager.weaponManager_0.myPlayer == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			int int32_ = WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2;
			if ((bool_16 && int32_ == 1) || (!bool_16 && int32_ == 2))
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(Localizer.Get("ui.msg.my_command_flag_returned"));
			}
			else if ((bool_16 && int32_ == 2) || (!bool_16 && int32_ == 1))
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(Localizer.Get("ui.msg.enemy_command_flag_returned"));
			}
		}
	}
}
