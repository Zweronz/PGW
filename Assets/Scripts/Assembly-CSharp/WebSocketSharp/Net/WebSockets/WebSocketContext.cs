using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;

namespace WebSocketSharp.Net.WebSockets
{
	public abstract class WebSocketContext
	{
		public abstract CookieCollection CookieCollection_0 { get; }

		public abstract NameValueCollection NameValueCollection_0 { get; }

		public abstract string String_1 { get; }

		public abstract bool Boolean_0 { get; }

		public abstract bool Boolean_1 { get; }

		public abstract bool Boolean_2 { get; }

		public abstract bool Boolean_3 { get; }

		public abstract string String_2 { get; }

		public abstract NameValueCollection NameValueCollection_1 { get; }

		public abstract Uri Uri_0 { get; }

		public abstract string String_3 { get; }

		public abstract IEnumerable<string> Prop_0 { get; }

		public abstract string String_4 { get; }

		public abstract IPEndPoint IPEndPoint_0 { get; }

		public abstract IPrincipal IPrincipal_0 { get; }

		public abstract IPEndPoint IPEndPoint_1 { get; }

		public abstract WebSocket WebSocket_0 { get; }
	}
}
