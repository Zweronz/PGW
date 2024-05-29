using UnityEngine;

public class TeamScoreLabelUIController : MonoBehaviour
{
	private const float float_0 = 0.1f;

	public bool isMyCommand;

	public UILabel label;

	private float float_1;

	private void Update()
	{
		float_1 -= Time.deltaTime;
		if (!(float_1 > 0f))
		{
			float_1 = 0.1f;
			Player_move_c playerMoveC = HeadUpDisplay.GetPlayerMoveC();
			if (!(playerMoveC == null))
			{
				int num = 0;
				num = (isMyCommand ? ((playerMoveC.Int32_2 != 1) ? playerMoveC.PlayerScoreController_0.Int16_5 : playerMoveC.PlayerScoreController_0.Int16_4) : ((playerMoveC.Int32_2 != 1) ? playerMoveC.PlayerScoreController_0.Int16_4 : playerMoveC.PlayerScoreController_0.Int16_5));
				label.String_0 = num.ToString();
			}
		}
	}
}
