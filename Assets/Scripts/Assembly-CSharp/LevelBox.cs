using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelBox
{
	public static List<LevelBox> list_0;

	public List<CampaignLevel> list_1 = new List<CampaignLevel>();

	public int int_0;

	public string string_0;

	public string string_1;

	public string string_2 = string.Empty;

	public int int_1;

	public int int_2;

	public static Dictionary<string, string> dictionary_0;

	[CompilerGenerated]
	private int int_3;

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	static LevelBox()
	{
		list_0 = new List<LevelBox>();
		dictionary_0 = new Dictionary<string, string>();
		LevelBox item = new LevelBox
		{
			int_0 = int.MaxValue,
			string_2 = "Box_coming_soon",
			string_0 = "coming soon",
			Int32_0 = 0
		};
		LevelBox levelBox = new LevelBox
		{
			int_0 = (Debug.isDebugBuild ? 1 : 25),
			string_0 = "minecraft",
			string_1 = string.Empty,
			string_2 = "Box_2",
			Int32_0 = 70,
			int_2 = 1500
		};
		CampaignLevel item2 = new CampaignLevel
		{
			string_0 = "Utopia"
		};
		CampaignLevel item3 = new CampaignLevel
		{
			string_0 = "Maze"
		};
		CampaignLevel item4 = new CampaignLevel
		{
			string_0 = "Sky_islands"
		};
		CampaignLevel item5 = new CampaignLevel
		{
			string_0 = "Winter"
		};
		CampaignLevel item6 = new CampaignLevel
		{
			string_0 = "Castle"
		};
		CampaignLevel item7 = new CampaignLevel
		{
			string_0 = "Gluk_2"
		};
		levelBox.list_1.Add(item2);
		levelBox.list_1.Add(item3);
		levelBox.list_1.Add(item4);
		levelBox.list_1.Add(item5);
		levelBox.list_1.Add(item6);
		levelBox.list_1.Add(item7);
		LevelBox levelBox2 = new LevelBox
		{
			string_0 = "Real",
			string_1 = string.Empty,
			string_2 = "Box_1",
			Int32_0 = 50,
			int_2 = 1500
		};
		CampaignLevel item8 = new CampaignLevel
		{
			string_0 = "Farm"
		};
		CampaignLevel item9 = new CampaignLevel
		{
			string_0 = "Cementery"
		};
		CampaignLevel item10 = new CampaignLevel
		{
			string_0 = "City"
		};
		CampaignLevel item11 = new CampaignLevel
		{
			string_0 = "Hospital"
		};
		CampaignLevel item12 = new CampaignLevel
		{
			string_0 = "Bridge"
		};
		CampaignLevel item13 = new CampaignLevel
		{
			string_0 = "Jail"
		};
		CampaignLevel item14 = new CampaignLevel
		{
			string_0 = "Slender"
		};
		CampaignLevel item15 = new CampaignLevel
		{
			string_0 = "Area52"
		};
		CampaignLevel item16 = new CampaignLevel
		{
			string_0 = "School"
		};
		levelBox2.list_1.Add(item8);
		levelBox2.list_1.Add(item9);
		levelBox2.list_1.Add(item10);
		levelBox2.list_1.Add(item11);
		levelBox2.list_1.Add(item12);
		levelBox2.list_1.Add(item13);
		levelBox2.list_1.Add(item14);
		levelBox2.list_1.Add(item15);
		levelBox2.list_1.Add(item16);
		list_0.Add(levelBox2);
		list_0.Add(levelBox);
		list_0.Add(item);
	}
}
