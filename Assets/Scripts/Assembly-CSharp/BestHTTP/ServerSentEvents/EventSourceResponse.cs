using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace BestHTTP.ServerSentEvents
{
	internal sealed class EventSourceResponse : HTTPResponse, IProtocol
	{
		public Action<EventSourceResponse, Message> action_0;

		public Action<EventSourceResponse> action_1;

		private Thread thread_0;

		private object object_1 = new object();

		private byte[] byte_4 = new byte[1024];

		private int int_6;

		private Message message_0;

		private List<Message> list_2 = new List<Message>();

		[CompilerGenerated]
		private bool bool_5;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_2;

		public bool Boolean_6
		{
			[CompilerGenerated]
			get
			{
				return bool_5;
			}
			[CompilerGenerated]
			private set
			{
				bool_5 = value;
			}
		}

		internal EventSourceResponse(HTTPRequest httprequest_1, Stream stream_2, bool bool_6, bool bool_7)
			: base(httprequest_1, stream_2, bool_6, bool_7)
		{
			base.Boolean_5 = true;
		}

		void IProtocol.HandleEvents()
		{
			lock (object_1)
			{
				if (list_2.Count > 0)
				{
					if (action_0 != null)
					{
						for (int i = 0; i < list_2.Count; i++)
						{
							try
							{
								action_0(this, list_2[i]);
							}
							catch (Exception ex)
							{
								HTTPManager.ILogger_0.Exception("EventSourceMessage", "HandleEvents - OnMessage", ex);
							}
						}
					}
					list_2.Clear();
				}
			}
			if (!Boolean_6)
			{
				return;
			}
			list_2.Clear();
			if (action_1 == null)
			{
				return;
			}
			try
			{
				action_1(this);
			}
			catch (Exception ex2)
			{
				HTTPManager.ILogger_0.Exception("EventSourceMessage", "HandleEvents - OnClosed", ex2);
			}
			finally
			{
				action_1 = null;
			}
		}

		internal override bool Receive(int int_7 = -1, bool bool_6 = true)
		{
			bool flag = base.Receive(int_7, false);
			base.Boolean_4 = flag && base.Int32_2 == 200 && HasHeaderWithValue("content-type", "text/event-stream");
			if (!base.Boolean_4)
			{
				ReadPayload(int_7);
			}
			return flag;
		}

		internal void StartReceive()
		{
			if (base.Boolean_4)
			{
				thread_0 = new Thread(ReceiveThreadFunc);
				thread_0.Name = "EventSource Receiver Thread";
				thread_0.IsBackground = true;
				thread_0.Start();
			}
		}

		private void ReceiveThreadFunc()
		{
			try
			{
				if (HasHeaderWithValue("transfer-encoding", "chunked"))
				{
					ReadChunked(stream_0);
				}
				else
				{
					ReadRaw(stream_0, -1);
				}
			}
			catch (ThreadAbortException)
			{
				httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Aborted;
			}
			catch (Exception exception_)
			{
				httprequest_0.Exception_0 = exception_;
				httprequest_0.HTTPRequestStates_0 = HTTPRequestStates.Error;
			}
			finally
			{
				Boolean_6 = true;
			}
		}

		private new void ReadChunked(Stream stream_2)
		{
			int num = ReadChunkLength(stream_2);
			byte[] array = new byte[num];
			while (num != 0)
			{
				if (array.Length < num)
				{
					Array.Resize(ref array, num);
				}
				int num2 = 0;
				do
				{
					int num3 = stream_2.Read(array, num2, num - num2);
					if (num3 != 0)
					{
						num2 += num3;
						continue;
					}
					throw new Exception("The remote server closed the connection unexpectedly!");
				}
				while (num2 < num);
				FeedData(array, num2);
				HTTPResponse.ReadTo(stream_2, 10);
				num = ReadChunkLength(stream_2);
			}
			ReadHeaders(stream_2);
		}

		private new void ReadRaw(Stream stream_2, int int_7)
		{
			byte[] array = new byte[1024];
			int num;
			do
			{
				num = stream_2.Read(array, 0, array.Length);
				FeedData(array, num);
			}
			while (num > 0);
		}

		public void FeedData(byte[] byte_5, int int_7)
		{
			if (int_7 == -1)
			{
				int_7 = byte_5.Length;
			}
			if (int_7 == 0)
			{
				return;
			}
			int num = 0;
			int num2;
			do
			{
				num2 = -1;
				int num3 = 1;
				for (int i = num; i < int_7; i++)
				{
					if (num2 != -1)
					{
						break;
					}
					if (byte_5[i] == 13)
					{
						if (i + 1 < int_7 && byte_5[i + 1] == 10)
						{
							num3 = 2;
						}
						num2 = i;
					}
					else if (byte_5[i] == 10)
					{
						num2 = i;
					}
				}
				int num4 = ((num2 != -1) ? num2 : int_7);
				if (byte_4.Length < int_6 + (num4 - num))
				{
					Array.Resize(ref byte_4, int_6 + (num4 - num));
				}
				Array.Copy(byte_5, num, byte_4, int_6, num4 - num);
				int_6 += num4 - num;
				if (num2 == -1)
				{
					break;
				}
				ParseLine(byte_4, int_6);
				int_6 = 0;
				num += num2 + num3;
			}
			while (num2 != -1 && num < int_7);
		}

		private void ParseLine(byte[] byte_5, int int_7)
		{
			if (int_7 == 0)
			{
				if (message_0 != null)
				{
					lock (object_1)
					{
						list_2.Add(message_0);
					}
					message_0 = null;
				}
			}
			else
			{
				if (byte_5[0] == 58)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < int_7; i++)
				{
					if (num != -1)
					{
						break;
					}
					if (byte_5[i] == 58)
					{
						num = i;
					}
				}
				string @string;
				string text;
				if (num != -1)
				{
					@string = Encoding.UTF8.GetString(byte_5, 0, num);
					if (num + 1 < int_7 && byte_5[num + 1] == 32)
					{
						num++;
					}
					num++;
					if (num >= int_7)
					{
						return;
					}
					text = Encoding.UTF8.GetString(byte_5, num, int_7 - num);
				}
				else
				{
					@string = Encoding.UTF8.GetString(byte_5, 0, int_7);
					text = string.Empty;
				}
				if (message_0 == null)
				{
					message_0 = new Message();
				}
				switch (@string)
				{
				case "id":
					message_0.String_0 = text;
					break;
				case "event":
					message_0.String_1 = text;
					break;
				case "data":
					if (message_0.String_2 != null)
					{
						message_0.String_2 += Environment.NewLine;
					}
					message_0.String_2 += text;
					break;
				case "retry":
				{
					int result;
					if (int.TryParse(text, out result))
					{
						message_0.TimeSpan_0 = TimeSpan.FromMilliseconds(result);
					}
					break;
				}
				}
			}
		}
	}
}
