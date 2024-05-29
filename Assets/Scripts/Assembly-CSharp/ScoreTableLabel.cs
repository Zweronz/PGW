using UnityEngine;

public class ScoreTableLabel : MonoBehaviour
{
	private void Start()
	{
		if (Defs.bool_4)
		{
			GetComponent<UILabel>().String_0 = LocalizationStore.Get("Key_0190");
		}
		else if (Defs.bool_6)
		{
			GetComponent<UILabel>().String_0 = LocalizationStore.Get("Key_1006");
		}
		else
		{
			GetComponent<UILabel>().String_0 = LocalizationStore.Get("Key_0191");
		}
	}

	private void Update()
	{
	}
}
