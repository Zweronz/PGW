using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using engine.helpers;
using engine.network;
using engine.unity;

public class PlayerScoreController : MonoBehaviour
{
	public string[] addScoreString = new string[3]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	public int sumScore;

	public float[] timerAddScoreShow = new float[3];

	public float maxTimerMessage = 2f;

	public float maxTimerSumMessage = 4f;

	public PhotonView photonView;

	private NetworkStartTable networkStartTable_0;

	private float float_0 = -1f;

	private float float_1;

	private float float_2 = 3f;

	private static float float_3 = 0.5f;

	private float float_4 = 1f;

	private static float float_5 = 1f;

	private List<string> list_0 = new List<string>();

	private float float_6;

	private float float_7 = 1.5f;

	private bool bool_0;

	private bool bool_1;

	private Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	private Dictionary<int, int> dictionary_1 = new Dictionary<int, int>();

	private ObscuredShort obscuredShort_0 = default(ObscuredShort);

	private ObscuredShort obscuredShort_1 = default(ObscuredShort);

	private ObscuredInt obscuredInt_0 = default(ObscuredInt);

	private ObscuredShort obscuredShort_2 = default(ObscuredShort);

	private ObscuredShort obscuredShort_3 = default(ObscuredShort);

	private ObscuredInt obscuredInt_1 = default(ObscuredInt);

	private ObscuredInt obscuredInt_2 = default(ObscuredInt);

	private ObscuredInt obscuredInt_3 = default(ObscuredInt);

	private ObscuredFloat obscuredFloat_0 = default(ObscuredFloat);

	private ObscuredShort obscuredShort_4 = default(ObscuredShort);

	private ObscuredShort obscuredShort_5 = default(ObscuredShort);

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private PlayerMessageQueueConroller playerMessageQueueConroller_0;

	[CompilerGenerated]
	private bool bool_2;

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_0;
		}
		[CompilerGenerated]
		set
		{
			player_move_c_0 = value;
		}
	}

	public PlayerMessageQueueConroller PlayerMessageQueueConroller_0
	{
		[CompilerGenerated]
		get
		{
			return playerMessageQueueConroller_0;
		}
		[CompilerGenerated]
		set
		{
			playerMessageQueueConroller_0 = value;
		}
	}

	private bool Boolean_0
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_9;
		}
	}

	private bool Boolean_1
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_4;
		}
	}

	private bool Boolean_2
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_7;
		}
	}

	private bool Boolean_3
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_8;
		}
	}

	private byte Byte_0
	{
		get
		{
			return (byte)((networkStartTable_0 != null) ? ((byte)networkStartTable_0.PlayerCommandController_0.Int32_1) : 0);
		}
	}

	public short Int16_0
	{
		get
		{
			return obscuredShort_0;
		}
		private set
		{
			obscuredShort_0 = value;
		}
	}

	public short Int16_1
	{
		get
		{
			return obscuredShort_1;
		}
		private set
		{
			obscuredShort_1 = value;
		}
	}

	public short Int16_2
	{
		get
		{
			return obscuredShort_2;
		}
		private set
		{
			if ((short)obscuredShort_2 != value)
			{
				bool_1 = true;
			}
			obscuredShort_2 = value;
		}
	}

	public int Int32_0
	{
		get
		{
			return obscuredInt_0;
		}
		private set
		{
			if ((int)obscuredInt_0 != value)
			{
				bool_1 = true;
			}
			obscuredInt_0 = value;
		}
	}

	public short Int16_3
	{
		get
		{
			return obscuredShort_3;
		}
		set
		{
			if ((short)obscuredShort_3 != value)
			{
				bool_1 = true;
			}
			obscuredShort_3 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return obscuredInt_1;
		}
		set
		{
			if ((int)obscuredInt_1 != value)
			{
				bool_1 = true;
			}
			obscuredInt_1 = value;
		}
	}

	public int Int32_2
	{
		get
		{
			return obscuredInt_2;
		}
		set
		{
			if ((int)obscuredInt_2 != value)
			{
				bool_1 = true;
			}
			obscuredInt_2 = value;
		}
	}

	public int Int32_3
	{
		get
		{
			return obscuredInt_3;
		}
		set
		{
			if ((int)obscuredInt_3 != value)
			{
				bool_1 = true;
			}
			obscuredInt_3 = value;
		}
	}

	public float Single_0
	{
		get
		{
			return obscuredFloat_0;
		}
		set
		{
			if ((float)obscuredFloat_0 != value)
			{
				bool_1 = true;
			}
			obscuredFloat_0 = value;
		}
	}

	public short Int16_4
	{
		get
		{
			return obscuredShort_4;
		}
		private set
		{
			obscuredShort_4 = value;
		}
	}

	public short Int16_5
	{
		get
		{
			return obscuredShort_5;
		}
		private set
		{
			obscuredShort_5 = value;
		}
	}

	public bool Boolean_4
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	private void Awake()
	{
		Boolean_4 = false;
		photonView = PhotonView.Get(this);
		PlayerMessageQueueConroller_0 = base.gameObject.GetComponent<PlayerMessageQueueConroller>();
	}

	private void Start()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
			if (gameObject.GetComponent<PhotonView>().PhotonPlayer_0 == base.transform.GetComponent<PhotonView>().PhotonPlayer_0)
			{
				networkStartTable_0 = component;
				networkStartTable_0.PlayerScoreController_0 = this;
			}
		}
		bool_0 = true;
	}

	private void Update()
	{
		if (bool_0 && (!Boolean_0 || Boolean_1))
		{
			scoreLabelsUpdate();
			scoreUpdate();
		}
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if ((bool)photonView && photonView.Boolean_1)
		{
			if (Boolean_4)
			{
				photonView.RPC("SynchFirstBloodFlagRPC", photonPlayer_0);
			}
			SynhCountKills();
			SendFlagsCount(false);
			sendScore();
		}
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer_0)
	{
		if (photonPlayer_0 != null)
		{
			int key = 0;
			try
			{
				key = (int)photonPlayer_0.Hashtable_0["uid"];
			}
			catch (Exception)
			{
			}
			if (dictionary_0.ContainsKey(key))
			{
				dictionary_0[key] = 0;
			}
			if (dictionary_1.ContainsKey(key))
			{
				dictionary_1[key] = 0;
			}
		}
	}

	public void Clear()
	{
		Int16_2 = 0;
		Int32_0 = 0;
		Int16_0 = 0;
		Int16_3 = 0;
		Int16_1 = 0;
		Int32_1 = 0;
		Int32_2 = 0;
		Int32_3 = 0;
		Single_0 = 0f;
		Int16_4 = 0;
		Int16_5 = 0;
		Boolean_4 = false;
		dictionary_0.Clear();
		dictionary_1.Clear();
		if (Boolean_1 && PlayerMessageQueueConroller_0 != null)
		{
			PlayerMessageQueueConroller_0.Clear();
		}
		bool_1 = false;
		float_4 = 5f;
	}

	public void IKill(int int_0)
	{
		if (dictionary_0.ContainsKey(int_0))
		{
			Dictionary<int, int> dictionary;
			Dictionary<int, int> dictionary2 = (dictionary = dictionary_0);
			int key;
			int key2 = (key = int_0);
			key = dictionary[key];
			dictionary2[key2] = key + 1;
		}
		else
		{
			dictionary_0.Add(int_0, 1);
		}
	}

	public void IKilled(int int_0)
	{
		if (dictionary_1.ContainsKey(int_0))
		{
			Dictionary<int, int> dictionary;
			Dictionary<int, int> dictionary2 = (dictionary = dictionary_1);
			int key;
			int key2 = (key = int_0);
			key = dictionary[key];
			dictionary2[key2] = key + 1;
		}
		else
		{
			dictionary_1.Add(int_0, 1);
		}
	}

	public int GetIKill(int int_0)
	{
		if (dictionary_0.ContainsKey(int_0))
		{
			return dictionary_0[int_0];
		}
		return 0;
	}

	public int GetIKilled(int int_0)
	{
		if (dictionary_1.ContainsKey(int_0))
		{
			return dictionary_1[int_0];
		}
		return 0;
	}

	public void AddKill()
	{
		Int16_0++;
		SynhCountKills();
		if (Boolean_2 && (Byte_0 == 1 || Byte_0 == 2))
		{
			photonView.RPC("plusCountKillsCommand", PhotonTargets.All, Byte_0);
			SendGlobalCommandParamsUpdateToServer();
		}
	}

	public void AddFlag()
	{
		Int16_1++;
		SendFlagsCount(true);
		if (Boolean_3 && (Byte_0 == 1 || Byte_0 == 2))
		{
			photonView.RPC("plusCountKillsCommand", PhotonTargets.All, Byte_0);
			SendGlobalCommandParamsUpdateToServer();
		}
		photonView.RPC("AddPaticleBazeRPC", PhotonTargets.All, Byte_0);
	}

	public void SelfKill()
	{
		if (Int16_0 > 0)
		{
			Int16_0--;
			SynhCountKills();
		}
	}

	public void AddDeath()
	{
		Int16_2++;
	}

	public void AddScoreOnEvent(KillStreakType killStreakType_0)
	{
		try
		{
			SafeAddScoreOnEvent(killStreakType_0, new Vector3(0f, 0f, 0f));
		}
		catch (Exception ex)
		{
			Log.AddLine(string.Format("[PlayerScoreController::AddScoreOnEvent] EXCEPTION!!! ERROR!!!   msg = {0} stack = {1}", ex.ToString(), ex.StackTrace));
		}
	}

	public void AddScoreOnEvent(KillStreakType killStreakType_0, Vector3 vector3_0)
	{
		try
		{
			SafeAddScoreOnEvent(killStreakType_0, vector3_0);
		}
		catch (Exception ex)
		{
			Log.AddLine(string.Format("[PlayerScoreController::AddScoreOnEvent] EXCEPTION!!! ERROR!!!   msg = {0} stack = {1}", ex.ToString(), ex.StackTrace));
		}
	}

	public void TakeGlobalParamsFromServer(int int_0, int int_1)
	{
		if (Boolean_2 || Boolean_3)
		{
			Int16_4 = (short)int_0;
			Int16_5 = (short)int_1;
		}
	}

	private void SendGlobalCommandParamsUpdateToServer()
	{
		GlobalFightParamsUpdateCommand globalFightParamsUpdateCommand = new GlobalFightParamsUpdateCommand();
		globalFightParamsUpdateCommand.string_0 = MonoSingleton<FightController>.Prop_0.String_1;
		globalFightParamsUpdateCommand.int_1 = Byte_0;
		AbstractNetworkCommand.Send(globalFightParamsUpdateCommand);
	}

	private void SafeAddScoreOnEvent(KillStreakType killStreakType_0, Vector3 vector3_0)
	{
		if (!bool_0)
		{
			return;
		}
		if (killStreakType_0 == KillStreakType.KILL_ASSIST)
		{
			Int16_3++;
		}
		if (killStreakType_0 == KillStreakType.KILL_HEADSHOT && Time.time - float_1 < 1.5f)
		{
			killStreakType_0 = KillStreakType.KILL_DOUBLE_HEADSHOT;
		}
		if ((killStreakType_0 == KillStreakType.KILL_COMMON || killStreakType_0 == KillStreakType.KILL_HEADSHOT || killStreakType_0 == KillStreakType.KILL_DOUBLE_HEADSHOT || killStreakType_0 == KillStreakType.KILL_GERENADE || killStreakType_0 == KillStreakType.KILL_TURRET || killStreakType_0 == KillStreakType.KILL_EXPLOSION) && MonoSingleton<FightController>.Prop_0.FightStatController_0.GetFirstBood() && !Boolean_4)
		{
			Boolean_4 = true;
			AddScoreOnEvent(KillStreakType.FIRST_BLOOD);
			photonView.RPC("FirstBloodRPC", PhotonTargets.AllBuffered, Player_move_c_0.mySkinName.NickName, 4);
			Player_move_c_0.PhotonView_0.RPC("ShowMultyKillRPC", PhotonTargets.Others, 1);
		}
		bool flag = true;
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			switch (killStreakType_0)
			{
			case KillStreakType.KILLSTREAK_MULTIKILL_2:
			case KillStreakType.KILLSTREAK_MULTIKILL_3:
			case KillStreakType.KILLSTREAK_MULTIKILL_4:
			case KillStreakType.KILLSTREAK_MULTIKILL_5:
			case KillStreakType.KILLSTREAK_MULTIKILL_6:
			case KillStreakType.KILLSTREAK_MULTIKILL_10:
			case KillStreakType.KILLSTREAK_MULTIKILL_20:
			case KillStreakType.KILLSTREAK_MULTIKILL_50:
			case KillStreakType.FIRST_BLOOD:
			{
				PlayerKillStrickObject playerKillStrickObject = new PlayerKillStrickObject();
				playerKillStrickObject.string_0 = PlayerEventScoreController.dictionary_3[killStreakType_0.ToString()];
				playerKillStrickObject.string_1 = PlayerEventScoreController.dictionary_2[killStreakType_0.ToString()];
				playerKillStrickObject.bool_1 = true;
				PlayerMessageQueueConroller_0.Add(playerKillStrickObject);
				flag = false;
				break;
			}
			}
		}
		if (killStreakType_0 == KillStreakType.KILL_PLAYER_WITH_FLAG || killStreakType_0 == KillStreakType.KILLSTREAK_FLAGTOUCH || killStreakType_0 == KillStreakType.KILLSTREAK_FLAGTOUCH_2 || killStreakType_0 == KillStreakType.KILLSTREAK_FLAGTOUCH_3)
		{
			DominationController.TypeDomination typeDomination = DominationController.TypeDomination.NONE;
			switch (killStreakType_0)
			{
			case KillStreakType.KILL_PLAYER_WITH_FLAG:
				typeDomination = DominationController.TypeDomination.FLAG_KILL;
				break;
			case KillStreakType.KILLSTREAK_FLAGTOUCH:
				typeDomination = DominationController.TypeDomination.TOUCHDOWN1;
				break;
			case KillStreakType.KILLSTREAK_FLAGTOUCH_2:
				typeDomination = DominationController.TypeDomination.TOUCHDOWN2;
				break;
			case KillStreakType.KILLSTREAK_FLAGTOUCH_3:
				typeDomination = DominationController.TypeDomination.TOUCHDOWN3;
				break;
			}
			photonView.RPC("FirstBloodRPC", PhotonTargets.AllBuffered, Player_move_c_0.mySkinName.NickName, (int)typeDomination);
		}
		int num = PlayerEventScoreController.dictionary_0[killStreakType_0.ToString()];
		if (num != 0)
		{
			Int32_0 += num;
			string text = PlayerEventScoreController.dictionary_1[killStreakType_0.ToString()];
			if (!string.IsNullOrEmpty(text))
			{
				AddScoreMessage("+" + num + " " + Localizer.Get(text), num);
			}
			string text2 = PlayerEventScoreController.dictionary_2[killStreakType_0.ToString()];
			if (!string.IsNullOrEmpty(text2) && flag)
			{
				PlayerKillStrickObject playerKillStrickObject2 = new PlayerKillStrickObject();
				playerKillStrickObject2.string_0 = text2 + "_badge";
				playerKillStrickObject2.string_1 = text2;
				playerKillStrickObject2.bool_1 = false;
				PlayerMessageQueueConroller_0.Add(playerKillStrickObject2);
			}
			createKillSteakBonus(killStreakType_0, vector3_0);
		}
	}

	private void scoreUpdate()
	{
		float_2 -= Time.deltaTime;
		if (!(float_2 > 0f) && bool_1)
		{
			float_2 = float_3;
			sendScore();
		}
	}

	private void sendScore()
	{
		UserScoreAndStatRPCData userScoreAndStatRPCData = new UserScoreAndStatRPCData();
		userScoreAndStatRPCData.int_0 = Int32_1;
		userScoreAndStatRPCData.int_1 = Int32_2;
		userScoreAndStatRPCData.int_2 = Int32_3;
		userScoreAndStatRPCData.float_0 = Single_0;
		userScoreAndStatRPCData.short_0 = Int16_3;
		userScoreAndStatRPCData.int_3 = Int32_0;
		userScoreAndStatRPCData.short_1 = Int16_2;
		photonView.RPC("SynchNewScoreRPC", PhotonTargets.Others, userScoreAndStatRPCData);
		bool_1 = false;
	}

	private void scoreLabelsUpdate()
	{
		WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
		if (!(weaponManager_ == null) && !(weaponManager_.myPlayerMoveC == null) && !weaponManager_.myPlayerMoveC.Boolean_18)
		{
			if (timerAddScoreShow[2] > 0f)
			{
				timerAddScoreShow[2] -= Time.deltaTime;
			}
			if (timerAddScoreShow[1] > 0f)
			{
				timerAddScoreShow[1] -= Time.deltaTime;
			}
			if (timerAddScoreShow[0] > 0f)
			{
				timerAddScoreShow[0] -= Time.deltaTime;
			}
		}
	}

	private void AddScoreMessage(string string_0, int int_0)
	{
		addScoreString[2] = addScoreString[1];
		addScoreString[1] = string_0;
		if (timerAddScoreShow[0] > 0f)
		{
			sumScore += int_0;
		}
		else
		{
			sumScore = int_0;
		}
		addScoreString[0] = sumScore.ToString();
		timerAddScoreShow[2] = timerAddScoreShow[1];
		timerAddScoreShow[1] = maxTimerMessage;
		timerAddScoreShow[0] = maxTimerSumMessage;
	}

	private void SynhCountKills()
	{
		photonView.RPC("SynhCountKillsRPC", PhotonTargets.Others, Int16_0);
	}

	private void SendFlagsCount(bool bool_3)
	{
		if (bool_3)
		{
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnFlagCaptured(UsersData.UsersData_0.UserData_0.user_0.int_0);
		}
		photonView.RPC("SendFlagsCountRPC", PhotonTargets.Others, Int16_1, bool_3);
	}

	private IEnumerator SynchFirstBloodFlagCorutine()
	{
		while (WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myScoreController == null)
		{
			yield return new WaitForEndOfFrame();
		}
		SynchFirstBloodFlag();
	}

	private void SynchFirstBloodFlag()
	{
		WeaponManager.weaponManager_0.myScoreController.Boolean_4 = true;
	}

	private void createKillSteakBonus(KillStreakType killStreakType_0, Vector3 vector3_0)
	{
		MapBonusItemData mapBonusItemData = null;
		if (killStreakType_0 >= KillStreakType.KILLSTREAK_MULTIKILL_2 && killStreakType_0 <= KillStreakType.KILLSTREAK_MULTIKILL_50)
		{
			mapBonusItemData = KillStreakStorage.Get.GetBonusKillStreak(killStreakType_0);
		}
		if (mapBonusItemData != null && BonusMapController.bonusMapController_0 != null && CommonParamsSettings.Get.BonusAfterKillPlayerProb > UnityEngine.Random.Range(0, 100))
		{
			Vector3 vector3_ = new Vector3(vector3_0.x, vector3_0.y - 0.5f, vector3_0.z);
			BonusMapController.bonusMapController_0.AddBonus(vector3_, mapBonusItemData.Int32_0);
		}
	}

	[RPC]
	private void SynhScoreRPC(int int_0)
	{
		Int32_0 = int_0;
	}

	[RPC]
	private void SynhCountKillsRPC(short short_0)
	{
		Int16_0 = short_0;
	}

	[RPC]
	private void SendFlagsCountRPC(short short_0, bool bool_3)
	{
		Int16_1 = short_0;
		if (bool_3)
		{
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnFlagCaptured((int)photonView.PhotonPlayer_0.Hashtable_0["uid"]);
		}
	}

	[RPC]
	private void AddPaticleBazeRPC(byte byte_0)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("BazaZoneCommand" + byte_0);
		UnityEngine.Object.Instantiate(Resources.Load((byte_0 != 1) ? "Ring_Particle_Red" : "Ring_Particle_Blue"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.22f, gameObject.transform.position.z), gameObject.transform.rotation);
	}

	[RPC]
	private void plusCountKillsCommand(byte byte_0)
	{
		if (byte_0 == 1 && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myScoreController != null)
		{
			WeaponManager.weaponManager_0.myScoreController.Int16_4++;
		}
		if (byte_0 == 2 && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myScoreController != null)
		{
			WeaponManager.weaponManager_0.myScoreController.Int16_5++;
		}
	}

	[RPC]
	private void SynchFirstBloodFlagRPC()
	{
		StartCoroutine(SynchFirstBloodFlagCorutine());
	}

	[RPC]
	private void SynchNewScoreRPC(UserScoreAndStatRPCData userScoreAndStatRPCData_0)
	{
		Int32_1 = userScoreAndStatRPCData_0.int_0;
		Int32_2 = userScoreAndStatRPCData_0.int_1;
		Int32_3 = userScoreAndStatRPCData_0.int_2;
		Single_0 = userScoreAndStatRPCData_0.float_0;
		Int16_3 = userScoreAndStatRPCData_0.short_0;
		Int32_0 = userScoreAndStatRPCData_0.int_3;
		Int16_2 = userScoreAndStatRPCData_0.short_1;
	}

	[RPC]
	public void FirstBloodRPC(string string_0, int int_0)
	{
		Boolean_4 = true;
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myScoreController != null)
		{
			WeaponManager.weaponManager_0.myScoreController.Boolean_4 = true;
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			Color color_ = Color.white;
			Color color_2 = Color.white;
			DominationController.GetDominationColor(string_0, out color_2, out color_);
			HeadUpDisplay.HeadUpDisplay_0.dominationController.SetItem(string_0, color_2, string.Empty, color_, (DominationController.TypeDomination)int_0);
		}
	}
}
