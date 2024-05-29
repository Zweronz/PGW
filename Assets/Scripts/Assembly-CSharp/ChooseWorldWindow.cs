using System;
using System.Collections;
using System.Collections.Generic;
using Rilisoft;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ChooseWorld)]
public class ChooseWorldWindow : BaseGameWindow
{
	private static ChooseWorldWindow chooseWorldWindow_0;

	public UIButton backButton;

	public UIButton startButton;

	public Transform gridTransform;

	public MultipleToggleButton difficultyToggle;

	public Transform ScrollTransform;

	public GameObject SelectMapPanel;

	private Vector2 vector2_0;

	private int int_0;

	private int int_1;

	private float float_0 = 824f;

	public static ChooseWorldWindow ChooseWorldWindow_0
	{
		get
		{
			return chooseWorldWindow_0;
		}
	}

	public static void Show(ChooseWorldWindowParams chooseWorldWindowParams_0 = null)
	{
		if (!(chooseWorldWindow_0 != null))
		{
			chooseWorldWindow_0 = BaseWindow.Load("ChooseWorldWindow") as ChooseWorldWindow;
			chooseWorldWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			chooseWorldWindow_0.Parameters_0.bool_5 = false;
			chooseWorldWindow_0.Parameters_0.bool_0 = false;
			chooseWorldWindow_0.Parameters_0.bool_6 = true;
			chooseWorldWindow_0.InternalShow(chooseWorldWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Init();
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		chooseWorldWindow_0 = null;
	}

	private void Init()
	{
		UIPanel component = ScrollTransform.GetComponent<UIPanel>();
		component.Vector4_1 = new Vector4(0f, 0f, 760 * Screen.width / Screen.height, 736f);
		component.Int32_1 = SelectMapPanel.GetComponent<UIPanel>().Int32_1 + 1;
		int_1 = gridTransform.childCount;
		InitButtonHandlers();
		vector2_0 = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		string string_0 = Localizer.Get("Key_0241");
		Func<int, string> func = (int int_0) => string.Format(string_0, int_0);
		for (int i = 0; i < LevelBox.list_0.Count; i++)
		{
			bool flag = CalculateStarsLeftToOpenTheBox(i) <= 0;
			Texture texture = null;
			if (flag)
			{
				texture = Resources.Load<Texture>(string.Format("Boxes/{0}", LevelBox.list_0[i].string_2));
			}
			else
			{
				texture = Resources.Load<Texture>(string.Format("Boxes/{0}_closed", LevelBox.list_0[i].string_2));
				if (texture == null)
				{
					texture = Resources.Load<Texture>(string.Format("Boxes/{0}", LevelBox.list_0[i].string_2));
				}
			}
			Transform child = gridTransform.GetChild(i);
			child.GetComponent<UITexture>().Texture_0 = texture;
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
			Transform transform2 = child.Find("CaptionLabel");
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(flag || i == LevelBox.list_0.Count - 1);
			}
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		Defs.int_19 = Storager.GetInt(Defs.String_12);
		if (difficultyToggle != null)
		{
			difficultyToggle.buttons[Defs.int_19].Boolean_0 = true;
			difficultyToggle.Clicked += delegate(object sender, MultipleToggleEventArgs e)
			{
				ButtonClickSound.buttonClickSound_0.PlayClick();
				Storager.SetInt(Defs.String_12, e.Int32_0);
				Defs.int_19 = e.Int32_0;
				Storager.Save();
			};
		}
	}

	private new void Update()
	{
		if (SelectMapPanel.activeInHierarchy)
		{
			if (ScrollTransform.localPosition.x > 0f)
			{
				int_0 = Mathf.RoundToInt((ScrollTransform.localPosition.x - (float)Mathf.FloorToInt(ScrollTransform.localPosition.x / float_0 / (float)int_1) * float_0 * (float)int_1) / float_0);
				int_0 = int_1 - int_0;
			}
			else
			{
				int_0 = -1 * Mathf.RoundToInt((ScrollTransform.localPosition.x - (float)Mathf.CeilToInt(ScrollTransform.localPosition.x / float_0 / (float)int_1) * float_0 * (float)int_1) / float_0);
			}
			if (int_0 == int_1)
			{
				int_0 = 0;
			}
		}
		if (startButton != null)
		{
			startButton.gameObject.SetActive(int_0 == 0 || CalculateStarsLeftToOpenTheBox(int_0) <= 0);
		}
	}

	private void InitButtonHandlers()
	{
		ButtonHandler component = startButton.GetComponent<ButtonHandler>();
		if (component != null)
		{
			component.Clicked += HandleStartClicked;
		}
		component = backButton.GetComponent<ButtonHandler>();
		if (component != null)
		{
			component.Clicked += HandleBackClicked;
		}
	}

	private void HandleStartClicked(object sender, EventArgs e)
	{
		Hide();
		ButtonClickSound.buttonClickSound_0.PlayClick();
		int index = int_0;
		CurrentCampaignGame.string_0 = LevelBox.list_0[index].string_0;
		LoadConnectScene.texture_0 = null;
		LoadConnectScene.string_0 = "ChooseLevel";
		LoadConnectScene.texture_1 = null;
		Application.LoadLevel(Defs.string_3);
	}

	private void HandleBackClicked(object sender, EventArgs e)
	{
		Hide();
	}

	private int CalculateStarsLeftToOpenTheBox(int int_2)
	{
		if (int_2 >= LevelBox.list_0.Count)
		{
			throw new ArgumentOutOfRangeException("boxIndex");
		}
		int num = 0;
		for (int i = 0; i < int_2; i++)
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
		int num2 = LevelBox.list_0[int_2].int_0;
		return num2 - num;
	}
}
