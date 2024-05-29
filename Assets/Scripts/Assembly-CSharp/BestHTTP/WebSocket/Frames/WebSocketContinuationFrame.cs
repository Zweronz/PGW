namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketContinuationFrame : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.Continuation;
			}
		}

		public WebSocketContinuationFrame(byte[] byte_2, bool bool_1)
			: base(byte_2, 0uL, (ulong)byte_2.Length, bool_1)
		{
		}

		public WebSocketContinuationFrame(byte[] byte_2, ulong ulong_2, ulong ulong_3, bool bool_1)
			: base(byte_2, ulong_2, ulong_3, bool_1)
		{
		}
	}
}
