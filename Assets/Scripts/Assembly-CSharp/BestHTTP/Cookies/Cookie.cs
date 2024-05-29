using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BestHTTP.Extensions;

namespace BestHTTP.Cookies
{
	public sealed class Cookie : IComparable<Cookie>, IEquatable<Cookie>
	{
		private const int int_0 = 1;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private DateTime dateTime_0;

		[CompilerGenerated]
		private DateTime dateTime_1;

		[CompilerGenerated]
		private DateTime dateTime_2;

		[CompilerGenerated]
		private long long_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private bool bool_2;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_0;

		[CompilerGenerated]
		private static Func<char, bool> func_0;

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

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			private set
			{
				string_1 = value;
			}
		}

		public DateTime DateTime_0
		{
			[CompilerGenerated]
			get
			{
				return dateTime_0;
			}
			[CompilerGenerated]
			internal set
			{
				dateTime_0 = value;
			}
		}

		public DateTime DateTime_1
		{
			[CompilerGenerated]
			get
			{
				return dateTime_1;
			}
			[CompilerGenerated]
			set
			{
				dateTime_1 = value;
			}
		}

		public DateTime DateTime_2
		{
			[CompilerGenerated]
			get
			{
				return dateTime_2;
			}
			[CompilerGenerated]
			private set
			{
				dateTime_2 = value;
			}
		}

		public long Int64_0
		{
			[CompilerGenerated]
			get
			{
				return long_0;
			}
			[CompilerGenerated]
			private set
			{
				long_0 = value;
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

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			private set
			{
				string_3 = value;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			private set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_2
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			private set
			{
				bool_2 = value;
			}
		}

		public Cookie(string string_4, string string_5)
			: this(string_4, string_5, string.Empty, string.Empty)
		{
		}

		public Cookie(string string_4, string string_5, string string_6)
			: this(string_4, string_5, string_6, string.Empty)
		{
		}

		public Cookie(string string_4, string string_5, string string_6, string string_7)
			: this()
		{
			String_0 = string_4;
			String_1 = string_5;
			String_3 = string_6;
			String_2 = string_7;
		}

		internal Cookie()
		{
			Boolean_0 = true;
			Int64_0 = -1L;
			DateTime_1 = DateTime.UtcNow;
		}

		public bool WillExpireInTheFuture()
		{
			if (Boolean_0)
			{
				return true;
			}
			return (Int64_0 == -1L) ? (DateTime_2 > DateTime.UtcNow) : (Math.Max(0L, (long)(DateTime.UtcNow - DateTime_0).TotalSeconds) < Int64_0);
		}

		public uint GuessSize()
		{
			return (uint)(((String_0 != null) ? (String_0.Length * 2) : 0) + ((String_1 != null) ? (String_1.Length * 2) : 0) + ((String_2 != null) ? (String_2.Length * 2) : 0) + ((String_3 != null) ? (String_3.Length * 2) : 0) + 32 + 3);
		}

		public static Cookie Parse(string string_4, Uri uri_0)
		{
			Cookie cookie = new Cookie();
			try
			{
				List<KeyValuePair> list = ParseCookieHeader(string_4);
				foreach (KeyValuePair item in list)
				{
					switch (item.String_0.ToLowerInvariant())
					{
					case "path":
					{
						object obj;
						if (!string.IsNullOrEmpty(item.String_1) && item.String_1.StartsWith("/"))
						{
							string text2 = (cookie.String_3 = item.String_1);
							obj = text2;
						}
						else
						{
							obj = "/";
						}
						cookie.String_3 = (string)obj;
						break;
					}
					case "domain":
						if (!string.IsNullOrEmpty(item.String_1))
						{
							cookie.String_2 = ((!item.String_1.StartsWith(".")) ? item.String_1 : item.String_1.Substring(1));
							break;
						}
						return null;
					case "expires":
						cookie.DateTime_2 = item.String_1.ToDateTime(DateTime.FromBinary(0L));
						cookie.Boolean_0 = false;
						break;
					case "max-age":
						cookie.Int64_0 = item.String_1.ToInt64(-1L);
						cookie.Boolean_0 = false;
						break;
					case "secure":
						cookie.Boolean_1 = true;
						break;
					case "httponly":
						cookie.Boolean_2 = true;
						break;
					default:
						cookie.String_0 = item.String_0;
						cookie.String_1 = item.String_1;
						break;
					}
				}
				if (HTTPManager.Boolean_3)
				{
					cookie.Boolean_0 = true;
				}
				if (string.IsNullOrEmpty(cookie.String_2))
				{
					cookie.String_2 = uri_0.Host;
				}
				if (string.IsNullOrEmpty(cookie.String_3))
				{
					cookie.String_3 = uri_0.AbsolutePath;
				}
				DateTime dateTime = (cookie.DateTime_1 = DateTime.UtcNow);
				cookie.DateTime_0 = dateTime;
			}
			catch
			{
			}
			return cookie;
		}

		internal void SaveTo(BinaryWriter binaryWriter_0)
		{
			binaryWriter_0.Write(1);
			binaryWriter_0.Write(String_0 ?? string.Empty);
			binaryWriter_0.Write(String_1 ?? string.Empty);
			binaryWriter_0.Write(DateTime_0.ToBinary());
			binaryWriter_0.Write(DateTime_1.ToBinary());
			binaryWriter_0.Write(DateTime_2.ToBinary());
			binaryWriter_0.Write(Int64_0);
			binaryWriter_0.Write(Boolean_0);
			binaryWriter_0.Write(String_2 ?? string.Empty);
			binaryWriter_0.Write(String_3 ?? string.Empty);
			binaryWriter_0.Write(Boolean_1);
			binaryWriter_0.Write(Boolean_2);
		}

		internal void LoadFrom(BinaryReader binaryReader_0)
		{
			binaryReader_0.ReadInt32();
			String_0 = binaryReader_0.ReadString();
			String_1 = binaryReader_0.ReadString();
			DateTime_0 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			DateTime_1 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			DateTime_2 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			Int64_0 = binaryReader_0.ReadInt64();
			Boolean_0 = binaryReader_0.ReadBoolean();
			String_2 = binaryReader_0.ReadString();
			String_3 = binaryReader_0.ReadString();
			Boolean_1 = binaryReader_0.ReadBoolean();
			Boolean_2 = binaryReader_0.ReadBoolean();
		}

		public override string ToString()
		{
			return String_0 + "=" + String_1;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			return Equals(obj as Cookie);
		}

		public bool Equals(Cookie other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			return String_0.Equals(other.String_0, StringComparison.Ordinal) && ((String_2 == null && other.String_2 == null) || String_2.Equals(other.String_2, StringComparison.Ordinal)) && ((String_3 == null && other.String_3 == null) || String_3.Equals(other.String_3, StringComparison.Ordinal));
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		private static string ReadValue(string string_4, ref int int_1)
		{
			string empty = string.Empty;
			if (string_4 == null)
			{
				return empty;
			}
			return string_4.Read(ref int_1, ';');
		}

		private static List<KeyValuePair> ParseCookieHeader(string string_4)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (string_4 == null)
			{
				return list;
			}
			int int_ = 0;
			while (int_ < string_4.Length)
			{
				string text = string_4.Read(ref int_, (char char_0) => char_0 != '=' && char_0 != ';').Trim();
				KeyValuePair keyValuePair = new KeyValuePair(text);
				if (int_ < string_4.Length && string_4[int_ - 1] == '=')
				{
					keyValuePair.String_1 = ReadValue(string_4, ref int_);
				}
				list.Add(keyValuePair);
			}
			return list;
		}

		public int CompareTo(Cookie a_Other)
		{
			return DateTime_1.CompareTo(a_Other.DateTime_1);
		}
	}
}
