using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace engine.launcher
{
	public sealed class DownloaderFileTriggerResourcesUrl
	{
		private List<string> list_0 = new List<string>();

		[CompilerGenerated]
		private int int_0;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public string String_0
		{
			get
			{
				return list_0[Int32_0 % list_0.Count];
			}
		}

		public bool Boolean_0
		{
			get
			{
				if (list_0.Count > 0 && Int32_0 > 0 && Int32_0 % list_0.Count == 0)
				{
					return true;
				}
				return false;
			}
		}

		public void AddUrl(string string_0)
		{
			list_0.Add(string_0);
		}

		public void Reset()
		{
			Int32_0 = 0;
			list_0.Clear();
		}
	}
}
