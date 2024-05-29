using System.Collections.Generic;
using Photon;
using UnityEngine;
using engine.helpers;
using engine.unity;
using pixelgun.tutorial;

public class BonusMapController : Photon.MonoBehaviour
{
	public enum BonusType
	{
		Ammo = 0,
		HealthFull = 1,
		Armor = 2,
		Grenade = 3,
		Explosion = 4,
		Health45 = 5
	}

	public static BonusMapController bonusMapController_0;

	private Dictionary<string, MapPointBonusController> dictionary_0 = new Dictionary<string, MapPointBonusController>();

	public static MapBonusPoint GetPointByName(string string_0)
	{
		if (MonoSingleton<FightController>.Prop_0.ModeData_0 != null)
		{
			if (!MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0)
			{
				return MonoSingleton<FightController>.Prop_0.ModeData_0.GetMapBonusPoint(string_0);
			}
			if (FightOfflineController.FightOfflineController_0 != null && FightOfflineController.FightOfflineController_0.WaveMonstersData_0 != null)
			{
				return FightOfflineController.FightOfflineController_0.WaveMonstersData_0.GetMapBonusPoint(string_0);
			}
		}
		return null;
	}

	public static MapBonusPoint GetMapBonusPoint(string string_0, Dictionary<string, MapBonusPoint> dictionary_1, List<MapBonusPoint> list_0, int int_0, bool bool_0)
	{
		if (dictionary_1.Count == 0)
		{
			dictionary_1 = new Dictionary<string, MapBonusPoint>();
			if (list_0 != null && list_0.Count > 0)
			{
				MapBonusPoint mapBonusPoint = null;
				for (int i = 0; i < list_0.Count; i++)
				{
					mapBonusPoint = list_0[i];
					MapBonusData objectByKey = MapBonusStorage.Get.Storage.GetObjectByKey(mapBonusPoint.Int32_0);
					if (objectByKey != null && objectByKey.List_0 != null && objectByKey.List_0.Count != 0)
					{
						if (!dictionary_1.ContainsKey(mapBonusPoint.String_0))
						{
							dictionary_1.Add(mapBonusPoint.String_0, mapBonusPoint);
						}
						else
						{
							Log.AddLine(string.Format("[BonusMapController::GetMapBonusPoint] ERROR: two points with key {0} in {1} = {2}", mapBonusPoint.String_0, (!bool_0) ? "ModeData" : "WaveMonstersData", int_0), Log.LogLevel.ERROR);
						}
					}
					else
					{
						Log.AddLine("[BonusMapController::GetMapBonusPoint] ERROR: mbd == null || mbd.items == null || mbd.items.Count == 0", Log.LogLevel.ERROR);
					}
				}
			}
		}
		if (dictionary_1.ContainsKey(string_0))
		{
			return dictionary_1[string_0];
		}
		return null;
	}

	public static MapBonusItemData GetItem(List<MapBonusWeightItemData> list_0)
	{
		List<MapBonusWeightItemData> list = new List<MapBonusWeightItemData>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < list_0.Count; i++)
		{
			MapBonusItemData objectByKey = MapBonusItemStorage.Get.Storage.GetObjectByKey(list_0[i].Int32_0);
			if (objectByKey != null && (objectByKey.NeedsData_0 == null || objectByKey.NeedsData_0.Check()))
			{
				list.Add(list_0[i]);
				if (list_0[i].Int32_1 == 0)
				{
					num++;
				}
				else
				{
					num2 += list_0[i].Int32_1;
				}
			}
		}
		int num3 = 0;
		if (num > 0)
		{
			num3 = Random.Range(0, num);
			int num4 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].Int32_1 == 0)
				{
					if (num4 == num3)
					{
						return MapBonusItemStorage.Get.Storage.GetObjectByKey(list[j].Int32_0);
					}
					num4++;
				}
			}
		}
		num3 = Random.Range(0, num2);
		int num5 = 0;
		int num6 = 0;
		while (true)
		{
			if (num6 < list.Count)
			{
				num5 += list[num6].Int32_1;
				if (num5 > num3)
				{
					break;
				}
				num6++;
				continue;
			}
			return null;
		}
		return MapBonusItemStorage.Get.Storage.GetObjectByKey(list[num6].Int32_0);
	}

	public void AddPoint(MapPointBonusController mapPointBonusController_0, string string_0)
	{
		if (!dictionary_0.ContainsKey(string_0))
		{
			dictionary_0.Add(string_0, mapPointBonusController_0);
		}
	}

	public MapPointBonusController GetPoint(string string_0)
	{
		if (dictionary_0.ContainsKey(string_0))
		{
			return dictionary_0[string_0];
		}
		return null;
	}

	public void DeleteObjectFromPoint(string string_0)
	{
		MapPointBonusController point = GetPoint(string_0);
		if (point != null)
		{
			point.SetMapBonusObject(null);
		}
	}

	public void CreateMapPoints()
	{
		if (MonoSingleton<FightController>.Prop_0.ModeData_0 == null)
		{
			return;
		}
		foreach (KeyValuePair<string, MapPointBonusController> item in dictionary_0)
		{
			item.Value.DeleteBonus();
			PhotonNetwork.Destroy(item.Value.gameObject);
		}
		dictionary_0.Clear();
		GameObject[] array = GameObject.FindGameObjectsWithTag("BonusCreationZone");
		Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
		if (array != null && array.Length > 0)
		{
			GameObject gameObject = null;
			for (int i = 0; i < array.Length; i++)
			{
				gameObject = array[i];
				if (!dictionary.ContainsKey(gameObject.name))
				{
					dictionary.Add(gameObject.name, gameObject);
				}
			}
		}
		foreach (KeyValuePair<string, GameObject> item2 in dictionary)
		{
			string string_ = "Prefabs/Bonuses/MapPoints/" + item2.Key;
			MapBonusPoint pointByName = GetPointByName(item2.Key);
			if (pointByName != null)
			{
				GameObject gameObject2 = PhotonNetwork.InstantiateSceneObject(string_, dictionary[item2.Key].transform.position, dictionary[item2.Key].transform.rotation, 0, null);
				if (!(gameObject2 == null))
				{
				}
			}
		}
	}

	private void Awake()
	{
		bonusMapController_0 = this;
	}

	private void Start()
	{
		if (PhotonNetwork.Boolean_9)
		{
			CreateMapPoints();
		}
	}

	private void OnDestroy()
	{
		bonusMapController_0 = null;
	}

	public GameObject AddBonus(Vector3 vector3_0, int int_0)
	{
		if (PhotonNetwork.Boolean_9)
		{
			return CreateBonus(int_0, vector3_0);
		}
		base.PhotonView_0.RPC("AddBonusAfterKillPlayerRPC", PhotonTargets.MasterClient, int_0, vector3_0);
		return null;
	}

	public GameObject CreateBonus(int int_0, Vector3 vector3_0, GameObject gameObject_0 = null)
	{
		Vector3 vector3_ = vector3_0;
		if (gameObject_0 != null)
		{
			BoxCollider component = gameObject_0.GetComponent<BoxCollider>();
			Vector2 vector = new Vector2(component.size.x * gameObject_0.transform.localScale.x, component.size.z * gameObject_0.transform.localScale.z);
			Rect rect = new Rect(gameObject_0.transform.position.x - vector.x / 2f, gameObject_0.transform.position.z - vector.y / 2f, vector.x, vector.y);
			vector3_ = new Vector3(rect.x, gameObject_0.transform.position.y, rect.y);
			if (!TutorialController.TutorialController_0.Boolean_0)
			{
				vector3_.x += Random.Range(0f, rect.width);
				vector3_.z += Random.Range(0f, rect.height);
			}
			else
			{
				vector3_.x += rect.width * 0.5f;
				vector3_.z += rect.height * 0.5f;
			}
		}
		GameObject gameObject = PhotonNetwork.InstantiateSceneObject("Prefabs/Bonuses/MapBonusObject", vector3_, Quaternion.identity, 0, null);
		if (gameObject != null)
		{
			MapBonusObject component2 = gameObject.GetComponent<MapBonusObject>();
			component2.Init(int_0, (!(gameObject_0 == null)) ? gameObject_0.name : string.Empty);
		}
		return gameObject;
	}

	[RPC]
	private void AddBonusAfterKillPlayerRPC(int int_0, Vector3 vector3_0)
	{
		if (PhotonNetwork.Boolean_9)
		{
			CreateBonus(int_0, vector3_0);
		}
	}
}
