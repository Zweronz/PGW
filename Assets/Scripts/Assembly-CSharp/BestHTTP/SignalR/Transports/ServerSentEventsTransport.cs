using System;
using System.Runtime.CompilerServices;
using BestHTTP.ServerSentEvents;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	public sealed class ServerSentEventsTransport : PostSendTransportBase
	{
		private EventSource eventSource_0;

		[CompilerGenerated]
		private static OnRetryDelegate onRetryDelegate_0;

		public override bool Boolean_0
		{
			get
			{
				return true;
			}
		}

		public override TransportTypes TransportTypes_0
		{
			get
			{
				return TransportTypes.ServerSentEvents;
			}
		}

		public ServerSentEventsTransport(Connection connection_0)
			: base("serverSentEvents", connection_0)
		{
		}

		public override void Connect()
		{
			if (eventSource_0 != null)
			{
				HTTPManager.ILogger_0.Warning("ServerSentEventsTransport", "Start - EventSource already created!");
				return;
			}
			if (base.TransportStates_0 != TransportStates.Reconnecting)
			{
				base.TransportStates_0 = TransportStates.Connecting;
			}
			RequestTypes type = ((base.TransportStates_0 != TransportStates.Reconnecting) ? RequestTypes.Connect : RequestTypes.Reconnect);
			Uri uri_ = base.IConnection_0.BuildUri(type, this);
			eventSource_0 = new EventSource(uri_);
			eventSource_0.OnOpen += OnEventSourceOpen;
			eventSource_0.OnMessage += OnEventSourceMessage;
			eventSource_0.OnError += OnEventSourceError;
			eventSource_0.OnClosed += OnEventSourceClosed;
			eventSource_0.OnRetry += (EventSource eventSource_1) => false;
			eventSource_0.Open();
		}

		public override void Stop()
		{
			eventSource_0.OnOpen -= OnEventSourceOpen;
			eventSource_0.OnMessage -= OnEventSourceMessage;
			eventSource_0.OnError -= OnEventSourceError;
			eventSource_0.OnClosed -= OnEventSourceClosed;
			eventSource_0.Close();
			eventSource_0 = null;
		}

		protected override void Started()
		{
		}

		public override void Abort()
		{
			base.Abort();
			eventSource_0.Close();
		}

		protected override void Aborted()
		{
			if (base.TransportStates_0 == TransportStates.Closing)
			{
				base.TransportStates_0 = TransportStates.Closed;
			}
		}

		private void OnEventSourceOpen(EventSource eventSource_1)
		{
			HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "OnEventSourceOpen");
		}

		private void OnEventSourceMessage(EventSource eventSource_1, Message message_0)
		{
			if (message_0.String_2.Equals("initialized"))
			{
				OnConnected();
				return;
			}
			IServerMessage serverMessage = TransportBase.Parse(base.IConnection_0.IJsonEncoder_0, message_0.String_2);
			if (serverMessage != null)
			{
				base.IConnection_0.OnMessage(serverMessage);
			}
		}

		private void OnEventSourceError(EventSource eventSource_1, string string_1)
		{
			HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "OnEventSourceError");
			if (base.TransportStates_0 == TransportStates.Reconnecting)
			{
				Connect();
			}
			else if (base.TransportStates_0 != TransportStates.Closed)
			{
				if (base.TransportStates_0 == TransportStates.Closing)
				{
					base.TransportStates_0 = TransportStates.Closed;
				}
				else
				{
					base.IConnection_0.Error(string_1);
				}
			}
		}

		private void OnEventSourceClosed(EventSource eventSource_1)
		{
			HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "OnEventSourceClosed");
			OnEventSourceError(eventSource_1, "EventSource Closed!");
		}
	}
}
