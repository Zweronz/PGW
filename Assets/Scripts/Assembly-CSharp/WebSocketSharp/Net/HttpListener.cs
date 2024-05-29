using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace WebSocketSharp.Net
{
	public sealed class HttpListener : IDisposable
	{
		private AuthenticationSchemes authenticationSchemes_0;

		private Func<HttpListenerRequest, AuthenticationSchemes> func_0;

		private string string_0;

		private Dictionary<HttpConnection, HttpConnection> dictionary_0;

		private object object_0;

		private List<HttpListenerContext> list_0;

		private object object_1;

		private Dictionary<HttpListenerContext, HttpListenerContext> dictionary_1;

		private object object_2;

		private Func<IIdentity, NetworkCredential> func_1;

		private bool bool_0;

		private bool bool_1;

		private bool bool_2;

		private Logger logger_0;

		private HttpListenerPrefixCollection httpListenerPrefixCollection_0;

		private string string_1;

		private bool bool_3;

		private ServerSslConfiguration serverSslConfiguration_0;

		private List<HttpListenerAsyncResult> list_1;

		private object object_3;

		[CompilerGenerated]
		private static Func<IIdentity, NetworkCredential> func_2;

		internal bool Boolean_0
		{
			get
			{
				return bool_0;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return bool_3;
			}
			set
			{
				bool_3 = value;
			}
		}

		public AuthenticationSchemes AuthenticationSchemes_0
		{
			get
			{
				CheckDisposed();
				return authenticationSchemes_0;
			}
			set
			{
				CheckDisposed();
				authenticationSchemes_0 = value;
			}
		}

		public Func<HttpListenerRequest, AuthenticationSchemes> Func_0
		{
			get
			{
				CheckDisposed();
				return func_0;
			}
			set
			{
				CheckDisposed();
				func_0 = value;
			}
		}

		public string String_0
		{
			get
			{
				CheckDisposed();
				return string_0;
			}
			set
			{
				CheckDisposed();
				string_0 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				CheckDisposed();
				return bool_1;
			}
			set
			{
				CheckDisposed();
				bool_1 = value;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_2;
			}
		}

		public static bool Boolean_4
		{
			get
			{
				return true;
			}
		}

		public Logger Logger_0
		{
			get
			{
				return logger_0;
			}
		}

		public HttpListenerPrefixCollection HttpListenerPrefixCollection_0
		{
			get
			{
				CheckDisposed();
				return httpListenerPrefixCollection_0;
			}
		}

		public string String_1
		{
			get
			{
				CheckDisposed();
				return (string_1 == null || string_1.Length <= 0) ? (string_1 = "SECRET AREA") : string_1;
			}
			set
			{
				CheckDisposed();
				string_1 = value;
			}
		}

		public ServerSslConfiguration ServerSslConfiguration_0
		{
			get
			{
				CheckDisposed();
				return serverSslConfiguration_0 ?? (serverSslConfiguration_0 = new ServerSslConfiguration(null));
			}
			set
			{
				CheckDisposed();
				serverSslConfiguration_0 = value;
			}
		}

		public bool Boolean_5
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public Func<IIdentity, NetworkCredential> Func_1
		{
			get
			{
				CheckDisposed();
				return (IIdentity iidentity_0) => null;
			}
			set
			{
				CheckDisposed();
				func_1 = value;
			}
		}

		public HttpListener()
		{
			authenticationSchemes_0 = AuthenticationSchemes.Anonymous;
			dictionary_0 = new Dictionary<HttpConnection, HttpConnection>();
			object_0 = ((ICollection)dictionary_0).SyncRoot;
			list_0 = new List<HttpListenerContext>();
			object_1 = ((ICollection)list_0).SyncRoot;
			dictionary_1 = new Dictionary<HttpListenerContext, HttpListenerContext>();
			object_2 = ((ICollection)dictionary_1).SyncRoot;
			logger_0 = new Logger();
			httpListenerPrefixCollection_0 = new HttpListenerPrefixCollection(this);
			list_1 = new List<HttpListenerAsyncResult>();
			object_3 = ((ICollection)list_1).SyncRoot;
		}

		void IDisposable.Dispose()
		{
			if (!bool_0)
			{
				close(true);
				bool_0 = true;
			}
		}

		private void cleanup(bool bool_4)
		{
			lock (object_2)
			{
				if (!bool_4)
				{
					sendServiceUnavailable();
				}
				cleanupContextRegistry();
				cleanupConnections();
				cleanupWaitQueue();
			}
		}

		private void cleanupConnections()
		{
			lock (object_0)
			{
				if (dictionary_0.Count != 0)
				{
					Dictionary<HttpConnection, HttpConnection>.KeyCollection keys = dictionary_0.Keys;
					HttpConnection[] array = new HttpConnection[keys.Count];
					keys.CopyTo(array, 0);
					dictionary_0.Clear();
					for (int num = array.Length - 1; num >= 0; num--)
					{
						array[num].Close(true);
					}
				}
			}
		}

		private void cleanupContextRegistry()
		{
			lock (object_2)
			{
				if (dictionary_1.Count != 0)
				{
					Dictionary<HttpListenerContext, HttpListenerContext>.KeyCollection keys = dictionary_1.Keys;
					HttpListenerContext[] array = new HttpListenerContext[keys.Count];
					keys.CopyTo(array, 0);
					dictionary_1.Clear();
					for (int num = array.Length - 1; num >= 0; num--)
					{
						array[num].HttpConnection_0.Close(true);
					}
				}
			}
		}

		private void cleanupWaitQueue()
		{
			lock (object_3)
			{
				if (list_1.Count == 0)
				{
					return;
				}
				ObjectDisposedException exception_ = new ObjectDisposedException(GetType().ToString());
				foreach (HttpListenerAsyncResult item in list_1)
				{
					item.Complete(exception_);
				}
				list_1.Clear();
			}
		}

		private void close(bool bool_4)
		{
			EndPointManager.RemoveListener(this);
			cleanup(bool_4);
		}

		private HttpListenerContext getContextFromQueue()
		{
			if (list_0.Count == 0)
			{
				return null;
			}
			HttpListenerContext result = list_0[0];
			list_0.RemoveAt(0);
			return result;
		}

		private void sendServiceUnavailable()
		{
			lock (object_1)
			{
				if (list_0.Count != 0)
				{
					HttpListenerContext[] array = list_0.ToArray();
					list_0.Clear();
					HttpListenerContext[] array2 = array;
					foreach (HttpListenerContext httpListenerContext in array2)
					{
						HttpListenerResponse httpListenerResponse_ = httpListenerContext.HttpListenerResponse_0;
						httpListenerResponse_.Int32_0 = 503;
						httpListenerResponse_.Close();
					}
				}
			}
		}

		internal void AddConnection(HttpConnection httpConnection_0)
		{
			lock (object_0)
			{
				dictionary_0[httpConnection_0] = httpConnection_0;
			}
		}

		internal bool Authenticate(HttpListenerContext httpListenerContext_0)
		{
			AuthenticationSchemes authenticationSchemes = SelectAuthenticationScheme(httpListenerContext_0);
			switch (authenticationSchemes)
			{
			case AuthenticationSchemes.Anonymous:
				return true;
			default:
				httpListenerContext_0.HttpListenerResponse_0.Close(HttpStatusCode.Forbidden);
				return false;
			case AuthenticationSchemes.Digest:
			case AuthenticationSchemes.Basic:
			{
				string text = String_1;
				HttpListenerRequest httpListenerRequest_ = httpListenerContext_0.HttpListenerRequest_0;
				IPrincipal principal = HttpUtility.CreateUser(httpListenerRequest_.NameValueCollection_0["Authorization"], authenticationSchemes, text, httpListenerRequest_.String_2, Func_1);
				if (principal != null && principal.Identity.IsAuthenticated)
				{
					httpListenerContext_0.IPrincipal_0 = principal;
					return true;
				}
				if (authenticationSchemes == AuthenticationSchemes.Basic)
				{
					httpListenerContext_0.HttpListenerResponse_0.CloseWithAuthChallenge(AuthenticationChallenge.CreateBasicChallenge(text).ToBasicString());
				}
				if (authenticationSchemes == AuthenticationSchemes.Digest)
				{
					httpListenerContext_0.HttpListenerResponse_0.CloseWithAuthChallenge(AuthenticationChallenge.CreateDigestChallenge(text).ToDigestString());
				}
				return false;
			}
			}
		}

		internal HttpListenerAsyncResult BeginGetContext(HttpListenerAsyncResult httpListenerAsyncResult_0)
		{
			CheckDisposed();
			if (httpListenerPrefixCollection_0.Count == 0)
			{
				throw new InvalidOperationException("The listener has no URI prefix on which listens.");
			}
			if (!bool_2)
			{
				throw new InvalidOperationException("The listener hasn't been started.");
			}
			lock (object_3)
			{
				lock (object_1)
				{
					HttpListenerContext contextFromQueue = getContextFromQueue();
					if (contextFromQueue != null)
					{
						httpListenerAsyncResult_0.Complete(contextFromQueue, true);
						return httpListenerAsyncResult_0;
					}
				}
				list_1.Add(httpListenerAsyncResult_0);
				return httpListenerAsyncResult_0;
			}
		}

		internal void CheckDisposed()
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
		}

		internal void RegisterContext(HttpListenerContext httpListenerContext_0)
		{
			lock (object_2)
			{
				dictionary_1[httpListenerContext_0] = httpListenerContext_0;
			}
			HttpListenerAsyncResult httpListenerAsyncResult = null;
			lock (object_3)
			{
				if (list_1.Count == 0)
				{
					lock (object_1)
					{
						list_0.Add(httpListenerContext_0);
					}
				}
				else
				{
					httpListenerAsyncResult = list_1[0];
					list_1.RemoveAt(0);
				}
			}
			if (httpListenerAsyncResult != null)
			{
				httpListenerAsyncResult.Complete(httpListenerContext_0);
			}
		}

		internal void RemoveConnection(HttpConnection httpConnection_0)
		{
			lock (object_0)
			{
				dictionary_0.Remove(httpConnection_0);
			}
		}

		internal AuthenticationSchemes SelectAuthenticationScheme(HttpListenerContext httpListenerContext_0)
		{
			return (Func_0 == null) ? authenticationSchemes_0 : Func_0(httpListenerContext_0.HttpListenerRequest_0);
		}

		internal void UnregisterContext(HttpListenerContext httpListenerContext_0)
		{
			lock (object_2)
			{
				dictionary_1.Remove(httpListenerContext_0);
			}
			lock (object_1)
			{
				int num = list_0.IndexOf(httpListenerContext_0);
				if (num >= 0)
				{
					list_0.RemoveAt(num);
				}
			}
		}

		public void Abort()
		{
			if (!bool_0)
			{
				close(true);
				bool_0 = true;
			}
		}

		public IAsyncResult BeginGetContext(AsyncCallback asyncCallback_0, object object_4)
		{
			return BeginGetContext(new HttpListenerAsyncResult(asyncCallback_0, object_4));
		}

		public void Close()
		{
			if (!bool_0)
			{
				close(false);
				bool_0 = true;
			}
		}

		public HttpListenerContext EndGetContext(IAsyncResult iasyncResult_0)
		{
			CheckDisposed();
			if (iasyncResult_0 == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpListenerAsyncResult httpListenerAsyncResult = iasyncResult_0 as HttpListenerAsyncResult;
			if (httpListenerAsyncResult == null)
			{
				throw new ArgumentException("A wrong IAsyncResult.", "asyncResult");
			}
			if (httpListenerAsyncResult.Boolean_0)
			{
				throw new InvalidOperationException("This IAsyncResult cannot be reused.");
			}
			httpListenerAsyncResult.Boolean_0 = true;
			if (!httpListenerAsyncResult.IsCompleted)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.GetContext();
		}

		public HttpListenerContext GetContext()
		{
			HttpListenerAsyncResult httpListenerAsyncResult = BeginGetContext(new HttpListenerAsyncResult(null, null));
			httpListenerAsyncResult.Boolean_1 = true;
			return EndGetContext(httpListenerAsyncResult);
		}

		public void Start()
		{
			CheckDisposed();
			if (!bool_2)
			{
				EndPointManager.AddListener(this);
				bool_2 = true;
			}
		}

		public void Stop()
		{
			CheckDisposed();
			if (bool_2)
			{
				bool_2 = false;
				EndPointManager.RemoveListener(this);
				sendServiceUnavailable();
			}
		}
	}
}
