using System;
using UnityEngine;

public class KillChatUIController : MonoBehaviour
{
	private const int int_0 = 4;

	public UILabel name1;

	public UILabel name2;

	public UISprite sprite1;

	public UISprite sprite2;

	public GameObject container;

	public int id;

	public static Color color_0 = new Color(0.18431373f, 0.6745098f, 0.88235295f);

	public static Color color_1 = new Color(1f, 0.41960785f, 0.2509804f);

	private static Color color_2 = new Color(1f, 1f, 1f);

	private void Start()
	{
	}

	private void Update()
	{
		Player_move_c playerMoveC = HeadUpDisplay.GetPlayerMoveC();
		if (playerMoveC == null || playerMoveC.Single_6.Length <= id || playerMoveC.String_0.Length <= id || id < 0)
		{
			return;
		}
		if (playerMoveC.Single_6[id] <= 0f)
		{
			container.SetActive(false);
			return;
		}
		string text = playerMoveC.String_0[id][0];
		string text2 = playerMoveC.String_0[id][2];
		string string_ = playerMoveC.String_0[id][1];
		string string_2 = playerMoveC.String_0[id][3];
		if (string.IsNullOrEmpty(text))
		{
			container.SetActive(false);
			return;
		}
		container.SetActive(true);
		name1.String_0 = text;
		int int_ = Mathf.CeilToInt((float)name1.Int32_0 * name1.transform.localScale.x);
		int num = Mathf.RoundToInt(name1.gameObject.transform.localPosition.x);
		int_ = setSprite(string_2, sprite1, int_, num);
		int_ = setSprite(string_, sprite2, int_, num);
		if (!string.IsNullOrEmpty(text2))
		{
			if (!name2.gameObject.activeSelf)
			{
				name2.gameObject.SetActive(true);
			}
			Vector3 localPosition = name2.transform.localPosition;
			localPosition.x = num + int_ + 4;
			name2.transform.localPosition = localPosition;
			name2.String_0 = text2;
		}
		else if (name2.gameObject.activeSelf)
		{
			name2.gameObject.SetActive(false);
		}
		updateColors(playerMoveC);
	}

	private int setSprite(string string_0, UISprite uisprite_0, int int_1, int int_2)
	{
		if (!string.IsNullOrEmpty(string_0))
		{
			if (!uisprite_0.gameObject.activeSelf)
			{
				uisprite_0.gameObject.SetActive(true);
			}
			Vector3 localPosition = uisprite_0.transform.localPosition;
			localPosition.x = int_2 + int_1 + 4;
			uisprite_0.transform.localPosition = localPosition;
			uisprite_0.String_0 = string_0;
			if (uisprite_0.GetAtlasSprite() != null)
			{
				uisprite_0.Int32_0 = uisprite_0.GetAtlasSprite().width;
				uisprite_0.Int32_1 = uisprite_0.GetAtlasSprite().height;
			}
			int_1 += uisprite_0.Int32_0 + 4;
		}
		else if (uisprite_0.gameObject.activeSelf)
		{
			uisprite_0.gameObject.SetActive(false);
		}
		return int_1;
	}

	private void updateColors(Player_move_c player_move_c_0)
	{
		if (!string.IsNullOrEmpty(name1.String_0) && name1.gameObject.activeSelf)
		{
			Color color = color_2;
			string text = player_move_c_0.String_0[id][4];
			if (!string.IsNullOrEmpty(text) && (string.Equals(text, "1") || string.Equals(text, "2")))
			{
				int num = Convert.ToInt32(text);
				color = ((player_move_c_0.Int32_2 != num) ? color_1 : color_0);
			}
			name1.Color_0 = color;
		}
		if (!string.IsNullOrEmpty(name2.String_0) && name2.gameObject.activeSelf)
		{
			Color color2 = color_2;
			string text2 = player_move_c_0.String_0[id][5];
			if (!string.IsNullOrEmpty(text2) && (string.Equals(text2, "1") || string.Equals(text2, "2")))
			{
				int num2 = Convert.ToInt32(text2);
				color2 = ((player_move_c_0.Int32_2 != num2) ? color_1 : color_0);
			}
			name2.Color_0 = color2;
		}
	}
}
