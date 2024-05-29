using System.Collections.Generic;
using engine.helpers;
using UnityEngine;

public sealed class WearController
{
	private static WearController wearController_0;

	public static WearController WearController_0
	{
		get
		{
			if (wearController_0 == null)
			{
				wearController_0 = new WearController();
			}
			return wearController_0;
		}
	}

	private WearController()
	{
	}

	private static List<WearData> wear
	{
		get
		{
			if (_wear == null)
			{
				List<WearData> wearData = new List<WearData>();
				Wear wear = Resources.Load<Wear>("Wear");

				foreach (Wear.WearData _wearData in wear.wear)
				{
					wearData.Add(_wearData.ToWearData());
				}

				_wear = wearData;
			}

			return _wear;
		}
	}

	private static List<WearData> _wear;

	public WearData GetWear(int int_0)
	{
		return wear.Find(x => x.Int32_0 == int_0);
	}

	public WearData GetWearByPrefabName(string string_0)
	{
		ArtikulData artikulByPrefabName = ArtikulController.ArtikulController_0.GetArtikulByPrefabName(string_0);
		return (artikulByPrefabName != null) ? GetWear(artikulByPrefabName.Int32_0) : null;
	}

	public void EquipWear(int int_0)
	{
		WearData wear = GetWear(int_0);
		if (wear == null)
		{
			Log.AddLine(string.Format("WearController::EquipWear > wear {0} is null", int_0));
		}
		else
		{
			EquipWearByArtikulId(wear.Int32_0);
		}
	}

	public void UnequipWear(int int_0)
	{
		WearData wear = GetWear(int_0);
		if (wear == null)
		{
			Log.AddLine(string.Format("WeaponController::UnequipWear > wear {0} is null", int_0));
		}
		else
		{
			UnequipWearByArtikulId(wear.Int32_0);
		}
	}

	public void EquipWearByArtikulId(int int_0)
	{
		UserController.UserController_0.EquipArtikul(int_0);
	}

	public void UnequipWearByArtikulId(int int_0)
	{
		UserController.UserController_0.UnequipArtikul(int_0);
	}
}
