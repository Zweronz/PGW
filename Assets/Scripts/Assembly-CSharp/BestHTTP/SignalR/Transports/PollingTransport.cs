using System;
using BestHTTP.Extensions;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	public sealed class PollingTransport : PostSendTransportBase, IHeartbeat
	{
		private DateTime dateTime_0;

		private TimeSpan timeSpan_0;

		private TimeSpan timeSpan_1;

		private HTTPRequest httprequest_0;

		public override bool Boolean_0
		{
			get
			{
				return false;
			}
		}

		public override TransportTypes TransportTypes_0
		{
			get
			{
				return TransportTypes.LongPoll;
			}
		}

		public PollingTransport(Connection connection_0)
			: base("longPolling", connection_0)
		{
			dateTime_0 = DateTime.MinValue;
			timeSpan_1 = connection_0.NegotiationData_0.TimeSpan_1 + TimeSpan.FromSeconds(10.0);
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			TransportStates transportStates = base.TransportStates_0;
			if (transportStates == TransportStates.Started && httprequest_0 == null && DateTime.UtcNow >= dateTime_0 + timeSpan_0 + base.IConnection_0.NegotiationData_0.TimeSpan_3)
			{
				Poll();
			}
		}

		public override void Connect()
		{
			HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "Sending Open Request");
			if (base.TransportStates_0 != TransportStates.Reconnecting)
			{
				base.TransportStates_0 = TransportStates.Connecting;
			}
			RequestTypes type = ((base.TransportStates_0 != TransportStates.Reconnecting) ? RequestTypes.Connect : RequestTypes.Reconnect);
			HTTPRequest hTTPRequest = new HTTPRequest(base.IConnection_0.BuildUri(type, this), HTTPMethods.Get, true, true, OnConnectRequestFinished);
			base.IConnection_0.PrepareRequest(hTTPRequest, type);
			hTTPRequest.Send();
		}

		public override void Stop()
		{
			HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			if (httprequest_0 != null)
			{
				httprequest_0.Abort();
				httprequest_0 = null;
			}
		}

		protected override void Started()
		{
			dateTime_0 = DateTime.UtcNow;
			HTTPManager.HeartbeatManager_0.Subscribe(this);
		}

		protected override void Aborted()
		{
			HTTPManager.HeartbeatManager_0.Unsubscribe(this);
		}

		private void OnConnectRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			string text = string.Empty;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "Connect - Request Finished Successfully! " + httpresponse_0.String_1);
					OnConnected();
					IServerMessage serverMessage = TransportBase.Parse(base.IConnection_0.IJsonEncoder_0, httpresponse_0.String_1);
					if (serverMessage != null)
					{
						base.IConnection_0.OnMessage(serverMessage);
						MultiMessage multiMessage = serverMessage as MultiMessage;
						if (multiMessage != null && multiMessage.Nullable_0.HasValue)
						{
							timeSpan_0 = multiMessage.Nullable_0.Value;
						}
					}
				}
				else
				{
					text = string.Format("Connect - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Connect - Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				text = "Connect - Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Connect - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Connect - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.IConnection_0.Error(text);
			}
		}

		private void OnPollRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			if (httprequest_1.HTTPRequestStates_0 == HTTPRequestStates.Aborted)
			{
				HTTPManager.ILogger_0.Warning("Transport - " + base.String_0, "Poll - Request Aborted!");
				return;
			}
			httprequest_0 = null;
			string text = string.Empty;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "Poll - Request Finished Successfully! " + httpresponse_0.String_1);
					IServerMessage serverMessage = TransportBase.Parse(base.IConnection_0.IJsonEncoder_0, httpresponse_0.String_1);
					if (serverMessage != null)
					{
						base.IConnection_0.OnMessage(serverMessage);
						MultiMessage multiMessage = serverMessage as MultiMessage;
						if (multiMessage != null && multiMessage.Nullable_0.HasValue)
						{
							timeSpan_0 = multiMessage.Nullable_0.Value;
						}
						dateTime_0 = DateTime.UtcNow;
					}
				}
				else
				{
					text = string.Format("Poll - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Poll - Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Poll - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Poll - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.IConnection_0.Error(text);
			}
		}

		private void Poll()
		{
			httprequest_0 = new HTTPRequest(base.IConnection_0.BuildUri(RequestTypes.Poll, this), HTTPMethods.Get, true, true, OnPollRequestFinished);
			base.IConnection_0.PrepareRequest(httprequest_0, RequestTypes.Poll);
			httprequest_0.TimeSpan_1 = timeSpan_1;
			httprequest_0.Send();
		}
	}
}
