using System.Collections.Generic;

namespace BestHTTP.SocketIO.Transports
{
	public interface ITransport
	{
		TransportStates TransportStates_0 { get; }

		SocketManager SocketManager_0 { get; }

		bool Boolean_0 { get; }

		void Open();

		void Poll();

		void Send(Packet packet);

		void Send(List<Packet> packets);

		void Close();
	}
}
