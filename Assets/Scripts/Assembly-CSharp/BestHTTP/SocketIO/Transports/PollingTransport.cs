using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.Logger;

namespace BestHTTP.SocketIO.Transports
{
	internal sealed class PollingTransport : ITransport
	{
		private HTTPRequest httprequest_0;

		private HTTPRequest httprequest_1;

		private Packet packet_0;

		[CompilerGenerated]
		private TransportStates transportStates_0;

		[CompilerGenerated]
		private SocketManager socketManager_0;

		public TransportStates TransportStates_0
		{
			[CompilerGenerated]
			get
			{
				return transportStates_0;
			}
			[CompilerGenerated]
			private set
			{
				transportStates_0 = value;
			}
		}

		public SocketManager SocketManager_0
		{
			[CompilerGenerated]
			get
			{
				return socketManager_0;
			}
			[CompilerGenerated]
			private set
			{
				socketManager_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return httprequest_0 != null;
			}
		}

		public PollingTransport(SocketManager socketManager_1)
		{
			SocketManager_0 = socketManager_1;
		}

		public void Open()
		{
			HTTPRequest hTTPRequest = new HTTPRequest(new Uri(string.Format("{0}?EIO={1}&transport=polling&t={2}-{3}&sid={4}{5}&b64=true", SocketManager_0.Uri_0.ToString(), 4, SocketManager_0.UInt32_0.ToString(), SocketManager_0.UInt64_0++.ToString(), SocketManager_0.HandshakeData_0.String_0, SocketManager_0.SocketOptions_0.Boolean_2 ? string.Empty : SocketManager_0.SocketOptions_0.BuildQueryParams())), OnRequestFinished);
			hTTPRequest.Boolean_3 = true;
			hTTPRequest.Boolean_5 = true;
			hTTPRequest.Send();
			TransportStates_0 = TransportStates.Opening;
		}

		public void Close()
		{
			if (TransportStates_0 != TransportStates.Closed)
			{
				TransportStates_0 = TransportStates.Closed;
			}
		}

		public void Send(Packet packet)
		{
			Send(new List<Packet> { packet });
		}

		public void Send(List<Packet> packets)
		{
			if (TransportStates_0 != TransportStates.Open)
			{
				throw new Exception("Transport is not in Open state!");
			}
			if (Boolean_0)
			{
				throw new Exception("Sending packets are still in progress!");
			}
			byte[] array = null;
			try
			{
				array = packets[0].EncodeBinary();
				for (int i = 1; i < packets.Count; i++)
				{
					byte[] array2 = packets[i].EncodeBinary();
					Array.Resize(ref array, array.Length + array2.Length);
					Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
				}
				packets.Clear();
			}
			catch (Exception ex)
			{
				((IManager)SocketManager_0).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
				return;
			}
			httprequest_0 = new HTTPRequest(new Uri(string.Format("{0}?EIO={1}&transport=polling&t={2}-{3}&sid={4}{5}&b64=true", SocketManager_0.Uri_0.ToString(), 4, SocketManager_0.UInt32_0.ToString(), SocketManager_0.UInt64_0++.ToString(), SocketManager_0.HandshakeData_0.String_0, SocketManager_0.SocketOptions_0.Boolean_2 ? string.Empty : SocketManager_0.SocketOptions_0.BuildQueryParams())), HTTPMethods.Post, OnRequestFinished);
			httprequest_0.Boolean_3 = true;
			httprequest_0.SetHeader("Content-Type", "application/octet-stream");
			httprequest_0.Byte_0 = array;
			httprequest_0.Send();
		}

		private void OnRequestFinished(HTTPRequest httprequest_2, HTTPResponse httpresponse_0)
		{
			httprequest_0 = null;
			if (TransportStates_0 == TransportStates.Closed)
			{
				return;
			}
			string text = null;
			switch (httprequest_2.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
				{
					HTTPManager.ILogger_0.Verbose("PollingTransport", "OnRequestFinished: " + httpresponse_0.String_1);
				}
				if (httpresponse_0.Boolean_0)
				{
					ParseResponse(httpresponse_0);
					break;
				}
				text = string.Format("Polling - Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.Error:
				text = ((httprequest_2.Exception_0 == null) ? "No Exception" : (httprequest_2.Exception_0.Message + "\n" + httprequest_2.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				text = string.Format("Polling - Request({0}) Aborted!", httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = string.Format("Polling - Connection Timed Out! Uri: {0}", httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.TimedOut:
				text = string.Format("Polling - Processing the request({0}) Timed Out!", httprequest_2.Uri_2);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IManager)SocketManager_0).OnTransportError((ITransport)this, text);
			}
		}

		public void Poll()
		{
			if (httprequest_1 == null && TransportStates_0 != TransportStates.Paused)
			{
				httprequest_1 = new HTTPRequest(new Uri(string.Format("{0}?EIO={1}&transport=polling&t={2}-{3}&sid={4}{5}&b64=true", SocketManager_0.Uri_0.ToString(), 4, SocketManager_0.UInt32_0.ToString(), SocketManager_0.UInt64_0++.ToString(), SocketManager_0.HandshakeData_0.String_0, SocketManager_0.SocketOptions_0.Boolean_2 ? string.Empty : SocketManager_0.SocketOptions_0.BuildQueryParams())), HTTPMethods.Get, OnPollRequestFinished);
				httprequest_1.Boolean_3 = true;
				httprequest_1.Boolean_5 = true;
				httprequest_1.Send();
			}
		}

		private void OnPollRequestFinished(HTTPRequest httprequest_2, HTTPResponse httpresponse_0)
		{
			httprequest_1 = null;
			if (TransportStates_0 == TransportStates.Closed)
			{
				return;
			}
			string text = null;
			switch (httprequest_2.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
				{
					HTTPManager.ILogger_0.Verbose("PollingTransport", "OnPollRequestFinished: " + httpresponse_0.String_1);
				}
				if (httpresponse_0.Boolean_0)
				{
					ParseResponse(httpresponse_0);
					break;
				}
				text = string.Format("Polling - Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1, httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.Error:
				text = ((httprequest_2.Exception_0 == null) ? "No Exception" : (httprequest_2.Exception_0.Message + "\n" + httprequest_2.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				text = string.Format("Polling - Request({0}) Aborted!", httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = string.Format("Polling - Connection Timed Out! Uri: {0}", httprequest_2.Uri_2);
				break;
			case HTTPRequestStates.TimedOut:
				text = string.Format("Polling - Processing the request({0}) Timed Out!", httprequest_2.Uri_2);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IManager)SocketManager_0).OnTransportError((ITransport)this, text);
			}
		}

		private void OnPacket(Packet packet_1)
		{
			if (packet_1.Int32_0 != 0 && !packet_1.Boolean_0)
			{
				packet_0 = packet_1;
				return;
			}
			TransportEventTypes transportEventTypes_ = packet_1.TransportEventTypes_0;
			if (transportEventTypes_ == TransportEventTypes.Message && packet_1.SocketIOEventTypes_0 == SocketIOEventTypes.Connect && TransportStates_0 == TransportStates.Opening)
			{
				TransportStates_0 = TransportStates.Open;
				if (!((IManager)SocketManager_0).OnTransportConnected((ITransport)this))
				{
					return;
				}
			}
			((IManager)SocketManager_0).OnPacket(packet_1);
		}

		private void ParseResponse(HTTPResponse httpresponse_0)
		{
			try
			{
				if (httpresponse_0 == null || httpresponse_0.Byte_0 == null || httpresponse_0.Byte_0.Length < 1)
				{
					return;
				}
				string string_ = httpresponse_0.String_1;
				if (string_ == "ok")
				{
					return;
				}
				int num = string_.IndexOf(':', 0);
				int num2 = 0;
				while (num >= 0 && num < string_.Length)
				{
					int num3 = int.Parse(string_.Substring(num2, num - num2));
					string text = string_.Substring(++num, num3);
					if (text.Length > 2 && text[0] == 'b' && text[1] == '4')
					{
						byte[] byte_ = Convert.FromBase64String(text.Substring(2));
						if (packet_0 != null)
						{
							packet_0.AddAttachmentFromServer(byte_, true);
							if (packet_0.Boolean_0)
							{
								try
								{
									OnPacket(packet_0);
								}
								catch (Exception ex)
								{
									HTTPManager.ILogger_0.Exception("PollingTransport", "ParseResponse - OnPacket with attachment", ex);
									((IManager)SocketManager_0).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
								}
								finally
								{
									packet_0 = null;
								}
							}
						}
					}
					else
					{
						try
						{
							Packet packet_ = new Packet(text);
							OnPacket(packet_);
						}
						catch (Exception ex2)
						{
							HTTPManager.ILogger_0.Exception("PollingTransport", "ParseResponse - OnPacket", ex2);
							((IManager)SocketManager_0).EmitError(SocketIOErrors.Internal, ex2.Message + " " + ex2.StackTrace);
						}
					}
					num2 = num + num3;
					num = string_.IndexOf(':', num2);
				}
			}
			catch (Exception ex3)
			{
				((IManager)SocketManager_0).EmitError(SocketIOErrors.Internal, ex3.Message + " " + ex3.StackTrace);
				HTTPManager.ILogger_0.Exception("PollingTransport", "ParseResponse", ex3);
			}
		}
	}
}
