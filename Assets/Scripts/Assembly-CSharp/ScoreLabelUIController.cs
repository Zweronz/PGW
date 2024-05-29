using UnityEngine;

public class ScoreLabelUIController : MonoBehaviour
{
	private UILabel uilabel_0;

	private int int_0;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
		if (!(HeadUpDisplay.GetPlayerMoveC() == null) && !(HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0 == null))
		{
			uilabel_0.String_0 = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int32_0.ToString();
		}
	}

	private void Update()
	{
		if (!(uilabel_0 == null) && !(HeadUpDisplay.GetPlayerMoveC() == null) && !(HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0 == null))
		{
			int int32_ = HeadUpDisplay.GetPlayerMoveC().PlayerScoreController_0.Int32_0;
			if (int32_ != int_0)
			{
				int_0 = int32_;
				uilabel_0.String_0 = int_0.ToString();
				uilabel_0.GetComponent<Animation>().Play("ScoreChange");
			}
		}
	}
}
