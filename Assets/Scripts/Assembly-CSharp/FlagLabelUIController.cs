using UnityEngine;

public class FlagLabelUIController : MonoBehaviour
{
	public bool myCommand;

	private UILabel uilabel_0;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
	}

	private void Update()
	{
		if (uilabel_0 == null || HeadUpDisplay.GetPlayerMoveC() == null || HeadUpDisplay.GetPlayerMoveC().NetworkStartTable_0 == null)
		{
			return;
		}
		if (myCommand)
		{
			if (HeadUpDisplay.GetPlayerMoveC().Int32_2 == 1)
			{
				uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int16_4.ToString();
			}
			else
			{
				uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int16_5.ToString();
			}
		}
		else if (HeadUpDisplay.GetPlayerMoveC().Int32_2 == 1)
		{
			uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int16_5.ToString();
		}
		else
		{
			uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int16_4.ToString();
		}
	}
}
