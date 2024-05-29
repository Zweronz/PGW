using System.Runtime.CompilerServices;
using UnityEngine;

public class FlagController : MonoBehaviour
{
	private static readonly string string_0 = "blue_base";

	private static readonly string string_1 = "red_base";

	private static readonly string string_2 = "blue_flag";

	private static readonly string string_3 = "red_flag";

	public bool isBlue;

	public float timerToBaza = 30f;

	public Transform targetTrasform;

	public GameObject pointObjTexture;

	public GameObject flagBlue;

	public GameObject flagRed;

	public GameObject rayBlue;

	public GameObject rayRed;

	public GameObject bazaBlue;

	public GameObject bazaRed;

	private bool bool_0;

	private PhotonView photonView_0;

	public int idCapturePlayer;

	private GameObject gameObject_0;

	private float float_0 = 30f;

	private FlagIndicator flagIndicator_0;

	private FlagIndicator flagIndicator_1;

	private UISprite uisprite_0;

	private UISprite uisprite_1;

	private GameObject gameObject_1;

	private GameObject gameObject_2;

	[CompilerGenerated]
	private GameObject gameObject_3;

	[CompilerGenerated]
	private GameObject gameObject_4;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	public GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_3;
		}
		[CompilerGenerated]
		set
		{
			gameObject_3 = value;
		}
	}

	public GameObject GameObject_1
	{
		[CompilerGenerated]
		get
		{
			return gameObject_4;
		}
		[CompilerGenerated]
		set
		{
			gameObject_4 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public bool Boolean_1
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
		photonView_0 = GetComponent<PhotonView>();
		Boolean_0 = false;
		Boolean_1 = true;
		if (isBlue)
		{
			gameObject_0 = GameObject.FindGameObjectWithTag("BazaZoneCommand1");
		}
		else
		{
			gameObject_0 = GameObject.FindGameObjectWithTag("BazaZoneCommand2");
		}
	}

	private void Start()
	{
		GameObject original = Resources.Load("Prefabs/Fight/Flags/Pedestal") as GameObject;
		GameObject gameObject = Object.Instantiate(original, gameObject_0.transform.position, gameObject_0.transform.rotation) as GameObject;
		PedestalController component = gameObject.GetComponent<PedestalController>();
		bazaBlue = component.bluePedestal;
		bazaRed = component.redPedestal;
		gameObject_1 = component.colliderObject;
		gameObject_2 = component.animationObject;
		flagIndicator_0 = ((!isBlue) ? FlagIndicators.FlagIndicators_0.flagIndicatorRed : FlagIndicators.FlagIndicators_0.flagIndicatorBlue);
		uisprite_0 = flagIndicator_0.GetComponent<UISprite>();
		flagIndicator_0.target = pointObjTexture.transform;
		flagIndicator_0.flagController = this;
		flagIndicator_1 = ((!isBlue) ? FlagIndicators.FlagIndicators_0.bazaIndicatorRed : FlagIndicators.FlagIndicators_0.bazaIndicatorBlue);
		uisprite_1 = flagIndicator_1.GetComponent<UISprite>();
		flagIndicator_1.target = component.point.transform;
		flagIndicator_1.flagController = this;
	}

	private void Update()
	{
		if (PhotonNetwork.Boolean_9 && !Boolean_0 && !Boolean_1)
		{
			timerToBaza -= Time.deltaTime;
			if (timerToBaza < 0f)
			{
				GoBaza();
				if (WeaponManager.weaponManager_0.myNetworkStartTable != null)
				{
					WeaponManager.weaponManager_0.myNetworkStartTable.SendSystemMessegeFromFlagReturned(isBlue);
				}
			}
		}
		if (WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myPlayerMoveC == null || WeaponManager.weaponManager_0.myNetworkStartTable == null || !WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_25)
		{
			return;
		}
		HeadUpDisplay headUpDisplay_ = HeadUpDisplay.HeadUpDisplay_0;
		if (GameObject_1 != null && GameObject_1.activeInHierarchy == Boolean_0)
		{
			GameObject_1.SetActive(!Boolean_0);
		}
		if (targetTrasform != null)
		{
			base.transform.position = targetTrasform.position;
			base.transform.rotation = targetTrasform.rotation;
		}
		else
		{
			Boolean_0 = false;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		int num = 0;
		int num2 = 0;
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject == null)
			{
				continue;
			}
			SkinName component = gameObject.GetComponent<SkinName>();
			if (!(component == null) && !(component.Player_move_c_0 == null))
			{
				int int32_ = component.Player_move_c_0.Int32_2;
				if (int32_ == 1)
				{
					num++;
				}
				if (int32_ == 2)
				{
					num2++;
				}
			}
		}
		if ((num == 0 || num2 == 0) && GameObject_0 != null && GameObject_0.activeSelf)
		{
			GameObject_0.SetActive(false);
			bool_0 = true;
		}
		if ((num == 0 || num2 == 0) && headUpDisplay_ != null)
		{
			if (!headUpDisplay_.message_wait.activeSelf)
			{
				headUpDisplay_.message_wait.SetActive(true);
			}
			headUpDisplay_.HideStartCapture();
		}
		if (num != 0 && num2 != 0 && headUpDisplay_ != null && headUpDisplay_.message_wait.activeSelf)
		{
			headUpDisplay_.message_wait.SetActive(false);
			headUpDisplay_.ShowStartCapture();
		}
		if (num != 0 && num2 != 0 && GameObject_0 != null && !GameObject_0.activeSelf)
		{
			GameObject_0.SetActive(true);
			bool_0 = false;
		}
		if ((num == 0 || num2 == 0) && Boolean_0)
		{
			GameObject[] array3 = array;
			foreach (GameObject gameObject2 in array3)
			{
				if (idCapturePlayer == gameObject2.GetComponent<PhotonView>().int_1)
				{
					gameObject2.GetComponent<SkinName>().Player_move_c_0.Boolean_15 = false;
				}
				GoBaza();
			}
		}
		setActive(gameObject_1, !Boolean_1 && bazaBlue.activeSelf);
		setActive(gameObject_2, !Boolean_1 && bazaBlue.activeSelf);
		updateColors();
	}

	private void updateColors()
	{
		TypeCommand typeCommand_ = WeaponManager.weaponManager_0.myNetworkStartTable.PlayerCommandController_0.TypeCommand_1;
		bool flag = typeCommand_ == TypeCommand.Diggers;
		bool flag2 = typeCommand_ == TypeCommand.Kritters;
		if ((flag && isBlue) || (flag2 && !isBlue))
		{
			GameObject_1 = rayBlue;
			GameObject_0 = flagBlue;
			setActive(bazaBlue, true);
			setActive(bazaRed, false);
		}
		else if ((flag && !isBlue) || (flag2 && isBlue))
		{
			GameObject_1 = rayRed;
			GameObject_0 = flagRed;
			setActive(bazaBlue, false);
			setActive(bazaRed, true);
		}
		else
		{
			GameObject_1 = null;
			GameObject_0 = null;
		}
		if (bool_0)
		{
			setActive(flagBlue, false);
			setActive(flagRed, false);
		}
		else if ((flag && isBlue) || (flag2 && !isBlue))
		{
			setActive(flagBlue, true);
			setActive(flagRed, false);
			setActive(rayBlue, (!Boolean_0) ? true : false);
			setActive(rayRed, false);
			if (!uisprite_1.String_0.Equals(string_0))
			{
				uisprite_1.String_0 = string_0;
			}
			if (!uisprite_0.String_0.Equals(string_2))
			{
				uisprite_0.String_0 = string_2;
			}
		}
		else if ((flag && !isBlue) || (flag2 && isBlue))
		{
			setActive(flagBlue, false);
			setActive(flagRed, true);
			setActive(rayBlue, false);
			setActive(rayRed, (!Boolean_0) ? true : false);
			if (!uisprite_1.String_0.Equals(string_1))
			{
				uisprite_1.String_0 = string_1;
			}
			if (!uisprite_0.String_0.Equals(string_3))
			{
				uisprite_0.String_0 = string_3;
			}
		}
		else
		{
			setActive(flagBlue, false);
			setActive(flagRed, false);
			setActive(rayBlue, false);
			setActive(rayRed, false);
		}
	}

	public void GoBaza()
	{
		timerToBaza = float_0;
		photonView_0.RPC("GoBazaRPC", PhotonTargets.All);
	}

	public void SetCapture(int int_0)
	{
		photonView_0.RPC("SetCaptureRPC", PhotonTargets.All, int_0);
	}

	public void SetNOCapture(Vector3 vector3_0, Quaternion quaternion_0)
	{
		timerToBaza = float_0;
		photonView_0.RPC("SetNOCaptureRPC", PhotonTargets.All, vector3_0, quaternion_0);
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (photonView_0 == null)
		{
			Debug.Log("FlagController.OnPhotonPlayerConnected():    photonView == null");
		}
		else if (Boolean_0)
		{
			photonView_0.RPC("SetCaptureRPCNewPlayer", photonPlayer_0, idCapturePlayer);
		}
		else if (Boolean_1)
		{
			photonView_0.RPC("GoBazaRPCNewPlayer", photonPlayer_0);
		}
		else
		{
			photonView_0.RPC("SetNOCaptureRPCNewPlayer", photonPlayer_0, base.transform.position, base.transform.rotation);
		}
	}

	private void setActive(GameObject gameObject_5, bool bool_3)
	{
		if (gameObject_5.activeSelf != bool_3)
		{
			gameObject_5.SetActive(bool_3);
		}
	}

	private void logFunc(string string_4)
	{
	}

	private void _setCapture(int int_0)
	{
		Boolean_1 = false;
		Boolean_0 = true;
		idCapturePlayer = int_0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject.GetComponent<PhotonView>().int_1 == int_0)
			{
				targetTrasform = gameObject.GetComponent<SkinName>().Player_move_c_0.flagPoint.transform;
				gameObject.GetComponent<SkinName>().Player_move_c_0.Boolean_15 = true;
			}
		}
	}

	private void _goBaza()
	{
		Boolean_1 = true;
		Boolean_0 = false;
		idCapturePlayer = -1;
		if (targetTrasform != null)
		{
			targetTrasform.parent.GetComponent<SkinName>().Player_move_c_0.Boolean_15 = false;
		}
		targetTrasform = null;
		base.transform.position = gameObject_0.transform.position;
		base.transform.rotation = gameObject_0.transform.rotation;
	}

	private void _setNOCapture(Vector3 vector3_0, Quaternion quaternion_0)
	{
		Boolean_1 = false;
		Boolean_0 = false;
		idCapturePlayer = -1;
		if (targetTrasform != null)
		{
			targetTrasform.parent.GetComponent<SkinName>().Player_move_c_0.Boolean_15 = false;
		}
		targetTrasform = null;
		base.transform.position = vector3_0;
		base.transform.rotation = quaternion_0;
		timerToBaza = float_0;
	}

	[RPC]
	public void SetCaptureRPC(int int_0)
	{
		logFunc("SetCaptureRPC");
		_setCapture(int_0);
	}

	[RPC]
	public void GoBazaRPC()
	{
		logFunc("GoBazaRPC");
		_goBaza();
	}

	[RPC]
	public void SetNOCaptureRPC(Vector3 vector3_0, Quaternion quaternion_0)
	{
		logFunc("SetNOCaptureRPC");
		if (!Boolean_1)
		{
			_setNOCapture(vector3_0, quaternion_0);
		}
	}

	[RPC]
	public void SetCaptureRPCNewPlayer(int int_0)
	{
		logFunc("SetCaptureRPCNewPlayer");
		_setCapture(int_0);
	}

	[RPC]
	public void GoBazaRPCNewPlayer()
	{
		logFunc("GoBazaRPCNewPlayer");
		_goBaza();
	}

	[RPC]
	public void SetNOCaptureRPCNewPlayer(Vector3 vector3_0, Quaternion quaternion_0)
	{
		logFunc("SetNOCaptureRPCNewPlayer");
		_setNOCapture(vector3_0, quaternion_0);
	}
}
