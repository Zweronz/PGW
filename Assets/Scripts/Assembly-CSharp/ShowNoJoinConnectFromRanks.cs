using UnityEngine;

public class ShowNoJoinConnectFromRanks : MonoBehaviour
{
	public float showTimer;

	public UILabel label;

	public GameObject panelMessage;

	public static ShowNoJoinConnectFromRanks showNoJoinConnectFromRanks_0;

	private void Start()
	{
		showNoJoinConnectFromRanks_0 = this;
	}

	private void Update()
	{
		if (showTimer > 0f)
		{
			showTimer -= Time.deltaTime;
			if (showTimer <= 0f)
			{
				panelMessage.SetActive(false);
			}
		}
	}

	public void resetShow(int int_0)
	{
		label.String_0 = "Reach rank " + int_0 + "  to play this mode!";
		panelMessage.SetActive(true);
		showTimer = 3f;
	}

	private void OnDestroy()
	{
		showNoJoinConnectFromRanks_0 = null;
	}
}
