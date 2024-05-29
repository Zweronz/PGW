using UnityEngine;

public class ComplaintPanelSecondItem : MonoBehaviour
{
	public BattleOverWindow myWindow;

	public UILabel text;

	public int myNum;

	private void Update()
	{
		int num = 0;
		int num2 = 0;
		if (myNum == 1)
		{
			num = myWindow.Int32_0;
			num2 = 10;
		}
		else if (myNum == 2)
		{
			num = myWindow.Int32_1;
			num2 = 10;
		}
		text.String_0 = string.Format("{0}/{1}", num, num2);
	}
}
