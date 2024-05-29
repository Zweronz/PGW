using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;

namespace WebSocketSharp.Net
{
	internal sealed class HttpUtility
	{
		private static Dictionary<string, char> dictionary_0;

		private static char[] char_0 = "0123456789abcdef".ToCharArray();

		private static object object_0 = new object();

		private static int getChar(byte[] byte_0, int int_0, int int_1)
		{
			int num = 0;
			int num2 = int_1 + int_0;
			int num3 = int_0;
			while (true)
			{
				if (num3 < num2)
				{
					int @int = getInt(byte_0[num3]);
					if (@int == -1)
					{
						break;
					}
					num = (num << 4) + @int;
					num3++;
					continue;
				}
				return num;
			}
			return -1;
		}

		private static int getChar(string string_0, int int_0, int int_1)
		{
			int num = 0;
			int num2 = int_1 + int_0;
			int num3 = int_0;
			while (true)
			{
				if (num3 < num2)
				{
					char c = string_0[num3];
					if (c > '\u007f')
					{
						break;
					}
					int @int = getInt((byte)c);
					if (@int != -1)
					{
						num = (num << 4) + @int;
						num3++;
						continue;
					}
					return -1;
				}
				return num;
			}
			return -1;
		}

		private static char[] getChars(MemoryStream memoryStream_0, Encoding encoding_0)
		{
			return encoding_0.GetChars(memoryStream_0.GetBuffer(), 0, (int)memoryStream_0.Length);
		}

		private static Dictionary<string, char> getEntities()
		{
			lock (object_0)
			{
				if (dictionary_0 == null)
				{
					initEntities();
				}
				return dictionary_0;
			}
		}

		private static int getInt(byte byte_0)
		{
			char c = (char)byte_0;
			return (c >= '0' && c <= '9') ? (c - 48) : ((c >= 'a' && c <= 'f') ? (c - 97 + 10) : ((c < 'A' || c > 'F') ? (-1) : (c - 65 + 10)));
		}

		private static void initEntities()
		{
			dictionary_0 = new Dictionary<string, char>();
			dictionary_0.Add("nbsp", '\u00a0');
			dictionary_0.Add("iexcl", '¡');
			dictionary_0.Add("cent", '¢');
			dictionary_0.Add("pound", '£');
			dictionary_0.Add("curren", '¤');
			dictionary_0.Add("yen", '¥');
			dictionary_0.Add("brvbar", '¦');
			dictionary_0.Add("sect", '§');
			dictionary_0.Add("uml", '\u00a8');
			dictionary_0.Add("copy", '©');
			dictionary_0.Add("ordf", 'ª');
			dictionary_0.Add("laquo", '«');
			dictionary_0.Add("not", '¬');
			dictionary_0.Add("shy", '\u00ad');
			dictionary_0.Add("reg", '®');
			dictionary_0.Add("macr", '\u00af');
			dictionary_0.Add("deg", '°');
			dictionary_0.Add("plusmn", '±');
			dictionary_0.Add("sup2", '²');
			dictionary_0.Add("sup3", '³');
			dictionary_0.Add("acute", '\u00b4');
			dictionary_0.Add("micro", 'µ');
			dictionary_0.Add("para", '¶');
			dictionary_0.Add("middot", '·');
			dictionary_0.Add("cedil", '\u00b8');
			dictionary_0.Add("sup1", '¹');
			dictionary_0.Add("ordm", 'º');
			dictionary_0.Add("raquo", '»');
			dictionary_0.Add("frac14", '¼');
			dictionary_0.Add("frac12", '½');
			dictionary_0.Add("frac34", '¾');
			dictionary_0.Add("iquest", '¿');
			dictionary_0.Add("Agrave", 'À');
			dictionary_0.Add("Aacute", 'Á');
			dictionary_0.Add("Acirc", 'Â');
			dictionary_0.Add("Atilde", 'Ã');
			dictionary_0.Add("Auml", 'Ä');
			dictionary_0.Add("Aring", 'Å');
			dictionary_0.Add("AElig", 'Æ');
			dictionary_0.Add("Ccedil", 'Ç');
			dictionary_0.Add("Egrave", 'È');
			dictionary_0.Add("Eacute", 'É');
			dictionary_0.Add("Ecirc", 'Ê');
			dictionary_0.Add("Euml", 'Ë');
			dictionary_0.Add("Igrave", 'Ì');
			dictionary_0.Add("Iacute", 'Í');
			dictionary_0.Add("Icirc", 'Î');
			dictionary_0.Add("Iuml", 'Ï');
			dictionary_0.Add("ETH", 'Ð');
			dictionary_0.Add("Ntilde", 'Ñ');
			dictionary_0.Add("Ograve", 'Ò');
			dictionary_0.Add("Oacute", 'Ó');
			dictionary_0.Add("Ocirc", 'Ô');
			dictionary_0.Add("Otilde", 'Õ');
			dictionary_0.Add("Ouml", 'Ö');
			dictionary_0.Add("times", '×');
			dictionary_0.Add("Oslash", 'Ø');
			dictionary_0.Add("Ugrave", 'Ù');
			dictionary_0.Add("Uacute", 'Ú');
			dictionary_0.Add("Ucirc", 'Û');
			dictionary_0.Add("Uuml", 'Ü');
			dictionary_0.Add("Yacute", 'Ý');
			dictionary_0.Add("THORN", 'Þ');
			dictionary_0.Add("szlig", 'ß');
			dictionary_0.Add("agrave", 'à');
			dictionary_0.Add("aacute", 'á');
			dictionary_0.Add("acirc", 'â');
			dictionary_0.Add("atilde", 'ã');
			dictionary_0.Add("auml", 'ä');
			dictionary_0.Add("aring", 'å');
			dictionary_0.Add("aelig", 'æ');
			dictionary_0.Add("ccedil", 'ç');
			dictionary_0.Add("egrave", 'è');
			dictionary_0.Add("eacute", 'é');
			dictionary_0.Add("ecirc", 'ê');
			dictionary_0.Add("euml", 'ë');
			dictionary_0.Add("igrave", 'ì');
			dictionary_0.Add("iacute", 'í');
			dictionary_0.Add("icirc", 'î');
			dictionary_0.Add("iuml", 'ï');
			dictionary_0.Add("eth", 'ð');
			dictionary_0.Add("ntilde", 'ñ');
			dictionary_0.Add("ograve", 'ò');
			dictionary_0.Add("oacute", 'ó');
			dictionary_0.Add("ocirc", 'ô');
			dictionary_0.Add("otilde", 'õ');
			dictionary_0.Add("ouml", 'ö');
			dictionary_0.Add("divide", '÷');
			dictionary_0.Add("oslash", 'ø');
			dictionary_0.Add("ugrave", 'ù');
			dictionary_0.Add("uacute", 'ú');
			dictionary_0.Add("ucirc", 'û');
			dictionary_0.Add("uuml", 'ü');
			dictionary_0.Add("yacute", 'ý');
			dictionary_0.Add("thorn", 'þ');
			dictionary_0.Add("yuml", 'ÿ');
			dictionary_0.Add("fnof", 'ƒ');
			dictionary_0.Add("Alpha", 'Α');
			dictionary_0.Add("Beta", 'Β');
			dictionary_0.Add("Gamma", 'Γ');
			dictionary_0.Add("Delta", 'Δ');
			dictionary_0.Add("Epsilon", 'Ε');
			dictionary_0.Add("Zeta", 'Ζ');
			dictionary_0.Add("Eta", 'Η');
			dictionary_0.Add("Theta", 'Θ');
			dictionary_0.Add("Iota", 'Ι');
			dictionary_0.Add("Kappa", 'Κ');
			dictionary_0.Add("Lambda", 'Λ');
			dictionary_0.Add("Mu", 'Μ');
			dictionary_0.Add("Nu", 'Ν');
			dictionary_0.Add("Xi", 'Ξ');
			dictionary_0.Add("Omicron", 'Ο');
			dictionary_0.Add("Pi", 'Π');
			dictionary_0.Add("Rho", 'Ρ');
			dictionary_0.Add("Sigma", 'Σ');
			dictionary_0.Add("Tau", 'Τ');
			dictionary_0.Add("Upsilon", 'Υ');
			dictionary_0.Add("Phi", 'Φ');
			dictionary_0.Add("Chi", 'Χ');
			dictionary_0.Add("Psi", 'Ψ');
			dictionary_0.Add("Omega", 'Ω');
			dictionary_0.Add("alpha", 'α');
			dictionary_0.Add("beta", 'β');
			dictionary_0.Add("gamma", 'γ');
			dictionary_0.Add("delta", 'δ');
			dictionary_0.Add("epsilon", 'ε');
			dictionary_0.Add("zeta", 'ζ');
			dictionary_0.Add("eta", 'η');
			dictionary_0.Add("theta", 'θ');
			dictionary_0.Add("iota", 'ι');
			dictionary_0.Add("kappa", 'κ');
			dictionary_0.Add("lambda", 'λ');
			dictionary_0.Add("mu", 'μ');
			dictionary_0.Add("nu", 'ν');
			dictionary_0.Add("xi", 'ξ');
			dictionary_0.Add("omicron", 'ο');
			dictionary_0.Add("pi", 'π');
			dictionary_0.Add("rho", 'ρ');
			dictionary_0.Add("sigmaf", 'ς');
			dictionary_0.Add("sigma", 'σ');
			dictionary_0.Add("tau", 'τ');
			dictionary_0.Add("upsilon", 'υ');
			dictionary_0.Add("phi", 'φ');
			dictionary_0.Add("chi", 'χ');
			dictionary_0.Add("psi", 'ψ');
			dictionary_0.Add("omega", 'ω');
			dictionary_0.Add("thetasym", 'ϑ');
			dictionary_0.Add("upsih", 'ϒ');
			dictionary_0.Add("piv", 'ϖ');
			dictionary_0.Add("bull", '•');
			dictionary_0.Add("hellip", '…');
			dictionary_0.Add("prime", '′');
			dictionary_0.Add("Prime", '″');
			dictionary_0.Add("oline", '‾');
			dictionary_0.Add("frasl", '⁄');
			dictionary_0.Add("weierp", '℘');
			dictionary_0.Add("image", 'ℑ');
			dictionary_0.Add("real", 'ℜ');
			dictionary_0.Add("trade", '™');
			dictionary_0.Add("alefsym", 'ℵ');
			dictionary_0.Add("larr", '←');
			dictionary_0.Add("uarr", '↑');
			dictionary_0.Add("rarr", '→');
			dictionary_0.Add("darr", '↓');
			dictionary_0.Add("harr", '↔');
			dictionary_0.Add("crarr", '↵');
			dictionary_0.Add("lArr", '⇐');
			dictionary_0.Add("uArr", '⇑');
			dictionary_0.Add("rArr", '⇒');
			dictionary_0.Add("dArr", '⇓');
			dictionary_0.Add("hArr", '⇔');
			dictionary_0.Add("forall", '∀');
			dictionary_0.Add("part", '∂');
			dictionary_0.Add("exist", '∃');
			dictionary_0.Add("empty", '∅');
			dictionary_0.Add("nabla", '∇');
			dictionary_0.Add("isin", '∈');
			dictionary_0.Add("notin", '∉');
			dictionary_0.Add("ni", '∋');
			dictionary_0.Add("prod", '∏');
			dictionary_0.Add("sum", '∑');
			dictionary_0.Add("minus", '−');
			dictionary_0.Add("lowast", '∗');
			dictionary_0.Add("radic", '√');
			dictionary_0.Add("prop", '∝');
			dictionary_0.Add("infin", '∞');
			dictionary_0.Add("ang", '∠');
			dictionary_0.Add("and", '∧');
			dictionary_0.Add("or", '∨');
			dictionary_0.Add("cap", '∩');
			dictionary_0.Add("cup", '∪');
			dictionary_0.Add("int", '∫');
			dictionary_0.Add("there4", '∴');
			dictionary_0.Add("sim", '∼');
			dictionary_0.Add("cong", '≅');
			dictionary_0.Add("asymp", '≈');
			dictionary_0.Add("ne", '≠');
			dictionary_0.Add("equiv", '≡');
			dictionary_0.Add("le", '≤');
			dictionary_0.Add("ge", '≥');
			dictionary_0.Add("sub", '⊂');
			dictionary_0.Add("sup", '⊃');
			dictionary_0.Add("nsub", '⊄');
			dictionary_0.Add("sube", '⊆');
			dictionary_0.Add("supe", '⊇');
			dictionary_0.Add("oplus", '⊕');
			dictionary_0.Add("otimes", '⊗');
			dictionary_0.Add("perp", '⊥');
			dictionary_0.Add("sdot", '⋅');
			dictionary_0.Add("lceil", '⌈');
			dictionary_0.Add("rceil", '⌉');
			dictionary_0.Add("lfloor", '⌊');
			dictionary_0.Add("rfloor", '⌋');
			dictionary_0.Add("lang", '〈');
			dictionary_0.Add("rang", '〉');
			dictionary_0.Add("loz", '◊');
			dictionary_0.Add("spades", '♠');
			dictionary_0.Add("clubs", '♣');
			dictionary_0.Add("hearts", '♥');
			dictionary_0.Add("diams", '♦');
			dictionary_0.Add("quot", '"');
			dictionary_0.Add("amp", '&');
			dictionary_0.Add("lt", '<');
			dictionary_0.Add("gt", '>');
			dictionary_0.Add("OElig", 'Œ');
			dictionary_0.Add("oelig", 'œ');
			dictionary_0.Add("Scaron", 'Š');
			dictionary_0.Add("scaron", 'š');
			dictionary_0.Add("Yuml", 'Ÿ');
			dictionary_0.Add("circ", 'ˆ');
			dictionary_0.Add("tilde", '\u02dc');
			dictionary_0.Add("ensp", '\u2002');
			dictionary_0.Add("emsp", '\u2003');
			dictionary_0.Add("thinsp", '\u2009');
			dictionary_0.Add("zwnj", '\u200c');
			dictionary_0.Add("zwj", '\u200d');
			dictionary_0.Add("lrm", '\u200e');
			dictionary_0.Add("rlm", '\u200f');
			dictionary_0.Add("ndash", '–');
			dictionary_0.Add("mdash", '—');
			dictionary_0.Add("lsquo", '‘');
			dictionary_0.Add("rsquo", '’');
			dictionary_0.Add("sbquo", '‚');
			dictionary_0.Add("ldquo", '“');
			dictionary_0.Add("rdquo", '”');
			dictionary_0.Add("bdquo", '„');
			dictionary_0.Add("dagger", '†');
			dictionary_0.Add("Dagger", '‡');
			dictionary_0.Add("permil", '‰');
			dictionary_0.Add("lsaquo", '‹');
			dictionary_0.Add("rsaquo", '›');
			dictionary_0.Add("euro", '€');
		}

		private static bool notEncoded(char char_1)
		{
			return char_1 == '!' || char_1 == '\'' || char_1 == '(' || char_1 == ')' || char_1 == '*' || char_1 == '-' || char_1 == '.' || char_1 == '_';
		}

		private static void urlEncode(char char_1, Stream stream_0, bool bool_0)
		{
			if (char_1 > 'ÿ')
			{
				stream_0.WriteByte(37);
				stream_0.WriteByte(117);
				int num = (int)char_1 >> 12;
				stream_0.WriteByte((byte)char_0[num]);
				num = ((int)char_1 >> 8) & 0xF;
				stream_0.WriteByte((byte)char_0[num]);
				num = ((int)char_1 >> 4) & 0xF;
				stream_0.WriteByte((byte)char_0[num]);
				num = char_1 & 0xF;
				stream_0.WriteByte((byte)char_0[num]);
			}
			else if (char_1 > ' ' && notEncoded(char_1))
			{
				stream_0.WriteByte((byte)char_1);
			}
			else if (char_1 == ' ')
			{
				stream_0.WriteByte(43);
			}
			else if (char_1 >= '0' && (char_1 >= 'A' || char_1 <= '9') && (char_1 <= 'Z' || char_1 >= 'a') && char_1 <= 'z')
			{
				stream_0.WriteByte((byte)char_1);
			}
			else
			{
				if (bool_0 && char_1 > '\u007f')
				{
					stream_0.WriteByte(37);
					stream_0.WriteByte(117);
					stream_0.WriteByte(48);
					stream_0.WriteByte(48);
				}
				else
				{
					stream_0.WriteByte(37);
				}
				int num2 = (int)char_1 >> 4;
				stream_0.WriteByte((byte)char_0[num2]);
				num2 = char_1 & 0xF;
				stream_0.WriteByte((byte)char_0[num2]);
			}
		}

		private static void urlPathEncode(char char_1, Stream stream_0)
		{
			if (char_1 >= '!' && char_1 <= '~')
			{
				if (char_1 == ' ')
				{
					stream_0.WriteByte(37);
					stream_0.WriteByte(50);
					stream_0.WriteByte(48);
				}
				else
				{
					stream_0.WriteByte((byte)char_1);
				}
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(char_1.ToString());
			byte[] array = bytes;
			foreach (byte b in array)
			{
				stream_0.WriteByte(37);
				int num = b;
				int num2 = num >> 4;
				stream_0.WriteByte((byte)char_0[num2]);
				num2 = num & 0xF;
				stream_0.WriteByte((byte)char_0[num2]);
			}
		}

		private static void writeCharBytes(char char_1, IList ilist_0, Encoding encoding_0)
		{
			if (char_1 > 'ÿ')
			{
				byte[] bytes = encoding_0.GetBytes(new char[1] { char_1 });
				foreach (byte b in bytes)
				{
					ilist_0.Add(b);
				}
			}
			else
			{
				ilist_0.Add((byte)char_1);
			}
		}

		internal static Uri CreateRequestUrl(string string_0, string string_1, bool bool_0, bool bool_1)
		{
			if (string_0 != null && string_0.Length != 0 && string_1 != null && string_1.Length != 0)
			{
				string text = null;
				string arg = null;
				if (string_0.StartsWith("/"))
				{
					arg = string_0;
				}
				else if (string_0.MaybeUri())
				{
					Uri result;
					if (!Uri.TryCreate(string_0, UriKind.Absolute, out result) || ((!(text = result.Scheme).StartsWith("http") || bool_0) && (!text.StartsWith("ws") || !bool_0)))
					{
						return null;
					}
					string_1 = result.Authority;
					arg = result.PathAndQuery;
				}
				else if (!(string_0 == "*"))
				{
					string_1 = string_0;
				}
				if (text == null)
				{
					text = ((!bool_0) ? "http" : "ws") + ((!bool_1) ? string.Empty : "s");
				}
				int num = string_1.IndexOf(':');
				if (num == -1)
				{
					string_1 = string.Format("{0}:{1}", string_1, (text == "http" || text == "ws") ? 80 : 443);
				}
				string uriString = string.Format("{0}://{1}{2}", text, string_1, arg);
				Uri result2;
				if (!Uri.TryCreate(uriString, UriKind.Absolute, out result2))
				{
					return null;
				}
				return result2;
			}
			return null;
		}

		internal static IPrincipal CreateUser(string string_0, AuthenticationSchemes authenticationSchemes_0, string string_1, string string_2, Func<IIdentity, NetworkCredential> func_0)
		{
			object result;
			if (string_0 != null && string_0.StartsWith(authenticationSchemes_0.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				AuthenticationResponse authenticationResponse = AuthenticationResponse.Parse(string_0);
				if (authenticationResponse == null)
				{
					return null;
				}
				IIdentity identity = authenticationResponse.ToIdentity();
				if (identity == null)
				{
					return null;
				}
				NetworkCredential networkCredential = null;
				try
				{
					networkCredential = func_0(identity);
				}
				catch
				{
				}
				if (networkCredential == null)
				{
					return null;
				}
				bool num;
				if (authenticationSchemes_0 == AuthenticationSchemes.Basic)
				{
					num = ((HttpBasicIdentity)identity).String_0 == networkCredential.String_1;
				}
				else
				{
					if (authenticationSchemes_0 != AuthenticationSchemes.Digest)
					{
						goto IL_008e;
					}
					num = ((HttpDigestIdentity)identity).IsValid(networkCredential.String_1, string_1, string_2, null);
				}
				if (!num)
				{
					goto IL_008e;
				}
				result = new GenericPrincipal(identity, networkCredential.String_2);
				goto IL_008f;
			}
			return null;
			IL_008f:
			return (IPrincipal)result;
			IL_008e:
			result = null;
			goto IL_008f;
		}

		internal static Encoding GetEncoding(string string_0)
		{
			string[] array = string_0.Split(';');
			string[] array2 = array;
			int num = 0;
			string text2;
			while (true)
			{
				if (num < array2.Length)
				{
					string text = array2[num];
					text2 = text.Trim();
					if (text2.StartsWith("charset", StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return Encoding.GetEncoding(text2.GetValue('=', true));
		}

		internal static NameValueCollection InternalParseQueryString(string string_0, Encoding encoding_0)
		{
			int length;
			if (string_0 != null && (length = string_0.Length) != 0 && (length != 1 || string_0[0] != '?'))
			{
				if (string_0[0] == '?')
				{
					string_0 = string_0.Substring(1);
				}
				QueryStringCollection queryStringCollection = new QueryStringCollection();
				string[] array = string_0.Split('&');
				string[] array2 = array;
				foreach (string text in array2)
				{
					int num = text.IndexOf('=');
					if (num > -1)
					{
						string name = UrlDecode(text.Substring(0, num), encoding_0);
						string val = ((text.Length <= num + 1) ? string.Empty : UrlDecode(text.Substring(num + 1), encoding_0));
						queryStringCollection.Add(name, val);
					}
					else
					{
						queryStringCollection.Add(null, UrlDecode(text, encoding_0));
					}
				}
				return queryStringCollection;
			}
			return new NameValueCollection(1);
		}

		internal static string InternalUrlDecode(byte[] byte_0, int int_0, int int_1, Encoding encoding_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = int_1 + int_0;
				for (int i = int_0; i < num; i++)
				{
					if (byte_0[i] == 37 && i + 2 < int_1 && byte_0[i + 1] != 37)
					{
						int @char;
						if (byte_0[i + 1] == 117 && i + 5 < num)
						{
							if (memoryStream.Length > 0L)
							{
								stringBuilder.Append(getChars(memoryStream, encoding_0));
								memoryStream.SetLength(0L);
							}
							@char = getChar(byte_0, i + 2, 4);
							if (@char != -1)
							{
								stringBuilder.Append((char)@char);
								i += 5;
								continue;
							}
						}
						else if ((@char = getChar(byte_0, i + 1, 2)) != -1)
						{
							memoryStream.WriteByte((byte)@char);
							i += 2;
							continue;
						}
					}
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(getChars(memoryStream, encoding_0));
						memoryStream.SetLength(0L);
					}
					if (byte_0[i] == 43)
					{
						stringBuilder.Append(' ');
					}
					else
					{
						stringBuilder.Append((char)byte_0[i]);
					}
				}
				if (memoryStream.Length > 0L)
				{
					stringBuilder.Append(getChars(memoryStream, encoding_0));
				}
			}
			return stringBuilder.ToString();
		}

		internal static byte[] InternalUrlDecodeToBytes(byte[] byte_0, int int_0, int int_1)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = int_0 + int_1;
				for (int i = int_0; i < num; i++)
				{
					char c = (char)byte_0[i];
					switch (c)
					{
					case '+':
						c = ' ';
						break;
					case '%':
						if (i < num - 2)
						{
							int @char = getChar(byte_0, i + 1, 2);
							if (@char != -1)
							{
								c = (char)@char;
								i += 2;
							}
						}
						break;
					}
					memoryStream.WriteByte((byte)c);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		internal static byte[] InternalUrlEncodeToBytes(byte[] byte_0, int int_0, int int_1)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = int_0 + int_1;
				for (int i = int_0; i < num; i++)
				{
					urlEncode((char)byte_0[i], memoryStream, false);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		internal static byte[] InternalUrlEncodeUnicodeToBytes(string string_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				foreach (char char_ in string_0)
				{
					urlEncode(char_, memoryStream, true);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		public static string HtmlAttributeEncode(string string_0)
		{
			if (string_0 != null && string_0.Length != 0 && string_0.Contains('&', '"', '<', '>'))
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < string_0.Length; i++)
				{
					char c = string_0[i];
					object value;
					switch (c)
					{
					case '&':
						value = "&amp;";
						break;
					case '"':
						value = "&quot;";
						break;
					case '<':
						value = "&lt;";
						break;
					case '>':
						value = "&gt;";
						break;
					default:
						value = c.ToString();
						break;
					}
					stringBuilder.Append((string)value);
				}
				return stringBuilder.ToString();
			}
			return string_0;
		}

		public static void HtmlAttributeEncode(string string_0, TextWriter textWriter_0)
		{
			if (textWriter_0 == null)
			{
				throw new ArgumentNullException("output");
			}
			textWriter_0.Write(HtmlAttributeEncode(string_0));
		}

		public static string HtmlDecode(string string_0)
		{
			if (string_0 != null && string_0.Length != 0 && string_0.Contains('&'))
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				int num = 0;
				int num2 = 0;
				bool flag = false;
				foreach (char c in string_0)
				{
					if (num == 0)
					{
						if (c == '&')
						{
							stringBuilder.Append(c);
							num = 1;
						}
						else
						{
							stringBuilder2.Append(c);
						}
						continue;
					}
					if (c == '&')
					{
						num = 1;
						if (flag)
						{
							stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
							flag = false;
						}
						stringBuilder2.Append(stringBuilder.ToString());
						stringBuilder.Length = 0;
						stringBuilder.Append('&');
						continue;
					}
					switch (num)
					{
					case 1:
						if (c == ';')
						{
							num = 0;
							stringBuilder2.Append(stringBuilder.ToString());
							stringBuilder2.Append(c);
							stringBuilder.Length = 0;
						}
						else
						{
							num2 = 0;
							num = ((c == '#') ? 3 : 2);
							stringBuilder.Append(c);
						}
						break;
					case 2:
						stringBuilder.Append(c);
						if (c == ';')
						{
							string text = stringBuilder.ToString();
							Dictionary<string, char> entities = getEntities();
							if (text.Length > 1 && entities.ContainsKey(text.Substring(1, text.Length - 2)))
							{
								text = entities[text.Substring(1, text.Length - 2)].ToString();
							}
							stringBuilder2.Append(text);
							num = 0;
							stringBuilder.Length = 0;
						}
						break;
					case 3:
						if (c == ';')
						{
							if (num2 > 65535)
							{
								stringBuilder2.Append("&#");
								stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
								stringBuilder2.Append(";");
							}
							else
							{
								stringBuilder2.Append((char)num2);
							}
							num = 0;
							stringBuilder.Length = 0;
							flag = false;
						}
						else if (char.IsDigit(c))
						{
							num2 = num2 * 10 + (c - 48);
							flag = true;
						}
						else
						{
							num = 2;
							if (flag)
							{
								stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
								flag = false;
							}
							stringBuilder.Append(c);
						}
						break;
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder2.Append(stringBuilder.ToString());
				}
				else if (flag)
				{
					stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
				}
				return stringBuilder2.ToString();
			}
			return string_0;
		}

		public static void HtmlDecode(string string_0, TextWriter textWriter_0)
		{
			if (textWriter_0 == null)
			{
				throw new ArgumentNullException("output");
			}
			textWriter_0.Write(HtmlDecode(string_0));
		}

		public static string HtmlEncode(string string_0)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				bool flag = false;
				foreach (char c in string_0)
				{
					if (c == '&' || c == '"' || c == '<' || c == '>' || c > '\u009f')
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return string_0;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c2 in string_0)
				{
					if (c2 == '&')
					{
						stringBuilder.Append("&amp;");
					}
					else if (c2 == '"')
					{
						stringBuilder.Append("&quot;");
					}
					else if (c2 == '<')
					{
						stringBuilder.Append("&lt;");
					}
					else if (c2 == '>')
					{
						stringBuilder.Append("&gt;");
					}
					else if (c2 > '\u009f')
					{
						stringBuilder.Append("&#");
						int num = c2;
						stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture));
						stringBuilder.Append(";");
					}
					else
					{
						stringBuilder.Append(c2);
					}
				}
				return stringBuilder.ToString();
			}
			return string_0;
		}

		public static void HtmlEncode(string string_0, TextWriter textWriter_0)
		{
			if (textWriter_0 == null)
			{
				throw new ArgumentNullException("output");
			}
			textWriter_0.Write(HtmlEncode(string_0));
		}

		public static NameValueCollection ParseQueryString(string string_0)
		{
			return ParseQueryString(string_0, Encoding.UTF8);
		}

		public static NameValueCollection ParseQueryString(string string_0, Encoding encoding_0)
		{
			if (string_0 == null)
			{
				throw new ArgumentNullException("query");
			}
			return InternalParseQueryString(string_0, encoding_0 ?? Encoding.UTF8);
		}

		public static string UrlDecode(string string_0)
		{
			return UrlDecode(string_0, Encoding.UTF8);
		}

		public static string UrlDecode(string string_0, Encoding encoding_0)
		{
			if (string_0 != null && string_0.Length != 0 && string_0.Contains('%', '+'))
			{
				if (encoding_0 == null)
				{
					encoding_0 = Encoding.UTF8;
				}
				List<byte> list = new List<byte>();
				int length = string_0.Length;
				for (int i = 0; i < length; i++)
				{
					char c = string_0[i];
					if (c == '%' && i + 2 < length && string_0[i + 1] != '%')
					{
						int @char;
						if (string_0[i + 1] == 'u' && i + 5 < length)
						{
							@char = getChar(string_0, i + 2, 4);
							if (@char != -1)
							{
								writeCharBytes((char)@char, list, encoding_0);
								i += 5;
							}
							else
							{
								writeCharBytes('%', list, encoding_0);
							}
						}
						else if ((@char = getChar(string_0, i + 1, 2)) != -1)
						{
							writeCharBytes((char)@char, list, encoding_0);
							i += 2;
						}
						else
						{
							writeCharBytes('%', list, encoding_0);
						}
					}
					else if (c == '+')
					{
						writeCharBytes(' ', list, encoding_0);
					}
					else
					{
						writeCharBytes(c, list, encoding_0);
					}
				}
				return encoding_0.GetString(list.ToArray());
			}
			return string_0;
		}

		public static string UrlDecode(byte[] byte_0, Encoding encoding_0)
		{
			int int_;
			return (byte_0 == null) ? null : (((int_ = byte_0.Length) != 0) ? InternalUrlDecode(byte_0, 0, int_, encoding_0 ?? Encoding.UTF8) : string.Empty);
		}

		public static string UrlDecode(byte[] byte_0, int int_0, int int_1, Encoding encoding_0)
		{
			if (byte_0 == null)
			{
				return null;
			}
			int num = byte_0.Length;
			if (num != 0 && int_1 != 0)
			{
				if (int_0 >= 0 && int_0 < num)
				{
					if (int_1 >= 0 && int_1 <= num - int_0)
					{
						return InternalUrlDecode(byte_0, int_0, int_1, encoding_0 ?? Encoding.UTF8);
					}
					throw new ArgumentOutOfRangeException("count");
				}
				throw new ArgumentOutOfRangeException("offset");
			}
			return string.Empty;
		}

		public static byte[] UrlDecodeToBytes(byte[] byte_0)
		{
			int int_;
			return (byte_0 == null || (int_ = byte_0.Length) <= 0) ? byte_0 : InternalUrlDecodeToBytes(byte_0, 0, int_);
		}

		public static byte[] UrlDecodeToBytes(string string_0)
		{
			return UrlDecodeToBytes(string_0, Encoding.UTF8);
		}

		public static byte[] UrlDecodeToBytes(string string_0, Encoding encoding_0)
		{
			if (string_0 == null)
			{
				return null;
			}
			if (string_0.Length == 0)
			{
				return new byte[0];
			}
			byte[] bytes = (encoding_0 ?? Encoding.UTF8).GetBytes(string_0);
			return InternalUrlDecodeToBytes(bytes, 0, bytes.Length);
		}

		public static byte[] UrlDecodeToBytes(byte[] byte_0, int int_0, int int_1)
		{
			int num;
			if (byte_0 != null && (num = byte_0.Length) != 0)
			{
				if (int_1 == 0)
				{
					return new byte[0];
				}
				if (int_0 >= 0 && int_0 < num)
				{
					if (int_1 >= 0 && int_1 <= num - int_0)
					{
						return InternalUrlDecodeToBytes(byte_0, int_0, int_1);
					}
					throw new ArgumentOutOfRangeException("count");
				}
				throw new ArgumentOutOfRangeException("offset");
			}
			return byte_0;
		}

		public static string UrlEncode(byte[] byte_0)
		{
			int int_;
			return (byte_0 == null) ? null : (((int_ = byte_0.Length) != 0) ? Encoding.ASCII.GetString(InternalUrlEncodeToBytes(byte_0, 0, int_)) : string.Empty);
		}

		public static string UrlEncode(string string_0)
		{
			return UrlEncode(string_0, Encoding.UTF8);
		}

		public static string UrlEncode(string string_0, Encoding encoding_0)
		{
			int length;
			if (string_0 != null && (length = string_0.Length) != 0)
			{
				bool flag = false;
				foreach (char c in string_0)
				{
					if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !notEncoded(c))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return string_0;
				}
				if (encoding_0 == null)
				{
					encoding_0 = Encoding.UTF8;
				}
				byte[] array = new byte[encoding_0.GetMaxByteCount(length)];
				int bytes = encoding_0.GetBytes(string_0, 0, length, array, 0);
				return Encoding.ASCII.GetString(InternalUrlEncodeToBytes(array, 0, bytes));
			}
			return string_0;
		}

		public static string UrlEncode(byte[] byte_0, int int_0, int int_1)
		{
			byte[] array = UrlEncodeToBytes(byte_0, int_0, int_1);
			return (array == null) ? null : ((array.Length != 0) ? Encoding.ASCII.GetString(array) : string.Empty);
		}

		public static byte[] UrlEncodeToBytes(byte[] byte_0)
		{
			int int_;
			return (byte_0 == null || (int_ = byte_0.Length) <= 0) ? byte_0 : InternalUrlEncodeToBytes(byte_0, 0, int_);
		}

		public static byte[] UrlEncodeToBytes(string string_0)
		{
			return UrlEncodeToBytes(string_0, Encoding.UTF8);
		}

		public static byte[] UrlEncodeToBytes(string string_0, Encoding encoding_0)
		{
			if (string_0 == null)
			{
				return null;
			}
			if (string_0.Length == 0)
			{
				return new byte[0];
			}
			byte[] bytes = (encoding_0 ?? Encoding.UTF8).GetBytes(string_0);
			return InternalUrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		public static byte[] UrlEncodeToBytes(byte[] byte_0, int int_0, int int_1)
		{
			int num;
			if (byte_0 != null && (num = byte_0.Length) != 0)
			{
				if (int_1 == 0)
				{
					return new byte[0];
				}
				if (int_0 >= 0 && int_0 < num)
				{
					if (int_1 >= 0 && int_1 <= num - int_0)
					{
						return InternalUrlEncodeToBytes(byte_0, int_0, int_1);
					}
					throw new ArgumentOutOfRangeException("count");
				}
				throw new ArgumentOutOfRangeException("offset");
			}
			return byte_0;
		}

		public static string UrlEncodeUnicode(string string_0)
		{
			return (string_0 == null || string_0.Length <= 0) ? string_0 : Encoding.ASCII.GetString(InternalUrlEncodeUnicodeToBytes(string_0));
		}

		public static byte[] UrlEncodeUnicodeToBytes(string string_0)
		{
			return (string_0 == null) ? null : ((string_0.Length != 0) ? InternalUrlEncodeUnicodeToBytes(string_0) : new byte[0]);
		}

		public static string UrlPathEncode(string string_0)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					foreach (char char_ in string_0)
					{
						urlPathEncode(char_, memoryStream);
					}
					memoryStream.Close();
					return Encoding.ASCII.GetString(memoryStream.ToArray());
				}
			}
			return string_0;
		}
	}
}
