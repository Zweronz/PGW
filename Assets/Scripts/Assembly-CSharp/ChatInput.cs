using UnityEngine;

[RequireComponent(typeof(UIInput))]
public class ChatInput : MonoBehaviour
{
	public UITextList textList;

	public bool fillWithDummyData;

	private UIInput uiinput_0;

	private void Start()
	{
		uiinput_0 = GetComponent<UIInput>();
		uiinput_0.label.Int32_10 = 1;
		if (fillWithDummyData && textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				textList.Add(((i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]") + "This is an example paragraph for the text list, testing line " + i + "[-]");
			}
		}
	}

	public void OnSubmit()
	{
		if (textList != null)
		{
			string text = NGUIText.StripSymbols(uiinput_0.String_2);
			if (!string.IsNullOrEmpty(text))
			{
				textList.Add(text);
				uiinput_0.String_2 = string.Empty;
				uiinput_0.Boolean_2 = false;
			}
		}
	}
}
