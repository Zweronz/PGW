using System;

namespace WebSocketSharp
{
	public class WebSocketException : Exception
	{
		private CloseStatusCode closeStatusCode_0;

		public CloseStatusCode CloseStatusCode_0
		{
			get
			{
				return closeStatusCode_0;
			}
		}

		internal WebSocketException()
			: this(CloseStatusCode.Abnormal, null, null)
		{
		}

		internal WebSocketException(Exception exception_0)
			: this(CloseStatusCode.Abnormal, null, exception_0)
		{
		}

		internal WebSocketException(string string_0)
			: this(CloseStatusCode.Abnormal, string_0, null)
		{
		}

		internal WebSocketException(CloseStatusCode closeStatusCode_1)
			: this(closeStatusCode_1, null, null)
		{
		}

		internal WebSocketException(string string_0, Exception exception_0)
			: this(CloseStatusCode.Abnormal, string_0, exception_0)
		{
		}

		internal WebSocketException(CloseStatusCode closeStatusCode_1, Exception exception_0)
			: this(closeStatusCode_1, null, exception_0)
		{
		}

		internal WebSocketException(CloseStatusCode closeStatusCode_1, string string_0)
			: this(closeStatusCode_1, string_0, null)
		{
		}

		internal WebSocketException(CloseStatusCode closeStatusCode_1, string string_0, Exception exception_0)
			: base(string_0 ?? closeStatusCode_1.GetMessage(), exception_0)
		{
			closeStatusCode_0 = closeStatusCode_1;
		}
	}
}
