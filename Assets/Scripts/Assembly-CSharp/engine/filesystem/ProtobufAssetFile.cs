using System.IO;
using System.Reflection;
using ProtoBuf;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public class ProtobufAssetFile<T> : BaseAssetFile where T : class
	{
		public T Data { get; private set; }

		public ProtobufAssetFile(FileName name)
			: base(name)
		{
		}

		public ProtobufAssetFile()
		{
		}

		public override void Destroy()
		{
			Data = (T)null;
			base.Destroy();
		}

		protected override void Convert(byte[] data)
		{
			Data = Serializer.Deserialize<T>(new MemoryStream(data));
		}
	}
}
