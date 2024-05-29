using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;

namespace WebSocketSharp.Server
{
	public class WebSocketSessionManager
	{
		private volatile bool bool_0;

		private object object_0;

		private Logger logger_0;

		private Dictionary<string, IWebSocketSession> dictionary_0;

		private volatile ServerState serverState_0;

		private volatile bool bool_1;

		private System.Timers.Timer timer_0;

		private object object_1;

		private TimeSpan timeSpan_0;

		internal ServerState ServerState_0
		{
			get
			{
				return serverState_0;
			}
		}

		public IEnumerable<string> IEnumerable_0
		{
			get
			{
				Dictionary<string, bool>.Enumerator enumerator = Broadping(WebSocketFrame.byte_3, timeSpan_0).GetEnumerator();
				/*Error near IL_004c: Could not find block for branch target IL_007a*/;
				yield break;
			}
		}

		public int Int32_0
		{
			get
			{
				lock (object_1)
				{
					return dictionary_0.Count;
				}
			}
		}

		public IEnumerable<string> IEnumerable_1
		{
			get
			{
				if (serverState_0 == ServerState.ShuttingDown)
				{
					return new string[0];
				}
				lock (object_1)
				{
					return dictionary_0.Keys.ToList();
				}
			}
		}

		public IEnumerable<string> IEnumerable_2
		{
			get
			{
				Dictionary<string, bool>.Enumerator enumerator = Broadping(WebSocketFrame.byte_3, timeSpan_0).GetEnumerator();
				/*Error near IL_004c: Could not find block for branch target IL_007a*/;
				yield break;
			}
		}

		public IWebSocketSession this[string id]
		{
			get
			{
				IWebSocketSession iwebSocketSession_;
				TryGetSession(id, out iwebSocketSession_);
				return iwebSocketSession_;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			internal set
			{
				if (value ^ bool_0)
				{
					bool_0 = value;
					if (serverState_0 == ServerState.Start)
					{
						timer_0.Enabled = value;
					}
				}
			}
		}

		public IEnumerable<IWebSocketSession> IEnumerable_3
		{
			get
			{
				if (serverState_0 == ServerState.ShuttingDown)
				{
					return new IWebSocketSession[0];
				}
				lock (object_1)
				{
					return dictionary_0.Values.ToList();
				}
			}
		}

		public TimeSpan TimeSpan_0
		{
			get
			{
				return timeSpan_0;
			}
			internal set
			{
				if (value == timeSpan_0)
				{
					return;
				}
				timeSpan_0 = value;
				foreach (IWebSocketSession item in IEnumerable_3)
				{
					item.WebSocketContext_0.WebSocket_0.TimeSpan_0 = value;
				}
			}
		}

		internal WebSocketSessionManager()
			: this(new Logger())
		{
		}

		internal WebSocketSessionManager(Logger logger_1)
		{
			logger_0 = logger_1;
			bool_0 = true;
			object_0 = new object();
			dictionary_0 = new Dictionary<string, IWebSocketSession>();
			serverState_0 = ServerState.Ready;
			object_1 = ((ICollection)dictionary_0).SyncRoot;
			timeSpan_0 = TimeSpan.FromSeconds(1.0);
			setSweepTimer(60000.0);
		}

		private void broadcast(Opcode opcode_0, byte[] byte_0, Action action_0)
		{
			Dictionary<CompressionMethod, byte[]> dictionary = new Dictionary<CompressionMethod, byte[]>();
			try
			{
				Broadcast(opcode_0, byte_0, dictionary);
				if (action_0 != null)
				{
					action_0();
				}
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
			}
			finally
			{
				dictionary.Clear();
			}
		}

		private void broadcast(Opcode opcode_0, Stream stream_0, Action action_0)
		{
			Dictionary<CompressionMethod, Stream> dictionary = new Dictionary<CompressionMethod, Stream>();
			try
			{
				Broadcast(opcode_0, stream_0, dictionary);
				if (action_0 != null)
				{
					action_0();
				}
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
			}
			finally
			{
				foreach (Stream value in dictionary.Values)
				{
					value.Dispose();
				}
				dictionary.Clear();
			}
		}

		private void broadcastAsync(Opcode opcode_0, byte[] byte_0, Action action_0)
		{
			ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode_0, byte_0, action_0);
			});
		}

		private void broadcastAsync(Opcode opcode_0, Stream stream_0, Action action_0)
		{
			ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode_0, stream_0, action_0);
			});
		}

		private static string createID()
		{
			return Guid.NewGuid().ToString("N");
		}

		private void setSweepTimer(double double_0)
		{
			timer_0 = new System.Timers.Timer(double_0);
			timer_0.Elapsed += delegate
			{
				Sweep();
			};
		}

		private bool tryGetSession(string string_0, out IWebSocketSession iwebSocketSession_0)
		{
			bool flag;
			lock (object_1)
			{
				flag = dictionary_0.TryGetValue(string_0, out iwebSocketSession_0);
			}
			if (!flag)
			{
				logger_0.Error("A session with the specified ID isn't found:\n  ID: " + string_0);
			}
			return flag;
		}

		internal string Add(IWebSocketSession iwebSocketSession_0)
		{
			lock (object_1)
			{
				if (serverState_0 != ServerState.Start)
				{
					return null;
				}
				string text = createID();
				dictionary_0.Add(text, iwebSocketSession_0);
				return text;
			}
		}

		internal void Broadcast(Opcode opcode_0, byte[] byte_0, Dictionary<CompressionMethod, byte[]> dictionary_1)
		{
			foreach (IWebSocketSession item in IEnumerable_3)
			{
				if (serverState_0 != ServerState.Start)
				{
					break;
				}
				item.WebSocketContext_0.WebSocket_0.Send(opcode_0, byte_0, dictionary_1);
			}
		}

		internal void Broadcast(Opcode opcode_0, Stream stream_0, Dictionary<CompressionMethod, Stream> dictionary_1)
		{
			foreach (IWebSocketSession item in IEnumerable_3)
			{
				if (serverState_0 != ServerState.Start)
				{
					break;
				}
				item.WebSocketContext_0.WebSocket_0.Send(opcode_0, stream_0, dictionary_1);
			}
		}

		internal Dictionary<string, bool> Broadping(byte[] byte_0, TimeSpan timeSpan_1)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (IWebSocketSession item in IEnumerable_3)
			{
				if (serverState_0 != ServerState.Start)
				{
					break;
				}
				dictionary.Add(item.String_0, item.WebSocketContext_0.WebSocket_0.Ping(byte_0, timeSpan_1));
			}
			return dictionary;
		}

		internal bool Remove(string string_0)
		{
			lock (object_1)
			{
				return dictionary_0.Remove(string_0);
			}
		}

		internal void Start()
		{
			lock (object_1)
			{
				timer_0.Enabled = bool_0;
				serverState_0 = ServerState.Start;
			}
		}

		internal void Stop(CloseEventArgs closeEventArgs_0, byte[] byte_0, TimeSpan timeSpan_1)
		{
			lock (object_1)
			{
				serverState_0 = ServerState.ShuttingDown;
				timer_0.Enabled = false;
				foreach (IWebSocketSession item in dictionary_0.Values.ToList())
				{
					item.WebSocketContext_0.WebSocket_0.Close(closeEventArgs_0, byte_0, timeSpan_1);
				}
				serverState_0 = ServerState.Stop;
			}
		}

		public void Broadcast(byte[] byte_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? byte_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
			}
			else if (byte_0.LongLength <= 2147483633L)
			{
				broadcast(Opcode.Binary, byte_0, null);
			}
			else
			{
				broadcast(Opcode.Binary, new MemoryStream(byte_0), null);
			}
		}

		public void Broadcast(string string_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? string_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(string_0);
			if (bytes.LongLength <= 2147483633L)
			{
				broadcast(Opcode.Text, bytes, null);
			}
			else
			{
				broadcast(Opcode.Text, new MemoryStream(bytes), null);
			}
		}

		public void BroadcastAsync(byte[] byte_0, Action action_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? byte_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
			}
			else if (byte_0.LongLength <= 2147483633L)
			{
				broadcastAsync(Opcode.Binary, byte_0, action_0);
			}
			else
			{
				broadcastAsync(Opcode.Binary, new MemoryStream(byte_0), action_0);
			}
		}

		public void BroadcastAsync(string string_0, Action action_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? string_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(string_0);
			if (bytes.LongLength <= 2147483633L)
			{
				broadcastAsync(Opcode.Text, bytes, action_0);
			}
			else
			{
				broadcastAsync(Opcode.Text, new MemoryStream(bytes), action_0);
			}
		}

		public void BroadcastAsync(Stream stream_0, int int_0, Action action_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? stream_0.CheckIfCanRead() ?? ((int_0 >= 1) ? null : "'length' is less than 1.");
			if (text != null)
			{
				logger_0.Error(text);
				return;
			}
			stream_0.ReadBytesAsync(int_0, delegate(byte[] byte_0)
			{
				int num = byte_0.Length;
				if (num == 0)
				{
					logger_0.Error("The data cannot be read from 'stream'.");
				}
				else
				{
					if (num < int_0)
					{
						logger_0.Warn(string.Format("The data with 'length' cannot be read from 'stream':\n  expected: {0}\n  actual: {1}", int_0, num));
					}
					if (num <= 2147483633)
					{
						broadcast(Opcode.Binary, byte_0, action_0);
					}
					else
					{
						broadcast(Opcode.Binary, new MemoryStream(byte_0), action_0);
					}
				}
			}, delegate(Exception exception_0)
			{
				logger_0.Fatal(exception_0.ToString());
			});
		}

		public Dictionary<string, bool> Broadping()
		{
			string text = serverState_0.CheckIfAvailable(false, true, false);
			if (text != null)
			{
				logger_0.Error(text);
				return null;
			}
			return Broadping(WebSocketFrame.byte_3, timeSpan_0);
		}

		public Dictionary<string, bool> Broadping(string string_0)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				byte[] byte_ = null;
				string text = serverState_0.CheckIfAvailable(false, true, false) ?? WebSocket.CheckPingParameter(string_0, out byte_);
				if (text != null)
				{
					logger_0.Error(text);
					return null;
				}
				return Broadping(WebSocketFrame.CreatePingFrame(byte_, false).ToByteArray(), timeSpan_0);
			}
			return Broadping();
		}

		public void CloseSession(string string_0)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.Close();
			}
		}

		public void CloseSession(string string_0, ushort ushort_0, string string_1)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.Close(ushort_0, string_1);
			}
		}

		public void CloseSession(string string_0, CloseStatusCode closeStatusCode_0, string string_1)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.Close(closeStatusCode_0, string_1);
			}
		}

		public bool PingTo(string string_0)
		{
			IWebSocketSession iwebSocketSession_;
			return TryGetSession(string_0, out iwebSocketSession_) && iwebSocketSession_.WebSocketContext_0.WebSocket_0.Ping();
		}

		public bool PingTo(string string_0, string string_1)
		{
			IWebSocketSession iwebSocketSession_;
			return TryGetSession(string_1, out iwebSocketSession_) && iwebSocketSession_.WebSocketContext_0.WebSocket_0.Ping(string_0);
		}

		public void SendTo(byte[] byte_0, string string_0)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.Send(byte_0);
			}
		}

		public void SendTo(string string_0, string string_1)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_1, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.Send(string_0);
			}
		}

		public void SendToAsync(byte[] byte_0, string string_0, Action<bool> action_0)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.SendAsync(byte_0, action_0);
			}
		}

		public void SendToAsync(string string_0, string string_1, Action<bool> action_0)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_1, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.SendAsync(string_0, action_0);
			}
		}

		public void SendToAsync(Stream stream_0, int int_0, string string_0, Action<bool> action_0)
		{
			IWebSocketSession iwebSocketSession_;
			if (TryGetSession(string_0, out iwebSocketSession_))
			{
				iwebSocketSession_.WebSocketContext_0.WebSocket_0.SendAsync(stream_0, int_0, action_0);
			}
		}

		public void Sweep()
		{
			if (serverState_0 != ServerState.Start || bool_1 || Int32_0 == 0)
			{
				return;
			}
			lock (object_0)
			{
				bool_1 = true;
				foreach (string item in IEnumerable_2)
				{
					if (serverState_0 != ServerState.Start)
					{
						break;
					}
					lock (object_1)
					{
						IWebSocketSession value;
						if (dictionary_0.TryGetValue(item, out value))
						{
							switch (value.WebSocketState_0)
							{
							case WebSocketState.Open:
								value.WebSocketContext_0.WebSocket_0.Close(CloseStatusCode.ProtocolError);
								break;
							case WebSocketState.Closing:
								break;
							default:
								dictionary_0.Remove(item);
								break;
							}
						}
					}
				}
				bool_1 = false;
			}
		}

		public bool TryGetSession(string string_0, out IWebSocketSession iwebSocketSession_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? string_0.CheckIfValidSessionID();
			if (text != null)
			{
				logger_0.Error(text);
				iwebSocketSession_0 = null;
				return false;
			}
			return tryGetSession(string_0, out iwebSocketSession_0);
		}
	}
}
