using System;

namespace LitJson
{
	public class JsonException : Exception
	{
		public JsonException()
		{
		}

		internal JsonException(ParserToken parserToken_0)
			: base(string.Format("Invalid token '{0}' in input string", parserToken_0))
		{
		}

		internal JsonException(ParserToken parserToken_0, Exception exception_0)
			: base(string.Format("Invalid token '{0}' in input string", parserToken_0), exception_0)
		{
		}

		internal JsonException(int int_0)
			: base(string.Format("Invalid character '{0}' in input string", (char)int_0))
		{
		}

		internal JsonException(int int_0, Exception exception_0)
			: base(string.Format("Invalid character '{0}' in input string", (char)int_0), exception_0)
		{
		}

		public JsonException(string string_0)
			: base(string_0)
		{
		}

		public JsonException(string string_0, Exception exception_0)
			: base(string_0, exception_0)
		{
		}
	}
}
