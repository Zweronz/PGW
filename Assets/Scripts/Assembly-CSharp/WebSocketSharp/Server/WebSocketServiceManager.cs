using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;

namespace WebSocketSharp.Server
{
	public class WebSocketServiceManager
	{
		private volatile bool bool_0;

		private Dictionary<string, WebSocketServiceHost> dictionary_0;

		private Logger logger_0;

		private volatile ServerState serverState_0;

		private object object_0;

		private TimeSpan timeSpan_0;

		public int Int32_0
		{
			get
			{
				lock (object_0)
				{
					return dictionary_0.Count;
				}
			}
		}

		public IEnumerable<WebSocketServiceHost> IEnumerable_0
		{
			get
			{
				lock (object_0)
				{
					return dictionary_0.Values.ToList();
				}
			}
		}

		public WebSocketServiceHost this[string path]
		{
			get
			{
				WebSocketServiceHost webSocketServiceHost_;
				TryGetServiceHost(path, out webSocketServiceHost_);
				return webSocketServiceHost_;
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
				lock (object_0)
				{
					if (!(value ^ bool_0))
					{
						return;
					}
					bool_0 = value;
					foreach (WebSocketServiceHost value2 in dictionary_0.Values)
					{
						value2.Boolean_0 = value;
					}
				}
			}
		}

		public IEnumerable<string> IEnumerable_1
		{
			get
			{
				lock (object_0)
				{
					return dictionary_0.Keys.ToList();
				}
			}
		}

		public int Int32_1
		{
			get
			{
				int num = 0;
				foreach (WebSocketServiceHost item in IEnumerable_0)
				{
					if (serverState_0 != ServerState.Start)
					{
						break;
					}
					num += item.WebSocketSessionManager_0.Int32_0;
				}
				return num;
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
				lock (object_0)
				{
					if (value == timeSpan_0)
					{
						return;
					}
					timeSpan_0 = value;
					foreach (WebSocketServiceHost value2 in dictionary_0.Values)
					{
						value2.TimeSpan_0 = value;
					}
				}
			}
		}

		internal WebSocketServiceManager()
			: this(new Logger())
		{
		}

		internal WebSocketServiceManager(Logger logger_1)
		{
			logger_0 = logger_1;
			bool_0 = true;
			dictionary_0 = new Dictionary<string, WebSocketServiceHost>();
			serverState_0 = ServerState.Ready;
			object_0 = ((ICollection)dictionary_0).SyncRoot;
			timeSpan_0 = TimeSpan.FromSeconds(1.0);
		}

		private void broadcast(Opcode opcode_0, byte[] byte_0, Action action_0)
		{
			Dictionary<CompressionMethod, byte[]> dictionary = new Dictionary<CompressionMethod, byte[]>();
			try
			{
				foreach (WebSocketServiceHost item in IEnumerable_0)
				{
					if (serverState_0 != ServerState.Start)
					{
						break;
					}
					item.WebSocketSessionManager_0.Broadcast(opcode_0, byte_0, dictionary);
				}
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
				foreach (WebSocketServiceHost item in IEnumerable_0)
				{
					if (serverState_0 != ServerState.Start)
					{
						break;
					}
					item.WebSocketSessionManager_0.Broadcast(opcode_0, stream_0, dictionary);
				}
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

		private Dictionary<string, Dictionary<string, bool>> broadping(byte[] byte_0, TimeSpan timeSpan_1)
		{
			Dictionary<string, Dictionary<string, bool>> dictionary = new Dictionary<string, Dictionary<string, bool>>();
			foreach (WebSocketServiceHost item in IEnumerable_0)
			{
				if (serverState_0 != ServerState.Start)
				{
					break;
				}
				dictionary.Add(item.String_0, item.WebSocketSessionManager_0.Broadping(byte_0, timeSpan_1));
			}
			return dictionary;
		}

		internal void Add<TBehavior>(string string_0, Func<TBehavior> func_0) where TBehavior : WebSocketBehavior
		{
			lock (object_0)
			{
				string_0 = HttpUtility.UrlDecode(string_0).TrimEndSlash();
				WebSocketServiceHost value;
				if (dictionary_0.TryGetValue(string_0, out value))
				{
					logger_0.Error("A WebSocket service with the specified path already exists:\n  path: " + string_0);
					return;
				}
				value = new WebSocketServiceHost<TBehavior>(string_0, func_0, logger_0);
				if (!bool_0)
				{
					value.Boolean_0 = false;
				}
				if (timeSpan_0 != value.TimeSpan_0)
				{
					value.TimeSpan_0 = timeSpan_0;
				}
				if (serverState_0 == ServerState.Start)
				{
					value.Start();
				}
				dictionary_0.Add(string_0, value);
			}
		}

		internal bool InternalTryGetServiceHost(string string_0, out WebSocketServiceHost webSocketServiceHost_0)
		{
			bool flag;
			lock (object_0)
			{
				string_0 = HttpUtility.UrlDecode(string_0).TrimEndSlash();
				flag = dictionary_0.TryGetValue(string_0, out webSocketServiceHost_0);
			}
			if (!flag)
			{
				logger_0.Error("A WebSocket service with the specified path isn't found:\n  path: " + string_0);
			}
			return flag;
		}

		internal bool Remove(string string_0)
		{
			WebSocketServiceHost value;
			lock (object_0)
			{
				string_0 = HttpUtility.UrlDecode(string_0).TrimEndSlash();
				if (!dictionary_0.TryGetValue(string_0, out value))
				{
					logger_0.Error("A WebSocket service with the specified path isn't found:\n  path: " + string_0);
					return false;
				}
				dictionary_0.Remove(string_0);
			}
			if (value.ServerState_0 == ServerState.Start)
			{
				value.Stop(1001, null);
			}
			return true;
		}

		internal void Start()
		{
			lock (object_0)
			{
				foreach (WebSocketServiceHost value in dictionary_0.Values)
				{
					value.Start();
				}
				serverState_0 = ServerState.Start;
			}
		}

		internal void Stop(CloseEventArgs closeEventArgs_0, bool bool_1, bool bool_2)
		{
			lock (object_0)
			{
				serverState_0 = ServerState.ShuttingDown;
				byte[] byte_ = ((!bool_1) ? null : WebSocketFrame.CreateCloseFrame(closeEventArgs_0.PayloadData_0, false).ToByteArray());
				TimeSpan timeSpan_ = ((!bool_2) ? TimeSpan.Zero : timeSpan_0);
				foreach (WebSocketServiceHost value in dictionary_0.Values)
				{
					value.WebSocketSessionManager_0.Stop(closeEventArgs_0, byte_, timeSpan_);
				}
				dictionary_0.Clear();
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

		public Dictionary<string, Dictionary<string, bool>> Broadping()
		{
			string text = serverState_0.CheckIfAvailable(false, true, false);
			if (text != null)
			{
				logger_0.Error(text);
				return null;
			}
			return broadping(WebSocketFrame.byte_3, timeSpan_0);
		}

		public Dictionary<string, Dictionary<string, bool>> Broadping(string string_0)
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
				return broadping(WebSocketFrame.CreatePingFrame(byte_, false).ToByteArray(), timeSpan_0);
			}
			return Broadping();
		}

		public bool TryGetServiceHost(string string_0, out WebSocketServiceHost webSocketServiceHost_0)
		{
			string text = serverState_0.CheckIfAvailable(false, true, false) ?? string_0.CheckIfValidServicePath();
			if (text != null)
			{
				logger_0.Error(text);
				webSocketServiceHost_0 = null;
				return false;
			}
			return InternalTryGetServiceHost(string_0, out webSocketServiceHost_0);
		}
	}
}
