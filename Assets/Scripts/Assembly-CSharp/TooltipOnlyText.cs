using UnityEngine;
using engine.unity;

public class TooltipOnlyText : MonoBehaviour
{
	public UILabel TextTooltipLabel;

	public void SetContent(string string_0)
	{
		TextTooltipLabel.String_0 = Localizer.Get(string_0);
		Vector2 cursorSize = CursorPGW.CursorPGW_0.GetCursorSize();
		Vector3 localPosition = Input.mousePosition * ScreenController.ScreenController_0.Single_0;
		localPosition.x += cursorSize.x + 10f;
		int num = (int)((float)Screen.width * ScreenController.ScreenController_0.Single_0);
		if ((int)(localPosition.x + (float)TextTooltipLabel.Int32_0 + 10f) >= num)
		{
			localPosition.x -= (float)TextTooltipLabel.Int32_0 + cursorSize.x + 20f;
		}
		if ((int)(localPosition.y - (float)TextTooltipLabel.Int32_1) <= 0)
		{
			localPosition.y += TextTooltipLabel.Int32_1;
		}
		base.transform.localPosition = localPosition;
		Invoke("ActivateTooltip", 0.5f);
	}

	public void DeActivateTooltip()
	{
		CancelInvoke("ActivateTooltip");
		NGUITools.SetActive(base.gameObject, false);
	}

	public void ActivateTooltip()
	{
		if (!base.gameObject.activeSelf)
		{
			NGUITools.SetActive(base.gameObject, true);
		}
	}
}
