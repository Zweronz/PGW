using System;
using System.Collections.Generic;
using Rilisoft;
using UnityEngine;

internal sealed class ChooseBox : MonoBehaviour
{
	private Vector2 vector2_0;

	private Vector2 vector2_1;

	private Vector2 vector2_2;

	private bool bool_0;

	private Vector2 vector2_3 = new Vector2(823f, 736f);

	private bool bool_1;

	private bool bool_2;

	private List<Texture> list_0 = new List<Texture>();

	private List<Texture> list_1 = new List<Texture>();

	public ChooseBoxNGUIController nguiController;

	public Transform gridTransform;

	private bool bool_3;

	private void UnloadBoxPreviews()
	{
		list_0.Clear();
		Resources.UnloadUnusedAssets();
	}

	private void Start()
	{
		if (nguiController.startButton != null)
		{
			ButtonHandler component = nguiController.startButton.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += HandleStartClicked;
			}
		}
		if (nguiController.backButton != null)
		{
			ButtonHandler component2 = nguiController.backButton.GetComponent<ButtonHandler>();
			if (component2 != null)
			{
				component2.Clicked += HandleBackClicked;
			}
		}
		StoreKitEventListener.StoreKitEventListenerState_0.String_0 = "Campaign";
		StoreKitEventListener.StoreKitEventListenerState_0.IDictionary_0.Clear();
		vector2_2 = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		string string_0 = LocalizationStore.Get("Key_0241");
		Func<int, string> func = (int int_0) => string.Format(string_0, int_0);
		for (int i = 0; i < LevelBox.list_0.Count; i++)
		{
			bool flag;
			Texture texture_ = ((!(flag = CalculateStarsLeftToOpenTheBox(i) <= 0)) ? (Resources.Load<Texture>(ResPath.Combine("Boxes", LevelBox.list_0[i].string_2 + "_closed")) ?? Resources.Load<Texture>(ResPath.Combine("Boxes", LevelBox.list_0[i].string_2))) : Resources.Load<Texture>(ResPath.Combine("Boxes", LevelBox.list_0[i].string_2)));
			Transform child = gridTransform.GetChild(i);
			child.GetComponent<UITexture>().Texture_0 = texture_;
			Transform transform = child.Find("NeedMoreStarsLabel");
			if (transform != null)
			{
				if (!flag && i < LevelBox.list_0.Count - 1)
				{
					transform.gameObject.SetActive(true);
					transform.GetComponent<UILabel>().String_0 = func(CalculateStarsLeftToOpenTheBox(i));
				}
				else
				{
					transform.gameObject.SetActive(false);
				}
			}
			else
			{
				Debug.LogWarning("Could not find “NeedMoreStarsLabel”.");
			}
			Transform transform2 = child.Find("CaptionLabel");
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(flag || i == LevelBox.list_0.Count - 1);
			}
			else
			{
				Debug.LogWarning("Could not find “CaptionLabel”.");
			}
		}
	}

	private void HandleStartClicked(object sender, EventArgs e)
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		int selectIndexMap = nguiController.selectIndexMap;
		CurrentCampaignGame.string_0 = LevelBox.list_0[selectIndexMap].string_0;
		LoadConnectScene.texture_0 = null;
		LoadConnectScene.string_0 = "ChooseLevel";
		LoadConnectScene.texture_1 = null;
		Application.LoadLevel(Defs.string_3);
	}

	private void HandleBackClicked(object sender, EventArgs e)
	{
		bool_3 = true;
	}

	private void OnDestroy()
	{
		CampaignProgress.SaveCampaignProgress();
		Storager.Save();
		UnloadBoxPreviews();
	}

	private void Update()
	{
		if (bool_3)
		{
			ButtonClickSound.buttonClickSound_0.PlayClick();
			FlurryPluginWrapper.LogEvent("Back to Main Menu");
			Resources.UnloadUnusedAssets();
			LoadConnectScene.texture_0 = null;
			LoadConnectScene.texture_1 = null;
			LoadConnectScene.string_0 = Defs.String_11;
			Application.LoadLevel(Defs.string_3);
			bool_3 = false;
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			bool_3 = true;
		}
		if (nguiController.startButton != null)
		{
			nguiController.startButton.gameObject.SetActive(nguiController.selectIndexMap == 0 || CalculateStarsLeftToOpenTheBox(nguiController.selectIndexMap) <= 0);
		}
	}

	private int CalculateStarsLeftToOpenTheBox(int int_0)
	{
		if (int_0 >= LevelBox.list_0.Count)
		{
			throw new ArgumentOutOfRangeException("boxIndex");
		}
		int num = 0;
		for (int i = 0; i < int_0; i++)
		{
			LevelBox levelBox = LevelBox.list_0[i];
			Dictionary<string, int> value;
			if (!CampaignProgress.dictionary_0.TryGetValue(levelBox.string_0, out value))
			{
				continue;
			}
			foreach (CampaignLevel item in levelBox.list_1)
			{
				int value2 = 0;
				if (value.TryGetValue(item.string_0, out value2))
				{
					num += value2;
				}
			}
		}
		int int_ = LevelBox.list_0[int_0].int_0;
		return int_ - num;
	}
}
