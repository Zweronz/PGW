using UnityEngine;

public class LabePlaterNameInSpectatorMode : MonoBehaviour
{
	private UILabel uilabel_0;

	public UILabel clanNameLabel;

	public UITexture clanTexture;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
	}

	private void Update()
	{
		if (uilabel_0 != null && WeaponManager.weaponManager_0.myTable != null)
		{
			uilabel_0.String_0 = WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>().String_0;
			clanNameLabel.String_0 = WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>().String_1;
			clanTexture.Texture_0 = WeaponManager.weaponManager_0.myTable.GetComponent<NetworkStartTable>().Texture_0;
		}
	}
}
