using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BestHTTP.ServerSentEvents;
using BestHTTP.WebSocket;

namespace BestHTTP
{
	internal static class HTTPProtocolFactory
	{
		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_0;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_1;

		public static HTTPResponse Get(SupportedProtocols supportedProtocols_0, HTTPRequest httprequest_0, Stream stream_0, bool bool_0, bool bool_1)
		{
			switch (supportedProtocols_0)
			{
			default:
				return new HTTPResponse(httprequest_0, stream_0, bool_0, bool_1);
			case SupportedProtocols.ServerSentEvents:
				return new EventSourceResponse(httprequest_0, stream_0, bool_0, bool_1);
			case SupportedProtocols.WebSocket:
				return new WebSocketResponse(httprequest_0, stream_0, bool_0, bool_1);
			}
		}

		public static SupportedProtocols GetProtocolFromUri(Uri uri_0)
		{
			string text = uri_0.Scheme.ToLowerInvariant();
			string text2 = text;
			if (text2 != null)
			{
				if (dictionary_0 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
					dictionary.Add("ws", 0);
					dictionary.Add("wss", 0);
					dictionary_0 = dictionary;
				}
				int value;
				if (dictionary_0.TryGetValue(text2, out value) && value == 0)
				{
					return SupportedProtocols.WebSocket;
				}
			}
			return SupportedProtocols.HTTP;
		}

		public static bool IsSecureProtocol(Uri uri_0)
		{
			string text = uri_0.Scheme.ToLowerInvariant();
			string text2 = text;
			if (text2 != null)
			{
				if (dictionary_1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
					dictionary.Add("https", 0);
					dictionary.Add("wss", 0);
					dictionary_1 = dictionary;
				}
				int value;
				if (dictionary_1.TryGetValue(text2, out value) && value == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
