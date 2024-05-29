using UnityEngine;

public class StartHeadUp : MonoBehaviour
{
	private void Start()
	{
		if (Defs.bool_3 && (!Defs.bool_3 || PhotonNetwork.Room_0 == null || PhotonNetwork.Room_0.Hashtable_0["Pass"].Equals(string.Empty)))
		{
			if (!Defs.bool_6 && !Defs.bool_5)
			{
				if (Defs.bool_4)
				{
					GetComponent<UILabel>().String_0 = LocalizationStore.String_30;
				}
				else
				{
					GetComponent<UILabel>().String_0 = LocalizationStore.String_31;
				}
			}
			else
			{
				GetComponent<UILabel>().String_0 = LocalizationStore.String_29;
			}
		}
		else
		{
			GetComponent<UILabel>().String_0 = LocalizationStore.String_28;
		}
	}

	private void Update()
	{
	}
}
