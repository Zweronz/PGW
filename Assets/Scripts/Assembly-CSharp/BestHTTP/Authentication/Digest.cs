using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BestHTTP.Extensions;

namespace BestHTTP.Authentication
{
	internal sealed class Digest
	{
		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private AuthenticationTypes authenticationTypes_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private List<string> list_0;

		[CompilerGenerated]
		private string string_4;

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private string string_5;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_0;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_1;

		public Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			private set
			{
				uri_0 = value;
			}
		}

		public AuthenticationTypes AuthenticationTypes_0
		{
			[CompilerGenerated]
			get
			{
				return authenticationTypes_0;
			}
			[CompilerGenerated]
			private set
			{
				authenticationTypes_0 = value;
			}
		}

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

		private string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			set
			{
				string_1 = value;
			}
		}

		private string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			set
			{
				string_2 = value;
			}
		}

		private string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			set
			{
				string_3 = value;
			}
		}

		public List<string> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			private set
			{
				list_0 = value;
			}
		}

		private string String_4
		{
			[CompilerGenerated]
			get
			{
				return string_4;
			}
			[CompilerGenerated]
			set
			{
				string_4 = value;
			}
		}

		private int Int32_0
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

		private string String_5
		{
			[CompilerGenerated]
			get
			{
				return string_5;
			}
			[CompilerGenerated]
			set
			{
				string_5 = value;
			}
		}

		internal Digest(Uri uri_1)
		{
			Uri_0 = uri_1;
			String_3 = "md5";
		}

		public void ParseChallange(string string_6)
		{
			AuthenticationTypes_0 = AuthenticationTypes.Unknown;
			Boolean_0 = false;
			String_2 = null;
			String_5 = null;
			Int32_0 = 0;
			String_4 = null;
			if (List_0 != null)
			{
				List_0.Clear();
			}
			WWWAuthenticateHeaderParser wWWAuthenticateHeaderParser = new WWWAuthenticateHeaderParser(string_6);
			foreach (KeyValuePair item2 in wWWAuthenticateHeaderParser.List_0)
			{
				switch (item2.String_0)
				{
				case "basic":
					AuthenticationTypes_0 = AuthenticationTypes.Basic;
					break;
				case "digest":
					AuthenticationTypes_0 = AuthenticationTypes.Digest;
					break;
				case "realm":
					String_0 = item2.String_1;
					break;
				case "domain":
					if (!string.IsNullOrEmpty(item2.String_1) && item2.String_1.Length != 0)
					{
						if (List_0 == null)
						{
							List_0 = new List<string>();
						}
						int num = 0;
						string item = item2.String_1.Read(ref num, ' ');
						do
						{
							List_0.Add(item);
							item = item2.String_1.Read(ref num, ' ');
						}
						while (num < item2.String_1.Length);
					}
					break;
				case "nonce":
					String_1 = item2.String_1;
					break;
				case "qop":
					String_4 = item2.String_1;
					break;
				case "stale":
					Boolean_0 = bool.Parse(item2.String_1);
					break;
				case "opaque":
					String_2 = item2.String_1;
					break;
				case "algorithm":
					String_3 = item2.String_1;
					break;
				}
			}
		}

		public string GenerateResponseHeader(HTTPRequest httprequest_0, Credentials credentials_0)
		{
			try
			{
				switch (AuthenticationTypes_0)
				{
				case AuthenticationTypes.Digest:
				{
					Int32_0++;
					string empty = string.Empty;
					string text = new Random(httprequest_0.GetHashCode()).Next(int.MinValue, int.MaxValue).ToString("X8");
					string text2 = Int32_0.ToString("X8");
					string text3;
					string empty2;
					string text6;
					switch (String_3.TrimAndLower())
					{
					default:
					{
						int num = 0;
						if (num == 1)
						{
							if (string.IsNullOrEmpty(String_5))
							{
								String_5 = string.Format("{0}:{1}:{2}:{3}:{4}", credentials_0.String_0, String_0, credentials_0.String_1, String_1, text2).CalculateMD5Hash();
							}
							empty = String_5;
							goto IL_014c;
						}
						return string.Empty;
					}
					case "md5":
						{
							empty = string.Format("{0}:{1}:{2}", credentials_0.String_0, String_0, credentials_0.String_1).CalculateMD5Hash();
							goto IL_014c;
						}
						IL_014c:
						empty2 = string.Empty;
						text3 = ((String_4 == null) ? null : String_4.TrimAndLower());
						if (text3 == null)
						{
							string arg = (httprequest_0.HTTPMethods_0.ToString().ToUpper() + ":" + httprequest_0.Uri_2.PathAndQuery).CalculateMD5Hash();
							empty2 = string.Format("{0}:{1}:{2}", empty, String_1, arg).CalculateMD5Hash();
						}
						else if (text3.Contains("auth-int"))
						{
							text3 = "auth-int";
							byte[] array = httprequest_0.GetEntityBody();
							if (array == null)
							{
								array = string.Empty.GetASCIIBytes();
							}
							string text4 = string.Format("{0}:{1}:{2}", httprequest_0.HTTPMethods_0.ToString().ToUpper(), httprequest_0.Uri_2.PathAndQuery, array.CalculateMD5Hash()).CalculateMD5Hash();
							empty2 = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", empty, String_1, text2, text, text3, text4).CalculateMD5Hash();
						}
						else
						{
							if (!text3.Contains("auth"))
							{
								return string.Empty;
							}
							text3 = "auth";
							string text5 = (httprequest_0.HTTPMethods_0.ToString().ToUpper() + ":" + httprequest_0.Uri_2.PathAndQuery).CalculateMD5Hash();
							empty2 = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", empty, String_1, text2, text, text3, text5).CalculateMD5Hash();
						}
						text6 = string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", cnonce=\"{4}\", response=\"{5}\"", credentials_0.String_0, String_0, String_1, httprequest_0.Uri_0.PathAndQuery, text, empty2);
						if (text3 != null)
						{
							text6 = string.Concat(text6, ", qop=\"" + text3 + "\", nc=" + text2);
						}
						if (!string.IsNullOrEmpty(String_2))
						{
							text6 = text6 + ", opaque=\"" + String_2 + "\"";
						}
						return text6;
					}
				}
				case AuthenticationTypes.Basic:
					return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", credentials_0.String_0, credentials_0.String_1)));
				}
			}
			catch
			{
			}
			return string.Empty;
		}

		public bool IsUriProtected(Uri uri_1)
		{
			if (string.CompareOrdinal(uri_1.Host, Uri_0.Host) != 0)
			{
				return false;
			}
			string text = uri_1.ToString();
			if (List_0 != null && List_0.Count > 0)
			{
				for (int i = 0; i < List_0.Count; i++)
				{
					if (text.Contains(List_0[i]))
					{
						return true;
					}
				}
			}
			return true;
		}
	}
}
