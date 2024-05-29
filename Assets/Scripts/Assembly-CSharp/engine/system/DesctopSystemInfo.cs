using System.Net.NetworkInformation;
using UnityEngine;

namespace engine.system
{
	public sealed class DesctopSystemInfo : ISystemInfo
	{
		private string string_1 = "Not available";

		public override string String_3
		{
			get
			{
				return String_16;
			}
		}

		public override string String_6
		{
			get
			{
				return string.Empty;
			}
		}

		public override string String_7
		{
			get
			{
				return string.Empty;
			}
		}

		public override string String_8
		{
			get
			{
				return string.Format("{0}:{1}", SystemInfo.deviceModel, SystemInfo.deviceName);
			}
		}

		public override string String_9
		{
			get
			{
				return SystemInfo.operatingSystem;
			}
		}

		public override string String_11
		{
			get
			{
				return string.Empty;
			}
		}

		public override string String_16
		{
			get
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				foreach (NetworkInterface networkInterface in allNetworkInterfaces)
				{
					if (networkInterface.OperationalStatus == OperationalStatus.Up)
					{
						string_1 = networkInterface.GetPhysicalAddress().ToString();
						break;
					}
				}
				return string_1;
			}
		}

		public override string String_17
		{
			get
			{
				return "Desctop";
			}
		}
	}
}
