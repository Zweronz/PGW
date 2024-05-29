using System.Text;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketTextFrame : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.Text;
			}
		}

		public WebSocketTextFrame(string string_0)
			: base(Encoding.UTF8.GetBytes(string_0))
		{
		}
	}
}
