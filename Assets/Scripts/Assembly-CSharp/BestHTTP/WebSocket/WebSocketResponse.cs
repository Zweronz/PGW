using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket
{
	public sealed class WebSocketResponse : HTTPResponse, IHeartbeat, IProtocol
	{
		public Action<WebSocketResponse, string> action_0;

		public Action<WebSocketResponse, byte[]> action_1;

		public Action<WebSocketResponse, WebSocketFrameReader> action_2;

		public Action<WebSocketResponse, ushort, string> action_3;

		private List<WebSocketFrameReader> list_2 = new List<WebSocketFrameReader>();

		private List<WebSocketFrameReader> list_3 = new List<WebSocketFrameReader>();

		private WebSocketFrameReader webSocketFrameReader_0;

		private Thread thread_0;

		private object object_1 = new object();

		private object object_2 = new object();

		private bool bool_5;

		private bool bool_6;

		private DateTime dateTime_0 = DateTime.MinValue;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private ushort ushort_0;

		public bool Boolean_6
		{
			get
			{
				return bool_6;
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

		public ushort UInt16_0
		{
			[CompilerGenerated]
			get
			{
				return ushort_0;
			}
			[CompilerGenerated]
			private set
			{
				ushort_0 = value;
			}
		}

		internal WebSocketResponse(HTTPRequest httprequest_1, Stream stream_2, bool bool_7, bool bool_8)
			: base(httprequest_1, stream_2, bool_7, bool_8)
		{
			base.Boolean_5 = true;
			bool_6 = false;
			UInt16_0 = 32767;
		}

		void IProtocol.HandleEvents()
		{
			lock (object_1)
			{
				for (int i = 0; i < list_3.Count; i++)
				{
					WebSocketFrameReader webSocketFrameReader = list_3[i];
					try
					{
						switch (webSocketFrameReader.WebSocketFrameTypes_0)
						{
						case WebSocketFrameTypes.Text:
							if (webSocketFrameReader.Boolean_0)
							{
								if (action_0 != null)
								{
									action_0(this, Encoding.UTF8.GetString(webSocketFrameReader.Byte_1, 0, webSocketFrameReader.Byte_1.Length));
								}
								break;
							}
							goto case WebSocketFrameTypes.Continuation;
						case WebSocketFrameTypes.Binary:
							if (webSocketFrameReader.Boolean_0)
							{
								if (action_1 != null)
								{
									action_1(this, webSocketFrameReader.Byte_1);
								}
								break;
							}
							goto case WebSocketFrameTypes.Continuation;
						case WebSocketFrameTypes.Continuation:
							if (action_2 != null)
							{
								action_2(this, webSocketFrameReader);
							}
							break;
						}
					}
					catch (Exception ex)
					{
						HTTPManager.ILogger_0.Exception("WebSocketResponse", "HandleEvents", ex);
					}
				}
				list_3.Clear();
			}
			if (!Boolean_6 || action_3 == null || httprequest_0.HTTPRequestStates_0 != HTTPRequestStates.Processing)
			{
				return;
			}
			try
			{
				ushort arg = 0;
				string arg2 = string.Empty;
				if (webSocketFrameReader_0 != null && webSocketFrameReader_0.Byte_1 != null && webSocketFrameReader_0.Byte_1.Length >= 2)
				{
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(webSocketFrameReader_0.Byte_1, 0, 2);
					}
					arg = BitConverter.ToUInt16(webSocketFrameReader_0.Byte_1, 0);
					if (webSocketFrameReader_0.Byte_1.Length > 2)
					{
						arg2 = Encoding.UTF8.GetString(webSocketFrameReader_0.Byte_1, 2, webSocketFrameReader_0.Byte_1.Length - 2);
					}
				}
				action_3(this, arg, arg2);
			}
			catch (Exception ex2)
			{
				HTTPManager.ILogger_0.Exception("WebSocketResponse", "HandleEvents - OnClosed", ex2);
			}
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			if (dateTime_0 == DateTime.MinValue)
			{
				dateTime_0 = DateTime.UtcNow;
			}
			else if (DateTime.UtcNow - dateTime_0 >= TimeSpan_0)
			{
				Send(new WebSocketPing(string.Empty));
				dateTime_0 = DateTime.UtcNow;
			}
		}

		internal void StartReceive()
		{
			if (base.Boolean_4)
			{
				thread_0 = new Thread(ReceiveThreadFunc);
				thread_0.Name = "WebSocket Receiver Thread";
				thread_0.IsBackground = true;
				thread_0.Start();
			}
		}

		public void Send(string string_2)
		{
			if (string_2 == null)
			{
				throw new ArgumentNullException("message must not be null!");
			}
			Send(new WebSocketTextFrame(string_2));
		}

		public void Send(byte[] byte_4)
		{
			if (byte_4 == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			if ((long)byte_4.Length > (long)(int)UInt16_0)
			{
				lock (object_2)
				{
					Send(new WebSocketBinaryFrame(byte_4, 0uL, UInt16_0, false));
					ulong num2;
					for (ulong num = UInt16_0; num < (ulong)byte_4.Length; num += num2)
					{
						num2 = Math.Min(UInt16_0, (ulong)byte_4.Length - num);
						Send(new WebSocketContinuationFrame(byte_4, num, num2, num + num2 >= (ulong)byte_4.Length));
					}
					return;
				}
			}
			Send(new WebSocketBinaryFrame(byte_4));
		}

		public void Send(byte[] byte_4, ulong ulong_0, ulong ulong_1)
		{
			if (byte_4 == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			if (ulong_0 + ulong_1 > (ulong)byte_4.Length)
			{
				throw new ArgumentOutOfRangeException("offset + count >= data.Length");
			}
			if ((long)ulong_1 > (long)(int)UInt16_0)
			{
				lock (object_2)
				{
					Send(new WebSocketBinaryFrame(byte_4, ulong_0, UInt16_0, false));
					ulong num2;
					for (ulong num = ulong_0 + UInt16_0; num < ulong_1; num += num2)
					{
						num2 = Math.Min(UInt16_0, ulong_1 - num);
						Send(new WebSocketContinuationFrame(byte_4, num, num2, num + num2 >= ulong_1));
					}
					return;
				}
			}
			Send(new WebSocketBinaryFrame(byte_4, ulong_0, ulong_1, true));
		}

		public void Send(IWebSocketFrameWriter iwebSocketFrameWriter_0)
		{
			if (iwebSocketFrameWriter_0 == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (!bool_6)
			{
				byte[] array = iwebSocketFrameWriter_0.Get();
				lock (object_2)
				{
					stream_0.Write(array, 0, array.Length);
					stream_0.Flush();
				}
				if (iwebSocketFrameWriter_0.WebSocketFrameTypes_0 == WebSocketFrameTypes.ConnectionClose)
				{
					bool_5 = true;
				}
			}
		}

		public void Close()
		{
			Close(1000, "Bye!");
		}

		public void Close(ushort ushort_1, string string_2)
		{
			if (!bool_6)
			{
				Send(new WebSocketClose(ushort_1, string_2));
			}
		}

		public void StartPinging(int int_6)
		{
			if (int_6 < 100)
			{
				throw new ArgumentException("frequency must be at least 100 millisec!");
			}
			TimeSpan_0 = TimeSpan.FromMilliseconds(int_6);
			HTTPManager.HeartbeatManager_0.Subscribe(this);
		}

		private void ReceiveThreadFunc()
		{
			try
			{
				while (!bool_6)
				{
					try
					{
						WebSocketFrameReader webSocketFrameReader = new WebSocketFrameReader();
						webSocketFrameReader.Read(stream_0);
						if (webSocketFrameReader.Boolean_1)
						{
							Close(1002, "Protocol Error: masked frame received from server!");
							continue;
						}
						if (!webSocketFrameReader.Boolean_0)
						{
							if (action_2 == null)
							{
								list_2.Add(webSocketFrameReader);
								continue;
							}
							lock (object_1)
							{
								list_3.Add(webSocketFrameReader);
							}
							continue;
						}
						switch (webSocketFrameReader.WebSocketFrameTypes_0)
						{
						case WebSocketFrameTypes.Continuation:
							if (action_2 == null)
							{
								webSocketFrameReader.Assemble(list_2);
								list_2.Clear();
								goto case WebSocketFrameTypes.Text;
							}
							lock (object_1)
							{
								list_3.Add(webSocketFrameReader);
							}
							break;
						case WebSocketFrameTypes.Text:
						case WebSocketFrameTypes.Binary:
							lock (object_1)
							{
								list_3.Add(webSocketFrameReader);
							}
							break;
						case WebSocketFrameTypes.ConnectionClose:
							webSocketFrameReader_0 = webSocketFrameReader;
							if (!bool_5)
							{
								Send(new WebSocketClose());
							}
							bool_6 = bool_5;
							break;
						case WebSocketFrameTypes.Ping:
							if (!bool_5 && !bool_6)
							{
								Send(new WebSocketPong(webSocketFrameReader));
							}
							break;
						case (WebSocketFrameTypes)3:
						case (WebSocketFrameTypes)4:
						case (WebSocketFrameTypes)5:
						case (WebSocketFrameTypes)6:
						case (WebSocketFrameTypes)7:
							break;
						}
					}
					catch (ThreadAbortException)
					{
						list_2.Clear();
						httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Aborted;
						bool_6 = true;
					}
					catch (Exception exception_)
					{
						httprequest_0.Exception_0 = exception_;
						httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Error;
						bool_6 = true;
					}
				}
			}
			finally
			{
				HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			}
		}
	}
}
