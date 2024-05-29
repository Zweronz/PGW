using System;
using UnityEngine;

public class SkinEditorToolItem : MonoBehaviour
{
	public enum ToolType
	{
		PENCIL = 0,
		BRUSH = 1,
		POURING = 2,
		DROPPER = 3,
		ERASER = 4
	}

	public ToolType toolType;

	public UISprite toolColor;

	public UISprite toolSelector;

	public UILabel toolLabel;

	private Action<ToolType> action_0;

	private UIPlaySound uiplaySound_0;

	private bool bool_0;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			NGUITools.SetActive(toolSelector.gameObject, value);
		}
	}

	private void Start()
	{
		Boolean_0 = false;
		OnColorChanged();
		SkinEditController.SkinEditController_0.Subscribe(OnColorChanged, SkinEditController.SkinEditorEvent.COLOR_CHANGED);
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	private void OnDestroy()
	{
		SkinEditController.SkinEditController_0.Unsubscribe(OnColorChanged, SkinEditController.SkinEditorEvent.COLOR_CHANGED);
	}

	public void SetOnSelectCallback(Action<ToolType> action_1)
	{
		action_0 = action_1;
	}

	private void OnClick()
	{
		if (action_0 != null)
		{
			action_0(toolType);
		}
		if (uiplaySound_0 != null && Defs.Boolean_0)
		{
			uiplaySound_0.Play();
		}
		Boolean_0 = true;
	}

	private void OnColorChanged(int int_0 = 0)
	{
		if (toolColor != null)
		{
			toolColor.Color_0 = SkinEditController.SkinEditController_0.color_0;
		}
		toolSelector.Color_0 = SkinEditController.SkinEditController_0.color_0;
	}
}
