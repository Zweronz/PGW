using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Extensions;
using BestHTTP.Logger;
using BestHTTP.Statistics;
using Org.BouncyCastle.Crypto.Tls;
using UnityEngine;

namespace BestHTTP
{
	public static class HTTPManager
	{
		private static byte byte_0;

		private static HeartbeatManager heartbeatManager_0;

		private static BestHTTP.Logger.ILogger ilogger_0;

		private static Dictionary<string, List<HTTPConnection>> dictionary_0;

		private static List<HTTPConnection> list_0;

		private static List<HTTPConnection> list_1;

		private static List<HTTPConnection> list_2;

		private static List<HTTPRequest> list_3;

		private static bool bool_0;

		internal static object object_0;

		[CompilerGenerated]
		private static bool bool_1;

		[CompilerGenerated]
		private static bool bool_2;

		[CompilerGenerated]
		private static TimeSpan timeSpan_0;

		[CompilerGenerated]
		private static bool bool_3;

		[CompilerGenerated]
		private static uint uint_0;

		[CompilerGenerated]
		private static bool bool_4;

		[CompilerGenerated]
		private static TimeSpan timeSpan_1;

		[CompilerGenerated]
		private static TimeSpan timeSpan_2;

		[CompilerGenerated]
		private static Func<string> func_0;

		[CompilerGenerated]
		private static HTTPProxy httpproxy_0;

		[CompilerGenerated]
		private static ICertificateVerifyer icertificateVerifyer_0;

		[CompilerGenerated]
		private static bool bool_5;

		[CompilerGenerated]
		private static int int_0;

		[CompilerGenerated]
		private static Predicate<HTTPRequest> predicate_0;

		[CompilerGenerated]
		private static Comparison<HTTPRequest> comparison_0;

		public static byte Byte_0
		{
			get
			{
				return byte_0;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MaxConnectionPerServer must be greater than 0!");
				}
				byte_0 = value;
			}
		}

		public static bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			set
			{
				bool_1 = value;
			}
		}

		public static bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			set
			{
				bool_2 = value;
			}
		}

		public static TimeSpan TimeSpan_0
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

		public static bool Boolean_2
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

		public static uint UInt32_0
		{
			[CompilerGenerated]
			get
			{
				return uint_0;
			}
			[CompilerGenerated]
			set
			{
				uint_0 = value;
			}
		}

		public static bool Boolean_3
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

		public static TimeSpan TimeSpan_1
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

		public static TimeSpan TimeSpan_2
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_2;
			}
			[CompilerGenerated]
			set
			{
				timeSpan_2 = value;
			}
		}

		public static Func<string> Func_0
		{
			[CompilerGenerated]
			get
			{
				return func_0;
			}
			[CompilerGenerated]
			set
			{
				func_0 = value;
			}
		}

		public static HTTPProxy HTTPProxy_0
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

		public static HeartbeatManager HeartbeatManager_0
		{
			get
			{
				if (heartbeatManager_0 == null)
				{
					heartbeatManager_0 = new HeartbeatManager();
				}
				return heartbeatManager_0;
			}
		}

		public static BestHTTP.Logger.ILogger ILogger_0
		{
			get
			{
				if (ilogger_0 == null)
				{
					ilogger_0 = new DefaultLogger();
					ilogger_0.Loglevels_0 = Loglevels.None;
				}
				return ilogger_0;
			}
			set
			{
				ilogger_0 = value;
			}
		}

		public static ICertificateVerifyer ICertificateVerifyer_0
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

		public static bool Boolean_4
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

		internal static int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		static HTTPManager()
		{
			dictionary_0 = new Dictionary<string, List<HTTPConnection>>();
			list_0 = new List<HTTPConnection>();
			list_1 = new List<HTTPConnection>();
			list_2 = new List<HTTPConnection>();
			list_3 = new List<HTTPRequest>();
			object_0 = new object();
			Byte_0 = 4;
			Boolean_0 = true;
			Int32_0 = 255;
			TimeSpan_0 = TimeSpan.FromSeconds(30.0);
			Boolean_2 = true;
			UInt32_0 = 10485760u;
			Boolean_3 = false;
			TimeSpan_1 = TimeSpan.FromSeconds(20.0);
			TimeSpan_2 = TimeSpan.FromSeconds(60.0);
			ilogger_0 = new DefaultLogger();
			ICertificateVerifyer_0 = null;
			Boolean_4 = false;
		}

		public static void Setup()
		{
			HTTPUpdateDelegator.CheckInstance();
			HTTPCacheService.CheckSetup();
			CookieJar.SetupFolder();
		}

		public static HTTPRequest SendRequest(string string_0, OnRequestFinishedDelegate onRequestFinishedDelegate_0)
		{
			return SendRequest(new HTTPRequest(new Uri(string_0), HTTPMethods.Get, onRequestFinishedDelegate_0));
		}

		public static HTTPRequest SendRequest(string string_0, HTTPMethods httpmethods_0, OnRequestFinishedDelegate onRequestFinishedDelegate_0)
		{
			return SendRequest(new HTTPRequest(new Uri(string_0), httpmethods_0, onRequestFinishedDelegate_0));
		}

		public static HTTPRequest SendRequest(string string_0, HTTPMethods httpmethods_0, bool bool_6, OnRequestFinishedDelegate onRequestFinishedDelegate_0)
		{
			return SendRequest(new HTTPRequest(new Uri(string_0), httpmethods_0, bool_6, onRequestFinishedDelegate_0));
		}

		public static HTTPRequest SendRequest(string string_0, HTTPMethods httpmethods_0, bool bool_6, bool bool_7, OnRequestFinishedDelegate onRequestFinishedDelegate_0)
		{
			return SendRequest(new HTTPRequest(new Uri(string_0), httpmethods_0, bool_6, bool_7, onRequestFinishedDelegate_0));
		}

		public static HTTPRequest SendRequest(HTTPRequest httprequest_0)
		{
			lock (object_0)
			{
				Setup();
				if (bool_0)
				{
					httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Queued;
					list_3.Add(httprequest_0);
				}
				else
				{
					SendRequestImpl(httprequest_0);
				}
				return httprequest_0;
			}
		}

		public static GeneralStatistics GetGeneralStatistics(StatisticsQueryFlags statisticsQueryFlags_0)
		{
			GeneralStatistics result = default(GeneralStatistics);
			result.statisticsQueryFlags_0 = statisticsQueryFlags_0;
			if ((statisticsQueryFlags_0 & StatisticsQueryFlags.Connections) != 0)
			{
				int num = 0;
				foreach (KeyValuePair<string, List<HTTPConnection>> item in dictionary_0)
				{
					if (item.Value != null)
					{
						num += item.Value.Count;
					}
				}
				result.int_0 = num;
				result.int_1 = list_0.Count;
				result.int_2 = list_1.Count;
				result.int_3 = list_2.Count;
				result.int_4 = list_3.Count;
			}
			if ((statisticsQueryFlags_0 & StatisticsQueryFlags.Cache) != 0)
			{
				result.int_5 = HTTPCacheService.GetCacheEntityCount();
				result.ulong_0 = HTTPCacheService.GetCacheSize();
			}
			if ((statisticsQueryFlags_0 & StatisticsQueryFlags.Cookies) != 0)
			{
				List<Cookie> all = CookieJar.GetAll();
				result.int_6 = all.Count;
				uint num2 = 0u;
				for (int i = 0; i < all.Count; i++)
				{
					num2 += all[i].GuessSize();
				}
				result.uint_0 = num2;
			}
			return result;
		}

		private static void SendRequestImpl(HTTPRequest httprequest_0)
		{
			HTTPConnection httpconnection_2 = FindOrCreateFreeConnection(httprequest_0);
			if (httpconnection_2 != null)
			{
				if (list_0.Find((HTTPConnection httpconnection_1) => httpconnection_1 == httpconnection_2) == null)
				{
					list_0.Add(httpconnection_2);
				}
				list_1.Remove(httpconnection_2);
				httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Processing;
				httprequest_0.Prepare();
				httpconnection_2.Process(httprequest_0);
			}
			else
			{
				httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Queued;
				list_3.Add(httprequest_0);
			}
		}

		private static string GetKeyForRequest(HTTPRequest httprequest_0)
		{
			return ((httprequest_0.HTTPProxy_0 == null) ? string.Empty : new UriBuilder(httprequest_0.HTTPProxy_0.Uri_0.Scheme, httprequest_0.HTTPProxy_0.Uri_0.Host, httprequest_0.HTTPProxy_0.Uri_0.Port).Uri.ToString()) + new UriBuilder(httprequest_0.Uri_2.Scheme, httprequest_0.Uri_2.Host, httprequest_0.Uri_2.Port).Uri.ToString();
		}

		private static HTTPConnection FindOrCreateFreeConnection(HTTPRequest httprequest_0)
		{
			HTTPConnection hTTPConnection = null;
			string keyForRequest = GetKeyForRequest(httprequest_0);
			List<HTTPConnection> value;
			if (dictionary_0.TryGetValue(keyForRequest, out value))
			{
				int num = 0;
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].Boolean_1)
					{
						num++;
					}
				}
				if (num <= Byte_0)
				{
					for (int j = 0; j < value.Count; j++)
					{
						if (hTTPConnection != null)
						{
							break;
						}
						HTTPConnection hTTPConnection2 = value[j];
						if (hTTPConnection2 != null && hTTPConnection2.Boolean_0 && (!hTTPConnection2.Boolean_3 || hTTPConnection2.Uri_0 == null || hTTPConnection2.Uri_0.Host.Equals(httprequest_0.Uri_2.Host, StringComparison.OrdinalIgnoreCase)))
						{
							hTTPConnection = hTTPConnection2;
						}
					}
				}
			}
			else
			{
				dictionary_0.Add(keyForRequest, value = new List<HTTPConnection>(Byte_0));
			}
			if (hTTPConnection == null)
			{
				if (value.Count >= Byte_0)
				{
					return null;
				}
				value.Add(hTTPConnection = new HTTPConnection(keyForRequest));
			}
			return hTTPConnection;
		}

		private static bool CanProcessFromQueue()
		{
			int num = 0;
			while (true)
			{
				if (num < list_3.Count)
				{
					if (FindOrCreateFreeConnection(list_3[num]) != null)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		private static void RecycleConnection(HTTPConnection httpconnection_0)
		{
			httpconnection_0.Recycle();
			list_2.Add(httpconnection_0);
		}

		internal static HTTPConnection GetConnectionWith(HTTPRequest httprequest_0)
		{
			lock (object_0)
			{
				int num = 0;
				HTTPConnection hTTPConnection;
				while (true)
				{
					if (num < list_0.Count)
					{
						hTTPConnection = list_0[num];
						if (hTTPConnection.HTTPRequest_0 == httprequest_0)
						{
							break;
						}
						num++;
						continue;
					}
					return null;
				}
				return hTTPConnection;
			}
		}

		internal static bool RemoveFromQueue(HTTPRequest httprequest_0)
		{
			return list_3.Remove(httprequest_0);
		}

		internal static string GetRootCacheFolder()
		{
			try
			{
				if (Func_0 != null)
				{
					return Func_0();
				}
			}
			catch (Exception ex)
			{
				ILogger_0.Exception("HTTPManager", "GetRootCacheFolder", ex);
			}
			return Application.persistentDataPath;
		}

		public static void OnUpdate()
		{
			lock (object_0)
			{
				bool_0 = true;
				try
				{
					for (int i = 0; i < list_0.Count; i++)
					{
						HTTPConnection hTTPConnection = list_0[i];
						switch (hTTPConnection.HTTPConnectionStates_0)
						{
						case HTTPConnectionStates.Processing:
							hTTPConnection.HandleProgressCallback();
							if (hTTPConnection.HTTPRequest_0.Boolean_4 && hTTPConnection.HTTPRequest_0.HTTPResponse_0 != null && hTTPConnection.HTTPRequest_0.HTTPResponse_0.HasStreamedFragments())
							{
								hTTPConnection.HandleCallback();
							}
							if (((!hTTPConnection.HTTPRequest_0.Boolean_4 && hTTPConnection.HTTPRequest_0.Stream_0 == null) || hTTPConnection.HTTPRequest_0.Boolean_10) && DateTime.UtcNow - hTTPConnection.DateTime_0 > hTTPConnection.HTTPRequest_0.TimeSpan_1)
							{
								hTTPConnection.Abort(HTTPConnectionStates.TimedOut);
							}
							break;
						case HTTPConnectionStates.Redirected:
							SendRequest(hTTPConnection.HTTPRequest_0);
							RecycleConnection(hTTPConnection);
							break;
						case HTTPConnectionStates.Upgraded:
							hTTPConnection.HandleCallback();
							break;
						case HTTPConnectionStates.WaitForProtocolShutdown:
						{
							IProtocol protocol = hTTPConnection.HTTPRequest_0.HTTPResponse_0 as IProtocol;
							if (protocol != null)
							{
								protocol.HandleEvents();
							}
							if (protocol == null || protocol.Boolean_6)
							{
								hTTPConnection.HandleCallback();
								hTTPConnection.Dispose();
								RecycleConnection(hTTPConnection);
							}
							break;
						}
						case HTTPConnectionStates.WaitForRecycle:
							hTTPConnection.HTTPRequest_0.FinishStreaming();
							hTTPConnection.HandleCallback();
							RecycleConnection(hTTPConnection);
							break;
						case HTTPConnectionStates.AbortRequested:
						{
							IProtocol protocol = hTTPConnection.HTTPRequest_0.HTTPResponse_0 as IProtocol;
							if (protocol != null)
							{
								protocol.HandleEvents();
								if (protocol.Boolean_6)
								{
									hTTPConnection.HandleCallback();
									hTTPConnection.Dispose();
									RecycleConnection(hTTPConnection);
								}
							}
							break;
						}
						case HTTPConnectionStates.TimedOut:
							if (DateTime.UtcNow - hTTPConnection.DateTime_1 > TimeSpan.FromMilliseconds(500.0))
							{
								ILogger_0.Information("HTTPManager", "Hard aborting connection becouse of a long waiting TimedOut state");
								hTTPConnection.HTTPRequest_0.HTTPResponse_0 = null;
								hTTPConnection.HTTPRequest_0.HTTPRequestStates_0 = HTTPRequestStates.TimedOut;
								hTTPConnection.HandleCallback();
								RecycleConnection(hTTPConnection);
							}
							break;
						case HTTPConnectionStates.Closed:
							hTTPConnection.HTTPRequest_0.FinishStreaming();
							hTTPConnection.HandleCallback();
							RecycleConnection(hTTPConnection);
							break;
						}
					}
				}
				finally
				{
					bool_0 = false;
				}
				if (list_2.Count > 0)
				{
					for (int j = 0; j < list_2.Count; j++)
					{
						HTTPConnection hTTPConnection2 = list_2[j];
						if (hTTPConnection2.Boolean_0)
						{
							list_0.Remove(hTTPConnection2);
							list_1.Add(hTTPConnection2);
						}
					}
					list_2.Clear();
				}
				if (list_1.Count > 0)
				{
					for (int k = 0; k < list_1.Count; k++)
					{
						HTTPConnection hTTPConnection3 = list_1[k];
						if (hTTPConnection3.Boolean_2)
						{
							List<HTTPConnection> value = null;
							if (dictionary_0.TryGetValue(hTTPConnection3.String_0, out value))
							{
								value.Remove(hTTPConnection3);
							}
							hTTPConnection3.Dispose();
							list_1.RemoveAt(k);
							k--;
						}
					}
				}
				if (CanProcessFromQueue())
				{
					if (list_3.Find((HTTPRequest httprequest_0) => httprequest_0.Int32_3 != 0) != null)
					{
						list_3.Sort((HTTPRequest httprequest_0, HTTPRequest httprequest_1) => httprequest_0.Int32_3 - httprequest_1.Int32_3);
					}
					HTTPRequest[] array = list_3.ToArray();
					list_3.Clear();
					for (int l = 0; l < array.Length; l++)
					{
						SendRequest(array[l]);
					}
				}
			}
			if (heartbeatManager_0 != null)
			{
				heartbeatManager_0.Update();
			}
		}

		internal static void OnQuit()
		{
			lock (object_0)
			{
				HTTPCacheService.SaveLibrary();
				foreach (KeyValuePair<string, List<HTTPConnection>> item in dictionary_0)
				{
					foreach (HTTPConnection item2 in item.Value)
					{
						item2.Dispose();
					}
					item.Value.Clear();
				}
				dictionary_0.Clear();
			}
		}
	}
}
