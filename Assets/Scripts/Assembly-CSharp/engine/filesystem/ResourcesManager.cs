using System;
using System.Collections.Generic;
using engine.events;
using engine.operations;

namespace engine.filesystem
{
	public class ResourcesManager
	{
		private static ResourcesManager resourcesManager_0;

		private Dictionary<FileName, BaseAssetFile> dictionary_0 = new Dictionary<FileName, BaseAssetFile>();

		public static ResourcesManager ResourcesManager_0
		{
			get
			{
				if (resourcesManager_0 == null)
				{
					resourcesManager_0 = new ResourcesManager();
				}
				return resourcesManager_0;
			}
		}

		private ResourcesManager()
		{
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(ClearCacheMemory);
		}

		public BaseAssetFile Load<T>(FileName fileName_0) where T : BaseAssetFile, new()
		{
			BaseAssetFile value = null;
			if (dictionary_0.TryGetValue(fileName_0, out value))
			{
				return value;
			}
			bool bool_;
			if (FileCache.FileCache_0.IsFileInCache(fileName_0, out bool_))
			{
				if (bool_)
				{
					value = FileCache.FileCache_0.LoadSync<T>(fileName_0, bool_);
					value.Convert();
					if (value.IsLoaded)
					{
						dictionary_0.Add(fileName_0, value);
					}
					return value;
				}
				value = BaseAssetFile.Create<T>(fileName_0);
				LoadLocalFileOperation operation_ = new LoadLocalFileOperation(FileCache.FileCache_0.String_0, value);
				OperationsManager.OperationsManager_0.Add(operation_);
				if (value is AssetBundleFile)
				{
					value.CreateBundleOperation = new CreateBundleFromFileOperation((AssetBundleFile)value);
					OperationsManager.OperationsManager_0.Add(value.CreateBundleOperation);
				}
				else
				{
					OperationsManager.OperationsManager_0.Add(value.Convert);
				}
				dictionary_0.Add(fileName_0, value);
				return value;
			}
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = LoadNetworkFileBestHttpOperation.Create<T>(string.Format("{0}{1}{2}", fileName_0.String_4, "?ts=", fileName_0.String_0));
			loadNetworkFileBestHttpOperation.Subscribe(SaveFile, Operation.StatusEvent.Complete);
			value = loadNetworkFileBestHttpOperation.BaseAssetFile_0;
			value.Name = fileName_0;
			dictionary_0.Add(fileName_0, value);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
			return value;
		}

		public BaseAssetFile Load<T>(string string_0) where T : BaseAssetFile, new()
		{
			FileName fileName_;
			if (FileName.CreateFileNameFromUrl(string_0, out fileName_))
			{
				return Load<T>(fileName_);
			}
			fileName_ = new FileName(string_0, 0u, string.Empty);
			if (dictionary_0.ContainsKey(fileName_))
			{
				return dictionary_0[fileName_];
			}
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = LoadNetworkFileBestHttpOperation.Create<T>(string_0);
			BaseAssetFile baseAssetFile_ = loadNetworkFileBestHttpOperation.BaseAssetFile_0;
			dictionary_0.Add(fileName_, baseAssetFile_);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
			return baseAssetFile_;
		}

		public BaseAssetFile Load<T>(string string_0, uint uint_0) where T : BaseAssetFile, new()
		{
			return Load<T>(new FileName(string_0, uint_0, string.Empty));
		}

		public BaseAssetFile Load<T>(string string_0, uint uint_0, string string_1) where T : BaseAssetFile, new()
		{
			return Load<T>(new FileName(string_0, uint_0, string_1));
		}

		public void Unload(string string_0)
		{
			FileName fileName_;
			if (FileName.CreateFileNameFromUrl(string_0, out fileName_))
			{
				Unload(fileName_);
			}
			else
			{
				Unload(new FileName(string_0, 0u, string.Empty));
			}
		}

		public void Unload(FileName fileName_0)
		{
			if (!dictionary_0.ContainsKey(fileName_0))
			{
				throw new NullReferenceException("Unable to Unload: file " + fileName_0.String_2 + " is missing in ResourcesManager!");
			}
			BaseAssetFile baseAssetFile_ = dictionary_0[fileName_0];
			dictionary_0.Remove(fileName_0);
			Unload(baseAssetFile_);
		}

		public void Unload(BaseAssetFile baseAssetFile_0)
		{
			baseAssetFile_0.Destroy();
		}

		public void Unregister(FileName fileName_0)
		{
			if (dictionary_0.ContainsKey(fileName_0))
			{
				dictionary_0.Remove(fileName_0);
			}
		}

		public BaseAssetFile ThreadLoad<T>(FileName fileName_0) where T : BaseAssetFile, new()
		{
			BaseAssetFile value;
			if (dictionary_0.TryGetValue(fileName_0, out value))
			{
				return value;
			}
			value = BaseAssetFile.Create<T>(fileName_0);
			LoadFileInTreadOperation loadFileInTreadOperation = new LoadFileInTreadOperation(value);
			loadFileInTreadOperation.Subscribe(OnFileLoaded, Operation.StatusEvent.Complete);
			OperationsManager.OperationsManager_0.AddToNewThread(loadFileInTreadOperation);
			return value;
		}

		public BaseAssetFile GetAsset(string string_0)
		{
			FileName fileName_;
			if (FileName.CreateFileNameFromUrl(string_0, out fileName_))
			{
				return GetAsset(fileName_);
			}
			return GetAsset(new FileName(string_0, 0u, string.Empty));
		}

		public BaseAssetFile GetAsset(FileName fileName_0)
		{
			BaseAssetFile value;
			if (dictionary_0.TryGetValue(fileName_0, out value))
			{
				return value;
			}
			return null;
		}

		public void ClearCacheMemory()
		{
			dictionary_0.Clear();
		}

		public void RemoveFileFromActiveCache(FileName fileName_0)
		{
			FileCache.FileCache_0.RemoveFileFromActiveCache(fileName_0);
		}

		private void OnFileLoaded(Operation operation_0)
		{
			BaseAssetFile baseAssetFile_ = (operation_0 as LoadFileOperation).BaseAssetFile_0;
			dictionary_0.Add(baseAssetFile_.Name, baseAssetFile_);
		}

		private void SaveFile(object object_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = (LoadNetworkFileBestHttpOperation)object_0;
			if (!loadNetworkFileBestHttpOperation.Boolean_1)
			{
				FileCache.FileCache_0.Save(loadNetworkFileBestHttpOperation.BaseAssetFile_0);
			}
		}
	}
}
