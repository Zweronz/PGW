using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

namespace engine.filesystem
{
	public class FileCache
	{
		public static readonly string string_0 = "shared/";

		public static readonly string string_1 = "cache_files";

		public static readonly string string_2 = "precache_files.bytes";

		private static FileCache fileCache_0;

		private Dictionary<FileName, BaseAssetFile> dictionary_0 = new Dictionary<FileName, BaseAssetFile>();

		private CacheResources cacheResources_0;

		private CacheResources cacheResources_1;

		private string string_3;

		public static FileCache FileCache_0
		{
			get
			{
				if (fileCache_0 == null)
				{
					fileCache_0 = new FileCache();
				}
				return fileCache_0;
			}
		}

		public string String_0
		{
			get
			{
				if (string_3 == null)
				{
					string_3 = GetCacheFolder(false);
				}
				return string_3;
			}
		}

		private FileCache()
		{
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(ClearCacheMemory);
			byte[] byte_ = null;
			TextAsset textAsset = Resources.Load<TextAsset>(GetCacheFileName(true));
			if (textAsset != null)
			{
				byte_ = textAsset.bytes;
			}
			cacheResources_0 = new CacheResources(byte_, true);
			byte_ = null;
			string cacheFileName = GetCacheFileName(false);
			if (File.Exists(cacheFileName))
			{
				byte_ = Utility.ReadAllBytes(cacheFileName);
			}
			cacheResources_1 = new CacheResources(byte_, false);
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(RebuildActiveCache);
		}

		public static string GetCacheFileName(bool bool_0)
		{
			return GetCacheFolder(bool_0) + string_1;
		}

		public static string GetCacheFolder(bool bool_0)
		{
			if (bool_0)
			{
				return string_0;
			}
			return Path.Combine(Application.persistentDataPath, string_0);
		}

		public bool IsFileInCache(FileName fileName_0, out bool bool_0)
		{
			if (dictionary_0.ContainsKey(fileName_0))
			{
				bool_0 = false;
				return true;
			}
			if (IsExistFileInCache(cacheResources_0, fileName_0))
			{
				bool_0 = true;
				return true;
			}
			if (IsExistFileInCache(cacheResources_1, fileName_0))
			{
				bool_0 = false;
				return true;
			}
			bool_0 = false;
			return false;
		}

		public BaseAssetFile LoadSync<T>(FileName fileName_0, bool bool_0) where T : BaseAssetFile, new()
		{
			return LoadSync(BaseAssetFile.Create<T>(fileName_0), bool_0);
		}

		public BaseAssetFile LoadSync(BaseAssetFile baseAssetFile_0, bool bool_0)
		{
			BaseAssetFile value = null;
			if (dictionary_0.TryGetValue(baseAssetFile_0.Name, out value))
			{
				return value;
			}
			value = baseAssetFile_0;
			if (bool_0)
			{
				string path = baseAssetFile_0.Name.String_2.Replace(".bytes", string.Empty);
				string path2 = Path.Combine(GetCacheFolder(true), path);
				TextAsset textAsset = Resources.Load<TextAsset>(path2);
				if (textAsset != null)
				{
					value.Bytes = BaseAssetFile.ToBytes(new MemoryStream(textAsset.bytes));
				}
			}
			else
			{
				value.Bytes = Utility.ReadAllBytes(String_0 + baseAssetFile_0.Name.String_2);
			}
			value.IsLoaded = value.Bytes != null;
			return value;
		}

		public void Save(BaseAssetFile baseAssetFile_0)
		{
			if (baseAssetFile_0.Name.UInt32_0 != 0)
			{
				Directory.CreateDirectory(String_0 + baseAssetFile_0.Name.String_5);
				Utility.WriteAllBytes(String_0 + baseAssetFile_0.Name.String_2, baseAssetFile_0.Bytes);
				dictionary_0[baseAssetFile_0.Name] = baseAssetFile_0;
				cacheResources_1.Add(baseAssetFile_0.Name);
			}
		}

		public void RemoveFileFromActiveCache(FileName fileName_0)
		{
			if (!IsExistFileInCache(cacheResources_1, fileName_0))
			{
				Log.AddLine("[FileCache::RemoveFileFromActiveCache. File not found in active cache, file]: " + fileName_0.ToString());
				return;
			}
			string path = Path.Combine(FileCache_0.String_0, fileName_0.String_2);
			File.Delete(path);
			RebuildActiveCache();
		}

		public void ClearCacheMemory()
		{
			try
			{
				IDictionaryEnumerator dictionaryEnumerator = dictionary_0.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					((BaseAssetFile)dictionaryEnumerator.Value).Destroy();
				}
				dictionary_0.Clear();
			}
			catch (Exception exception_)
			{
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
				Log.AddLine("Error clear cache memory!");
			}
		}

		private void RebuildActiveCache()
		{
			string text = String_0 + string_1;
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			cacheResources_1.Clear();
			string[] allFiles = Utility.GetAllFiles(string_3, new string[3] { ".meta", string_2, string_1 }, null);
			for (int i = 0; i < allFiles.Length; i++)
			{
				string text2 = allFiles[i].Replace(string_3, string.Empty);
				FileName fileName_;
				if (FileName.CreateFileNameFromUrl(text2, out fileName_))
				{
					cacheResources_1.Add(fileName_);
				}
				else
				{
					Log.AddLine("File cache: cannot parse file: " + text2);
				}
			}
			if (!Directory.Exists(string_3))
			{
				Directory.CreateDirectory(string_3);
			}
			Utility.WriteAllBytes(text, cacheResources_1.Byte_0);
			Log.AddLine("Save active file cache to " + text);
		}

		private bool IsExistFileInCache(CacheResources cacheResources_2, FileName fileName_0)
		{
			if (!cacheResources_2.Contains(fileName_0.String_1))
			{
				return false;
			}
			if (cacheResources_2.Contains(fileName_0))
			{
				return true;
			}
			if (!cacheResources_2.Boolean_0)
			{
				string path = String_0 + fileName_0.String_3 + cacheResources_2[fileName_0.String_1].ToString();
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			return false;
		}
	}
}
