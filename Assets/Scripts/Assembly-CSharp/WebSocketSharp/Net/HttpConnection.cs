using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal sealed class HttpConnection
	{
		private const int int_0 = 8192;

		private byte[] byte_0;

		private HttpListenerContext httpListenerContext_0;

		private bool bool_0;

		private StringBuilder stringBuilder_0;

		private InputState inputState_0;

		private RequestStream requestStream_0;

		private HttpListener httpListener_0;

		private LineState lineState_0;

		private EndPointListener endPointListener_0;

		private ResponseStream responseStream_0;

		private int int_1;

		private HttpListenerPrefix httpListenerPrefix_0;

		private MemoryStream memoryStream_0;

		private int int_2;

		private bool bool_1;

		private Socket socket_0;

		private Stream stream_0;

		private object object_0;

		private int int_3;

		private Timer timer_0;

		public bool Boolean_0
		{
			get
			{
				return socket_0 == null;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_1;
			}
		}

		public IPEndPoint IPEndPoint_0
		{
			get
			{
				return (IPEndPoint)socket_0.LocalEndPoint;
			}
		}

		public HttpListenerPrefix HttpListenerPrefix_0
		{
			get
			{
				return httpListenerPrefix_0;
			}
			set
			{
				httpListenerPrefix_0 = value;
			}
		}

		public IPEndPoint IPEndPoint_1
		{
			get
			{
				return (IPEndPoint)socket_0.RemoteEndPoint;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_2;
			}
		}

		public Stream Stream_0
		{
			get
			{
				return stream_0;
			}
		}

		internal HttpConnection(Socket socket_1, EndPointListener endPointListener_1)
		{
			socket_0 = socket_1;
			endPointListener_0 = endPointListener_1;
			bool_1 = endPointListener_1.Boolean_0;
			NetworkStream innerStream = new NetworkStream(socket_1, false);
			if (bool_1)
			{
				ServerSslConfiguration serverSslConfiguration_ = endPointListener_1.ServerSslConfiguration_0;
				SslStream sslStream = new SslStream(innerStream, false, serverSslConfiguration_.RemoteCertificateValidationCallback_1);
				sslStream.AuthenticateAsServer(serverSslConfiguration_.X509Certificate2_0, serverSslConfiguration_.Boolean_1, serverSslConfiguration_.SslProtocols_0, serverSslConfiguration_.Boolean_0);
				stream_0 = sslStream;
			}
			else
			{
				stream_0 = innerStream;
			}
			object_0 = new object();
			int_3 = 90000;
			timer_0 = new Timer(onTimeout, this, -1, -1);
			init();
		}

		private void close()
		{
			lock (object_0)
			{
				if (socket_0 == null)
				{
					return;
				}
				disposeTimer();
				disposeRequestBuffer();
				disposeStream();
				closeSocket();
			}
			unbind();
			removeConnection();
		}

		private void closeSocket()
		{
			try
			{
				socket_0.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			socket_0.Close();
			socket_0 = null;
		}

		private void disposeRequestBuffer()
		{
			if (memoryStream_0 != null)
			{
				memoryStream_0.Dispose();
				memoryStream_0 = null;
			}
		}

		private void disposeStream()
		{
			if (stream_0 != null)
			{
				requestStream_0 = null;
				responseStream_0 = null;
				stream_0.Dispose();
				stream_0 = null;
			}
		}

		private void disposeTimer()
		{
			if (timer_0 != null)
			{
				try
				{
					timer_0.Change(-1, -1);
				}
				catch
				{
				}
				timer_0.Dispose();
				timer_0 = null;
			}
		}

		private void init()
		{
			httpListenerContext_0 = new HttpListenerContext(this);
			inputState_0 = InputState.RequestLine;
			requestStream_0 = null;
			lineState_0 = LineState.None;
			responseStream_0 = null;
			int_1 = 0;
			httpListenerPrefix_0 = null;
			memoryStream_0 = new MemoryStream();
		}

		private static void onRead(IAsyncResult iasyncResult_0)
		{
			HttpConnection httpConnection = (HttpConnection)iasyncResult_0.AsyncState;
			if (httpConnection.socket_0 == null)
			{
				return;
			}
			lock (httpConnection.object_0)
			{
				if (httpConnection.socket_0 == null)
				{
					return;
				}
				int num = -1;
				int num2 = 0;
				try
				{
					httpConnection.timer_0.Change(-1, -1);
					num = httpConnection.stream_0.EndRead(iasyncResult_0);
					httpConnection.memoryStream_0.Write(httpConnection.byte_0, 0, num);
					num2 = (int)httpConnection.memoryStream_0.Length;
				}
				catch (Exception ex)
				{
					if (httpConnection.memoryStream_0 != null && httpConnection.memoryStream_0.Length > 0L)
					{
						httpConnection.SendError(ex.Message, 400);
					}
					else
					{
						httpConnection.close();
					}
					return;
				}
				if (num <= 0)
				{
					httpConnection.close();
				}
				else if (httpConnection.processInput(httpConnection.memoryStream_0.GetBuffer(), num2))
				{
					if (!httpConnection.httpListenerContext_0.Boolean_0)
					{
						httpConnection.httpListenerContext_0.HttpListenerRequest_0.FinishInitialization();
					}
					if (httpConnection.httpListenerContext_0.Boolean_0)
					{
						httpConnection.SendError();
						return;
					}
					if (!httpConnection.endPointListener_0.BindContext(httpConnection.httpListenerContext_0))
					{
						httpConnection.SendError("Invalid host", 400);
						return;
					}
					HttpListener httpListener = httpConnection.httpListenerContext_0.HttpListener_0;
					if (httpConnection.httpListener_0 != httpListener)
					{
						httpConnection.removeConnection();
						httpListener.AddConnection(httpConnection);
						httpConnection.httpListener_0 = httpListener;
					}
					httpConnection.bool_0 = true;
					httpListener.RegisterContext(httpConnection.httpListenerContext_0);
				}
				else
				{
					httpConnection.stream_0.BeginRead(httpConnection.byte_0, 0, 8192, onRead, httpConnection);
				}
			}
		}

		private static void onTimeout(object object_1)
		{
			HttpConnection httpConnection = (HttpConnection)object_1;
			httpConnection.close();
		}

		private bool processInput(byte[] byte_1, int int_4)
		{
			if (stringBuilder_0 == null)
			{
				stringBuilder_0 = new StringBuilder(64);
			}
			int int_5 = 0;
			try
			{
				string text;
				while ((text = readLineFrom(byte_1, int_1, int_4, out int_5)) != null)
				{
					int_1 += int_5;
					if (text.Length == 0)
					{
						if (inputState_0 != 0)
						{
							if (int_1 > 32768)
							{
								httpListenerContext_0.String_0 = "Headers too long";
							}
							stringBuilder_0 = null;
							return true;
						}
					}
					else
					{
						if (inputState_0 == InputState.RequestLine)
						{
							httpListenerContext_0.HttpListenerRequest_0.SetRequestLine(text);
							inputState_0 = InputState.Headers;
						}
						else
						{
							httpListenerContext_0.HttpListenerRequest_0.AddHeader(text);
						}
						if (httpListenerContext_0.Boolean_0)
						{
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				httpListenerContext_0.String_0 = ex.Message;
				return true;
			}
			int_1 += int_5;
			if (int_1 >= 32768)
			{
				httpListenerContext_0.String_0 = "Headers too long";
				return true;
			}
			return false;
		}

		private string readLineFrom(byte[] byte_1, int int_4, int int_5, out int int_6)
		{
			int_6 = 0;
			for (int i = int_4; i < int_5; i++)
			{
				if (lineState_0 == LineState.Lf)
				{
					break;
				}
				int_6++;
				byte b = byte_1[i];
				switch (b)
				{
				case 13:
					lineState_0 = LineState.Cr;
					break;
				case 10:
					lineState_0 = LineState.Lf;
					break;
				default:
					stringBuilder_0.Append((char)b);
					break;
				}
			}
			if (lineState_0 == LineState.Lf)
			{
				lineState_0 = LineState.None;
				string result = stringBuilder_0.ToString();
				stringBuilder_0.Length = 0;
				return result;
			}
			return null;
		}

		private void removeConnection()
		{
			if (httpListener_0 != null)
			{
				httpListener_0.RemoveConnection(this);
			}
			else
			{
				endPointListener_0.RemoveConnection(this);
			}
		}

		private void unbind()
		{
			if (bool_0)
			{
				endPointListener_0.UnbindContext(httpListenerContext_0);
				bool_0 = false;
			}
		}

		internal void Close(bool bool_2)
		{
			if (socket_0 == null)
			{
				return;
			}
			lock (object_0)
			{
				if (socket_0 == null)
				{
					return;
				}
				if (!bool_2)
				{
					GetResponseStream().Close(false);
					if (!httpListenerContext_0.HttpListenerResponse_0.Boolean_0 && httpListenerContext_0.HttpListenerRequest_0.FlushInput())
					{
						int_2++;
						disposeRequestBuffer();
						unbind();
						init();
						BeginReadRequest();
						return;
					}
				}
				else if (responseStream_0 != null)
				{
					responseStream_0.Close(true);
				}
				close();
			}
		}

		public void BeginReadRequest()
		{
			if (byte_0 == null)
			{
				byte_0 = new byte[8192];
			}
			if (int_2 == 1)
			{
				int_3 = 15000;
			}
			try
			{
				timer_0.Change(int_3, -1);
				stream_0.BeginRead(byte_0, 0, 8192, onRead, this);
			}
			catch
			{
				close();
			}
		}

		public void Close()
		{
			Close(false);
		}

		public RequestStream GetRequestStream(long long_0, bool bool_2)
		{
			if (requestStream_0 == null && socket_0 != null)
			{
				lock (object_0)
				{
					if (socket_0 == null)
					{
						return requestStream_0;
					}
					byte[] buffer = memoryStream_0.GetBuffer();
					int num = (int)memoryStream_0.Length;
					disposeRequestBuffer();
					if (bool_2)
					{
						httpListenerContext_0.HttpListenerResponse_0.Boolean_3 = true;
						requestStream_0 = new ChunkedRequestStream(stream_0, buffer, int_1, num - int_1, httpListenerContext_0);
					}
					else
					{
						requestStream_0 = new RequestStream(stream_0, buffer, int_1, num - int_1, long_0);
					}
					return requestStream_0;
				}
			}
			return requestStream_0;
		}

		public ResponseStream GetResponseStream()
		{
			if (responseStream_0 == null && socket_0 != null)
			{
				lock (object_0)
				{
					if (socket_0 == null)
					{
						return responseStream_0;
					}
					HttpListener httpListener = httpListenerContext_0.HttpListener_0;
					bool bool_ = httpListener == null || httpListener.Boolean_2;
					responseStream_0 = new ResponseStream(stream_0, httpListenerContext_0.HttpListenerResponse_0, bool_);
					return responseStream_0;
				}
			}
			return responseStream_0;
		}

		public void SendError()
		{
			SendError(httpListenerContext_0.String_0, httpListenerContext_0.Int32_0);
		}

		public void SendError(string string_0, int int_4)
		{
			if (socket_0 == null)
			{
				return;
			}
			lock (object_0)
			{
				if (socket_0 == null)
				{
					return;
				}
				try
				{
					HttpListenerResponse httpListenerResponse_ = httpListenerContext_0.HttpListenerResponse_0;
					httpListenerResponse_.Int32_0 = int_4;
					httpListenerResponse_.String_0 = "text/html";
					StringBuilder stringBuilder = new StringBuilder(64);
					stringBuilder.AppendFormat("<html><body><h1>{0} {1}", int_4, httpListenerResponse_.String_2);
					if (string_0 != null && string_0.Length > 0)
					{
						stringBuilder.AppendFormat(" ({0})</h1></body></html>", string_0);
					}
					else
					{
						stringBuilder.Append("</h1></body></html>");
					}
					Encoding uTF = Encoding.UTF8;
					byte[] bytes = uTF.GetBytes(stringBuilder.ToString());
					httpListenerResponse_.Encoding_0 = uTF;
					httpListenerResponse_.Int64_0 = bytes.LongLength;
					httpListenerResponse_.Close(bytes, true);
				}
				catch
				{
					Close(true);
				}
			}
		}
	}
}
