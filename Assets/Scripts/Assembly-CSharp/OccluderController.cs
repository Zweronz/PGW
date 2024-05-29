using UnityEngine;

public class OccluderController : HighlightingController
{
	private int int_0 = -220;

	private int int_1 = 20;

	private void OnGUI()
	{
		float left = Screen.width + int_0;
		GUI.Label(new Rect(left, int_1, 500f, 100f), "Occluder (moving wall) controls:");
		if (GUI.Button(new Rect(left, int_1 + 30, 200f, 30f), "Toggle Occluder"))
		{
			highlightableObject_0.OccluderSwitch();
		}
	}
}
