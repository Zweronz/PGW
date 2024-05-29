using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace I2.Loc
{
	public static class LocalizationManager
	{
		public delegate void OnLocalizeCallback();

		private static string string_0;

		private static string string_1;

		public static bool bool_0 = false;

		public static List<LanguageSource> list_0 = new List<LanguageSource>();

		private static string[] string_2 = new string[20]
		{
			"ar-DZ", "ar", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA",
			"ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "he", "ur", "ji"
		};

		private static OnLocalizeCallback onLocalizeCallback_0;

		public static string String_0
		{
			get
			{
				if (string.IsNullOrEmpty(string_0))
				{
					RegisterSceneSources();
					RegisterSourceInResources();
					SelectStartupLanguage();
				}
				return string_0;
			}
			set
			{
				string supportedLanguage = GetSupportedLanguage(value);
				if (string_0 != value && !string.IsNullOrEmpty(supportedLanguage))
				{
					PlayerPrefs.SetString("I2 Language", supportedLanguage);
					string_0 = supportedLanguage;
					String_1 = GetLanguageCode(supportedLanguage);
					LocalizeAll();
				}
			}
		}

		public static string String_1
		{
			get
			{
				return string_1;
			}
			set
			{
				string_1 = value;
				bool_0 = IsRTL(string_1);
			}
		}

		public static event OnLocalizeCallback OnLocalizeEvent
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onLocalizeCallback_0 = (OnLocalizeCallback)Delegate.Combine(onLocalizeCallback_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onLocalizeCallback_0 = (OnLocalizeCallback)Delegate.Remove(onLocalizeCallback_0, value);
			}
		}

		private static void SelectStartupLanguage()
		{
			string @string = PlayerPrefs.GetString("I2 Language", string.Empty);
			string string_ = Application.systemLanguage.ToString();
			if (HasLanguage(@string))
			{
				String_0 = @string;
				return;
			}
			string supportedLanguage = GetSupportedLanguage(string_);
			if (!string.IsNullOrEmpty(supportedLanguage))
			{
				String_0 = supportedLanguage;
				return;
			}
			int num = 0;
			int count = list_0.Count;
			while (true)
			{
				if (num < count)
				{
					if (list_0[num].mLanguages.Count > 0)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			String_0 = list_0[num].mLanguages[0].Name;
		}

		public static string GetTermTranslation(string string_3)
		{
			return GetTranslation(string_3);
		}

		public static string GetTermTranslationByDefault(string string_3)
		{
			if (list_0.Count == 0)
			{
				RegisterSourceInResources();
			}
			int num = 0;
			int count = list_0.Count;
			while (true)
			{
				if (num < count)
				{
					TermData termData = list_0[num].GetTermData(string_3);
					if (termData == null)
					{
						break;
					}
					if (termData.Languages.Length == 0)
					{
						num++;
						continue;
					}
					return termData.Languages[0];
				}
				return string.Empty;
			}
			return string.Empty;
		}

		public static string GetTranslation(string string_3)
		{
			int num = 0;
			int count = list_0.Count;
			TermData termData;
			while (true)
			{
				if (num < count)
				{
					termData = list_0[num].GetTermData(string_3);
					if (termData != null)
					{
						int languageIndex = list_0[num].GetLanguageIndex(String_0);
						if (languageIndex != -1)
						{
							string text = termData.Languages[languageIndex];
							if (!string.IsNullOrEmpty(text))
							{
								return text;
							}
						}
						if (termData.Languages.Length != 0)
						{
							break;
						}
					}
					num++;
					continue;
				}
				return string_3;
			}
			return termData.Languages[0];
		}

		internal static void LocalizeAll()
		{
			Localize[] array = (Localize[])Resources.FindObjectsOfTypeAll(typeof(Localize));
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				Localize localize = array[i];
				localize.OnLocalize();
			}
			if (onLocalizeCallback_0 != null)
			{
				onLocalizeCallback_0();
			}
			ResourceManager.ResourceManager_0.CleanResourceCache();
		}

		private static void RegisterSceneSources()
		{
			LanguageSource[] array = (LanguageSource[])Resources.FindObjectsOfTypeAll(typeof(LanguageSource));
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				if (!list_0.Contains(array[i]))
				{
					AddSource(array[i]);
				}
			}
		}

		private static void RegisterSourceInResources()
		{
			GameObject asset = ResourceManager.ResourceManager_0.GetAsset<GameObject>("I2Languages");
			LanguageSource languageSource = ((!asset) ? null : asset.GetComponent<LanguageSource>());
			if ((bool)languageSource && !list_0.Contains(languageSource))
			{
				AddSource(languageSource);
			}
		}

		internal static void AddSource(LanguageSource languageSource_0)
		{
			if (!list_0.Contains(languageSource_0))
			{
				list_0.Add(languageSource_0);
				languageSource_0.Import_Google();
			}
		}

		internal static void RemoveSource(LanguageSource languageSource_0)
		{
			list_0.Remove(languageSource_0);
		}

		public static bool HasLanguage(string string_3, bool bool_1 = true)
		{
			int num = 0;
			int count = list_0.Count;
			while (true)
			{
				if (num < count)
				{
					if (list_0[num].GetLanguageIndex(string_3, false) >= 0)
					{
						break;
					}
					num++;
					continue;
				}
				if (bool_1)
				{
					int i = 0;
					for (int count2 = list_0.Count; i < count2; i++)
					{
						if (list_0[i].GetLanguageIndex(string_3) >= 0)
						{
							return true;
						}
					}
				}
				return false;
			}
			return true;
		}

		public static string GetSupportedLanguage(string string_3)
		{
			int num = 0;
			int count = list_0.Count;
			int languageIndex;
			while (true)
			{
				if (num < count)
				{
					languageIndex = list_0[num].GetLanguageIndex(string_3, false);
					if (languageIndex >= 0)
					{
						break;
					}
					num++;
					continue;
				}
				int num2 = 0;
				int count2 = list_0.Count;
				int languageIndex2;
				while (true)
				{
					if (num2 < count2)
					{
						languageIndex2 = list_0[num2].GetLanguageIndex(string_3);
						if (languageIndex2 >= 0)
						{
							break;
						}
						num2++;
						continue;
					}
					return string.Empty;
				}
				return list_0[num2].mLanguages[languageIndex2].Name;
			}
			return list_0[num].mLanguages[languageIndex].Name;
		}

		public static string GetLanguageCode(string string_3)
		{
			int num = 0;
			int count = list_0.Count;
			int languageIndex;
			while (true)
			{
				if (num < count)
				{
					languageIndex = list_0[num].GetLanguageIndex(string_3);
					if (languageIndex >= 0)
					{
						break;
					}
					num++;
					continue;
				}
				return string.Empty;
			}
			return list_0[num].mLanguages[languageIndex].Code;
		}

		public static List<string> GetAllLanguages()
		{
			List<string> list = new List<string>();
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				int j = 0;
				for (int count2 = list_0[i].mLanguages.Count; j < count2; j++)
				{
					if (!list.Contains(list_0[i].mLanguages[j].Name))
					{
						list.Add(list_0[i].mLanguages[j].Name);
					}
				}
			}
			return list;
		}

		public static UnityEngine.Object FindAsset(string string_3)
		{
			int num = 0;
			int count = list_0.Count;
			UnityEngine.Object @object;
			while (true)
			{
				if (num < count)
				{
					@object = list_0[num].FindAsset(string_3);
					if ((bool)@object)
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return @object;
		}

		private static bool IsRTL(string string_3)
		{
			return Array.IndexOf(string_2, string_3) >= 0;
		}
	}
}
