using System;
using UnityEngine;

public class CreateBattleWindowItem : MonoBehaviour
{
	public UITexture bkg;

	public GameObject[] borders;

	public UILabel[] mapTexts;

	public UILabel title;

	public bool initSelected;

	public MapData map;

	public UIWidget IsNewwWidget;

	public UIWidget IsBestwWidget;

	public UIWidget IsActionwWidget;

	private bool bool_0;

	private Action<CreateBattleWindowItem> action_0;

	private Action<int> action_1;

	private float float_0 = 3f;

	private void Start()
	{
		if (map != null)
		{
			Texture texture = ImageLoader.LoadMapPreviewTexture(map);
			if (texture != null)
			{
				bkg.Texture_0 = texture;
			}
			title.String_0 = Localizer.Get(map.String_1);
			string string_ = string.Empty;
			switch (map.MapSize_0)
			{
			case MapSize.SMALL:
				string_ = Localizer.Get("ui.small_map");
				break;
			case MapSize.MEDIUM:
				string_ = Localizer.Get("ui.medium_map");
				break;
			case MapSize.LARGE:
				string_ = Localizer.Get("ui.large_map");
				break;
			case MapSize.HUGE:
				string_ = Localizer.Get("ui.huge_map");
				break;
			}
			for (int i = 0; i < mapTexts.Length; i++)
			{
				mapTexts[i].String_0 = string_;
			}
			bool flag = map.Boolean_1;
			bool bool_ = false;
			bool bool_2;
			if (bool_2 = ((map.Int32_0 == 24 || map.Int32_0 == 25) ? true : false))
			{
				flag = false;
				bool_ = false;
			}
			if (flag)
			{
				bool_ = false;
			}
			NGUITools.SetActive(IsNewwWidget.gameObject, flag);
			NGUITools.SetActive(IsBestwWidget.gameObject, bool_);
			NGUITools.SetActive(IsActionwWidget.gameObject, bool_2);
			SetSelected(initSelected);
		}
	}

	private void OnClick()
	{
		if (map != null)
		{
			if (!bool_0)
			{
				SetSelected(true);
			}
			else
			{
				ToRoom();
			}
		}
	}

	public void SetSelected(bool bool_1)
	{
		if (bool_1 && bool_1 != bool_0 && action_0 != null)
		{
			action_0(this);
		}
		bool_0 = bool_1;
		borders[0].SetActive(!bool_0);
		borders[1].SetActive(bool_0);
	}

	public void SetCallbacks(Action<CreateBattleWindowItem> action_2, Action<int> action_3)
	{
		action_0 = action_2;
		action_1 = action_3;
	}

	public void ToRoom()
	{
		if (map != null && action_1 != null)
		{
			action_1(map.Int32_0);
		}
	}
}
