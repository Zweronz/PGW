using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

public sealed class NickLabelController : MonoBehaviour
{
	public static Camera camera_0;

	public GameObject isEnemySprite;

	public GameObject placeMarker;

	public UITexture clanTexture;

	public UILabel clanName;

	public GameObject healthObj;

	public UISprite healthSprite;

	public UISprite multyKill;

	public UISprite multyKillBadge;

	public UILabel nickLabel;

	public UILabel rankNumLabel;

	public UILabel freeAwardTitle;

	public LobbyUserInfoController userInfoController;

	public UISprite adminIcon;

	public bool inWindow;

	private int int_0;

	private float float_0;

	private Vector3 vector3_0;

	private float float_1;

	private float float_2 = 5f;

	private float float_3 = 10f;

	private float float_4 = 30f;

	private int int_1 = 134;

	private Transform transform_0;

	private Color color_0 = new Color(1f, 0.41960785f, 0.2509804f);

	private Color color_1 = new Color(0.18431373f, 0.6745098f, 0.88235295f);

	[CompilerGenerated]
	private Transform transform_1;

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private Vector3 vector3_1;

	[CompilerGenerated]
	private Color color_2;

	public Transform Transform_0
	{
		[CompilerGenerated]
		get
		{
			return transform_1;
		}
		[CompilerGenerated]
		set
		{
			transform_1 = value;
		}
	}

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

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public bool Boolean_1
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

	public bool Boolean_2
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

	public bool Boolean_3
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		set
		{
			bool_3 = value;
		}
	}

	public Vector3 Vector3_0
	{
		[CompilerGenerated]
		get
		{
			return vector3_1;
		}
		[CompilerGenerated]
		set
		{
			vector3_1 = value;
		}
	}

	public Color Color_0
	{
		[CompilerGenerated]
		get
		{
			return color_2;
		}
		[CompilerGenerated]
		set
		{
			color_2 = value;
		}
	}

	private void Awake()
	{
		Boolean_0 = false;
		Boolean_1 = false;
		Boolean_2 = false;
		Boolean_3 = false;
		Vector3_0 = Vector3.up;
		Color_0 = Color.white;
	}

	private void Start()
	{
		transform_0 = base.transform;
		transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
		UsersData.Subscribe(UsersData.EventType.USER_CHANGED, OnUserChanged);
		if (userInfoController != null)
		{
			userInfoController.SetVisible(false);
		}
		NGUITools.SetActive(nickLabel.gameObject, true);
		Boolean_2 = false;
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.USER_CHANGED, OnUserChanged);
	}

	public void ShowMultyKill(int int_2)
	{
		if (int_2 > 0)
		{
			string text = ((int_2 != 1) ? (int_2 - 1).ToString() : "fblood");
			multyKill.String_0 = "kill_" + text;
			multyKillBadge.String_0 = "badge_" + text;
			float_1 = float_2;
		}
	}

	private void OnUserChanged(UsersData.EventData eventData_0)
	{
		if (!Defs.bool_2 || (Player_move_c_0 != null && Player_move_c_0.Boolean_5))
		{
			UpdateData();
		}
	}

	public void StartShow(int int_2 = 0)
	{
		int_0 = int_2;
		nickLabel.Color_0 = Color.white;
		Color_0 = Color.white;
		placeMarker.SetActive(false);
		isEnemySprite.SetActive(false);
		multyKill.gameObject.SetActive(false);
		multyKillBadge.gameObject.SetActive(false);
		clanName.gameObject.SetActive(false);
		clanTexture.Texture_0 = null;
		clanName.String_0 = string.Empty;
		healthObj.SetActive(false);
		rankNumLabel.gameObject.SetActive(true);
		freeAwardTitle.gameObject.SetActive(false);
		if (userInfoController != null)
		{
			userInfoController.SetVisible(false);
		}
		Boolean_2 = false;
		if (Boolean_0)
		{
			UpdateData();
			return;
		}
		if (!Boolean_3)
		{
			Vector3_0 = new Vector3(0f, 0.81f, 0f);
			healthObj.SetActive(false);
		}
		else
		{
			Vector3_0 = new Vector3(0f, 1.76f, 0f);
			healthObj.SetActive(true);
		}
		transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
	}

	public void UpdateData()
	{
		Vector3_0 = new Vector3(0f, 2.26f, 0f);
		if (int_0 == 0)
		{
			nickLabel.String_0 = Defs.GetPlayerNameOrDefault();
			rankNumLabel.String_0 = UserController.UserController_0.GetUserLevel().ToString();
		}
		else
		{
			nickLabel.String_0 = UserController.UserController_0.GetUser(int_0).user_0.string_0;
			rankNumLabel.String_0 = UserController.UserController_0.GetUser(int_0).user_0.int_2.ToString();
		}
		if (userInfoController != null)
		{
			userInfoController.SetVisible(Boolean_0);
		}
		NGUITools.SetActive(nickLabel.gameObject, !Boolean_0);
		clanTexture.Texture_0 = null;
		clanName.String_0 = string.Empty;
	}

	public void OnEnable()
	{
	}

	public void ResetTimeShow()
	{
		float_0 = 0.1f;
	}

	public void Update()
	{
		if ((Transform_0 == null || camera_0 == null) && int_0 == 0)
		{
			Boolean_3 = false;
			if (transform_0.position.y > -5000f)
			{
				transform_0.position = new Vector3(-10000f, -10000f, -10000f);
			}
			Boolean_0 = false;
			return;
		}
		if (Boolean_0)
		{
			if (rankNumLabel.gameObject.activeSelf)
			{
				rankNumLabel.gameObject.SetActive(false);
			}
			float num = 1.3f;
			base.transform.localScale = new Vector3(num, num, num);
		}
		else
		{
			if (!rankNumLabel.gameObject.activeSelf)
			{
				rankNumLabel.gameObject.SetActive(true);
			}
			NGUITools.SetActive(nickLabel.gameObject, true);
		}
		if (float_1 > 0f)
		{
			float_1 -= Time.deltaTime;
		}
		bool flag = float_1 > 0f && Boolean_1;
		if (multyKill.gameObject.activeSelf != flag)
		{
			multyKill.gameObject.SetActive(flag);
			multyKillBadge.gameObject.SetActive(flag);
		}
		Vector3 localPosition = adminIcon.transform.localPosition;
		if (flag)
		{
			localPosition.y = 158f;
		}
		else
		{
			localPosition.y = 72f;
		}
		adminIcon.transform.localPosition = localPosition;
		NGUITools.SetActive(adminIcon.gameObject, Boolean_2);
		if (float_0 > 0f)
		{
			float_0 -= Time.deltaTime;
		}
		if (WeaponManager.weaponManager_0.myPlayer == null)
		{
			ResetTimeShow();
		}
		if (WeaponManager.weaponManager_0.myPlayerMoveC != null && WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_20)
		{
			ResetTimeShow();
		}
		if ((Defs.bool_5 || Defs.bool_6) && WeaponManager.weaponManager_0.myPlayer != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && Transform_0.GetComponent<Player_move_c>() != null && WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2 == Transform_0.GetComponent<Player_move_c>().Int32_2)
		{
			ResetTimeShow();
		}
		try
		{
			if (!inWindow)
			{
				if (float_0 > 0f || flag || (Player_move_c_0 != null && Player_move_c_0.Boolean_11 && Boolean_1))
				{
					vector3_0 = camera_0.WorldToViewportPoint(Transform_0.position + Vector3_0);
				}
				if ((!(float_0 > 0f) && !flag && (!(Player_move_c_0 != null) || !Player_move_c_0.Boolean_11 || !Boolean_1)) || (!(vector3_0.z >= 0f) && int_0 == 0))
				{
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
				transform_0.localPosition = new Vector3((vector3_0.x - 0.5f) * (float)Screen.width / (float)Screen.height * 768f, (vector3_0.y - 0.5f) * 768f, 0f);
			}
			else
			{
				transform_0.localPosition = new Vector3(0f, 0f, 0f);
			}
			if (!Boolean_0 && !Boolean_3)
			{
				if (Transform_0 == null || Transform_0.transform.parent == null)
				{
					Log.AddLine("[NickLabelController::Update] ERROR target == null", Log.LogLevel.ERROR);
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
				if (Transform_0.transform.parent.transform.position.y < -1000f)
				{
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
			}
			if (!Boolean_0)
			{
				float num2 = 1f;
				if (WeaponManager.weaponManager_0.myPlayerMoveC != null && !WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_20)
				{
					float num3 = Vector3.Magnitude(Transform_0.position - WeaponManager.weaponManager_0.myPlayerMoveC.myTransform.position);
					if (num3 < float_3)
					{
						num3 = float_3;
					}
					if (num3 > float_4)
					{
						num3 = float_4;
					}
					num2 = 1f - 0.5f * (num3 - float_3) / (float_4 - float_3);
				}
				transform_0.localScale = new Vector3(num2, num2, num2);
			}
			if (!Boolean_0 && !Boolean_3)
			{
				if (Player_move_c_0 == null && !Boolean_0)
				{
					Log.AddLine("[NickLabelController::Update] ERROR playerScript == null", Log.LogLevel.ERROR);
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
				Color white = Color.white;
				if (MonoSingleton<FightController>.Prop_0.ModeData_0 == null)
				{
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
				white = (((MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DEATH_MATCH && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DUEL) || Player_move_c_0.Int32_2 != Player_move_c.int_0) ? ((Player_move_c_0.Int32_2 != Player_move_c.int_0) ? color_0 : color_1) : color_0);
				if (!white.Equals(Color_0))
				{
					if (nickLabel == null)
					{
						Log.AddLine("[NickLabelController::Update] ERROR nickLabel == null", Log.LogLevel.ERROR);
						transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
						return;
					}
					nickLabel.Color_0 = white;
					Color_0 = white;
				}
				if (placeMarker == null)
				{
					Log.AddLine("[NickLabelController::Update] ERROR placeMarker == null", Log.LogLevel.ERROR);
					transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
					return;
				}
				if (Player_move_c_0.Boolean_11 != placeMarker.activeSelf)
				{
					placeMarker.SetActive(Player_move_c_0.Boolean_11);
				}
				if (Player_move_c_0.NetworkStartTable_0 != null)
				{
					NetworkStartTable networkStartTable_ = Player_move_c_0.NetworkStartTable_0;
					if (rankNumLabel == null || clanTexture == null || clanName == null)
					{
						Log.AddLine("[NickLabelController::Update] ERROR rankTexture == null || clanTexture == null || clanName==null", Log.LogLevel.ERROR);
						transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
						return;
					}
					rankNumLabel.String_0 = networkStartTable_.Int32_9.ToString();
					if (clanTexture.Texture_0 == null && networkStartTable_.Texture_2 != null)
					{
						clanTexture.Texture_0 = networkStartTable_.Texture_2;
					}
					clanName.String_0 = networkStartTable_.String_3;
					clanName.gameObject.SetActive(true);
				}
			}
			if (Boolean_0 || !Boolean_3)
			{
				return;
			}
			TurretController component = Transform_0.GetComponent<TurretController>();
			SkinName skinName = ((!(component.GameObject_0 != null)) ? null : component.GameObject_0.GetComponent<SkinName>());
			Player_move_c player_move_c = ((!(skinName != null)) ? null : skinName.Player_move_c_0);
			if (player_move_c != null)
			{
				Color white2 = Color.white;
				white2 = (((MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DEATH_MATCH && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.DUEL) || player_move_c.Int32_2 != Player_move_c.int_0) ? ((player_move_c.Int32_2 != Player_move_c.int_0) ? color_0 : color_1) : ((!component.Boolean_0) ? color_1 : color_0));
				if (!white2.Equals(Color_0))
				{
					nickLabel.Color_0 = white2;
					Color_0 = white2;
				}
			}
			NetworkStartTable networkStartTable = ((!(player_move_c != null) || !(player_move_c.GameObject_1 != null)) ? null : player_move_c.GameObject_1.GetComponent<NetworkStartTable>());
			if (networkStartTable != null)
			{
				rankNumLabel.String_0 = networkStartTable.Int32_9.ToString();
				if (clanTexture.Texture_0 == null && networkStartTable.Texture_2 != null)
				{
					clanTexture.Texture_0 = networkStartTable.Texture_2;
				}
				clanName.String_0 = networkStartTable.String_3;
				nickLabel.String_0 = skinName.NickName;
				clanName.gameObject.SetActive(true);
			}
			if (!Defs.bool_2)
			{
				rankNumLabel.String_0 = UserController.UserController_0.GetUserLevel().ToString();
				nickLabel.String_0 = FilterBadWorld.FilterString(Defs.GetPlayerNameOrDefault());
			}
			float num4 = Mathf.RoundToInt((float)int_1 * (((!(component.Single_3 < 0f)) ? component.Single_3 : 0f) / component.Single_4));
			if (num4 < 0.1f)
			{
				num4 = 0f;
				healthSprite.enabled = false;
			}
			healthSprite.Int32_0 = Mathf.RoundToInt(num4);
		}
		catch (Exception ex)
		{
			transform_0.localPosition = new Vector3(-10000f, -10000f, -10000f);
			Debug.Log(string.Concat("Exception in ObjectLabel: ", ex, " stack: ", ex.StackTrace));
		}
	}
}
