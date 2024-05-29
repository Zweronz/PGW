using UnityEngine;

public class UISavedOption : MonoBehaviour
{
	public string keyName;

	private UIPopupList uipopupList_0;

	private UIToggle uitoggle_0;

	private string String_0
	{
		get
		{
			return (!string.IsNullOrEmpty(keyName)) ? keyName : ("NGUI State: " + base.name);
		}
	}

	private void Awake()
	{
		uipopupList_0 = GetComponent<UIPopupList>();
		uitoggle_0 = GetComponent<UIToggle>();
	}

	private void OnEnable()
	{
		if (uipopupList_0 != null)
		{
			EventDelegate.Add(uipopupList_0.onChange, SaveSelection);
		}
		if (uitoggle_0 != null)
		{
			EventDelegate.Add(uitoggle_0.list_0, SaveState);
		}
		if (uipopupList_0 != null)
		{
			string @string = PlayerPrefs.GetString(String_0);
			if (!string.IsNullOrEmpty(@string))
			{
				uipopupList_0.String_0 = @string;
			}
			return;
		}
		if (uitoggle_0 != null)
		{
			uitoggle_0.Boolean_0 = PlayerPrefs.GetInt(String_0, 1) != 0;
			return;
		}
		string string2 = PlayerPrefs.GetString(String_0);
		UIToggle[] componentsInChildren = GetComponentsInChildren<UIToggle>(true);
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			UIToggle uIToggle = componentsInChildren[i];
			uIToggle.Boolean_0 = uIToggle.name == string2;
		}
	}

	private void OnDisable()
	{
		if (uitoggle_0 != null)
		{
			EventDelegate.Remove(uitoggle_0.list_0, SaveState);
		}
		if (uipopupList_0 != null)
		{
			EventDelegate.Remove(uipopupList_0.onChange, SaveSelection);
		}
		if (!(uitoggle_0 == null) || !(uipopupList_0 == null))
		{
			return;
		}
		UIToggle[] componentsInChildren = GetComponentsInChildren<UIToggle>(true);
		int num = 0;
		int num2 = componentsInChildren.Length;
		UIToggle uIToggle;
		while (true)
		{
			if (num < num2)
			{
				uIToggle = componentsInChildren[num];
				if (uIToggle.Boolean_0)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		PlayerPrefs.SetString(String_0, uIToggle.name);
	}

	public void SaveSelection()
	{
		PlayerPrefs.SetString(String_0, UIPopupList.uipopupList_0.String_0);
	}

	public void SaveState()
	{
		PlayerPrefs.SetInt(String_0, UIToggle.uitoggle_0.Boolean_0 ? 1 : 0);
	}
}
