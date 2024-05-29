using System;

namespace WebSocketSharp
{
	public class ErrorEventArgs : EventArgs
	{
		private Exception exception_0;

		private string string_0;

		public Exception Exception_0
		{
			get
			{
				return exception_0;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		internal ErrorEventArgs(string string_1)
			: this(string_1, null)
		{
		}

		internal ErrorEventArgs(string string_1, Exception exception_1)
		{
			string_0 = string_1;
			exception_0 = exception_1;
		}
	}
}
