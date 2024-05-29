using System.Reflection;
using UnityEngine;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public class TextureAssetFile : BaseAssetFile
	{
		public Texture2D Texture { get; set; }

		public TextureAssetFile(FileName name)
			: base(name)
		{
		}

		public TextureAssetFile()
		{
		}

		public override void Destroy()
		{
			Texture = null;
			base.Destroy();
		}

		protected override void Convert(byte[] data)
		{
			Texture = new Texture2D(1, 1);
			Texture.LoadImage(data);
		}
	}
}
