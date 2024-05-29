using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

public class Loader : MonoBehaviour
{
	private static Loader loader_0;

	public GameObject LoaderContainer;

	public UILabel message;

	public GameObject background;

	public List<string> listBackgrounds;

	private readonly string string_0 = "UI/Controls/Loader/{0}";

	private UIPanel uipanel_0;

	[CompilerGenerated]
	private static Action<GameObject> action_0;

	public static Loader Loader_0
	{
		get
		{
			return loader_0 ?? (loader_0 = ScreenController.ScreenController_0.LoadUI("Loader").GetComponent<Loader>());
		}
	}

	private void OnDestroy()
	{
		loader_0 = null;
	}

	public void Show(bool bool_0 = false)
	{
		if (!base.gameObject.activeSelf)
		{
			NGUITools.SetActive(base.gameObject, true);
			OnShow(bool_0);
		}
	}

	public void Hide()
	{
		if (base.gameObject.activeSelf)
		{
			NGUITools.SetActive(base.gameObject, false);
			OnHide();
		}
	}

	private void OnShow(bool bool_0)
	{
		if (message != null)
		{
			message.String_0 = LocalizationStore.Get("Key_0853");
		}
		if (uipanel_0 == null)
		{
			uipanel_0 = GetComponent<UIPanel>();
		}
		uipanel_0.Int32_1 = 10000;
		if (bool_0)
		{
			SetBackground();
		}
		else
		{
			DestroyBackground();
		}
	}

	private void OnHide()
	{
		DestroyBackground();
	}

	private void SetBackground()
	{
		DestroyBackground();
		if (background == null || listBackgrounds == null || listBackgrounds.Count == 0)
		{
			return;
		}
		string text = string.Format(string_0, listBackgrounds[UnityEngine.Random.Range(0, listBackgrounds.Count)]);
		GameObject gameObject = Resources.Load(text) as GameObject;
		if (gameObject == null)
		{
			Log.AddLine("Loader::SetBackground. Prefab not found: " + text, Log.LogLevel.WARNING);
			return;
		}
		GameObject gameObject2 = NGUITools.AddChild(background, gameObject);
		if (gameObject2 != null)
		{
			UIWidget component = gameObject2.GetComponent<UIWidget>();
			component.SetAnchor(background, 0, 0, 0, 0);
		}
	}

	private void DestroyBackground()
	{
		if (background == null)
		{
			return;
		}
		Transform transform = background.transform;
		int childCount = transform.childCount;
		if (childCount != 0)
		{
			List<GameObject> list = new List<GameObject>(childCount);
			for (int i = 0; i < childCount; i++)
			{
				list.Add(transform.GetChild(i).gameObject);
			}
			list.ForEach(delegate(GameObject gameObject_0)
			{
				UnityEngine.Object.Destroy(gameObject_0);
			});
		}
	}
}
