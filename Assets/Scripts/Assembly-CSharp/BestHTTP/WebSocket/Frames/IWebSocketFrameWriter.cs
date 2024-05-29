namespace BestHTTP.WebSocket.Frames
{
	public interface IWebSocketFrameWriter
	{
		WebSocketFrameTypes WebSocketFrameTypes_0 { get; }

		byte[] Get();
	}
}
