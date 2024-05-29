using UnityEngine;

public class ComplaintPanelItem : MonoBehaviour
{
	public BattleOverWindow myWindow;

	public UILabel text;

	public GameObject sendText;

	public GameObject sendSprite;

	public GameObject rect;

	public BattleOverPlayerData.ComplaintType type;

	public bool sended;

	private void Start()
	{
		SetActiveContent();
	}

	public void Click()
	{
		if (!sended)
		{
			sended = true;
			myWindow.OnComplaint(this);
			SetActiveContent();
		}
	}

	public void SetActiveContent()
	{
		sendText.SetActive(sended);
		sendSprite.SetActive(sended);
		rect.SetActive(!sended);
	}

	private void Update()
	{
		if (type < BattleOverPlayerData.ComplaintType.WALL_HACK)
		{
			if (myWindow.Int32_0 >= 10)
			{
				rect.SetActive(false);
			}
		}
		else if (myWindow.Int32_1 >= 10)
		{
			rect.SetActive(false);
		}
	}
}
