using System;
using UnityEngine;
using engine.helpers;

public class SkinEditorPartItem : MonoBehaviour
{
	public enum PartItemType
	{
		LEFT = 0,
		RIGHT = 1,
		FRONT = 2,
		BACK = 3,
		UP = 4,
		DOWN = 5
	}

	public PartItemType itemType;

	public UITexture itemTexture;

	public UISprite itemSelector;

	public UISprite itemGrid;

	public Rect rect;

	private Action<SkinEditorPartItem> action_0;

	private bool bool_0;

	private float float_0 = 1f;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			NGUITools.SetActive(itemSelector.gameObject, value);
		}
	}

	private void Start()
	{
		Boolean_0 = false;
		itemGrid.Single_2 = float_0;
		InitTexture();
	}

	public void InitTexture()
	{
		itemTexture.Texture_0 = Utility.TextureFromRect(SkinEditController.SkinEditController_0.texture2D_0, rect);
	}

	public void SetOnSelectCallback(Action<SkinEditorPartItem> action_1)
	{
		action_0 = action_1;
	}

	private void OnClick()
	{
		if (action_0 != null)
		{
			action_0(this);
		}
		Boolean_0 = true;
	}
}
