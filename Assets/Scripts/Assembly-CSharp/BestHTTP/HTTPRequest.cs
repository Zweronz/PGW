using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BestHTTP.Authentication;
using BestHTTP.Cookies;
using BestHTTP.Extensions;
using BestHTTP.Forms;
using BestHTTP.Logger;
using Org.BouncyCastle.Crypto.Tls;
using UnityEngine;

namespace BestHTTP
{
	public sealed class HTTPRequest : IEnumerator, IDisposable, IEnumerator<HTTPRequest>
	{
		internal static readonly byte[] byte_0 = new byte[2] { 13, 10 };

		internal static readonly string[] string_0 = new string[6]
		{
			HTTPMethods.Get.ToString().ToUpper(),
			HTTPMethods.Head.ToString().ToUpper(),
			HTTPMethods.Post.ToString().ToUpper(),
			HTTPMethods.Put.ToString().ToUpper(),
			HTTPMethods.Delete.ToString().ToUpper(),
			HTTPMethods.Patch.ToString().ToUpper()
		};

		public static int int_0 = 1024;

		public OnUploadProgressDelegate onUploadProgressDelegate_0;

		public OnDownloadProgressDelegate onDownloadProgressDelegate_0;

		public OnRequestFinishedDelegate onRequestFinishedDelegate_0;

		private List<Cookie> list_0;

		private OnBeforeRedirectionDelegate onBeforeRedirectionDelegate_0;

		private bool bool_0;

		private bool bool_1;

		private int int_1;

		private bool bool_2;

		private HTTPFormBase httpformBase_0;

		private HTTPFormBase httpformBase_1;

		private Func<HTTPRequest, X509Certificate, X509Chain, bool> func_0;

		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private HTTPMethods httpmethods_0;

		[CompilerGenerated]
		private byte[] byte_1;

		[CompilerGenerated]
		private Stream stream_0;

		[CompilerGenerated]
		private bool bool_3;

		[CompilerGenerated]
		private bool bool_4;

		[CompilerGenerated]
		private OnRequestFinishedDelegate onRequestFinishedDelegate_1;

		[CompilerGenerated]
		private bool bool_5;

		[CompilerGenerated]
		private bool bool_6;

		[CompilerGenerated]
		private Uri uri_1;

		[CompilerGenerated]
		private HTTPResponse httpresponse_0;

		[CompilerGenerated]
		private HTTPResponse httpresponse_1;

		[CompilerGenerated]
		private Exception exception_0;

		[CompilerGenerated]
		private object object_0;

		[CompilerGenerated]
		private Credentials credentials_0;

		[CompilerGenerated]
		private HTTPProxy httpproxy_0;

		[CompilerGenerated]
		private int int_2;

		[CompilerGenerated]
		private bool bool_7;

		[CompilerGenerated]
		private bool bool_8;

		[CompilerGenerated]
		private HTTPFormUsage httpformUsage_0;

		[CompilerGenerated]
		private HTTPRequestStates httprequestStates_0;

		[CompilerGenerated]
		private int int_3;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_1;

		[CompilerGenerated]
		private bool bool_9;

		[CompilerGenerated]
		private int int_4;

		[CompilerGenerated]
		private ICertificateVerifyer icertificateVerifyer_0;

		[CompilerGenerated]
		private SupportedProtocols supportedProtocols_0;

		[CompilerGenerated]
		private int int_5;

		[CompilerGenerated]
		private int int_6;

		[CompilerGenerated]
		private bool bool_10;

		[CompilerGenerated]
		private long long_0;

		[CompilerGenerated]
		private long long_1;

		[CompilerGenerated]
		private bool bool_11;

		[CompilerGenerated]
		private Dictionary<string, List<string>> dictionary_0;

		HTTPRequest IEnumerator<HTTPRequest>.Current
		{
			get
			{
				return this;
			}
		}

		public Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			private set
			{
				uri_0 = value;
			}
		}

		public HTTPMethods HTTPMethods_0
		{
			[CompilerGenerated]
			get
			{
				return httpmethods_0;
			}
			[CompilerGenerated]
			set
			{
				httpmethods_0 = value;
			}
		}

		public byte[] Byte_0
		{
			[CompilerGenerated]
			get
			{
				return byte_1;
			}
			[CompilerGenerated]
			set
			{
				byte_1 = value;
			}
		}

		public Stream Stream_0
		{
			[CompilerGenerated]
			get
			{
				return stream_0;
			}
			[CompilerGenerated]
			set
			{
				stream_0 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_3;
			}
			[CompilerGenerated]
			set
			{
				bool_3 = value;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_4;
			}
			[CompilerGenerated]
			set
			{
				bool_4 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return bool_0;
			}
			set
			{
				if (HTTPRequestStates_0 == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the IsKeepAlive property while processing the request is not supported.");
				}
				bool_0 = value;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_1;
			}
			set
			{
				if (HTTPRequestStates_0 == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the DisableCache property while processing the request is not supported.");
				}
				bool_1 = value;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return bool_2;
			}
			set
			{
				if (HTTPRequestStates_0 == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the UseStreaming property while processing the request is not supported.");
				}
				bool_2 = value;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_1;
			}
			set
			{
				if (HTTPRequestStates_0 == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
				}
				if (value < 1)
				{
					throw new ArgumentException("StreamFragmentSize must be at least 1.");
				}
				int_1 = value;
			}
		}

		public OnRequestFinishedDelegate OnRequestFinishedDelegate_0
		{
			[CompilerGenerated]
			get
			{
				return onRequestFinishedDelegate_1;
			}
			[CompilerGenerated]
			set
			{
				onRequestFinishedDelegate_1 = value;
			}
		}

		public bool Boolean_5
		{
			[CompilerGenerated]
			get
			{
				return bool_5;
			}
			[CompilerGenerated]
			set
			{
				bool_5 = value;
			}
		}

		public bool Boolean_6
		{
			[CompilerGenerated]
			get
			{
				return bool_6;
			}
			[CompilerGenerated]
			internal set
			{
				bool_6 = value;
			}
		}

		public Uri Uri_1
		{
			[CompilerGenerated]
			get
			{
				return uri_1;
			}
			[CompilerGenerated]
			internal set
			{
				uri_1 = value;
			}
		}

		public Uri Uri_2
		{
			get
			{
				return (!Boolean_6) ? Uri_0 : Uri_1;
			}
		}

		public HTTPResponse HTTPResponse_0
		{
			[CompilerGenerated]
			get
			{
				return httpresponse_0;
			}
			[CompilerGenerated]
			internal set
			{
				httpresponse_0 = value;
			}
		}

		public HTTPResponse HTTPResponse_1
		{
			[CompilerGenerated]
			get
			{
				return httpresponse_1;
			}
			[CompilerGenerated]
			internal set
			{
				httpresponse_1 = value;
			}
		}

		public Exception Exception_0
		{
			[CompilerGenerated]
			get
			{
				return exception_0;
			}
			[CompilerGenerated]
			internal set
			{
				exception_0 = value;
			}
		}

		public object Object_0
		{
			[CompilerGenerated]
			get
			{
				return object_0;
			}
			[CompilerGenerated]
			set
			{
				object_0 = value;
			}
		}

		public Credentials Credentials_0
		{
			[CompilerGenerated]
			get
			{
				return credentials_0;
			}
			[CompilerGenerated]
			set
			{
				credentials_0 = value;
			}
		}

		public bool Boolean_7
		{
			get
			{
				return HTTPProxy_0 != null;
			}
		}

		public HTTPProxy HTTPProxy_0
		{
			[CompilerGenerated]
			get
			{
				return httpproxy_0;
			}
			[CompilerGenerated]
			set
			{
				httpproxy_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_2;
			}
			[CompilerGenerated]
			set
			{
				int_2 = value;
			}
		}

		public bool Boolean_8
		{
			[CompilerGenerated]
			get
			{
				return bool_7;
			}
			[CompilerGenerated]
			set
			{
				bool_7 = value;
			}
		}

		public bool Boolean_9
		{
			[CompilerGenerated]
			get
			{
				return bool_8;
			}
			[CompilerGenerated]
			set
			{
				bool_8 = value;
			}
		}

		public List<Cookie> List_0
		{
			get
			{
				if (list_0 == null)
				{
					list_0 = new List<Cookie>();
				}
				return list_0;
			}
			set
			{
				list_0 = value;
			}
		}

		public HTTPFormUsage HTTPFormUsage_0
		{
			[CompilerGenerated]
			get
			{
				return httpformUsage_0;
			}
			[CompilerGenerated]
			set
			{
				httpformUsage_0 = value;
			}
		}

		public HTTPRequestStates HTTPRequestStates_0
		{
			[CompilerGenerated]
			get
			{
				return httprequestStates_0;
			}
			[CompilerGenerated]
			internal set
			{
				httprequestStates_0 = value;
			}
		}

		public int Int32_2
		{
			[CompilerGenerated]
			get
			{
				return int_3;
			}
			[CompilerGenerated]
			internal set
			{
				int_3 = value;
			}
		}

		public TimeSpan TimeSpan_0
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_0;
			}
			[CompilerGenerated]
			set
			{
				timeSpan_0 = value;
			}
		}

		public TimeSpan TimeSpan_1
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_1;
			}
			[CompilerGenerated]
			set
			{
				timeSpan_1 = value;
			}
		}

		public bool Boolean_10
		{
			[CompilerGenerated]
			get
			{
				return bool_9;
			}
			[CompilerGenerated]
			set
			{
				bool_9 = value;
			}
		}

		public int Int32_3
		{
			[CompilerGenerated]
			get
			{
				return int_4;
			}
			[CompilerGenerated]
			set
			{
				int_4 = value;
			}
		}

		public ICertificateVerifyer ICertificateVerifyer_0
		{
			[CompilerGenerated]
			get
			{
				return icertificateVerifyer_0;
			}
			[CompilerGenerated]
			set
			{
				icertificateVerifyer_0 = value;
			}
		}

		public SupportedProtocols SupportedProtocols_0
		{
			[CompilerGenerated]
			get
			{
				return supportedProtocols_0;
			}
			[CompilerGenerated]
			set
			{
				supportedProtocols_0 = value;
			}
		}

		internal int Int32_4
		{
			[CompilerGenerated]
			get
			{
				return int_5;
			}
			[CompilerGenerated]
			set
			{
				int_5 = value;
			}
		}

		internal int Int32_5
		{
			[CompilerGenerated]
			get
			{
				return int_6;
			}
			[CompilerGenerated]
			set
			{
				int_6 = value;
			}
		}

		internal bool Boolean_11
		{
			[CompilerGenerated]
			get
			{
				return bool_10;
			}
			[CompilerGenerated]
			set
			{
				bool_10 = value;
			}
		}

		internal long Int64_0
		{
			get
			{
				if (Stream_0 != null && Boolean_1)
				{
					try
					{
						return Stream_0.Length;
					}
					catch
					{
						return -1L;
					}
				}
				return -1L;
			}
		}

		internal long Int64_1
		{
			[CompilerGenerated]
			get
			{
				return long_0;
			}
			[CompilerGenerated]
			private set
			{
				long_0 = value;
			}
		}

		internal long Int64_2
		{
			[CompilerGenerated]
			get
			{
				return long_1;
			}
			[CompilerGenerated]
			private set
			{
				long_1 = value;
			}
		}

		internal bool Boolean_12
		{
			[CompilerGenerated]
			get
			{
				return bool_11;
			}
			[CompilerGenerated]
			set
			{
				bool_11 = value;
			}
		}

		private Dictionary<string, List<string>> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			set
			{
				dictionary_0 = value;
			}
		}

		public object Current
		{
			get
			{
				return this;
			}
		}

		public event Func<HTTPRequest, X509Certificate, X509Chain, bool> CustomCertificationValidator
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				func_0 = (Func<HTTPRequest, X509Certificate, X509Chain, bool>)Delegate.Combine(func_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				func_0 = (Func<HTTPRequest, X509Certificate, X509Chain, bool>)Delegate.Remove(func_0, value);
			}
		}

		public event OnBeforeRedirectionDelegate OnBeforeRedirection
		{
			add
			{
				onBeforeRedirectionDelegate_0 = (OnBeforeRedirectionDelegate)Delegate.Combine(onBeforeRedirectionDelegate_0, value);
			}
			remove
			{
				onBeforeRedirectionDelegate_0 = (OnBeforeRedirectionDelegate)Delegate.Remove(onBeforeRedirectionDelegate_0, value);
			}
		}

		public HTTPRequest(Uri uri_2)
			: this(uri_2, HTTPMethods.Get, HTTPManager.Boolean_0, HTTPManager.Boolean_1, null)
		{
		}

		public HTTPRequest(Uri uri_2, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
			: this(uri_2, HTTPMethods.Get, HTTPManager.Boolean_0, HTTPManager.Boolean_1, onRequestFinishedDelegate_2)
		{
		}

		public HTTPRequest(Uri uri_2, bool bool_12, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
			: this(uri_2, HTTPMethods.Get, bool_12, HTTPManager.Boolean_1, onRequestFinishedDelegate_2)
		{
		}

		public HTTPRequest(Uri uri_2, bool bool_12, bool bool_13, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
			: this(uri_2, HTTPMethods.Get, bool_12, bool_13, onRequestFinishedDelegate_2)
		{
		}

		public HTTPRequest(Uri uri_2, HTTPMethods httpmethods_1)
			: this(uri_2, httpmethods_1, HTTPManager.Boolean_0, HTTPManager.Boolean_1 || httpmethods_1 != HTTPMethods.Get, null)
		{
		}

		public HTTPRequest(Uri uri_2, HTTPMethods httpmethods_1, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
			: this(uri_2, httpmethods_1, HTTPManager.Boolean_0, HTTPManager.Boolean_1 || httpmethods_1 != HTTPMethods.Get, onRequestFinishedDelegate_2)
		{
		}

		public HTTPRequest(Uri uri_2, HTTPMethods httpmethods_1, bool bool_12, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
			: this(uri_2, httpmethods_1, bool_12, HTTPManager.Boolean_1 || httpmethods_1 != HTTPMethods.Get, onRequestFinishedDelegate_2)
		{
		}

		public HTTPRequest(Uri uri_2, HTTPMethods httpmethods_1, bool bool_12, bool bool_13, OnRequestFinishedDelegate onRequestFinishedDelegate_2)
		{
			Uri_0 = uri_2;
			HTTPMethods_0 = httpmethods_1;
			Boolean_2 = bool_12;
			Boolean_3 = bool_13;
			OnRequestFinishedDelegate_0 = onRequestFinishedDelegate_2;
			Int32_0 = 4096;
			Boolean_5 = httpmethods_1 == HTTPMethods.Post;
			Int32_1 = int.MaxValue;
			Int32_2 = 0;
			Boolean_9 = HTTPManager.Boolean_2;
			Int32_5 = 0;
			Int32_4 = 0;
			Boolean_11 = false;
			HTTPRequestStates_0 = HTTPRequestStates.Initial;
			TimeSpan_0 = HTTPManager.TimeSpan_1;
			TimeSpan_1 = HTTPManager.TimeSpan_2;
			Boolean_10 = false;
			HTTPProxy_0 = HTTPManager.HTTPProxy_0;
			Boolean_1 = true;
			Boolean_0 = true;
			ICertificateVerifyer_0 = HTTPManager.ICertificateVerifyer_0;
			Boolean_8 = HTTPManager.Boolean_4;
		}

		public void AddField(string string_1, string string_2)
		{
			AddField(string_1, string_2, Encoding.UTF8);
		}

		public void AddField(string string_1, string string_2, Encoding encoding_0)
		{
			if (httpformBase_0 == null)
			{
				httpformBase_0 = new HTTPFormBase();
			}
			httpformBase_0.AddField(string_1, string_2, encoding_0);
		}

		public void AddBinaryData(string string_1, byte[] byte_2)
		{
			AddBinaryData(string_1, byte_2, null, null);
		}

		public void AddBinaryData(string string_1, byte[] byte_2, string string_2)
		{
			AddBinaryData(string_1, byte_2, string_2, null);
		}

		public void AddBinaryData(string string_1, byte[] byte_2, string string_2, string string_3)
		{
			if (httpformBase_0 == null)
			{
				httpformBase_0 = new HTTPFormBase();
			}
			httpformBase_0.AddBinaryData(string_1, byte_2, string_2, string_3);
		}

		public void SetFields(WWWForm wwwform_0)
		{
			HTTPFormUsage_0 = HTTPFormUsage.Unity;
			httpformBase_1 = new UnityForm(wwwform_0);
		}

		public void SetForm(HTTPFormBase httpformBase_2)
		{
			httpformBase_1 = httpformBase_2;
		}

		public void ClearForm()
		{
			httpformBase_1 = null;
			httpformBase_0 = null;
		}

		private HTTPFormBase SelectFormImplementation()
		{
			if (httpformBase_1 != null)
			{
				return httpformBase_1;
			}
			if (httpformBase_0 == null)
			{
				return null;
			}
			switch (HTTPFormUsage_0)
			{
			case HTTPFormUsage.Automatic:
				if (!httpformBase_0.Boolean_2 && !httpformBase_0.Boolean_3)
				{
					goto case HTTPFormUsage.UrlEncoded;
				}
				goto case HTTPFormUsage.Multipart;
			case HTTPFormUsage.UrlEncoded:
				httpformBase_1 = new HTTPUrlEncodedForm();
				break;
			case HTTPFormUsage.Multipart:
				httpformBase_1 = new HTTPMultiPartForm();
				break;
			case HTTPFormUsage.Unity:
				httpformBase_1 = new UnityForm();
				break;
			}
			httpformBase_1.CopyFrom(httpformBase_0);
			return httpformBase_1;
		}

		public void AddHeader(string string_1, string string_2)
		{
			if (Dictionary_0 == null)
			{
				Dictionary_0 = new Dictionary<string, List<string>>();
			}
			List<string> value;
			if (!Dictionary_0.TryGetValue(string_1, out value))
			{
				Dictionary_0.Add(string_1, value = new List<string>(1));
			}
			value.Add(string_2);
		}

		public void SetHeader(string string_1, string string_2)
		{
			if (Dictionary_0 == null)
			{
				Dictionary_0 = new Dictionary<string, List<string>>();
			}
			List<string> value;
			if (!Dictionary_0.TryGetValue(string_1, out value))
			{
				Dictionary_0.Add(string_1, value = new List<string>(1));
			}
			value.Clear();
			value.Add(string_2);
		}

		public bool RemoveHeader(string string_1)
		{
			if (Dictionary_0 == null)
			{
				return false;
			}
			return Dictionary_0.Remove(string_1);
		}

		public bool HasHeader(string string_1)
		{
			return Dictionary_0 != null && Dictionary_0.ContainsKey(string_1);
		}

		public string GetFirstHeaderValue(string string_1)
		{
			if (Dictionary_0 == null)
			{
				return null;
			}
			List<string> value = null;
			if (Dictionary_0.TryGetValue(string_1, out value) && value.Count > 0)
			{
				return value[0];
			}
			return null;
		}

		public List<string> GetHeaderValues(string string_1)
		{
			if (Dictionary_0 == null)
			{
				return null;
			}
			List<string> value = null;
			if (Dictionary_0.TryGetValue(string_1, out value) && value.Count > 0)
			{
				return value;
			}
			return null;
		}

		public void RemoveHeaders()
		{
			if (Dictionary_0 != null)
			{
				Dictionary_0.Clear();
			}
		}

		public void SetRangeHeader(int int_7)
		{
			SetHeader("Range", string.Format("bytes={0}-", int_7));
		}

		public void SetRangeHeader(int int_7, int int_8)
		{
			SetHeader("Range", string.Format("bytes={0}-{1}", int_7, int_8));
		}

		private void SendHeaders(BinaryWriter binaryWriter_0)
		{
			if (!HasHeader("Host"))
			{
				SetHeader("Host", Uri_2.Authority);
			}
			if (Boolean_6 && !HasHeader("Referer"))
			{
				AddHeader("Referer", Uri_0.ToString());
			}
			if (!HasHeader("Accept-Encoding"))
			{
				AddHeader("Accept-Encoding", "gzip, identity");
			}
			if (Boolean_7 && !HasHeader("Proxy-Connection"))
			{
				AddHeader("Proxy-Connection", (!Boolean_2) ? "Close" : "Keep-Alive");
			}
			if (!HasHeader("Connection"))
			{
				AddHeader("Connection", (!Boolean_2) ? "Close, TE" : "Keep-Alive, TE");
			}
			if (!HasHeader("TE"))
			{
				AddHeader("TE", "identity");
			}
			if (!HasHeader("User-Agent"))
			{
				AddHeader("User-Agent", "BestHTTP");
			}
			long num = -1L;
			if (Stream_0 == null)
			{
				byte[] entityBody = GetEntityBody();
				num = ((entityBody != null) ? entityBody.Length : 0);
				if (Byte_0 == null && (httpformBase_1 != null || (httpformBase_0 != null && !httpformBase_0.Boolean_0)))
				{
					SelectFormImplementation();
					if (httpformBase_1 != null)
					{
						httpformBase_1.PrepareRequest(this);
					}
				}
			}
			else
			{
				num = Int64_0;
				if (num == -1L)
				{
					SetHeader("Transfer-Encoding", "Chunked");
				}
				if (!HasHeader("Content-Type"))
				{
					SetHeader("Content-Type", "application/octet-stream");
				}
			}
			if (num != -1L && !HasHeader("Content-Length"))
			{
				SetHeader("Content-Length", num.ToString());
			}
			if (Boolean_7 && HTTPProxy_0.Credentials_0 != null)
			{
				switch (HTTPProxy_0.Credentials_0.AuthenticationTypes_0)
				{
				case AuthenticationTypes.Basic:
					SetHeader("Proxy-Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(HTTPProxy_0.Credentials_0.String_0 + ":" + HTTPProxy_0.Credentials_0.String_1)));
					break;
				case AuthenticationTypes.Unknown:
				case AuthenticationTypes.Digest:
				{
					Digest digest = DigestStore.Get(HTTPProxy_0.Uri_0);
					if (digest != null)
					{
						string text = digest.GenerateResponseHeader(this, HTTPProxy_0.Credentials_0);
						if (!string.IsNullOrEmpty(text))
						{
							SetHeader("Proxy-Authorization", text);
						}
					}
					break;
				}
				}
			}
			if (Credentials_0 != null)
			{
				switch (Credentials_0.AuthenticationTypes_0)
				{
				case AuthenticationTypes.Basic:
					SetHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Credentials_0.String_0 + ":" + Credentials_0.String_1)));
					break;
				case AuthenticationTypes.Unknown:
				case AuthenticationTypes.Digest:
				{
					Digest digest2 = DigestStore.Get(Uri_2);
					if (digest2 != null)
					{
						string text2 = digest2.GenerateResponseHeader(this, Credentials_0);
						if (!string.IsNullOrEmpty(text2))
						{
							SetHeader("Authorization", text2);
						}
					}
					break;
				}
				}
			}
			List<Cookie> list = ((!Boolean_9) ? null : CookieJar.Get(Uri_2));
			if (list != null && list.Count != 0)
			{
				if (list_0 != null)
				{
					for (int i = 0; i < list_0.Count; i++)
					{
						Cookie cookie_2 = list_0[i];
						int num2 = list.FindIndex((Cookie cookie_1) => cookie_1.String_0.Equals(cookie_2.String_0));
						if (num2 >= 0)
						{
							list[num2] = cookie_2;
						}
						else
						{
							list.Add(cookie_2);
						}
					}
				}
			}
			else
			{
				list = list_0;
			}
			if (list != null && list.Count > 0)
			{
				bool flag = true;
				string text3 = string.Empty;
				bool flag2 = HTTPProtocolFactory.IsSecureProtocol(Uri_2);
				SupportedProtocols protocolFromUri = HTTPProtocolFactory.GetProtocolFromUri(Uri_2);
				foreach (Cookie item in list)
				{
					if ((!item.Boolean_1 || (item.Boolean_1 && flag2)) && (!item.Boolean_2 || (item.Boolean_2 && protocolFromUri == SupportedProtocols.HTTP)))
					{
						if (!flag)
						{
							text3 += "; ";
						}
						else
						{
							flag = false;
						}
						text3 += item.ToString();
						item.DateTime_1 = DateTime.UtcNow;
					}
				}
				SetHeader("Cookie", text3);
			}
			foreach (KeyValuePair<string, List<string>> item2 in Dictionary_0)
			{
				byte[] aSCIIBytes = (item2.Key + ": ").GetASCIIBytes();
				for (int j = 0; j < item2.Value.Count; j++)
				{
					binaryWriter_0.Write(aSCIIBytes);
					binaryWriter_0.Write(item2.Value[j].GetASCIIBytes());
					binaryWriter_0.Write(byte_0);
				}
			}
		}

		public string DumpHeaders()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter_ = new BinaryWriter(memoryStream))
				{
					SendHeaders(binaryWriter_);
					return memoryStream.ToArray().AsciiToString();
				}
			}
		}

		internal byte[] GetEntityBody()
		{
			if (Byte_0 != null)
			{
				return Byte_0;
			}
			if (httpformBase_1 != null || (httpformBase_0 != null && !httpformBase_0.Boolean_0))
			{
				SelectFormImplementation();
				if (httpformBase_1 != null)
				{
					return httpformBase_1.GetData();
				}
			}
			return null;
		}

		internal void SendOutTo(Stream stream_1)
		{
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream_1);
				string arg = string.Format("{0} {1} HTTP/1.1", string_0[(uint)HTTPMethods_0], (!Boolean_7 || !HTTPProxy_0.Boolean_1) ? Uri_2.PathAndQuery : Uri_2.OriginalString);
				if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.Information)
				{
					HTTPManager.ILogger_0.Information("HTTPRequest", string.Format("Sending request: {0}", arg));
				}
				binaryWriter.Write(arg.GetASCIIBytes());
				binaryWriter.Write(byte_0);
				SendHeaders(binaryWriter);
				binaryWriter.Write(byte_0);
				binaryWriter.Flush();
				byte[] data = Byte_0;
				if (data == null && httpformBase_1 != null)
				{
					data = httpformBase_1.GetData();
				}
				if (data == null && Stream_0 == null)
				{
					return;
				}
				Stream stream = Stream_0;
				if (stream == null)
				{
					stream = new MemoryStream(data, 0, data.Length);
					Int64_2 = data.Length;
				}
				else
				{
					Int64_2 = ((!Boolean_1) ? (-1L) : Int64_0);
				}
				Int64_1 = 0L;
				byte[] array = new byte[int_0];
				int num = 0;
				while ((num = stream.Read(array, 0, array.Length)) > 0)
				{
					if (!Boolean_1)
					{
						binaryWriter.Write(num.ToString("X").GetASCIIBytes());
						binaryWriter.Write(byte_0);
					}
					binaryWriter.Write(array, 0, num);
					if (!Boolean_1)
					{
						binaryWriter.Write(byte_0);
					}
					binaryWriter.Flush();
					Int64_1 += num;
					Boolean_12 = true;
				}
				if (!Boolean_1)
				{
					binaryWriter.Write("0".GetASCIIBytes());
					binaryWriter.Write(byte_0);
					binaryWriter.Write(byte_0);
				}
				binaryWriter.Flush();
				if (Stream_0 == null && stream != null)
				{
					stream.Dispose();
				}
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("HTTPRequest", "SendOutTo", ex);
				throw ex;
			}
			finally
			{
				if (Stream_0 != null && Boolean_0)
				{
					Stream_0.Dispose();
				}
			}
		}

		internal void UpgradeCallback()
		{
			if (HTTPResponse_0 == null || !HTTPResponse_0.Boolean_4)
			{
				return;
			}
			try
			{
				if (onRequestFinishedDelegate_0 != null)
				{
					onRequestFinishedDelegate_0(this, HTTPResponse_0);
				}
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("HTTPRequest", "UpgradeCallback", ex);
			}
		}

		internal void CallCallback()
		{
			try
			{
				if (OnRequestFinishedDelegate_0 != null)
				{
					OnRequestFinishedDelegate_0(this, HTTPResponse_0);
				}
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("HTTPRequest", "CallCallback", ex);
			}
		}

		internal bool CallOnBeforeRedirection(Uri uri_2)
		{
			if (onBeforeRedirectionDelegate_0 != null)
			{
				return onBeforeRedirectionDelegate_0(this, HTTPResponse_0, uri_2);
			}
			return true;
		}

		internal void FinishStreaming()
		{
			if (HTTPResponse_0 != null && Boolean_4)
			{
				HTTPResponse_0.FinishStreaming();
			}
		}

		internal void Prepare()
		{
			if (HTTPFormUsage_0 == HTTPFormUsage.Unity)
			{
				SelectFormImplementation();
			}
		}

		internal bool CallCustomCertificationValidator(X509Certificate x509Certificate_0, X509Chain x509Chain_0)
		{
			if (func_0 != null)
			{
				return func_0(this, x509Certificate_0, x509Chain_0);
			}
			return true;
		}

		public HTTPRequest Send()
		{
			return HTTPManager.SendRequest(this);
		}

		public void Abort()
		{
			lock (HTTPManager.object_0)
			{
				if (HTTPRequestStates_0 >= HTTPRequestStates.Finished)
				{
					HTTPManager.ILogger_0.Warning("HTTPRequest", string.Format("Abort - Already in a state({0}) where no Abort required!", HTTPRequestStates_0.ToString()));
					return;
				}
				HTTPConnection connectionWith = HTTPManager.GetConnectionWith(this);
				if (connectionWith == null)
				{
					if (!HTTPManager.RemoveFromQueue(this))
					{
						HTTPManager.ILogger_0.Warning("HTTPRequest", "Abort - No active connection found with this request! (The request may already finished?)");
					}
					HTTPRequestStates_0 = HTTPRequestStates.Aborted;
				}
				else
				{
					if (HTTPResponse_0 != null && HTTPResponse_0.Boolean_1)
					{
						HTTPResponse_0.Dispose();
					}
					connectionWith.Abort(HTTPConnectionStates.AbortRequested);
				}
			}
		}

		public void Clear()
		{
			ClearForm();
			RemoveHeaders();
		}

		public bool MoveNext()
		{
			return HTTPRequestStates_0 < HTTPRequestStates.Finished;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}
	}
}
