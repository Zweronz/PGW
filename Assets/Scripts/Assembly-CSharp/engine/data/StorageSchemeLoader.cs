using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using engine.controllers;
using engine.filesystem;
using engine.helpers;
using engine.launcher;
using engine.operations;

namespace engine.data
{
	internal sealed class StorageSchemeLoader
	{
		private string string_0;

		private string string_1;

		private string string_2;

		private ProtobufAssetFile<DataScheme> protobufAssetFile_0;

		private DataScheme dataScheme_0;

		private Action action_0;

		private Dictionary<Type, BaseAssetFile> dictionary_0 = new Dictionary<Type, BaseAssetFile>();

		public DownloaderFileTriggerResourcesUrl downloaderFileTriggerResourcesUrl_0 = new DownloaderFileTriggerResourcesUrl();

		public void Init(string string_3, string string_4, string string_5, Action action_1)
		{
			string_0 = string_3;
			string_1 = string_4;
			string_2 = string_5;
			action_0 = action_1;
			InitUrls();
			OperationsManager.OperationsManager_0.Add(LoadScheme);
		}

		private void InitUrls()
		{
			downloaderFileTriggerResourcesUrl_0.AddUrl(AppController.AppController_0.serverInfo_0.String_1);
			downloaderFileTriggerResourcesUrl_0.AddUrl(AppController.AppController_0.serverInfo_0.String_0);
		}

		private void LoadScheme()
		{
			SetHost(true);
			string text = string.Format("{0}/{1}/{2}", string_0, string_2, string_1);
			string text2 = string.Format("{0}/{1}", string_0, string_1);
			Log.AddLine("LoadScheme() : deployPath = " + text);
			protobufAssetFile_0 = (ProtobufAssetFile<DataScheme>)ResourcesManager.ResourcesManager_0.Load<ProtobufAssetFile<DataScheme>>(text2, Convert.ToUInt32(Utility.Double_0), text);
			OperationsManager.OperationsManager_0.Add(SchemeIsValid);
		}

		private void SchemeIsValid()
		{
			if (protobufAssetFile_0 != null && protobufAssetFile_0.IsLoaded)
			{
				dataScheme_0 = protobufAssetFile_0.Data;
				OperationsManager.OperationsManager_0.Add(LoadStoragesData);
				return;
			}
			downloaderFileTriggerResourcesUrl_0.Int32_0++;
			if (downloaderFileTriggerResourcesUrl_0.Int32_0 < 3)
			{
				OperationsManager.OperationsManager_0.Add(LoadScheme);
			}
			else
			{
				InitStorageError("Error init sceme data storages!");
			}
		}

		private void LoadStoragesData()
		{
			Type typeFromHandle = typeof(StorageData<, >);
			Type typeFromHandle2 = typeof(ProtobufAssetFile<>);
			MethodInfo method = typeof(ResourcesManager).GetMethod("Load", new Type[1] { typeof(FileName) });
			foreach (DataSchemeFileInfo item in dataScheme_0.List_0)
			{
				string text = item.string_0;
				if (string.IsNullOrEmpty(text))
				{
					Log.AddLine(string.Format("StorageSchemeLoader::LoadStoragesData. Type name not valid", text));
					continue;
				}
				Type type = Type.GetType(text);
				if (type == null)
				{
					Log.AddLine(string.Format("StorageSchemeLoader::LoadStoragesData. Error get type definition from string {0}", text));
					continue;
				}
				object[] customAttributes = type.GetCustomAttributes(typeof(StorageDataKeyAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					Type type_ = ((StorageDataKeyAttribute)customAttributes[0]).Type_0;
					Type[] typeArguments = new Type[2] { type_, type };
					Type type2 = typeFromHandle.MakeGenericType(typeArguments);
					Type type3 = typeFromHandle2.MakeGenericType(type2);
					string string_ = string.Format("{0}/{1}/{2}", string_0, this.string_2, text);
					string string_2 = string.Format("{0}/{1}", string_0, text);
					BaseAssetFile value = (BaseAssetFile)method.MakeGenericMethod(type3).Invoke(ResourcesManager.ResourcesManager_0, new object[1]
					{
						new FileName(string_2, item.UInt32_0, string_)
					});
					dictionary_0.Add(type, value);
				}
				else
				{
					Console.WriteLine(string.Format("StorageSchemeLoader::LoadStoragesData. Type {0} need StorageDataKeyAttribute sets key for storage", text));
				}
			}
			OperationsManager.OperationsManager_0.Add(DesererializeStorages);
		}

		private void DesererializeStorages()
		{
			StorageManager storageManager_ = StorageManager.StorageManager_0;
			foreach (Type key in dictionary_0.Keys)
			{
				BaseAssetFile baseAssetFile = dictionary_0[key];
				if (baseAssetFile != null && baseAssetFile.IsLoaded)
				{
					PropertyInfo property = baseAssetFile.GetType().GetProperty("Data");
					storageManager_.dictionary_1.Add(key, property.GetValue(baseAssetFile, null));
					continue;
				}
				InitStorageError(string.Format("[StorageManager. DesererializeStorages()] Storage load error! Storage = {0}", key.ToString()));
				return;
			}
			dictionary_0.Clear();
			OperationsManager.OperationsManager_0.Add(InitStorageDataIndexes);
		}

		private void InitStorageDataIndexes()
		{
			if (action_0 != null)
			{
				action_0();
				IDictionaryEnumerator dictionaryEnumerator = StorageManager.StorageManager_0.dictionary_1.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					Type[] genericArguments = dictionaryEnumerator.Value.GetType().GetGenericArguments();
					MethodInfo method = typeof(StorageData<, >).MakeGenericType(genericArguments[0], genericArguments[1]).GetMethod("InitIndexes", BindingFlags.Instance | BindingFlags.NonPublic);
					method.Invoke(dictionaryEnumerator.Value, new object[0]);
				}
				OperationsManager.OperationsManager_0.Add(CheckDeployHash);
			}
		}

		private void CheckDeployHash()
		{
			Log.AddLine("[StorageManager::CheckDeployHash. Start check deploy hash]");
			foreach (DataSchemeFileInfo item in dataScheme_0.List_0)
			{
				string text = item.string_0;
				if (!string.IsNullOrEmpty(text))
				{
					string string_ = string.Format("{0}/{1}", string_0, text);
					FileName fileName_ = new FileName(string_, item.UInt32_0, string.Empty);
					CheatsChecker.CheatsChecker_0.CheckHashFileDeploy(fileName_);
				}
			}
			OperationsManager.OperationsManager_0.Add(InitStorageDone);
		}

		private void InitStorageDone()
		{
			SetHost(false);
			Log.AddLine("StorageLoaderQueue complete!");
			StorageManager.StorageManager_0.Dispatch(StorageManager.StatusEvent.LOADING_COMPLETE);
		}

		private void InitStorageError(string string_3)
		{
			SetHost(false);
			Log.AddLine(string_3, Log.LogLevel.FATAL);
			StorageManager.StorageManager_0.Dispatch(StorageManager.StatusEvent.LOADING_ERROR);
		}

		private void SetHost(bool bool_0)
		{
			if (bool_0)
			{
				string arg = downloaderFileTriggerResourcesUrl_0.String_0;
				Log.AddLine(string.Format("Set url for load storage data: {0}", arg));
				LoadNetworkFileBestHttpOperation.string_1 = arg;
			}
			else
			{
				downloaderFileTriggerResourcesUrl_0.Reset();
				LoadNetworkFileBestHttpOperation.string_1 = string.Empty;
			}
		}
	}
}
