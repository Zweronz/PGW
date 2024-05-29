using Rilisoft.Phone.Info;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class MemoryLimitMonitor : MonoBehaviour
	{
		private int int_0;

		private long long_0;

		private long long_1;

		private string string_0 = string.Empty;

		internal void Update()
		{
			if (Application.platform == RuntimePlatform.WP8Player && Time.frameCount % 60 == 0)
			{
				long applicationPeakMemoryUsage = DeviceStatus.ApplicationPeakMemoryUsage;
				if (long_1 < applicationPeakMemoryUsage)
				{
					long_1 = applicationPeakMemoryUsage;
					int_0 = Time.frameCount;
				}
				long_0 = DeviceStatus.ApplicationCurrentMemoryUsage;
			}
		}

		private static int GetBitsPerPixel(TextureFormat textureFormat_0)
		{
			switch (textureFormat_0)
			{
			case TextureFormat.Alpha8:
				return 8;
			case TextureFormat.ARGB4444:
				return 16;
			case TextureFormat.RGB24:
				return 24;
			case TextureFormat.RGBA32:
				return 32;
			case TextureFormat.ARGB32:
				return 32;
			case TextureFormat.RGB565:
				return 16;
			case TextureFormat.DXT1:
				return 4;
			case TextureFormat.DXT5:
				return 8;
			case TextureFormat.PVRTC_RGB2:
				return 2;
			case TextureFormat.PVRTC_RGBA2:
				return 2;
			case TextureFormat.PVRTC_RGB4:
				return 4;
			case TextureFormat.PVRTC_RGBA4:
				return 4;
			case TextureFormat.ETC_RGB4:
				return 4;
			case TextureFormat.ATC_RGB4:
				return 4;
			case TextureFormat.ATC_RGBA8:
				return 8;
			default:
				return 0;
			case TextureFormat.BGRA32:
				return 32;
			}
		}
	}
}
