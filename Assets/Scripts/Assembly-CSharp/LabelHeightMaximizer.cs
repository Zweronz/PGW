using UnityEngine;

public class LabelHeightMaximizer : MonoBehaviour
{
	public int MaxHeight;

	public bool isUpdate;

	private UILabel uilabel_0;

	private string string_0;

	private int int_0;

	private bool bool_0;

	private void Start()
	{
		uilabel_0 = base.gameObject.GetComponent<UILabel>();
		if (!(uilabel_0 == null) && MaxHeight > 0 && uilabel_0.Overflow_0 == UILabel.Overflow.ResizeHeight)
		{
			int_0 = uilabel_0.Int32_5;
			bool_0 = true;
		}
		else
		{
			base.enabled = false;
		}
	}

	public void Update()
	{
		if (!bool_0)
		{
			Start();
		}
		if (bool_0)
		{
			if (!string.Equals(uilabel_0.String_0, string_0))
			{
				string_0 = uilabel_0.String_0;
				uilabel_0.Int32_5 = int_0;
				uilabel_0.ProcessText();
			}
			while ((float)uilabel_0.Int32_1 * uilabel_0.transform.localScale.y > (float)MaxHeight)
			{
				uilabel_0.Int32_5 -= 2;
				uilabel_0.ProcessText();
			}
			if (!isUpdate)
			{
				base.enabled = false;
			}
		}
	}
}
