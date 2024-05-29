using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

internal sealed class LevelArt : MonoBehaviour
{
	private const int int_0 = 4;

	public static readonly bool bool_0 = true;

	public GUIStyle startButton;

	public static bool bool_1;

	public GUIStyle labelsStyle;

	public float widthBackLabel = 770f;

	public float heightBackLabel = 100f;

	private float float_0;

	private int int_1;

	private bool bool_2 = true;

	public float _delayShowComics = 3f;

	private bool bool_3;

	private int int_2 = 4;

	private Texture texture_0;

	private List<Texture> list_0 = new List<Texture>();

	private bool bool_4;

	private string string_0;

	private bool bool_5;

	[Obsolete("Use ComicsCampaign via uGUI instead of this class.")]
	private void Start()
	{
		bool_5 = LocalizationStore.String_44 != "English";
		labelsStyle.font = LocalizationStore.GetFontByLocalize("Key_04B_03");
		labelsStyle.fontSize = Mathf.RoundToInt(20f * Defs.Single_0);
		if (Resources.Load<Texture>(_NameForNumber(5)) != null)
		{
			int_2 *= 2;
		}
		StartCoroutine("ShowArts");
		texture_0 = Resources.Load<Texture>("Arts_background_" + CurrentCampaignGame.string_0);
		if (bool_1)
		{
			string[] array = Storager.LoadStringArray(Defs.String_6) ?? new string[0];
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text.Equals(CurrentCampaignGame.string_0))
				{
					bool_2 = false;
					break;
				}
			}
		}
		else
		{
			string[] array3 = Storager.LoadStringArray(Defs.String_5) ?? new string[0];
			string[] array4 = array3;
			foreach (string text2 in array4)
			{
				if (text2.Equals(CurrentCampaignGame.string_1))
				{
					bool_2 = false;
					break;
				}
			}
		}
		bool_4 = !bool_2;
	}

	private void GoToLevel()
	{
		if (bool_1)
		{
			string[] array = Storager.LoadStringArray(Defs.String_6) ?? new string[0];
			if (Array.IndexOf(array, CurrentCampaignGame.string_0) == -1)
			{
				List<string> list = new List<string>();
				string[] array2 = array;
				foreach (string item in array2)
				{
					list.Add(item);
				}
				list.Add(CurrentCampaignGame.string_0);
				Storager.SaveStringArray(Defs.String_6, list.ToArray());
			}
		}
		else
		{
			string[] array3 = Storager.LoadStringArray(Defs.String_5) ?? new string[0];
			if (!bool_1 && Array.IndexOf(array3, CurrentCampaignGame.string_1) == -1)
			{
				List<string> list2 = new List<string>();
				string[] array4 = array3;
				foreach (string item2 in array4)
				{
					list2.Add(item2);
				}
				list2.Add(CurrentCampaignGame.string_1);
				Storager.SaveStringArray(Defs.String_5, list2.ToArray());
			}
		}
		Application.LoadLevel((!bool_1) ? "CampaignLoading" : "ChooseLevel");
	}

	private string _NameForNumber(int int_3)
	{
		return ResPath.Combine("Arts", ResPath.Combine((!bool_1) ? CurrentCampaignGame.string_1 : CurrentCampaignGame.string_0, int_3.ToString()));
	}

	[Obfuscation(Exclude = true)]
	private IEnumerator ShowArts()
	{
		string empty = string.Empty;
		Texture texture = null;
		do
		{
			texture = null;
			int_1++;
			empty = _NameForNumber(int_1);
			texture = Resources.Load<Texture>(empty);
			if (texture != null)
			{
				if (list_0.Count == 4)
				{
					list_0.Clear();
				}
				list_0.Add(texture);
				string text = ((!bool_1) ? string.Format("{0}_{1}", CurrentCampaignGame.string_1, int_1 - 1) : string.Format("{0}_{1}", CurrentCampaignGame.string_0, int_1 - 1));
				string_0 = LocalizationStore.Get(text) ?? string.Empty;
				if (text.Equals(string_0))
				{
					string_0 = string.Empty;
				}
				Resources.UnloadUnusedAssets();
				float_0 = 0f;
				float time = Time.time;
				float time2 = Time.time;
				do
				{
					yield return new WaitForEndOfFrame();
					float_0 += (Time.time - time) / _delayShowComics;
					time = Time.time;
				}
				while (Time.time - time2 < _delayShowComics && !bool_3);
				bool_3 = false;
				float_0 = 1f;
				continue;
			}
			GoToLevel();
			yield break;
		}
		while (texture != null && int_1 % 4 != 0);
		yield return new WaitForSeconds(_delayShowComics);
		bool_4 = true;
	}

	[Obsolete("Use ComicsCampaign via uGUI instead of this class.")]
	private void OnGUI()
	{
	}

	public static string WrappedText(string string_1)
	{
		int num = 30;
		StringBuilder stringBuilder = new StringBuilder();
		int num2 = 0;
		int num3 = 0;
		while (num2 < string_1.Length)
		{
			stringBuilder.Append(string_1[num2]);
			if (string_1[num2] == '\n')
			{
				stringBuilder.Append('\n');
			}
			if (num3 >= num && string_1[num2] == ' ')
			{
				stringBuilder.Append("\n\n");
				num3 = 0;
			}
			num2++;
			num3++;
		}
		return stringBuilder.ToString();
	}
}
