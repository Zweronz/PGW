using engine.filesystem;

namespace engine.operations
{
	public class LoadFileFromFileCacheOperation : LoadFileOperation
	{
		private bool bool_6;

		public LoadFileFromFileCacheOperation(BaseAssetFile baseAssetFile_1, bool bool_7)
		{
			base.BaseAssetFile_0 = baseAssetFile_1;
			bool_6 = bool_7;
		}

		protected override void Execute()
		{
			BaseAssetFile baseAssetFile = FileCache.FileCache_0.LoadSync(base.BaseAssetFile_0, bool_6);
			baseAssetFile.Convert();
			baseAssetFile.LoadFileOperation = base.BaseAssetFile_0.LoadFileOperation;
			base.BaseAssetFile_0 = baseAssetFile;
			Complete();
		}
	}
}
