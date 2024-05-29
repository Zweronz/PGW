using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace engine.system
{
	public abstract class ISystemInfo
	{
		private Dictionary<DEVICE_TYPE, string> dictionary_0 = new Dictionary<DEVICE_TYPE, string>
		{
			{
				DEVICE_TYPE.PHONE,
				"Phone"
			},
			{
				DEVICE_TYPE.TABLET,
				"Tablet"
			},
			{
				DEVICE_TYPE.STANDALONE,
				"Standalone"
			}
		};

		private static ISystemInfo isystemInfo_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private DEVICE_TYPE device_TYPE_0;

		[CompilerGenerated]
		private bool bool_0;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		public DEVICE_TYPE DEVICE_TYPE_0
		{
			[CompilerGenerated]
			get
			{
				return device_TYPE_0;
			}
			[CompilerGenerated]
			private set
			{
				device_TYPE_0 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public static ISystemInfo ISystemInfo_0
		{
			get
			{
				if (isystemInfo_0 == null)
				{
					isystemInfo_0 = new DesctopSystemInfo();
					Debug.Log(isystemInfo_0.ToString());
				}
				return isystemInfo_0;
			}
		}

		public float Single_0
		{
			get
			{
				return Mathf.Ceil(Screen.dpi);
			}
		}

		public virtual object Object_0
		{
			get
			{
				return null;
			}
		}

		public virtual string String_3
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		public virtual string String_4
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual string String_5
		{
			get
			{
				return string.Empty;
			}
		}

		public abstract string String_6 { get; }

		public abstract string String_7 { get; }

		public abstract string String_8 { get; }

		public abstract string String_9 { get; }

		public virtual string String_10
		{
			get
			{
				switch (Application.systemLanguage)
				{
				default:
					return "en";
				case SystemLanguage.Spanish:
					return "es";
				case SystemLanguage.Russian:
					return "ru";
				case SystemLanguage.Italian:
					return "it";
				case SystemLanguage.German:
					return "de";
				case SystemLanguage.English:
					return "en";
				}
			}
		}

		public abstract string String_11 { get; }

		public virtual string String_12
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual string String_13
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual string String_14
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual string String_15
		{
			get
			{
				return string.Empty;
			}
		}

		public abstract string String_16 { get; }

		public abstract string String_17 { get; }

		public string String_1
		{
			get
			{
				string value = string.Empty;
				dictionary_0.TryGetValue(DEVICE_TYPE_0, out value);
				return value;
			}
		}

		public string String_2
		{
			get
			{
				if (DEVICE_TYPE_0 == DEVICE_TYPE.STANDALONE)
				{
					return TYPE_ATLAS.string_0;
				}
				return (!Boolean_0) ? TYPE_ATLAS.string_1 : TYPE_ATLAS.string_0;
			}
		}

		protected ISystemInfo()
		{
			SetSystemType();
			SetDeviceType();
			SetIsHiRes();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("---------- SYSTEM INFO ----------");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Density: " + Single_0);
			stringBuilder.AppendLine("SystemType: " + String_0);
			stringBuilder.AppendLine("DeviceType: " + DEVICE_TYPE_0);
			stringBuilder.AppendLine("IsHiRes: " + Boolean_0);
			stringBuilder.AppendLine("TypeUIPath: " + String_1);
			stringBuilder.AppendLine("TypeUIAtlasResolutionPath: " + String_2);
			stringBuilder.AppendLine("AndroidActivity: " + ((Object_0 != null) ? Object_0.GetHashCode().ToString() : "none"));
			stringBuilder.AppendLine("Unique device id: " + String_3);
			stringBuilder.AppendLine("Version: " + String_6);
			stringBuilder.AppendLine("VersionCode: " + String_7);
			stringBuilder.AppendLine("ModelDeviceName: " + String_8);
			stringBuilder.AppendLine("DeviceSystemName: " + String_9);
			stringBuilder.AppendLine("SystemLanguage: " + String_10);
			stringBuilder.AppendLine("SystemCoutry: " + String_11);
			stringBuilder.AppendLine("BundleName: " + String_12);
			stringBuilder.AppendLine("IMEI: " + String_13);
			stringBuilder.AppendLine("IMSI: " + String_14);
			stringBuilder.AppendLine("AndroidId: " + String_14);
			stringBuilder.AppendLine("XoredMac: " + String_16);
			stringBuilder.AppendLine(string.Format("Screen WxH: {0}x{1}", Screen.width, Screen.height));
			stringBuilder.AppendLine("------------------------");
			return stringBuilder.ToString();
		}

		private void SetSystemType()
		{
			String_0 = SYSTEM_TYPE.string_1;
		}

		private void SetDeviceType()
		{
			if (!(String_0 == SYSTEM_TYPE.string_2) && !(String_0 == SYSTEM_TYPE.string_3))
			{
				DEVICE_TYPE_0 = DEVICE_TYPE.STANDALONE;
				return;
			}
			float num = Mathf.Max((float)Screen.width / Single_0, (float)Screen.height / Single_0);
			if (num > 6f)
			{
				DEVICE_TYPE_0 = DEVICE_TYPE.TABLET;
			}
			else
			{
				DEVICE_TYPE_0 = DEVICE_TYPE.PHONE;
			}
		}

		private void SetIsHiRes()
		{
			Boolean_0 = ((Screen.width >= 1536 || Screen.height >= 1536) ? true : false);
		}
	}
}
