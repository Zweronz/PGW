using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	public abstract class TransportBase
	{
		private const int int_0 = 5;

		public TransportStates transportStates_0;

		private OnTransportStateChangedDelegate onTransportStateChangedDelegate_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private IConnection iconnection_0;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			protected set
			{
				string_0 = value;
			}
		}

		public abstract bool Boolean_0 { get; }

		public abstract TransportTypes TransportTypes_0 { get; }

		public IConnection IConnection_0
		{
			[CompilerGenerated]
			get
			{
				return iconnection_0;
			}
			[CompilerGenerated]
			protected set
			{
				iconnection_0 = value;
			}
		}

		public TransportStates TransportStates_0
		{
			get
			{
				return transportStates_0;
			}
			protected set
			{
				TransportStates transportStates = transportStates_0;
				transportStates_0 = value;
				if (onTransportStateChangedDelegate_0 != null)
				{
					onTransportStateChangedDelegate_0(this, transportStates, transportStates_0);
				}
			}
		}

		public event OnTransportStateChangedDelegate OnStateChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onTransportStateChangedDelegate_0 = (OnTransportStateChangedDelegate)Delegate.Combine(onTransportStateChangedDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onTransportStateChangedDelegate_0 = (OnTransportStateChangedDelegate)Delegate.Remove(onTransportStateChangedDelegate_0, value);
			}
		}

		public TransportBase(string string_1, Connection connection_0)
		{
			String_0 = string_1;
			IConnection_0 = connection_0;
			TransportStates_0 = TransportStates.Initial;
		}

		public abstract void Connect();

		public abstract void Stop();

		protected abstract void SendImpl(string string_1);

		protected abstract void Started();

		protected abstract void Aborted();

		protected void OnConnected()
		{
			if (TransportStates_0 != TransportStates.Reconnecting)
			{
				Start();
				return;
			}
			IConnection_0.TransportReconnected();
			Started();
			TransportStates_0 = TransportStates.Started;
		}

		protected void Start()
		{
			HTTPManager.ILogger_0.Information("Transport - " + String_0, "Sending Start Request");
			TransportStates_0 = TransportStates.Starting;
			HTTPRequest hTTPRequest = new HTTPRequest(IConnection_0.BuildUri(RequestTypes.Start, this), HTTPMethods.Get, true, true, OnStartRequestFinished);
			hTTPRequest.Object_0 = 0;
			hTTPRequest.Boolean_5 = true;
			hTTPRequest.TimeSpan_1 = IConnection_0.NegotiationData_0.TimeSpan_1 + TimeSpan.FromSeconds(10.0);
			IConnection_0.PrepareRequest(hTTPRequest, RequestTypes.Start);
			hTTPRequest.Send();
		}

		private void OnStartRequestFinished(HTTPRequest httprequest_0, HTTPResponse httpresponse_0)
		{
			HTTPRequestStates hTTPRequestStates_ = httprequest_0.HTTPRequestStates_0;
			if (hTTPRequestStates_ == HTTPRequestStates.Finished)
			{
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("Transport - " + String_0, "Start - Returned: " + httpresponse_0.String_1);
					string text = IConnection_0.ParseResponse(httpresponse_0.String_1);
					if (text != "started")
					{
						IConnection_0.Error(string.Format("Expected 'started' response, but '{0}' found!", text));
						return;
					}
					TransportStates_0 = TransportStates.Started;
					Started();
					IConnection_0.TransportStarted();
					return;
				}
				HTTPManager.ILogger_0.Warning("Transport - " + String_0, string.Format("Start - request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_0.Uri_2));
			}
			HTTPManager.ILogger_0.Information("Transport - " + String_0, "Start request state: " + httprequest_0.HTTPRequestStates_0);
			int num = (int)httprequest_0.Object_0;
			if (num++ < 5)
			{
				httprequest_0.Object_0 = num;
				httprequest_0.Send();
			}
			else
			{
				IConnection_0.Error("Failed to send Start request.");
			}
		}

		public virtual void Abort()
		{
			if (TransportStates_0 == TransportStates.Started)
			{
				TransportStates_0 = TransportStates.Closing;
				HTTPRequest hTTPRequest = new HTTPRequest(IConnection_0.BuildUri(RequestTypes.Abort, this), HTTPMethods.Get, true, true, OnAbortRequestFinished);
				hTTPRequest.Object_0 = 0;
				hTTPRequest.Boolean_5 = true;
				IConnection_0.PrepareRequest(hTTPRequest, RequestTypes.Abort);
				hTTPRequest.Send();
			}
		}

		protected void AbortFinished()
		{
			TransportStates_0 = TransportStates.Closed;
			IConnection_0.TransportAborted();
			Aborted();
		}

		private void OnAbortRequestFinished(HTTPRequest httprequest_0, HTTPResponse httpresponse_0)
		{
			HTTPRequestStates hTTPRequestStates_ = httprequest_0.HTTPRequestStates_0;
			if (hTTPRequestStates_ == HTTPRequestStates.Finished)
			{
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("Transport - " + String_0, "Abort - Returned: " + httpresponse_0.String_1);
					if (TransportStates_0 == TransportStates.Closing)
					{
						AbortFinished();
					}
					return;
				}
				HTTPManager.ILogger_0.Warning("Transport - " + String_0, string.Format("Abort - Handshake request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_0.Uri_2));
			}
			HTTPManager.ILogger_0.Information("Transport - " + String_0, "Abort request state: " + httprequest_0.HTTPRequestStates_0);
			int num = (int)httprequest_0.Object_0;
			if (num++ < 5)
			{
				httprequest_0.Object_0 = num;
				httprequest_0.Send();
			}
			else
			{
				IConnection_0.Error("Failed to send Abort request!");
			}
		}

		public void Send(string string_1)
		{
			try
			{
				HTTPManager.ILogger_0.Information("Transport - " + String_0, "Sending: " + string_1);
				SendImpl(string_1);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("Transport - " + String_0, "Send", ex);
			}
		}

		public void Reconnect()
		{
			HTTPManager.ILogger_0.Information("Transport - " + String_0, "Reconnecting");
			Stop();
			TransportStates_0 = TransportStates.Reconnecting;
			Connect();
		}

		public static IServerMessage Parse(IJsonEncoder ijsonEncoder_0, string string_1)
		{
			if (string.IsNullOrEmpty(string_1))
			{
				HTTPManager.ILogger_0.Error("MessageFactory", "Parse - called with empty or null string!");
				return null;
			}
			if (string_1.Length == 2 && string_1 == "{}")
			{
				return new KeepAliveMessage();
			}
			IDictionary<string, object> dictionary = null;
			try
			{
				dictionary = ijsonEncoder_0.DecodeMessage(string_1);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("MessageFactory", "Parse - encoder.DecodeMessage", ex);
				return null;
			}
			if (dictionary == null)
			{
				HTTPManager.ILogger_0.Error("MessageFactory", "Parse - Json Decode failed for json string: \"" + string_1 + "\"");
				return null;
			}
			IServerMessage serverMessage = null;
			serverMessage = (dictionary.ContainsKey("C") ? new MultiMessage() : (dictionary.ContainsKey("E") ? ((IServerMessage)new FailureMessage()) : ((IServerMessage)new ResultMessage())));
			serverMessage.Parse(dictionary);
			return serverMessage;
		}
	}
}
