using System.Reflection;
using System.Text;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public class StringAssetFile : BaseAssetFile
	{
		public string StringData { get; private set; }

		public StringAssetFile(FileName name)
			: base(name)
		{
		}

		public StringAssetFile()
		{
		}

		public override void Destroy()
		{
			StringData = null;
			base.Destroy();
		}

		protected override void Convert(byte[] data)
		{
			StringData = Encoding.UTF8.GetString(data, 0, data.Length);
		}
	}
}
