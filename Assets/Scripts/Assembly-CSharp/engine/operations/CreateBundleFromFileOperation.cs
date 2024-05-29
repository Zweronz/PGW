using UnityEngine;
using engine.events;
using engine.filesystem;
using engine.helpers;

namespace engine.operations
{
	public class CreateBundleFromFileOperation : Operation
	{
		private AssetBundleCreateRequest assetBundleCreateRequest_0;

		private AssetBundleFile assetBundleFile_0;

		public CreateBundleFromFileOperation(AssetBundleFile assetBundleFile_1)
		{
			assetBundleFile_0 = assetBundleFile_1;
		}

		protected override void Execute()
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(Destroy);
			assetBundleCreateRequest_0 = AssetBundle.LoadFromMemoryAsync(assetBundleFile_0.Bytes);
		}

		private void Update()
		{
			if (!assetBundleCreateRequest_0.isDone)
			{
				base.ProgressEvent_0.Dispatch(assetBundleCreateRequest_0.progress);
				return;
			}
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			try
			{
				assetBundleFile_0.AssetBundleData = assetBundleCreateRequest_0.assetBundle;
				assetBundleFile_0.BundleData = assetBundleCreateRequest_0.assetBundle.mainAsset;
			}
			catch
			{
				Log.AddLine(assetBundleFile_0.Name.String_1 + " is still in memory!");
			}
			Complete();
		}

		public void Destroy()
		{
			if (assetBundleCreateRequest_0 != null)
			{
				if (assetBundleCreateRequest_0.isDone && assetBundleCreateRequest_0.assetBundle != null)
				{
					assetBundleCreateRequest_0.assetBundle.Unload(true);
				}
				assetBundleCreateRequest_0 = null;
			}
		}
	}
}
