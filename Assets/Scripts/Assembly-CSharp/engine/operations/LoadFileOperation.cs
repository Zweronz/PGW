using System.Runtime.CompilerServices;
using engine.filesystem;

namespace engine.operations
{
	public abstract class LoadFileOperation : TheradOperation
	{
		[CompilerGenerated]
		private BaseAssetFile baseAssetFile_0;

		public BaseAssetFile BaseAssetFile_0
		{
			[CompilerGenerated]
			get
			{
				return baseAssetFile_0;
			}
			[CompilerGenerated]
			set
			{
				baseAssetFile_0 = value;
			}
		}
	}
}
