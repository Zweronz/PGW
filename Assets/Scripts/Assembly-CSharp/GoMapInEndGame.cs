using UnityEngine;
using engine.unity;

public class GoMapInEndGame : MonoBehaviour
{
	public ModeData mode;

	public UITexture mapTexture;

	public UILabel mapLabel;

	private void Start()
	{
		if (!Defs.bool_3)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void SetMap(ModeData modeData_0)
	{
		mode = modeData_0;
		MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(mode.Int32_1);
		mapTexture.Texture_0 = Resources.Load<Texture>("LevelLoadingsSmall/Loading_" + objectByKey.String_0);
		mapLabel.String_0 = Defs2.dictionary_0[objectByKey.String_0];
	}

	public void OnClick()
	{
		MapStorage.Get.Storage.GetObjectByKey(mode.Int32_1);
		if (WeaponManager.weaponManager_0 != null)
		{
			WeaponManager.weaponManager_0.Reset();
		}
		MonoSingleton<FightController>.Prop_0.SwitchRoom(mode);
	}
}
