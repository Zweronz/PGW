using System;
using System.Net.Sockets;
using ExitGames.Client.Photon;
using UnityEngine;

public class PingMonoEditor : PhotonPing
{
	private Socket socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

	public override bool StartPing(string ip)
	{
		Init();
		try
		{
			socket_0.ReceiveTimeout = 5000;
			socket_0.Connect(ip, 5055);
			PingBytes[PingBytes.Length - 1] = PingId;
			socket_0.Send(PingBytes);
			PingBytes[PingBytes.Length - 1] = (byte)(PingId - 1);
		}
		catch (Exception value)
		{
			socket_0 = null;
			Console.WriteLine(value);
		}
		return false;
	}

	public override bool Done()
	{
		if (!GotResult && socket_0 != null)
		{
			if (socket_0.Available <= 0)
			{
				return false;
			}
			int num = socket_0.Receive(PingBytes, SocketFlags.None);
			if (PingBytes[PingBytes.Length - 1] != PingId || num != PingLength)
			{
				Debug.Log("ReplyMatch is false! ");
			}
			Successful = num == PingBytes.Length && PingBytes[PingBytes.Length - 1] == PingId;
			GotResult = true;
			return true;
		}
		return true;
	}

	public override void Dispose()
	{
		try
		{
			socket_0.Close();
		}
		catch
		{
		}
		socket_0 = null;
	}
}
