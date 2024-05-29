using UnityEngine;

[RequireComponent(typeof(UISlider))]
public class UISoundVolume : MonoBehaviour
{
	private UISlider uislider_0;

	private void Awake()
	{
		uislider_0 = GetComponent<UISlider>();
		uislider_0.Single_0 = NGUITools.Single_0;
		EventDelegate.Add(uislider_0.list_0, OnChange);
	}

	private void OnChange()
	{
		NGUITools.Single_0 = UIProgressBar.uiprogressBar_0.Single_0;
	}
}
