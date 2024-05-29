using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.network;
using engine.unity;
using pixelgun.tutorial;

public sealed class Initializer : MonoBehaviour
{
	public GameObject tc;

	public GameObject tempCam;

	public bool isDisconnect;

	public UIButton buttonCancel;

	public UILabel descriptionLabel;

	private GameObject gameObject_0;

	private List<Vector3> list_0 = new List<Vector3>();

	private List<float> list_1 = new List<float>();

	private PauseONGuiDrawer pauseONGuiDrawer_0;

	[NonSerialized]
	public RenderTexture respawnWindowRT;

	private bool bool_0;

	private Stopwatch stopwatch_0 = new Stopwatch();

	private static Action action_0;

	[CompilerGenerated]
	private static Initializer initializer_0;

	public static Initializer Initializer_0
	{
		[CompilerGenerated]
		get
		{
			return initializer_0;
		}
		[CompilerGenerated]
		private set
		{
			initializer_0 = value;
		}
	}

	public static event Action PlayerAddedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action)Delegate.Remove(action_0, value);
		}
	}

	private void Awake()
	{
		Initializer_0 = this;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("PauseONGuiDrawer")) as GameObject;
		gameObject.transform.parent = base.transform;
		pauseONGuiDrawer_0 = gameObject.GetComponent<PauseONGuiDrawer>();
		if ((bool)pauseONGuiDrawer_0)
		{
			pauseONGuiDrawer_0.act = DoOnGUI;
		}
		GameObject gameObject2 = null;
		if (!MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0)
		{
			if (Defs.dictionary_1.ContainsKey(Application.loadedLevelName))
			{
				GlobalGameController.Int32_0 = Defs.dictionary_1[Application.loadedLevelName];
			}
			gameObject2 = Resources.Load("BackgroundMusic/BackgroundMusic_Level" + GlobalGameController.Int32_0) as GameObject;
		}
		else if (CurrentCampaignGame.Int32_0 == 0)
		{
			string path = "BackgroundMusic/" + ((!Defs.bool_0) ? "Background_Training" : "BackgroundMusic_Level0");
			gameObject2 = Resources.Load(path) as GameObject;
		}
		else
		{
			gameObject2 = Resources.Load("BackgroundMusic/BackgroundMusic_Level" + CurrentCampaignGame.Int32_0) as GameObject;
		}
		if (gameObject2 != null)
		{
			UnityEngine.Object.Instantiate(gameObject2);
		}
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.CAMPAIGN)
		{
			string[] array = Storager.GetString(Defs.String_8, string.Empty).Split('#');
			List<string> list = new List<string>();
			string[] array2 = array;
			foreach (string item in array2)
			{
				list.Add(item);
			}
			if (!list.Contains(Application.loadedLevelName))
			{
				GameObject gameObject3 = GameObject.FindGameObjectWithTag("Configurator");
				CoinConfigurator component = gameObject3.GetComponent<CoinConfigurator>();
				if (component.CoinIsPresent)
				{
					CreateCoinAtPos(component.pos);
				}
			}
		}
		WaitDisconnectFromGameServer(true);
	}

	public static GameObject CreateCoinAtPos(Vector3 vector3_0)
	{
		GameObject original = Resources.Load("coin") as GameObject;
		return UnityEngine.Object.Instantiate(original, vector3_0, Quaternion.Euler(270f, 0f, 0f)) as GameObject;
	}

	private void Start()
	{
		MonoSingleton<FightController>.Prop_0.LevelLoaded();
		Defs.bool_11 = false;
		Storager.SetInt("StartAfterDisconnect", 0);
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0)
		{
			list_0.Add(new Vector3(12f, 1f, 9f));
			list_0.Add(new Vector3(17f, 1f, -15f));
			list_0.Add(new Vector3(-42f, 1f, -10.487f));
			list_0.Add(new Vector3(0f, 1f, 19.5f));
			list_0.Add(new Vector3(-33f, 1.2f, -13f));
			list_0.Add(new Vector3(-2.67f, 1f, 2.67f));
			list_0.Add(new Vector3(0f, 1f, 0f));
			list_0.Add(new Vector3(19f, 1f, -0.8f));
			list_0.Add(new Vector3(-28.5f, 1.75f, -3.73f));
			list_0.Add(new Vector3(-2.5f, 1.75f, 0f));
			list_0.Add(new Vector3(-1.596549f, 2.5f, 2.684792f));
			list_0.Add(new Vector3(-6.611357f, 1.5f, -105.2573f));
			list_0.Add(new Vector3(-20.3f, 2f, 17.6f));
			list_0.Add(new Vector3(5f, 2.5f, 0f));
			list_0.Add(new Vector3(0f, 2.5f, 0f));
			list_0.Add(new Vector3(-7.3f, 3.6f, 6.46f));
			list_1.Add(0f);
			list_1.Add(0f);
			list_1.Add(0f);
			list_1.Add(180f);
			list_1.Add(180f);
			list_1.Add(0f);
			list_1.Add(0f);
			list_1.Add(270f);
			list_1.Add(270f);
			list_1.Add(270f);
			list_1.Add(0f);
			list_1.Add(0f);
			list_1.Add(90f);
			list_1.Add(0f);
			list_1.Add(0f);
			list_1.Add(90f);
			AddPlayer();
		}
		else
		{
			tc = UnityEngine.Object.Instantiate(tempCam, Vector3.zero, Quaternion.identity) as GameObject;
			WeaponManager.weaponManager_0.myTable = PhotonNetwork.Instantiate("NetworkTable", base.transform.position, base.transform.rotation, 0);
			WeaponManager.weaponManager_0.myNetworkStartTable = WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>();
		}
		GameObject gameObject = PhotonNetwork.Instantiate("NetworkScoreController", base.transform.position, base.transform.rotation, 0);
		WeaponManager.weaponManager_0.myScoreController = gameObject.GetComponent<PlayerScoreController>();
		stopwatch_0.Start();
		if (respawnWindowRT == null)
		{
			respawnWindowRT = new RenderTexture(700, 700, 16, RenderTextureFormat.ARGB32);
			respawnWindowRT.Create();
		}
	}

	private void AddPlayer()
	{
		gameObject_0 = Resources.Load("Player") as GameObject;
		Vector3 vector3_;
		float y;
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0)
		{
			if (Application.loadedLevelName.Equals("Arena_Underwater"))
			{
				vector3_ = new Vector3(0f, 3.5f, 0f);
				y = 0f;
			}
			else if (Application.loadedLevelName.Equals("Pizza"))
			{
				vector3_ = new Vector3(-32.48f, 2.46f, 2.01f);
				y = 90f;
			}
			else
			{
				vector3_ = new Vector3(0f, 2.5f, 0f);
				y = 0f;
			}
		}
		else
		{
			int index = Mathf.Max(0, CurrentCampaignGame.Int32_0 - 1);
			vector3_ = ((CurrentCampaignGame.Int32_0 != 0) ? list_0[index] : new Vector3(-0.72f, 1.75f, -13.23f));
			y = ((CurrentCampaignGame.Int32_0 != 0) ? list_1[index] : 0f);
		}
		GameObject gameObject = PhotonNetwork.Instantiate("Player", vector3_, Quaternion.Euler(0f, y, 0f), 0);
		NickLabelController.camera_0 = gameObject.GetComponent<SkinName>().camPlayer.GetComponent<Camera>();
		WeaponManager.weaponManager_0.myPlayer = gameObject;
		WeaponManager.weaponManager_0.myPlayerMoveC = gameObject.GetComponent<SkinName>().Player_move_c_0;
		Invoke("SetupObjectThatNeedsPlayer", 0.01f);
	}

	[Obfuscation(Exclude = true)]
	public void SetupObjectThatNeedsPlayer()
	{
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("CoinBonus");
			if (gameObject != null)
			{
				CoinBonus component = gameObject.GetComponent<CoinBonus>();
				if ((bool)component)
				{
					component.SetPlayer();
				}
			}
			if (!TutorialController.TutorialController_0.Boolean_0)
			{
				ZombieCreator.zombieCreator_0.BeganCreateEnemies();
			}
		}
		action_0();
	}

	private void ShowDescriptionLabel(string string_0)
	{
		descriptionLabel.gameObject.SetActive(true);
		descriptionLabel.String_0 = string_0;
	}

	public void HideReconnectInterface()
	{
		descriptionLabel.gameObject.SetActive(false);
		buttonCancel.gameObject.SetActive(false);
	}

	[Obfuscation(Exclude = true)]
	public void OnCancelButtonClick()
	{
		MonoSingleton<FightController>.Prop_0.Quit();
	}

	private void DoOnGUI()
	{
		if (isDisconnect)
		{
			if (bool_0)
			{
				ShowDescriptionLabel(LocalizationStore.Get("Key_1005"));
				buttonCancel.gameObject.SetActive(false);
			}
			else
			{
				ShowDescriptionLabel(LocalizationStore.Get("Key_1004"));
				buttonCancel.gameObject.SetActive(true);
			}
		}
	}

	private void Update()
	{
		if ((bool)pauseONGuiDrawer_0)
		{
			pauseONGuiDrawer_0.gameObject.SetActive(isDisconnect);
		}
	}

	private void OnDestroy()
	{
		WaitDisconnectFromGameServer(false);
		Initializer_0 = null;
		Defs.bool_9 = false;
		Defs.bool_10 = false;
		if (respawnWindowRT != null)
		{
			respawnWindowRT.Release();
			respawnWindowRT = null;
		}
		if ((bool)pauseONGuiDrawer_0)
		{
			pauseONGuiDrawer_0.act = null;
		}
		stopwatch_0.Stop();
	}

	private void WaitDisconnectFromGameServer(bool bool_1)
	{
		ConnectionStatusEvent @event = EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>();
		if (!bool_1)
		{
			if (@event.Contains(OnDisconnectFromGameServer))
			{
				@event.Unsubscribe(OnDisconnectFromGameServer, BaseConnection.ConnectionStatus.CONNECT_FAILURE);
			}
		}
		else if (!@event.Contains(OnDisconnectFromGameServer))
		{
			@event.Subscribe(OnDisconnectFromGameServer, BaseConnection.ConnectionStatus.CONNECT_FAILURE);
		}
	}

	private void OnDisconnectFromGameServer(ConnectionStatusEventArg connectionStatusEventArg_0)
	{
		Log.AddLine("[Initializer::OnDisconnectFromGameServer. Network websocket connectopn error in photon game!]: " + connectionStatusEventArg_0.string_0);
		OnCancelButtonClick();
	}

	private void OnJoinedRoom()
	{
		isDisconnect = false;
		bool_0 = false;
		HideReconnectInterface();
	}

	private void OnPhotonJoinRoomFailed()
	{
		bool_0 = true;
	}

	public void OnLeftRoom()
	{
		if (MonoSingleton<FightController>.Prop_0.ConnectionStatus_0 == FightController.ConnectionStatus.Exiting && !(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myTable == null))
		{
			WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>().Boolean_3 = false;
			WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>().Boolean_2 = false;
		}
	}

	private void OnConnectionFail(DisconnectCause disconnectCause_0)
	{
		if (ZombiManager.zombiManager_0 != null)
		{
			UnityEngine.Object.Destroy(ZombiManager.zombiManager_0.gameObject);
		}
		tc.SetActive(true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Bonus");
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("Enemy");
		for (int j = 0; j < array2.Length; j++)
		{
			UnityEngine.Object.Destroy(array2[j]);
		}
		isDisconnect = true;
	}

	public void FakeReconnect()
	{
		OnConnectionFail(DisconnectCause.Exception);
	}
}
