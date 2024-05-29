using System.Reflection;
using UnityEngine;
using engine.operations;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public class AssetBundleFile : BaseAssetFile
	{
		private CreateBundleFromFileOperation _bundleOperation;

		public Object BundleData { get; set; }

		public AssetBundle AssetBundleData { get; set; }

		public AssetBundleFile(FileName name)
			: base(name)
		{
		}

		public AssetBundleFile()
		{
		}

		public override void Destroy()
		{
			if (_bundleOperation != null)
			{
				_bundleOperation.Destroy();
			}
			if (AssetBundleData != null)
			{
				AssetBundleData.Unload(true);
				Object.DestroyImmediate(AssetBundleData);
				AssetBundleData = null;
			}
			BundleData = null;
			base.Destroy();
		}

		protected override void Convert(byte[] data)
		{
			_bundleOperation = new CreateBundleFromFileOperation(this);
			OperationsManager.OperationsManager_0.Add(_bundleOperation);
		}
	}
}
