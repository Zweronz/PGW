using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class SkinEditorColorPalette : MonoBehaviour
{
	private const string string_0 = "29AB06";

	private const int int_0 = 2;

	private const int int_1 = 2;

	private const int int_2 = 7;

	private const int int_3 = 8;

	public UITexture paletteTex;

	public UITexture colorTex;

	public UIInput inputHex;

	public UISprite colorSelector;

	private Camera camera_0;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	private static string string_1 = string.Empty;

	public static readonly Dictionary<char, char> dictionary_0 = new Dictionary<char, char>
	{
		{ 'A', 'F' },
		{ 'a', 'f' },
		{ '0', '9' }
	};

	private static string String_0
	{
		get
		{
			if (string.IsNullOrEmpty(string_1))
			{
				return "29AB06";
			}
			return string_1;
		}
	}

	private void Start()
	{
		camera_0 = NGUITools.FindCameraForLayer(paletteTex.gameObject.layer);
		float_0 = paletteTex.Vector2_4.x / 2f;
		float_1 = paletteTex.Vector2_4.y / 2f;
		float_2 = (float)paletteTex.Int32_0 / (float)paletteTex.Texture_0.width;
		float_3 = (float)paletteTex.Int32_1 / (float)paletteTex.Texture_0.height;
		colorTex.Texture_0 = Utility.CopyTexture(Texture2D.whiteTexture);
		inputHex.onValidate = OnInputValidate;
		Init();
	}

	private void Init()
	{
		inputHex.String_2 = String_0;
		SkinEditController.SkinEditController_0.color_0 = Utility.HexToColor(String_0);
		UpdateColorBox(true);
		SkinEditController.SkinEditController_0.Subscribe(OnDropperUser, SkinEditController.SkinEditorEvent.DROPPER_USED);
	}

	private void OnDestroy()
	{
		SkinEditController.SkinEditController_0.Unsubscribe(OnDropperUser, SkinEditController.SkinEditorEvent.DROPPER_USED);
	}

	private void OnPress(bool bool_0)
	{
		if (bool_0)
		{
			Vector3 v = camera_0.ScreenToWorldPoint(Input.mousePosition);
			Vector3 vector = paletteTex.transform.worldToLocalMatrix.MultiplyPoint3x4(v);
			vector.x += float_0;
			vector.y += float_1;
			int num = Mathf.FloorToInt(vector.x / float_2);
			int num2 = Mathf.FloorToInt(vector.y / float_3);
			SkinEditController.SkinEditController_0.color_0 = ((Texture2D)paletteTex.Texture_0).GetPixel(num, num2);
			UpdateColorBox();
			UpdateColorSelector(num, num2);
		}
	}

	private void UpdateColorBox(bool bool_0 = false)
	{
		string string_ = (string_1 = Utility.ColorToHex(SkinEditController.SkinEditController_0.color_0));
		inputHex.String_2 = string_;
		colorTex.Color_0 = SkinEditController.SkinEditController_0.color_0;
		if (bool_0)
		{
			FindColor();
		}
		SkinEditController.SkinEditController_0.Dispatch(0, SkinEditController.SkinEditorEvent.COLOR_CHANGED);
	}

	private void UpdateColorSelector(int int_4, int int_5)
	{
		Vector3 localPosition = new Vector3((float)int_4 * float_2, (float)int_5 * float_3, 0f);
		localPosition.x -= float_0 + 2f;
		localPosition.y = localPosition.y - float_1 + (float)(colorSelector.Int32_1 - 2);
		colorSelector.transform.localPosition = localPosition;
		NGUITools.SetActive(colorSelector.gameObject, true);
	}

	private void FindColor()
	{
		Texture2D texture2D = (Texture2D)paletteTex.Texture_0;
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				if (texture2D.GetPixel(i, j).Equals(SkinEditController.SkinEditController_0.color_0))
				{
					UpdateColorSelector(i, j);
					return;
				}
			}
		}
		NGUITools.SetActive(colorSelector.gameObject, false);
	}

	public char OnInputValidate(string string_2, int int_4, char char_0)
	{
		foreach (KeyValuePair<char, char> item in dictionary_0)
		{
			if (char_0 >= item.Key && char_0 <= item.Value)
			{
				return char_0;
			}
		}
		return '\0';
	}

	public void OnInputColor()
	{
		if (inputHex.String_2.Length >= 6)
		{
			SkinEditController.SkinEditController_0.color_0 = Utility.HexToColor(inputHex.String_2);
			UpdateColorBox(true);
			NGUITools.SetActive(colorSelector.gameObject, false);
		}
	}

	private void OnDropperUser(int int_4 = 0)
	{
		UpdateColorBox(true);
		NGUITools.SetActive(colorSelector.gameObject, false);
	}
}
