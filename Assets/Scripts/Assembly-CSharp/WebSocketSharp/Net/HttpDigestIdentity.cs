using System.Collections.Specialized;
using System.Security.Principal;

namespace WebSocketSharp.Net
{
	public class HttpDigestIdentity : GenericIdentity
	{
		private NameValueCollection nameValueCollection_0;

		public string String_0
		{
			get
			{
				return nameValueCollection_0["algorithm"];
			}
		}

		public string String_1
		{
			get
			{
				return nameValueCollection_0["cnonce"];
			}
		}

		public string String_2
		{
			get
			{
				return nameValueCollection_0["nc"];
			}
		}

		public string String_3
		{
			get
			{
				return nameValueCollection_0["nonce"];
			}
		}

		public string String_4
		{
			get
			{
				return nameValueCollection_0["opaque"];
			}
		}

		public string String_5
		{
			get
			{
				return nameValueCollection_0["qop"];
			}
		}

		public string String_6
		{
			get
			{
				return nameValueCollection_0["realm"];
			}
		}

		public string String_7
		{
			get
			{
				return nameValueCollection_0["response"];
			}
		}

		public string String_8
		{
			get
			{
				return nameValueCollection_0["uri"];
			}
		}

		internal HttpDigestIdentity(NameValueCollection nameValueCollection_1)
			: base(nameValueCollection_1["username"], "Digest")
		{
			nameValueCollection_0 = nameValueCollection_1;
		}

		internal bool IsValid(string string_0, string string_1, string string_2, string string_3)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(nameValueCollection_0);
			nameValueCollection["password"] = string_0;
			nameValueCollection["realm"] = string_1;
			nameValueCollection["method"] = string_2;
			nameValueCollection["entity"] = string_3;
			return nameValueCollection_0["response"] == AuthenticationResponse.CreateRequestDigest(nameValueCollection);
		}
	}
}
