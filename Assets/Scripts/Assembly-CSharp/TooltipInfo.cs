using UnityEngine;
using engine.unity;

public class TooltipInfo : MonoBehaviour
{
	public enum TooltipType
	{
		TOOLTIP_TYPE_ONLYTEXT = 0,
		TOOLTIP_TYPE_COMPARER = 1
	}

	public TooltipType tooltipType;

	public string text;

	public int weaponID;

	public int wearID;

	public GameObject ClicHandler;

	public bool isKarl = true;

	private void OnEnable()
	{
		GameObject gameObject_ = ScreenController.ScreenController_0.GameObject_0;
		for (int i = 0; i < gameObject_.transform.childCount; i++)
		{
			GameObject gameObject = gameObject_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf && !needDisableUIRootObject(gameObject))
			{
				isKarl = false;
			}
		}
	}

	private bool needDisableUIRootObject(GameObject gameObject_0)
	{
		if (string.Equals(gameObject_0.name, "SelectMapWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "MapListWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "CreateBattleWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "Shop@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "StockGachaWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "StockGachaRewardWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_0.name, "Lobby@Common"))
		{
			return !Lobby.Boolean_0;
		}
		return true;
	}
}
