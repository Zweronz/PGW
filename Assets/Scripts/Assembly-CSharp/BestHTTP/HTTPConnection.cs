using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using BestHTTP.Authentication;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Extensions;
using BestHTTP.Logger;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Security;
using SocketEx;

namespace BestHTTP
{
	internal sealed class HTTPConnection : IDisposable
	{
		private enum RetryCauses
		{
			None = 0,
			Reconnect = 1,
			Authenticate = 2,
			ProxyAuthenticate = 3
		}

		private TcpClient tcpClient_0;

		private Stream stream_0;

		private DateTime dateTime_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private HTTPConnectionStates httpconnectionStates_0;

		[CompilerGenerated]
		private HTTPRequest httprequest_0;

		[CompilerGenerated]
		private DateTime dateTime_1;

		[CompilerGenerated]
		private DateTime dateTime_2;

		[CompilerGenerated]
		private HTTPProxy httpproxy_0;

		[CompilerGenerated]
		private Uri uri_0;

		internal string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		internal HTTPConnectionStates HTTPConnectionStates_0
		{
			[CompilerGenerated]
			get
			{
				return httpconnectionStates_0;
			}
			[CompilerGenerated]
			private set
			{
				httpconnectionStates_0 = value;
			}
		}

		internal bool Boolean_0
		{
			get
			{
				return HTTPConnectionStates_0 == HTTPConnectionStates.Initial || HTTPConnectionStates_0 == HTTPConnectionStates.Free;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return HTTPConnectionStates_0 > HTTPConnectionStates.Initial && HTTPConnectionStates_0 < HTTPConnectionStates.Free;
			}
		}

		internal HTTPRequest HTTPRequest_0
		{
			[CompilerGenerated]
			get
			{
				return httprequest_0;
			}
			[CompilerGenerated]
			private set
			{
				httprequest_0 = value;
			}
		}

		internal bool Boolean_2
		{
			get
			{
				return Boolean_0 && DateTime.UtcNow - dateTime_0 > HTTPManager.TimeSpan_0;
			}
		}

		internal DateTime DateTime_0
		{
			[CompilerGenerated]
			get
			{
				return dateTime_1;
			}
			[CompilerGenerated]
			private set
			{
				dateTime_1 = value;
			}
		}

		internal DateTime DateTime_1
		{
			[CompilerGenerated]
			get
			{
				return dateTime_2;
			}
			[CompilerGenerated]
			private set
			{
				dateTime_2 = value;
			}
		}

		internal HTTPProxy HTTPProxy_0
		{
			[CompilerGenerated]
			get
			{
				return httpproxy_0;
			}
			[CompilerGenerated]
			private set
			{
				httpproxy_0 = value;
			}
		}

		internal bool Boolean_3
		{
			get
			{
				return HTTPProxy_0 != null;
			}
		}

		internal Uri Uri_0
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

		internal HTTPConnection(string string_1)
		{
			String_0 = string_1;
			HTTPConnectionStates_0 = HTTPConnectionStates.Initial;
			dateTime_0 = DateTime.UtcNow;
		}

		internal void Process(HTTPRequest httprequest_1)
		{
			if (HTTPConnectionStates_0 == HTTPConnectionStates.Processing)
			{
				throw new Exception("Connection already processing a request!");
			}
			DateTime_0 = DateTime.MaxValue;
			HTTPConnectionStates_0 = HTTPConnectionStates.Processing;
			HTTPRequest_0 = httprequest_1;
			new Thread(ThreadFunc).Start();
		}

		internal void Recycle()
		{
			if (HTTPConnectionStates_0 == HTTPConnectionStates.TimedOut)
			{
				dateTime_0 = DateTime.MinValue;
			}
			HTTPConnectionStates_0 = HTTPConnectionStates.Free;
			HTTPRequest_0 = null;
		}

		private void ThreadFunc(object object_0)
		{
			bool flag = false;
			bool flag2 = false;
			RetryCauses retryCauses = RetryCauses.None;
			try
			{
				if (!Boolean_3 && HTTPRequest_0.Boolean_7)
				{
					HTTPProxy_0 = HTTPRequest_0.HTTPProxy_0;
				}
				if (TryLoadAllFromCache())
				{
					return;
				}
				if (tcpClient_0 != null && !tcpClient_0.IsConnected())
				{
					Close();
				}
				while (true)
				{
					if (retryCauses == RetryCauses.Reconnect)
					{
						Close();
						Thread.Sleep(100);
					}
					Uri_0 = HTTPRequest_0.Uri_2;
					retryCauses = RetryCauses.None;
					Connect();
					if (HTTPConnectionStates_0 != HTTPConnectionStates.AbortRequested)
					{
						if (!HTTPRequest_0.Boolean_3)
						{
							HTTPCacheService.SetHeaders(HTTPRequest_0);
						}
						bool flag3 = false;
						try
						{
							HTTPRequest_0.SendOutTo(stream_0);
							flag3 = true;
						}
						catch (Exception ex)
						{
							Close();
							if (HTTPConnectionStates_0 == HTTPConnectionStates.TimedOut)
							{
								throw new Exception("AbortRequested");
							}
							if (flag || HTTPRequest_0.Boolean_5)
							{
								throw ex;
							}
							flag = true;
							retryCauses = RetryCauses.Reconnect;
						}
						if (flag3)
						{
							bool flag4 = Receive();
							if (HTTPConnectionStates_0 == HTTPConnectionStates.TimedOut)
							{
								break;
							}
							if (!flag4 && !flag && !HTTPRequest_0.Boolean_5)
							{
								flag = true;
								retryCauses = RetryCauses.Reconnect;
							}
							if (HTTPRequest_0.HTTPResponse_0 != null)
							{
								switch (HTTPRequest_0.HTTPResponse_0.Int32_2)
								{
								case 407:
									if (HTTPRequest_0.Boolean_7)
									{
										string text2 = DigestStore.FindBest(HTTPRequest_0.HTTPResponse_0.GetHeaderValues("proxy-authenticate"));
										if (!string.IsNullOrEmpty(text2))
										{
											Digest orCreate2 = DigestStore.GetOrCreate(HTTPRequest_0.HTTPProxy_0.Uri_0);
											orCreate2.ParseChallange(text2);
											if (HTTPRequest_0.HTTPProxy_0.Credentials_0 != null && orCreate2.IsUriProtected(HTTPRequest_0.HTTPProxy_0.Uri_0) && (!HTTPRequest_0.HasHeader("Proxy-Authorization") || orCreate2.Boolean_0))
											{
												retryCauses = RetryCauses.ProxyAuthenticate;
											}
										}
									}
									goto default;
								case 401:
								{
									string text = DigestStore.FindBest(HTTPRequest_0.HTTPResponse_0.GetHeaderValues("www-authenticate"));
									if (!string.IsNullOrEmpty(text))
									{
										Digest orCreate = DigestStore.GetOrCreate(HTTPRequest_0.Uri_2);
										orCreate.ParseChallange(text);
										if (HTTPRequest_0.Credentials_0 != null && orCreate.IsUriProtected(HTTPRequest_0.Uri_2) && (!HTTPRequest_0.HasHeader("Authorization") || orCreate.Boolean_0))
										{
											retryCauses = RetryCauses.Authenticate;
										}
									}
									goto default;
								}
								case 301:
								case 302:
								case 307:
								case 308:
									if (HTTPRequest_0.Int32_2 < HTTPRequest_0.Int32_1)
									{
										HTTPRequest_0.Int32_2++;
										string firstHeaderValue = HTTPRequest_0.HTTPResponse_0.GetFirstHeaderValue("location");
										if (string.IsNullOrEmpty(firstHeaderValue))
										{
											throw new MissingFieldException(string.Format("Got redirect status({0}) without 'location' header!", HTTPRequest_0.HTTPResponse_0.Int32_2.ToString()));
										}
										Uri redirectUri = GetRedirectUri(firstHeaderValue);
										if (!HTTPRequest_0.CallOnBeforeRedirection(redirectUri))
										{
											HTTPManager.ILogger_0.Information("HTTPConnection", "OnBeforeRedirection returned False");
										}
										else
										{
											HTTPRequest_0.RemoveHeader("Host");
											HTTPRequest_0.SetHeader("Referer", HTTPRequest_0.Uri_2.ToString());
											HTTPRequest_0.Uri_1 = redirectUri;
											HTTPRequest_0.HTTPResponse_0 = null;
											HTTPRequest_0.Boolean_6 = true;
											flag2 = true;
										}
									}
									goto default;
								default:
									if (HTTPRequest_0.Boolean_9)
									{
										CookieJar.Set(HTTPRequest_0.HTTPResponse_0);
									}
									TryStoreInCache();
									if (HTTPRequest_0.HTTPResponse_0 == null || (!HTTPRequest_0.HTTPResponse_0.Boolean_5 && HTTPRequest_0.HTTPResponse_0.HasHeaderWithValue("connection", "close")))
									{
										Close();
									}
									break;
								}
							}
						}
						if (retryCauses == RetryCauses.None)
						{
							return;
						}
						continue;
					}
					throw new Exception("AbortRequested");
				}
				throw new Exception("AbortRequested");
			}
			catch (TimeoutException exception_)
			{
				HTTPRequest_0.HTTPResponse_0 = null;
				HTTPRequest_0.Exception_0 = exception_;
				HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.ConnectionTimedOut;
				Close();
			}
			catch (Exception exception_2)
			{
				if (HTTPRequest_0 != null)
				{
					if (HTTPRequest_0.Boolean_4)
					{
						HTTPCacheService.DeleteEntity(HTTPRequest_0.Uri_2);
					}
					HTTPRequest_0.HTTPResponse_0 = null;
					switch (HTTPConnectionStates_0)
					{
					default:
						HTTPRequest_0.Exception_0 = exception_2;
						HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.Error;
						break;
					case HTTPConnectionStates.TimedOut:
						HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.TimedOut;
						break;
					case HTTPConnectionStates.AbortRequested:
						HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.Aborted;
						break;
					}
				}
				Close();
			}
			finally
			{
				if (HTTPRequest_0 != null)
				{
					lock (HTTPManager.object_0)
					{
						if (HTTPRequest_0 != null && HTTPRequest_0.HTTPResponse_0 != null && HTTPRequest_0.HTTPResponse_0.Boolean_4)
						{
							HTTPConnectionStates_0 = HTTPConnectionStates.Upgraded;
						}
						else
						{
							HTTPConnectionStates_0 = (flag2 ? HTTPConnectionStates.Redirected : ((tcpClient_0 != null) ? HTTPConnectionStates.WaitForRecycle : HTTPConnectionStates.Closed));
						}
						if (HTTPRequest_0.HTTPRequestStates_0 == HTTPRequestStates.Processing && (HTTPConnectionStates_0 == HTTPConnectionStates.Closed || HTTPConnectionStates_0 == HTTPConnectionStates.WaitForRecycle))
						{
							if (HTTPRequest_0.HTTPResponse_0 != null)
							{
								HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.Finished;
							}
							else
							{
								HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.Error;
							}
						}
						if (HTTPRequest_0.HTTPRequestStates_0 == HTTPRequestStates.ConnectionTimedOut)
						{
							HTTPConnectionStates_0 = HTTPConnectionStates.Closed;
						}
						dateTime_0 = DateTime.UtcNow;
					}
					HTTPCacheService.SaveLibrary();
					CookieJar.Persist();
				}
			}
		}

		private void Connect()
		{
			Uri uri = ((!HTTPRequest_0.Boolean_7) ? HTTPRequest_0.Uri_2 : HTTPRequest_0.HTTPProxy_0.Uri_0);
			if (tcpClient_0 == null)
			{
				tcpClient_0 = new TcpClient();
			}
			if (!tcpClient_0.Connected)
			{
				tcpClient_0.ConnectTimeout = HTTPRequest_0.TimeSpan_0;
				tcpClient_0.Connect(uri.Host, uri.Port);
				if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.Information)
				{
					HTTPManager.ILogger_0.Information("HTTPConnection", "Connected to " + uri.Host + ":" + uri.Port);
				}
			}
			else if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.Information)
			{
				HTTPManager.ILogger_0.Information("HTTPConnection", "Already connected to " + uri.Host + ":" + uri.Port);
			}
			lock (HTTPManager.object_0)
			{
				DateTime_0 = DateTime.UtcNow;
			}
			if (stream_0 != null)
			{
				return;
			}
			bool flag = HTTPProtocolFactory.IsSecureProtocol(HTTPRequest_0.Uri_2);
			if (Boolean_3 && (!HTTPProxy_0.Boolean_0 || (flag && HTTPProxy_0.Boolean_2)))
			{
				stream_0 = tcpClient_0.GetStream();
				BinaryWriter binaryWriter = new BinaryWriter(stream_0);
				bool flag2;
				do
				{
					flag2 = false;
					binaryWriter.SendAsASCII(string.Format("CONNECT {0}:{1} HTTP/1.1", HTTPRequest_0.Uri_2.Host, HTTPRequest_0.Uri_2.Port));
					binaryWriter.Write(HTTPRequest.byte_0);
					binaryWriter.SendAsASCII("Proxy-Connection: Keep-Alive");
					binaryWriter.Write(HTTPRequest.byte_0);
					binaryWriter.SendAsASCII("Connection: Keep-Alive");
					binaryWriter.Write(HTTPRequest.byte_0);
					binaryWriter.SendAsASCII(string.Format("Host: {0}:{1}", HTTPRequest_0.Uri_2.Host, HTTPRequest_0.Uri_2.Port));
					binaryWriter.Write(HTTPRequest.byte_0);
					if (Boolean_3 && HTTPProxy_0.Credentials_0 != null)
					{
						switch (HTTPProxy_0.Credentials_0.AuthenticationTypes_0)
						{
						case AuthenticationTypes.Basic:
							binaryWriter.Write(string.Format("Proxy-Authorization: {0}", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(HTTPProxy_0.Credentials_0.String_0 + ":" + HTTPProxy_0.Credentials_0.String_1))).GetASCIIBytes());
							binaryWriter.Write(HTTPRequest.byte_0);
							break;
						case AuthenticationTypes.Unknown:
						case AuthenticationTypes.Digest:
						{
							Digest digest = DigestStore.Get(HTTPProxy_0.Uri_0);
							if (digest != null)
							{
								string text = digest.GenerateResponseHeader(HTTPRequest_0, HTTPProxy_0.Credentials_0);
								if (!string.IsNullOrEmpty(text))
								{
									binaryWriter.Write(string.Format("Proxy-Authorization: {0}", text).GetASCIIBytes());
									binaryWriter.Write(HTTPRequest.byte_0);
								}
							}
							break;
						}
						}
					}
					binaryWriter.Write(HTTPRequest.byte_0);
					binaryWriter.Flush();
					HTTPRequest_0.HTTPResponse_1 = new HTTPResponse(HTTPRequest_0, stream_0, false, false);
					if (HTTPRequest_0.HTTPResponse_1.Receive())
					{
						if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.Information)
						{
							HTTPManager.ILogger_0.Information("HTTPConnection", "Proxy returned - status code: " + HTTPRequest_0.HTTPResponse_1.Int32_2 + " message: " + HTTPRequest_0.HTTPResponse_1.String_0);
						}
						int int32_ = HTTPRequest_0.HTTPResponse_1.Int32_2;
						if (int32_ != 407)
						{
							if (!HTTPRequest_0.HTTPResponse_1.Boolean_0)
							{
								throw new Exception(string.Format("Proxy returned Status Code: \"{0}\", Message: \"{1}\" and Response: {2}", HTTPRequest_0.HTTPResponse_1.Int32_2, HTTPRequest_0.HTTPResponse_1.String_0, HTTPRequest_0.HTTPResponse_1.String_1));
							}
							continue;
						}
						string text2 = DigestStore.FindBest(HTTPRequest_0.HTTPResponse_1.GetHeaderValues("proxy-authenticate"));
						if (!string.IsNullOrEmpty(text2))
						{
							Digest orCreate = DigestStore.GetOrCreate(HTTPProxy_0.Uri_0);
							orCreate.ParseChallange(text2);
							if (HTTPProxy_0.Credentials_0 != null && orCreate.IsUriProtected(HTTPProxy_0.Uri_0) && (!HTTPRequest_0.HasHeader("Proxy-Authorization") || orCreate.Boolean_0))
							{
								flag2 = true;
							}
						}
						continue;
					}
					throw new Exception("Connection to the Proxy Server failed!");
				}
				while (flag2);
			}
			if (flag)
			{
				if (HTTPRequest_0.Boolean_8)
				{
					TlsClientProtocol tlsClientProtocol = new TlsClientProtocol(tcpClient_0.GetStream(), new SecureRandom());
					List<string> list = new List<string>(1);
					list.Add(HTTPRequest_0.Uri_2.Host);
					Uri uri_ = HTTPRequest_0.Uri_2;
					ICertificateVerifyer verifyer;
					if (HTTPRequest_0.ICertificateVerifyer_0 == null)
					{
						ICertificateVerifyer certificateVerifyer = new AlwaysValidVerifyer();
						verifyer = certificateVerifyer;
					}
					else
					{
						verifyer = HTTPRequest_0.ICertificateVerifyer_0;
					}
					tlsClientProtocol.Connect(new LegacyTlsClient(uri_, verifyer, null, list));
					stream_0 = tlsClientProtocol.Stream;
				}
				else
				{
					SslStream sslStream = new SslStream(tcpClient_0.GetStream(), false, (object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0) => HTTPRequest_0.CallCustomCertificationValidator(x509Certificate_0, x509Chain_0));
					if (!sslStream.IsAuthenticated)
					{
						sslStream.AuthenticateAsClient(HTTPRequest_0.Uri_2.Host);
					}
					stream_0 = sslStream;
				}
			}
			else
			{
				stream_0 = tcpClient_0.GetStream();
			}
		}

		private bool Receive()
		{
			SupportedProtocols supportedProtocols_ = ((HTTPRequest_0.SupportedProtocols_0 != 0) ? HTTPRequest_0.SupportedProtocols_0 : HTTPProtocolFactory.GetProtocolFromUri(HTTPRequest_0.Uri_2));
			HTTPRequest_0.HTTPResponse_0 = HTTPProtocolFactory.Get(supportedProtocols_, HTTPRequest_0, stream_0, HTTPRequest_0.Boolean_4, false);
			if (!HTTPRequest_0.HTTPResponse_0.Receive())
			{
				HTTPRequest_0.HTTPResponse_0 = null;
				return false;
			}
			if (HTTPRequest_0.HTTPResponse_0.Int32_2 == 304)
			{
				int int_;
				using (Stream stream_ = HTTPCacheService.GetBody(HTTPRequest_0.Uri_2, out int_))
				{
					if (!HTTPRequest_0.HTTPResponse_0.HasHeader("content-length"))
					{
						HTTPRequest_0.HTTPResponse_0.Dictionary_0.Add("content-length", new List<string>(1) { int_.ToString() });
					}
					HTTPRequest_0.HTTPResponse_0.Boolean_3 = true;
					HTTPRequest_0.HTTPResponse_0.ReadRaw(stream_, int_);
				}
			}
			return true;
		}

		private bool TryLoadAllFromCache()
		{
			if (!HTTPRequest_0.Boolean_3 && HTTPCacheService.Boolean_0)
			{
				try
				{
					if (HTTPCacheService.IsCachedEntityExpiresInTheFuture(HTTPRequest_0))
					{
						HTTPRequest_0.HTTPResponse_0 = HTTPCacheService.GetFullResponse(HTTPRequest_0);
						if (HTTPRequest_0.HTTPResponse_0 != null)
						{
							return true;
						}
					}
				}
				catch
				{
					HTTPCacheService.DeleteEntity(HTTPRequest_0.Uri_2);
				}
				return false;
			}
			return false;
		}

		private void TryStoreInCache()
		{
			if (!HTTPRequest_0.Boolean_4 && !HTTPRequest_0.Boolean_3 && HTTPRequest_0.HTTPResponse_0 != null && HTTPCacheService.Boolean_0 && HTTPCacheService.IsCacheble(HTTPRequest_0.Uri_2, HTTPRequest_0.HTTPMethods_0, HTTPRequest_0.HTTPResponse_0))
			{
				HTTPCacheService.Store(HTTPRequest_0.Uri_2, HTTPRequest_0.HTTPMethods_0, HTTPRequest_0.HTTPResponse_0);
			}
		}

		private Uri GetRedirectUri(string string_1)
		{
			Uri uri = null;
			try
			{
				return new Uri(string_1);
			}
			catch (UriFormatException)
			{
				Uri uri2 = HTTPRequest_0.Uri_0;
				UriBuilder uriBuilder = new UriBuilder(uri2.Scheme, uri2.Host, uri2.Port, string_1);
				return uriBuilder.Uri;
			}
		}

		internal void HandleProgressCallback()
		{
			if (HTTPRequest_0.onDownloadProgressDelegate_0 != null && HTTPRequest_0.Boolean_11)
			{
				try
				{
					HTTPRequest_0.onDownloadProgressDelegate_0(HTTPRequest_0, HTTPRequest_0.Int32_4, HTTPRequest_0.Int32_5);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("HTTPManager", "HandleProgressCallback - OnProgress", ex);
				}
				HTTPRequest_0.Boolean_11 = false;
			}
			if (HTTPRequest_0.onUploadProgressDelegate_0 != null && HTTPRequest_0.Boolean_12)
			{
				try
				{
					HTTPRequest_0.onUploadProgressDelegate_0(HTTPRequest_0, HTTPRequest_0.Int64_1, HTTPRequest_0.Int64_2);
				}
				catch (Exception ex2)
				{
					HTTPManager.ILogger_0.Exception("HTTPManager", "HandleProgressCallback - OnUploadProgress", ex2);
				}
				HTTPRequest_0.Boolean_12 = false;
			}
		}

		internal void HandleCallback()
		{
			try
			{
				HandleProgressCallback();
				if (HTTPConnectionStates_0 == HTTPConnectionStates.Upgraded)
				{
					if (HTTPRequest_0 != null && HTTPRequest_0.HTTPResponse_0 != null && HTTPRequest_0.HTTPResponse_0.Boolean_4)
					{
						HTTPRequest_0.UpgradeCallback();
					}
					HTTPConnectionStates_0 = HTTPConnectionStates.WaitForProtocolShutdown;
				}
				else
				{
					HTTPRequest_0.CallCallback();
				}
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("HTTPManager", "HandleCallback", ex);
			}
		}

		internal void Abort(HTTPConnectionStates httpconnectionStates_1)
		{
			HTTPConnectionStates_0 = httpconnectionStates_1;
			HTTPConnectionStates hTTPConnectionStates_ = HTTPConnectionStates_0;
			if (hTTPConnectionStates_ == HTTPConnectionStates.TimedOut)
			{
				DateTime_1 = DateTime.UtcNow;
			}
			if (stream_0 != null)
			{
				stream_0.Dispose();
			}
		}

		private void Close()
		{
			Uri_0 = null;
			if (tcpClient_0 == null)
			{
				return;
			}
			try
			{
				tcpClient_0.Close();
			}
			catch
			{
			}
			finally
			{
				stream_0 = null;
				tcpClient_0 = null;
			}
		}

		public void Dispose()
		{
			Close();
		}
	}
}
