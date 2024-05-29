using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace I2.Loc
{
	public class LanguageSource : MonoBehaviour
	{
		public string Google_WebServiceURL;

		public string Google_SpreadsheetKey;

		public string Google_SpreadsheetName;

		public string Google_LastUpdatedVersion;

		private CoroutineManager coroutineManager_0;

		public static string string_0 = "Default";

		public static char[] char_0 = "/\\".ToCharArray();

		public List<TermData> mTerms = new List<TermData>();

		public List<LanguageData> mLanguages = new List<LanguageData>();

		[NonSerialized]
		public Dictionary<string, TermData> mDictionary = new Dictionary<string, TermData>();

		public UnityEngine.Object[] Assets;

		public bool NeverDestroy = true;

		public bool UserAgreesToHaveItOnTheScene;

		public string Export_CSV(string string_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = mLanguages.Count;
			stringBuilder.Append("Key,Type,Desc");
			foreach (LanguageData mLanguage in mLanguages)
			{
				stringBuilder.Append(",");
				AppendString(stringBuilder, GoogleLanguages.GetCodedLanguage(mLanguage.Name, mLanguage.Code));
			}
			stringBuilder.Append("\n");
			foreach (TermData mTerm in mTerms)
			{
				string string_2;
				if (!string.IsNullOrEmpty(string_1) && (!(string_1 == string_0) || mTerm.Term.IndexOfAny(char_0) >= 0))
				{
					if (!mTerm.Term.StartsWith(string_1) || !(string_1 != mTerm.Term))
					{
						continue;
					}
					string_2 = mTerm.Term.Substring(string_1.Length + 1);
				}
				else
				{
					string_2 = mTerm.Term;
				}
				AppendString(stringBuilder, string_2);
				stringBuilder.AppendFormat(",{0}", mTerm.TermType.ToString());
				stringBuilder.Append(",");
				AppendString(stringBuilder, mTerm.Description);
				for (int i = 0; i < Mathf.Min(count, mTerm.Languages.Length); i++)
				{
					stringBuilder.Append(",");
					AppendString(stringBuilder, mTerm.Languages[i]);
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		private static void AppendString(StringBuilder stringBuilder_0, string string_1)
		{
			if (!string.IsNullOrEmpty(string_1))
			{
				string_1 = string_1.Replace("\\n", "\n");
				if (string_1.IndexOfAny(",\n\"".ToCharArray()) >= 0)
				{
					string_1 = string_1.Replace("\"", "\"\"");
					stringBuilder_0.AppendFormat("\"{0}\"", string_1);
				}
				else
				{
					stringBuilder_0.Append(string_1);
				}
			}
		}

		public WWW Export_Google_CreateWWWcall(eSpreadsheetUpdateMode eSpreadsheetUpdateMode_0 = eSpreadsheetUpdateMode.Replace)
		{
			string value = Export_Google_CreateData();
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("key", Google_SpreadsheetKey);
			wWWForm.AddField("action", "SetLanguageSource");
			wWWForm.AddField("data", value);
			wWWForm.AddField("updateMode", eSpreadsheetUpdateMode_0.ToString());
			return new WWW(Google_WebServiceURL, wWWForm);
		}

		private string Export_Google_CreateData()
		{
			List<string> categories = GetCategories(true);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string item in categories)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append("<I2Loc>");
				}
				string value = Export_CSV(item);
				stringBuilder.Append(item);
				stringBuilder.Append("<I2Loc>");
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		public string Import_CSV(string string_1, string string_2, eSpreadsheetUpdateMode eSpreadsheetUpdateMode_0 = eSpreadsheetUpdateMode.Replace)
		{
			List<string[]> list = LocalizationReader.ReadCSV(string_2);
			string[] array = list[0];
			if (eSpreadsheetUpdateMode_0 == eSpreadsheetUpdateMode.Replace)
			{
				ClearAllData();
			}
			int num = Mathf.Max(array.Length - 3, 0);
			int[] array2 = new int[num];
			for (int i = 0; i < num; i++)
			{
				string string_3;
				string string_4;
				GoogleLanguages.UnPackCodeFromLanguageName(array[i + 3], out string_3, out string_4);
				int num2 = GetLanguageIndex(string_3);
				if (num2 < 0)
				{
					LanguageData languageData = new LanguageData();
					languageData.Name = string_3;
					languageData.Code = string_4;
					mLanguages.Add(languageData);
					num2 = mLanguages.Count - 1;
				}
				array2[i] = num2;
			}
			num = mLanguages.Count;
			int j = 0;
			for (int count = mTerms.Count; j < count; j++)
			{
				TermData termData = mTerms[j];
				if (termData.Languages.Length < num)
				{
					Array.Resize(ref termData.Languages, num);
				}
			}
			int k = 1;
			for (int count2 = list.Count; k < count2; k++)
			{
				array = list[k];
				string string_5 = ((!string.IsNullOrEmpty(string_1)) ? (string_1 + "/" + array[0]) : array[0]);
				ValidateFullTerm(ref string_5);
				TermData termData2 = GetTermData(string_5);
				if (termData2 == null)
				{
					termData2 = new TermData();
					termData2.Term = string_5;
					termData2.Languages = new string[mLanguages.Count];
					for (int l = 0; l < mLanguages.Count; l++)
					{
						termData2.Languages[l] = string.Empty;
					}
					mTerms.Add(termData2);
					mDictionary.Add(string_5, termData2);
				}
				else if (eSpreadsheetUpdateMode_0 == eSpreadsheetUpdateMode.AddNewTerms)
				{
					continue;
				}
				termData2.TermType = GetTermType(array[1]);
				termData2.Description = array[2];
				for (int m = 0; m < array2.Length && m < array.Length - 3; m++)
				{
					if (!string.IsNullOrEmpty(array[m + 3]))
					{
						termData2.Languages[array2[m]] = array[m + 3];
					}
				}
			}
			return string.Empty;
		}

		public static eTermType GetTermType(string string_1)
		{
			int num = 0;
			int num2 = 8;
			while (true)
			{
				if (num <= num2)
				{
					if (string.Equals(((eTermType)num).ToString(), string_1, StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
					num++;
					continue;
				}
				return eTermType.Text;
			}
			return (eTermType)num;
		}

		public void Import_Google()
		{
			if (Application.isPlaying && !(coroutineManager_0 != null))
			{
				string @string = PlayerPrefs.GetString("I2Source_" + Google_SpreadsheetKey, string.Empty);
				if (!string.IsNullOrEmpty(@string))
				{
					Import_Google_Result(@string, eSpreadsheetUpdateMode.Replace);
				}
				GameObject gameObject = new GameObject("ImportingSpreadsheet");
				gameObject.hideFlags |= HideFlags.HideAndDontSave;
				coroutineManager_0 = gameObject.AddComponent<CoroutineManager>();
				coroutineManager_0.StartCoroutine(Import_Google_Coroutine());
			}
		}

		private IEnumerator Import_Google_Coroutine()
		{
			WWW wWW = Import_Google_CreateWWWcall();
			if (wWW != null)
			{
				while (!wWW.isDone)
				{
					yield return null;
				}
				if (string.IsNullOrEmpty(wWW.error) && wWW.text != "\"\"")
				{
					PlayerPrefs.SetString("I2Source_" + Google_SpreadsheetKey, wWW.text);
					PlayerPrefs.Save();
					Import_Google_Result(wWW.text, eSpreadsheetUpdateMode.Replace);
					LocalizationManager.LocalizeAll();
					Debug.Log("Done Google Sync '" + wWW.text + "'");
				}
				else
				{
					Debug.Log("Language Source was up-to-date with Google Spreadsheet");
				}
				UnityEngine.Object.Destroy(coroutineManager_0.gameObject);
				coroutineManager_0 = null;
			}
		}

		public WWW Import_Google_CreateWWWcall(bool bool_0 = false)
		{
			if (!HasGoogleSpreadsheet())
			{
				return null;
			}
			string url = string.Format("{0}?key={1}&action=GetLanguageSource&version={2}", Google_WebServiceURL, Google_SpreadsheetKey, (!bool_0) ? Google_LastUpdatedVersion : "0");
			return new WWW(url);
		}

		public bool HasGoogleSpreadsheet()
		{
			return !string.IsNullOrEmpty(Google_WebServiceURL) && !string.IsNullOrEmpty(Google_SpreadsheetKey);
		}

		public void Import_Google_Result(string string_1, eSpreadsheetUpdateMode eSpreadsheetUpdateMode_0)
		{
			if (string_1 == "\"\"")
			{
				Debug.Log("Language Source was up to date");
				return;
			}
			JSONClass jSONClass_ = JSON.Parse(string_1).JSONClass_0;
			if (eSpreadsheetUpdateMode_0 == eSpreadsheetUpdateMode.Replace)
			{
				ClearAllData();
			}
			Google_LastUpdatedVersion = jSONClass_["version"];
			JSONClass jSONClass_2 = jSONClass_["spreadsheet"].JSONClass_0;
			foreach (KeyValuePair<string, JSONNode> item in jSONClass_2)
			{
				Import_CSV(item.Key, item.Value, eSpreadsheetUpdateMode_0);
				if (eSpreadsheetUpdateMode_0 == eSpreadsheetUpdateMode.Replace)
				{
					eSpreadsheetUpdateMode_0 = eSpreadsheetUpdateMode.Merge;
				}
			}
		}

		public List<string> GetCategories(bool bool_0 = false)
		{
			List<string> list = new List<string>();
			foreach (TermData mTerm in mTerms)
			{
				string categoryFromFullTerm = GetCategoryFromFullTerm(mTerm.Term, bool_0);
				if (!list.Contains(categoryFromFullTerm))
				{
					list.Add(categoryFromFullTerm);
				}
			}
			list.Sort();
			return list;
		}

		internal static string GetKeyFromFullTerm(string string_1, bool bool_0 = false)
		{
			int num = ((!bool_0) ? string_1.LastIndexOfAny(char_0) : string_1.IndexOfAny(char_0));
			return (num >= 0) ? string_1.Substring(num + 1) : string_1;
		}

		internal static string GetCategoryFromFullTerm(string string_1, bool bool_0 = false)
		{
			int num = ((!bool_0) ? string_1.LastIndexOfAny(char_0) : string_1.IndexOfAny(char_0));
			return (num >= 0) ? string_1.Substring(0, num) : string_0;
		}

		internal static void DeserializeFullTerm(string string_1, out string string_2, out string string_3, bool bool_0 = false)
		{
			int num = ((!bool_0) ? string_1.LastIndexOfAny(char_0) : string_1.IndexOfAny(char_0));
			if (num < 0)
			{
				string_3 = string_0;
				string_2 = string_1;
			}
			else
			{
				string_3 = string_1.Substring(0, num);
				string_2 = string_1.Substring(num + 1);
			}
		}

		private void Awake()
		{
			if (NeverDestroy)
			{
				if (ManagerHasASimilarSource())
				{
					UnityEngine.Object.Destroy(this);
					return;
				}
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			LocalizationManager.AddSource(this);
			UpdateDictionary();
		}

		public void UpdateDictionary()
		{
			mDictionary.Clear();
			int i = 0;
			for (int count = mTerms.Count; i < count; i++)
			{
				ValidateFullTerm(ref mTerms[i].Term);
				mDictionary[mTerms[i].Term] = mTerms[i];
			}
		}

		public string GetSourceName()
		{
			string text = base.gameObject.name;
			Transform parent = base.transform.parent;
			while ((bool)parent)
			{
				text = parent.name + "_" + text;
				parent = parent.parent;
			}
			return text;
		}

		public int GetLanguageIndex(string string_1, bool bool_0 = true)
		{
			int num = 0;
			int count = mLanguages.Count;
			while (true)
			{
				if (num < count)
				{
					if (string.Compare(mLanguages[num].Name, string_1, StringComparison.OrdinalIgnoreCase) == 0)
					{
						break;
					}
					num++;
					continue;
				}
				if (bool_0)
				{
					int i = 0;
					for (int count2 = mLanguages.Count; i < count2; i++)
					{
						if (AreTheSameLanguage(mLanguages[i].Name, string_1))
						{
							return i;
						}
					}
				}
				return -1;
			}
			return num;
		}

		public static bool AreTheSameLanguage(string string_1, string string_2)
		{
			string_1 = GetLanguageWithoutRegion(string_1);
			string_2 = GetLanguageWithoutRegion(string_2);
			return string.Compare(string_1, string_2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static string GetLanguageWithoutRegion(string string_1)
		{
			int num = string_1.IndexOfAny("(/\\[,{".ToCharArray());
			if (num < 0)
			{
				return string_1;
			}
			return string_1.Substring(0, num).Trim();
		}

		public void AddLanguage(string string_1, string string_2)
		{
			if (GetLanguageIndex(string_1) < 0)
			{
				LanguageData languageData = new LanguageData();
				languageData.Name = string_1;
				languageData.Code = string_2;
				mLanguages.Add(languageData);
				int count = mLanguages.Count;
				int i = 0;
				for (int count2 = mTerms.Count; i < count2; i++)
				{
					Array.Resize(ref mTerms[i].Languages, count);
				}
			}
		}

		public void RemoveLanguage(string string_1)
		{
			int languageIndex = GetLanguageIndex(string_1);
			if (languageIndex < 0)
			{
				return;
			}
			int count = mLanguages.Count;
			int i = 0;
			for (int count2 = mTerms.Count; i < count2; i++)
			{
				for (int j = languageIndex + 1; j < count; j++)
				{
					mTerms[i].Languages[j - 1] = mTerms[i].Languages[j];
				}
				Array.Resize(ref mTerms[i].Languages, count - 1);
			}
			mLanguages.RemoveAt(languageIndex);
		}

		public List<string> GetLanguages()
		{
			List<string> list = new List<string>();
			int i = 0;
			for (int count = mLanguages.Count; i < count; i++)
			{
				list.Add(mLanguages[i].Name);
			}
			return list;
		}

		public string GetTermTranslation(string string_1)
		{
			int languageIndex = GetLanguageIndex(LocalizationManager.String_0);
			if (languageIndex < 0)
			{
				return string.Empty;
			}
			TermData termData = GetTermData(string_1);
			if (termData != null)
			{
				return termData.Languages[languageIndex];
			}
			return string.Empty;
		}

		public TermData AddTerm(string string_1)
		{
			return AddTerm(string_1, eTermType.Text);
		}

		public TermData GetTermData(string string_1)
		{
			if (mDictionary.Count == 0)
			{
				UpdateDictionary();
			}
			TermData value;
			mDictionary.TryGetValue(string_1, out value);
			return value;
		}

		public bool ContainsTerm(string string_1)
		{
			return GetTermData(string_1) != null;
		}

		public List<string> GetTermsList()
		{
			return new List<string>(mDictionary.Keys);
		}

		public TermData AddTerm(string string_1, eTermType eTermType_0)
		{
			ValidateFullTerm(ref string_1);
			string_1 = string_1.Trim();
			TermData termData = GetTermData(string_1);
			if (termData == null)
			{
				termData = new TermData();
				termData.Term = string_1;
				termData.TermType = eTermType_0;
				termData.Languages = new string[mLanguages.Count];
				mTerms.Add(termData);
				mDictionary.Add(string_1, termData);
			}
			return termData;
		}

		public void RemoveTerm(string string_1)
		{
			int num = 0;
			int count = mTerms.Count;
			while (true)
			{
				if (num < count)
				{
					if (mTerms[num].Term == string_1)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			mTerms.RemoveAt(num);
			mDictionary.Remove(string_1);
		}

		public static void ValidateFullTerm(ref string string_1)
		{
			string_1 = string_1.Replace('\\', '/');
			string_1 = string_1.Trim();
			if (string_1.StartsWith(string_0) && string_1.Length > string_0.Length && string_1[string_0.Length] == '/')
			{
				string_1 = string_1.Substring(string_0.Length + 1);
			}
		}

		public bool IsEqualTo(LanguageSource languageSource_0)
		{
			if (languageSource_0.mLanguages.Count != mLanguages.Count)
			{
				return false;
			}
			int num = 0;
			int count = mLanguages.Count;
			while (true)
			{
				if (num < count)
				{
					if (languageSource_0.GetLanguageIndex(mLanguages[num].Name) < 0)
					{
						break;
					}
					num++;
					continue;
				}
				return true;
			}
			return false;
		}

		internal bool ManagerHasASimilarSource()
		{
			int num = 0;
			int count = LocalizationManager.list_0.Count;
			while (true)
			{
				if (num < count)
				{
					LanguageSource languageSource = LocalizationManager.list_0[num];
					if (languageSource != null && languageSource.IsEqualTo(this) && languageSource != this)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		public void ClearAllData()
		{
			mTerms.Clear();
			mLanguages.Clear();
			mDictionary.Clear();
		}

		public UnityEngine.Object FindAsset(string string_1)
		{
			if (Assets != null)
			{
				int i = 0;
				for (int num = Assets.Length; i < num; i++)
				{
					if (Assets[i] != null && Assets[i].name == string_1)
					{
						return Assets[i];
					}
				}
			}
			return null;
		}

		public bool HasAsset(UnityEngine.Object object_0)
		{
			return Array.IndexOf(Assets, object_0) >= 0;
		}
	}
}
