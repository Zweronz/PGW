using System;
using UnityEngine;
using engine.helpers;
using engine.unity;
using System.Collections.Generic;

public class SelectMapWindowItem : MonoBehaviour
{
	public GameObject[] players;

	public UITexture bkg;

	public GameObject[] borders;

	public UILabel[] mapTexts;

	public UILabel title;

	public UIWidget IsNewwWidget;

	public UIWidget IsBestwWidget;

	public UIWidget IsActionwWidget;

	public bool initSelected;

	public ModeData mode;

	private MapData mapData_0;

	private bool bool_0;

	private Action<SelectMapWindowItem> action_0;

	private float float_0 = 3f;

	private static List<MapData> mapData
	{
		get
		{
			if (_mapData == null)
			{
				List<MapData> data = new List<MapData>();
				Maps maps = Resources.Load<Maps>("Maps");

					foreach (Maps.Map map in maps.maps)
					{
						data.Add(map.ToMapData());
					}

					_mapData = data;
				}

				return _mapData;
		}
	}

	private static List<MapData> _mapData;

	private void Start()
	{
		if (mode == null)
		{
			return;
		}
		mapData_0 = mapData.Find(x => x.Int32_0 == mode.Int32_1);//MapStorage.Get.Storage.GetObjectByKey(mode.Int32_1);
		if (mapData_0 != null)
		{
			Texture texture = ImageLoader.LoadMapPreviewTexture(mapData_0);
			if (texture != null)
			{
				bkg.Texture_0 = texture;
			}
			title.String_0 = Localizer.Get(mapData_0.String_1);
			string string_ = string.Empty;
			switch (mapData_0.MapSize_0)
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
			bool flag = mapData_0.Boolean_1;
			bool bool_ = false;
			UserOverrideContentGroupData activeStockItemByType = StocksController.StocksController_0.GetActiveStockItemByType(mode.Int32_0, ContentGroupItemType.MODE);
			int num = ((activeStockItemByType != null) ? activeStockItemByType.int_2 : 0);
			bool flag2 = num > 0 && Utility.Double_0 < (double)num;
			IsActionwWidget.transform.GetChild(1).gameObject.GetComponent<ActionUUUUUltraKostyl>().actionTimeEnd = num;
			if (flag2)
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
			NGUITools.SetActive(IsActionwWidget.gameObject, flag2);
			SetSelected(initSelected);
			updatePlayers();
		}
	}

	private void Update()
	{
		if (mode != null && mapData_0 != null)
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				float_0 = 5f;
				updatePlayers();
			}
		}
	}

	private void OnClick()
	{
		if (mode != null && mapData_0 != null)
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

	public void SetCallbacks(Action<SelectMapWindowItem> action_1)
	{
		action_0 = action_1;
	}

	public void ToRoom()
	{
		MonoSingleton<FightController>.Prop_0.StartFightForMode(mode);
	}

	private void updatePlayers()
	{
		int modePopularityType_ = (int)mode.ModePopularityType_0;
		for (int i = 0; i < players.Length; i++)
		{
			players[i].SetActive(i == modePopularityType_);
		}
	}
}
