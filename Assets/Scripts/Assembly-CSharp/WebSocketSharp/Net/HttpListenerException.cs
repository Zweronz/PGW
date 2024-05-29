using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebSocketSharp.Net
{
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		protected HttpListenerException(SerializationInfo serializationInfo_0, StreamingContext streamingContext_0)
			: base(serializationInfo_0, streamingContext_0)
		{
		}

		public HttpListenerException()
		{
		}

		public HttpListenerException(int int_0)
			: base(int_0)
		{
		}

		public HttpListenerException(int int_0, string string_0)
			: base(int_0, string_0)
		{
		}
	}
}
