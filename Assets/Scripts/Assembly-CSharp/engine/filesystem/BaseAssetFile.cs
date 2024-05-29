using System;
using System.IO;
using System.Reflection;
using engine.helpers;
using engine.operations;

namespace engine.filesystem
{
	[Obfuscation(Exclude = true)]
	public abstract class BaseAssetFile
	{
		private static readonly int BUFFER_SIZE = 2048;

		private static byte[] _buffer = new byte[BUFFER_SIZE];

		public CreateBundleFromFileOperation CreateBundleOperation { get; set; }

		public LoadFileOperation LoadFileOperation { get; set; }

		public byte[] Bytes { get; set; }

		public bool IsLoaded { get; set; }

		public FileName Name { get; set; }

		public BaseAssetFile(FileName name)
		{
			Name = name;
		}

		public BaseAssetFile()
		{
			Name = default(FileName);
		}

		public static T Create<T>(FileName name) where T : BaseAssetFile, new()
		{
			return Activator.CreateInstance(typeof(T), name) as T;
		}

		public static T Create<T>() where T : BaseAssetFile, new()
		{
			return Activator.CreateInstance(typeof(T)) as T;
		}

		public static byte[] ToBytes(Stream stream)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					int count;
					while ((count = binaryReader.Read(_buffer, 0, BUFFER_SIZE)) > 0)
					{
						memoryStream.Write(_buffer, 0, count);
					}
				}
				return memoryStream.ToArray();
			}
		}

		public void Convert()
		{
			if (Bytes == null)
			{
				Log.AddLine("Not found data in asset file");
			}
			else
			{
				Convert(Bytes);
			}
		}

		public virtual void Destroy()
		{
			Bytes = null;
			IsLoaded = false;
			ResourcesManager.ResourcesManager_0.Unregister(Name);
		}

		public override string ToString()
		{
			return string.Format("[AssetFile is " + ((!IsLoaded) ? "not " : string.Empty) + "loaded]");
		}

		protected abstract void Convert(byte[] data);
	}
}
