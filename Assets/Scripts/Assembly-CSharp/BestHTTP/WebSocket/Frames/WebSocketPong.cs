namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketPong : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.Pong;
			}
		}

		public WebSocketPong(WebSocketFrameReader webSocketFrameReader_0)
			: base(webSocketFrameReader_0.Byte_1)
		{
		}
	}
}
