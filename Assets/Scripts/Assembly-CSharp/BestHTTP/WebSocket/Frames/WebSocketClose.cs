using System;
using System.IO;
using System.Text;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketClose : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.ConnectionClose;
			}
		}

		public WebSocketClose()
			: base(null)
		{
		}

		public WebSocketClose(ushort ushort_0, string string_0)
			: base(GetData(ushort_0, string_0))
		{
		}

		private static byte[] GetData(ushort ushort_0, string string_0)
		{
			int byteCount = Encoding.UTF8.GetByteCount(string_0);
			using (MemoryStream memoryStream = new MemoryStream(2 + byteCount))
			{
				byte[] bytes = BitConverter.GetBytes(ushort_0);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes, 0, bytes.Length);
				}
				memoryStream.Write(bytes, 0, bytes.Length);
				bytes = Encoding.UTF8.GetBytes(string_0);
				memoryStream.Write(bytes, 0, bytes.Length);
				return memoryStream.ToArray();
			}
		}
	}
}
