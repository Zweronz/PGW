using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.JSON;

namespace BestHTTP.SignalR
{
	public sealed class NegotiationData
	{
		public Action<NegotiationData> action_0;

		public Action<NegotiationData, string> action_1;

		private HTTPRequest httprequest_0;

		private IConnection iconnection_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private TimeSpan? nullable_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_1;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_4;

		[CompilerGenerated]
		private TimeSpan timeSpan_2;

		[CompilerGenerated]
		private TimeSpan timeSpan_3;

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

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			private set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			private set
			{
				string_3 = value;
			}
		}

		public TimeSpan? Nullable_0
		{
			[CompilerGenerated]
			get
			{
				return nullable_0;
			}
			[CompilerGenerated]
			private set
			{
				nullable_0 = value;
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

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public string String_4
		{
			[CompilerGenerated]
			get
			{
				return string_4;
			}
			[CompilerGenerated]
			private set
			{
				string_4 = value;
			}
		}

		public TimeSpan TimeSpan_2
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_2;
			}
			[CompilerGenerated]
			private set
			{
				timeSpan_2 = value;
			}
		}

		public TimeSpan TimeSpan_3
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_3;
			}
			[CompilerGenerated]
			private set
			{
				timeSpan_3 = value;
			}
		}

		public NegotiationData(Connection connection_0)
		{
			iconnection_0 = connection_0;
		}

		public void Start()
		{
			httprequest_0 = new HTTPRequest(iconnection_0.BuildUri(RequestTypes.Negotiate), HTTPMethods.Get, true, true, OnNegotiationRequestFinished);
			iconnection_0.PrepareRequest(httprequest_0, RequestTypes.Negotiate);
			httprequest_0.Send();
			HTTPManager.ILogger_0.Information("NegotiationData", "Negotiation request sent");
		}

		public void Abort()
		{
			if (httprequest_0 != null)
			{
				action_0 = null;
				action_1 = null;
				httprequest_0.Abort();
			}
		}

		private void OnNegotiationRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
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
					HTTPManager.ILogger_0.Information("NegotiationData", "Negotiation data arrived: " + httpresponse_0.String_1);
					int num = httpresponse_0.String_1.IndexOf("{");
					if (num < 0)
					{
						RaiseOnError("Invalid negotiation text: " + httpresponse_0.String_1);
						break;
					}
					NegotiationData negotiationData = Parse(httpresponse_0.String_1.Substring(num));
					if (negotiationData == null)
					{
						RaiseOnError("Parsing Negotiation data failed: " + httpresponse_0.String_1);
					}
					else if (action_0 != null)
					{
						action_0(this);
						action_0 = null;
					}
				}
				else
				{
					RaiseOnError(string.Format("Negotiation request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_1.Uri_2));
				}
				break;
			}
		}

		private void RaiseOnError(string string_5)
		{
			HTTPManager.ILogger_0.Error("NegotiationData", "Negotiation request failed with error: " + string_5);
			if (action_1 != null)
			{
				action_1(this, string_5);
				action_1 = null;
			}
		}

		private NegotiationData Parse(string string_5)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = Json.Decode(string_5, ref flag) as Dictionary<string, object>;
			if (!flag)
			{
				return null;
			}
			try
			{
				String_0 = GetString(dictionary, "Url");
				if (dictionary.ContainsKey("webSocketServerUrl"))
				{
					String_1 = GetString(dictionary, "webSocketServerUrl");
				}
				String_2 = Uri.EscapeDataString(GetString(dictionary, "ConnectionToken"));
				String_3 = GetString(dictionary, "ConnectionId");
				if (dictionary.ContainsKey("KeepAliveTimeout"))
				{
					Nullable_0 = TimeSpan.FromSeconds(GetDouble(dictionary, "KeepAliveTimeout"));
				}
				TimeSpan_0 = TimeSpan.FromSeconds(GetDouble(dictionary, "DisconnectTimeout"));
				TimeSpan_1 = TimeSpan.FromSeconds(GetDouble(dictionary, "ConnectionTimeout"));
				Boolean_0 = (bool)Get(dictionary, "TryWebSockets");
				String_4 = GetString(dictionary, "ProtocolVersion");
				TimeSpan_2 = TimeSpan.FromSeconds(GetDouble(dictionary, "TransportConnectTimeout"));
				TimeSpan_3 = TimeSpan.FromSeconds(GetDouble(dictionary, "LongPollDelay"));
				return this;
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("NegotiationData", "Parse", ex);
				return null;
			}
		}

		private static object Get(Dictionary<string, object> dictionary_0, string string_5)
		{
			object value;
			if (!dictionary_0.TryGetValue(string_5, out value))
			{
				throw new Exception(string.Format("Can't get {0} from Negotiation data!", string_5));
			}
			return value;
		}

		private static string GetString(Dictionary<string, object> dictionary_0, string string_5)
		{
			return Get(dictionary_0, string_5) as string;
		}

		private static List<string> GetStringList(Dictionary<string, object> dictionary_0, string string_5)
		{
			List<object> list = Get(dictionary_0, string_5) as List<object>;
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

		private static int GetInt(Dictionary<string, object> dictionary_0, string string_5)
		{
			return (int)(double)Get(dictionary_0, string_5);
		}

		private static double GetDouble(Dictionary<string, object> dictionary_0, string string_5)
		{
			return (double)Get(dictionary_0, string_5);
		}
	}
}
