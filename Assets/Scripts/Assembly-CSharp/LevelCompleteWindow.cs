using System;
using System.Collections;
using System.Collections.Generic;
using Rilisoft;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.LevelComplete)]
public class LevelCompleteWindow : BaseGameWindow
{
	private static LevelCompleteWindow levelCompleteWindow_0;

	public GameObject panel;

	public Transform RentWindowPoint;

	public GameObject quitButton;

	public GameObject menuButton;

	public GameObject retryButton;

	public GameObject nextButton;

	public GameObject shopButton;

	public GameObject[] statisticLabels;

	public UILabel[] afterLevelAwardLabels;

	public UILabel[] awardBoxLabels;

	public GameObject brightStarPrototypeSprite;

	public GameObject darkStarPrototypeSprite;

	public GameObject award1coinSprite;

	public GameObject award15coinsSprite;

	public GameObject gameOverSprite;

	public GameObject checkboxSpritePrototype;

	public GameObject survivalResults;

	public GameObject backgroundTexture;

	public GameObject backgroundSurvivalTexture;

	public AudioClip[] coinClips;

	public AudioClip[] starClips;

	public AudioClip shopButtonSound;

	public AudioClip awardClip;

	private int int_0;

	private int int_1;

	private int? nullable_0;

	private string string_0 = string.Empty;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5;

	private bool bool_6;

	private bool bool_7;

	public static LevelCompleteWindow LevelCompleteWindow_0
	{
		get
		{
			return levelCompleteWindow_0;
		}
	}

	public static void Show(LevelCompleteWindowParams levelCompleteWindowParams_0)
	{
		if (!(levelCompleteWindow_0 != null) && levelCompleteWindowParams_0 != null)
		{
			levelCompleteWindow_0 = BaseWindow.Load("LevelCompleteWindow") as LevelCompleteWindow;
			levelCompleteWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			levelCompleteWindow_0.Parameters_0.bool_5 = false;
			levelCompleteWindow_0.Parameters_0.bool_0 = false;
			levelCompleteWindow_0.Parameters_0.bool_6 = false;
			levelCompleteWindow_0.InternalShow(levelCompleteWindowParams_0);
		}
	}

	public override void OnShow()
	{
		LevelCompleteWindowParams levelCompleteWindowParams = base.WindowShowParameters_0 as LevelCompleteWindowParams;
		if (levelCompleteWindowParams != null)
		{
			bool_2 = levelCompleteWindowParams.bool_1;
			bool_3 = levelCompleteWindowParams.bool_2;
			bool_4 = levelCompleteWindowParams.bool_0;
			Init();
			base.OnShow();
		}
	}

	public override void OnHide()
	{
		base.OnHide();
		levelCompleteWindow_0 = null;
		if (bool_7)
		{
			FightScreen.FightScreen_0.SwitchToMenu();
		}
	}

	private void Init()
	{
		Screen.lockCursor = false;
		if (bool_4)
		{
			backgroundSurvivalTexture.SetActive(true);
		}
		else
		{
			backgroundTexture.SetActive(true);
		}
		BindButtonHandler(menuButton, HandleMenuButton);
		BindButtonHandler(retryButton, HandleRetryButton);
		BindButtonHandler(nextButton, HandleNextButton);
		BindButtonHandler(shopButton, HandleShopButton);
		BindButtonHandler(quitButton, HandleQuitButton);
		if (!bool_4)
		{
			int num = -1;
			LevelBox levelBox = null;
			foreach (LevelBox item in LevelBox.list_0)
			{
				if (!item.string_0.Equals(CurrentCampaignGame.string_0))
				{
					continue;
				}
				levelBox = item;
				for (int i = 0; i != item.list_1.Count; i++)
				{
					CampaignLevel campaignLevel = item.list_1[i];
					if (campaignLevel.string_0.Equals(CurrentCampaignGame.string_1))
					{
						num = i;
						break;
					}
				}
				break;
			}
			if (levelBox != null)
			{
				bool_1 = num >= levelBox.list_1.Count - 1;
				string_0 = levelBox.list_1[(!bool_1) ? (num + 1) : num].string_0;
			}
			else
			{
				Debug.LogError("Current box not found in the list of boxes!");
				bool_1 = true;
				string_0 = Application.loadedLevelName;
			}
			int_0 = 0;
			int_1 = InitializeStarCount();
			if (!bool_2)
			{
				Dictionary<string, int> dictionary = CampaignProgress.dictionary_0[CurrentCampaignGame.string_0];
				if (!dictionary.ContainsKey(CurrentCampaignGame.string_1))
				{
					bool_5 = true;
					if (bool_1)
					{
						nullable_0 = levelBox.Int32_0;
					}
					dictionary.Add(CurrentCampaignGame.string_1, int_1);
					CampaignProgress.SaveCampaignProgress();
				}
				else
				{
					int_0 = dictionary[CurrentCampaignGame.string_1];
					dictionary[CurrentCampaignGame.string_1] = Math.Max(int_0, int_1);
					CampaignProgress.SaveCampaignProgress();
				}
				GameStatHelper.GameStatHelper_0.BattleStop();
				CampaignProgress.OpenNewBoxIfPossible();
				CampaignProgress.SaveCampaignProgress();
			}
			bool_6 = InitializeAwardConferred();
		}
		survivalResults.SetActive(false);
		quitButton.SetActive(false);
		if (!bool_2)
		{
			if (award1coinSprite != null && award15coinsSprite != null)
			{
				award1coinSprite.SetActive(!bool_6);
				UILabel[] array = afterLevelAwardLabels;
				foreach (UILabel uILabel in array)
				{
					uILabel.gameObject.SetActive(int_1 > int_0 && int_1 <= InitializeCoinIndexBound());
				}
				award15coinsSprite.SetActive(bool_6);
				if (bool_6)
				{
					UILabel[] array2 = awardBoxLabels;
					foreach (UILabel uILabel2 in array2)
					{
						uILabel2.String_0 = string.Format(uILabel2.String_0, GemsToAddForBox());
					}
				}
			}
			GameObject[] array3 = statisticLabels;
			foreach (GameObject gameObject in array3)
			{
				gameObject.SetActive(bool_4);
			}
			if (brightStarPrototypeSprite != null && darkStarPrototypeSprite != null)
			{
				StartCoroutine(DisplayLevelResult());
			}
		}
		else
		{
			award1coinSprite.SetActive(false);
			award15coinsSprite.SetActive(false);
			nextButton.SetActive(false);
			checkboxSpritePrototype.SetActive(false);
			if (!bool_4 && gameOverSprite != null)
			{
				gameOverSprite.SetActive(true);
				UILabel[] componentsInChildren = gameOverSprite.GetComponentsInChildren<UILabel>();
				UILabel[] array4 = componentsInChildren;
				foreach (UILabel uILabel3 in array4)
				{
					if (uILabel3.name.Contains("Deathreason") && bool_3)
					{
						uILabel3.String_0 = LocalizationStorage.Get.Term("suicide_in_single_game");
					}
				}
			}
			if (bool_4)
			{
				DisplaySurvivalResult();
			}
			GameObject[] array5 = statisticLabels;
			foreach (GameObject gameObject2 in array5)
			{
				gameObject2.SetActive(bool_4);
			}
			if (!bool_4)
			{
				float x = (retryButton.transform.position.x - menuButton.transform.position.x) / 2f;
				Vector3 vector = new Vector3(x, 0f, 0f);
				menuButton.transform.position = retryButton.transform.position - vector;
				retryButton.transform.position += vector;
			}
			menuButton.SetActive(!bool_4);
		}
		if (bool_4)
		{
			WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
			weaponManager_.Reset();
		}
		levelCompleteWindow_0 = this;
	}

	private bool InitializeAwardConferred()
	{
		return bool_1 && bool_5;
	}

	private int GemsToAddForBox()
	{
		bool flag = CurrentCampaignGame.string_1.Equals("School");
		bool flag2 = CurrentCampaignGame.string_1.StartsWith("Gluk");
		int result = 0;
		if (flag)
		{
			result = LevelBox.list_0[0].int_1;
		}
		else if (flag2)
		{
			result = LevelBox.list_0[1].int_1;
		}
		return result;
	}

	private IEnumerator DisplayLevelResult()
	{
		menuButton.SetActive(false);
		retryButton.SetActive(false);
		nextButton.SetActive(false);
		shopButton.SetActive(false);
		int num = InitializeCoinIndexBound();
		List<GameObject> list = new List<GameObject>(3);
		for (int i = 0; i != 3; i++)
		{
			float x = -140f + (float)i * 140f;
			GameObject gameObject = UnityEngine.Object.Instantiate(darkStarPrototypeSprite) as GameObject;
			gameObject.transform.parent = darkStarPrototypeSprite.transform.parent;
			gameObject.transform.localPosition = new Vector3(x, darkStarPrototypeSprite.transform.localPosition.y, 0f);
			gameObject.transform.localScale = darkStarPrototypeSprite.transform.localScale;
			gameObject.SetActive(true);
			list.Add(gameObject);
		}
		int num2 = 0;
		int num3 = 0;
		for (int j = 0; j < 3; j++)
		{
			if ((j == 1 && !CurrentCampaignGame.bool_1) || (j == 2 && !CurrentCampaignGame.bool_0))
			{
				continue;
			}
			yield return new WaitForSeconds(0.4f);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(brightStarPrototypeSprite) as GameObject;
			gameObject2.transform.parent = brightStarPrototypeSprite.transform.parent;
			gameObject2.transform.localPosition = list[num2].transform.localPosition;
			gameObject2.transform.localScale = list[num2].transform.localScale;
			gameObject2.SetActive(true);
			UnityEngine.Object.Destroy(list[num2]);
			GameObject gameObject3 = UnityEngine.Object.Instantiate(checkboxSpritePrototype) as GameObject;
			gameObject3.transform.parent = checkboxSpritePrototype.transform.parent;
			gameObject3.transform.localPosition = new Vector3(checkboxSpritePrototype.transform.localPosition.x, checkboxSpritePrototype.transform.localPosition.y - 50f * (float)j, checkboxSpritePrototype.transform.localPosition.z);
			gameObject3.transform.localScale = checkboxSpritePrototype.transform.localScale;
			gameObject3.SetActive(true);
			if (starClips != null && num2 < starClips.Length && starClips[num2] != null && Defs.Boolean_0)
			{
				NGUITools.PlaySound(starClips[num2]);
			}
			yield return new WaitForSeconds(0.3f);
			bool flag = int_1 > int_0;
			if (num2 < num && flag)
			{
				num3++;
				if (coinClips != null && num2 < coinClips.Length && coinClips[num2] != null && Defs.Boolean_0)
				{
					NGUITools.PlaySound(coinClips[num2]);
				}
			}
			num2++;
		}
		int num4 = num2;
		if (bool_6)
		{
			yield return new WaitForSeconds(0.4f);
			string empty = string.Empty;
			string empty2 = string.Empty;
			bool flag2 = CurrentCampaignGame.string_1.Equals("School");
			bool flag3 = CurrentCampaignGame.string_1.StartsWith("Gluk");
			if (flag2)
			{
				empty = "CgkIr8rGkPIJEAIQCA";
				if (Defs.RuntimeAndroidEdition_0 == Defs.RuntimeAndroidEdition.Amazon)
				{
					empty = "Block_Survivor_id";
				}
				empty2 = "Block World Survivor";
			}
			else if (flag3)
			{
				empty = "CgkIr8rGkPIJEAIQCQ";
				empty2 = "Dragon Slayer";
			}
		}
		UnityEngine.Object.Destroy(brightStarPrototypeSprite);
		UnityEngine.Object.Destroy(darkStarPrototypeSprite);
		if (bool_6 && awardClip != null)
		{
			yield return new WaitForSeconds(awardClip.length);
		}
		yield return new WaitForSeconds(1f);
		int num5 = 0;
		if (num4 == 3)
		{
			num5 += 5;
		}
		if (nullable_0.HasValue)
		{
			num5 += nullable_0.Value;
		}
		if (num5 != 0)
		{
		}
		menuButton.SetActive(true);
		retryButton.SetActive(true);
		nextButton.SetActive(true);
		shopButton.SetActive(true);
	}

	private void DisplaySurvivalResult()
	{
		menuButton.SetActive(false);
		retryButton.SetActive(false);
		nextButton.SetActive(false);
		shopButton.SetActive(false);
		quitButton.SetActive(false);
		survivalResults.SetActive(true);
		retryButton.SetActive(true);
		shopButton.SetActive(true);
		quitButton.SetActive(true);
	}

	private IEnumerator MyWaitForSeconds(float float_0)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		do
		{
			yield return null;
		}
		while (Time.realtimeSinceStartup - realtimeSinceStartup < float_0);
	}

	private static void SetInitialAmmoForAllGuns()
	{
		WeaponManager.weaponManager_0.ResetAmmoInAllWeapon();
	}

	private static void BindButtonHandler(GameObject gameObject_0, EventHandler eventHandler_0)
	{
		if (gameObject_0 != null)
		{
			ButtonHandler component = gameObject_0.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += eventHandler_0;
			}
		}
	}

	private static int InitializeStarCount()
	{
		int num = 1;
		if (CurrentCampaignGame.bool_1)
		{
			num++;
		}
		if (CurrentCampaignGame.bool_0)
		{
			num++;
		}
		return num;
	}

	private static int InitializeCoinIndexBound()
	{
		int int_ = Defs.int_19;
		return int_ + 1;
	}

	private void HandleMenuButton(object sender, EventArgs e)
	{
		Application.LoadLevel((!bool_4) ? "ChooseLevel" : Defs.String_11);
	}

	private void HandleRetryButton(object sender, EventArgs e)
	{
		Hide();
		if (FightOfflineController.FightOfflineController_0.ModeData_0 != null)
		{
			FightOfflineController.FightOfflineController_0.StartFight(0, FightOfflineController.FightOfflineController_0.ModeData_0.Int32_0);
		}
	}

	private void HandleQuitButton(object sender, EventArgs e)
	{
		bool_7 = true;
		Hide();
	}

	private void HandleNextButton(object sender, EventArgs e)
	{
		if (!bool_1)
		{
			CurrentCampaignGame.string_1 = string_0;
			SetInitialAmmoForAllGuns();
			LevelArt.bool_1 = false;
			Application.LoadLevel((!LevelArt.bool_0) ? "CampaignLoading" : "LevelArt");
		}
		else
		{
			LevelArt.bool_1 = true;
			Application.LoadLevel((!LevelArt.bool_0) ? "ChooseLevel" : "LevelArt");
		}
	}

	private void HandleShopButton(object sender, EventArgs e)
	{
	}
}
