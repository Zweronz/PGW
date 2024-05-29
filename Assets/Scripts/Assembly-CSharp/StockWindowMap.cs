using UnityEngine;
using engine.unity;

public class StockWindowMap : MonoBehaviour
{
	public UILabel title;

	public UITexture bkg;

	public UISprite modeSprite;

	public ModeData mode;

	private MapData mapData_0;

	private void Start()
	{
		if (mode == null)
		{
			return;
		}
		mapData_0 = MapStorage.Get.Storage.GetObjectByKey(mode.Int32_1);
		if (mapData_0 != null)
		{
			Texture texture = ImageLoader.LoadMapPreviewTexture(mapData_0);
			if (texture != null)
			{
				bkg.Texture_0 = texture;
			}
			title.String_0 = Localizer.Get(mapData_0.String_1);
			switch (mode.ModeType_0)
			{
			case ModeType.DEATH_MATCH:
				modeSprite.String_0 = "icon_Deathmatch";
				break;
			case ModeType.TEAM_FIGHT:
				modeSprite.String_0 = "icon_Teamfight";
				break;
			default:
				modeSprite.String_0 = string.Empty;
				break;
			case ModeType.FLAG_CAPTURE:
				modeSprite.String_0 = "icon_CTF";
				break;
			}
		}
	}

	public void ToRoom()
	{
		if (SelectMapWindow.SelectMapWindow_0 != null)
		{
			SelectMapWindow.SelectMapWindow_0.Hide();
		}
		MonoSingleton<FightController>.Prop_0.JoinRandomRoom(mode);
	}
}
