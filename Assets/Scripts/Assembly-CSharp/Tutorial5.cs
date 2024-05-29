using UnityEngine;

public class Tutorial5 : MonoBehaviour
{
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = GetComponentsInChildren<UITweener>();
		UITweener[] array = componentsInChildren;
		foreach (UITweener uITweener in array)
		{
			uITweener.float_1 = Mathf.Lerp(2f, 0.5f, UIProgressBar.uiprogressBar_0.Single_0);
		}
	}
}
