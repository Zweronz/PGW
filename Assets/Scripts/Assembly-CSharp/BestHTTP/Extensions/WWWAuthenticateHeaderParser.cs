using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.Extensions
{
	public sealed class WWWAuthenticateHeaderParser : KeyValuePairList
	{
		[CompilerGenerated]
		private static Func<char, bool> func_0;

		public WWWAuthenticateHeaderParser(string string_0)
		{
			base.List_0 = ParseQuotedHeader(string_0);
		}

		private List<KeyValuePair> ParseQuotedHeader(string string_0)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (string_0 != null)
			{
				int int_ = 0;
				string string_ = string_0.Read(ref int_, (char char_0) => !char.IsWhiteSpace(char_0) && !char.IsControl(char_0)).TrimAndLower();
				list.Add(new KeyValuePair(string_));
				while (int_ < string_0.Length)
				{
					string string_2 = string_0.Read(ref int_, '=').TrimAndLower();
					KeyValuePair keyValuePair = new KeyValuePair(string_2);
					string_0.SkipWhiteSpace(ref int_);
					keyValuePair.String_1 = string_0.ReadQuotedText(ref int_);
					list.Add(keyValuePair);
				}
			}
			return list;
		}
	}
}
