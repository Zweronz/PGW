using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace WebSocketSharp.Net
{
	internal class AuthenticationResponse : AuthenticationBase
	{
		private uint uint_0;

		[CompilerGenerated]
		private static Func<string, bool> func_0;

		internal uint UInt32_0
		{
			get
			{
				return (uint_0 < uint.MaxValue) ? uint_0 : 0u;
			}
		}

		public string String_5
		{
			get
			{
				return nameValueCollection_0["cnonce"];
			}
		}

		public string String_6
		{
			get
			{
				return nameValueCollection_0["nc"];
			}
		}

		public string String_7
		{
			get
			{
				return nameValueCollection_0["password"];
			}
		}

		public string String_8
		{
			get
			{
				return nameValueCollection_0["response"];
			}
		}

		public string String_9
		{
			get
			{
				return nameValueCollection_0["uri"];
			}
		}

		public string String_10
		{
			get
			{
				return nameValueCollection_0["username"];
			}
		}

		private AuthenticationResponse(AuthenticationSchemes authenticationSchemes_1, NameValueCollection nameValueCollection_1)
			: base(authenticationSchemes_1, nameValueCollection_1)
		{
		}

		internal AuthenticationResponse(NetworkCredential networkCredential_0)
			: this(AuthenticationSchemes.Basic, new NameValueCollection(), networkCredential_0, 0u)
		{
		}

		internal AuthenticationResponse(AuthenticationChallenge authenticationChallenge_0, NetworkCredential networkCredential_0, uint uint_1)
			: this(authenticationChallenge_0.AuthenticationSchemes_0, authenticationChallenge_0.nameValueCollection_0, networkCredential_0, uint_1)
		{
		}

		internal AuthenticationResponse(AuthenticationSchemes authenticationSchemes_1, NameValueCollection nameValueCollection_1, NetworkCredential networkCredential_0, uint uint_1)
			: base(authenticationSchemes_1, nameValueCollection_1)
		{
			nameValueCollection_0["username"] = networkCredential_0.String_3;
			nameValueCollection_0["password"] = networkCredential_0.String_1;
			nameValueCollection_0["uri"] = networkCredential_0.String_0;
			uint_0 = uint_1;
			if (authenticationSchemes_1 == AuthenticationSchemes.Digest)
			{
				initAsDigest();
			}
		}

		private static string createA1(string string_0, string string_1, string string_2)
		{
			return string.Format("{0}:{1}:{2}", string_0, string_2, string_1);
		}

		private static string createA1(string string_0, string string_1, string string_2, string string_3, string string_4)
		{
			return string.Format("{0}:{1}:{2}", hash(createA1(string_0, string_1, string_2)), string_3, string_4);
		}

		private static string createA2(string string_0, string string_1)
		{
			return string.Format("{0}:{1}", string_0, string_1);
		}

		private static string createA2(string string_0, string string_1, string string_2)
		{
			return string.Format("{0}:{1}:{2}", string_0, string_1, hash(string_2));
		}

		private static string hash(string string_0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_0);
			MD5 mD = MD5.Create();
			byte[] array = mD.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(64);
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private void initAsDigest()
		{
			string text = nameValueCollection_0["qop"];
			if (text != null)
			{
				if (text.Split(',').Contains((string string_0) => string_0.Trim().ToLower() == "auth"))
				{
					nameValueCollection_0["qop"] = "auth";
					nameValueCollection_0["cnonce"] = AuthenticationBase.CreateNonceValue();
					nameValueCollection_0["nc"] = string.Format("{0:x8}", ++uint_0);
				}
				else
				{
					nameValueCollection_0["qop"] = null;
				}
			}
			nameValueCollection_0["method"] = "GET";
			nameValueCollection_0["response"] = CreateRequestDigest(nameValueCollection_0);
		}

		internal static string CreateRequestDigest(NameValueCollection nameValueCollection_1)
		{
			string string_ = nameValueCollection_1["username"];
			string string_2 = nameValueCollection_1["password"];
			string string_3 = nameValueCollection_1["realm"];
			string text = nameValueCollection_1["nonce"];
			string string_4 = nameValueCollection_1["uri"];
			string text2 = nameValueCollection_1["algorithm"];
			string text3 = nameValueCollection_1["qop"];
			string text4 = nameValueCollection_1["cnonce"];
			string text5 = nameValueCollection_1["nc"];
			string string_5 = nameValueCollection_1["method"];
			string string_6 = ((text2 == null || !(text2.ToLower() == "md5-sess")) ? createA1(string_, string_2, string_3) : createA1(string_, string_2, string_3, text, text4));
			string string_7 = ((text3 == null || !(text3.ToLower() == "auth-int")) ? createA2(string_5, string_4) : createA2(string_5, string_4, nameValueCollection_1["entity"]));
			string arg = hash(string_6);
			string arg2 = ((text3 == null) ? string.Format("{0}:{1}", text, hash(string_7)) : string.Format("{0}:{1}:{2}:{3}:{4}", text, text5, text4, text3, hash(string_7)));
			return hash(string.Format("{0}:{1}", arg, arg2));
		}

		internal static AuthenticationResponse Parse(string string_0)
		{
			try
			{
				string[] array = string_0.Split(new char[1] { ' ' }, 2);
				if (array.Length != 2)
				{
					return null;
				}
				string text = array[0].ToLower();
				return (text == "basic") ? new AuthenticationResponse(AuthenticationSchemes.Basic, ParseBasicCredentials(array[1])) : ((!(text == "digest")) ? null : new AuthenticationResponse(AuthenticationSchemes.Digest, AuthenticationBase.ParseParameters(array[1])));
			}
			catch
			{
			}
			return null;
		}

		internal static NameValueCollection ParseBasicCredentials(string string_0)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(string_0));
			int num = @string.IndexOf(':');
			string text = @string.Substring(0, num);
			string value = ((num >= @string.Length - 1) ? string.Empty : @string.Substring(num + 1));
			num = text.IndexOf('\\');
			if (num > -1)
			{
				text = text.Substring(num + 1);
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["username"] = text;
			nameValueCollection["password"] = value;
			return nameValueCollection;
		}

		internal override string ToBasicString()
		{
			string s = string.Format("{0}:{1}", nameValueCollection_0["username"], nameValueCollection_0["password"]);
			string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
			return "Basic " + text;
		}

		internal override string ToDigestString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", response=\"{4}\"", nameValueCollection_0["username"], nameValueCollection_0["realm"], nameValueCollection_0["nonce"], nameValueCollection_0["uri"], nameValueCollection_0["response"]);
			string text = nameValueCollection_0["opaque"];
			if (text != null)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text);
			}
			string text2 = nameValueCollection_0["algorithm"];
			if (text2 != null)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text2);
			}
			string text3 = nameValueCollection_0["qop"];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(", qop={0}, cnonce=\"{1}\", nc={2}", text3, nameValueCollection_0["cnonce"], nameValueCollection_0["nc"]);
			}
			return stringBuilder.ToString();
		}

		public IIdentity ToIdentity()
		{
			object result;
			switch (base.AuthenticationSchemes_0)
			{
			case AuthenticationSchemes.Basic:
			{
				IIdentity identity = new HttpBasicIdentity(nameValueCollection_0["username"], nameValueCollection_0["password"]);
				result = identity;
				break;
			}
			case AuthenticationSchemes.Digest:
				result = new HttpDigestIdentity(nameValueCollection_0);
				break;
			default:
				result = null;
				break;
			}
			return (IIdentity)result;
		}
	}
}
