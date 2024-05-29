using UnityEngine;

[RequireComponent(typeof(UIPopupList))]
public class LanguageSelection : MonoBehaviour
{
	private UIPopupList uipopupList_0;

	private void Start()
	{
		uipopupList_0 = GetComponent<UIPopupList>();
		if (Localization.String_0 != null)
		{
			uipopupList_0.items.Clear();
			int i = 0;
			for (int num = Localization.String_0.Length; i < num; i++)
			{
				uipopupList_0.items.Add(Localization.String_0[i]);
			}
			uipopupList_0.String_0 = Localization.String_1;
		}
		EventDelegate.Add(uipopupList_0.onChange, OnChange);
	}

	private void OnChange()
	{
		Localization.String_1 = UIPopupList.uipopupList_0.String_0;
	}
}
