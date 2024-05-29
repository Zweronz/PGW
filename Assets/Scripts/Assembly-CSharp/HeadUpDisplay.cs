using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using WebSocketSharp;
using engine.helpers;
using engine.integrations;
using engine.unity;
using pixelgun.tutorial;

public class HeadUpDisplay : MonoBehaviour
{
	public enum BonusNotifyType
	{
		HEALTH_ADD = 0,
		HEALTH_REMOVE = 1,
		ARMOR_ADD = 2,
		ARMOR_REMOVE = 3,
		AMMO_ADD = 4,
		GRENADE = 5,
		HOOK_MISS = 6,
		TURRET_BUILDING = 7
	}

	private const float float_0 = 2f;

	private static HeadUpDisplay headUpDisplay_0;

	public static bool bool_0;

	public static bool bool_1;

	public static float float_1 = 1f;

	public static bool bool_2;

	public GameObject deathmatchContainer;

	public GameObject teamBattleContainer;

	public GameObject flagCaptureContainer;

	public SurvivalUIController survivalContainer;

	public GameObject CampaignContainer;

	public HealthUIController healthController;

	public GameObject WeaponPanel;

	public UILabel gearNotifyLabel;

	public UILabel lowFpsNotifyLabel;

	public UILabel[] messageAddScore;

	public UISprite multyKillSprite;

	public UISprite killStrikeBadgeSprite;

	public UISprite killStrikeBadge2Sprite;

	public UISprite killStrikeTitleSprite;

	public UISprite killStrikeTitle2Sprite;

	public UILabel messageBadge;

	public UILabel blinkNoAmmoLabel;

	public UISprite blinkNoAmmoSprite;

	public GameObject blinkNoAmmoCon;

	public UILabel blinkNoHeathLabel;

	public GameObject reloadLabel;

	public UISprite reloadCircle;

	public GameObject reloadText1;

	public GameObject reloadText2;

	public UILabel perfectLabels;

	public GameObject NoGrenadesLabel;

	public GameObject ArenaWaves;

	public UILabel ArenaWavesTxt;

	public UILabel ArenaWavesNum;

	public GameObject ArenaFirstWave;

	public UILabel ArenaFirstWavesLabel;

	public UILabel TeamCommadLabel;

	public UIWidget TeamCommandsContainer;

	public GameObject downBloodTexture;

	public GameObject upBloodTexture;

	public GameObject leftBloodTexture;

	public GameObject rightBloodTexture;

	public GameObject aimUp;

	public GameObject aimDown;

	public GameObject aimRight;

	public GameObject aimLeft;

	public UISprite aimHit;

	public UISprite[] blinkNoHeathFrames;

	public DamageTakenController[] damageTakenControllers;

	public GameObject enemiesLeftLabel;

	public GameObject message_draw;

	public GameObject message_wait;

	public GameObject scopeText;

	public GameObject aimPanel;

	public GameObject flagBlueCaptureTexture;

	public GameObject flagRedCaptureTexture;

	public AudioClip lowResourceBeep;

	public DominationController dominationController;

	public GameObject startTeamCon;

	public UILabel startTeamLabel;

	public GameObject ArenaBossLabel;

	public GameObject ArenaTimeAttentionLabelCon;

	public UILabel ArenaTimeAttentionLabel1;

	public UILabel ArenaTimeAttentionLabel2;

	public GameObject SpecialContainer;

	public GameObject[] BonusNotifyObjects;

	public UILabel[] BonusNotifyLabels;

	private Player_move_c player_move_c_0;

	private bool bool_3;

	private float float_2;

	private float float_3;

	private float float_4;

	private float float_5;

	private int int_0;

	private IEnumerator ienumerator_0;

	private ZombieCreator zombieCreator_0;

	private int int_1;

	private double double_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private bool bool_4;

	public static HeadUpDisplay HeadUpDisplay_0
	{
		get
		{
			return headUpDisplay_0;
		}
	}

	public UIPanel UIPanel_0
	{
		get
		{
			return GetComponent<UIPanel>();
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	public static void Show()
	{
		if (!(headUpDisplay_0 != null))
		{
			headUpDisplay_0 = ScreenController.ScreenController_0.LoadUI("HUD").GetComponent<HeadUpDisplay>();
			NGUITools.SetActive(headUpDisplay_0.gameObject, true);
		}
	}

	public void Hide()
	{
		NGUITools.SetActive(base.gameObject, false);
		Object.Destroy(headUpDisplay_0.gameObject);
		headUpDisplay_0 = null;
	}

	public void ShowElements()
	{
		if (!BattleStatWindow.Boolean_1 && ProfileWindow.ProfileWindow_0 == null && KillCamWindow.KillCamWindow_0 == null)
		{
			UIPanel_0.Single_2 = 1f;
		}
	}

	public void HideElements()
	{
		UIPanel_0.Single_2 = 0f;
	}

	private void Awake()
	{
		gearNotifyLabel.gameObject.SetActive(false);
		lowFpsNotifyLabel.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (WeaponManager.weaponManager_0.myPlayerMoveC != null)
		{
			player_move_c_0 = WeaponManager.weaponManager_0.myPlayerMoveC;
			bool_3 = player_move_c_0.Boolean_4;
		}
		else
		{
			Log.AddLine("HeadUpDisplay::Start. Error set Player_move_c instance!", Log.LogLevel.FATAL);
		}
		if (bool_3)
		{
			enemiesLeftLabel.SetActive(false);
		}
		else
		{
			zombieCreator_0 = ZombieCreator.zombieCreator_0;
		}
		if (!bool_3 && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.CAMPAIGN)
		{
			CampaignContainer.SetActive(true);
		}
		if (!bool_3 && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.ARENA)
		{
			survivalContainer.gameObject.SetActive(true);
		}
		if (bool_3 && (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.DEATH_MATCH || MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.DUEL))
		{
			deathmatchContainer.SetActive(true);
		}
		if (bool_3 && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
		{
			teamBattleContainer.SetActive(true);
		}
		if (bool_3 && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			flagCaptureContainer.SetActive(true);
		}
		WeaponManager.WeaponEquipped += OnWeaponEquipped;
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT || MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			ShowStartCapture();
		}
		double_0 = Utility.Double_0;
		IntegrationsManager.IntegrationsManager_0.AddListener(IntegrationEventType.GAME_STAT, ShowLowFPSLabel);
		NGUITools.SetActive(healthController.gameObject, !TutorialController.TutorialController_0.Boolean_0);
		Boolean_0 = false;
		for (int i = 0; i < TeamCommandsContainer.transform.childCount; i++)
		{
			UILabel component = TeamCommandsContainer.transform.GetChild(i).GetComponent<UILabel>();
			component.String_0 = string.Format(Localizer.Get("hud.team_command_hud.mode_" + (int)MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 + ".id_" + (i + 1)));
		}
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			SpecialContainer.SetActive(false);
		}
		WindowController.WindowController_0.HideAllWindow();
		if (LoadingWindow.LoadingWindow_0 != null && LoadingWindow.LoadingWindow_0.Boolean_0)
		{
			LoadingWindow.LoadingWindow_0.Hide();
		}
	}

	private void OnDestroy()
	{
		WeaponManager.WeaponEquipped -= OnWeaponEquipped;
		headUpDisplay_0 = null;
		IntegrationsManager.IntegrationsManager_0.RemoveListener(IntegrationEventType.GAME_STAT, ShowLowFPSLabel);
	}

	private void Update()
	{
		HandleKeyboard();
		UpdateMessageAddScope();
		UpdateBloodSprites();
		UpdateAim();
		UpdateHealthBlinc();
		UpdateMonsterLabel();
		UpdateReloadCircle();
		UpdateWaitPlayers();
	}

	private void OnEnable()
	{
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0 != null)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0.Resume();
		}
		foreach (AnimationState item in aimHit.GetComponent<Animation>())
		{
			item.normalizedTime = 1f;
			aimHit.GetComponent<Animation>().Play(item.clip.name);
		}
		Boolean_0 = false;
	}

	private void OnDisable()
	{
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0 != null)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0.Pause();
		}
	}

	public void ShowStartCapture()
	{
		int customProperties = PhotonNetwork.Room_0.Hashtable_0.GetCustomProperties("roundTime", int.TryParse, 5);
		double num = MonoSingleton<FightController>.Prop_0.FightTimeController_0.Double_0;
		double num2 = (double)(customProperties * 60) - (num - Utility.Double_0);
		if (!(num2 > 5.0))
		{
			startTeamCon.gameObject.SetActive(true);
			startTeamCon.GetComponent<UITweener>().enabled = true;
			startTeamCon.GetComponent<UITweener>().ResetToBeginning();
			if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
			{
				startTeamLabel.String_0 = string.Format(Localizer.Get("ui.start_fight.team_battle"));
			}
			else if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
			{
				startTeamLabel.String_0 = string.Format(Localizer.Get("ui.start_fight.flag_battle"));
			}
		}
	}

	public void HideStartCapture()
	{
		if (startTeamCon.gameObject.activeSelf)
		{
			startTeamCon.gameObject.SetActive(false);
		}
	}

	public void ShowCircularIndicatorOnReload(float float_6)
	{
		reloadLabel.gameObject.SetActive(true);
		reloadText1.SetActive(true);
		reloadText2.SetActive(true);
		Invoke("ReloadAmmo", float_6);
		if (player_move_c_0 != null)
		{
			player_move_c_0.Boolean_10 = true;
		}
		float_4 = float_6;
		float_5 = float_6;
		reloadCircle.Single_0 = 0f;
	}

	public void ShowCircularIndicatorOnTurretBuilding(float float_6)
	{
		reloadLabel.gameObject.SetActive(true);
		reloadText1.SetActive(false);
		reloadText2.SetActive(false);
		Invoke("EndTurretBuilding", float_6);
		float_4 = float_6;
		float_5 = float_6;
		reloadCircle.Single_0 = 0f;
	}

	public void SetEnablePerfectLabel(bool bool_5)
	{
		if (!(perfectLabels == null))
		{
			perfectLabels.gameObject.SetActive(bool_5);
		}
	}

	public void BlinkNoAmmo(int int_2)
	{
		if (int_2 == 0)
		{
			StopPlayingLowResourceBeep();
		}
		float_2 = (float)int_2 * 2f;
		blinkNoAmmoLabel.Color_0 = new Color(blinkNoAmmoLabel.Color_0.r, blinkNoAmmoLabel.Color_0.g, blinkNoAmmoLabel.Color_0.b, 0f);
		blinkNoAmmoSprite.Color_0 = new Color(blinkNoAmmoSprite.Color_0.r, blinkNoAmmoSprite.Color_0.g, blinkNoAmmoSprite.Color_0.b, 0f);
	}

	public void SetKillStrikeBadge(string string_2, string string_3, bool bool_5 = true)
	{
		UISprite uISprite;
		UISprite uISprite2;
		if (bool_5)
		{
			uISprite = killStrikeBadgeSprite;
			uISprite2 = killStrikeTitleSprite;
		}
		else
		{
			uISprite = killStrikeBadge2Sprite;
			uISprite2 = killStrikeTitle2Sprite;
		}
		uISprite.gameObject.SetActive(!string_2.IsNullOrEmpty());
		uISprite.String_0 = string_2;
		uISprite2.gameObject.SetActive(!string_3.IsNullOrEmpty());
		uISprite2.String_0 = string_3;
		if (string.IsNullOrEmpty(string_2) && string.IsNullOrEmpty(string_3))
		{
			return;
		}
		Animation animation = uISprite.GetComponentInChildren<Animation>();
		if (animation == null)
		{
			animation = uISprite.GetComponent<Animation>();
		}
		foreach (AnimationState item in animation)
		{
			animation.Play(item.clip.name);
		}
		Animation animation2 = uISprite2.GetComponentInChildren<Animation>();
		if (animation2 == null)
		{
			animation2 = uISprite2.GetComponent<Animation>();
		}
		foreach (AnimationState item2 in animation2)
		{
			animation2.Play(item2.clip.name);
		}
	}

	public void SetKillStrikePause(bool bool_5, bool bool_6)
	{
		UISprite uISprite;
		UISprite uISprite2;
		if (bool_5)
		{
			uISprite = killStrikeBadgeSprite;
			uISprite2 = killStrikeTitleSprite;
		}
		else
		{
			uISprite = killStrikeBadge2Sprite;
			uISprite2 = killStrikeTitle2Sprite;
		}
		Animation animation = uISprite.GetComponentInChildren<Animation>();
		if (animation == null)
		{
			animation = uISprite.GetComponent<Animation>();
		}
		foreach (AnimationState item in animation)
		{
			if (bool_6)
			{
				animation.Stop(item.clip.name);
			}
			else
			{
				animation.Play(item.clip.name);
			}
		}
		Animation animation2 = uISprite2.GetComponentInChildren<Animation>();
		if (animation2 == null)
		{
			animation2 = uISprite2.GetComponent<Animation>();
		}
		foreach (AnimationState item2 in animation2)
		{
			if (bool_6)
			{
				animation2.Stop(item2.clip.name);
			}
			else
			{
				animation2.Play(item2.clip.name);
			}
		}
	}

	public void ShowMessageBadge(string string_2)
	{
		messageBadge.String_0 = string_2;
		messageBadge.gameObject.SetActive(true);
		TweenScale[] components = messageBadge.gameObject.GetComponents<TweenScale>();
		if (components != null && components.Length > 0)
		{
			TweenScale[] array = components;
			foreach (TweenScale tweenScale in array)
			{
				tweenScale.ResetToBeginning();
				tweenScale.PlayForward();
			}
		}
	}

	public void StopMessageBadge()
	{
		messageBadge.gameObject.SetActive(false);
	}

	public void AimHitAnimPlay()
	{
		foreach (AnimationState item in aimHit.GetComponent<Animation>())
		{
			item.normalizedTime = 0f;
			aimHit.GetComponent<Animation>().Play(item.clip.name);
		}
	}

	public void AddDamageTaken(float float_6)
	{
		int_1++;
		if (int_1 >= damageTakenControllers.Length)
		{
			int_1 = 0;
		}
		damageTakenControllers[int_1].reset(float_6);
	}

	public void ResetDamageTaken()
	{
		for (int i = 0; i < damageTakenControllers.Length; i++)
		{
			damageTakenControllers[i].Remove();
		}
	}

	public void SetScopeForWeapon(string string_2)
	{
		scopeText.SetActive(true);
		string path = ResPath.Combine("Scopes", "Scope_" + string_2);
		scopeText.GetComponent<UITexture>().Texture_0 = Resources.Load(path) as Texture;
	}

	public void ResetScope()
	{
		scopeText.GetComponent<UITexture>().Texture_0 = null;
		scopeText.SetActive(false);
	}

	public void ShowArenaBossLabel(bool bool_5)
	{
		ArenaBossLabel.gameObject.SetActive(bool_5);
		if (bool_5)
		{
			HideArenaTimeAttention();
		}
	}

	public void ShowArenaTimeAttention(int int_2, bool bool_5 = true)
	{
		NGUITools.SetActive(ArenaTimeAttentionLabelCon, true);
		ArenaTimeAttentionLabel1.gameObject.SetActive(true);
		ArenaTimeAttentionLabel2.gameObject.SetActive(true);
		ArenaTimeAttentionLabel1.GetComponent<TweenAlpha>().ResetToBeginning();
		ArenaTimeAttentionLabel2.GetComponent<TweenAlpha>().ResetToBeginning();
		ArenaTimeAttentionLabel1.String_0 = string.Format(Localizer.Get("ui." + ((!bool_5) ? "multy" : "arena") + ".hurry_up"));
		ArenaTimeAttentionLabel2.String_0 = string.Format(Localizer.Get("ui." + ((!bool_5) ? "multy" : "arena") + ".time_attention"), int_2);
	}

	public void HideArenaTimeAttention()
	{
		ArenaTimeAttentionLabel1.gameObject.SetActive(false);
		ArenaTimeAttentionLabel1.GetComponent<UITweener>().enabled = true;
		ArenaTimeAttentionLabel2.gameObject.SetActive(false);
		ArenaTimeAttentionLabel2.GetComponent<UITweener>().enabled = true;
		NGUITools.SetActive(ArenaTimeAttentionLabelCon, false);
	}

	public void ShowLowFPSLabel(IIntegration iintegration_0, IntegrationEvent integrationEvent_0)
	{
		IntegrationEventGameStat integrationEventGameStat = integrationEvent_0 as IntegrationEventGameStat;
		if (integrationEventGameStat != null && !lowFpsNotifyLabel.isActiveAndEnabled && !(Utility.Double_0 < double_0))
		{
			SetActiveChecked(lowFpsNotifyLabel.gameObject, true);
			if (integrationEventGameStat.Boolean_1)
			{
				lowFpsNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.low_fps");
			}
			else if (integrationEventGameStat.Boolean_2)
			{
				lowFpsNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.high_ping");
			}
			double_0 = Utility.Double_0 + 60.0;
		}
	}

	public void HandleLowFPSNotifyTweenEnd()
	{
		lowFpsNotifyLabel.gameObject.SetActive(false);
		lowFpsNotifyLabel.GetComponent<UITweener>().enabled = true;
	}

	private void HandleKeyboard()
	{
		if (!Player_move_c.Boolean_0)
		{
			bool flag;
			int num;
			if (InputManager.GetButtonDown("ShowTeamCommands") && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.ARENA && MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.CAMPAIGN)
			{
				flag = MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 != ModeType.TUTORIAL;
			}
			else
				num = 0;
			if (InputManager.GetButtonUp("Attack"))
			{
				RunTurret();
			}
		}
		if (Boolean_0)
		{
			for (int i = 0; i < 5; i++)
			{
				if (Input.GetKeyUp((KeyCode)(49 + i)))
				{
					player_move_c_0.SendGlobalMessageToTeamates(i + 1);
					TeamCommandsContainer.gameObject.SetActive(false);
					Boolean_0 = false;
				}
			}
		}
		if (InputManager.GetButtonUp("Back"))
		{
			if (Boolean_0)
			{
				TeamCommandsContainer.gameObject.SetActive(false);
				Boolean_0 = false;
			}
			else if (!player_move_c_0.PlayerTurretController_0.Boolean_1)
			{
				CancelTurret();
			}
			else if (!BugReportWindow.Boolean_1 && !SettingsControlWindow.Boolean_3 && !FeedbackMenuController.Boolean_0 && !KillCamWindow.Boolean_2 && MessageWindowConfirm.MessageWindowConfirm_0 == null)
			{
				player_move_c_0.SwitchPause();
			}
		}
	}

	private void UpdateMessageAddScope()
	{
		if (!bool_3)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			float num = 0.3f;
			float num2 = 0.2f;
			if (i == 0)
			{
				float num3 = 1f;
				if (player_move_c_0.PlayerScoreController_0.maxTimerSumMessage - player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] < num)
				{
					num3 = 1f + num2 * (player_move_c_0.PlayerScoreController_0.maxTimerSumMessage - player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i]) / num;
				}
				if (player_move_c_0.PlayerScoreController_0.maxTimerSumMessage - player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] - num < num)
				{
					num3 = 1f + num2 * (1f - (player_move_c_0.PlayerScoreController_0.maxTimerSumMessage - player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] - num) / num);
				}
				messageAddScore[i].transform.localScale = new Vector3(num3, num3, num3);
			}
			if (player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] > 0f)
			{
				if (!messageAddScore[i].gameObject.activeSelf)
				{
					messageAddScore[i].gameObject.SetActive(true);
				}
				messageAddScore[i].String_0 = player_move_c_0.PlayerScoreController_0.addScoreString[i];
				messageAddScore[i].Color_0 = new Color(1f, 1f, 1f, (!(player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] > 1f)) ? player_move_c_0.PlayerScoreController_0.timerAddScoreShow[i] : 1f);
			}
			else if (messageAddScore[i].gameObject.activeSelf)
			{
				messageAddScore[i].gameObject.SetActive(false);
			}
		}
	}

	private void UpdateBloodSprites()
	{
		if (!(player_move_c_0 == null))
		{
			if (player_move_c_0.Single_4 > 0f && player_move_c_0.Single_4 < player_move_c_0.Single_1 - 0.03f)
			{
				downBloodTexture.SetActive(true);
			}
			else
			{
				downBloodTexture.SetActive(false);
			}
			if (player_move_c_0.Single_2 > 0f && player_move_c_0.Single_2 < player_move_c_0.Single_1 - 0.03f)
			{
				upBloodTexture.SetActive(true);
			}
			else
			{
				upBloodTexture.SetActive(false);
			}
			if (player_move_c_0.Single_3 > 0f && player_move_c_0.Single_3 < player_move_c_0.Single_1 - 0.03f)
			{
				leftBloodTexture.SetActive(true);
			}
			else
			{
				leftBloodTexture.SetActive(false);
			}
			if (player_move_c_0.Single_5 > 0f && player_move_c_0.Single_5 < player_move_c_0.Single_1 - 0.03f)
			{
				rightBloodTexture.SetActive(true);
			}
			else
			{
				rightBloodTexture.SetActive(false);
			}
		}
	}

	private void UpdateWaitPlayers()
	{
		if (MonoSingleton<FightController>.Prop_0.ModeData_0 != null && (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT || MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.DEATH_MATCH) && !(WeaponManager.weaponManager_0 == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null) && WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_25)
		{
			GameObject[] source = GameObject.FindGameObjectsWithTag("Player");
			message_wait.SetActive(source.Count() == 1);
		}
	}

	private void UpdateReloadCircle()
	{
		if (float_4 > 0f)
		{
			float_4 -= Time.deltaTime;
		}
		if (!(float_4 <= 0f))
		{
			reloadCircle.Single_0 = 1f - float_4 / float_5;
		}
	}

	private void UpdateAim()
	{
		WeaponSounds weaponSounds_ = WeaponManager.weaponManager_0.WeaponSounds_0;
		if (player_move_c_0 == null || weaponSounds_ == null || weaponSounds_.WeaponData_0 == null || aimUp == null || aimDown == null || aimRight == null || aimLeft == null)
		{
			return;
		}
		if (!player_move_c_0.Boolean_16 && weaponSounds_.WeaponData_0.Boolean_1)
		{
			if (!aimUp.activeSelf)
			{
				aimUp.SetActive(true);
			}
			if (!aimDown.activeSelf)
			{
				aimDown.SetActive(true);
			}
			if (!aimRight.activeSelf)
			{
				aimRight.SetActive(true);
			}
			if (!aimLeft.activeSelf)
			{
				aimLeft.SetActive(true);
			}
			aimUp.transform.localPosition = new Vector3(0f, 12f + weaponSounds_.Single_1 * 0.5f, 0f);
			aimDown.transform.localPosition = new Vector3(0f, -12f - weaponSounds_.Single_1 * 0.5f, 0f);
			aimRight.transform.localPosition = new Vector3(12f + weaponSounds_.Single_1 * 0.5f, 0f, 0f);
			aimLeft.transform.localPosition = new Vector3(-12f - weaponSounds_.Single_1 * 0.5f, 0f, 0f);
		}
		else
		{
			if (aimUp.activeSelf)
			{
				aimUp.SetActive(false);
			}
			if (aimDown.activeSelf)
			{
				aimDown.SetActive(false);
			}
			if (aimRight.activeSelf)
			{
				aimRight.SetActive(false);
			}
			if (aimLeft.activeSelf)
			{
				aimLeft.SetActive(false);
			}
		}
	}

	private void UpdateHealthBlinc()
	{
		if (float_2 > 0f)
		{
			float_2 -= Time.deltaTime;
		}
		bool flag = !player_move_c_0.PlayerMechController_0.Boolean_1;
		if (float_2 > 0f && player_move_c_0 != null && !flag)
		{
			blinkNoAmmoLabel.gameObject.SetActive(true);
			blinkNoAmmoSprite.gameObject.SetActive(true);
			blinkNoAmmoCon.gameObject.SetActive(true);
			float num = float_2 % 2f / 2f;
			blinkNoAmmoLabel.Color_0 = new Color(blinkNoAmmoLabel.Color_0.r, blinkNoAmmoLabel.Color_0.g, blinkNoAmmoLabel.Color_0.b, (!(num < 0.5f)) ? ((1f - num) * 2f) : (num * 2f));
			blinkNoAmmoSprite.Color_0 = new Color(blinkNoAmmoSprite.Color_0.r, blinkNoAmmoSprite.Color_0.g, blinkNoAmmoSprite.Color_0.b, (!(num < 0.5f)) ? ((1f - num) * 2f) : (num * 2f));
		}
		if ((float_2 < 0f || (player_move_c_0 != null && flag)) && blinkNoAmmoLabel.gameObject.activeSelf)
		{
			blinkNoAmmoLabel.gameObject.SetActive(false);
			blinkNoAmmoSprite.gameObject.SetActive(false);
			blinkNoAmmoCon.gameObject.SetActive(false);
		}
		int num2 = Mathf.CeilToInt(0.3f * ((!flag) ? player_move_c_0.PlayerParametersController_0.Single_0 : player_move_c_0.PlayerMechController_0.Single_1));
		int num3 = Mathf.FloorToInt(player_move_c_0.PlayerParametersController_0.Single_2);
		if (num3 < int_0 && float_3 < 0f && num3 <= num2)
		{
			float_3 = 6f;
		}
		if (num3 > num2)
		{
			float_3 = -1f;
		}
		int_0 = num3;
		if (float_3 > 0f)
		{
			float_3 -= Time.deltaTime;
		}
		if (float_3 > 0f && !flag)
		{
			if (num3 > 0)
			{
				PlayLowResourceBeepIfNotPlaying(1);
			}
			blinkNoHeathLabel.gameObject.SetActive(true);
			float num4 = float_3 % 2f / 2f;
			float a = ((!(num4 < 0.5f)) ? ((1f - num4) * 2f) : (num4 * 2f));
			blinkNoHeathLabel.Color_0 = new Color(blinkNoHeathLabel.Color_0.r, blinkNoHeathLabel.Color_0.g, blinkNoHeathLabel.Color_0.b, a);
			for (int i = 0; i < blinkNoHeathFrames.Length; i++)
			{
				blinkNoHeathFrames[i].gameObject.SetActive(true);
				blinkNoHeathFrames[i].Color_0 = new Color(1f, 1f, 1f, a);
			}
		}
		if ((float_3 < 0f || player_move_c_0 == null || (player_move_c_0 != null && flag)) && blinkNoHeathLabel.gameObject.activeSelf)
		{
			blinkNoHeathLabel.gameObject.SetActive(false);
			for (int j = 0; j < blinkNoHeathFrames.Length; j++)
			{
				blinkNoHeathFrames[j].gameObject.SetActive(false);
			}
		}
	}

	private void UpdateMonsterLabel()
	{
		if (bool_3 || zombieCreator_0 == null || TutorialController.TutorialController_0.Boolean_0)
		{
			return;
		}
		bool flag = MonstersController.Int32_0 - zombieCreator_0.Int32_2 == 0 && !MonstersController.Boolean_0;
		if (!Defs.bool_0 && flag)
		{
			string text = ((!LevelBox.dictionary_0.ContainsKey(Application.loadedLevelName)) ? LocalizationStore.Get("Key_0854") : LocalizationStore.Get("Key_0192"));
			if (zombieCreator_0.bossShowm)
			{
				text = LocalizationStore.Get("Key_0855");
			}
			enemiesLeftLabel.SetActive(true);
			enemiesLeftLabel.GetComponent<UILabel>().String_0 = text;
		}
		else
		{
			enemiesLeftLabel.SetActive(false);
		}
	}

	public void SlotButtonDown(SlotType slotType_0)
	{
		if (Player_move_c.Boolean_0 || player_move_c_0.PlayerParametersController_0.Single_2 <= 0f || player_move_c_0.Boolean_20)
		{
			return;
		}
		if (slotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && slotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
		{
			SelectWeaponFromCategory(slotType_0);
			return;
		}
		switch (slotType_0)
		{
		case SlotType.SLOT_CONSUM_GRENADE:
			if (!player_move_c_0.PlayerGrenadeController_0.GrenadePress() && player_move_c_0.PlayerGrenadeController_0.Boolean_0)
			{
				ShowNoGrenadesLabel();
			}
			break;
		case SlotType.SLOT_CONSUM_TURRET:
			if (player_move_c_0.PlayerGrenadeController_0.Boolean_0)
			{
				ConsumablesController.UseResultType useResultType2 = ConsumablesController.ConsumablesController_0.IsSlotDurationTypeMayBeUsed(slotType_0);
				if (useResultType2 == ConsumablesController.UseResultType.SUCCESS)
				{
					ShowTurretInterface();
				}
				else
				{
					ShowGearNotify(slotType_0, useResultType2);
				}
			}
			break;
		case SlotType.SLOT_CONSUM_MECH:
		{
			ConsumablesController.UseResultType useResultType3 = ConsumablesController.ConsumablesController_0.UseDurationConsumableSlot(slotType_0);
			if (useResultType3 == ConsumablesController.UseResultType.SUCCESS)
			{
				if (player_move_c_0.PlayerMechController_0.ActivateMech())
				{
					player_move_c_0.PlayerParametersController_0.OnUseConsumableForAllStatistics(slotType_0);
				}
			}
			else
			{
				ShowGearNotify(slotType_0, useResultType3);
			}
			break;
		}
		case SlotType.SLOT_CONSUM_POTION:
		case SlotType.SLOT_CONSUM_JETPACK:
		{
			ConsumablesController.UseResultType useResultType = ConsumablesController.ConsumablesController_0.UseDurationConsumableSlot(slotType_0);
			if (useResultType == ConsumablesController.UseResultType.SUCCESS)
			{
				player_move_c_0.PlayerParametersController_0.OnUseConsumableForAllStatistics(slotType_0);
			}
			else
			{
				ShowGearNotify(slotType_0, useResultType);
			}
			break;
		}
		}
	}

	public void SlotButtonUp(SlotType slotType_0)
	{
		if (slotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			player_move_c_0.PlayerGrenadeController_0.GrenadeFire();
		}
	}

	public void SelectWeaponFromCategory(SlotType slotType_0, bool bool_5 = false)
	{
		if (!(player_move_c_0 == null) && player_move_c_0.PlayerMechController_0.Boolean_1)
		{
			if (!player_move_c_0.PlayerTurretController_0.Boolean_1)
			{
				Weapon weaponFromCurrentSlot = WeaponManager.weaponManager_0.GetWeaponFromCurrentSlot();
				CancelTurret(slotType_0 == weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.SlotType_0);
			}
			SelectWeaponFromIndex(slotType_0, bool_5);
		}
	}

	private void SelectWeaponFromIndex(SlotType slotType_0, bool bool_5 = false)
	{
		if (UserController.UserController_0.GetArtikulIdFromSlot(slotType_0) != 0)
		{
			StopReloadAmmo();
			SetChangeWeapon(slotType_0, bool_5);
		}
	}

	[Obfuscation(Exclude = true)]
	private void ReloadAmmo()
	{
		reloadLabel.gameObject.SetActive(false);
		WeaponManager.weaponManager_0.ReloadAmmo();
	}

	[Obfuscation(Exclude = true)]
	public void EndTurretBuilding()
	{
		CancelInvoke("EndTurretBuilding");
		reloadLabel.gameObject.SetActive(false);
		HandleTurretBuildingTweenEnd();
	}

	public void StopReloadAmmo()
	{
		CancelInvoke("ReloadAmmo");
		reloadLabel.gameObject.SetActive(false);
		if (player_move_c_0 != null)
		{
			player_move_c_0.Boolean_10 = false;
		}
	}

	private void SetChangeWeapon(SlotType slotType_0, bool bool_5 = false)
	{
		Weapon weaponFromCurrentSlot = WeaponManager.weaponManager_0.GetWeaponFromCurrentSlot();
		if (weaponFromCurrentSlot != null && player_move_c_0 != null)
		{
			player_move_c_0.ChangeWeapon(slotType_0, false, bool_5);
		}
	}

	private void OnWeaponEquipped(SlotType slotType_0)
	{
		if (KillCamWindow.KillCamWindow_0 != null || WeaponController.WeaponController_0.GetActiveWeaponSlotType() == slotType_0 || TutorialController.TutorialController_0.Boolean_0)
		{
			SelectWeaponFromCategory(slotType_0, true);
		}
	}

	private void ShowGearNotify(SlotType slotType_0, ConsumablesController.UseResultType useResultType_0)
	{
		if (gearNotifyLabel.gameObject.activeInHierarchy)
		{
			return;
		}
		SetActiveChecked(gearNotifyLabel.gameObject, true);
		switch (useResultType_0)
		{
		case ConsumablesController.UseResultType.SOME_CONS_ALREADY_USES:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.cons_already_uses");
			return;
		case ConsumablesController.UseResultType.IN_COOLDOWN:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.cons_in_cooldown");
			return;
		case ConsumablesController.UseResultType.THIS_CONS_ALREADY_USES:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.this_cons_already_uses");
			return;
		}
		switch (slotType_0)
		{
		case SlotType.SLOT_CONSUM_POTION:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.not_enough_invis");
			break;
		case SlotType.SLOT_CONSUM_JETPACK:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.not_enough_jetpacks");
			break;
		case SlotType.SLOT_CONSUM_MECH:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.not_enough_mechs");
			break;
		case SlotType.SLOT_CONSUM_TURRET:
			gearNotifyLabel.String_0 = Localizer.Get("ui.ingamegui.not_enough_turrets");
			break;
		}
	}

	public void HandleGearNotifyTweenEnd()
	{
		gearNotifyLabel.gameObject.SetActive(false);
		gearNotifyLabel.GetComponent<UITweener>().enabled = true;
	}

	private void ShowNoGrenadesLabel()
	{
		if (NoGrenadesLabel.gameObject.activeInHierarchy)
		{
			TweenAlpha component = NoGrenadesLabel.gameObject.GetComponent<TweenAlpha>();
			if (component != null)
			{
				component.ResetToBeginning();
			}
			SetActiveChecked(NoGrenadesLabel.gameObject, false);
		}
		SetActiveChecked(NoGrenadesLabel.gameObject, true);
	}

	public void HandleNoGrenadesTweenEnd()
	{
		NoGrenadesLabel.gameObject.SetActive(false);
		NoGrenadesLabel.GetComponent<UITweener>().enabled = true;
	}

	public void ShowAddBonusHealth(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.HEALTH_ADD, string.Format("+{0}", int_2));
	}

	public void ShowRemoveBonusHealth(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.HEALTH_REMOVE, string.Format("-{0}", int_2));
	}

	public void ShowAddBonusArmor(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.ARMOR_ADD, string.Format("+{0}", int_2));
	}

	public void ShowRemoveBonusArmor(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.ARMOR_REMOVE, string.Format("-{0}", int_2));
	}

	public void ShowAddBonusAmmo(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.AMMO_ADD, string.Format("+{0}", int_2));
	}

	public void ShowAddBonusGrenade(int int_2)
	{
		ShowBonusNotify(BonusNotifyType.GRENADE, string.Format("+{0}", int_2));
	}

	public void ShowMaxBonusHealth()
	{
		ShowBonusNotify(BonusNotifyType.HEALTH_ADD, Localizer.Get("ui.hud.add_max_health"));
	}

	public void ShowMaxBonusArmor()
	{
		ShowBonusNotify(BonusNotifyType.ARMOR_ADD, Localizer.Get("ui.hud.add_max_armor"));
	}

	public void ShowMaxBonusAmmo()
	{
		ShowBonusNotify(BonusNotifyType.AMMO_ADD, Localizer.Get("ui.hud.add_max_ammo"));
	}

	public void ShowMaxBonusGrenade()
	{
		ShowBonusNotify(BonusNotifyType.GRENADE, Localizer.Get("ui.hud.add_max_grenade"));
	}

	public void ShowHookMiss()
	{
		ShowBonusNotify(BonusNotifyType.HOOK_MISS, Localizer.Get("ui.hud.hook_miss"));
	}

	public void ShowTurretBuilding(float float_6)
	{
		GameObject gameObject = BonusNotifyObjects[7];
		TweenAlpha component = gameObject.GetComponent<TweenAlpha>();
		if (component != null)
		{
			component.float_1 = float_6;
		}
		ShowBonusNotify(BonusNotifyType.TURRET_BUILDING, Localizer.Get("ui.hud.turret_building"));
	}

	public void HandleAddHealthBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.HEALTH_ADD);
	}

	public void HandleRemoveHealthBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.HEALTH_REMOVE);
	}

	public void HandleAddArmorBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.ARMOR_ADD);
	}

	public void HandleRemoveArmorBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.ARMOR_REMOVE);
	}

	public void HandleAddAmmoBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.AMMO_ADD);
	}

	public void HandleAddGrenadeBonusTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.GRENADE);
	}

	public void HandleHookMissTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.HOOK_MISS);
	}

	public void HandleTurretBuildingTweenEnd()
	{
		HandleBonusTweenEnd(BonusNotifyType.TURRET_BUILDING);
	}

	private void ShowBonusNotify(BonusNotifyType bonusNotifyType_0, string string_2)
	{
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			return;
		}
		GameObject gameObject = null;
		BonusNotifyLabels[(int)bonusNotifyType_0].String_0 = string_2;
		for (int i = 0; i < BonusNotifyObjects.Length; i++)
		{
			gameObject = BonusNotifyObjects[i];
			if (gameObject.activeSelf)
			{
				TweenAlpha component = gameObject.GetComponent<TweenAlpha>();
				if (component != null)
				{
					component.ResetToBeginning();
				}
				SetActiveChecked(gameObject, false);
			}
		}
		gameObject = BonusNotifyObjects[(int)bonusNotifyType_0];
		SetActiveChecked(gameObject, true);
	}

	private void HandleBonusTweenEnd(BonusNotifyType bonusNotifyType_0)
	{
		GameObject gameObject = BonusNotifyObjects[(int)bonusNotifyType_0];
		gameObject.SetActive(false);
		gameObject.GetComponent<UITweener>().enabled = true;
	}

	private void ShowTurretInterface()
	{
		NGUITools.SetActive(WeaponPanel, false);
		HeadUpDisplay_0.StopReloadAmmo();
		player_move_c_0.ChangeWeapon(SlotType.SLOT_CONSUM_TURRET, false);
	}

	public void HideTurretInterface()
	{
		NGUITools.SetActive(WeaponPanel, true);
	}

	private void RunTurret()
	{
		if (!player_move_c_0.PlayerTurretController_0.Boolean_1 && player_move_c_0.PlayerTurretController_0.RunTurret())
		{
			HideTurretInterface();
		}
	}

	private void CancelTurret(bool bool_5 = true)
	{
		if (!player_move_c_0.PlayerTurretController_0.Boolean_1)
		{
			HideTurretInterface();
			player_move_c_0.PlayerTurretController_0.CancelTurret(bool_5);
		}
	}

	public void PlayLowResourceBeep(int int_2)
	{
		StopPlayingLowResourceBeep();
		ienumerator_0 = PlayLowResourceBeepCoroutine(int_2);
		StartCoroutine(ienumerator_0);
	}

	public void PlayLowResourceBeepIfNotPlaying(int int_2)
	{
		if (ienumerator_0 == null)
		{
			PlayLowResourceBeep(int_2);
		}
	}

	public void StopPlayingLowResourceBeep()
	{
		if (ienumerator_0 != null)
		{
			StopCoroutine(ienumerator_0);
			ienumerator_0 = null;
		}
	}

	private IEnumerator PlayLowResourceBeepCoroutine(int int_2)
	{
		for (int i = 0; i < int_2; i++)
		{
			if (Defs.Boolean_0)
			{
				NGUITools.PlaySound(lowResourceBeep);
			}
			yield return new WaitForSeconds(1f);
		}
		ienumerator_0 = null;
	}

	public static Player_move_c GetPlayerMoveC()
	{
		if (HeadUpDisplay_0 == null)
		{
			return null;
		}
		return HeadUpDisplay_0.player_move_c_0;
	}

	private static void SetActiveChecked(GameObject gameObject_0, bool bool_5)
	{
		if (bool_5 != gameObject_0.activeSelf)
		{
			gameObject_0.SetActive(bool_5);
		}
	}
}
