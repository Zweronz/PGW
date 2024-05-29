using System;
using System.Collections.Generic;
using engine.helpers;
using UnityEngine;

public sealed class ArtikulController
{
	private static ArtikulController artikulController_0;

	public static ArtikulController ArtikulController_0
	{
		get
		{
			if (artikulController_0 == null)
			{
				artikulController_0 = new ArtikulController();
			}
			return artikulController_0;
		}
	}

	private ArtikulController()
	{
	}

	public Dictionary<int, ArtikulData> artikuls
	{
		get
		{
			if (_artikuls == null)
			{
				Dictionary<int, ArtikulData> artikulData = new Dictionary<int, ArtikulData>();
				Article article = Resources.Load<Article>("Article");

				foreach (Article.ArticleData articleData in article.articleData)
				{
					artikulData.Add(articleData.id, articleData.ToArtikul());
				}

				_artikuls = artikulData;
			}

			return _artikuls;
		}
	}

	private Dictionary<int, ArtikulData> _artikuls;

	public ArtikulData GetArtikul(int int_0)
	{
		return artikuls[int_0];//ArtikulStorage.Get.Storage.GetObjectByKey(int_0);
	}

	public List<ArtikulData> GetArtikulsBySlot(SlotType slotType_0)
	{
		return ArtikulStorage.Get.Storage.Search(0, slotType_0);
	}

	public ArtikulData GetArtikulByPrefabName(string string_0)
	{
		return ArtikulStorage.Get.Storage.SearchUnique(1, string_0);
	}

	public ArtikulData GetArtikulByPrefabTag(string string_0)
	{
		return ArtikulStorage.Get.Storage.SearchUnique(2, string_0);
	}

	public SkillData GetSkill(int int_0, SkillId skillId_0)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul != null && artikul.Dictionary_0 != null && artikul.Dictionary_0.ContainsKey(skillId_0))
		{
			return artikul.Dictionary_0[skillId_0];
		}
		if (int_0 > 0)
		{
		}
		return null;
	}

	public void GetSkills(int int_0, ref List<SkillData> list_0, bool bool_0 = false)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul == null)
		{
			return;
		}
		if (list_0 == null)
		{
			list_0 = new List<SkillData>(artikul.Dictionary_0.Count);
		}
		NeedData needData_ = null;
		IEnumerator<KeyValuePair<SkillId, SkillData>> enumerator = artikul.Dictionary_0.GetEnumerator();
		while (enumerator.MoveNext())
		{
			SkillData value = enumerator.Current.Value;
			if (!bool_0)
			{
				list_0.Add(value);
			}
			else if (value.NeedsData_0.Check(out needData_))
			{
				list_0.Add(value);
			}
		}
	}

	public void GetSkills(int int_0, ref Dictionary<SkillId, SkillData> dictionary_0, bool bool_0 = false)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul == null || artikul.Dictionary_0 == null)
		{
			return;
		}
		if (!bool_0)
		{
			artikul.Dictionary_0.ToDictionary(dictionary_0);
			return;
		}
		if (dictionary_0 == null)
		{
			dictionary_0 = new Dictionary<SkillId, SkillData>();
		}
		NeedData needData_ = null;
		IEnumerator<KeyValuePair<SkillId, SkillData>> enumerator = artikul.Dictionary_0.GetEnumerator();
		while (enumerator.MoveNext())
		{
			SkillData value = enumerator.Current.Value;
			if (value.NeedsData_0.Check(out needData_))
			{
				if (dictionary_0.ContainsKey(enumerator.Current.Key))
				{
					dictionary_0[enumerator.Current.Key] = enumerator.Current.Value;
				}
				else
				{
					dictionary_0.Add(enumerator.Current.Key, enumerator.Current.Value);
				}
			}
		}
	}

	public void ApplaySkillsForArtikuls(Dictionary<SkillId, SkillData> dictionary_0, params int[] int_0)
	{
		if (int_0.Length == 0)
		{
			return;
		}
		if (dictionary_0 == null)
		{
			dictionary_0 = new Dictionary<SkillId, SkillData>();
		}
		Dictionary<SkillId, SkillData> dictionary_ = new Dictionary<SkillId, SkillData>();
		for (int i = 0; i < int_0.Length; i++)
		{
			if (int_0[i] == 0)
			{
				continue;
			}
			dictionary_.Clear();
			GetSkills(int_0[i], ref dictionary_, true);
			foreach (KeyValuePair<SkillId, SkillData> item in dictionary_)
			{
				if (!dictionary_0.ContainsKey(item.Key))
				{
					dictionary_0.Add(item.Key, new SkillData
					{
						Single_1 = 1f,
						Int32_1 = 1
					});
				}
				SkillData skillData = dictionary_0[item.Key];
				skillData.Int32_0 += item.Value.Int32_0;
				skillData.Int32_1 *= ((item.Value.Int32_1 == 0) ? 1 : item.Value.Int32_1);
				skillData.Single_0 += item.Value.Single_0;
				skillData.Single_1 *= ((item.Value.Single_1 != 0f) ? item.Value.Single_1 : 1f);
			}
		}
	}

	public ArtikulData GetDowngrade(int int_0)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul == null)
		{
			if (int_0 > 0)
			{
				Log.AddLine(string.Format("ArtikulController::GetDowngrade > artikul {0} is null", int_0));
			}
			return null;
		}
		ArtikulData artikul2 = GetArtikul(artikul.Int32_2);
		if (artikul2 == null)
		{
			if (artikul.Int32_2 > 0)
			{
				Log.AddLine(string.Format("ArtikulController::GetDowngrade > downgrade artikul {0} is null", artikul.Int32_2));
			}
			return null;
		}
		return artikul2;
	}

	private void GetPrevDowngrade(int int_0, ref List<ArtikulData> list_0)
	{
		//ArtikulData downgrade = GetDowngrade(int_0);
		//if (downgrade != null)
		//{
		//	if (list_0.Count > 20)
		//	{
		//		throw new Exception("Are you fucking idiots?! >>> ArtikulController::GetPrevDowngrade overhead recursive calls!");
		//	}
		//	list_0.Add(downgrade);
		//	GetPrevDowngrade(downgrade.Int32_0, ref list_0);
		//}
	}

	public List<ArtikulData> GetDowngrades(int int_0)
	{
		List<ArtikulData> list_ = new List<ArtikulData>();
		GetPrevDowngrade(int_0, ref list_);
		return list_;
	}

	public ArtikulData GetUpgrade(int int_0)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul == null)
		{
			if (int_0 > 0)
			{
				Log.AddLine(string.Format("ArtikulController::GetUpgrade > artikul {0} is null", int_0));
			}
			return null;
		}
		ArtikulData artikul2 = GetArtikul(artikul.Int32_1);
		if (artikul2 == null)
		{
			if (artikul.Int32_1 > 0)
			{
				Log.AddLine(string.Format("ArtikulController::GetUpgrade > upgrade artikul {0} is null", artikul.Int32_1));
			}
			return null;
		}
		return artikul2;
	}

	private void GetNextUpgrade(int int_0, ref List<ArtikulData> list_0)
	{
		ArtikulData upgrade = GetUpgrade(int_0);
		if (upgrade != null)
		{
			if (list_0.Count > 20)
			{
				throw new Exception("Are you fucking idiots?! >>> ArtikulController::GetNextUpgrade overhead recursive calls!");
			}
			list_0.Add(upgrade);
			GetNextUpgrade(upgrade.Int32_0, ref list_0);
		}
	}

	public List<ArtikulData> GetUpgrades(int int_0)
	{
		List<ArtikulData> list_ = new List<ArtikulData>();
		GetNextUpgrade(int_0, ref list_);
		return list_;
	}

	public bool CheckNeeds(int int_0, out NeedData needData_0)
	{
		ArtikulData artikul = GetArtikul(int_0);
		if (artikul == null)
		{
			Log.AddLine(string.Format("ArtikulController::CheckNeeds > artikul {0} is null", int_0));
			needData_0 = null;
			return false;
		}
		if (artikul.NeedsData_0 == null)
		{
			needData_0 = null;
			return true;
		}
		return artikul.NeedsData_0.Check(out needData_0);
	}
}
