using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace ExitGames.Client.Photon
{
	internal class SocketUdp : IPhotonSocket
	{
		private Socket socket_0;

		private readonly object object_0 = new object();

		public SocketUdp(PeerBase peerBase_0)
			: base(peerBase_0)
		{
			if (ReportDebugOfLevel(DebugLevel.ALL))
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "CSharpSocket: UDP, Unity3d.");
			}
			base.Protocol = ConnectionProtocol.Udp;
			PollReceive = false;
		}

		public override bool Connect()
		{
			lock (object_0)
			{
				if (!base.Connect())
				{
					return false;
				}
				base.State = PhotonSocketState.Connecting;
				Thread thread = new Thread(DnsAndConnect);
				thread.Name = "photon dns thread";
				thread.IsBackground = true;
				thread.Start();
				return true;
			}
		}

		public override bool Disconnect()
		{
			if (ReportDebugOfLevel(DebugLevel.INFO))
			{
				EnqueueDebugReturn(DebugLevel.INFO, "CSharpSocket.Disconnect()");
			}
			base.State = PhotonSocketState.Disconnecting;
			lock (object_0)
			{
				if (socket_0 != null)
				{
					try
					{
						socket_0.Close();
						socket_0 = null;
					}
					catch (Exception ex)
					{
						EnqueueDebugReturn(DebugLevel.INFO, "Exception in Disconnect(): " + ex);
					}
				}
			}
			base.State = PhotonSocketState.Disconnected;
			return true;
		}

		public override PhotonSocketError Send(byte[] data, int length)
		{
			lock (object_0)
			{
				if (!socket_0.Connected)
				{
					return PhotonSocketError.Skipped;
				}
				try
				{
					socket_0.Send(data, 0, length, SocketFlags.None);
				}
				catch
				{
					return PhotonSocketError.Exception;
				}
			}
			return PhotonSocketError.Success;
		}

		public override PhotonSocketError Receive(out byte[] data)
		{
			data = null;
			return PhotonSocketError.NoData;
		}

		internal void DnsAndConnect()
		{
			try
			{
				lock (object_0)
				{
					socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					IPAddress ipAddress = IPhotonSocket.GetIpAddress(base.ServerAddress);
					socket_0.Connect(ipAddress, base.ServerPort);
					base.State = PhotonSocketState.Connected;
				}
			}
			catch (SecurityException ex)
			{
				if (ReportDebugOfLevel(DebugLevel.ERROR))
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: " + ex.ToString());
				}
				HandleException(StatusCode.SecurityExceptionOnConnect);
				return;
			}
			catch (Exception ex2)
			{
				if (ReportDebugOfLevel(DebugLevel.ERROR))
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: " + ex2.ToString());
				}
				HandleException(StatusCode.ExceptionOnConnect);
				return;
			}
			Thread thread = new Thread(ReceiveLoop);
			thread.Name = "photon receive thread";
			thread.IsBackground = true;
			thread.Start();
		}

		public void ReceiveLoop()
		{
			byte[] array = new byte[base.MTU];
			while (base.State == PhotonSocketState.Connected)
			{
				try
				{
					int length = socket_0.Receive(array);
					HandleReceivedDatagram(array, length, true);
				}
				catch (Exception ex)
				{
					if (base.State != PhotonSocketState.Disconnecting && base.State != 0)
					{
						if (ReportDebugOfLevel(DebugLevel.ERROR))
						{
							EnqueueDebugReturn(DebugLevel.ERROR, string.Concat("Receive issue. State: ", base.State, " Exception: ", ex));
						}
						HandleException(StatusCode.ExceptionOnReceive);
					}
				}
			}
			Disconnect();
		}
	}
}
