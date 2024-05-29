using System.IO;

namespace engine.launcher
{
	public abstract class IPlatformSettings
	{
		public abstract string String_0 { get; }

		public abstract string String_1 { get; }

		public abstract string String_2 { get; }

		public abstract string String_3 { get; }

		public abstract string String_4 { get; }

		public void CleanLocalAppInfo()
		{
			string path = Path.Combine(String_3, String_2);
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}
}
