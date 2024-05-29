using UnityEngine;
using engine.unity;

public class TooltipWindow : MonoBehaviour
{
	private static TooltipWindow tooltipWindow_0;

	public GameObject[] containers;

	public static TooltipWindow TooltipWindow_0
	{
		get
		{
			return tooltipWindow_0 ?? (tooltipWindow_0 = ScreenController.ScreenController_0.LoadUI("TooltipWindow").GetComponent<TooltipWindow>());
		}
	}

	private void OnDestroy()
	{
		tooltipWindow_0 = null;
	}
}
