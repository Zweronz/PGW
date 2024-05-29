using UnityEngine;

public class ConnectToServerLabelAnim : MonoBehaviour
{
	public UILabel myLabel;

	public string startText;

	private int int_0;

	private float float_0;

	private float float_1 = 1f;

	private void Start()
	{
		float_0 = float_1;
		startText = LocalizationStore.String_32;
		myLabel.String_0 = startText;
	}

	private void Update()
	{
		float_0 -= Time.deltaTime;
		if (float_0 < 0f)
		{
			float_0 = float_1;
			int_0++;
			if (int_0 > 3)
			{
				int_0 = 0;
			}
			string text = string.Empty;
			for (int i = 0; i < int_0; i++)
			{
				text += ".";
			}
			myLabel.String_0 = string.Format("{0} {1}", startText, text);
		}
	}
}
