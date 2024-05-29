using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class SetColorOnSelection : MonoBehaviour
{
	private UIWidget uiwidget_0;

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_0;

	public void SetSpriteBySelection()
	{
		if (!(UIPopupList.uipopupList_0 == null))
		{
			if (uiwidget_0 == null)
			{
				uiwidget_0 = GetComponent<UIWidget>();
			}
			switch (UIPopupList.uipopupList_0.String_0)
			{
			case "White":
				uiwidget_0.Color_0 = Color.white;
				break;
			case "Red":
				uiwidget_0.Color_0 = Color.red;
				break;
			case "Green":
				uiwidget_0.Color_0 = Color.green;
				break;
			case "Blue":
				uiwidget_0.Color_0 = Color.blue;
				break;
			case "Yellow":
				uiwidget_0.Color_0 = Color.yellow;
				break;
			case "Cyan":
				uiwidget_0.Color_0 = Color.cyan;
				break;
			case "Magenta":
				uiwidget_0.Color_0 = Color.magenta;
				break;
			}
		}
	}
}
