using UnityEngine;

public class SetHeadLabelText : MonoBehaviour
{
	public UILabel[] labels;

	public void SetText(string string_0)
	{
		for (int i = 0; i < labels.Length; i++)
		{
			labels[i].String_0 = string_0;
		}
	}
}
