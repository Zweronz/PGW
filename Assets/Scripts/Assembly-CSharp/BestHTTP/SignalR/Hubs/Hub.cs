using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Hubs
{
	public class Hub : IHub
	{
		private Dictionary<string, object> dictionary_0;

		private Dictionary<ulong, ClientMessage> dictionary_1 = new Dictionary<ulong, ClientMessage>();

		private Dictionary<string, OnMethodCallCallbackDelegate> dictionary_2 = new Dictionary<string, OnMethodCallCallbackDelegate>();

		private StringBuilder stringBuilder_0 = new StringBuilder();

		private OnMethodCallDelegate onMethodCallDelegate_0;

		[CompilerGenerated]
		private Connection connection_0;

		[CompilerGenerated]
		private string string_0;

		Connection IHub.Connection
		{
			[CompilerGenerated]
			get
			{
				return connection_0;
			}
			[CompilerGenerated]
			set
			{
				connection_0 = value;
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

		public Dictionary<string, object> Dictionary_0
		{
			get
			{
				if (dictionary_0 == null)
				{
					dictionary_0 = new Dictionary<string, object>();
				}
				return dictionary_0;
			}
		}

		public event OnMethodCallDelegate OnMethodCall
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onMethodCallDelegate_0 = (OnMethodCallDelegate)Delegate.Combine(onMethodCallDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onMethodCallDelegate_0 = (OnMethodCallDelegate)Delegate.Remove(onMethodCallDelegate_0, value);
			}
		}

		public Hub(string string_1)
			: this(string_1, null)
		{
		}

		public Hub(string string_1, Connection connection_1)
		{
			String_0 = string_1;
			((IHub)this).Connection = connection_1;
		}

		void IHub.Call(ClientMessage msg)
		{
			lock (((IHub)this).Connection.object_0)
			{
				dictionary_1.Add(msg.ulong_0, msg);
				((IHub)this).Connection.SendJson(BuildMessage(msg));
			}
		}

		bool IHub.HasSentMessageId(ulong id)
		{
			return dictionary_1.ContainsKey(id);
		}

		void IHub.Close()
		{
			dictionary_1.Clear();
		}

		void IHub.OnMethod(MethodCallMessage msg)
		{
			MergeState(msg.IDictionary_0);
			if (onMethodCallDelegate_0 != null)
			{
				try
				{
					onMethodCallDelegate_0(this, msg.String_1, msg.Object_0);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("Hub - " + String_0, "IHub.OnMethod - OnMethodCall", ex);
				}
			}
			OnMethodCallCallbackDelegate value;
			if (dictionary_2.TryGetValue(msg.String_1, out value) && value != null)
			{
				try
				{
					value(this, msg);
					return;
				}
				catch (Exception ex2)
				{
					HTTPManager.ILogger_0.Exception("Hub - " + String_0, "IHub.OnMethod - callback", ex2);
					return;
				}
			}
			HTTPManager.ILogger_0.Information("Hub - " + String_0, string.Format("[Client] {0}.{1} (args: {2})", String_0, msg.String_1, msg.Object_0.Length));
		}

		void IHub.OnMessage(IServerMessage msg)
		{
			ulong uInt64_ = (msg as IHubMessage).UInt64_0;
			ClientMessage value;
			if (!dictionary_1.TryGetValue(uInt64_, out value))
			{
				HTTPManager.ILogger_0.Warning("Hub - " + String_0, "OnMessage - Sent message not found with id: " + uInt64_);
				return;
			}
			switch (msg.Type)
			{
			case MessageTypes.Result:
			{
				ResultMessage resultMessage = msg as ResultMessage;
				MergeState(resultMessage.IDictionary_0);
				if (value.onMethodResultDelegate_0 != null)
				{
					value.onMethodResultDelegate_0(this, value, resultMessage);
				}
				dictionary_1.Remove(uInt64_);
				break;
			}
			case MessageTypes.Failure:
			{
				FailureMessage failureMessage = msg as FailureMessage;
				MergeState(failureMessage.IDictionary_1);
				if (value.onMethodFailedDelegate_0 != null)
				{
					value.onMethodFailedDelegate_0(this, value, failureMessage);
				}
				dictionary_1.Remove(uInt64_);
				break;
			}
			case MessageTypes.Progress:
				if (value.onMethodProgressDelegate_0 != null)
				{
					value.onMethodProgressDelegate_0(this, value, msg as ProgressMessage);
				}
				break;
			case MessageTypes.MethodCall:
				break;
			}
		}

		public void On(string string_1, OnMethodCallCallbackDelegate onMethodCallCallbackDelegate_0)
		{
			dictionary_2[string_1] = onMethodCallCallbackDelegate_0;
		}

		public void Off(string string_1)
		{
			dictionary_2[string_1] = null;
		}

		public void Call(string string_1, params object[] object_0)
		{
			Call(string_1, null, null, null, object_0);
		}

		public void Call(string string_1, OnMethodResultDelegate onMethodResultDelegate_0, params object[] object_0)
		{
			Call(string_1, onMethodResultDelegate_0, null, null, object_0);
		}

		public void Call(string string_1, OnMethodResultDelegate onMethodResultDelegate_0, OnMethodFailedDelegate onMethodFailedDelegate_0, params object[] object_0)
		{
			Call(string_1, onMethodResultDelegate_0, onMethodFailedDelegate_0, null, object_0);
		}

		public void Call(string string_1, OnMethodResultDelegate onMethodResultDelegate_0, OnMethodProgressDelegate onMethodProgressDelegate_0, params object[] object_0)
		{
			Call(string_1, onMethodResultDelegate_0, null, onMethodProgressDelegate_0, object_0);
		}

		public void Call(string string_1, OnMethodResultDelegate onMethodResultDelegate_0, OnMethodFailedDelegate onMethodFailedDelegate_0, OnMethodProgressDelegate onMethodProgressDelegate_0, params object[] object_0)
		{
			lock (((IHub)this).Connection.object_0)
			{
				((IHub)this).Connection.UInt64_0 %= ulong.MaxValue;
				((IHub)this).Call(new ClientMessage(this, string_1, object_0, ((IHub)this).Connection.UInt64_0++, onMethodResultDelegate_0, onMethodFailedDelegate_0, onMethodProgressDelegate_0));
			}
		}

		private void MergeState(IDictionary<string, object> idictionary_0)
		{
			if (idictionary_0 == null || idictionary_0.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<string, object> item in idictionary_0)
			{
				Dictionary_0[item.Key] = item.Value;
			}
		}

		private string BuildMessage(ClientMessage clientMessage_0)
		{
			try
			{
				stringBuilder_0.Append("{\"H\":\"");
				stringBuilder_0.Append(String_0);
				stringBuilder_0.Append("\",\"M\":\"");
				stringBuilder_0.Append(clientMessage_0.string_0);
				stringBuilder_0.Append("\",\"A\":");
				string empty = string.Empty;
				empty = ((clientMessage_0.object_0 == null || clientMessage_0.object_0.Length <= 0) ? "[]" : ((IHub)this).Connection.IJsonEncoder_0.Encode(clientMessage_0.object_0));
				stringBuilder_0.Append(empty);
				stringBuilder_0.Append(",\"I\":\"");
				stringBuilder_0.Append(clientMessage_0.ulong_0.ToString());
				stringBuilder_0.Append("\"");
				if (clientMessage_0.hub_0.dictionary_0 != null && clientMessage_0.hub_0.dictionary_0.Count > 0)
				{
					stringBuilder_0.Append(",\"S\":");
					empty = ((IHub)this).Connection.IJsonEncoder_0.Encode(clientMessage_0.hub_0.dictionary_0);
					stringBuilder_0.Append(empty);
				}
				stringBuilder_0.Append("}");
				return stringBuilder_0.ToString();
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("Hub - " + String_0, "Send", ex);
				return null;
			}
			finally
			{
				stringBuilder_0.Length = 0;
			}
		}
	}
}
