using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

public static class LocalizationStore
{
	private static int int_0;

	private static LanguageSource languageSource_0;

	public static string String_0
	{
		get
		{
			return Get("Key_0190");
		}
	}

	public static string String_1
	{
		get
		{
			return Get("Key_0193");
		}
	}

	public static string String_2
	{
		get
		{
			return Get("Key_0204");
		}
	}

	public static string String_3
	{
		get
		{
			return Get("Key_0207");
		}
	}

	public static string String_4
	{
		get
		{
			return Get("Key_0226");
		}
	}

	public static string String_5
	{
		get
		{
			return Get("Key_0275");
		}
	}

	public static string String_6
	{
		get
		{
			return Get("Key_0318");
		}
	}

	public static string String_7
	{
		get
		{
			return Get("Key_0319");
		}
	}

	public static string String_8
	{
		get
		{
			return Get("Key_0348");
		}
	}

	public static string String_9
	{
		get
		{
			return Get("Key_0349");
		}
	}

	public static string String_10
	{
		get
		{
			return Get("Key_0350");
		}
	}

	public static string String_11
	{
		get
		{
			return Get("Key_0351");
		}
	}

	public static string String_12
	{
		get
		{
			return Get("Key_0419");
		}
	}

	public static string String_13
	{
		get
		{
			return Get("Key_0545");
		}
	}

	public static string String_14
	{
		get
		{
			return Get("Key_0546");
		}
	}

	public static string String_15
	{
		get
		{
			return Get("Key_0547");
		}
	}

	public static string String_16
	{
		get
		{
			return Get("Key_0548");
		}
	}

	public static string String_17
	{
		get
		{
			return Get("Key_0549");
		}
	}

	public static string String_18
	{
		get
		{
			return Get("Key_0550");
		}
	}

	public static string String_19
	{
		get
		{
			return Get("Key_0551");
		}
	}

	public static string String_20
	{
		get
		{
			return Get("Key_0552");
		}
	}

	public static string String_21
	{
		get
		{
			return Get("Key_0553");
		}
	}

	public static string String_22
	{
		get
		{
			return Get("Key_0554");
		}
	}

	public static string String_23
	{
		get
		{
			return Get("Key_0555");
		}
	}

	public static string String_24
	{
		get
		{
			return Get("Key_0556");
		}
	}

	public static string String_25
	{
		get
		{
			return Get("Key_0557");
		}
	}

	public static string String_26
	{
		get
		{
			return Get("Key_0558");
		}
	}

	public static string String_27
	{
		get
		{
			return Get("Key_0559");
		}
	}

	public static string String_28
	{
		get
		{
			return Get("Key_0560");
		}
	}

	public static string String_29
	{
		get
		{
			return Get("Key_0561");
		}
	}

	public static string String_30
	{
		get
		{
			return Get("Key_0562");
		}
	}

	public static string String_31
	{
		get
		{
			return Get("Key_0563");
		}
	}

	public static string String_32
	{
		get
		{
			return Get("Key_0564");
		}
	}

	public static string String_33
	{
		get
		{
			return Get("Key_0565");
		}
	}

	public static string String_34
	{
		get
		{
			return Get("Key_0566");
		}
	}

	public static string String_35
	{
		get
		{
			return Get("Key_0567");
		}
	}

	public static string String_36
	{
		get
		{
			return Get("Key_0568");
		}
	}

	public static string String_37
	{
		get
		{
			return Get("Key_0569");
		}
	}

	public static string String_38
	{
		get
		{
			return Get("Key_0570");
		}
	}

	public static string String_39
	{
		get
		{
			return Get("Key_0571");
		}
	}

	public static string String_40
	{
		get
		{
			return Get("Key_0588");
		}
	}

	public static string String_41
	{
		get
		{
			return Get("Key_0589");
		}
	}

	public static string String_42
	{
		get
		{
			return Get("ui.rank_table.team.diggers");
		}
	}

	public static string String_43
	{
		get
		{
			return Get("ui.rank_table.team.kriters");
		}
	}

	public static string String_44
	{
		get
		{
			return LocalizationManager.String_0;
		}
		set
		{
			LocalizationManager.String_0 = value;
		}
	}

	public static string Get(string string_0)
	{
		return LocalizationManager.GetTranslation(string_0);
	}

	public static string GetByDefault(string string_0)
	{
		return LocalizationManager.GetTermTranslationByDefault(string_0);
	}

	public static void AddEventCallAfterLocalize(LocalizationManager.OnLocalizeCallback onLocalizeCallback_0)
	{
		LocalizationManager.OnLocalizeEvent += onLocalizeCallback_0;
	}

	public static void ImportWeaponLocalizeToSource(string string_0, string string_1)
	{
		if (languageSource_0 == null)
		{
			GameObject gameObject = Resources.Load("I2Languages") as GameObject;
			languageSource_0 = ((!gameObject) ? null : gameObject.GetComponent<LanguageSource>());
			if (languageSource_0 == null)
			{
				Debug.Log("Not found LanguageResource. Process stop!");
				return;
			}
		}
		TermData termData = null;
		if (languageSource_0.ContainsTerm(string_0))
		{
			termData = languageSource_0.GetTermData(string_0);
			if (termData != null && termData.Languages[int_0] != string_1)
			{
				termData.Languages[int_0] = string_1;
			}
		}
		else
		{
			termData = languageSource_0.AddTerm(string_0, eTermType.Text);
			termData.Languages[int_0] = string_1;
		}
	}

	public static Font GetFontByLocalize(string string_0)
	{
		string path = Get(string_0);
		return Resources.Load<Font>(path);
	}

	public static int GetCurrentLanguageIndex()
	{
		List<string> allLanguages = LocalizationManager.GetAllLanguages();
		if (allLanguages != null && allLanguages.Count != 0)
		{
			int num = 0;
			while (true)
			{
				if (num < allLanguages.Count)
				{
					if (allLanguages[num] == String_44)
					{
						break;
					}
					num++;
					continue;
				}
				return -1;
			}
			return num;
		}
		return -1;
	}

	public static string GetCurrentLanguageCode()
	{
		string languageCode = LocalizationManager.GetLanguageCode(String_44);
		if (languageCode.Contains("ru"))
		{
			return "ru";
		}
		if (languageCode.Contains("en"))
		{
			return "en";
		}
		return languageCode;
	}
}
