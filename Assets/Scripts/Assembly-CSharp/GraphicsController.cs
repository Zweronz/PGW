using System;
using System.Collections.Generic;
using Rilisoft.MiniJson;
using UnityEngine;
using engine.data;

public sealed class GraphicsController
{
	private const string string_0 = "GraphicsSettingsData";

	private const string string_1 = "setting_resolution_width";

	private const string string_2 = "setting_resolution_height";

	private const string string_3 = "setting_window_mode";

	private const string string_4 = "setting_quality";

	private const int int_0 = 1024;

	private const int int_1 = 768;

	private int int_2;

	private int int_3;

	private int int_4;

	private bool bool_0;

	private static BaseSharedSettings baseSharedSettings_0;

	private readonly List<float> list_0 = new List<float> { 1.5f, 1.25f };

	private List<Resolution> list_1 = new List<Resolution>();

	public List<string> list_2 = new List<string>();

	public List<string> list_3 = new List<string>();

	private static GraphicsController graphicsController_0;

	public static GraphicsController GraphicsController_0
	{
		get
		{
			if (graphicsController_0 == null)
			{
				graphicsController_0 = new GraphicsController();
			}
			return graphicsController_0;
		}
	}

	public string String_0
	{
		get
		{
			return string.Format("{0} x {1}", int_2, int_3);
		}
		set
		{
			string[] array = value.Split('x');
			int num = int.Parse(array[0].Trim());
			int num2 = int.Parse(array[1].Trim());
			if (int_2 != num || int_3 != num2)
			{
				int_2 = num;
				int_3 = num2;
				SetResolution();
				Save();
			}
		}
	}

	public string String_1
	{
		get
		{
			return list_3[int_4];
		}
		set
		{
			int num = list_3.IndexOf(value);
			if (int_4 != num)
			{
				int_4 = num;
				SetQuality();
				Save();
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (bool_0 != value)
			{
				bool_0 = value;
				SetWindowMode();
				Save();
			}
		}
	}

	public bool Boolean_1
	{
		get
		{
			float num = 1.3333334f;
			float num2 = (float)Screen.width / (float)Screen.height;
			return num2 - num > float.Epsilon;
		}
	}

	private GraphicsController()
	{
	}

	public int GetCurrentScreenWidth()
	{
		return int_2;
	}

	public int GetCurrentScreenHeight()
	{
		return int_3;
	}

	private void SetResolution()
	{
		Screen.SetResolution(int_2, int_3, !bool_0);
	}

	private void SetQuality()
	{
		QualitySettings.SetQualityLevel(0, true);
	}

	private void SetWindowMode()
	{
		Screen.SetResolution(int_2, int_3, !bool_0);
	}

	public void Init(BaseSharedSettings baseSharedSettings_1)
	{
		baseSharedSettings_0 = baseSharedSettings_1;
		list_2.Clear();
		list_1.Clear();
		list_3.Clear();
		Resolution[] resolutions = Screen.resolutions;
		Resolution[] array = resolutions;
		for (int i = 0; i < array.Length; i++)
		{
			Resolution item = array[i];
			float num = (float)item.width / (float)item.height;
			bool flag = true;
			foreach (float item4 in list_0)
			{
				float num2 = item4;
				if (!(Math.Abs(num - num2) >= float.Epsilon))
				{
					flag = false;
					break;
				}
			}
			if (item.width >= 1024 && item.height >= 768 && flag)
			{
				string item2 = string.Format("{0} x {1}", item.width, item.height);
				list_2.Add(item2);
				list_1.Add(item);
			}
		}
		string[] names = QualitySettings.names;
		string[] array2 = names;
		foreach (string item3 in array2)
		{
			list_3.Add(item3);
		}
		Load();
		SetResolution();
		SetWindowMode();
		SetQuality();
	}

	private void Load()
	{
		string value = baseSharedSettings_0.GetValue<string>("GraphicsSettingsData");
		if (!string.IsNullOrEmpty(value))
		{
			Dictionary<string, object> dictionary = Json.Deserialize(value) as Dictionary<string, object>;
			if (dictionary == null)
			{
				SetDefaultSettings();
				return;
			}
			if (dictionary.ContainsKey("setting_resolution_width"))
			{
				int_2 = Convert.ToInt32(dictionary["setting_resolution_width"]);
			}
			if (dictionary.ContainsKey("setting_resolution_height"))
			{
				int_3 = Convert.ToInt32(dictionary["setting_resolution_height"]);
			}
			if (dictionary.ContainsKey("setting_quality"))
			{
				int_4 = Convert.ToInt32(dictionary["setting_quality"]);
			}
			if (dictionary.ContainsKey("setting_window_mode"))
			{
				bool_0 = Convert.ToInt32(dictionary["setting_window_mode"]) == 1;
			}
		}
		else
		{
			SetDefaultSettings();
		}
	}

	private void Save()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		dictionary.Add("setting_resolution_width", int_2);
		dictionary.Add("setting_resolution_height", int_3);
		dictionary.Add("setting_quality", int_4);
		dictionary.Add("setting_window_mode", bool_0 ? 1 : 0);
		string gparam_ = Json.Serialize(dictionary);
		baseSharedSettings_0.SetValue("GraphicsSettingsData", gparam_, true);
	}

	public void SetDefaultSettings()
	{
		if (list_1.Count > 0)
		{
			Resolution resolution = list_1[list_1.Count - 1];
			int_2 = resolution.width;
			int_3 = resolution.height;
		}
		int_4 = 0;
		bool_0 = false;
	}
}
