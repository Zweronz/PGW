using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.Extensions;

namespace BestHTTP.ServerSentEvents
{
	public class EventSource : IHeartbeat
	{
		private States states_0;

		private Dictionary<string, OnEventDelegate> dictionary_0;

		private byte byte_0;

		private DateTime dateTime_0;

		private OnGeneralEventDelegate onGeneralEventDelegate_0;

		private OnMessageDelegate onMessageDelegate_0;

		private OnErrorDelegate onErrorDelegate_0;

		private OnRetryDelegate onRetryDelegate_0;

		private OnGeneralEventDelegate onGeneralEventDelegate_1;

		private OnStateChangedDelegate onStateChangedDelegate_0;

		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private HTTPRequest httprequest_0;

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

		public States States_0
		{
			get
			{
				return states_0;
			}
			private set
			{
				States states = states_0;
				states_0 = value;
				if (onStateChangedDelegate_0 != null)
				{
					try
					{
						onStateChangedDelegate_0(this, states, states_0);
					}
					catch (Exception ex)
					{
						HTTPManager.ILogger_0.Exception("EventSource", "OnStateChanged", ex);
					}
				}
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

		public HTTPRequest HTTPRequest_0
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

		public event OnGeneralEventDelegate OnOpen
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onGeneralEventDelegate_0 = (OnGeneralEventDelegate)Delegate.Combine(onGeneralEventDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onGeneralEventDelegate_0 = (OnGeneralEventDelegate)Delegate.Remove(onGeneralEventDelegate_0, value);
			}
		}

		public event OnMessageDelegate OnMessage
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onMessageDelegate_0 = (OnMessageDelegate)Delegate.Combine(onMessageDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onMessageDelegate_0 = (OnMessageDelegate)Delegate.Remove(onMessageDelegate_0, value);
			}
		}

		public event OnErrorDelegate OnError
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onErrorDelegate_0 = (OnErrorDelegate)Delegate.Combine(onErrorDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onErrorDelegate_0 = (OnErrorDelegate)Delegate.Remove(onErrorDelegate_0, value);
			}
		}

		public event OnRetryDelegate OnRetry
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onRetryDelegate_0 = (OnRetryDelegate)Delegate.Combine(onRetryDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onRetryDelegate_0 = (OnRetryDelegate)Delegate.Remove(onRetryDelegate_0, value);
			}
		}

		public event OnGeneralEventDelegate OnClosed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onGeneralEventDelegate_1 = (OnGeneralEventDelegate)Delegate.Combine(onGeneralEventDelegate_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onGeneralEventDelegate_1 = (OnGeneralEventDelegate)Delegate.Remove(onGeneralEventDelegate_1, value);
			}
		}

		public event OnStateChangedDelegate OnStateChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onStateChangedDelegate_0 = (OnStateChangedDelegate)Delegate.Combine(onStateChangedDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onStateChangedDelegate_0 = (OnStateChangedDelegate)Delegate.Remove(onStateChangedDelegate_0, value);
			}
		}

		public EventSource(Uri uri_1)
		{
			Uri_0 = uri_1;
			TimeSpan_0 = TimeSpan.FromMilliseconds(2000.0);
			HTTPRequest_0 = new HTTPRequest(Uri_0, HTTPMethods.Get, false, true, OnRequestFinished);
			HTTPRequest_0.SetHeader("Accept", "text/event-stream");
			HTTPRequest_0.SetHeader("Cache-Control", "no-cache");
			HTTPRequest_0.SetHeader("Accept-Encoding", "identity");
			HTTPRequest_0.SupportedProtocols_0 = SupportedProtocols.ServerSentEvents;
			HTTPRequest_0.onRequestFinishedDelegate_0 = OnUpgraded;
			HTTPRequest_0.Boolean_5 = true;
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			if (States_0 != States.Retrying)
			{
				HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			}
			else if (DateTime.UtcNow - dateTime_0 >= TimeSpan_0)
			{
				Open();
				if (States_0 != States.Connecting)
				{
					SetClosed("OnHeartbeatUpdate");
				}
				HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			}
		}

		public void Open()
		{
			if (States_0 == States.Initial || States_0 == States.Retrying || States_0 == States.Closed)
			{
				States_0 = States.Connecting;
				if (!string.IsNullOrEmpty(String_0))
				{
					HTTPRequest_0.SetHeader("Last-Event-ID", String_0);
				}
				HTTPRequest_0.Send();
			}
		}

		public void Close()
		{
			if (States_0 != States.Closing && States_0 != States.Closed)
			{
				States_0 = States.Closing;
				if (HTTPRequest_0 != null)
				{
					HTTPRequest_0.Abort();
				}
				else
				{
					States_0 = States.Closed;
				}
			}
		}

		public void On(string string_1, OnEventDelegate onEventDelegate_0)
		{
			if (dictionary_0 == null)
			{
				dictionary_0 = new Dictionary<string, OnEventDelegate>();
			}
			dictionary_0[string_1] = onEventDelegate_0;
		}

		public void Off(string string_1)
		{
			if (string_1 != null)
			{
				dictionary_0.Remove(string_1);
			}
		}

		private void CallOnError(string string_1, string string_2)
		{
			if (onErrorDelegate_0 != null)
			{
				try
				{
					onErrorDelegate_0(this, string_1);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("EventSource", string_2 + " - OnError", ex);
				}
			}
		}

		private bool CallOnRetry()
		{
			if (onRetryDelegate_0 != null)
			{
				try
				{
					return onRetryDelegate_0(this);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("EventSource", "CallOnRetry", ex);
				}
			}
			return true;
		}

		private void SetClosed(string string_1)
		{
			States_0 = States.Closed;
			if (onGeneralEventDelegate_1 != null)
			{
				try
				{
					onGeneralEventDelegate_1(this);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("EventSource", string_1 + " - OnClosed", ex);
				}
			}
		}

		private void Retry()
		{
			if (byte_0 <= 0 && CallOnRetry())
			{
				byte_0++;
				dateTime_0 = DateTime.UtcNow;
				HTTPManager.HeartbeatManager_0.Subscribe(this);
				States_0 = States.Retrying;
			}
			else
			{
				SetClosed("Retry");
			}
		}

		private void OnUpgraded(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			EventSourceResponse eventSourceResponse = httpresponse_0 as EventSourceResponse;
			if (eventSourceResponse == null)
			{
				CallOnError("Not an EventSourceResponse!", "OnUpgraded");
				return;
			}
			if (onGeneralEventDelegate_0 != null)
			{
				try
				{
					onGeneralEventDelegate_0(this);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("EventSource", "OnOpen", ex);
				}
			}
			eventSourceResponse.action_0 = (Action<EventSourceResponse, Message>)Delegate.Combine(eventSourceResponse.action_0, new Action<EventSourceResponse, Message>(OnMessageReceived));
			eventSourceResponse.StartReceive();
			byte_0 = 0;
			States_0 = States.Open;
		}

		private void OnRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			if (States_0 == States.Closed)
			{
				return;
			}
			if (States_0 == States.Closing)
			{
				SetClosed("OnRequestFinished");
				return;
			}
			string text = string.Empty;
			bool flag = true;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Processing:
				flag = !httpresponse_0.HasHeader("content-length");
				break;
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Int32_2 == 200 && !httpresponse_0.HasHeaderWithValue("content-type", "text/event-stream"))
				{
					text = "No Content-Type header with value 'text/event-stream' present.";
					flag = false;
				}
				if (flag && httpresponse_0.Int32_2 != 500 && httpresponse_0.Int32_2 != 502 && httpresponse_0.Int32_2 != 503 && httpresponse_0.Int32_2 != 504)
				{
					flag = false;
					text = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				text = "OnRequestFinished - Aborted without request. EventSource's State: " + States_0;
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Processing the request Timed Out!";
				break;
			}
			if (States_0 < States.Closing)
			{
				if (!string.IsNullOrEmpty(text))
				{
					CallOnError(text, "OnRequestFinished");
				}
				if (flag)
				{
					Retry();
				}
				else
				{
					SetClosed("OnRequestFinished");
				}
			}
			else
			{
				SetClosed("OnRequestFinished");
			}
		}

		private void OnMessageReceived(EventSourceResponse eventSourceResponse_0, Message message_0)
		{
			if (States_0 >= States.Closing)
			{
				return;
			}
			if (message_0.String_0 != null)
			{
				String_0 = message_0.String_0;
			}
			if (message_0.TimeSpan_0.TotalMilliseconds > 0.0)
			{
				TimeSpan_0 = message_0.TimeSpan_0;
			}
			if (string.IsNullOrEmpty(message_0.String_2))
			{
				return;
			}
			if (onMessageDelegate_0 != null)
			{
				try
				{
					onMessageDelegate_0(this, message_0);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("EventSource", "OnMessageReceived - OnMessage", ex);
				}
			}
			OnEventDelegate value;
			if (string.IsNullOrEmpty(message_0.String_1) || !dictionary_0.TryGetValue(message_0.String_1, out value) || value == null)
			{
				return;
			}
			try
			{
				value(this, message_0);
			}
			catch (Exception ex2)
			{
				HTTPManager.ILogger_0.Exception("EventSource", "OnMessageReceived - action", ex2);
			}
		}
	}
}
