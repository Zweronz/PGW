using UnityEngine;

public class LobbyClanButtonLocalizer : MonoBehaviour
{
	public UILabel label;

	private byte byte_0;

	private void Update()
	{
		byte b = (byte)((ClanController.ClanController_0.UserClanData_0 == null) ? 1 : 2);
		if (b != byte_0)
		{
			label.String_0 = Localizer.Get((ClanController.ClanController_0.UserClanData_0 != null) ? "ui.lobby.btn.my_clan" : "ui.lobby.btn.clans");
			byte_0 = b;
		}
	}
}
