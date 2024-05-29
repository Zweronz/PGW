using System.Collections;
using UnityEngine;

public sealed class CampaignLoading : MonoBehaviour
{
	public static readonly string string_0 = "Coliseum";

	public UITexture backgroundUiTexture;

	public GameObject survivalNotesOverlay;

	public GameObject campaignNotesOverlay;

	public GameObject trainingNotesOverlay;

	public GameObject ordinaryAwardLabel;

	public GameObject stackOfCoinsLabel;

	public Texture loadingNote;

	private Texture texture_0;

	private Texture texture_1;

	private Rect rect_0;

	private IEnumerator Start()
	{
		Lobby.Lobby_0.Hide();
		if (StoreKitEventListener.gameObject_0 != null)
		{
			StoreKitEventListener.gameObject_0.SetActive(true);
		}
		string string_;
		if (!Defs.bool_0)
		{
			int num = 0;
			LevelBox levelBox = null;
			foreach (LevelBox item in LevelBox.list_0)
			{
				if (!item.string_0.Equals(CurrentCampaignGame.string_0))
				{
					continue;
				}
				levelBox = item;
				foreach (CampaignLevel item2 in item.list_1)
				{
					if (item2.string_0.Equals(CurrentCampaignGame.string_1))
					{
						num = item.list_1.IndexOf(item2);
						break;
					}
				}
			}
			bool flag = false;
			flag = num >= levelBox.list_1.Count - 1;
			bool flag2 = false;
			if (!CampaignProgress.dictionary_0[CurrentCampaignGame.string_0].ContainsKey(CurrentCampaignGame.string_1))
			{
				flag2 = true;
			}
			bool flag3 = flag2 && flag;
			string_ = ((!flag3) ? "gey_1" : "gey_15");
			if (ordinaryAwardLabel != null)
			{
				ordinaryAwardLabel.SetActive(!flag3);
			}
			if (stackOfCoinsLabel != null)
			{
				stackOfCoinsLabel.SetActive(flag3);
			}
			if (campaignNotesOverlay != null)
			{
				campaignNotesOverlay.SetActive(true);
			}
		}
		else
		{
			string_ = "gey_surv";
			if (survivalNotesOverlay != null)
			{
				survivalNotesOverlay.SetActive(true);
			}
		}
		texture_1 = Resources.Load<Texture>(ResPath.Combine("CoinsIndicationSystem", string_));
		float num2 = 500f * Defs.Single_0;
		float num3 = 244f * Defs.Single_0;
		rect_0 = new Rect(((float)Screen.width - num2) / 2f, (float)Screen.height * 0.8f - num3 / 2f, num2, num3);
		string text = ((!Defs.bool_0) ? CurrentCampaignGame.string_1 : Defs.string_1[Defs.int_18 % Defs.string_1.Length]);
		texture_0 = Resources.Load<Texture>(ResPath.Combine(string_1: "Loading_" + text, string_0: Switcher.string_0 + "/Hi"));
		if (backgroundUiTexture != null)
		{
			backgroundUiTexture.Texture_0 = texture_0;
		}
		yield return new WaitForSeconds(1f);
		FightOfflineController.FightOfflineController_0.StartFight();
	}
}
