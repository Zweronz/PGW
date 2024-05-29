using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;
using engine.events;
using engine.network;

public class SkinEditorController : MonoBehaviour
{
	public enum ModeEditor
	{
		SkinPers = 0,
		Cape = 1
	}

	public enum BrashMode
	{
		Pencil = 0,
		Brash = 1,
		Eraser = 2,
		Fill = 3,
		Pipette = 4
	}

	public enum EventType
	{
		SAVE_OK = 0,
		SAVE_ERROR = 1,
		DATA_UPDATE_OK = 2
	}

	public static Color color_0 = new Color(0f, 1f, 0f, 1f);

	public static BrashMode brashMode_0 = BrashMode.Pencil;

	public GameObject topPart;

	public GameObject downPart;

	public GameObject leftPart;

	public GameObject frontPart;

	public GameObject rigthPart;

	public GameObject backPart;

	public ModeEditor modeEditor;

	public static SkinEditorController skinEditorController_0 = null;

	public ButtonHandler saveButton;

	public ButtonHandler backButton;

	public ButtonHandler fillButton;

	public ButtonHandler eraserButton;

	public ButtonHandler brashButton;

	public ButtonHandler pencilButton;

	public ButtonHandler pipetteButton;

	public ButtonHandler colorButton;

	public ButtonHandler okColorInPalitraButton;

	public ButtonHandler setColorButton;

	public ButtonHandler saveChangesButton;

	public ButtonHandler cancelInSaveChangesButton;

	public ButtonHandler okInSaveSkin;

	public ButtonHandler cancelInSaveSkin;

	public ButtonHandler yesInLeaveSave;

	public ButtonHandler noInLeaveSave;

	public ButtonHandler prevHistoryButton;

	public ButtonHandler nextHistoryButton;

	public ButtonHandler yesSaveButtonInEdit;

	public ButtonHandler noSaveButtonInEdit;

	public GameObject previewPers;

	public GameObject previewPersShadow;

	public GameObject skinPreviewPanel;

	public GameObject partPreviewPanel;

	public GameObject editorPanel;

	public GameObject colorPanel;

	public GameObject saveChangesPanel;

	public GameObject saveSkinPanel;

	public GameObject leavePanel;

	public GameObject savePanelInEditorTexture;

	public string selectedPartName;

	public GameObject selectedSide;

	public Texture2D currentSkin;

	public static int int_0 = 0;

	public static Dictionary<string, Dictionary<string, Rect>> dictionary_0 = new Dictionary<string, Dictionary<string, Rect>>();

	public static Dictionary<string, Dictionary<string, Texture2D>> dictionary_1 = new Dictionary<string, Dictionary<string, Texture2D>>();

	public UILabel pensilLabel;

	public UILabel brashLabel;

	public UILabel eraserLabel;

	public UILabel fillLabel;

	public UILabel pipetteLabel;

	public UITexture editorTexture;

	public UISprite oldColor;

	public UISprite newColor;

	public UIInput skinNameInput;

	public static bool bool_0 = false;

	public static bool bool_1 = false;

	public bool isSaveAndExit;

	private List<GameObject> list_0 = new List<GameObject>();

	private string string_0;

	public UISprite[] colorOnBrashSprites;

	private static int int_1 = 0;

	private static readonly BaseEvent<int> baseEvent_0 = new BaseEvent<int>();

	private static Action<string> action_0 = null;

	public static event Action<string> ExitFromSkinEditor
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action<string>)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action<string>)Delegate.Remove(action_0, value);
		}
	}

	public static void Dispatch<EventType>(EventType gparam_0, int int_2 = 0)
	{
		baseEvent_0.Dispatch(int_2, gparam_0);
	}

	private void Start()
	{
		brashMode_0 = BrashMode.Pencil;
		bool_1 = false;
		bool_0 = false;
		skinEditorController_0 = this;
		color_0 = new Color(Storager.GetFloat("ColorForPaintR", 0f), Storager.GetFloat("ColorForPaintG", 1f), Storager.GetFloat("ColorForPaintB", 0f), 1f);
		colorButton.gameObject.GetComponent<UIButton>().Color_0 = color_0;
		colorButton.gameObject.GetComponent<UIButton>().color_1 = color_0;
		colorButton.gameObject.GetComponent<UIButton>().color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().Color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_1 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_0 = color_0;
		for (int i = 0; i < colorOnBrashSprites.Length; i++)
		{
			colorOnBrashSprites[i].Color_0 = color_0;
		}
		if (modeEditor == ModeEditor.SkinPers)
		{
			WearStorage.Get.Storage.GetObjectByKey(int_0);
			if (!SkinsController.CanWearCustomSkin(int_0))
			{
				currentSkin = LocalSkinTextureData.Texture2D_1;
				skinNameInput.String_2 = string.Empty;
			}
			else
			{
				currentSkin = SkinsController.GetSkinTexture(int_0);
				skinNameInput.String_2 = SkinsController.CustomSkinName(int_0);
			}
			partPreviewPanel.SetActive(false);
			skinPreviewPanel.SetActive(true);
			editorPanel.SetActive(false);
			list_0.Add(previewPers);
			list_0.Add(previewPersShadow);
			ShowPreviewSkin();
		}
		if (modeEditor == ModeEditor.Cape)
		{
			Texture2D texture2D_ = null;
			if (modeEditor == ModeEditor.Cape)
			{
				texture2D_ = SkinsController.GetCapeTexture(int_0);
			}
			currentSkin = EditorTextures.CreateCopyTexture(texture2D_);
			partPreviewPanel.SetActive(false);
			skinPreviewPanel.SetActive(false);
			editorPanel.SetActive(true);
			editorTexture.gameObject.GetComponent<EditorTextures>().SetStartCanvas(currentSkin);
		}
		savePanelInEditorTexture.SetActive(false);
		SkinsController.SetTextureRecursivelyFrom(previewPers, currentSkin);
		SetPartsRect();
		UpdateTexturesPartsInDictionary();
		colorPanel.SetActive(false);
		saveChangesPanel.SetActive(false);
		saveSkinPanel.SetActive(false);
		leavePanel.SetActive(false);
		if (topPart != null)
		{
			ButtonHandler component = topPart.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += HandleSideClicked;
			}
		}
		if (downPart != null)
		{
			ButtonHandler component2 = downPart.GetComponent<ButtonHandler>();
			if (component2 != null)
			{
				component2.Clicked += HandleSideClicked;
			}
		}
		if (leftPart != null)
		{
			ButtonHandler component3 = leftPart.GetComponent<ButtonHandler>();
			if (component3 != null)
			{
				component3.Clicked += HandleSideClicked;
			}
		}
		if (frontPart != null)
		{
			ButtonHandler component4 = frontPart.GetComponent<ButtonHandler>();
			if (component4 != null)
			{
				component4.Clicked += HandleSideClicked;
			}
		}
		if (rigthPart != null)
		{
			ButtonHandler component5 = rigthPart.GetComponent<ButtonHandler>();
			if (component5 != null)
			{
				component5.Clicked += HandleSideClicked;
			}
		}
		if (backPart != null)
		{
			ButtonHandler component6 = backPart.GetComponent<ButtonHandler>();
			if (component6 != null)
			{
				component6.Clicked += HandleSideClicked;
			}
		}
		if (saveButton != null)
		{
			saveButton.Clicked += HandleSaveButtonClicked;
		}
		if (backButton != null)
		{
			backButton.Clicked += HandleBackButtonClicked;
		}
		if (fillButton != null)
		{
			fillButton.Clicked += HandleSelectBrashClicked;
		}
		if (brashButton != null)
		{
			brashButton.Clicked += HandleSelectBrashClicked;
		}
		if (pencilButton != null)
		{
			pencilButton.Clicked += HandleSelectBrashClicked;
		}
		if (pipetteButton != null)
		{
			pipetteButton.Clicked += HandleSelectBrashClicked;
		}
		if (eraserButton != null)
		{
			eraserButton.Clicked += HandleSelectBrashClicked;
		}
		if (colorButton != null)
		{
			colorButton.Clicked += HandleSelectColorClicked;
		}
		if (setColorButton != null)
		{
			setColorButton.Clicked += HandleSetColorClicked;
		}
		if (saveChangesButton != null)
		{
			saveChangesButton.Clicked += HandleSaveChangesButtonClicked;
		}
		if (cancelInSaveChangesButton != null)
		{
			cancelInSaveChangesButton.Clicked += HandleCancelInSaveChangesButtonClicked;
		}
		if (okInSaveSkin != null)
		{
			okInSaveSkin.Clicked += HandleOkInSaveSkinClicked;
		}
		if (cancelInSaveSkin != null)
		{
			cancelInSaveSkin.Clicked += HandleCancelInSaveSkinClicked;
		}
		if (yesInLeaveSave != null)
		{
			yesInLeaveSave.Clicked += HandleYesInLeaveSaveClicked;
		}
		if (noInLeaveSave != null)
		{
			noInLeaveSave.Clicked += HandleNoInLeaveSaveClicked;
		}
		if (yesSaveButtonInEdit != null)
		{
			yesSaveButtonInEdit.Clicked += HandleYesSaveButtonInEditClicked;
		}
		if (noSaveButtonInEdit != null)
		{
			noSaveButtonInEdit.Clicked += HandleNoSaveButtonInEditClicked;
		}
	}

	private void HandleYesSaveButtonInEditClicked(object sender, EventArgs e)
	{
		if (modeEditor == ModeEditor.Cape)
		{
			string text = string.Empty;
			WearData wear = WearController.WearController_0.GetWear(int_0);
			if (wear != null)
			{
				text = wear.ArtikulData_0.String_4;
			}
			UserTextureSkinData userTextureSkinData = SkinsController.AddUserSkin(text, (Texture2D)editorTexture.Texture_0, int_0);
			if (userTextureSkinData != null)
			{
				int_1 = 0;
				baseEvent_0.Subscribe(UserSave, EventType.SAVE_OK);
				baseEvent_0.Subscribe(UserSaveCheck, EventType.DATA_UPDATE_OK);
				baseEvent_0.Subscribe(UserSkinSaveError, EventType.SAVE_ERROR);
				UpdateUserSkinNetworkCommand updateUserSkinNetworkCommand = new UpdateUserSkinNetworkCommand();
				updateUserSkinNetworkCommand.userTextureSkinData_0 = userTextureSkinData;
				updateUserSkinNetworkCommand.bool_0 = false;
				AbstractNetworkCommand.Send(updateUserSkinNetworkCommand);
			}
		}
		bool_0 = false;
		HandleBackButtonClicked(null, null);
	}

	private void HandleNoSaveButtonInEditClicked(object sender, EventArgs e)
	{
		bool_0 = false;
		HandleBackButtonClicked(null, null);
	}

	private void HandleOkInSaveSkinClicked(object sender, EventArgs e)
	{
		UserTextureSkinData userTextureSkinData = SkinsController.AddUserSkin(skinNameInput.String_2, currentSkin, int_0);
		if (userTextureSkinData != null)
		{
			int_1 = 0;
			baseEvent_0.Subscribe(UserSave, EventType.SAVE_OK);
			baseEvent_0.Subscribe(UserSaveCheck, EventType.DATA_UPDATE_OK);
			baseEvent_0.Subscribe(UserSkinSaveError, EventType.SAVE_ERROR);
			UpdateUserSkinNetworkCommand updateUserSkinNetworkCommand = new UpdateUserSkinNetworkCommand();
			updateUserSkinNetworkCommand.userTextureSkinData_0 = userTextureSkinData;
			updateUserSkinNetworkCommand.bool_0 = false;
			AbstractNetworkCommand.Send(updateUserSkinNetworkCommand);
			ShowPreviewSkin();
			bool_1 = false;
			saveSkinPanel.SetActive(false);
			HandleBackButtonClicked(null, null);
		}
	}

	private static void UserSave(int int_2)
	{
		int_1++;
		if (int_1 == 2)
		{
			save();
		}
	}

	private static void UserSaveCheck(int int_2)
	{
		if (int_0 != int_2)
		{
			clearWeitAnswer();
			return;
		}
		int_1++;
		if (int_1 == 2)
		{
			save();
		}
	}

	private static void UserSkinSaveError(int int_2)
	{
		clearWeitAnswer();
	}

	private static void clearWeitAnswer()
	{
		int_1 = 0;
		baseEvent_0.UnsubscribeAll();
		int_0 = 0;
	}

	private static void save()
	{
		int int32_ = int_0;
		clearWeitAnswer();
		WearData wear = WearController.WearController_0.GetWear(int32_);
		if (wear != null && wear.Boolean_0)
		{
			if (wear.Boolean_1)
			{
				SkinsController.Int32_0 = int32_;
			}
			else if (wear.Boolean_2)
			{
				WearController.WearController_0.EquipWear(int32_);
			}
		}
	}

	private void HandleCancelInSaveSkinClicked(object sender, EventArgs e)
	{
		if (isSaveAndExit)
		{
			saveSkinPanel.SetActive(false);
			leavePanel.SetActive(true);
		}
		else
		{
			ShowPreviewSkin();
			saveSkinPanel.SetActive(false);
		}
	}

	private void HandleYesInLeaveSaveClicked(object sender, EventArgs e)
	{
		leavePanel.SetActive(false);
		saveSkinPanel.SetActive(true);
		isSaveAndExit = true;
	}

	private void HandleNoInLeaveSaveClicked(object sender, EventArgs e)
	{
		bool_1 = false;
		ShowPreviewSkin();
		leavePanel.SetActive(false);
		HandleBackButtonClicked(null, null);
	}

	private void ShowPreviewSkin()
	{
		foreach (GameObject item in list_0)
		{
			item.SetActive(true);
		}
		backButton.gameObject.GetComponent<UIButton>().Boolean_0 = true;
		saveButton.gameObject.GetComponent<UIButton>().Boolean_0 = true;
	}

	private void HidePreviewSkin()
	{
		foreach (GameObject item in list_0)
		{
			item.SetActive(false);
		}
		backButton.gameObject.GetComponent<UIButton>().Boolean_0 = false;
		saveButton.gameObject.GetComponent<UIButton>().Boolean_0 = false;
	}

	private void HandleSaveChangesButtonClicked(object sender, EventArgs e)
	{
		bool_0 = false;
		bool_1 = true;
		saveChangesPanel.SetActive(false);
		SavePartInTexturesParts(selectedPartName);
		currentSkin = BuildSkin(dictionary_1);
		SkinsController.SetTextureRecursivelyFrom(previewPers, currentSkin);
		UpdateTexturesPartsInDictionary();
		HandleBackButtonClicked(null, null);
	}

	private void HandleCancelInSaveChangesButtonClicked(object sender, EventArgs e)
	{
		bool_0 = false;
		saveChangesPanel.SetActive(false);
		HandleBackButtonClicked(null, null);
	}

	private void HandleSelectColorClicked(object sender, EventArgs e)
	{
		editorPanel.SetActive(false);
		colorPanel.SetActive(true);
		oldColor.Color_0 = color_0;
		newColor.Color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().Color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_1 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_0 = color_0;
		if (brashMode_0 == BrashMode.Pipette)
		{
			brashMode_0 = BrashMode.Pencil;
			pencilButton.gameObject.GetComponent<UIToggle>().Boolean_0 = true;
			pipetteButton.gameObject.GetComponent<UIToggle>().Boolean_0 = false;
			pipetteButton.gameObject.GetComponent<UIButton>().State_0 = UIButtonColor.State.Normal;
			pipetteButton.transform.Find("Checkmark").GetComponent<UISprite>().Color_0 = new Color(1f, 1f, 1f, 0f);
			HandleSelectBrashClicked(pencilButton, null);
		}
	}

	public void HandleSetColorClicked(object sender, EventArgs e)
	{
		editorPanel.SetActive(true);
		colorPanel.SetActive(false);
		color_0 = newColor.Color_0;
		colorButton.gameObject.GetComponent<UIButton>().Color_0 = color_0;
		colorButton.gameObject.GetComponent<UIButton>().color_1 = color_0;
		colorButton.gameObject.GetComponent<UIButton>().color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().Color_0 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_1 = color_0;
		okColorInPalitraButton.gameObject.GetComponent<UIButton>().color_0 = color_0;
		Storager.SetFloat("ColorForPaintR", color_0.r);
		Storager.SetFloat("ColorForPaintG", color_0.g);
		Storager.SetFloat("ColorForPaintB", color_0.b);
		for (int i = 0; i < colorOnBrashSprites.Length; i++)
		{
			colorOnBrashSprites[i].Color_0 = color_0;
		}
	}

	private void HandleSelectBrashClicked(object sender, EventArgs e)
	{
		GameObject gameObject = (sender as MonoBehaviour).gameObject;
		string text = gameObject.name;
		Debug.Log(text);
		if (text.Equals("Fill"))
		{
			brashMode_0 = BrashMode.Fill;
		}
		if (text.Equals("Brash"))
		{
			brashMode_0 = BrashMode.Brash;
		}
		if (text.Equals("Pencil"))
		{
			brashMode_0 = BrashMode.Pencil;
		}
		if (text.Equals("Eraser"))
		{
			brashMode_0 = BrashMode.Eraser;
		}
		if (text.Equals("Pipette"))
		{
			brashMode_0 = BrashMode.Pipette;
		}
	}

	private void HandleSideClicked(object sender, EventArgs e)
	{
		selectedSide = (sender as MonoBehaviour).gameObject;
		Debug.Log(selectedSide.name);
		editorPanel.SetActive(true);
		partPreviewPanel.SetActive(false);
		editorTexture.gameObject.GetComponent<EditorTextures>().SetStartCanvas((Texture2D)selectedSide.transform.GetChild(0).GetComponent<UITexture>().Texture_0);
	}

	private void HandleSaveButtonClicked(object sender, EventArgs e)
	{
		isSaveAndExit = false;
		saveSkinPanel.SetActive(true);
		HidePreviewSkin();
	}

	private void HandleBackButtonClicked(object sender, EventArgs e)
	{
		if (partPreviewPanel.activeSelf)
		{
			if (bool_0)
			{
				saveChangesPanel.SetActive(true);
				backButton.gameObject.GetComponent<UIButton>().Boolean_0 = false;
				topPart.GetComponent<UIButton>().Boolean_0 = false;
				downPart.GetComponent<UIButton>().Boolean_0 = false;
				leftPart.GetComponent<UIButton>().Boolean_0 = false;
				frontPart.GetComponent<UIButton>().Boolean_0 = false;
				rigthPart.GetComponent<UIButton>().Boolean_0 = false;
				backPart.GetComponent<UIButton>().Boolean_0 = false;
			}
			else
			{
				partPreviewPanel.SetActive(false);
				skinPreviewPanel.SetActive(true);
				backButton.gameObject.GetComponent<UIButton>().Boolean_0 = true;
				topPart.GetComponent<UIButton>().Boolean_0 = true;
				downPart.GetComponent<UIButton>().Boolean_0 = true;
				leftPart.GetComponent<UIButton>().Boolean_0 = true;
				frontPart.GetComponent<UIButton>().Boolean_0 = true;
				rigthPart.GetComponent<UIButton>().Boolean_0 = true;
				backPart.GetComponent<UIButton>().Boolean_0 = true;
			}
		}
		else if (editorPanel.activeSelf)
		{
			if (modeEditor == ModeEditor.SkinPers)
			{
				editorPanel.SetActive(false);
				partPreviewPanel.SetActive(true);
				selectedSide.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture((Texture2D)editorTexture.Texture_0);
			}
			else if (modeEditor == ModeEditor.Cape)
			{
				if (bool_0)
				{
					savePanelInEditorTexture.SetActive(true);
				}
				else
				{
					ExitFromScene();
				}
			}
		}
		else if (colorPanel.activeSelf)
		{
			editorPanel.SetActive(true);
			colorPanel.SetActive(false);
		}
		else if (skinPreviewPanel.activeSelf)
		{
			if (bool_1)
			{
				leavePanel.SetActive(true);
				HidePreviewSkin();
			}
			else
			{
				ExitFromScene();
			}
		}
	}

	private void SavePartInTexturesParts(string string_1)
	{
		Dictionary<string, Texture2D> dictionary = new Dictionary<string, Texture2D>();
		foreach (KeyValuePair<string, Texture2D> item in dictionary_1[string_1])
		{
			if (item.Key.Equals("Top"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)topPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
			if (item.Key.Equals("Down"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)downPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
			if (item.Key.Equals("Left"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)leftPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
			if (item.Key.Equals("Front"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)frontPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
			if (item.Key.Equals("Right"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)rigthPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
			if (item.Key.Equals("Back"))
			{
				dictionary.Add(item.Key, EditorTextures.CreateCopyTexture((Texture2D)backPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0));
			}
		}
		if (string_1.Equals("Arm_right") || string_1.Equals("Arm_left"))
		{
			dictionary_1.Remove("Arm_right");
			dictionary_1.Add("Arm_right", dictionary);
			dictionary_1.Remove("Arm_left");
			dictionary_1.Add("Arm_left", dictionary);
		}
		if (string_1.Equals("Foot_right") || string_1.Equals("Foot_left"))
		{
			dictionary_1.Remove("Foot_right");
			dictionary_1.Add("Foot_right", dictionary);
			dictionary_1.Remove("Foot_left");
			dictionary_1.Add("Foot_left", dictionary);
		}
		dictionary_1.Remove(string_1);
		dictionary_1.Add(string_1, dictionary);
	}

	private void SetPartsRect()
	{
		new Dictionary<string, Rect>();
		dictionary_0.Clear();
		if (modeEditor == ModeEditor.SkinPers)
		{
			Dictionary<string, Rect> dictionary = new Dictionary<string, Rect>();
			dictionary.Add("Top", new Rect(8f, 24f, 8f, 8f));
			dictionary.Add("Down", new Rect(16f, 24f, 8f, 8f));
			dictionary.Add("Left", new Rect(0f, 16f, 8f, 8f));
			dictionary.Add("Front", new Rect(8f, 16f, 8f, 8f));
			dictionary.Add("Right", new Rect(16f, 16f, 8f, 8f));
			dictionary.Add("Back", new Rect(24f, 16f, 8f, 8f));
			dictionary_0.Add("Head", dictionary);
			Dictionary<string, Rect> dictionary2 = new Dictionary<string, Rect>();
			dictionary2.Add("Top", new Rect(4f, 12f, 4f, 4f));
			dictionary2.Add("Down", new Rect(8f, 12f, 4f, 4f));
			dictionary2.Add("Left", new Rect(0f, 0f, 4f, 12f));
			dictionary2.Add("Front", new Rect(4f, 0f, 4f, 12f));
			dictionary2.Add("Right", new Rect(8f, 0f, 4f, 12f));
			dictionary2.Add("Back", new Rect(12f, 0f, 4f, 12f));
			dictionary_0.Add("Foot_left", dictionary2);
			Dictionary<string, Rect> dictionary3 = new Dictionary<string, Rect>();
			dictionary3.Add("Top", new Rect(4f, 12f, 4f, 4f));
			dictionary3.Add("Down", new Rect(8f, 12f, 4f, 4f));
			dictionary3.Add("Left", new Rect(0f, 0f, 4f, 12f));
			dictionary3.Add("Front", new Rect(4f, 0f, 4f, 12f));
			dictionary3.Add("Right", new Rect(8f, 0f, 4f, 12f));
			dictionary3.Add("Back", new Rect(12f, 0f, 4f, 12f));
			dictionary_0.Add("Foot_right", dictionary3);
			Dictionary<string, Rect> dictionary4 = new Dictionary<string, Rect>();
			dictionary4.Add("Top", new Rect(20f, 12f, 8f, 4f));
			dictionary4.Add("Down", new Rect(28f, 12f, 8f, 4f));
			dictionary4.Add("Left", new Rect(16f, 0f, 4f, 12f));
			dictionary4.Add("Front", new Rect(20f, 0f, 8f, 12f));
			dictionary4.Add("Right", new Rect(28f, 0f, 4f, 12f));
			dictionary4.Add("Back", new Rect(32f, 0f, 8f, 12f));
			dictionary_0.Add("Body", dictionary4);
			Dictionary<string, Rect> dictionary5 = new Dictionary<string, Rect>();
			dictionary5.Add("Top", new Rect(44f, 12f, 4f, 4f));
			dictionary5.Add("Down", new Rect(48f, 12f, 4f, 4f));
			dictionary5.Add("Left", new Rect(40f, 0f, 4f, 12f));
			dictionary5.Add("Front", new Rect(44f, 0f, 4f, 12f));
			dictionary5.Add("Right", new Rect(48f, 0f, 4f, 12f));
			dictionary5.Add("Back", new Rect(52f, 0f, 4f, 12f));
			dictionary_0.Add("Arm_right", dictionary5);
			Dictionary<string, Rect> dictionary6 = new Dictionary<string, Rect>();
			dictionary6.Add("Top", new Rect(44f, 12f, 4f, 4f));
			dictionary6.Add("Down", new Rect(48f, 12f, 4f, 4f));
			dictionary6.Add("Left", new Rect(40f, 0f, 4f, 12f));
			dictionary6.Add("Front", new Rect(44f, 0f, 4f, 12f));
			dictionary6.Add("Right", new Rect(48f, 0f, 4f, 12f));
			dictionary6.Add("Back", new Rect(52f, 0f, 4f, 12f));
			dictionary_0.Add("Arm_left", dictionary6);
		}
	}

	public void UpdateTexturesPartsInDictionary()
	{
		dictionary_1.Clear();
		foreach (KeyValuePair<string, Dictionary<string, Rect>> item in dictionary_0)
		{
			Dictionary<string, Texture2D> dictionary = new Dictionary<string, Texture2D>();
			foreach (KeyValuePair<string, Rect> item2 in item.Value)
			{
				Rect value = item2.Value;
				if (Debug.isDebugBuild)
				{
				}
				dictionary.Add(item2.Key, TextureFromRect(currentSkin, value));
			}
			dictionary_1.Add(item.Key, dictionary);
		}
	}

	public Texture2D BuildSkin(Dictionary<string, Dictionary<string, Texture2D>> dictionary_2)
	{
		int width = currentSkin.width;
		int height = currentSkin.height;
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, false);
		Color clear = Color.clear;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				texture2D.SetPixel(j, i, clear);
			}
		}
		foreach (KeyValuePair<string, Dictionary<string, Texture2D>> item in dictionary_2)
		{
			foreach (KeyValuePair<string, Texture2D> item2 in dictionary_2[item.Key])
			{
				texture2D.SetPixels(Mathf.RoundToInt(dictionary_0[item.Key][item2.Key].x), Mathf.RoundToInt(dictionary_0[item.Key][item2.Key].y), Mathf.RoundToInt(dictionary_0[item.Key][item2.Key].width), Mathf.RoundToInt(dictionary_0[item.Key][item2.Key].height), dictionary_2[item.Key][item2.Key].GetPixels());
			}
		}
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}

	public void SelectPart(string string_1)
	{
		if (!dictionary_1.ContainsKey(string_1))
		{
			Debug.Log("texturesParts not contain key");
			return;
		}
		bool_0 = false;
		selectedPartName = string_1;
		topPart.SetActive(false);
		downPart.SetActive(false);
		leftPart.SetActive(false);
		frontPart.SetActive(false);
		rigthPart.SetActive(false);
		backPart.SetActive(false);
		int num = 22;
		foreach (KeyValuePair<string, Texture2D> item in dictionary_1[string_1])
		{
			if (item.Key.Equals("Top"))
			{
				topPart.SetActive(true);
				topPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				topPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				topPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				topPart.transform.localPosition = new Vector3((float)(-item.Value.width) * 0.5f * (float)num, (float)(dictionary_1[string_1]["Front"].height + item.Value.height) * 0.5f * (float)num, 0f);
				topPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
			if (item.Key.Equals("Down"))
			{
				downPart.SetActive(true);
				downPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				downPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				downPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				downPart.transform.localPosition = new Vector3((float)(-item.Value.width) * 0.5f * (float)num, (float)(-(dictionary_1[string_1]["Front"].height + item.Value.height)) * 0.5f * (float)num, 0f);
				downPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
			if (item.Key.Equals("Left"))
			{
				leftPart.SetActive(true);
				leftPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				leftPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				leftPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				leftPart.transform.localPosition = new Vector3((0f - ((float)item.Value.width * 0.5f + (float)dictionary_1[string_1]["Front"].width)) * (float)num, 0f, 0f);
				leftPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
			if (item.Key.Equals("Front"))
			{
				frontPart.SetActive(true);
				frontPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				frontPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				frontPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				frontPart.transform.localPosition = new Vector3((0f - (float)item.Value.width * 0.5f) * (float)num, 0f, 0f);
				frontPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
			if (item.Key.Equals("Right"))
			{
				rigthPart.SetActive(true);
				rigthPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				rigthPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				rigthPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				rigthPart.transform.localPosition = new Vector3((float)item.Value.width * 0.5f * (float)num, 0f, 0f);
				rigthPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
			if (item.Key.Equals("Back"))
			{
				backPart.SetActive(true);
				backPart.GetComponent<BoxCollider>().size = new Vector3(item.Value.width * num, item.Value.height * num, 0f);
				backPart.transform.GetChild(0).GetComponent<UITexture>().Int32_0 = item.Value.width * num;
				backPart.transform.GetChild(0).GetComponent<UITexture>().Int32_1 = item.Value.height * num;
				backPart.transform.localPosition = new Vector3(((float)item.Value.width * 0.5f + (float)dictionary_1[string_1]["Right"].width) * (float)num, 0f, 0f);
				backPart.transform.GetChild(0).GetComponent<UITexture>().Texture_0 = EditorTextures.CreateCopyTexture(item.Value);
			}
		}
		partPreviewPanel.SetActive(true);
		skinPreviewPanel.SetActive(false);
	}

	private Texture2D TextureFromRect(Texture2D texture2D_0, Rect rect_0)
	{
		Color[] pixels = texture2D_0.GetPixels((int)rect_0.x, (int)rect_0.y, (int)rect_0.width, (int)rect_0.height);
		Texture2D texture2D = new Texture2D((int)rect_0.width, (int)rect_0.height);
		texture2D.filterMode = FilterMode.Point;
		texture2D.SetPixels(pixels);
		texture2D.Apply();
		return texture2D;
	}

	private void ExitFromScene()
	{
		if (action_0 != null)
		{
			action_0(string_0);
			currentSkin = null;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Input.ResetInputAxes();
			HandleBackButtonClicked(this, EventArgs.Empty);
		}
	}

	private void OnDestroy()
	{
		skinEditorController_0 = null;
	}
}
