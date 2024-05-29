using System;
using System.Collections.Specialized;
using System.Text;

namespace WebSocketSharp.Net
{
	internal abstract class AuthenticationBase
	{
		private AuthenticationSchemes authenticationSchemes_0;

		internal NameValueCollection nameValueCollection_0;

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
				return nameValueCollection_0["nonce"];
			}
		}

		public string String_2
		{
			get
			{
				return nameValueCollection_0["opaque"];
			}
		}

		public string String_3
		{
			get
			{
				return nameValueCollection_0["qop"];
			}
		}

		public string String_4
		{
			get
			{
				return nameValueCollection_0["realm"];
			}
		}

		public AuthenticationSchemes AuthenticationSchemes_0
		{
			get
			{
				return authenticationSchemes_0;
			}
		}

		protected AuthenticationBase(AuthenticationSchemes authenticationSchemes_1, NameValueCollection nameValueCollection_1)
		{
			authenticationSchemes_0 = authenticationSchemes_1;
			nameValueCollection_0 = nameValueCollection_1;
		}

		internal static string CreateNonceValue()
		{
			byte[] array = new byte[16];
			Random random = new Random();
			random.NextBytes(array);
			StringBuilder stringBuilder = new StringBuilder(32);
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		internal static NameValueCollection ParseParameters(string string_0)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string item in string_0.SplitHeaderValue(','))
			{
				int num = item.IndexOf('=');
				string name = ((num <= 0) ? null : item.Substring(0, num).Trim());
				string val = ((num >= 0) ? ((num >= item.Length - 1) ? string.Empty : item.Substring(num + 1).Trim().Trim('"')) : item.Trim().Trim('"'));
				nameValueCollection.Add(name, val);
			}
			return nameValueCollection;
		}

		internal abstract string ToBasicString();

		internal abstract string ToDigestString();

		public override string ToString()
		{
			return (authenticationSchemes_0 == AuthenticationSchemes.Basic) ? ToBasicString() : ((authenticationSchemes_0 != AuthenticationSchemes.Digest) ? string.Empty : ToDigestString());
		}
	}
}
