using UnityEngine;
using pixelgun.tutorial;

public class MapPointBonusController : MonoBehaviour
{
	private MapBonusPoint mapBonusPoint_0;

	private MapBonusObject mapBonusObject_0;

	private GameObject gameObject_0;

	private float float_0;

	private void Awake()
	{
		string text = base.gameObject.name.Replace("(Clone)", string.Empty);
		mapBonusPoint_0 = BonusMapController.GetPointByName(text);
		if (mapBonusPoint_0 == null)
		{
			base.enabled = false;
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("BonusCreationZone");
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Equals(array[i].name, text))
				{
					gameObject_0 = array[i];
					break;
				}
			}
		}
		if (gameObject_0 == null)
		{
			base.enabled = false;
			return;
		}
		BonusMapController.bonusMapController_0.AddPoint(this, gameObject_0.name);
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		float_0 = mapBonusPoint_0.Int32_1;
	}

	private void Update()
	{
		if (mapBonusObject_0 != null)
		{
			return;
		}
		float_0 -= Time.deltaTime;
		if (float_0 <= 0f)
		{
			float_0 = mapBonusPoint_0.Int32_1;
			if (PhotonNetwork.Boolean_9)
			{
				tryCreateBonus();
			}
		}
	}

	private void tryCreateBonus()
	{
		if (!(BonusMapController.bonusMapController_0 == null) && Random.Range(1, 101) <= mapBonusPoint_0.Int32_2)
		{
			MapBonusData objectByKey = MapBonusStorage.Get.Storage.GetObjectByKey(mapBonusPoint_0.Int32_0);
			MapBonusItemData item = objectByKey.GetItem();
			if (item != null)
			{
				BonusMapController.bonusMapController_0.CreateBonus(item.Int32_0, new Vector3(0f, 0f, 0f), gameObject_0);
			}
		}
	}

	public void SetMapBonusObject(MapBonusObject mapBonusObject_1)
	{
		mapBonusObject_0 = mapBonusObject_1;
	}

	public void SetZeroTime()
	{
		float_0 = 0f;
		Update();
	}

	public void DeleteBonus()
	{
		if (mapBonusObject_0 != null)
		{
			mapBonusObject_0.DestroyItem();
			mapBonusObject_0 = null;
		}
	}
}
