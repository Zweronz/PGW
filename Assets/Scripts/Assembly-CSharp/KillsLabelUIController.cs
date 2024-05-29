using UnityEngine;

public class KillsLabelUIController : MonoBehaviour
{
	private UILabel uilabel_0;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
	}

	private void Update()
	{
		if (!(uilabel_0 == null) && !(HeadUpDisplay.GetPlayerMoveC() == null) && !(HeadUpDisplay.GetPlayerMoveC().NetworkStartTable_0 == null))
		{
			uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().NetworkStartTable_0.Int32_7.ToString();
		}
	}
}
