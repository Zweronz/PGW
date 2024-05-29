using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ShopItemInfoUpgrades : MonoBehaviour
{
	public UISprite[] fills;

	public UISprite[] bkgs;

	public void Init(ArtikulData artikulData_0)
	{
		if (artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_ARMOR)
		{
			if (artikulData_0.Int32_2 <= 0 && artikulData_0.Int32_1 <= 0)
			{
				SetCells(0);
				ActiveCells(0);
				return;
			}
			Regex regex = new Regex("(UP|LVL)\\d+");
			Match match = regex.Match(artikulData_0.String_4);
			int result = 0;
			if (match != null)
			{
				if (match.Value.Contains("UP"))
				{
					int.TryParse(match.Value.Substring(2, 1), out result);
				}
				else if (match.Value.Contains("LVL"))
				{
					int.TryParse(match.Value.Substring(3, 1), out result);
				}
				SetCells(3);
				ActiveCells(result + 1);
			}
		}
		else if (artikulData_0.Int32_2 > 0)
		{
			List<ArtikulData> downgrades = ArtikulController.ArtikulController_0.GetDowngrades(artikulData_0.Int32_0);
			List<ArtikulData> upgrades = ArtikulController.ArtikulController_0.GetUpgrades(artikulData_0.Int32_0);
			SetCells(downgrades.Count + 1 + upgrades.Count);
			ActiveCells(downgrades.Count + 1);
		}
		else if (artikulData_0.Int32_1 > 0)
		{
			List<ArtikulData> upgrades2 = ArtikulController.ArtikulController_0.GetUpgrades(artikulData_0.Int32_0);
			SetCells(upgrades2.Count + 1);
			bool flag = UserController.UserController_0.HasUserArtikul(artikulData_0.Int32_0);
			ActiveCells(flag ? 1 : 0);
		}
		else
		{
			SetCells(0);
			ActiveCells(0);
		}
	}

	private void SetCells(int int_0)
	{
		if (int_0 <= bkgs.Length)
		{
			for (int i = 0; i < bkgs.Length; i++)
			{
				NGUITools.SetActive(bkgs[i].gameObject, i < int_0);
			}
		}
	}

	private void ActiveCells(int int_0)
	{
		if (int_0 <= fills.Length)
		{
			for (int i = 0; i < fills.Length; i++)
			{
				NGUITools.SetActive(fills[i].gameObject, i < int_0);
			}
		}
	}
}
