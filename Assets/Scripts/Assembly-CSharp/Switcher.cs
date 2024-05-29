using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Rilisoft;
using UnityEngine;
using engine.helpers;

[RequireComponent(typeof(FrameStopwatchScript))]
internal sealed class Switcher : MonoBehaviour
{
	public static Dictionary<string, int> dictionary_0;

	private float float_0;

	public static string string_0;

	public static string[] string_1;

	public GameObject nickStackPrefab;

	public GameObject weaponManagerPrefab;

	public GameObject backgroundMusicPrefab;

	private Rect rect_0;

	private bool bool_0;

	static Switcher()
	{
		dictionary_0 = new Dictionary<string, int>();
		string_0 = "LevelLoadings";
		string_1 = new string[17]
		{
			"Loading_coliseum", "loading_Cementery", "Loading_Maze", "Loading_City", "Loading_Hospital", "Loading_Jail", "Loading_end_world_2", "Loading_Arena", "Loading_Area52", "Loading_Slender",
			"Loading_Hell", "Loading_bloody_farm", "Loading_most", "Loading_school", "Loading_utopia", "Loading_sky", "Loading_winter"
		};
		dictionary_0.Add("Tutorial", 0);
		dictionary_0.Add("Cementery", 1);
		dictionary_0.Add("Maze", 2);
		dictionary_0.Add("City", 3);
		dictionary_0.Add("Hospital", 4);
		dictionary_0.Add("Jail", 5);
		dictionary_0.Add("Gluk_2", 6);
		dictionary_0.Add("Arena", 7);
		dictionary_0.Add("Area52", 8);
		dictionary_0.Add("Slender", 9);
		dictionary_0.Add("Castle", 10);
		dictionary_0.Add("Farm", 11);
		dictionary_0.Add("Bridge", 12);
		dictionary_0.Add("School", 13);
		dictionary_0.Add("Utopia", 14);
		dictionary_0.Add("Sky_islands", 15);
		dictionary_0.Add("Winter", 16);
	}

	private IEnumerator Start()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		UnityEngine.Debug.Log(">>> Switcher.InitializeSwitcher()");
		ProgressBounds progressBounds = new ProgressBounds();
		Action action = delegate
		{
			UnityEngine.Debug.Log(string.Format("[{0}%, {1}%)    {2}    {3}", progressBounds.Single_0 * 100f, progressBounds.Single_1 * 100f, Time.frameCount, Time.realtimeSinceStartup));
		};
		FrameStopwatchScript component = GetComponent<FrameStopwatchScript>();
		if (component == null)
		{
			component = base.gameObject.AddComponent<FrameStopwatchScript>();
		}
		progressBounds.SetBounds(0f, 0.09f);
		action();
		float_0 = progressBounds.Single_0;
		float_0 = progressBounds.Lerp(float_0, 0.2f);
		yield return float_0;
		float_0 = progressBounds.Lerp(float_0, 0.2f);
		yield return float_0;
		Action<string> action2 = delegate
		{
		};
		Func<float, long, string> func = delegate(float float_1, long long_0)
		{
			int num2 = Mathf.RoundToInt(float_1 * 100f);
			return string.Format(" << {0}%: {1}", num2, long_0);
		};
		float_0 = progressBounds.Lerp(float_0, 0.2f);
		yield return float_0;
		while (float_0 < progressBounds.Single_1)
		{
			float_0 = progressBounds.Clamp(float_0 + 0.01f);
			yield return float_0;
		}
		progressBounds.SetBounds(0.1f, 0.19f);
		action();
		float_0 = progressBounds.Single_0;
		yield return float_0;
		if (nickStackPrefab != null)
		{
			if (!GameObject.FindGameObjectWithTag("NickLabelNGUI"))
			{
				UnityEngine.Object @object = UnityEngine.Object.Instantiate(nickStackPrefab, Vector3.zero, Quaternion.identity);
				yield return float_0;
			}
		}
		else
		{
			Log.AddLine("[Switcher::InitializeSwitcher]  nickStackPrefab == null", Log.LogLevel.WARNING);
		}
		if (!GameObject.FindGameObjectWithTag("MenuBackgroundMusic") && backgroundMusicPrefab != null)
		{
			UnityEngine.Object.Instantiate(backgroundMusicPrefab, Vector3.zero, Quaternion.identity);
			float_0 = progressBounds.Lerp(float_0, 0.1f);
			yield return float_0;
		}
		SkinsController.Init();
		ShopArtikulController.ShopArtikulController_0.Init();
		progressBounds.SetBounds(0.2f, 0.29f);
		action();
		float_0 = progressBounds.Single_0;
		yield return float_0;
		float_0 = progressBounds.Lerp(float_0, 0.2f);
		yield return float_0;
		if ((bool)weaponManagerPrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(weaponManagerPrefab, Vector3.zero, Quaternion.identity);
			WeaponManager component2 = gameObject.GetComponent<WeaponManager>();
			if (component2 != null)
			{
				int num = 0;
				while (!component2.Boolean_0)
				{
					float_0 = progressBounds.Clamp(Mathf.Lerp(progressBounds.Single_0, progressBounds.Single_1, 0.004f * (float)num));
					yield return float_0;
					num++;
				}
			}
			float_0 = progressBounds.Lerp(float_0, 0.12f);
			yield return float_0;
		}
		progressBounds.SetBounds(0.6f, 0.99f);
		action();
		float_0 = progressBounds.Single_0;
		yield return float_0;
		CampaignProgress.OpenNewBoxIfPossible();
		float_0 = progressBounds.Lerp(float_0, 0.2f);
		yield return float_0;
		CampaignProgress.SaveCampaignProgress();
		float_0 = 0.99f;
		yield return float_0;
		stopwatch.Stop();
		UnityEngine.Debug.Log(string.Format("<<< Switcher.InitializeSwitcher():    {0} ms,    Last Frame: {1}", stopwatch.ElapsedMilliseconds, Time.frameCount));
		string levelName = DetermineSceneName();
		AsyncOperation asyncOperation = Application.LoadLevelAsync(levelName);
		while (!asyncOperation.isDone)
		{
			yield return asyncOperation.progress;
		}
	}

	public static float SecondsFrom1970()
	{
		DateTime dateTime = new DateTime(1970, 1, 9, 0, 0, 0);
		DateTime now = DateTime.Now;
		return (float)(now - dateTime).TotalSeconds;
	}

	private void OnGUI()
	{
	}

	private static string DetermineSceneName()
	{
		switch (GlobalGameController.Int32_0)
		{
		default:
			return Defs.String_11;
		case 101:
			return "Tutorial";
		case -1:
			return Defs.String_11;
		case 0:
			return "Cementery";
		case 1:
			return "Maze";
		case 2:
			return "City";
		case 3:
			return "Hospital";
		case 4:
			return "Jail";
		case 5:
			return "Gluk_2";
		case 6:
			return "Arena";
		case 7:
			return "Area52";
		case 8:
			return "Slender";
		case 9:
			return "Castle";
		}
	}
}
