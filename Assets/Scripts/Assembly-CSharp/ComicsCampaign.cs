using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal sealed class ComicsCampaign : MonoBehaviour
{
	public RawImage background;

	public RawImage[] comicFrames = new RawImage[4];

	public Button skipButton;

	public Text subtitlesText;

	private string[] string_0 = new string[4]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	private int int_0;

	private bool bool_0;

	private bool bool_1 = true;

	private Coroutine coroutine_0;

	private Action action_0;

	public void HandleSkipPressed()
	{
		Debug.Log("[Skip] pressed.");
		if (action_0 != null)
		{
			action_0();
		}
	}

	private bool DetermineIfFirstLaunch()
	{
		if (LevelArt.bool_1)
		{
			string[] source = Storager.LoadStringArray(Defs.String_6) ?? new string[0];
			return !source.Any(CurrentCampaignGame.string_0.Equals);
		}
		string[] source2 = Storager.LoadStringArray(Defs.String_5) ?? new string[0];
		return !source2.Any(CurrentCampaignGame.string_1.Equals);
	}

	private void Awake()
	{
		if (subtitlesText != null)
		{
			subtitlesText.transform.parent.gameObject.SetActive(LocalizationStore.String_44 != "English");
		}
		int_0 = Math.Min(4, comicFrames.Length);
		bool_1 = DetermineIfFirstLaunch();
	}

	private IEnumerator Start()
	{
		Texture texture = Resources.Load<Texture>(GetNameForIndex(int_0 + 1, LevelArt.bool_1));
		bool_0 = texture != null;
		if (bool_1)
		{
			SetSkipHandler(null);
		}
		else if (bool_0)
		{
			SetSkipHandler(GotoNextPage);
		}
		else
		{
			SetSkipHandler(GotoLevelOrBoxmap);
		}
		if (background != null)
		{
			background.texture = Resources.Load<Texture>("Arts_background_" + CurrentCampaignGame.string_0);
		}
		for (int i = 0; i != int_0; i++)
		{
			string nameForIndex = GetNameForIndex(i + 1, LevelArt.bool_1);
			Texture texture2 = Resources.Load<Texture>(nameForIndex);
			if (!(texture2 == null))
			{
				comicFrames[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, texture2.width);
				comicFrames[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, texture2.height);
				comicFrames[i].texture = texture2;
				comicFrames[i].color = new Color(1f, 1f, 1f, 0f);
				string text = ((!LevelArt.bool_1) ? string.Format("{0}_{1}", CurrentCampaignGame.string_1, i) : string.Format("{0}_{1}", CurrentCampaignGame.string_0, i));
				string_0[i] = LocalizationStore.Get(text) ?? string.Empty;
				continue;
			}
			Debug.LogWarning("Texture is null: " + nameForIndex);
			break;
		}
		coroutine_0 = StartCoroutine(FadeInCoroutine());
		yield return coroutine_0;
		if (bool_0)
		{
			SetSkipHandler(GotoNextPage);
		}
		else
		{
			SetSkipHandler(GotoLevelOrBoxmap);
		}
	}

	private void GotoNextPage()
	{
		if (bool_1)
		{
			SetSkipHandler(null);
		}
		else
		{
			SetSkipHandler(GotoLevelOrBoxmap);
		}
		if (coroutine_0 != null)
		{
			StopCoroutine(coroutine_0);
		}
		for (int i = 0; i != comicFrames.Length; i++)
		{
			if (!(comicFrames[i] == null))
			{
				comicFrames[i].texture = null;
				comicFrames[i].color = new Color(1f, 1f, 1f, 0f);
				string_0[i] = string.Empty;
			}
		}
		Resources.UnloadUnusedAssets();
		for (int j = 0; j != int_0; j++)
		{
			string nameForIndex = GetNameForIndex(int_0 + j + 1, LevelArt.bool_1);
			Texture texture = Resources.Load<Texture>(nameForIndex);
			if (texture == null)
			{
				break;
			}
			comicFrames[j].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, texture.width);
			comicFrames[j].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, texture.height);
			comicFrames[j].texture = texture;
			string text = ((!LevelArt.bool_1) ? string.Format("{0}_{1}", CurrentCampaignGame.string_1, int_0 + j) : string.Format("{0}_{1}", CurrentCampaignGame.string_0, int_0 + j));
			string_0[j] = LocalizationStore.Get(text) ?? string.Empty;
		}
		coroutine_0 = StartCoroutine(FadeInCoroutine(GotoLevelOrBoxmap));
	}

	private void GotoLevelOrBoxmap()
	{
		if (coroutine_0 != null)
		{
			StopCoroutine(coroutine_0);
		}
		if (LevelArt.bool_1)
		{
			string[] array = Storager.LoadStringArray(Defs.String_6) ?? new string[0];
			if (Array.IndexOf(array, CurrentCampaignGame.string_0) == -1)
			{
				List<string> list = new List<string>(array);
				list.Add(CurrentCampaignGame.string_0);
				Storager.SaveStringArray(Defs.String_6, list.ToArray());
			}
		}
		else
		{
			string[] array2 = Storager.LoadStringArray(Defs.String_5) ?? new string[0];
			if (Array.IndexOf(array2, CurrentCampaignGame.string_1) == -1)
			{
				List<string> list2 = new List<string>(array2);
				list2.Add(CurrentCampaignGame.string_1);
				Storager.SaveStringArray(Defs.String_5, list2.ToArray());
			}
		}
		Application.LoadLevel((!LevelArt.bool_1) ? "CampaignLoading" : "ChooseLevel");
	}

	private void SetSkipHandler(Action action_1)
	{
		action_0 = action_1;
		if (skipButton != null)
		{
			skipButton.gameObject.SetActive(action_1 != null);
		}
	}

	private IEnumerator FadeInCoroutine(Action action_1 = null)
	{
		for (int i = 0; i != comicFrames.Length; i++)
		{
			RawImage rawImage = comicFrames[i];
			if (!(rawImage == null))
			{
				if (subtitlesText != null)
				{
					subtitlesText.text = string_0[i];
				}
				for (int j = 0; j != 30; j++)
				{
					float a = Mathf.InverseLerp(0f, 30f, j);
					rawImage.color = new Color(1f, 1f, 1f, a);
					yield return new WaitForSeconds(1f / 30f);
				}
				rawImage.color = new Color(1f, 1f, 1f, 1f);
				yield return new WaitForSeconds(2f);
			}
		}
		if (action_1 != null)
		{
			SetSkipHandler(action_1);
		}
	}

	private static string GetNameForIndex(int int_1, bool bool_2)
	{
		return ResPath.Combine("Arts", ResPath.Combine((!bool_2) ? CurrentCampaignGame.string_1 : CurrentCampaignGame.string_0, int_1.ToString()));
	}
}
