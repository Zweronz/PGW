using UnityEngine;

public class LabelWidthMaximizer : MonoBehaviour
{
	public int MaxWidth;

	private UILabel uilabel_0;

	private string string_0;

	private int int_0;

	private void Start()
	{
		uilabel_0 = base.gameObject.GetComponent<UILabel>();
		if (uilabel_0 == null || MaxWidth <= 0 || uilabel_0.Overflow_0 != UILabel.Overflow.ResizeFreely)
		{
			base.enabled = false;
		}
		int_0 = uilabel_0.Int32_5;
	}

	private void Update()
	{
		if (!string.Equals(uilabel_0.String_0, string_0))
		{
			string_0 = uilabel_0.String_0;
			uilabel_0.Int32_5 = int_0;
			uilabel_0.ProcessText();
		}
		while ((float)uilabel_0.Int32_0 * uilabel_0.transform.localScale.x > (float)MaxWidth)
		{
			uilabel_0.Int32_5 -= 2;
			uilabel_0.ProcessText();
		}
	}
}
