using System.Collections.Specialized;
using System.Text;

namespace WebSocketSharp.Net
{
	internal class AuthenticationChallenge : AuthenticationBase
	{
		public string String_5
		{
			get
			{
				return nameValueCollection_0["domain"];
			}
		}

		public string String_6
		{
			get
			{
				return nameValueCollection_0["stale"];
			}
		}

		private AuthenticationChallenge(AuthenticationSchemes authenticationSchemes_1, NameValueCollection nameValueCollection_1)
			: base(authenticationSchemes_1, nameValueCollection_1)
		{
		}

		internal AuthenticationChallenge(AuthenticationSchemes authenticationSchemes_1, string string_0)
			: base(authenticationSchemes_1, new NameValueCollection())
		{
			nameValueCollection_0["realm"] = string_0;
			if (authenticationSchemes_1 == AuthenticationSchemes.Digest)
			{
				nameValueCollection_0["nonce"] = AuthenticationBase.CreateNonceValue();
				nameValueCollection_0["algorithm"] = "MD5";
				nameValueCollection_0["qop"] = "auth";
			}
		}

		internal static AuthenticationChallenge CreateBasicChallenge(string string_0)
		{
			return new AuthenticationChallenge(AuthenticationSchemes.Basic, string_0);
		}

		internal static AuthenticationChallenge CreateDigestChallenge(string string_0)
		{
			return new AuthenticationChallenge(AuthenticationSchemes.Digest, string_0);
		}

		internal static AuthenticationChallenge Parse(string string_0)
		{
			string[] array = string_0.Split(new char[1] { ' ' }, 2);
			if (array.Length != 2)
			{
				return null;
			}
			string text = array[0].ToLower();
			return (text == "basic") ? new AuthenticationChallenge(AuthenticationSchemes.Basic, AuthenticationBase.ParseParameters(array[1])) : ((!(text == "digest")) ? null : new AuthenticationChallenge(AuthenticationSchemes.Digest, AuthenticationBase.ParseParameters(array[1])));
		}

		internal override string ToBasicString()
		{
			return string.Format("Basic realm=\"{0}\"", nameValueCollection_0["realm"]);
		}

		internal override string ToDigestString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			string text = nameValueCollection_0["domain"];
			if (text != null)
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", domain=\"{1}\", nonce=\"{2}\"", nameValueCollection_0["realm"], text, nameValueCollection_0["nonce"]);
			}
			else
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", nonce=\"{1}\"", nameValueCollection_0["realm"], nameValueCollection_0["nonce"]);
			}
			string text2 = nameValueCollection_0["opaque"];
			if (text2 != null)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text2);
			}
			string text3 = nameValueCollection_0["stale"];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(", stale={0}", text3);
			}
			string text4 = nameValueCollection_0["algorithm"];
			if (text4 != null)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text4);
			}
			string text5 = nameValueCollection_0["qop"];
			if (text5 != null)
			{
				stringBuilder.AppendFormat(", qop=\"{0}\"", text5);
			}
			return stringBuilder.ToString();
		}
	}
}
