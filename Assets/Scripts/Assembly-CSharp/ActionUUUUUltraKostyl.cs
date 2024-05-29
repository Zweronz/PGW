using UnityEngine;
using engine.helpers;

public class ActionUUUUUltraKostyl : MonoBehaviour
{
	public double actionTimeEnd;

	private UILabel uilabel_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
		if (uilabel_0 == null)
		{
			base.enabled = false;
		}
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
	}

	private void Update()
	{
		int int_ = Mathf.FloorToInt((float)(actionTimeEnd - Utility.Double_0));
		uilabel_0.String_0 = Utility.GetLocalizedTime(int_, string_0, string_1, string_2, string_3);
		if (actionTimeEnd - Utility.Double_0 <= 1.0)
		{
			NGUITools.SetActive(base.gameObject.transform.parent.gameObject, false);
		}
	}
}
