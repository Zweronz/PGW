using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

internal sealed class ChooseLevel : MonoBehaviour
{
	private sealed class LevelInfo
	{
		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private int int_0;

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
	}

	public GameObject panel;

	public GameObject[] starEnabledPrototypes;

	public GameObject[] starDisabledPrototypes;

	public GameObject gainedStarCountLabel;

	public GameObject backButton;

	public GameObject shopButton;

	public ButtonHandler nextButton;

	public GameObject[] boxOneLevelButtons;

	public GameObject[] boxTwoLevelButtons;

	public AudioClip shopButtonSound;

	public GameObject backgroundHolder;

	public GameObject backgroundHolder_2;

	public GameObject[] boxContents;

	public static ChooseLevel chooseLevel_0;

	private float float_0;

	private int int_0;

	private GameObject[] gameObject_0;

	private string string_0 = string.Empty;

	private IList<LevelInfo> ilist_0 = new List<LevelInfo>();

	private float float_1;

	[CompilerGenerated]
	private static Predicate<LevelBox> predicate_0;

	[CompilerGenerated]
	private static Func<LevelInfo, bool> func_0;

	private void Start()
	{
		if (StoreKitEventListener.gameObject_0 != null)
		{
			StoreKitEventListener.gameObject_0.SetActive(false);
		}
		StoreKitEventListener.StoreKitEventListenerState_0.String_1 = "In Map";
		StoreKitEventListener.StoreKitEventListenerState_0.IDictionary_0.Clear();
		chooseLevel_0 = this;
		float_0 = Time.realtimeSinceStartup;
		bool bool_ = false;
		int_0 = LevelBox.list_0.FindIndex((LevelBox levelBox_0) => levelBox_0.string_0 == CurrentCampaignGame.string_0);
		if (int_0 == -1)
		{
			Debug.LogWarning("Box not found in list!");
			throw new InvalidOperationException("Box not found in list!");
		}
		IList<LevelInfo> list = InitializeLevelInfos(bool_);
		ilist_0 = list;
		string_0 = InitializeGainStarCount(ilist_0);
		if (CurrentCampaignGame.string_0 == "Real")
		{
			gameObject_0 = boxOneLevelButtons;
			backgroundHolder.SetActive(true);
		}
		else if (CurrentCampaignGame.string_0 == "minecraft")
		{
			gameObject_0 = boxTwoLevelButtons;
			backgroundHolder_2.SetActive(true);
		}
		else
		{
			Debug.LogWarning("Unknown box: " + CurrentCampaignGame.string_0);
		}
		InitializeLevelButtons();
		InitializeFixedDisplay();
		InitializeNextButton(ilist_0, nextButton);
		CampaignProgress.SaveCampaignProgress();
		Storager.Save();
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			HandleBackButton(this, EventArgs.Empty);
			Input.ResetInputAxes();
		}
	}

	private void InitializeNextButton(IList<LevelInfo> ilist_1, ButtonHandler buttonHandler_0)
	{
		if (ilist_1 == null)
		{
			throw new ArgumentNullException("levels");
		}
		if (buttonHandler_0 == null)
		{
			throw new ArgumentNullException("nextButton");
		}
		LevelInfo levelInfo_ = ilist_1.LastOrDefault((LevelInfo levelInfo_0) => levelInfo_0.Boolean_0 && levelInfo_0.Int32_0 == 0);
		buttonHandler_0.gameObject.SetActive(levelInfo_ != null);
		if (levelInfo_ != null)
		{
			buttonHandler_0.Clicked += delegate
			{
				HandleLevelButton(levelInfo_.String_0);
			};
		}
	}

	private void InitializeFixedDisplay()
	{
		if (backButton != null)
		{
			backButton.GetComponent<ButtonHandler>().Clicked += HandleBackButton;
		}
		if (gainedStarCountLabel != null)
		{
			gainedStarCountLabel.GetComponent<UILabel>().String_0 = string_0;
		}
	}

	private void HandleBackButton(object sender, EventArgs e)
	{
		if (!(Time.time - float_1 < 1f))
		{
			LoadConnectScene.texture_0 = null;
			LoadConnectScene.string_0 = "CampaignChooseBox";
			LoadConnectScene.texture_1 = null;
			Application.LoadLevel(Defs.string_3);
		}
	}

	private void InitializeLevelButtons()
	{
		if (starEnabledPrototypes != null)
		{
			GameObject[] array = starEnabledPrototypes;
			foreach (GameObject gameObject in array)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
		}
		if (starDisabledPrototypes != null)
		{
			GameObject[] array2 = starDisabledPrototypes;
			foreach (GameObject gameObject2 in array2)
			{
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
		}
		if (boxContents != null)
		{
			for (int k = 0; k != boxContents.Length; k++)
			{
				boxContents[k].SetActive(k == int_0);
			}
			if (gameObject_0 == null)
			{
				throw new InvalidOperationException("Box level buttons are null.");
			}
			GameObject[] array3 = gameObject_0;
			foreach (GameObject gameObject3 in array3)
			{
				if (gameObject3 != null)
				{
					UIButton component = gameObject3.GetComponent<UIButton>();
					if (component != null)
					{
						component.Boolean_0 = false;
					}
				}
			}
			int num = Math.Min(ilist_0.Count, gameObject_0.Length);
			for (int m = 0; m != num; m++)
			{
				LevelInfo levelInfo = ilist_0[m];
				GameObject gameObject4 = gameObject_0[m];
				gameObject4.transform.parent = gameObject4.transform.parent;
				gameObject4.GetComponent<UIButton>().Boolean_0 = levelInfo.Boolean_0;
				UISprite componentInChildren = gameObject4.GetComponentInChildren<UISprite>();
				if (componentInChildren == null)
				{
					Debug.LogWarning("Could not find background of level button.");
				}
				else
				{
					UILabel componentInChildren2 = componentInChildren.GetComponentInChildren<UILabel>();
					if (componentInChildren2 == null)
					{
						Debug.LogWarning("Could not find caption of level button.");
					}
					else
					{
						componentInChildren2.Boolean_6 = levelInfo.Boolean_0;
					}
				}
				gameObject4.AddComponent<ButtonHandler>();
				string string_0 = levelInfo.String_0;
				gameObject4.GetComponent<ButtonHandler>().Clicked += delegate
				{
					HandleLevelButton(string_0);
				};
				gameObject4.SetActive(true);
				for (int n = 0; n != starEnabledPrototypes.Length; n++)
				{
					if (levelInfo.Boolean_0)
					{
						GameObject gameObject5 = starEnabledPrototypes[n];
						if (!(gameObject5 == null))
						{
							GameObject gameObject6 = UnityEngine.Object.Instantiate(gameObject5) as GameObject;
							gameObject6.transform.parent = gameObject4.transform;
							gameObject6.GetComponent<UIToggle>().Boolean_0 = n < levelInfo.Int32_0;
							gameObject6.transform.localPosition = gameObject5.transform.localPosition;
							gameObject6.transform.localScale = gameObject5.transform.localScale;
							gameObject6.SetActive(true);
						}
					}
				}
			}
			GameObject[] array4 = starEnabledPrototypes;
			foreach (GameObject gameObject7 in array4)
			{
				if (gameObject7 != null)
				{
					UnityEngine.Object.Destroy(gameObject7);
				}
			}
			GameObject[] array5 = starDisabledPrototypes;
			foreach (GameObject gameObject8 in array5)
			{
				if (gameObject8 != null)
				{
					UnityEngine.Object.Destroy(gameObject8);
				}
			}
			return;
		}
		throw new InvalidOperationException("boxContents == 0");
	}

	private void HandleLevelButton(string string_1)
	{
		if (!(Time.realtimeSinceStartup - float_0 < 0.15f))
		{
			CurrentCampaignGame.string_1 = string_1;
			WeaponManager.weaponManager_0.Reset();
			FlurryPluginWrapper.LogLevelPressed(CurrentCampaignGame.string_1);
			LevelArt.bool_1 = false;
			Application.LoadLevel((!LevelArt.bool_0) ? "CampaignLoading" : "LevelArt");
		}
	}

	private static IList<LevelInfo> InitializeLevelInfosWithTestData(bool bool_0 = false)
	{
		List<LevelInfo> list = new List<LevelInfo>();
		list.Add(new LevelInfo
		{
			Boolean_0 = true,
			String_0 = "Cementery",
			Int32_0 = 1
		});
		list.Add(new LevelInfo
		{
			Boolean_0 = true,
			String_0 = "City",
			Int32_0 = 3
		});
		list.Add(new LevelInfo
		{
			Boolean_0 = false,
			String_0 = "Hospital"
		});
		return list;
	}

	private static IList<LevelInfo> InitializeLevelInfos(bool bool_0 = false)
	{
		List<LevelInfo> list = new List<LevelInfo>();
		string string_0 = CurrentCampaignGame.string_0;
		int num = LevelBox.list_0.FindIndex((LevelBox levelBox_0) => levelBox_0.string_0 == string_0);
		if (num == -1)
		{
			Debug.LogWarning("Box not found in list!");
			return list;
		}
		LevelBox levelBox = LevelBox.list_0[num];
		List<CampaignLevel> list_ = levelBox.list_1;
		Dictionary<string, int> value;
		if (!CampaignProgress.dictionary_0.TryGetValue(string_0, out value))
		{
			Debug.LogWarning("Box not found in dictionary!");
			value = new Dictionary<string, int>();
		}
		for (int i = 0; i != list_.Count; i++)
		{
			string key = list_[i].string_0;
			int value2 = 0;
			value.TryGetValue(key, out value2);
			LevelInfo levelInfo = new LevelInfo();
			levelInfo.Boolean_0 = i <= value.Count;
			levelInfo.String_0 = key;
			levelInfo.Int32_0 = value2;
			LevelInfo item = levelInfo;
			list.Add(item);
		}
		return list;
	}

	private static string InitializeGainStarCount(IList<LevelInfo> ilist_1)
	{
		int num = 3 * ilist_1.Count;
		int num2 = 0;
		foreach (LevelInfo item in ilist_1)
		{
			num2 += item.Int32_0;
		}
		return string.Format("{0}/{1}", num2, num);
	}

	private void OnDestroy()
	{
		chooseLevel_0 = null;
	}
}
