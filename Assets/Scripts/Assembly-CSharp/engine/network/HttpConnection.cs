using System;

namespace engine.network
{
	public class HttpConnection : BaseConnection
	{
		public override ConnectionType ConnectionType_0
		{
			get
			{
				return ConnectionType.HTTP;
			}
		}

		public override bool Boolean_1
		{
			get
			{
				return false;
			}
		}

		public HttpConnection(string string_2)
			: base(string_2, string.Empty)
		{
		}

		public override void Connect(string string_2)
		{
			throw new NotImplementedException("HTTP connection not inplement!");
		}

		public override void CloseConnect()
		{
			throw new NotImplementedException("HTTP connection not inplement!");
		}

		public override void Send(byte[] byte_0, AbstractNetworkCommand abstractNetworkCommand_0)
		{
			throw new NotImplementedException("HTTP connection not inplement!");
		}
	}
}
