using System;
using System.Globalization;
using engine.helpers;

namespace engine.launcher
{
	public class VersionInfo : ICloneable, IComparable
	{
		private int int_0;

		private int int_1;

		private int int_2;

		private int int_3;

		public int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			get
			{
				return int_1;
			}
			set
			{
				int_1 = value;
			}
		}

		public int Int32_2
		{
			get
			{
				return int_2;
			}
			set
			{
				int_2 = value;
			}
		}

		public int Int32_3
		{
			get
			{
				return int_3;
			}
			set
			{
				int_3 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return int_0 == 0 && int_1 == 0 && int_2 == -1 && int_3 == -1;
			}
		}

		public VersionInfo(Version version_0)
			: this(version_0.Major, version_0.Minor, version_0.Build, version_0.Revision)
		{
		}

		public VersionInfo()
		{
			int_2 = -1;
			int_3 = -1;
			int_0 = 0;
			int_1 = 0;
		}

		public VersionInfo(string string_0)
		{
			int_2 = -1;
			int_3 = -1;
			if (string_0 == null)
			{
				throw new ArgumentNullException("version");
			}
			char[] separator = new char[1] { '.' };
			string[] array = string_0.Split(separator);
			int num = array.Length;
			if (num >= 2 && num <= 4)
			{
				int_0 = int.Parse(array[0], CultureInfo.InvariantCulture);
				if (int_0 < 0)
				{
					throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
				}
				int_1 = int.Parse(array[1], CultureInfo.InvariantCulture);
				if (int_1 < 0)
				{
					throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
				}
				num -= 2;
				if (num <= 0)
				{
					return;
				}
				int_2 = int.Parse(array[2], CultureInfo.InvariantCulture);
				if (int_2 < 0)
				{
					throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
				}
				num--;
				if (num > 0)
				{
					int_3 = int.Parse(array[3], CultureInfo.InvariantCulture);
					if (int_3 < 0)
					{
						throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
					}
				}
				return;
			}
			throw new ArgumentException("Arg_VersionString");
		}

		public VersionInfo(int int_4, int int_5)
		{
			int_2 = -1;
			int_3 = -1;
			if (int_4 < 0)
			{
				throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
			}
			if (int_5 < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
			}
			int_0 = int_4;
			int_1 = int_5;
		}

		public VersionInfo(int int_4, int int_5, int int_6)
		{
			int_2 = -1;
			int_3 = -1;
			if (int_4 < 0)
			{
				throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
			}
			if (int_5 < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
			}
			if (int_6 < 0)
			{
				throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
			}
			int_0 = int_4;
			int_1 = int_5;
			int_2 = int_6;
		}

		public VersionInfo(int int_4, int int_5, int int_6, int int_7)
		{
			int_2 = -1;
			int_3 = -1;
			if (int_4 < 0)
			{
				throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
			}
			if (int_5 < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
			}
			if (int_6 < 0)
			{
				throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
			}
			if (int_7 < 0)
			{
				throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
			}
			int_0 = int_4;
			int_1 = int_5;
			int_2 = int_6;
			int_3 = int_7;
		}

		public object Clone()
		{
			VersionInfo versionInfo = new VersionInfo();
			versionInfo.Int32_0 = int_0;
			versionInfo.Int32_1 = int_1;
			versionInfo.Int32_2 = int_2;
			versionInfo.Int32_3 = int_3;
			return versionInfo;
		}

		public void CopyFrom(VersionInfo versionInfo_0)
		{
			if (versionInfo_0 == null)
			{
				Log.AddLine("For copy VersionInfo needs source info!", Log.LogLevel.ERROR);
				return;
			}
			int_0 = versionInfo_0.Int32_0;
			int_1 = versionInfo_0.Int32_1;
			int_2 = versionInfo_0.Int32_2;
			int_3 = versionInfo_0.Int32_3;
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is VersionInfo))
			{
				throw new ArgumentException("Arg_MustBeVersion");
			}
			VersionInfo versionInfo = (VersionInfo)other;
			if (int_0 != versionInfo.Int32_0)
			{
				if (int_0 > versionInfo.Int32_0)
				{
					return 1;
				}
				return -1;
			}
			if (int_1 != versionInfo.Int32_1)
			{
				if (int_1 > versionInfo.Int32_1)
				{
					return 1;
				}
				return -1;
			}
			if (int_2 != versionInfo.Int32_2)
			{
				if (int_2 > versionInfo.Int32_2)
				{
					return 1;
				}
				return -1;
			}
			if (int_3 == versionInfo.Int32_3)
			{
				return 0;
			}
			if (int_3 > versionInfo.Int32_3)
			{
				return 1;
			}
			return -1;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is VersionInfo)
			{
				VersionInfo versionInfo = (VersionInfo)obj;
				if (int_0 == versionInfo.Int32_0 && int_1 == versionInfo.Int32_1 && int_2 == versionInfo.Int32_2 && int_3 == versionInfo.Int32_3)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			int num = 0;
			num = 0 | ((int_0 & 0xF) << 28);
			num |= (int_1 & 0xFF) << 20;
			num |= (int_2 & 0xFF) << 12;
			return num | (int_3 & 0xFFF);
		}

		public override string ToString()
		{
			if (int_2 == -1)
			{
				return ToString(2);
			}
			if (int_3 == -1)
			{
				return ToString(3);
			}
			return ToString(4);
		}

		public string ToString(int int_4)
		{
			switch (int_4)
			{
			default:
				if (int_2 == -1)
				{
					throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "2"), "fieldCount");
				}
				if (int_4 == 3)
				{
					object[] array = new object[5] { int_0, ".", int_1, ".", int_2 };
					return string.Concat(array);
				}
				if (int_3 == -1)
				{
					throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "3"), "fieldCount");
				}
				if (int_4 == 4)
				{
					object[] array = new object[7] { int_0, ".", int_1, ".", int_2, ".", int_3 };
					return string.Concat(array);
				}
				throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "4"), "fieldCount");
			case 0:
				return string.Empty;
			case 1:
				return int_0.ToString();
			case 2:
				return int_0 + "." + int_1;
			}
		}

		public static bool operator ==(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			return versionInfo_0.Equals(versionInfo_1);
		}

		public static bool operator >(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			return versionInfo_1 < versionInfo_0;
		}

		public static bool operator >=(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			return versionInfo_1 <= versionInfo_0;
		}

		public static bool operator !=(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			return !versionInfo_0.Equals(versionInfo_1);
		}

		public static bool operator <(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			if (versionInfo_0 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return versionInfo_0.CompareTo(versionInfo_1) < 0;
		}

		public static bool operator <=(VersionInfo versionInfo_0, VersionInfo versionInfo_1)
		{
			if (versionInfo_0 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return versionInfo_0.CompareTo(versionInfo_1) <= 0;
		}
	}
}
