using System;
using WebSocketSharp.Net;

namespace WebSocketSharp.Server
{
	public class HttpRequestEventArgs : EventArgs
	{
		private HttpListenerRequest httpListenerRequest_0;

		private HttpListenerResponse httpListenerResponse_0;

		public HttpListenerRequest HttpListenerRequest_0
		{
			get
			{
				return httpListenerRequest_0;
			}
		}

		public HttpListenerResponse HttpListenerResponse_0
		{
			get
			{
				return httpListenerResponse_0;
			}
		}

		internal HttpRequestEventArgs(HttpListenerContext httpListenerContext_0)
		{
			httpListenerRequest_0 = httpListenerContext_0.HttpListenerRequest_0;
			httpListenerResponse_0 = httpListenerContext_0.HttpListenerResponse_0;
		}
	}
}
