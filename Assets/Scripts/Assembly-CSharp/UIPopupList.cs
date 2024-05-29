using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupList : UIWidgetContainer
{
	public enum Position
	{
		Auto = 0,
		Above = 1,
		Below = 2
	}

	public delegate void LegacyEvent(string string_0);

	private const float float_0 = 0.15f;

	public static UIPopupList uipopupList_0;

	public UIAtlas atlas;

	public UIFont bitmapFont;

	public Font trueTypeFont;

	public int fontSize = 16;

	public FontStyle fontStyle;

	public string backgroundSprite;

	public string highlightSprite;

	public Position position;

	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	public List<string> items = new List<string>();

	public Vector2 padding = new Vector3(4f, 4f);

	public Color textColor = Color.white;

	public Color backgroundColor = Color.white;

	public Color highlightColor = new Color(0.88235295f, 40f / 51f, 0.5882353f, 1f);

	public bool isAnimated = true;

	public bool isLocalized;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[SerializeField]
	private string string_0;

	private UIPanel uipanel_0;

	private GameObject gameObject_0;

	private UISprite uisprite_0;

	private UISprite uisprite_1;

	private UILabel uilabel_0;

	private List<UILabel> list_0 = new List<UILabel>();

	private float float_1;

	[SerializeField]
	private GameObject gameObject_1;

	[SerializeField]
	private string string_1 = "OnSelectionChange";

	[SerializeField]
	private float float_2;

	[SerializeField]
	private UIFont uifont_0;

	[SerializeField]
	private UILabel uilabel_1;

	private LegacyEvent legacyEvent_0;

	private bool bool_0;

	private bool bool_1;

	public UnityEngine.Object Object_0
	{
		get
		{
			if (trueTypeFont != null)
			{
				return trueTypeFont;
			}
			if (bitmapFont != null)
			{
				return bitmapFont;
			}
			return uifont_0;
		}
		set
		{
			if (value is Font)
			{
				trueTypeFont = value as Font;
				bitmapFont = null;
				uifont_0 = null;
			}
			else if (value is UIFont)
			{
				bitmapFont = value as UIFont;
				trueTypeFont = null;
				uifont_0 = null;
			}
		}
	}

	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public LegacyEvent LegacyEvent_0
	{
		get
		{
			return legacyEvent_0;
		}
		set
		{
			legacyEvent_0 = value;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return gameObject_0 != null;
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
			if (string_0 != null && string_0 != null)
			{
				TriggerCallbacks();
			}
		}
	}

	[Obsolete("Use 'value' instead")]
	public string String_1
	{
		get
		{
			return String_0;
		}
		set
		{
			String_0 = value;
		}
	}

	private bool Boolean_1
	{
		get
		{
			UIKeyNavigation component = GetComponent<UIKeyNavigation>();
			return component == null || !component.enabled;
		}
		set
		{
			UIKeyNavigation component = GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	private bool Boolean_2
	{
		get
		{
			return bitmapFont != null || trueTypeFont != null;
		}
	}

	private int Int32_0
	{
		get
		{
			return (trueTypeFont != null || bitmapFont == null) ? fontSize : bitmapFont.Int32_3;
		}
	}

	private float Single_0
	{
		get
		{
			return (trueTypeFont != null || bitmapFont == null) ? 1f : ((float)fontSize / (float)bitmapFont.Int32_3);
		}
	}

	protected void TriggerCallbacks()
	{
		if (uipopupList_0 != this)
		{
			UIPopupList uIPopupList = uipopupList_0;
			uipopupList_0 = this;
			if (legacyEvent_0 != null)
			{
				legacyEvent_0(string_0);
			}
			if (EventDelegate.IsValid(onChange))
			{
				EventDelegate.Execute(onChange);
			}
			else if (gameObject_1 != null && !string.IsNullOrEmpty(string_1))
			{
				gameObject_1.SendMessage(string_1, string_0, SendMessageOptions.DontRequireReceiver);
			}
			uipopupList_0 = uIPopupList;
		}
	}

	private void OnEnable()
	{
		if (EventDelegate.IsValid(onChange))
		{
			gameObject_1 = null;
			string_1 = null;
		}
		if (uifont_0 != null)
		{
			if (uifont_0.Boolean_4)
			{
				trueTypeFont = uifont_0.Font_0;
				fontStyle = uifont_0.FontStyle_0;
				bool_0 = true;
			}
			else if (bitmapFont == null)
			{
				bitmapFont = uifont_0;
				bool_0 = false;
			}
			uifont_0 = null;
		}
		if (float_2 != 0f)
		{
			fontSize = ((!(bitmapFont != null)) ? 16 : Mathf.RoundToInt((float)bitmapFont.Int32_3 * float_2));
			float_2 = 0f;
		}
		if (trueTypeFont == null && bitmapFont != null && bitmapFont.Boolean_4)
		{
			trueTypeFont = bitmapFont.Font_0;
			bitmapFont = null;
		}
	}

	private void OnValidate()
	{
		Font font = trueTypeFont;
		UIFont uIFont = bitmapFont;
		bitmapFont = null;
		trueTypeFont = null;
		if (font != null && (uIFont == null || !bool_0))
		{
			bitmapFont = null;
			trueTypeFont = font;
			bool_0 = true;
		}
		else if (uIFont != null)
		{
			if (uIFont.Boolean_4)
			{
				trueTypeFont = uIFont.Font_0;
				fontStyle = uIFont.FontStyle_0;
				fontSize = uIFont.Int32_3;
				bool_0 = true;
			}
			else
			{
				bitmapFont = uIFont;
				bool_0 = false;
			}
		}
		else
		{
			trueTypeFont = font;
			bool_0 = true;
		}
	}

	private void Start()
	{
		if (uilabel_1 != null)
		{
			EventDelegate.Add(onChange, uilabel_1.SetCurrentSelection);
			uilabel_1 = null;
		}
		if (!Application.isPlaying)
		{
			return;
		}
		if (string.IsNullOrEmpty(string_0))
		{
			if (items.Count > 0)
			{
				String_0 = items[0];
			}
		}
		else
		{
			string text = string_0;
			string_0 = null;
			String_0 = text;
		}
	}

	private void OnLocalize()
	{
		if (isLocalized)
		{
			TriggerCallbacks();
		}
	}

	private void Highlight(UILabel uilabel_2, bool bool_2)
	{
		if (!(uisprite_1 != null))
		{
			return;
		}
		uilabel_0 = uilabel_2;
		UISpriteData atlasSprite = uisprite_1.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Vector3 highlightPosition = GetHighlightPosition();
		if (!bool_2 && isAnimated)
		{
			TweenPosition.Begin(uisprite_1.gameObject, 0.1f, highlightPosition).method_0 = UITweener.Method.EaseOut;
			if (!bool_1)
			{
				bool_1 = true;
				StartCoroutine(UpdateTweenPosition());
			}
		}
		else
		{
			uisprite_1.Transform_0.localPosition = highlightPosition;
		}
	}

	private Vector3 GetHighlightPosition()
	{
		if (uilabel_0 == null)
		{
			return Vector3.zero;
		}
		if (uisprite_1 == null)
		{
			return Vector3.zero;
		}
		UISpriteData atlasSprite = uisprite_1.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return Vector3.zero;
		}
		float single_ = atlas.Single_0;
		float num = (float)atlasSprite.borderLeft * single_;
		float y = (float)atlasSprite.borderTop * single_;
		return uilabel_0.Transform_0.localPosition + new Vector3(0f - num, y, 1f);
	}

	private IEnumerator UpdateTweenPosition()
	{
		if (uisprite_1 != null && uilabel_0 != null)
		{
			TweenPosition component = uisprite_1.GetComponent<TweenPosition>();
			while (component != null && component.enabled)
			{
				component.vector3_1 = GetHighlightPosition();
				yield return null;
			}
		}
		bool_1 = false;
	}

	private void OnItemHover(GameObject gameObject_2, bool bool_2)
	{
		if (bool_2)
		{
			UILabel component = gameObject_2.GetComponent<UILabel>();
			Highlight(component, false);
		}
	}

	private void Select(UILabel uilabel_2, bool bool_2)
	{
		Highlight(uilabel_2, bool_2);
		UIEventListener component = uilabel_2.gameObject.GetComponent<UIEventListener>();
		String_0 = component.parameter as string;
		UIPlaySound[] components = GetComponents<UIPlaySound>();
		int i = 0;
		for (int num = components.Length; i < num; i++)
		{
			UIPlaySound uIPlaySound = components[i];
			if (uIPlaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uIPlaySound.audioClip, uIPlaySound.volume, 1f);
			}
		}
	}

	private void OnItemPress(GameObject gameObject_2, bool bool_2)
	{
		if (bool_2)
		{
			Select(gameObject_2.GetComponent<UILabel>(), true);
		}
	}

	private void OnItemClick(GameObject gameObject_2)
	{
		Close();
	}

	private void OnKey(KeyCode keyCode_0)
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !Boolean_1)
		{
			return;
		}
		int num = list_0.IndexOf(uilabel_0);
		if (num == -1)
		{
			num = 0;
		}
		switch (keyCode_0)
		{
		case KeyCode.UpArrow:
			if (num > 0)
			{
				Select(list_0[--num], false);
			}
			break;
		case KeyCode.DownArrow:
			if (num + 1 < list_0.Count)
			{
				Select(list_0[++num], false);
			}
			break;
		case KeyCode.Escape:
			OnSelect(false);
			break;
		}
	}

	private void OnSelect(bool bool_2)
	{
		if (!bool_2)
		{
			Close();
		}
	}

	public void Close()
	{
		if (!(gameObject_0 != null))
		{
			return;
		}
		list_0.Clear();
		Boolean_1 = false;
		if (isAnimated)
		{
			UIWidget[] componentsInChildren = gameObject_0.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				Color color_ = uIWidget.Color_0;
				color_.a = 0f;
				TweenColor.Begin(uIWidget.gameObject, 0.15f, color_).method_0 = UITweener.Method.EaseOut;
			}
			Collider[] componentsInChildren2 = gameObject_0.GetComponentsInChildren<Collider>();
			int j = 0;
			for (int num2 = componentsInChildren2.Length; j < num2; j++)
			{
				componentsInChildren2[j].enabled = false;
			}
			UnityEngine.Object.Destroy(gameObject_0, 0.15f);
		}
		else
		{
			UnityEngine.Object.Destroy(gameObject_0);
		}
		uisprite_0 = null;
		uisprite_1 = null;
		gameObject_0 = null;
	}

	private void AnimateColor(UIWidget uiwidget_0)
	{
		Color color_ = uiwidget_0.Color_0;
		uiwidget_0.Color_0 = new Color(color_.r, color_.g, color_.b, 0f);
		TweenColor.Begin(uiwidget_0.gameObject, 0.15f, color_).method_0 = UITweener.Method.EaseOut;
	}

	private void AnimatePosition(UIWidget uiwidget_0, bool bool_2, float float_3)
	{
		Vector3 localPosition = uiwidget_0.Transform_0.localPosition;
		Vector3 localPosition2 = ((!bool_2) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, float_3, localPosition.z));
		uiwidget_0.Transform_0.localPosition = localPosition2;
		GameObject gameObject = uiwidget_0.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method_0 = UITweener.Method.EaseOut;
	}

	private void AnimateScale(UIWidget uiwidget_0, bool bool_2, float float_3)
	{
		GameObject gameObject = uiwidget_0.gameObject;
		Transform transform_ = uiwidget_0.Transform_0;
		float num = (float)Int32_0 * Single_0 + float_1 * 2f;
		transform_.localScale = new Vector3(1f, num / (float)uiwidget_0.Int32_1, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method_0 = UITweener.Method.EaseOut;
		if (bool_2)
		{
			Vector3 localPosition = transform_.localPosition;
			transform_.localPosition = new Vector3(localPosition.x, localPosition.y - (float)uiwidget_0.Int32_1 + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method_0 = UITweener.Method.EaseOut;
		}
	}

	private void Animate(UIWidget uiwidget_0, bool bool_2, float float_3)
	{
		AnimateColor(uiwidget_0);
		AnimatePosition(uiwidget_0, bool_2, float_3);
	}

	private void OnClick()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && gameObject_0 == null && atlas != null && Boolean_2 && items.Count > 0)
		{
			list_0.Clear();
			if (uipanel_0 == null)
			{
				uipanel_0 = UIPanel.Find(base.transform);
				if (uipanel_0 == null)
				{
					return;
				}
			}
			Boolean_1 = true;
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			gameObject_0 = new GameObject("Drop-down List");
			gameObject_0.layer = base.gameObject.layer;
			Transform transform2 = gameObject_0.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			uisprite_0 = NGUITools.AddSprite(gameObject_0, atlas, backgroundSprite);
			uisprite_0.Pivot_1 = UIWidget.Pivot.TopLeft;
			uisprite_0.Int32_2 = NGUITools.CalculateNextDepth(uipanel_0.gameObject);
			uisprite_0.Color_0 = backgroundColor;
			Vector4 vector4_ = uisprite_0.Vector4_3;
			float_1 = vector4_.y;
			uisprite_0.Transform_0.localPosition = new Vector3(0f, vector4_.y, 0f);
			uisprite_1 = NGUITools.AddSprite(gameObject_0, atlas, highlightSprite);
			uisprite_1.Pivot_1 = UIWidget.Pivot.TopLeft;
			uisprite_1.Color_0 = highlightColor;
			UISpriteData atlasSprite = uisprite_1.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = atlasSprite.borderTop;
			float num2 = Int32_0;
			float single_ = Single_0;
			float num3 = num2 * single_;
			float a = 0f;
			float num4 = 0f - padding.y;
			int int32_ = ((!(bitmapFont != null)) ? fontSize : bitmapFont.Int32_3);
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			for (int count = items.Count; i < count; i++)
			{
				string text = items[i];
				UILabel uILabel = NGUITools.AddWidget<UILabel>(gameObject_0);
				uILabel.name = i.ToString();
				uILabel.Pivot_1 = UIWidget.Pivot.TopLeft;
				uILabel.UIFont_1 = bitmapFont;
				uILabel.Font_0 = trueTypeFont;
				uILabel.Int32_5 = int32_;
				uILabel.FontStyle_0 = fontStyle;
				uILabel.String_0 = ((!isLocalized) ? text : Localization.Get(text));
				uILabel.Color_0 = textColor;
				uILabel.Transform_0.localPosition = new Vector3(vector4_.x + padding.x, num4, -1f);
				uILabel.Overflow_0 = UILabel.Overflow.ResizeFreely;
				uILabel.Alignment_0 = alignment;
				uILabel.MakePixelPerfect();
				if (single_ != 1f)
				{
					uILabel.Transform_0.localScale = Vector3.one * single_;
				}
				list.Add(uILabel);
				num4 -= num3;
				num4 -= padding.y;
				a = Mathf.Max(a, uILabel.Vector2_3.x);
				UIEventListener uIEventListener = UIEventListener.Get(uILabel.gameObject);
				uIEventListener.onHover = OnItemHover;
				uIEventListener.onPress = OnItemPress;
				uIEventListener.onClick = OnItemClick;
				uIEventListener.parameter = text;
				if (string_0 == text || (i == 0 && string.IsNullOrEmpty(string_0)))
				{
					Highlight(uILabel, true);
				}
				list_0.Add(uILabel);
			}
			a = Mathf.Max(a, bounds.size.x * single_ - (vector4_.x + padding.x) * 2f);
			float num5 = a / single_;
			Vector3 vector = new Vector3(num5 * 0.5f, (0f - num2) * 0.5f, 0f);
			Vector3 vector2 = new Vector3(num5, (num3 + padding.y) / single_, 1f);
			int j = 0;
			for (int count2 = list.Count; j < count2; j++)
			{
				UILabel uILabel2 = list[j];
				NGUITools.AddWidgetCollider(uILabel2.gameObject);
				uILabel2.bool_6 = false;
				BoxCollider component = uILabel2.GetComponent<BoxCollider>();
				if (component != null)
				{
					vector.z = component.center.z;
					component.center = vector;
					component.size = vector2;
				}
				else
				{
					BoxCollider2D component2 = uILabel2.GetComponent<BoxCollider2D>();
					component2.offset = vector;
					component2.size = vector2;
				}
			}
			int int32_2 = Mathf.RoundToInt(a);
			a += (vector4_.x + padding.x) * 2f;
			num4 -= vector4_.y;
			uisprite_0.Int32_0 = Mathf.RoundToInt(a);
			uisprite_0.Int32_1 = Mathf.RoundToInt(0f - num4 + vector4_.y);
			int k = 0;
			for (int count3 = list.Count; k < count3; k++)
			{
				UILabel uILabel3 = list[k];
				uILabel3.Overflow_0 = UILabel.Overflow.ShrinkContent;
				uILabel3.Int32_0 = int32_2;
			}
			float num6 = 2f * atlas.Single_0;
			float f = a - (vector4_.x + padding.x) * 2f + (float)atlasSprite.borderLeft * num6;
			float f2 = num3 + num * num6;
			uisprite_1.Int32_0 = Mathf.RoundToInt(f);
			uisprite_1.Int32_1 = Mathf.RoundToInt(f2);
			bool flag = position == Position.Above;
			if (position == Position.Auto)
			{
				UICamera uICamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uICamera != null)
				{
					flag = uICamera.Camera_0.WorldToViewportPoint(transform.position).y < 0.5f;
				}
			}
			if (isAnimated)
			{
				float float_ = num4 + num3;
				Animate(uisprite_1, flag, float_);
				int l = 0;
				for (int count4 = list.Count; l < count4; l++)
				{
					Animate(list[l], flag, float_);
				}
				AnimateColor(uisprite_0);
				AnimateScale(uisprite_0, flag, float_);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num4 - vector4_.y, bounds.min.z);
			}
		}
		else
		{
			OnSelect(false);
		}
	}
}
