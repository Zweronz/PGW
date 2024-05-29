using System.Text;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketPing : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.Ping;
			}
		}

		public WebSocketPing(string string_0)
			: base(Encoding.UTF8.GetBytes(string_0))
		{
		}
	}
}
