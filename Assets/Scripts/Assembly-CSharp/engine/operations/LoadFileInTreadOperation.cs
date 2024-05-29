using engine.filesystem;

namespace engine.operations
{
	public class LoadFileInTreadOperation : LoadFileOperation
	{
		public LoadFileInTreadOperation(BaseAssetFile baseAssetFile_1)
		{
			base.BaseAssetFile_0 = baseAssetFile_1;
			base.BaseAssetFile_0.LoadFileOperation = this;
		}

		protected override void Execute()
		{
			bool flag = false;
			if (FileCache.FileCache_0.IsFileInCache(base.BaseAssetFile_0.Name, out flag))
			{
				if (flag)
				{
					LoadFileFromFileCacheOperation loadFileFromFileCacheOperation = new LoadFileFromFileCacheOperation(base.BaseAssetFile_0, flag);
					loadFileFromFileCacheOperation.Subscribe(OnFromFileCacheComplete, StatusEvent.Complete);
					OperationsManager.OperationsManager_0.AddToNewThread(loadFileFromFileCacheOperation);
				}
				else
				{
					LoadLocalFileOperation loadLocalFileOperation = new LoadLocalFileOperation(FileCache.FileCache_0.String_0, base.BaseAssetFile_0);
					loadLocalFileOperation.Subscribe(OnFromLocalComplete, StatusEvent.Complete);
					OperationsManager.OperationsManager_0.AddToNewThread(loadLocalFileOperation);
				}
			}
			else
			{
				LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(base.BaseAssetFile_0.Name.String_1 + "?ts=" + base.BaseAssetFile_0.Name.UInt32_0, base.BaseAssetFile_0);
				loadNetworkFileBestHttpOperation.Subscribe(OnFromNetComplete, StatusEvent.Complete);
				OperationsManager.OperationsManager_0.AddToNewThread(loadNetworkFileBestHttpOperation);
			}
		}

		private void OnFromLocalComplete(Operation operation_0)
		{
			if (base.BaseAssetFile_0 is AssetBundleFile)
			{
				CreateBundleFromFileOperation createBundleFromFileOperation = new CreateBundleFromFileOperation((AssetBundleFile)base.BaseAssetFile_0);
				createBundleFromFileOperation.Subscribe(OnFromFileCacheComplete);
				OperationsManager.OperationsManager_0.AddToNewQueue(createBundleFromFileOperation);
			}
			else
			{
				base.BaseAssetFile_0.Convert();
				OnFromFileCacheComplete(null);
			}
		}

		private void OnFromNetComplete(Operation operation_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = (LoadNetworkFileBestHttpOperation)operation_0;
			if (!loadNetworkFileBestHttpOperation.Boolean_1)
			{
				FileCache.FileCache_0.Save(loadNetworkFileBestHttpOperation.BaseAssetFile_0);
			}
			OnFromFileCacheComplete(loadNetworkFileBestHttpOperation);
		}

		private void OnFromFileCacheComplete(Operation operation_0)
		{
			Complete();
		}
	}
}
