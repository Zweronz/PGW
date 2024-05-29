using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.JSON;

namespace BestHTTP.SocketIO
{
	public sealed class HandshakeData
	{
		public Action<HandshakeData> action_0;

		public Action<HandshakeData, string> action_1;

		private HTTPRequest httprequest_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private List<string> list_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_1;

		[CompilerGenerated]
		private SocketManager socketManager_0;

		public string String_0
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

		public List<string> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			private set
			{
				list_0 = value;
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
			private set
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
			private set
			{
				timeSpan_1 = value;
			}
		}

		public SocketManager SocketManager_0
		{
			[CompilerGenerated]
			get
			{
				return socketManager_0;
			}
			[CompilerGenerated]
			private set
			{
				socketManager_0 = value;
			}
		}

		public HandshakeData(SocketManager socketManager_1)
		{
			SocketManager_0 = socketManager_1;
		}

		internal void Start()
		{
			if (httprequest_0 == null)
			{
				httprequest_0 = new HTTPRequest(new Uri(string.Format("{0}?EIO={1}&transport=polling&t={2}-{3}{4}&b64=true", SocketManager_0.Uri_0.ToString(), 4, SocketManager_0.UInt32_0, SocketManager_0.UInt64_0++, SocketManager_0.SocketOptions_0.BuildQueryParams())), OnHandshakeCallback);
				httprequest_0.Boolean_3 = true;
				httprequest_0.Send();
				HTTPManager.ILogger_0.Information("HandshakeData", "Handshake request sent");
			}
		}

		internal void Abort()
		{
			if (httprequest_0 != null)
			{
				httprequest_0.Abort();
			}
			httprequest_0 = null;
			action_0 = null;
			action_1 = null;
		}

		private void OnHandshakeCallback(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			httprequest_0 = null;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			default:
				RaiseOnError(httprequest_1.HTTPRequestStates_0.ToString());
				break;
			case HTTPRequestStates.Error:
				RaiseOnError((httprequest_1.Exception_0 == null) ? string.Empty : (httprequest_1.Exception_0.Message + " " + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("HandshakeData", "Handshake data arrived: " + httpresponse_0.String_1);
					int num = httpresponse_0.String_1.IndexOf("{");
					if (num < 0)
					{
						RaiseOnError("Invalid handshake text: " + httpresponse_0.String_1);
						break;
					}
					HandshakeData handshakeData = Parse(httpresponse_0.String_1.Substring(num));
					if (handshakeData == null)
					{
						RaiseOnError("Parsing Handshake data failed: " + httpresponse_0.String_1);
					}
					else if (action_0 != null)
					{
						action_0(this);
						action_0 = null;
					}
				}
				else
				{
					RaiseOnError(string.Format("Handshake request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_1.Uri_2));
				}
				break;
			}
		}

		private void RaiseOnError(string string_1)
		{
			HTTPManager.ILogger_0.Error("HandshakeData", "Handshake request failed with error: " + string_1);
			if (action_1 != null)
			{
				action_1(this, string_1);
				action_1 = null;
			}
		}

		private HandshakeData Parse(string string_1)
		{
			bool bool_ = false;
			Dictionary<string, object> dictionary_ = Json.Decode(string_1, ref bool_) as Dictionary<string, object>;
			if (!bool_)
			{
				return null;
			}
			try
			{
				String_0 = GetString(dictionary_, "sid");
				List_0 = GetStringList(dictionary_, "upgrades");
				TimeSpan_0 = TimeSpan.FromMilliseconds(GetInt(dictionary_, "pingInterval"));
				TimeSpan_1 = TimeSpan.FromMilliseconds(GetInt(dictionary_, "pingTimeout"));
				return this;
			}
			catch
			{
				return null;
			}
		}

		private static object Get(Dictionary<string, object> dictionary_0, string string_1)
		{
			object value;
			if (!dictionary_0.TryGetValue(string_1, out value))
			{
				throw new Exception(string.Format("Can't get {0} from Handshake data!", string_1));
			}
			return value;
		}

		private static string GetString(Dictionary<string, object> dictionary_0, string string_1)
		{
			return Get(dictionary_0, string_1) as string;
		}

		private static List<string> GetStringList(Dictionary<string, object> dictionary_0, string string_1)
		{
			List<object> list = Get(dictionary_0, string_1) as List<object>;
			List<string> list2 = new List<string>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i] as string;
				if (text != null)
				{
					list2.Add(text);
				}
			}
			return list2;
		}

		private static int GetInt(Dictionary<string, object> dictionary_0, string string_1)
		{
			return (int)(double)Get(dictionary_0, string_1);
		}
	}
}
