using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Article")]
public class Article : ScriptableObject
{
	[System.Serializable]
	public class ArticleData
	{
		public int id;

		public SlotType slotType;

		public string localizationKey, description;

		public int upgradeID, downgradeID;

		public List<Skill> skills = new List<Skill>();

		[System.Serializable]
		public class Skill
		{
			public SkillId id;

			//public SkillData data;
		}

		public string iconName, weaponPrefab;

		public int price;

		public ArtikulData ToArtikul()
		{
            return new ArtikulData
            {
                Dictionary_0 = new Dictionary<SkillId, SkillData>(),
                List_0 = new List<int>(),

                Int32_0 = id,
                SlotType_0 = slotType,

                String_0 = localizationKey,
                String_1 = description,

                Int32_1 = upgradeID,
                Int32_2 = downgradeID,

                String_2 = iconName,
                String_3 = weaponPrefab,

                Int32_5 = price
            };
		}
	}

	public List<ArticleData> articleData = new List<ArticleData>();
}
