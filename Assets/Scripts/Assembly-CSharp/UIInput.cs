using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UIInput : MonoBehaviour
{
	public enum InputType
	{
		Standard = 0,
		AutoCorrect = 1,
		Password = 2
	}

	public enum Validation
	{
		None = 0,
		Integer = 1,
		Float = 2,
		Alphanumeric = 3,
		Username = 4,
		Name = 5
	}

	public enum KeyboardType
	{
		Default = 0,
		ASCIICapable = 1,
		NumbersAndPunctuation = 2,
		URL = 3,
		NumberPad = 4,
		PhonePad = 5,
		NamePhonePad = 6,
		EmailAddress = 7
	}

	public enum OnReturnKey
	{
		Default = 0,
		Submit = 1,
		NewLine = 2
	}

	public delegate char OnValidate(string string_0, int int_0, char char_0);

	public static UIInput uiinput_0;

	public static UIInput uiinput_1;

	public UILabel label;

	public InputType inputType;

	public OnReturnKey onReturnKey;

	public KeyboardType keyboardType;

	public bool hideInput;

	public Validation validation;

	public int characterLimit;

	public string savedAs;

	public GameObject selectOnTab;

	public Color activeTextColor = Color.white;

	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	public Color selectionColor = new Color(1f, 0.8745098f, 47f / 85f, 0.5f);

	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	public List<EventDelegate> onChange = new List<EventDelegate>();

	public OnValidate onValidate;

	[SerializeField]
	protected string string_0;

	[NonSerialized]
	protected string string_1 = string.Empty;

	[NonSerialized]
	protected Color color_0 = Color.white;

	[NonSerialized]
	protected float float_0;

	[NonSerialized]
	protected bool bool_0 = true;

	[NonSerialized]
	protected UIWidget.Pivot pivot_0;

	[NonSerialized]
	protected bool bool_1 = true;

	protected static int int_0;

	protected static string string_2 = string.Empty;

	[NonSerialized]
	protected int int_1;

	[NonSerialized]
	protected int int_2;

	[NonSerialized]
	protected UITexture uitexture_0;

	[NonSerialized]
	protected UITexture uitexture_1;

	[NonSerialized]
	protected Texture2D texture2D_0;

	[NonSerialized]
	protected float float_1;

	[NonSerialized]
	protected float float_2;

	[NonSerialized]
	protected string string_3 = string.Empty;

	[NonSerialized]
	protected int int_3 = -1;

	public string String_0
	{
		get
		{
			return string_1;
		}
		set
		{
			if (bool_0)
			{
				Init();
			}
			string_1 = value;
			UpdateLabel();
		}
	}

	public bool Boolean_0
	{
		get
		{
			return hideInput && label != null && !label.Boolean_9 && inputType != InputType.Password;
		}
	}

	[Obsolete("Use UIInput.value instead")]
	public string String_1
	{
		get
		{
			return String_2;
		}
		set
		{
			String_2 = value;
		}
	}

	public string String_2
	{
		get
		{
			if (bool_0)
			{
				Init();
			}
			return string_0;
		}
		set
		{
			if (bool_0)
			{
				Init();
			}
			int_0 = 0;
			if (Application.platform == RuntimePlatform.BB10Player)
			{
				value = value.Replace("\\b", "\b");
			}
			value = Validate(value);
			if (!(string_0 != value))
			{
				return;
			}
			string_0 = value;
			bool_1 = false;
			if (Boolean_2)
			{
				if (string.IsNullOrEmpty(value))
				{
					int_1 = 0;
					int_2 = 0;
				}
				else
				{
					int_1 = value.Length;
					int_2 = int_1;
				}
			}
			else
			{
				SaveToPlayerPrefs(value);
			}
			UpdateLabel();
			ExecuteOnChange();
		}
	}

	[Obsolete("Use UIInput.isSelected instead")]
	public bool Boolean_1
	{
		get
		{
			return Boolean_2;
		}
		set
		{
			Boolean_2 = value;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return uiinput_1 == this;
		}
		set
		{
			if (!value)
			{
				if (Boolean_2)
				{
					UICamera.GameObject_0 = null;
				}
			}
			else
			{
				UICamera.GameObject_0 = base.gameObject;
			}
		}
	}

	public int Int32_0
	{
		get
		{
			return (!Boolean_2) ? String_2.Length : int_2;
		}
		set
		{
			if (Boolean_2)
			{
				int_2 = value;
				UpdateLabel();
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return (!Boolean_2) ? String_2.Length : int_1;
		}
		set
		{
			if (Boolean_2)
			{
				int_1 = value;
				UpdateLabel();
			}
		}
	}

	public int Int32_2
	{
		get
		{
			return (!Boolean_2) ? String_2.Length : int_2;
		}
		set
		{
			if (Boolean_2)
			{
				int_2 = value;
				UpdateLabel();
			}
		}
	}

	public UITexture UITexture_0
	{
		get
		{
			return uitexture_1;
		}
	}

	public string Validate(string string_4)
	{
		if (string.IsNullOrEmpty(string_4))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(string_4.Length);
		for (int i = 0; i < string_4.Length; i++)
		{
			char c = string_4[i];
			if (onValidate != null)
			{
				c = onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (validation != 0)
			{
				c = Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != 0)
			{
				stringBuilder.Append(c);
			}
		}
		if (characterLimit > 0 && stringBuilder.Length > characterLimit)
		{
			return stringBuilder.ToString(0, characterLimit);
		}
		return stringBuilder.ToString();
	}

	private void Start()
	{
		if (bool_1 && !string.IsNullOrEmpty(savedAs))
		{
			LoadValue();
		}
		else
		{
			String_2 = string_0.Replace("\\n", "\n");
		}
	}

	protected void Init()
	{
		if (bool_0 && label != null)
		{
			bool_0 = false;
			string_1 = label.String_0;
			color_0 = label.Color_0;
			label.Boolean_8 = false;
			if (label.Alignment_0 == NGUIText.Alignment.Justified)
			{
				label.Alignment_0 = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			pivot_0 = label.Pivot_1;
			float_0 = label.Transform_0.localPosition.x;
			UpdateLabel();
		}
	}

	protected void SaveToPlayerPrefs(string string_4)
	{
		if (!string.IsNullOrEmpty(savedAs))
		{
			if (string.IsNullOrEmpty(string_4))
			{
				PlayerPrefs.DeleteKey(savedAs);
			}
			else
			{
				PlayerPrefs.SetString(savedAs, string_4);
			}
		}
	}

	protected virtual void OnSelect(bool bool_2)
	{
		if (bool_2)
		{
			OnSelectEvent();
		}
		else
		{
			OnDeselectEvent();
		}
	}

	protected void OnSelectEvent()
	{
		uiinput_1 = this;
		if (bool_0)
		{
			Init();
		}
		if (label != null && NGUITools.GetActive(this))
		{
			int_3 = Time.frameCount;
		}
	}

	protected void OnDeselectEvent()
	{
		if (bool_0)
		{
			Init();
		}
		if (label != null && NGUITools.GetActive(this))
		{
			string_0 = String_2;
			if (string.IsNullOrEmpty(string_0))
			{
				label.String_0 = string_1;
				label.Color_0 = color_0;
			}
			else
			{
				label.String_0 = string_0;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			RestoreLabelPivot();
		}
		uiinput_1 = null;
		UpdateLabel();
	}

	private void Update()
	{
		if (!Boolean_2)
		{
			return;
		}
		if (bool_0)
		{
			Init();
		}
		if (int_3 != -1 && int_3 != Time.frameCount)
		{
			int_3 = -1;
			int_1 = 0;
			int_2 = ((!string.IsNullOrEmpty(string_0)) ? string_0.Length : 0);
			int_0 = 0;
			label.Color_0 = activeTextColor;
			Vector2 compositionCursorPos = ((!(UICamera.uicamera_0 != null) || !(UICamera.uicamera_0.Camera_0 != null)) ? label.Vector3_3[0] : UICamera.uicamera_0.Camera_0.WorldToScreenPoint(label.Vector3_3[0]));
			compositionCursorPos.y = (float)Screen.height - compositionCursorPos.y;
			Input.imeCompositionMode = IMECompositionMode.On;
			Input.compositionCursorPos = compositionCursorPos;
			UpdateLabel();
			return;
		}
		if (selectOnTab != null && Input.GetKeyDown(KeyCode.Tab))
		{
			UICamera.GameObject_0 = selectOnTab;
			return;
		}
		string compositionString = Input.compositionString;
		if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
		{
			string inputString = Input.inputString;
			for (int i = 0; i < inputString.Length; i++)
			{
				char c = inputString[i];
				if (c >= ' ' && c != '\uf700' && c != '\uf701' && c != '\uf702' && c != '\uf703')
				{
					Insert(c.ToString());
				}
			}
		}
		if (string_2 != compositionString)
		{
			int_2 = ((!string.IsNullOrEmpty(compositionString)) ? (string_0.Length + compositionString.Length) : int_1);
			string_2 = compositionString;
			UpdateLabel();
			ExecuteOnChange();
		}
		if (uitexture_1 != null && float_1 < RealTime.Single_0)
		{
			float_1 = RealTime.Single_0 + 0.5f;
			uitexture_1.enabled = !uitexture_1.enabled;
		}
		if (Boolean_2 && float_2 != label.finalAlpha)
		{
			UpdateLabel();
		}
	}

	private void OnGUI()
	{
		if (Boolean_2 && Event.current.rawType == EventType.KeyDown)
		{
			ProcessEvent(Event.current);
		}
	}

	protected void DoBackspace()
	{
		if (string.IsNullOrEmpty(string_0))
		{
			return;
		}
		if (int_1 == int_2)
		{
			if (int_1 < 1)
			{
				return;
			}
			int_2--;
		}
		Insert(string.Empty);
	}

	protected virtual bool ProcessEvent(Event event_0)
	{
		if (label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = ((platform != 0 && platform != RuntimePlatform.OSXPlayer) ? ((event_0.modifiers & EventModifiers.Control) != 0) : ((event_0.modifiers & EventModifiers.Command) != 0));
		bool flag2 = (event_0.modifiers & EventModifiers.Shift) != 0;
		switch (event_0.keyCode)
		{
		case KeyCode.UpArrow:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = label.GetCharacterIndex(int_2, KeyCode.UpArrow);
				if (int_2 != 0)
				{
					int_2 += int_0;
				}
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.DownArrow:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = label.GetCharacterIndex(int_2, KeyCode.DownArrow);
				if (int_2 != label.String_1.Length)
				{
					int_2 += int_0;
				}
				else
				{
					int_2 = string_0.Length;
				}
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.RightArrow:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = Mathf.Min(int_2 + 1, string_0.Length);
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.LeftArrow:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = Mathf.Max(int_2 - 1, 0);
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.A:
			if (flag)
			{
				event_0.Use();
				int_1 = 0;
				int_2 = string_0.Length;
				UpdateLabel();
			}
			return true;
		case KeyCode.V:
			if (flag)
			{
				event_0.Use();
				Insert(NGUITools.String_0);
			}
			return true;
		default:
			return false;
		case KeyCode.Delete:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				if (int_1 == int_2)
				{
					if (int_1 >= string_0.Length)
					{
						return true;
					}
					int_2++;
				}
				Insert(string.Empty);
			}
			return true;
		case KeyCode.Return:
		case KeyCode.KeypadEnter:
			event_0.Use();
			if (onReturnKey == OnReturnKey.NewLine || (onReturnKey == OnReturnKey.Default && label.Boolean_9 && !flag && label.Overflow_0 != UILabel.Overflow.ClampContent && validation == Validation.None))
			{
				Insert("\n");
			}
			else
			{
				UICamera.controlScheme_0 = UICamera.ControlScheme.Controller;
				UICamera.keyCode_0 = event_0.keyCode;
				Submit();
				UICamera.keyCode_0 = KeyCode.None;
			}
			return true;
		case KeyCode.Backspace:
			event_0.Use();
			DoBackspace();
			return true;
		case KeyCode.X:
			if (flag)
			{
				event_0.Use();
				NGUITools.String_0 = GetSelection();
				Insert(string.Empty);
			}
			return true;
		case KeyCode.C:
			if (flag)
			{
				event_0.Use();
				NGUITools.String_0 = GetSelection();
			}
			return true;
		case KeyCode.Home:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				if (label.Boolean_9)
				{
					int_2 = label.GetCharacterIndex(int_2, KeyCode.Home);
				}
				else
				{
					int_2 = 0;
				}
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.End:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				if (label.Boolean_9)
				{
					int_2 = label.GetCharacterIndex(int_2, KeyCode.End);
				}
				else
				{
					int_2 = string_0.Length;
				}
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.PageUp:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = 0;
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		case KeyCode.PageDown:
			event_0.Use();
			if (!string.IsNullOrEmpty(string_0))
			{
				int_2 = string_0.Length;
				if (!flag2)
				{
					int_1 = int_2;
				}
				UpdateLabel();
			}
			return true;
		}
	}

	protected virtual void Insert(string string_4)
	{
		string leftText = GetLeftText();
		string rightText = GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + string_4.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		for (int length2 = string_4.Length; i < length2; i++)
		{
			char c = string_4[i];
			if (c == '\b')
			{
				DoBackspace();
				continue;
			}
			if (characterLimit > 0 && stringBuilder.Length + length >= characterLimit)
			{
				break;
			}
			if (onValidate != null)
			{
				c = onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (validation != 0)
			{
				c = Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != 0)
			{
				stringBuilder.Append(c);
			}
		}
		int_1 = stringBuilder.Length;
		int_2 = int_1;
		int j = 0;
		for (int length3 = rightText.Length; j < length3; j++)
		{
			char c2 = rightText[j];
			if (onValidate != null)
			{
				c2 = onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (validation != 0)
			{
				c2 = Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != 0)
			{
				stringBuilder.Append(c2);
			}
		}
		string_0 = stringBuilder.ToString();
		UpdateLabel();
		ExecuteOnChange();
	}

	protected string GetLeftText()
	{
		int num = Mathf.Min(int_1, int_2);
		return (string.IsNullOrEmpty(string_0) || num < 0) ? string.Empty : string_0.Substring(0, num);
	}

	protected string GetRightText()
	{
		int num = Mathf.Max(int_1, int_2);
		return (string.IsNullOrEmpty(string_0) || num >= string_0.Length) ? string.Empty : string_0.Substring(num);
	}

	protected string GetSelection()
	{
		if (!string.IsNullOrEmpty(string_0) && int_1 != int_2)
		{
			int num = Mathf.Min(int_1, int_2);
			int num2 = Mathf.Max(int_1, int_2);
			return string_0.Substring(num, num2 - num);
		}
		return string.Empty;
	}

	protected int GetCharUnderMouse()
	{
		Vector3[] vector3_ = label.Vector3_3;
		Ray ray_ = UICamera.Ray_0;
		float enter;
		return new Plane(vector3_[0], vector3_[1], vector3_[2]).Raycast(ray_, out enter) ? (int_0 + label.GetCharacterIndexAtPosition(ray_.GetPoint(enter))) : 0;
	}

	protected virtual void OnPress(bool bool_2)
	{
		if (bool_2 && Boolean_2 && label != null && (UICamera.controlScheme_0 == UICamera.ControlScheme.Mouse || UICamera.controlScheme_0 == UICamera.ControlScheme.Touch))
		{
			Int32_2 = GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				Int32_1 = int_2;
			}
		}
	}

	protected virtual void OnDrag(Vector2 vector2_0)
	{
		if (label != null && (UICamera.controlScheme_0 == UICamera.ControlScheme.Mouse || UICamera.controlScheme_0 == UICamera.ControlScheme.Touch))
		{
			Int32_2 = GetCharUnderMouse();
		}
	}

	private void OnDisable()
	{
		Cleanup();
	}

	protected virtual void Cleanup()
	{
		if ((bool)uitexture_0)
		{
			uitexture_0.enabled = false;
		}
		if ((bool)uitexture_1)
		{
			uitexture_1.enabled = false;
		}
		if ((bool)texture2D_0)
		{
			NGUITools.Destroy(texture2D_0);
			texture2D_0 = null;
		}
	}

	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			string_0 = String_2;
			if (uiinput_0 == null)
			{
				uiinput_0 = this;
				EventDelegate.Execute(onSubmit);
				uiinput_0 = null;
			}
			SaveToPlayerPrefs(string_0);
		}
	}

	public void UpdateLabel()
	{
		if (!(label != null))
		{
			return;
		}
		if (bool_0)
		{
			Init();
		}
		bool boolean_ = Boolean_2;
		string text = String_2;
		bool flag = string.IsNullOrEmpty(text) && string.IsNullOrEmpty(Input.compositionString);
		label.Color_0 = ((!flag || boolean_) ? activeTextColor : color_0);
		string text2;
		if (flag)
		{
			text2 = ((!boolean_) ? string_1 : string.Empty);
			RestoreLabelPivot();
		}
		else
		{
			if (inputType == InputType.Password)
			{
				text2 = string.Empty;
				string text3 = "*";
				if (label.UIFont_1 != null && label.UIFont_1.BMFont_0 != null && label.UIFont_1.BMFont_0.GetGlyph(42) == null)
				{
					text3 = "x";
				}
				int i = 0;
				for (int length = text.Length; i < length; i++)
				{
					text2 += text3;
				}
			}
			else
			{
				text2 = text;
			}
			int num = (boolean_ ? Mathf.Min(text2.Length, Int32_0) : 0);
			string text4 = text2.Substring(0, num);
			if (boolean_)
			{
				text4 += Input.compositionString;
			}
			text2 = text4 + text2.Substring(num, text2.Length - num);
			if (boolean_ && label.Overflow_0 == UILabel.Overflow.ClampContent && label.Int32_10 == 1)
			{
				int num2 = label.CalculateOffsetToFit(text2);
				if (num2 == 0)
				{
					int_0 = 0;
					RestoreLabelPivot();
				}
				else if (num < int_0)
				{
					int_0 = num;
					SetPivotToLeft();
				}
				else if (num2 < int_0)
				{
					int_0 = num2;
					SetPivotToLeft();
				}
				else
				{
					num2 = label.CalculateOffsetToFit(text2.Substring(0, num));
					if (num2 > int_0)
					{
						int_0 = num2;
						SetPivotToRight();
					}
				}
				if (int_0 != 0)
				{
					text2 = text2.Substring(int_0, text2.Length - int_0);
				}
			}
			else
			{
				int_0 = 0;
				RestoreLabelPivot();
			}
		}
		label.String_0 = text2;
		if (boolean_)
		{
			int num3 = int_1 - int_0;
			int num4 = int_2 - int_0;
			if (texture2D_0 == null)
			{
				texture2D_0 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						texture2D_0.SetPixel(k, j, Color.white);
					}
				}
				texture2D_0.Apply();
			}
			if (num3 != num4)
			{
				if (uitexture_0 == null)
				{
					uitexture_0 = NGUITools.AddWidget<UITexture>(label.GameObject_0);
					uitexture_0.name = "Input Highlight";
					uitexture_0.Texture_0 = texture2D_0;
					uitexture_0.bool_8 = false;
					uitexture_0.Pivot_1 = label.Pivot_1;
					uitexture_0.SetAnchor(label.Transform_0);
				}
				else
				{
					uitexture_0.Pivot_1 = label.Pivot_1;
					uitexture_0.Texture_0 = texture2D_0;
					uitexture_0.MarkAsChanged();
					uitexture_0.enabled = true;
				}
			}
			if (uitexture_1 == null)
			{
				uitexture_1 = NGUITools.AddWidget<UITexture>(label.GameObject_0);
				uitexture_1.name = "Input Caret";
				uitexture_1.Texture_0 = texture2D_0;
				uitexture_1.bool_8 = false;
				uitexture_1.Pivot_1 = label.Pivot_1;
				uitexture_1.SetAnchor(label.Transform_0);
			}
			else
			{
				uitexture_1.Pivot_1 = label.Pivot_1;
				uitexture_1.Texture_0 = texture2D_0;
				uitexture_1.MarkAsChanged();
				uitexture_1.enabled = true;
			}
			if (num3 != num4)
			{
				label.PrintOverlay(num3, num4, uitexture_1.uigeometry_0, uitexture_0.uigeometry_0, caretColor, selectionColor);
				uitexture_0.enabled = uitexture_0.uigeometry_0.Boolean_0;
			}
			else
			{
				label.PrintOverlay(num3, num4, uitexture_1.uigeometry_0, null, caretColor, selectionColor);
				if (uitexture_0 != null)
				{
					uitexture_0.enabled = false;
				}
			}
			float_1 = RealTime.Single_0 + 0.5f;
			float_2 = label.finalAlpha;
		}
		else
		{
			Cleanup();
		}
	}

	protected void SetPivotToLeft()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(pivot_0);
		pivotOffset.x = 0f;
		label.Pivot_1 = NGUIMath.GetPivot(pivotOffset);
	}

	protected void SetPivotToRight()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(pivot_0);
		pivotOffset.x = 1f;
		label.Pivot_1 = NGUIMath.GetPivot(pivotOffset);
	}

	protected void RestoreLabelPivot()
	{
		if (label != null && label.Pivot_1 != pivot_0)
		{
			label.Pivot_1 = pivot_0;
		}
	}

	protected char Validate(string string_4, int int_4, char char_0)
	{
		if (validation != 0 && base.enabled)
		{
			if (validation == Validation.Integer)
			{
				if (char_0 >= '0' && char_0 <= '9')
				{
					return char_0;
				}
				if (char_0 == '-' && int_4 == 0 && !string_4.Contains("-"))
				{
					return char_0;
				}
			}
			else if (validation == Validation.Float)
			{
				if (char_0 >= '0' && char_0 <= '9')
				{
					return char_0;
				}
				if (char_0 == '-' && int_4 == 0 && !string_4.Contains("-"))
				{
					return char_0;
				}
				if (char_0 == '.' && !string_4.Contains("."))
				{
					return char_0;
				}
			}
			else if (validation == Validation.Alphanumeric)
			{
				if (char_0 >= 'A' && char_0 <= 'Z')
				{
					return char_0;
				}
				if (char_0 >= 'a' && char_0 <= 'z')
				{
					return char_0;
				}
				if (char_0 >= '0' && char_0 <= '9')
				{
					return char_0;
				}
			}
			else if (validation == Validation.Username)
			{
				if (char_0 >= 'A' && char_0 <= 'Z')
				{
					return (char)(char_0 - 65 + 97);
				}
				if (char_0 >= 'a' && char_0 <= 'z')
				{
					return char_0;
				}
				if (char_0 >= '0' && char_0 <= '9')
				{
					return char_0;
				}
			}
			else if (validation == Validation.Name)
			{
				char c = ((string_4.Length <= 0) ? ' ' : string_4[Mathf.Clamp(int_4, 0, string_4.Length - 1)]);
				char c2 = ((string_4.Length <= 0) ? '\n' : string_4[Mathf.Clamp(int_4 + 1, 0, string_4.Length - 1)]);
				if (char_0 >= 'a' && char_0 <= 'z')
				{
					if (c == ' ')
					{
						return (char)(char_0 - 97 + 65);
					}
					return char_0;
				}
				if (char_0 >= 'A' && char_0 <= 'Z')
				{
					if (c != ' ' && c != '\'')
					{
						return (char)(char_0 - 65 + 97);
					}
					return char_0;
				}
				switch (char_0)
				{
				case '\'':
					if (c != ' ' && c != '\'' && c2 != '\'' && !string_4.Contains("'"))
					{
						return char_0;
					}
					break;
				case ' ':
					if (c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
					{
						return char_0;
					}
					break;
				}
			}
			return '\0';
		}
		return char_0;
	}

	protected void ExecuteOnChange()
	{
		if (uiinput_0 == null && EventDelegate.IsValid(onChange))
		{
			uiinput_0 = this;
			EventDelegate.Execute(onChange);
			uiinput_0 = null;
		}
	}

	public void RemoveFocus()
	{
		Boolean_2 = false;
	}

	public void SaveValue()
	{
		SaveToPlayerPrefs(string_0);
	}

	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(savedAs))
		{
			string text = string_0.Replace("\\n", "\n");
			string_0 = string.Empty;
			String_2 = ((!PlayerPrefs.HasKey(savedAs)) ? text : PlayerPrefs.GetString(savedAs));
		}
	}
}
