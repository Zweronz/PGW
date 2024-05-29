using System;
using UnityEngine;

public class SkinEditorPartScheme : MonoBehaviour
{
	public enum PartType
	{
		HEAD = 0,
		BODY = 1,
		ARM = 2,
		FOOT = 3
	}

	public PartType partType;

	public SkinEditorPartItem[] items;

	private Action<SkinEditorPartItem> action_0;

	private void Start()
	{
		SkinEditorPartItem[] array = items;
		foreach (SkinEditorPartItem skinEditorPartItem in array)
		{
			skinEditorPartItem.SetOnSelectCallback(OnSelectedItem);
		}
		items[0].Invoke("OnClick", 0.01f);
	}

	public void InitTextures()
	{
		SkinEditorPartItem[] array = items;
		foreach (SkinEditorPartItem skinEditorPartItem in array)
		{
			skinEditorPartItem.InitTexture();
		}
	}

	private void UnselectAll()
	{
		SkinEditorPartItem[] array = items;
		foreach (SkinEditorPartItem skinEditorPartItem in array)
		{
			skinEditorPartItem.Boolean_0 = false;
		}
	}

	public void SetOnSelectPartCallback(Action<SkinEditorPartItem> action_1)
	{
		action_0 = action_1;
	}

	private void OnSelectedItem(SkinEditorPartItem skinEditorPartItem_0)
	{
		if (action_0 != null)
		{
			action_0(skinEditorPartItem_0);
		}
		UnselectAll();
	}
}
