using System.Reflection;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public class ByteAssetFile : BaseAssetFile
	{
		public ByteAssetFile(FileName name)
			: base(name)
		{
		}

		public ByteAssetFile()
		{
		}

		protected override void Convert(byte[] data)
		{
		}
	}
}
