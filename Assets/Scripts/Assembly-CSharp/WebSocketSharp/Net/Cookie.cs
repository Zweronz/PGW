using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebSocketSharp.Net
{
	[Serializable]
	public sealed class Cookie
	{
		private string string_0;

		private Uri uri_0;

		private bool bool_0;

		private string string_1;

		private DateTime dateTime_0;

		private bool bool_1;

		private string string_2;

		private string string_3;

		private string string_4;

		private int[] int_0;

		private static readonly char[] char_0;

		private static readonly char[] char_1;

		private bool bool_2;

		private DateTime dateTime_1;

		private string string_5;

		private int int_1;

		[CompilerGenerated]
		private bool bool_3;

		internal bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_3;
			}
			[CompilerGenerated]
			set
			{
				bool_3 = value;
			}
		}

		internal int Int32_0
		{
			get
			{
				if (dateTime_0 == DateTime.MinValue)
				{
					return 0;
				}
				DateTime dateTime = ((dateTime_0.Kind == DateTimeKind.Local) ? dateTime_0 : dateTime_0.ToLocalTime());
				TimeSpan timeSpan = dateTime - DateTime.Now;
				return (timeSpan > TimeSpan.Zero) ? ((int)timeSpan.TotalSeconds) : 0;
			}
		}

		internal int[] Int32_1
		{
			get
			{
				return int_0;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
			set
			{
				string_0 = value ?? string.Empty;
			}
		}

		public Uri Uri_0
		{
			get
			{
				return uri_0;
			}
			set
			{
				uri_0 = value;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_1;
			}
			set
			{
				if (value.IsNullOrEmpty())
				{
					string_1 = string.Empty;
					Boolean_0 = true;
				}
				else
				{
					string_1 = value;
					Boolean_0 = value[0] != '.';
				}
			}
		}

		public bool Boolean_2
		{
			get
			{
				return dateTime_0 != DateTime.MinValue && dateTime_0 <= DateTime.Now;
			}
			set
			{
				dateTime_0 = ((!value) ? DateTime.MinValue : DateTime.Now);
			}
		}

		public DateTime DateTime_0
		{
			get
			{
				return dateTime_0;
			}
			set
			{
				dateTime_0 = value;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		public string String_2
		{
			get
			{
				return string_2;
			}
			set
			{
				string string_;
				if (!canSetName(value, out string_))
				{
					throw new CookieException(string_);
				}
				string_2 = value;
			}
		}

		public string String_3
		{
			get
			{
				return string_3;
			}
			set
			{
				string_3 = value ?? string.Empty;
			}
		}

		public string String_4
		{
			get
			{
				return string_4;
			}
			set
			{
				if (value.IsNullOrEmpty())
				{
					string_4 = string.Empty;
					int_0 = new int[0];
					return;
				}
				if (!value.IsEnclosedIn('"'))
				{
					throw new CookieException("The value specified for the Port attribute isn't enclosed in double quotes.");
				}
				string string_;
				if (!tryCreatePorts(value, out int_0, out string_))
				{
					throw new CookieException(string.Format("The value specified for the Port attribute contains an invalid value: {0}", string_));
				}
				string_4 = value;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return bool_2;
			}
			set
			{
				bool_2 = value;
			}
		}

		public DateTime DateTime_1
		{
			get
			{
				return dateTime_1;
			}
		}

		public string String_5
		{
			get
			{
				return string_5;
			}
			set
			{
				string string_;
				if (!canSetValue(value, out string_))
				{
					throw new CookieException(string_);
				}
				string_5 = ((value.Length <= 0) ? "\"\"" : value);
			}
		}

		public int Int32_2
		{
			get
			{
				return int_1;
			}
			set
			{
				if (value < 0 || value > 1)
				{
					throw new ArgumentOutOfRangeException("value", "Not 0 or 1.");
				}
				int_1 = value;
			}
		}

		public Cookie()
		{
			string_0 = string.Empty;
			string_1 = string.Empty;
			dateTime_0 = DateTime.MinValue;
			string_2 = string.Empty;
			string_3 = string.Empty;
			string_4 = string.Empty;
			int_0 = new int[0];
			dateTime_1 = DateTime.Now;
			string_5 = string.Empty;
			int_1 = 0;
		}

		public Cookie(string string_6, string string_7)
			: this()
		{
			String_2 = string_6;
			String_5 = string_7;
		}

		public Cookie(string string_6, string string_7, string string_8)
			: this(string_6, string_7)
		{
			String_3 = string_8;
		}

		public Cookie(string string_6, string string_7, string string_8, string string_9)
			: this(string_6, string_7, string_8)
		{
			String_1 = string_9;
		}

		static Cookie()
		{
			char_0 = new char[7] { ' ', '=', ';', ',', '\n', '\r', '\t' };
			char_1 = new char[2] { ';', ',' };
		}

		private static bool canSetName(string string_6, out string string_7)
		{
			if (string_6.IsNullOrEmpty())
			{
				string_7 = "The value specified for the Name is null or empty.";
				return false;
			}
			if (string_6[0] != '$' && !string_6.Contains(char_0))
			{
				string_7 = string.Empty;
				return true;
			}
			string_7 = "The value specified for the Name contains an invalid character.";
			return false;
		}

		private static bool canSetValue(string string_6, out string string_7)
		{
			if (string_6 == null)
			{
				string_7 = "The value specified for the Value is null.";
				return false;
			}
			if (string_6.Contains(char_1) && !string_6.IsEnclosedIn('"'))
			{
				string_7 = "The value specified for the Value contains an invalid character.";
				return false;
			}
			string_7 = string.Empty;
			return true;
		}

		private static int hash(int int_2, int int_3, int int_4, int int_5, int int_6)
		{
			return int_2 ^ ((int_3 << 13) | (int_3 >> 19)) ^ ((int_4 << 26) | (int_4 >> 6)) ^ ((int_5 << 7) | (int_5 >> 25)) ^ ((int_6 << 20) | (int_6 >> 12));
		}

		private string toResponseStringVersion0()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0}={1}", string_2, string_5);
			if (dateTime_0 != DateTime.MinValue)
			{
				stringBuilder.AppendFormat("; Expires={0}", dateTime_0.ToUniversalTime().ToString("ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US")));
			}
			if (!string_3.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; Path={0}", string_3);
			}
			if (!string_1.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; Domain={0}", string_1);
			}
			if (bool_2)
			{
				stringBuilder.Append("; Secure");
			}
			if (bool_1)
			{
				stringBuilder.Append("; HttpOnly");
			}
			return stringBuilder.ToString();
		}

		private string toResponseStringVersion1()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0}={1}; Version={2}", string_2, string_5, int_1);
			if (dateTime_0 != DateTime.MinValue)
			{
				stringBuilder.AppendFormat("; Max-Age={0}", Int32_0);
			}
			if (!string_3.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; Path={0}", string_3);
			}
			if (!string_1.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; Domain={0}", string_1);
			}
			if (!string_4.IsNullOrEmpty())
			{
				if (string_4 == "\"\"")
				{
					stringBuilder.Append("; Port");
				}
				else
				{
					stringBuilder.AppendFormat("; Port={0}", string_4);
				}
			}
			if (!string_0.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; Comment={0}", string_0.UrlEncode());
			}
			if (uri_0 != null)
			{
				string originalString = uri_0.OriginalString;
				stringBuilder.AppendFormat("; CommentURL={0}", (!originalString.IsToken()) ? originalString.Quote() : originalString);
			}
			if (bool_0)
			{
				stringBuilder.Append("; Discard");
			}
			if (bool_2)
			{
				stringBuilder.Append("; Secure");
			}
			return stringBuilder.ToString();
		}

		private static bool tryCreatePorts(string string_6, out int[] int_2, out string string_7)
		{
			string[] array = string_6.Trim('"').Split(',');
			int num = array.Length;
			int[] array2 = new int[num];
			int num2 = 0;
			string text;
			while (true)
			{
				if (num2 < num)
				{
					array2[num2] = int.MinValue;
					text = array[num2].Trim();
					if (text.Length != 0 && !int.TryParse(text, out array2[num2]))
					{
						break;
					}
					num2++;
					continue;
				}
				int_2 = array2;
				string_7 = string.Empty;
				return true;
			}
			int_2 = new int[0];
			string_7 = text;
			return false;
		}

		internal string ToRequestString(Uri uri_1)
		{
			if (string_2.Length == 0)
			{
				return string.Empty;
			}
			if (int_1 == 0)
			{
				return string.Format("{0}={1}", string_2, string_5);
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("$Version={0}; {1}={2}", int_1, string_2, string_5);
			if (!string_3.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; $Path={0}", string_3);
			}
			else if (uri_1 != null)
			{
				stringBuilder.AppendFormat("; $Path={0}", uri_1.GetAbsolutePath());
			}
			else
			{
				stringBuilder.Append("; $Path=/");
			}
			if ((uri_1 == null || uri_1.Host != string_1) && !string_1.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat("; $Domain={0}", string_1);
			}
			if (!string_4.IsNullOrEmpty())
			{
				if (string_4 == "\"\"")
				{
					stringBuilder.Append("; $Port");
				}
				else
				{
					stringBuilder.AppendFormat("; $Port={0}", string_4);
				}
			}
			return stringBuilder.ToString();
		}

		internal string ToResponseString()
		{
			return (string_2.Length <= 0) ? string.Empty : ((int_1 != 0) ? toResponseStringVersion1() : toResponseStringVersion0());
		}

		public override bool Equals(object obj)
		{
			Cookie cookie = obj as Cookie;
			return cookie != null && string_2.Equals(cookie.String_2, StringComparison.InvariantCultureIgnoreCase) && string_5.Equals(cookie.String_5, StringComparison.InvariantCulture) && string_3.Equals(cookie.String_3, StringComparison.InvariantCulture) && string_1.Equals(cookie.String_1, StringComparison.InvariantCultureIgnoreCase) && int_1 == cookie.Int32_2;
		}

		public override int GetHashCode()
		{
			return hash(StringComparer.InvariantCultureIgnoreCase.GetHashCode(string_2), string_5.GetHashCode(), string_3.GetHashCode(), StringComparer.InvariantCultureIgnoreCase.GetHashCode(string_1), int_1);
		}

		public override string ToString()
		{
			return ToRequestString(null);
		}
	}
}
